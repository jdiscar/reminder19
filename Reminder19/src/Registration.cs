using System;
using System.Collections.Generic;
using System.Text;

namespace Reminder19.src
{
    class Registration
    {
        private static bool isRegistered = false;

        public static bool isValid()
        {
            return isRegistered;
        }

        public static void markValid()
        {
            isRegistered = true;
        }

        public static bool checkRegistration(String username, String serial)
        {
            // This code has been removed for the open source version.  Also,
            // I did my own serialization algorithm that was really embarrassing.
            return true;
        }
    }
}
