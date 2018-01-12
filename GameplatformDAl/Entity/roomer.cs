using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class roomer
    {
        public int roomid { get; set; }
        public string  delay { get; set; } //延迟
        public string type { get; set; }//类型
        public int number { get; set; }//人数
        public string houseowner { get; set; } //房主

    }
}
