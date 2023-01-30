using System.Collections.Generic;

namespace MoneyMe.Web.Constant
{
    public class Email
    {
        public static List<string> Invalid()
        {
            return new List<string>()
            {
                "@yahoo.com",
                "@yopmail.com"
            };
        }
    }
}
