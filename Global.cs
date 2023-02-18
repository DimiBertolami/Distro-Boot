using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QemuUtil
{
    class Global
    {
        public static int id { get; set; } = 0;
        public static Process ps { get; set; }
        public static int Handle { get; set; } = 0;
        public static string removelistitem { get; set; } = string.Empty;
    }
}
