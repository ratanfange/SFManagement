using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScoreManagement.Utils
{
    public static class StringUtil
    {
        public static bool HasSpecial(this string str)
        {
            if (string.IsNullOrWhiteSpace(str)) 
                return false;
            Regex regExp = new Regex("[ \\[ \\]<>《》；;/\'\"{}-]"); //特殊字符判断
            return regExp.IsMatch(str);
        }

        public static bool IsNull(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
    }
}
