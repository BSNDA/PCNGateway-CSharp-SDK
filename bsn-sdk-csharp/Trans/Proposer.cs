using Protos;

namespace bsn_sdk_csharp.Trans
{
    public class TransactionProposal
    {
        public string TxnID { get; set; }

        public Proposal Proposal { get; set; }
    }
}