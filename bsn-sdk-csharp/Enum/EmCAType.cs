using System;
using System.Collections.Generic;
using System.Linq;

namespace bsn_sdk_csharp.Enum
{
    /// <summary>
    /// ca type enumeration
    /// </summary>
    public class EmCAType : Enumeration
    {
        private EmCAType()
        {
            throw new Exception("");
        }

        private EmCAType(int value, string displayName) : base(value, displayName)
        {
        }

        public static readonly EmCAType Trusteeship = new EmCAType(1, "key trust mode");
        public static readonly EmCAType Unmanaged = new EmCAType(2, "public key upload mode");

        public static IEnumerable<EmCAType> List() =>
            new[] { Trusteeship, Unmanaged };

        public static EmCAType From(int value)
        {
            var state = List().SingleOrDefault(s => s.Value == value);

            if (state == null)
            {
                throw new Exception($"Possible values for EmCAType: {string.Join(",", List().Select(s => s.Value))}");
            }

            return state;
        }

        public static EmCAType FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.DisplayName, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new Exception($"Possible values for EmCAType: {string.Join(",", List().Select(s => s.DisplayName))}");
            }

            return state;
        }
    }
}