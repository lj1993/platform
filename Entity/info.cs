using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entity
{[DataContract]
    public enum Sex
    {
        男,女
    }
    [DataContract]
    public class info
    {
        [DataMember]
        public int zh { get; set; }
    [DataMember]
        public string name { get; set; }
        [DataMember]
        public int age{ get; set; }
        [DataMember]
        public Sex  sex { get; set; }
        [DataMember]
        public string pwd { get; set; }
        [DataMember]
        public int State { get; set; }

        
    }
}
