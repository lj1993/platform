using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public enum Sex
    {
        男,女
    }
    public class info
    {
        public int zh { get; set; }
        public string name { get; set; }
        public int age{ get; set; }
        public Sex  sex { get; set; }
        public string pwd { get; set; }
        public int State { get; set; }

        
    }
}
