using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entity
{
    [DataContract]
    public class roomer
    {
        [DataMember]
        public int roomid { get; set; }
        [DataMember]
        public string  delay { get; set; } //延迟
        [DataMember]
        public string type { get; set; }//类型
        [DataMember]
        public int number { get; set; }//人数
        [DataMember]
        public string houseowner { get; set; } //房主

    }
}
