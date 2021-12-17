using bsn_sdk_csharp.Enum;
using bsn_sdk_csharp.Models;
using Newtonsoft.Json;
using System;

namespace bsn_sdk_csharp.NodeExtends
{
    public class CitaNodeServer : Client
    {
        public CitaNodeServer(AppSetting _config) : base(_config)
        {
            config = _config;
        }

        public CitaNodeServer(string path)
        {
            AppSetting conf = Config.GetAppSettingFromFile(path);
            base.SetConfig(conf);
        }

        public CitaNodeServer()
        {
            AppSetting conf = Config.GetDefaultConfig();
            base.SetConfig(conf);
        }

        /// <summary>
        ///
        /// </summary>
        private static string registerUserUrl = "/api/cita/v1/user/register";

        /// <summary>
        ///
        /// </summary>
        private string GetBlockHeightUrl = "/api/cita/v1/node/getBlockHeight";

        /// <summary>
        ///
        /// </summary>
        private string ReqChainCodeUrl = "/api/cita/v1/node/reqChainCode";

        /// <summary>
        ///
        /// </summary>
        private string TxReceiptByTxHashUrl = "/api/cita/v1/node/getTxReceiptByTxHash";

        /// <summary>
        ///
        /// </summary>
        private string TxInfoByTxHashUrl = "/api/cita/v1/node/getTxInfoByTxHash";

        /// <summary>
        ///
        /// </summary>
        private string BlockInfoUrl = "/api/cita/v1/node/getBlockInfo";

        /// <summary>
        ///
        /// </summary>
        private string TransUrl = "/api/cita/v1/node/trans";

        /// <summary>
        ///
        /// </summary>
        private string EventRegisterUrl = "/api/cita/v1/event/register";

        /// <summary>
        ///
        /// </summary>
        private string EventQueryUrl = "/api/cita/v1/event/query";

        /// <summary>
        ///
        /// </summary>
        private string EventremoveUrl = "/api/cita/v1/event/remove";

        private ReqHeader GetReqHeader()
        {
            return new ReqHeader()
            {
                userCode = config.userCode,
                appCode = config.appInfo.AppCode
            };
        }

        /// <summary>
        /// register an user
        /// </summary>
        public Tuple<bool, string, CitaRegisterUserResBody> RegisterUser(CitaRegisterReqBody reqBody)
        {
            try
            {
                NodeApiReqBody<CitaRegisterReqBody> req = new NodeApiReqBody<CitaRegisterReqBody>()
                {
                    body = new CitaRegisterReqBody()
                    {
                        UserId = reqBody.UserId,//one user can only be registered once, the second call returns a failed registration
                    },
                    header = GetReqHeader()
                };
                req.mac = sign.Sign(CitaReqMacExtends.GetRegisterUserReqMac(req));
                var res = SendHelper.SendPost<NodeApiResBody<CitaRegisterUserResBody>>(config.reqUrl + registerUserUrl, JsonConvert.SerializeObject(req), config.httpsCert);

                if (res != null)
                {
                    //Check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, CitaRegisterUserResBody>(false, res.header.msg, null);
                    //assemble the original string to verify
                    var datares = CitaResMacExtends.GetRegisterUserResMac(res);
                    //data verified
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, CitaRegisterUserResBody>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, CitaRegisterUserResBody>(false, "failed to verify the signature", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, CitaRegisterUserResBody>(false, "failed to register the user", null); ;
        }

        /// <summary>
        /// get block info
        /// </summary>
        /// <param name="reqBody"></param>
        /// <returns></returns>
        public Tuple<bool, string, CitaBlockData> GetBlockInfo(CitaBlockReqDataBody reqBody)
        {
            try
            {
                NodeApiReqBody<CitaBlockReqDataBody> req = new NodeApiReqBody<CitaBlockReqDataBody>
                {
                    header = GetReqHeader(),
                    body = reqBody
                };
                req.mac = sign.Sign(CitaReqMacExtends.GetCitaBlockInfoReqMac(req));
                var res = SendHelper.SendPost<NodeApiResBody<CitaBlockData>>(config.reqUrl + BlockInfoUrl, JsonConvert.SerializeObject(req), config.httpsCert);

                if (res != null)
                {
                    //Check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, CitaBlockData>(false, res.header.msg, null);
                    //assemble the original string to verify
                    var datares = CitaResMacExtends.GetCitaBlockInfoResMac(res);
                    //data verified
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, CitaBlockData>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, CitaBlockData>(false, "failed to verify the signature", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, CitaBlockData>(false, "failed to get block info", null);
        }

        /// <summary>
        /// Get application block height information
        ///  </summary>
        /// <returns></returns>
        public Tuple<bool, string, CitaBlockHeightResBody> GetBlockHeight()
        {
            try
            {
                NodeApiReq req = new NodeApiReq()
                {
                    header = GetReqHeader()
                };
                req.mac = sign.Sign(CitaReqMacExtends.GetReqHeaderMac(req.header));
                var res = SendHelper.SendPost<NodeApiResBody<CitaBlockHeightResBody>>(config.reqUrl + GetBlockHeightUrl, JsonConvert.SerializeObject(req), config.httpsCert);

                if (res != null)
                {
                    //Check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, CitaBlockHeightResBody>(false, res.header.msg, null);
                    //assemble the original string to verify
                    var datares = CitaResMacExtends.GetBlockHeightResMac(res);
                    //data verified
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, CitaBlockHeightResBody>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, CitaBlockHeightResBody>(false, "failed to verify the signature", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, CitaBlockHeightResBody>(false, "failed to get the block height in the application", null);
        }

        /// <summary>
        /// Get transaction receipt
        /// </summary>
        /// <param name="reqBody"></param>
        /// <returns></returns>
        public Tuple<bool, string, CitaTxReceiptResBody> GetTxReceiptByTxHash(CitaTxReqDataBody reqBody)
        {
            try
            {
                NodeApiReqBody<CitaTxReqDataBody> req = new NodeApiReqBody<CitaTxReqDataBody>
                {
                    header = GetReqHeader(),
                    body = reqBody
                };
                req.mac = sign.Sign(CitaReqMacExtends.GetCitaTxReceiptReqMac(req));
                var res = SendHelper.SendPost<NodeApiResBody<CitaTxReceiptResBody>>(config.reqUrl + TxReceiptByTxHashUrl, JsonConvert.SerializeObject(req), config.httpsCert);

                if (res != null)
                {
                    //Check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, CitaTxReceiptResBody>(false, res.header.msg, null);
                    //assemble the original string to verify
                    var datares = CitaResMacExtends.GetCitaTxReceiptResMac(res);
                    //data verified
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, CitaTxReceiptResBody>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, CitaTxReceiptResBody>(false, "failed to verify the signature", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, CitaTxReceiptResBody>(false, "failed to obtain transaction receipt information", null);
        }

        /// <summary>
        /// get transaction info
        /// </summary>
        /// <param name="reqBody"></param>
        /// <returns></returns>
        public Tuple<bool, string, CitaTransactionData> GetTxInfoByTxHash(CitaTxReqDataBody reqBody)
        {
            try
            {
                NodeApiReqBody<CitaTxReqDataBody> req = new NodeApiReqBody<CitaTxReqDataBody>
                {
                    header = GetReqHeader(),
                    body = reqBody
                };
                req.mac = sign.Sign(CitaReqMacExtends.GetCitaTxReceiptReqMac(req));
                var res = SendHelper.SendPost<NodeApiResBody<CitaTransactionData>>(config.reqUrl + TxInfoByTxHashUrl, JsonConvert.SerializeObject(req), config.httpsCert);

                if (res != null)
                {
                    //Check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, CitaTransactionData>(false, res.header.msg, null);
                    //assemble the original string to verify
                    var datares = CitaResMacExtends.GetCitaTxInfoResMac(res);
                    //data verified
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, CitaTransactionData>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, CitaTransactionData>(false, "failed to verify the signature", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, CitaTransactionData>(false, "failed to get transactions", null);
        }
        /// <summary>
        /// transactions under Key-Trust Mode
        /// </summary>
        /// <param name="reqBody"></param>
        /// <returns></returns>
        public Tuple<bool, string, CitaTransResBody> ReqChainCode(CitaTransReqDataBody reqBody)
        {
            try
            {
                NodeApiReqBody<CitaTransReqDataBody> req = new NodeApiReqBody<CitaTransReqDataBody>
                {
                    header = GetReqHeader(),
                    body = reqBody
                };
                req.mac = sign.Sign(CitaReqMacExtends.GetCitaTransReqMac(req));

                var res = SendHelper.SendPost<NodeApiResBody<CitaTransResBody>>(config.reqUrl + ReqChainCodeUrl, JsonConvert.SerializeObject(req), config.httpsCert);

                if (res != null)
                {
                    //Check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, CitaTransResBody>(false, res.header.msg, null);

                    //assemble the original string to verify
                    var datares = CitaResMacExtends.GetCitaTransactionResMac(res);

                    //data verified
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, CitaTransResBody>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, CitaTransResBody>(false, "failed to verify the signature", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, CitaTransResBody>(false, "The deal failed", null);
        }
        /// <summary>
        /// transaction processing under Key-Upload Mode
        /// </summary>
        /// <param name="reqBody"></param>
        /// <returns></returns>
        public Tuple<bool, string, CitaTransResBody> SDKTrans(CitaTransReq reqBody)
        {
            try
            {
                if (config.appInfo.CAType == EmCAType.Trusteeship)
                {
                    return new Tuple<bool, string, CitaTransResBody>(false, "the trusteeship application cannot call the api", null);
                }
                var tx = new CitaClient(config).GetTransData(reqBody);
                if (!string.IsNullOrEmpty(tx.Item2))
                {
                    return new Tuple<bool, string, CitaTransResBody>(false, tx.Item2, null);
                }

                NodeApiReqBody<CitaTransReqBody> req = new NodeApiReqBody<CitaTransReqBody>
                {
                    header = GetReqHeader()
                };
                req.body = new CitaTransReqBody()
                {
                    ContractName = reqBody.Contract.ContractName,
                    TransData = "0x" + tx.Item1
                };
                req.mac = sign.Sign(CitaReqMacExtends.GetSDKTransReqMac(req));
                var res = SendHelper.SendPost<NodeApiResBody<CitaTransResBody>>(config.reqUrl + TransUrl, JsonConvert.SerializeObject(req), config.httpsCert);

                if (res != null)
                {
                    //Check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, CitaTransResBody>(false, res.header.msg, null);
                    //assemble the original string to verify
                    var datares = CitaResMacExtends.GetCitaTransactionResMac(res);
                    //data verified
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, CitaTransResBody>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, CitaTransResBody>(false, "failed to verify the signature", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, CitaTransResBody>(false, "The deal failed", null);
        }

        /// <summary>
        /// event registration
        /// </summary>
        /// <param name="reqBody"></param>
        /// <returns></returns>
        public Tuple<bool, string, CitaRegisterEventResData> EventRegister(CitaRegisterEventReqDataBody reqBody)
        {
            try
            {
                NodeApiReqBody<CitaRegisterEventReqDataBody> req = new NodeApiReqBody<CitaRegisterEventReqDataBody>
                {
                    header = GetReqHeader(),
                    body = reqBody
                };
                req.mac = sign.Sign(CitaReqMacExtends.GetCitaEventRegisterReqMac(req));
                var res = SendHelper.SendPost<NodeApiResBody<CitaRegisterEventResData>>(config.reqUrl + EventRegisterUrl, JsonConvert.SerializeObject(req), config.httpsCert);

                if (res != null)
                {
                    //Check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, CitaRegisterEventResData>(false, res.header.msg, null);
                    //assemble the original string to verify
                    var datares = CitaResMacExtends.GetCitaEventRegisterResMac(res);
                    //data verified
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, CitaRegisterEventResData>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, CitaRegisterEventResData>(false, "failed to verify the signature", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, CitaRegisterEventResData>(false, "failed to register chaincode event", null);
        }

        /// <summary>
        /// event query
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string, CitaQueryEventResData> EventQuery()
        {
            try
            {
                NodeApiReq req = new NodeApiReq()
                {
                    header = GetReqHeader()
                };
                req.mac = sign.Sign(CitaReqMacExtends.GetReqHeaderMac(req.header));
                var res = SendHelper.SendPost<NodeApiResBody<CitaQueryEventResData>>(config.reqUrl + EventQueryUrl, JsonConvert.SerializeObject(req), config.httpsCert);

                if (res != null)
                {
                    //Check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, CitaQueryEventResData>(false, res.header.msg, null);
                    //assemble the original string to verify
                    var datares = CitaResMacExtends.GetCitaQueryEventResMac(res);
                    //data verified
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, CitaQueryEventResData>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, CitaQueryEventResData>(false, "failed to verify the signature", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, CitaQueryEventResData>(false, "failed to query the chaincode", null);
        }

        /// <summary>
        /// event logout
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string, NodeApiRes> EventRemove(CitaRemoveEventReqDataBody reqBody)
        {
            try
            {
                NodeApiReqBody<CitaRemoveEventReqDataBody> req = new NodeApiReqBody<CitaRemoveEventReqDataBody>()
                {
                    header = GetReqHeader(),
                    body = reqBody
                };
                req.mac = sign.Sign(CitaReqMacExtends.GetCitaEventRemoveReqMac(req));
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