using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entity
{
    [DataContract]
    public class  xianshi
    {
        [DataMember]
        public int zh { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int age { get; set; }
        [DataMember]
        public  string sex { get; set; }

    }
}
