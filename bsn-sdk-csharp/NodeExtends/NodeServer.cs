using bsn_sdk_csharp.Csr;
using bsn_sdk_csharp.Ecdsa;
using bsn_sdk_csharp.Enum;
using bsn_sdk_csharp.Models;
using bsn_sdk_csharp.Trans;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace bsn_sdk_csharp.NodeExtends
{
    /// <summary>
    /// Node Gateway Operations
    /// </summary>
    public class NodeServer : Client
    {
        public NodeServer(AppSetting _config) : base(_config)
        {
            config = _config;
        }

        public NodeServer(string path)
        {
            AppSetting conf = Config.GetAppSettingFromFile(path);
            base.SetConfig(conf);
        }

        public NodeServer()
        {
            AppSetting conf = Config.GetDefaultConfig();
            base.SetConfig(conf);
        }

        /// <summary>
        /// User registration URL
        /// </summary>
        private static string registerUserUrl = "/api/fabric/v1/user/register";

        /// <summary>
        /// user cert registration URL
        /// </summary>
        private static string EnrollUserUrl = "/api/fabric/v1/user/enroll";

        /// <summary>
        /// get transaction URL
        /// </summary>
        private static string GetTransUrl = "/api/fabric/v1/node/getTransaction";

        /// <summary>
        /// get transaction data URL
        /// </summary>
        private static string GetTransDataUrl = "/api/fabric/v1/node/getTransdata";

        /// <summary>
        /// get blockinfo URL
        /// </summary>
        private static string GetBlockUrl = "/api/fabric/v1/node/getBlockInfo";
        /// <summary>
        /// get blockinfo URL
        /// </summary>
        private static string GetBlockDataUrl = "/api/fabric/v1/node/getBlockData";

        /// <summary>
        /// get ledgerinfo URL
        /// </summary>
        private static string GetLedgerUrl = "/api/fabric/v1/node/getLedgerInfo";

        /// <summary>
        /// chaincode event registration URL
        /// </summary>
        private static string EventRegisterUrl = "/api/fabric/v1/chainCode/event/register";

        /// <summary>
        ///  chaincode event query URL
        /// </summary>
        private static string EventQueryUrl = "/api/fabric/v1/chainCode/event/query";

        /// <summary>
        /// event chaincode logout URL
        /// </summary>
        private static string EventRemoveUrl = "/api/fabric/v1/chainCode/event/remove";

        /// <summary>
        /// transaction processing under Key-Trust Mode URL
        /// </summary>
        private static string ReqChainCodeUrl = "/api/fabric/v1/node/reqChainCode";

        /// <summary>
        /// transaction processing under Public-Key-Upload Mode URL
        /// </summary>
        private static string TransUrl = "/api/fabric/v1/node/trans";

        /// <summary>
        /// user registration
        /// </summary>
        /// <param name="req">request content</param>
        /// <param name="url">interface address</param>
        /// <param name="certPath">https cert path</param>
        /// <returns></returns>
        public Tuple<bool, string, RegisterUserResBody> RegisterUser(RegisterUserReqBody reqBody)
        {
            try
            {
                NodeApiReqBody<RegisterUserReqBody> req = new NodeApiReqBody<RegisterUserReqBody>()
                {
                    body = new RegisterUserReqBody()
                    {
                        name = reqBody.name,//one user can only be registered once, the second call returns a failed registration
                        secret = reqBody.secret,//If the password is empty, a random password will be returned. Users under Key Mode needs to store the returned random password and pass it in when registering the certificate
                        extendProperties=reqBody.extendProperties
                    },
                    header = new ReqHeader()
                    {
                        appCode = config.appInfo.AppCode,
                        userCode = config.userCode
                    }
                };
                // assemble the orginal string to sign
                var data = ReqMacExtends.GetRegisterUserReqMac(req);
                //data signature
                req.mac = sign.Sign(data);

                var res = SendHelper.SendPost<NodeApiResBody<RegisterUserResBody>>(config.reqUrl + registerUserUrl, JsonConvert.SerializeObject(req), config.httpsCert);

                if (res != null)
                {
                    //Check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, RegisterUserResBody>(false, res.header.msg, null);
                    //assemble the original string to verify
                    var datares = ResMacExtends.GetRegisterUserResMac(res);
                    //data verified
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, RegisterUserResBody>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, RegisterUserResBody>(false, "failed to verify the signature", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, RegisterUserResBody>(false, "failed to register the user", null); ;
        }

        /// <summary>
        /// user cert application
        /// </summary>
        /// <param name="config">basic information</param>
        /// <param name="reqBody">user cert requests</param>
        /// <returns></returns>
        public Tuple<bool, string> EnrollUser(EnrollUserReqBody reqBody)
        {
            try
            {
                NodeApiReqBody<EnrollUserReqBody> req = new NodeApiReqBody<EnrollUserReqBody>()
                {
                    body = new EnrollUserReqBody()
                    {
                        name = reqBody.name,
                        secret = reqBody.secret
                    },
                    header = new ReqHeader()
                    {
                        appCode = config.appInfo.AppCode,
                        userCode = config.userCode
                    }
                };
                ////get csr
                var resCsr = config.appInfo.AlgorithmType == EmAlgorithmType.SM2 ?
                  CsrHelper.GetSMCsr(string.Format("{0}@{1}", reqBody.name, config.appInfo.AppCode))
                  : CsrHelper.GetCsr(string.Format("{0}@{1}", reqBody.name, config.appInfo.AppCode));
                req.body.csrPem = resCsr.Item1.Replace("\r","");
                // assemble the original string to sign
                var data = ReqMacExtends.GetEnrollUserReqMac(req);
                req.mac = sign.Sign(data);
                var res = SendHelper.SendPost<NodeApiResBody<EnrollUserResBody>>(config.reqUrl + EnrollUserUrl, JsonConvert.SerializeObject(req), config.httpsCert);
                if (res != null)
                {
                    //check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string>(false, res.header.msg);
                    //assemble the original string to sign
                    var datares = ResMacExtends.GetEnrollUserResMac(res);
                    //verify data
                    if (sign.Verify(res.mac, datares))
                    {
                        //save the private key and cert
                        if (!string.IsNullOrEmpty(res.body.cert))
                        {
                            CertStore.SaveCert(res.body.cert, Path.Combine(config.mspDir, string.Format("{0}@{1}_cert.pem", reqBody.name, config.appInfo.AppCode)));
                            ECDSAStore.SavePriKey(resCsr.Item2, Path.Combine(config.mspDir, string.Format("{0}@{1}_sk.pem", reqBody.name, config.appInfo.AppCode)));
                        }
                        return new Tuple<bool, string>(true, "cert registration successful");
                    }
                    else
                    {
                        return new Tuple<bool, string>(false, "failed to verify the signature");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string>(false, "failed to verify the cert");
        }

        /// <summary>
        /// get transaction info
        /// </summary>
        /// <param name="config"></param>
        /// <param name="reqBody"></param>
        /// <returns></returns>
        public Tuple<bool, string, GetTransResBody> GetTransaction(GetTransReqBody reqBody)
        {
            try
            {
                NodeApiReqBody<GetTransReqBody> req = new NodeApiReqBody<GetTransReqBody>()
                {
                    body = new GetTransReqBody()
                    {
                        txId = reqBody.txId
                    },
                    header = new ReqHeader()
                    {
                        appCode = config.appInfo.AppCode,
                        userCode = config.userCode
                    }
                };
                //assemble the string to sign
                var data = ReqMacExtends.GetTransactionReqMac(req);
                //sign the data
                req.mac = sign.Sign(data);

                var res = SendHelper.SendPost<NodeApiResBody<GetTransResBody>>(config.reqUrl + GetTransUrl, JsonConvert.SerializeObject(req), config.httpsCert);
                if (res != null)
                {
                    //check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, GetTransResBody>(false, res.header.msg, null);
                    //assemble the original string to sign
                    var datares = ResMacExtends.GetTransactionResMac(res);
                    //verify data
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, GetTransResBody>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, GetTransResBody>(false, "failed to sign", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, GetTransResBody>(false, "failed to get transactions", null);
        }
        /// <summary>
        /// get transaction data
        /// </summary>
        /// <param name="config"></param>
        /// <param name="reqBody"></param>
        /// <returns></returns>
        public Tuple<bool, string, GetTransDataResBody> GetTransactionData(GetTransReqBody reqBody)
        {
            try
            {
                NodeApiReqBody<GetTransReqBody> req = new NodeApiReqBody<GetTransReqBody>()
                {
                    body = new GetTransReqBody()
                    {
                        txId = reqBody.txId
                    },
                    header = new ReqHeader()
                    {
                        appCode = config.appInfo.AppCode,
                        userCode = config.userCode
                    }
                };
                //assemble the string to sign
                var data = ReqMacExtends.GetTransactionReqMac(req);
                //sign the data
                req.mac = sign.Sign(data);

                var res = SendHelper.SendPost<NodeApiResBody<GetTransDataResBody>>(config.reqUrl + GetTransDataUrl, JsonConvert.SerializeObject(req), config.httpsCert);
                if (res != null)
                {
                    //check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, GetTransDataResBody>(false, res.header.msg, null);
                    //assemble the original string to sign
                    var datares = ResMacExtends.GetTransactionDataResMac(res);
                    //verify data
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, GetTransDataResBody>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, GetTransDataResBody>(false, "failed to sign", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, GetTransDataResBody>(false, "failed to get transactions", null);
        }

        /// <summary>
        /// get block info
        /// </summary>
        /// <param name="config"></param>
        /// <param name="reqBody"></param>
        /// <returns></returns>
        public Tuple<bool, string, GetBlockResBody> GetBlockInfo(GetBlockReqBody reqBody)
        {
            try
            {
                NodeApiReqBody<GetBlockReqBody> req = new NodeApiReqBody<GetBlockReqBody>()
                {
                    body = new GetBlockReqBody()
                    {
                        txId = reqBody.txId,
                        blockHash = reqBody.blockHash,
                        blockNumber = reqBody.blockNumber
                    },
                    header = new ReqHeader()
                    {
                        appCode = config.appInfo.AppCode,
                        userCode = config.userCode
                    }
                };
                //assemble the string to sign
                var data = ReqMacExtends.GetBlockInfoReqMac(req);
                //sign the data
                req.mac = sign.Sign(data);

                var res = SendHelper.SendPost<NodeApiResBody<GetBlockResBody>>(config.reqUrl + GetBlockUrl, JsonConvert.SerializeObject(req), config.httpsCert);
                if (res != null)
                {
                    //Check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, GetBlockResBody>(false, res.header.msg, null);
                    //Assemble the original string to sign
                    var datares = ResMacExtends.GetBlockInfoResMac(res);

                    //verify data
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, GetBlockResBody>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, GetBlockResBody>(false, "failed to verify the signature", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, GetBlockResBody>(false, "failed to get block info", null);
        }
        /// <summary>
        /// get block data
        /// </summary>
        /// <param name="config"></param>
        /// <param name="reqBody"></param>
        /// <returns></returns>
        public Tuple<bool, string, GetBlockDataResBody> GetBlockData(GetBlockReqBody reqBody)
        {
            try
            {
                NodeApiReqBody<GetBlockReqBody> req = new NodeApiReqBody<GetBlockReqBody>()
                {
                    body = new GetBlockReqBody()
                    {
                        txId = reqBody.txId,
                        blockHash = reqBody.blockHash,
                        blockNumber = reqBody.blockNumber,
                        dataType=reqBody.dataType
                    },
                    header = new ReqHeader()
                    {
                        appCode = config.appInfo.AppCode,
                        userCode = config.userCode
                    }
                };
                //assemble the string to sign
                var data = ReqMacExtends.GetBlockInfoReqMac(req);
                //sign the data
                req.mac = sign.Sign(data);

                var res = SendHelper.SendPost<NodeApiResBody<GetBlockDataResBody>>(config.reqUrl + GetBlockDataUrl, JsonConvert.SerializeObject(req), config.httpsCert);
                if (res != null)
                {
                    //Check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, GetBlockDataResBody>(false, res.header.msg, null);
                    //Assemble the original string to sign
                    var datares = ResMacExtends.GetBlockDataResMac(res);

                    //verify data
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, GetBlockDataResBody>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, GetBlockDataResBody>(false, "failed to verify the signature", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, GetBlockDataResBody>(false, "failed to get block data", null);
        }

        /// <summary>
        /// get the ledger info
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public Tuple<bool, string, GetLedgerResBody> GetLedgerInfo()
        {
            try
            {
                NodeApiReq req = new NodeApiReq()
                {
                    header = new ReqHeader()
                    {
                        appCode = config.appInfo.AppCode,
                        userCode = config.userCode
                    }
                };
                //assemble the original string to sign
                var data = ReqMacExtends.GetReqHeaderMac(req.header);
                //sign data
                req.mac = sign.Sign(data);

                var res = SendHelper.SendPost<NodeApiResBody<GetLedgerResBody>>(config.reqUrl + GetLedgerUrl, JsonConvert.SerializeObject(req), config.httpsCert);
                if (res != null)
                {
                    //check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, GetLedgerResBody>(false, res.header.msg, null);
                    //assemble the original strong to sign
                    var datares = ResMacExtends.GetLedgerInfoResMac(res);

                    //verify data
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, GetLedgerResBody>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, GetLedgerResBody>(false, "failed to sign ", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, GetLedgerResBody>(false, "failed to get ledger info", null);
        }

        /// <summary>
        /// chaincode event registration
        /// </summary>
        /// <param name="config"></param>
        /// <param name="reqBody"></param>
        /// <returns></returns>
        public Tuple<bool, string, EventRegisterResBody> EventRegister(EventRegisterReqBody reqBody)
        {
            try
            {
                NodeApiReqBody<EventRegisterReqBody> req = new NodeApiReqBody<EventRegisterReqBody>()
                {
                    header = new ReqHeader()
                    {
                        appCode = config.appInfo.AppCode,
                        userCode = config.userCode
                    },
                    body = new EventRegisterReqBody()
                    {
                        attachArgs = reqBody.attachArgs,
                        chainCode = reqBody.chainCode,
                        eventKey = reqBody.eventKey,
                        notifyUrl = reqBody.notifyUrl
                    }
                };
                //assemble the original string to sign
                var data = ReqMacExtends.EventRegisterReqMac(req);
                //sign data
                req.mac = sign.Sign(data);

                var res = SendHelper.SendPost<NodeApiResBody<EventRegisterResBody>>(config.reqUrl + EventRegisterUrl, JsonConvert.SerializeObject(req), config.httpsCert);
                if (res != null)
                {
                    //check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, EventRegisterResBody>(false, res.header.msg, null);
                    //assemble the original string to be checked
                    var datares = ResMacExtends.EventRegisterResMac(res);

                    //verify data
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, EventRegisterResBody>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, EventRegisterResBody>(false, "failed to verify the signature", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, EventRegisterResBody>(false, "failed to register chaincode event", null);
        }

        /// <summary>
        /// event chaincode query
        /// </summary>
        /// <param name="config"></param>
        /// <param name="reqBody"></param>
        /// <returns></returns>
        public Tuple<bool, string, List<EventQueryResBody>> EventQuery()
        {
            try
            {
                NodeApiReq req = new NodeApiReq
                {
                    header = new ReqHeader()
                    {
                        appCode = config.appInfo.AppCode,
                        userCode = config.userCode
                    }
                };
                //assemble the original string
                var data = ReqMacExtends.GetReqHeaderMac(req.header);
                //sign data
                req.mac = sign.Sign(data);

                var res = SendHelper.SendPost<NodeApiResBody<List<EventQueryResBody>>>(config.reqUrl + EventQueryUrl, JsonConvert.SerializeObject(req), config.httpsCert);
                if (res != null)
                {
                    //Check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, List<EventQueryResBody>>(false, res.header.msg, null);
                    //assemble the original string to verify
                    var datares = ResMacExtends.EventQueryResMac(res);
                    //verify data
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, List<EventQueryResBody>>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, List<EventQueryResBody>>(false, "failed to verify the signature", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, List<EventQueryResBody>>(false, "failed to query the chaincode", null);
        }

        /// <summary>
        /// event chaincode logout
        /// </summary>
        /// <param name="config"></param>
        /// <param name="reqBody"></param>
        /// <returns></returns>
        public Tuple<bool, string> EventRemove(EventRemoveReqBody reqBody)
        {
            try
            {
                NodeApiReqBody<EventRemoveReqBody> req = new NodeApiReqBody<EventRemoveReqBody>
                {
                    header = new ReqHeader()
                    {
                        appCode = config.appInfo.AppCode,
                        userCode = config.userCode
                    },
                    body = new EventRemoveReqBody()
                    {
                        eventId = reqBody.eventId
                    }
                };
                //assemble the original string to sign
                var data = ReqMacExtends.EventRemoveReqMac(req);
                //sign data
                req.mac = sign.Sign(data);

                var res = SendHelper.SendPost<NodeApiRes>(config.reqUrl + EventRemoveUrl, JsonConvert.SerializeObject(req), config.httpsCert);
                if (res != null)
                {
                    //check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string>(false, res.header.msg);
                    //assemble the original string to sign
                    var datares = ResMacExtends.GetResHeaderMac(res.header);

                    //verify data
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string>(true, res.header.msg);
                    }
                    else
                    {
                        return new Tuple<bool, string>(false, "failed to sign the signature");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string>(false, "failed to logout event chaincode");
        }

        /// <summary>
        /// transactions under Key-Trust Mode
        /// </summary>
        /// <param name="config"></param>
        /// <param name="reqBody"></param>
        /// <returns></returns>
        public Tuple<bool, string, ReqChainCodeResBody> ReqChainCode(ReqChainCodeReqBody reqBody)
        {
            try
            {
                NodeApiReqBody<ReqChainCodeReqBody> req = new NodeApiReqBody<ReqChainCodeReqBody>()
                {
                    header = new ReqHeader()
                    {
                        appCode = config.appInfo.AppCode,
                        userCode = config.userCode
                    },
                    body = reqBody
                };
                //assemble the original string to sign
                var data = ReqMacExtends.ReqChainCodeReqMac(req);
                //sign data
                req.mac = sign.Sign(data);

                var res = SendHelper.SendPost<NodeApiResBody<ReqChainCodeResBody>>(config.reqUrl + ReqChainCodeUrl, JsonConvert.SerializeObject(req), config.httpsCert);
                if (res != null)
                {
                    //check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, ReqChainCodeResBody>(false, res.header.msg, null);
                    //assemble the original string to sign
                    var datares = ResMacExtends.ReqChainCodeResMac(res);

                    //verify data
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, ReqChainCodeResBody>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, ReqChainCodeResBody>(false, "failed to verify", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, ReqChainCodeResBody>(false, "failed to transact under Key-Trust Mode", null);
        }

        /// <summary>
        /// transaction processing under Key-Upload Mode
        /// </summary>
        /// <param name="config"></param>
        /// <param name="reqBody"></param>
        /// <returns></returns>
        public Tuple<bool, string, ReqChainCodeResBody> Trans(TransRequest reqBody)
        {
            try
            {
                NodeApiReqBody<TransReqBody> req = new NodeApiReqBody<TransReqBody>()
                {
                    header = new ReqHeader()
                    {
                        appCode = config.appInfo.AppCode,
                        userCode = config.userCode
                    },
                    body = new TransReqBody()
                    {
                        transData = Transaction.CreateRequest(config, reqBody)
                    }
                };
                //Assemble the original string to sign
                var data = ReqMacExtends.TransReqMac(req);
                //sign data
                req.mac = sign.Sign(data);

                var res = SendHelper.SendPost<NodeApiResBody<ReqChainCodeResBody>>(config.reqUrl + TransUrl, JsonConvert.SerializeObject(req), config.httpsCert);
                if (res != null)
                {
                    //check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, ReqChainCodeResBody>(false, res.header.msg, null);
                    //assemble the original string to sign
                    var datares = ResMacExtends.ReqChainCodeResMac(res);

                    //verify data
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, ReqChainCodeResBody>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, ReqChainCodeResBody>(false, "failed to verify data", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, ReqChainCodeResBody>(false, "failed to process transactions under Key-Trust Mode", null);
        }
    }
}