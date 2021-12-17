using bsn_sdk_csharp.Models;
using Newtonsoft.Json;
using System;

namespace bsn_sdk_csharp.NodeExtends
{
    public class XuperchainNodeServer : Client
    {
        public XuperchainNodeServer(AppSetting _config) : base(_config)
        {
            config = _config;
        }

        public XuperchainNodeServer(string path)
        {
            AppSetting conf = Config.GetAppSettingFromFile(path);
            base.SetConfig(conf);
        }

        public XuperchainNodeServer()
        {
            AppSetting conf = Config.GetDefaultConfig();
            base.SetConfig(conf);
        }

        private static string registerUserUrl = "/api/xuperchain/v1/user/register";

        private string ReqChainCodeUrl = "/api/xuperchain/v1/node/reqChainCode";
        private string GetTxInfoUrl = "/api/xuperchain/v1/node/getTxInfoByTxHash";
        private string GetBlockInfoUrl = "/api/xuperchain/v1/node/getBlockInfo";

        private ReqHeader GetReqHeader()
        {
            return new ReqHeader()
            {
                userCode = config.userCode,
                appCode = config.appInfo.AppCode
            };
        }
        /// <summary>
        /// user registration
        /// </summary>
        /// <param name="reqBody"></param>
        /// <returns></returns>
        public Tuple<bool, string, RegisterUserResDataBody> RegisterUser(RegisterUserReqDataBody reqBody)
        {
            try
            {
                NodeApiReqBody<RegisterUserReqDataBody> req = new NodeApiReqBody<RegisterUserReqDataBody>()
                {
                    body = reqBody,
                    header = GetReqHeader()
                };

                var data = XuperchainReqMacExtends.GetRegisterUserReqMac(req);

                req.mac = sign.Sign(data);
                var res = SendHelper.SendPost<NodeApiResBody<RegisterUserResDataBody>>(config.reqUrl + registerUserUrl, JsonConvert.SerializeObject(req), config.httpsCert);

                if (res != null)
                {
                    //Check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, RegisterUserResDataBody>(false, res.header.msg, null);
                    //assemble the original string to verify
                    var datares = XuperchainResMacExtends.GetRegisterUserResMac(res);
                    //data verified
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, RegisterUserResDataBody>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, RegisterUserResDataBody>(false, "failed to verify the signature", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, RegisterUserResDataBody>(false, "failed to register the user", null); ;
        }
        /// <summary>
        /// transactions under Key-Trust Mode
        /// </summary>
        /// <param name="reqBody"></param>
        /// <returns></returns>
        public Tuple<bool, string, CallContractResDataBody> ReqChainCode(CallContractReqDataReqDataBody reqBody)
        {
            try
            {
                NodeApiReqBody<CallContractReqDataReqDataBody> req = new NodeApiReqBody<CallContractReqDataReqDataBody>()
                {
                    header = GetReqHeader(),
                    body = reqBody
                };

                var data = XuperchainReqMacExtends.ReqChainCodeReqMac(req);

                req.mac = sign.Sign(data);
                var res = SendHelper.SendPost<NodeApiResBody<CallContractResDataBody>>(config.reqUrl + ReqChainCodeUrl, JsonConvert.SerializeObject(req), config.httpsCert);
                if (res != null)
                {
                    //Check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, CallContractResDataBody>(false, res.header.msg, null);
                    //assemble the original string to verify
                    var datares = XuperchainResMacExtends.ReqChainCodeResMac(res);

                    //data verified
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, CallContractResDataBody>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, CallContractResDataBody>(false, "failed to verify the signature", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, CallContractResDataBody>(false, "failed to transact", null);
        }
        /// <summary>
        /// get transaction info
        /// </summary>
        /// <param name="reqBody"></param>
        /// <returns></returns>
        public Tuple<bool, string, XuperchainTransaction> GetTxInfo(GetTxInfoReqDataBody reqBody)
        {
            try
            {
                NodeApiReqBody<GetTxInfoReqDataBody> req = new NodeApiReqBody<GetTxInfoReqDataBody>()
                {
                    header = GetReqHeader(),
                    body = reqBody
                };

                var data = XuperchainReqMacExtends.GetTxInfoReqMac(req);

                req.mac = sign.Sign(data);
                var res = SendHelper.SendPost<NodeApiResBody<XuperchainTransaction>>(config.reqUrl + GetTxInfoUrl, JsonConvert.SerializeObject(req), config.httpsCert);
                if (res != null)
                {
                    //Check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, XuperchainTransaction>(false, res.header.msg, null);
                    //assemble the original string to verify
                    var datares = XuperchainResMacExtends.GetTxInfoResMac(res);

                    //data verified
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, XuperchainTransaction>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, XuperchainTransaction>(false, "failed to verify the signature", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, XuperchainTransaction>(false, "failed to transact", null);
        }
        /// <summary>
        /// get block info
        /// </summary>
        /// <param name="reqBody"></param>
        /// <returns></returns>
        public Tuple<bool, string, XuperchainBlock> GetBlockInfo(GetBlockInfoReqDataBody reqBody)
        {
            try
            {
                NodeApiReqBody<GetBlockInfoReqDataBody> req = new NodeApiReqBody<GetBlockInfoReqDataBody>()
                {
                    header = GetReqHeader(),
                    body = reqBody
                };
                var data = XuperchainReqMacExtends.GetBlockInfoReqMac(req);

                req.mac = sign.Sign(data);
                var res = SendHelper.SendPost<NodeApiResBody<XuperchainBlock>>(config.reqUrl + GetBlockInfoUrl, JsonConvert.SerializeObject(req), config.httpsCert);
                if (res != null)
                {
                    //Check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, XuperchainBlock>(false, res.header.msg, null);
                    //assemble the original string to verify
                    var datares = XuperchainResMacExtends.GetBlockInfoResMac(res);

                    //data verified
                    if (sign.Verify(res.mac, datares))
                    {
                        return new Tuple<bool, string, XuperchainBlock>(true, res.header.msg, res.body);
                    }
                    else
                    {
                        return new Tuple<bool, string, XuperchainBlock>(false, "failed to verify the signature", null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, XuperchainBlock>(false, "failed to transact", null);
        }
    }
}