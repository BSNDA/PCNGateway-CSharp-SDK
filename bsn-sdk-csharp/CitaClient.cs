using bsn_sdk_csharp.Enum;
using bsn_sdk_csharp.Models;
using bsn_sdk_csharp.NodeExtends;
using bsn_sdk_csharp.Trans;
using Org.BouncyCastle.Math;
using System;
using System.Collections.Generic;

using System.Text;

namespace bsn_sdk_csharp
{
    public class CitaClient
    {
        public AppSetting config;
        private int limit = 80;
        private UInt64 quota = UInt64.Parse("10000000");

        public CitaClient(AppSetting _config)
        {
            config = _config;
        }

        public bool isSM()
        {
            return config.appInfo.AlgorithmType == EmAlgorithmType.SM2;
        }

        public UInt64 GetBlockLimit()
        {
            try
            {
                var res = new CitaNodeServer(config).GetBlockHeight();
                if (res.Item1)
                {
                    var height = Convert.ToInt64(res.Item3.data);
                    return UInt64.Parse((height + limit).ToString());
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

        public string GetChannelId()
        {
            try
            {
                return config.appInfo.ChannelId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public uint GetVersionId()
        {
            try
            {
                return uint.Parse(config.appInfo.Version);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tuple<string, string> GetTransData(CitaTransReq data)
        {
            var blockLimit = GetBlockLimit();
            var channelId = GetChannelId();
            var version = GetVersionId();
            var key = CitaConfig.GetUserKey(data.UserName, config);
            var tx = CitaTrans.TransData(data.Contract.ContractAbi, data.Contract.ContractAddress, data.FuncName, data.Args, channelId, blockLimit, version, quota, isSM(), key);
            return tx;
        }
    }
}