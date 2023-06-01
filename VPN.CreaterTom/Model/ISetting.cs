using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPN.CreaterTom.Model
{
    public interface ISetting
    {
        public string PathSaveFile { get; set; }
        public string PathLoadFile { get; set; }
    }
}
