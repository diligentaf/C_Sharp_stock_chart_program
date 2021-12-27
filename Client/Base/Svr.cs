using Common;
using Common.Api;

using Grpc.Net.Client;

using ProtoBuf.Grpc.Client;

using Common.DataCls;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Client.Base.Repository;
using System.Windows.Controls;
using System.Windows.Media;

using System.Net;
using System.Net.Sockets;

using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace Client.Base {
    public sealed class Svr {
        private static readonly Lazy<Svr> lazy = new Lazy<Svr>(() => new Svr(), true);

        private Svr() {
            this.IsClientServerMatched = this.ClientVersion == this.ServerVersion ? true : false;
        }

        public static Svr BE => lazy.Value;

        public SolidColorBrush StatusBarColor { get; set; } = Brushes.Orange;

        public Version ClientVersion { get; private set; }

        public Version ServerVersion { get; private set; }

        public bool IsClientServerMatched { get; private set; }
    }
}
