using bsn_sdk_csharp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace bsn_sdk_csharp.NodeExtends
{
    public class CitaNodeServer : Client
    {
        public CitaNodeServer(AppSetting _config) : base(_config)
        {
            config = _config;
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
    }
}