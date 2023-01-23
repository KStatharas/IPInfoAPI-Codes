using IPInfoAPI_Codes.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace IPInfoAPI_Codes.Utils
{
    public class Validate
    {
        public static void twoLetterList (List<String>? list)
        {
            if (list.IsNullOrEmpty()) return;

            string regString = @"^[a-zA-Z]+$";
            Regex regex = new Regex(regString);

            foreach (String code in list)
            {
                if (code.Length != 2 || !regex.IsMatch(code)) 
                {
                    throw new InvalidTwoLetterCodeException(code);
                } 
            }
        }
    }
}
