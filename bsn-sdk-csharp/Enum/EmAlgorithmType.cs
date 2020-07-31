using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bsn_sdk_csharp.Enum
{
    public class EmAlgorithmType : Enumeration
    {
        private EmAlgorithmType()
        {
            throw new Exception("");
        }

        private EmAlgorithmType(int value, string displayName) : base(value, displayName)
        {
        }

        public static readonly EmAlgorithmType SM2 = new EmAlgorithmType(1, "SM2");
        public static readonly EmAlgorithmType Ecdsa = new EmAlgorithmType(2, "ECDSA(secp256r1)");
        public static readonly EmAlgorithmType Ecdsak1 = new EmAlgorithmType(3, "ECDSA(secp256k1)");

        public static IEnumerable<EmAlgorithmType> List() =>
            new[] { SM2, Ecdsa, Ecdsak1 };

        public static EmAlgorithmType From(int value)
        {
            var state = List().SingleOrDefault(s => s.Value == value);

            if (state == null)
            {
                throw new Exception($"Possible values for EmAlgorithmType: {string.Join(",", List().Select(s => s.Value))}");
            }

            return state;
        }

        public static EmAlgorithmType FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.DisplayName, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new Exception($"Possible values for EmAlgorithmType: {string.Join(",", List().Select(s => s.DisplayName))}");
            }

            return state;
        }
    }
}