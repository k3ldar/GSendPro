using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSendShared.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class GrblSettingAttribute : Attribute
    {
        public GrblSettingAttribute(string dollarValue)
        {
            DollarValue = dollarValue;
            
            if (Int32.TryParse(dollarValue.Substring(1), out int intValue))
                IntValue = intValue;
        }

        public string DollarValue { get; }

        public int IntValue { get; } = -1;
    }
}
