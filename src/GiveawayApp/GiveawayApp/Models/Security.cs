namespace GiveawayApp.Models {
#if !DEBUG
    using System;
    using System.Configuration;
#endif

    public static class Security {
        public static bool ValidatePassword(string password) {
#if !DEBUG
            var adminPassword = ConfigurationManager.AppSettings["adminPassword"];
            return string.Equals(password, adminPassword, StringComparison.CurrentCultureIgnoreCase);
#else
            return true;
#endif
        }
    }
}