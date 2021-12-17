using bsn_sdk_csharp.Models;
using Newtonsoft.Json;
using System;

namespace bsn_sdk_csharp.NodeExtends
{
    public class FiscoNodeServer : Client
    {
        public FiscoNodeServer(AppSetting _config) : base(_config)
        {
            config = _config;
        }

        public FiscoNodeServer(string path)
        {
            AppSetting conf = Config.GetAppSettingFromFile(path);
            base.SetConfig(conf);
        }

        public FiscoNodeServer()
        {
            AppSetting conf = Config.GetDefaultConfig();
            base.SetConfig(conf);
        }

        /// <summary>
        ///
        /// </summary>
        private static string registerUserUrl = "/api/fiscobcos/v1/user/register";

        /// <summary>
        ///
        /// </summary>
        private string GetBlockHeightUrl = "/api/fiscobcos/v1/node/getBlockHeight";

        /// <summary>
        ///
        /// </summary>
        private string TransUrl = "/api/fiscobcos/v1/node/trans";

        /// <summary>
        ///
        /// </summary>
        private string ReqChainCodeUrl = "/api/fiscobcos/v1/node/reqChainCode";

        /// <summary>
        ///
        /// </summary>
        private string TxReceiptByTxHashUrl = "/api/fiscobcos/v1/node/getTxReceiptByTxHash";

        /// <summary>
        ///
        /// </summary>
        private string TxInfoByTxHashUrl = "/api/fiscobcos/v1/node/getTxInfoByTxHash";

        /// <summary>
        ///
        /// </summary>
        private string BlockInfoUrl = "/api/fiscobcos/v1/node/getBlockInfo";

        /// <summary>
        ///
        /// </summary>
        private string GetTxCountUrl = "/api/fiscobcos/v1/node/getTxCount";

        /// <summary>
        ///
        /// </summary>
        private string GetTxCountByBlockNumberUrl = "/api/fiscobcos/v1/node/getTxCountByBlockNumber";

        /// <summary>
        ///
        /// </summary>
        private string EventRegisterUrl = "/api/fiscobcos/v1/event/register";

        /// <summary>
        ///
        /// </summary>
        private string EventQueryUrl = "/api/fiscobcos/v1/event/query";

        /// <summary>
        ///
        /// </summary>
        private string EventremoveUrl = "/api/fiscobcos/v1/event/remove";

        private ReqHeader GetReqHeader()
        {
            return new ReqHeader()
            {
                userCode = config.userCode,
                appCode = config.appInfo.AppCode
            };
        }

        /// <summary>
        /// Get application block height information
        ///  </summary>
        /// <returns></returns>
        public Tuple<bool, string, GetBlockHeightResBody> GetBlockHeight()
        {
            try
            {
                NodeApiReq req = new NodeApiReq()
                {
                    header = GetReqHeader()
                };
                req.mac = sign.Sign(FiscoReqMacExtends.GetReqHeaderMac(req.header));
                var res = SendHelper.SendPost<NodeApiResBody<GetBlockHeightResBody>>(config.reqUrl + GetBlockHeightUrl, JsonConvert.SerializeObject(req), config.httpsCert);

                if (res != null)
                {
                    //Check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, GetBlockHeightResBody>(false, res.header.msg, null);
                    //assemble the original string to verify
                    var datares = FiscoResMacExtends.GetBlockHeightResMac(res);
                    //data verified
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, GetBlockHeightResBody>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, GetBlockHeightResBody>(false, "failed to verify the signature", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, GetBlockHeightResBody>(false, "failed to get the block height in the application", null);
        }

        /// <summary>
        /// transaction processing under Key-Upload Mode
        /// </summary>
        /// <param name="reqBody"></param>
        /// <returns></returns>
        public Tuple<bool, string, FiscoTransResBody> GetTrans(FiscoTransReq reqBody)
        {
            try
            {
                var tx = new FiscoClient(config).GetTransData(reqBody);
                if (!string.IsNullOrEmpty(tx.Item2))
                {
                    return new Tuple<bool, string, FiscoTransResBody>(false, tx.Item2, null);
                }

                NodeApiReqBody<FiscoTransReqBody> req = new NodeApiReqBody<FiscoTransReqBody>
                {
                    header = GetReqHeader()
                };
                req.body = new FiscoTransReqBody()
                {
                    ContractName = reqBody.Contract.ContractName,
                    TransData = "0x" + tx.Item1
                };
                req.mac = sign.Sign(FiscoReqMacExtends.GetFiscoTransReqMac(req));
                var res = SendHelper.SendPost<NodeApiResBody<FiscoTransResBody>>(config.reqUrl + TransUrl, JsonConvert.SerializeObject(req), config.httpsCert);

                if (res != null)
                {
                    //Check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, FiscoTransResBody>(false, res.header.msg, null);
                    //assemble the original string to verify
                    var datares = FiscoResMacExtends.GetFiscoTransactionResMac(res);
                    //data verified
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, FiscoTransResBody>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, FiscoTransResBody>(false, "failed to verify the signature", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, FiscoTransResBody>(false, "The deal failed", null);
        }

        /// <summary>
        /// register an user
        /// </summary>
        public Tuple<bool, string, FiscoRegisterUserResBody> RegisterUser(FiscoRegisterReqBody reqBody)
        {
            try
            {
                NodeApiReqBody<FiscoRegisterReqBody> req = new NodeApiReqBody<FiscoRegisterReqBody>()
                {
                    body = new FiscoRegisterReqBody()
                    {
                        UserId = reqBody.UserId,//同一个用户名只能注册一次，第二次调用会返回注册失败
                    },
                    header = GetReqHeader()
                };
                //数据进行Base64编码
                req.mac = sign.Sign(FiscoReqMacExtends.GetRegisterUserReqMac(req));
                var res = SendHelper.SendPost<NodeApiResBody<FiscoRegisterUserResBody>>(config.reqUrl + registerUserUrl, JsonConvert.SerializeObject(req), config.httpsCert);

                if (res != null)
                {
                    //Check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, FiscoRegisterUserResBody>(false, res.header.msg, null);
                    //assemble the original string to verify
                    var datares = FiscoResMacExtends.GetRegisterUserResMac(res);
                    //data verified
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, FiscoRegisterUserResBody>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, FiscoRegisterUserResBody>(false, "failed to verify the signature", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, FiscoRegisterUserResBody>(false, "用户注册失败", null); ;
        }
        /// <summary>
        /// transactions under Key-Trust Mode
        /// </summary>
        /// <param name="reqBody"></param>
        /// <returns></returns>
        public Tuple<bool, string, FiscoTransResBody> ReqChainCode(FiscoTransReqDataBody reqBody)
        {
            try
            {
                NodeApiReqBody<FiscoTransReqDataBody> req = new NodeApiReqBody<FiscoTransReqDataBody>
                {
                    header = GetReqHeader(),
                    body = reqBody
                };
                req.mac = sign.Sign(FiscoReqMacExtends.GetFiscoTransReqMac(req));
                var res = SendHelper.SendPost<NodeApiResBody<FiscoTransResBody>>(config.reqUrl + ReqChainCodeUrl, JsonConvert.SerializeObject(req), config.httpsCert);

                if (res != null)
                {
                    //Check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, FiscoTransResBody>(false, res.header.msg, null);
                    //assemble the original string to verify
                    var datares = FiscoResMacExtends.GetFiscoTransactionResMac(res);
                    //data verified
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, FiscoTransResBody>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, FiscoTransResBody>(false, "failed to verify the signature", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, FiscoTransResBody>(false, "The deal failed", null);
        }

        /// <summary>
        /// Get transaction receipt
        /// </summary>
        /// <param name="reqBody"></param>
        /// <returns></returns>
        public Tuple<bool, string, FiscoTxReceiptResBody> GetTxReceiptByTxHash(FiscoTxReqDataBody reqBody)
        {
            try
            {
                NodeApiReqBody<FiscoTxReqDataBody> req = new NodeApiReqBody<FiscoTxReqDataBody>
                {
                    header = GetReqHeader(),
                    body = reqBody
                };
                req.mac = sign.Sign(FiscoReqMacExtends.GetFiscoTxReceiptReqMac(req));
                var res = SendHelper.SendPost<NodeApiResBody<FiscoTxReceiptResBody>>(config.reqUrl + TxReceiptByTxHashUrl, JsonConvert.SerializeObject(req), config.httpsCert);

                if (res != null)
                {
                    //Check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, FiscoTxReceiptResBody>(false, res.header.msg, null);
                    //assemble the original string to verify
                    var datares = FiscoResMacExtends.GetFiscoTxReceiptResMac(res);
                    //data verified
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, FiscoTxReceiptResBody>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, FiscoTxReceiptResBody>(false, "failed to verify the signature", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, FiscoTxReceiptResBody>(false, "failed to obtain transaction receipt information", null);
        }

        /// <summary>
        /// get transaction info
        /// </summary>
        /// <param name="reqBody"></param>
        /// <returns></returns>
        public Tuple<bool, string, FiscoTransactionData> GetTxInfoByTxHash(FiscoTxReqDataBody reqBody)
        {
            try
            {
                NodeApiReqBody<FiscoTxReqDataBody> req = new NodeApiReqBody<FiscoTxReqDataBody>
                {
                    header = GetReqHeader(),
                    body = reqBody
                };
                req.mac = sign.Sign(FiscoReqMacExtends.GetFiscoTxReceiptReqMac(req));
                var res = SendHelper.SendPost<NodeApiResBody<FiscoTransactionData>>(config.reqUrl + TxInfoByTxHashUrl, JsonConvert.SerializeObject(req), config.httpsCert);

                if (res != null)
                {
                    //Check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, FiscoTransactionData>(false, res.header.msg, null);
                    //assemble the original string to verify
                    var datares = FiscoResMacExtends.GetFiscoTxInfoResMac(res);
                    //data verified
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, FiscoTransactionData>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, FiscoTransactionData>(false, "failed to verify the signature", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, FiscoTransactionData>(false, "failed to get transactions", null);
        }

        /// <summary>
        /// get block info
        /// </summary>
        /// <param name="reqBody"></param>
        /// <returns></returns>
        public Tuple<bool, string, FiscoBlockData> GetBlockInfo(FiscoBlockReqDataBody reqBody)
        {
            try
            {
                NodeApiReqBody<FiscoBlockReqDataBody> req = new NodeApiReqBody<FiscoBlockReqDataBody>
                {
                    header = GetReqHeader(),
                    body = reqBody
                };
                req.mac = sign.Sign(FiscoReqMacExtends.GetFiscoBlockInfoReqMac(req));
                var res = SendHelper.SendPost<NodeApiResBody<FiscoBlockData>>(config.reqUrl + BlockInfoUrl, JsonConvert.SerializeObject(req), config.httpsCert);

                if (res != null)
                {
                    //Check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, FiscoBlockData>(false, res.header.msg, null);
                    //assemble the original string to verify
                    var datares = FiscoResMacExtends.GetFiscoBlockInfoResMac(res);
                    //data verified
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, FiscoBlockData>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, FiscoBlockData>(false, "failed to verify the signature", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, FiscoBlockData>(false, "failed to get block info", null);
        }

        /// <summary>
        /// gets the total number of transactions in the app
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string, GetBlockHeightResBody> GetTxCount()
        {
            try
            {
                NodeApiReq req = new NodeApiReq()
                {
                    header = GetReqHeader()
                };
                req.mac = sign.Sign(FiscoReqMacExtends.GetReqHeaderMac(req.header));
                var res = SendHelper.SendPost<NodeApiResBody<GetBlockHeightResBody>>(config.reqUrl + GetTxCountUrl, JsonConvert.SerializeObject(req), config.httpsCert);

                if (res != null)
                {
                    //Check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, GetBlockHeightResBody>(false, res.header.msg, null);
                    //assemble the original string to verify
                    var datares = FiscoResMacExtends.GetBlockHeightResMac(res);
                    //data verified
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, GetBlockHeightResBody>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, GetBlockHeightResBody>(false, "failed to verify the signature", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, GetBlockHeightResBody>(false, "failed to get the total number of transactions in the app", null);
        }

        /// <summary>
        /// gets the total number of transactions in the block
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string, GetBlockHeightResBody> GetTxCountByBlockNumber(string blockNumber)
        {
            try
            {
                NodeApiReqBody<FiscoBlockReqDataBody> req = new NodeApiReqBody<FiscoBlockReqDataBody>
                {
                    header = GetReqHeader(),
                    body = new FiscoBlockReqDataBody()
                    {
                        BlockNumber = blockNumber
                    }
                };
                req.mac = sign.Sign(FiscoReqMacExtends.GetFiscoBlockInfoReqMac(req));
                var res = SendHelper.SendPost<NodeApiResBody<GetBlockHeightResBody>>(config.reqUrl + GetTxCountByBlockNumberUrl, JsonConvert.SerializeObject(req), config.httpsCert);

                if (res != null)
                {
                    //Check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, GetBlockHeightResBody>(false, res.header.msg, null);
                    //assemble the original string to verify
                    var datares = FiscoResMacExtends.GetBlockHeightResMac(res);
                    //data verified
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, GetBlockHeightResBody>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, GetBlockHeightResBody>(false, "failed to verify the signature", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, GetBlockHeightResBody>(false, "failed to get the total number of transactions in the block", null);
        }

        /// <summary>
        /// event registration
        /// </summary>
        /// <param name="reqBody"></param>
        /// <returns></returns>
        public Tuple<bool, string, RegisterEventResData> EventRegister(FiscoRegisterReqDataBody reqBody)
        {
            try
            {
                NodeApiReqBody<FiscoRegisterReqDataBody> req = new NodeApiReqBody<FiscoRegisterReqDataBody>
                {
                    header = GetReqHeader(),
                    body = reqBody
                };
                req.mac = sign.Sign(FiscoReqMacExtends.GetFiscoEventRegisterReqMac(req));
                var res = SendHelper.SendPost<NodeApiResBody<RegisterEventResData>>(config.reqUrl + EventRegisterUrl, JsonConvert.SerializeObject(req), config.httpsCert);

                if (res != null)
                {
                    //Check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, RegisterEventResData>(false, res.header.msg, null);
                    //assemble the original string to verify
                    var datares = FiscoResMacExtends.GetFiscoEventRegisterResMac(res);
                    //data verified
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, RegisterEventResData>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, RegisterEventResData>(false, "failed to verify the signature", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, RegisterEventResData>(false, "failed to register chaincode event", null);
        }

        /// <summary>
        /// event  query
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string, QueryEventResData> EventQuery()
        {
            try
            {
                NodeApiReq req = new NodeApiReq()
                {
                    header = GetReqHeader()
                };
                req.mac = sign.Sign(FiscoReqMacExtends.GetReqHeaderMac(req.header));
                var res = SendHelper.SendPost<NodeApiResBody<QueryEventResData>>(config.reqUrl + EventQueryUrl, JsonConvert.SerializeObject(req), config.httpsCert);

                if (res != null)
                {
                    //Check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, QueryEventResData>(false, res.header.msg, null);
                    //assemble the original string to verify
                    var datares = FiscoResMacExtends.GetFiscoQueryEventResMac(res);
                    //data verified
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, QueryEventResData>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, QueryEventResData>(false, "failed to verify the signature", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, QueryEventResData>(false, "failed to query the chaincode", null);
        }

        /// <summary>
        /// event  logout
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string, NodeApiRes> EventRemove(RemoveReqDataBody reqBody)
        {
            try
            {
                NodeApiReqBody<RemoveReqDataBody> req = new NodeApiReqBody<RemoveReqDataBody>()
                {
                    header = GetReqHeader(),
                    body = reqBody
                };
                req.mac = sign.Sign(FiscoReqMacExtends.GetFiscoEventRemoveReqMac(req));
                var res = SendHelper.SendPost<NodeApiRes>(config.reqUrl + EventremoveUrl, JsonConvert.SerializeObject(req), config.httpsCert);

                if (res != null)
                {
                    //Check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, NodeApiRes>(false, res.header.msg, null);
                    //assemble the original string to verify
                    var datares = ResMacExtends.GetResHeaderMac(res.header);
                    //data verified
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, NodeApiRes>(true, res.header.msg, res);
                    }
                    else
                    {
                        return new Tuple<bool, string, NodeApiRes>(false, "failed to verify the signature", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, NodeApiRes>(false, "failed to logout event chaincode", null);
        }
    }
}