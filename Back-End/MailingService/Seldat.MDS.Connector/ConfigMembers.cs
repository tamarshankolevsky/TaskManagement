using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seldat.MDS.Connector
{
    public static class ConfigMembers
    {
        private static Uri url;

        public static Uri URL
        {
            get
            {
                if (url == null)
                    return new Uri(GetFromConfigSection("URL"));
                return url;
            }
            set { url = value; }
        }

        private static string password;

        public static string Password
        {
            get {
                if (string.IsNullOrEmpty(password))
                    return GetFromConfigSection("Password");
                return password;
            }
            set { password = value; }
        }

        private static string userName;

        public static string Username
        {
            get
            {
                if (string.IsNullOrEmpty(userName))
                    return GetFromConfigSection("Username");
                return userName;
            }
            set { userName = value; }
        }

        private static string applicationDomainId;

        public static string ApplicationDomainId
        {
            get
            {
                if (string.IsNullOrEmpty(applicationDomainId))
                    return GetFromConfigSection("ApplicationDomainId");
                return applicationDomainId;
            }
            set { applicationDomainId = value; }
        }

        private static string GetFromConfigSection(string propName)
        {
            var section = ConfigurationManager.GetSection("MDS") as Hashtable;
            return section[propName].ToString();
        }
    }
}
