using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GPFserver;
using System.ServiceModel;
using Entity;

namespace GPFsvform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            ServiceHost sh1 = new ServiceHost(typeof(GPFservice));
            ServiceHost sh2 = new ServiceHost(typeof(UploadFile));
            sh1.Open();
            sh2.Open();
            InitializeComponent();
            label1.Text = "服务器运行中";
        }
    }
}
