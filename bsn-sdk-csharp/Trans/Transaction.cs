using bsn_sdk_csharp.Ecdsa;
using Common;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Protos;
using System;
using System.Collections.Generic;

namespace bsn_sdk_csharp.Trans
{
    public class Transaction
    {
        public static string CreateRequest(AppSetting config, TransRequest request)
        {
            var header = new TransactionHeader(config.appInfo.MspId, request.EnrollmentCertificate, config.appInfo.ChannelId);
            var proposal = createInvokerProposal(request, header);
            var pb_proposal = signProposal(proposal.Proposal, request.PrivateKey);
            var res = Util.Marshal(pb_proposal).ToBase64();
            return res;
        }

        public static TransactionProposal createInvokerProposal(TransRequest request, TransactionHeader transactionHeader)
        {
            var args = new RepeatedField<ByteString>();
            args.Add(Util.ConvertToByteString(request.Fcn));
            foreach (var item in request.Args)
            {
                args.Add(Util.ConvertToByteString(item));
            }
            var ccis = new ChaincodeSpec()
            {
                Type = ChaincodeSpec.Types.Type.Undefined,
                ChaincodeId = new ChaincodeID() { Name = request.ChaincodeId },
                Input = new ChaincodeInput()
                {
                    Args = args,
                }
            };
            var proposal = CreateChaincodeProposalWithTxIDNonceAndTransient(ccis, transactionHeader, request.TransientMap);
            var tp = new TransactionProposal()
            {
                TxnID = transactionHeader.Id,
                Proposal = proposal
            };
            return tp;
        }

        private static Proposal CreateChaincodeProposalWithTxIDNonceAndTransient(ChaincodeSpec ccis, TransactionHeader transactionHeader, Dictionary<string, string> transientMap)
        {
            var ccHdrExt = new ChaincodeHeaderExtension()
            {
                ChaincodeId = ccis.ChaincodeId
            };
            var ccHdrExtBytes = Util.Marshal(ccHdrExt);
            var cis = new ChaincodeInvocationSpec()
            {
                ChaincodeSpec = ccis
            };

            var cisBytes = Util.Marshal(cis);
            var ccPropPayload = new ChaincodeProposalPayload()
            {
                Input = cisBytes,
                TransientMap = Util.ConvertMapField(transientMap)
            };
            var ccPropPayloadBytes = Util.Marshal(ccPropPayload);
            var timestamp = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow);

            // TODO: epoch is now set to zero. This must be changed once we
            // get a more appropriate mechanism to handle it in.
            ulong epoch = 0;
            var hdr = new Header()
            {
                ChannelHeader = Util.Marshal(new ChannelHeader()
                {
                    Type = (int)HeaderType.EndorserTransaction,
                    TxId = transactionHeader.Id,
                    Timestamp = timestamp,
                    ChannelId = transactionHeader.ChannelID,
                    Extension = ccHdrExtBytes,
                    Epoch = epoch
                }),
                SignatureHeader = Util.Marshal(new SignatureHeader()
                {
                    Nonce = transactionHeader.Nonce,
                    Creator = transactionHeader.Creator
                })
            };
            var hdrBytes = Util.Marshal(hdr);
            var prop = new Proposal()
            {
                Header = hdrBytes,
                Payload = ccPropPayloadBytes
            };
            return prop;
        }

        private static SignedProposal signProposal(Proposal proposal, string prikey)
        {
            var proposalBytes = Util.Marshal(proposal);
            var signature = Util.ConvertToByteString(ECDSAHelper.SignData(proposalBytes, prikey));

            return new SignedProposal()
            {
                ProposalBytes = proposalBytes,
                Signature = signature
            };
        }
    }
}