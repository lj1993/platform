using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entity
{
    [DataContract]
    public  class LoginTag
    {
        [DataMember]
        public string userid { get; set; } //用户账号
        [DataMember]
        public string singe { get; set; } //临时标记
    }
}
