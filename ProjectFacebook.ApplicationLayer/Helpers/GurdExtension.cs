using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFacebook.ApplicationLayer.Helpers
{
    public static class GurdExtension
    {
        public static void AgainstNull(object value, string paremeterName)
        {
            if (value == null)
                throw new ApplicationException(paremeterName);
        }

        public static void AgainstFalse(bool condition, string parematerName)
        {
            if (!condition)
                throw new ApplicationException(parematerName);
        }
    }
}
