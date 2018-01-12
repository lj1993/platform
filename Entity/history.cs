using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entity
{[DataContract]
  public class history
    {[DataMember]
        public string name { get; set; }
[DataMember]
        public  DateTime Registertime { get; set; }
    [DataMember]
        public DateTime quittime { get; set; }
    }
}
