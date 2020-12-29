using Google.Protobuf;
using Google.Protobuf.Collections;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace bsn_sdk_csharp.Trans
{
    public class Util
    {
        /// <summary>
        /// Convert class ByteString
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static Google.Protobuf.ByteString Marshal(Google.Protobuf.IMessage msg)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                var s = new Google.Protobuf.CodedOutputStream(ms);
                msg.WriteTo(s);
                s.Flush();
                var str = Google.Protobuf.ByteString.CopyFrom(ms.ToArray());
                return str;
            }
        }

        /// <summary>
        /// Convert class ByteString
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static byte[] MarshalByte(Google.Protobuf.IMessage msg)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                var s = new Google.Protobuf.CodedOutputStream(ms);
                msg.WriteTo(s);
                s.Flush();

                return ms.ToArray();
            }
        }

        /// <summary>
        /// convert CharacterString to ByteString
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Google.Protobuf.ByteString ConvertToByteString(string str)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(str);

            return Google.Protobuf.ByteString.CopyFrom(bytes);
        }

        /// <summary>
        /// byte to ByteString
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Google.Protobuf.ByteString ConvertToByteString(byte[] str)
        {
            return Google.Protobuf.ByteString.CopyFrom(str);
        }

        /// <summary>
        ///byte to sha256 string
        /// </summary>
        /// <param name="nonce"></param>
        /// <param name="creator"></param>
        /// <returns></returns>
        public static string ConvertSHA256String(byte[] nonce, byte[] creator)
        {
            List<byte> temp = new List<byte>();
            temp.AddRange(nonce);
            temp.AddRange(creator);
            var b = new byte[temp.Count];
            temp.CopyTo(b);
            SHA256Managed Sha256 = new SHA256Managed();
            byte[] bytes = Sha256.ComputeHash(b);

            return HashEncodeToString(bytes);
        }

        /// <summary>
        /// byteto hex string
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string HashEncodeToString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder(bytes.Length * 3);
            foreach (byte b in bytes)
            {
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            }

            return sb.ToString().ToLower();
        }

        /// <summary>
        /// Dictionary<string, string> convert MapField<string, ByteString>
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public static MapField<string, ByteString> ConvertMapField(Dictionary<string, string> map)
        {
            var res = new MapField<string, ByteString>();
            if (map != null && map.Count > 0)
            {
                foreach (var item in map)
                {
                    res.Add(item.Key, ConvertToByteString(item.Value));
                }
            }

            return res;
        }
    }
}