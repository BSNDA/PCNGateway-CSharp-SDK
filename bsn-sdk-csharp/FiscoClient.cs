using bsn_sdk_csharp.Enum;
using bsn_sdk_csharp.Models;
using bsn_sdk_csharp.NodeExtends;
using bsn_sdk_csharp.Trans;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities.Encoders;
using System;

namespace bsn_sdk_csharp
{
    public class FiscoClient
    {
        public AppSetting config;
        public int limit = 100;

        public FiscoClient(AppSetting _config)
        {
            config = _config;
        }

        public bool isSM()
        {
            return config.appInfo.AlgorithmType == EmAlgorithmType.SM2;
        }

        public BigInteger GetBlockLimit()
        {
            try
            {
                var res = new FiscoNodeServer(config).GetBlockHeight();
                if (res.Item1)
                {
                    var height = Convert.ToInt64(res.Item3.data);
                    return new BigInteger((height + limit).ToString());
                }
                else
                {
                    throw new Exception(res.Item2);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BigInteger GetGroupId()
        {
            try
            {
                return new BigInteger(config.appInfo.ChannelId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tuple<string, string> GetTransData(FiscoTransReq data)
        {
            var blockLimit = GetBlockLimit();
            var groupId = GetGroupId();
            var key = FiscoConfig.GetUserKey(data.UserName, config);
            var tx = FiscoTrans.TransData(data.Contract.ContractAbi, data.Contract.ContractAddress, data.FuncName, data.Args, groupId, blockLimit, null, isSM(), key);
            return tx;
        }
    }
}