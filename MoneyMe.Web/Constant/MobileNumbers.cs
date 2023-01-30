using System.Collections.Generic;

namespace MoneyMe.Web.Constant
{
    public class MobileNumbers
    {
        public static List<string> Invalid()
        {
            return new List<string>()
            {
                "0123456789",
                "9876543210",
            };
        }
    }
}
