using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common {
    public static class AppMasterConfig {

        public static DEV_STATUS CurrentStatus = DEV_STATUS.DEVELOPMENT;

        public static string MASTER_IP => ConfigurationManager.AppSettings["Debug_IP"];

        public static string MASTER_PORT => ConfigurationManager.AppSettings["Debug_Port"];

        public static string MASTER_ADDR => @$"{MASTER_IP}:{MASTER_PORT}/";

        

        public enum DEV_STATUS {
            DEVELOPMENT = 1,
            RELEASE = 2
        };

        public const int SYSUSER_ID = 1;

        public static int INDENTSIZE = 4;

        public static long MAX_MSG_SIZE = 64 * 1_024 * 1_024;
    }
}
