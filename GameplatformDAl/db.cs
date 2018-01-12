using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entity;
using System.Xml;
using System.Xml.XmlConfiguration;
using System.IO;

namespace GameplatformDAl
{
   public class db
    {
       static Dbhelps q = new Dbhelps();
       public static string path = "D:\\";
       
       //public bool Register(string id,string pwd )//验证登录
       //{
       //    string sql = "select count(*) from info where id="+int.Parse(id)+"and pwd='"+pwd+"'";
       //    q.OpenConnect();
       //    SqlCommand com = new SqlCommand();
       //    com.CommandText = sql;
       //    com.Connection = q.Connect;
       //    int d = (int)com.ExecuteScalar();
       //    if (d > 0)
       //    {
       //        return true;
       //    }
       //    else
       //    {
       //        return false;
       //    }
       //    q.CloseConnect();
           
       //}
     

       //public bool State(bool register,string id) //登录成功后，更改状态
       //{
       //    if (register)
       //    {
       //        SqlCommand cmd = new SqlCommand();
       //        cmd.CommandText = "update info set state=1 where id=" + int.Parse(id) + "";
       //        cmd.Connection = q.Connect;
       //        cmd.ExecuteNonQuery();
       //        return true;
       //    }
       //    else
       //    {
       //        return false;
       //    }
       //}

       //public bool nicheng(string id,string pwd) //起游戏名称
       //{
       //    string sql = "select count(name) from info where id=" + int.Parse(id) + "and pwd='" + pwd + "'";
       //    q.OpenConnect();
       //    SqlCommand com = new SqlCommand();
       //    com.CommandText = sql;
       //    com.Connection = q.Connect;
       //    int d = (int)com.ExecuteScalar();
       //    if (d > 0)
       //    {
       //        return true;
       //    }
       //    else
       //    {
       //        return false;
       //    }
       //    q.CloseConnect();
           
       //}
       public static bool Register(info s)     //用户注册
       {
            string d = "insert into info(ZH,name,age,sex,pwd,state) values ('"+s.zh+"','"+s.name+"',"+s.age+",'"+s.sex+"',"+s.pwd+",0)";
            q.OpenConnect();
            SqlCommand bn = new SqlCommand();
            bn.CommandText = d;
            bn.Connection = q.Connect;
            int vb = bn.ExecuteNonQuery();
            q.CloseConnect();
           if(vb>0){
               return true;
           }
           else
           {
               return false;
           }
         
       }

       public bool quitdaim(int zh)//用户登出的时间
       {
           string dengtime = "update history set quittime =" + DateTime.Now + " where  userid=" + zh + " ";
           q.OpenConnect();
           SqlCommand com = new SqlCommand();
           com.CommandText = dengtime;
           com.Connection = q.Connect;
           int rr = com.ExecuteNonQuery();
           q.CloseConnect();

           if (rr > 0)
           {
               return true;
           }
           else
           {
               return false;
           }

       }
       public static bool Enter(string zh,string pwd) //用户登录
       {
           try
           {
               string state = "update [info] set state=1 where [ZH]=" + zh;
               string dengtime = " insert history (userid, registertime) values(" + zh + "," + DateTime.Now + ")";
               q.OpenConnect();
               SqlCommand coms = new SqlCommand();
               coms.CommandText = state;
               coms.Connection = q.Connect;
               SqlCommand comse = new SqlCommand();
               comse.CommandText = dengtime;
               comse.Connection = q.Connect;
               int er = coms.ExecuteNonQuery();
               //int rr = comse.ExecuteNonQuery();
               if (er > 0)
               {
                   return true;
               }
               else
               {
                   return false;
               }
           }
           catch
           {
               return false;
           }
           finally
           {
               q.CloseConnect();
           }           
       }

       public static bool sing(LoginTag s)//登录时的标记
       {
           string sql = " update LoginTag set sign='"+s.singe+"' where userid= "+s.userid+"";
           SqlCommand com = new SqlCommand();
           q.OpenConnect();
           com.CommandText = sql;
           com.Connection = q.Connect;
           int sd = com.ExecuteNonQuery();
           q.CloseConnect();
           if(sd>0){
               return true;
           }
           else
           {
               return false;
           }
       }
       public static bool ging(LoginTag s)//注销是的标记清除
       {
           string sql = "UPDATE    LoginTag    SET  state=0,sign = ' '   WHERE     (userid = " + s.userid + ")";
           SqlCommand com = new SqlCommand();
           q.OpenConnect();
           com.CommandText = sql;
           com.Connection = q.Connect;
           int sd = com.ExecuteNonQuery();
           q.CloseConnect();
           if (sd > 0)
           {
               return true;
           }
           else
           {
               return false;
           }
         
       }
       //标记查看
       public static string biaojjichakan(string s)
       {
           bool ad;
           string hjs = "不存在";
           string sql = "SELECT  userid, sign  FROM   LoginTag where sign='"+s+"'";
           q.OpenConnect();
           SqlCommand com = new SqlCommand();
           com.Connection = q.Connect;
           com.CommandText = sql;
           int hj = (int)com.ExecuteScalar();           
           if(hj>0){
               ad= true;
           }
           else
           {
               ad= false;
           }
           if(ad){
               SqlDataReader ads = com.ExecuteReader();
               while(ads.Read()){
                  hjs=ads["userid"].ToString();
                 
               }
               ads.Close();
               q.CloseConnect();
               return hjs;
           }
           else
           {
               q.CloseConnect();
               return hjs;
           }
       }       //查看guid

       public static bool updateUser(info s)//用户信息的更改 Z
       {
           string da = "update [info] set ZH='" + s.zh + "',name='" + s.name + "',age=" + s.age + ",sex='" + s.sex + "'";
           q.OpenConnect();
           SqlCommand bn = new SqlCommand();
           bn.CommandText = da;
           bn.Connection = q.Connect;
           int vb = bn.ExecuteNonQuery();
           q.CloseConnect();
           if (vb > 0)
           {
               return true;
           }
           else
           {
               return false;
           }
         
       }
       public static bool updatepwd(string zh, string pwd)//修改密码
       {
           string sql = "update info pwd='"+pwd+"'where ZH='"+zh+"'";
           q.OpenConnect();
           SqlCommand com = new SqlCommand();
           com.CommandText = sql;
           com.Connection = q.Connect;
           int da = com.ExecuteNonQuery();
           q.CloseConnect();
           if (da > 0)
           {
               return true;
           }
           else
           {
               return false;
           }
       

       }

       public static List<string> friends(string s)//用户列表
       {
           List<string> an = new List<string>();
           string da = "SELECT  Friend FROM friend where Users='"+s+"'";
           q.OpenConnect();
           SqlCommand com = new SqlCommand();
           com.Connection = q.Connect;
           com.CommandText = da;
           SqlDataReader jj = com.ExecuteReader();
           while(jj.Read()){
              an.Add(jj["friend"].ToString());
           }
           jj.Close();
           q.CloseConnect();
           return an;
       }

       public static bool firendadd(string user,string friend)//好友添加
       {
           string d = "insert friend (users,friend) valuse('"+user+"','"+friend+"')";
           q.OpenConnect();
           SqlCommand bn = new SqlCommand();
           bn.CommandText = d;
           bn.Connection = q.Connect;
           int vb = bn.ExecuteNonQuery();
           q.CloseConnect();
           if (vb > 0)
           {
               return true;
           }
           else
           {
               return false;
           }
        
       }

       public static List<xianshi> chakan()//查看
       {
           List<xianshi> an = new List<xianshi>();
           string sd = "SELECT [ZH], name, age, sex FROM    [info]";
           q.OpenConnect();
           SqlCommand com = new SqlCommand();
           com.Connection = q.Connect;
           com.CommandText = sd;
           SqlDataReader jj = com.ExecuteReader();
           while (jj.Read())
           {
               xianshi n = new xianshi();
               n.zh =Int32.Parse(jj["ZH"].ToString());
               n.name = jj["name"].ToString();
               n.sex = jj["sex"].ToString();
               n.age = int.Parse(jj["age"].ToString());
               an.Add(n);
           }
           jj.Close();
           q.CloseConnect();
           return an;
       }

       //public List<friendid> firendid()//好友id
       //{
       //    List<friendid> an = new List<friendid>();
       //    string da = " SELECT    Userid, Friendid FROM    firendid";
       //    q.OpenConnect();
       //    SqlCommand com = new SqlCommand();
       //    com.Connection = q.Connect;
       //    com.CommandText = da;
       //    SqlDataReader jj = com.ExecuteReader();
       //    while (jj.Read())
       //    {
       //        friendid n = new friendid();
       //        n.Usersid = int.Parse(jj["userid"].ToString());
       //        n.frienid = int.Parse(jj["friendid"].ToString());
       //        an.Add(n);
       //    }
       //    jj.Close();
       //    q.CloseConnect();
       //    return an;
       //}//郑世润写的皮皮代码

       public static List<string> getfriend(string id)
       {
           List<string> an = new List<string>();
           string da = " SELECT friendid from [firendids] where Userid=@id";
           q.OpenConnect();
           SqlCommand com = new SqlCommand();
           SqlParameter sp = new SqlParameter("@id",id);           
           com.Connection = q.Connect;
           com.CommandText = da;
           com.Parameters.Add(sp);
           SqlDataReader jj = com.ExecuteReader();
           while (jj.Read())
           {
               an.Add(jj["friendid"].ToString());
           }
           jj.Close();
           com.CommandText = "SELECT Userid from [firendids] where friendid=@id";
           jj = com.ExecuteReader();
           while (jj.Read())
           {
               an.Add(jj["Userid"].ToString());
           }
           jj.Close();
           q.CloseConnect();
           return an;
       }//获取好友列表

       public static info PersonalInformation(string id)
       {
           info an = new info();
           string da = " SELECT name,sex from [info] where ZH=@id";
           q.OpenConnect();
           SqlCommand com = new SqlCommand();
           SqlParameter sp = new SqlParameter("@id", id);
           com.Connection = q.Connect;
           com.CommandText = da;
           com.Parameters.Add(sp);
           SqlDataReader jj = com.ExecuteReader();
           while (jj.Read())
           {
               an.zh = int.Parse(id);
               an.name=jj["name"].ToString();
               if(jj["sex"].ToString().Equals("男")){
               an.sex = Sex.男;
               }
               else
               {
                   an.sex = Sex.女;
               }
           }
           jj.Close();
           q.CloseConnect();
           return an;
       }
       public static roomer TheRoomInformation(string roomid)
       {
           string da = " SELECT roomid,number,houseowner from [roomer] where roomid=@roomid";
           q.OpenConnect();
           SqlCommand com = new SqlCommand();
           SqlParameter sp = new SqlParameter("@roomid", roomid);
           com.Connection = q.Connect;
           com.CommandText = da;
           com.Parameters.Add(sp);
           SqlDataReader jj = com.ExecuteReader();
           roomer ro = new roomer();
           while (jj.Read())
           {
               ro.roomid = int.Parse(jj["roomid"].ToString());
               ro.number=int.Parse(jj["number"].ToString());
               ro.houseowner = jj["houseowner"].ToString();
           }
           jj.Close();
           q.CloseConnect();
           return ro;
       }
       public static bool updataroom(roomer s)//房间的添加
       {
         string aql = "insert into roomer values("+s.roomid+",'"+s.delay+"','"+s.type+"',"+s.number+",'"+s.houseowner+"')";
         q.OpenConnect();
         SqlCommand bn = new SqlCommand();
         bn.CommandText = aql;
         bn.Connection = q.Connect;
         int vb = bn.ExecuteNonQuery();
         q.CloseConnect();
         if (vb > 0)
         {
             return true;
         }
         else
         {
             return false;
         }
        
       }

       public static void CreateXwspFile(string fileName)
       {
           XmlDocument xmlDoc = new XmlDocument();
           //创建类型声明节点  
           XmlDeclaration xdDec = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
           xmlDoc.AppendChild(xdDec);

           //创建根节点  
           XmlElement xeRoot = xmlDoc.CreateElement("xxx");
           //给节点属性赋值
           xeRoot.SetAttribute("version", "1.0");
           xeRoot.SetAttribute("name", fileName);
           xmlDoc.AppendChild(xeRoot);

           ////创建并添加<CreationInfo></CreationInfo>节点
           ////创建并添加<CreatedBy></CreatedBy>节点
           ////创建并添加<CreatedTime></CreatedTime>节点
           ////创建并添加<SavedTime></SavedTime>节点
           
           XmlNode xnXwsp = xmlDoc.SelectSingleNode("xxx");
         
           //保存XML文档
           try
           {
               xmlDoc.Save(path + fileName + ".xwsp");
           }
           catch (Exception ep)
           {
               Console.WriteLine(ep.Message);
           }
       }

       public static void UPdateXwspFile(string fileName, string wan)//建房间
       {
           string paths = path + fileName + ".xwsp";
           XmlDocument doc = new XmlDocument();
           doc.Load(paths);
           XmlElement root = doc.DocumentElement;
           XmlElement person = doc.CreateElement("player");
           person.InnerText = wan;
           root.AppendChild(person);
           doc.Save(paths);
       }

       public static List<roomer> Roomchakan()//获取房间列表
       {
           string sql = "SELECT   roomid, delay, type, number, houseowner FROM     roomer";
           q.OpenConnect();
           SqlCommand com = new SqlCommand();
           com.Connection = q.Connect;
           com.CommandText = sql;
           SqlDataReader jj = com.ExecuteReader();
           List<roomer> vb = new List<roomer>();
           while (jj.Read())
           {
               roomer n = new roomer();
               n.roomid = int.Parse(jj["roomid"].ToString());
               n.delay = jj["delay"].ToString();
               n.type = jj["type"].ToString();
               n.number = int.Parse(jj["number"].ToString());
               n.houseowner = jj["houseowner"].ToString();
               vb.Add(n);
           }

           jj.Close();
           q.CloseConnect();
           return vb;
       }

       public static string[] chakanXwspFile(string fileName)//
       {
           string paths = path + fileName + ".xwsp";
           string[] asd=new string[16];
           XmlDocument df = new XmlDocument();
           df.Load(paths);
           XmlElement rfv = df.DocumentElement;
           int i = 0;
           foreach (XmlNode q in rfv.ChildNodes)
           {
               asd[i] = q.InnerText;
               i++;
           }
           return asd;

       }

       private static void Createfile(string fileName, string path) //创建 聊天文本
       {
           string paths = path + fileName + ".txt";
           FileStream cf = new FileStream(paths, FileMode.Create);
           cf.Close();

       }
       private static void Writefile(string fileName, string path, string liaotian)//写入 聊天记录
       {
           string paths = path + fileName + ".txt";
           FileStream cf = new FileStream(paths, FileMode.Append);
           StreamWriter hai = new StreamWriter(cf);
           hai.WriteLine(liaotian);
           hai.WriteLine(DateTime.Now.ToString());
           hai.Close();
           cf.Close();
       }
       private static string Readfile(string fileName, string path)// 读取聊天记录
       {
           string paths = path + fileName + ".txt";
           FileStream myfs = new FileStream(paths, FileMode.Open);
           StreamReader hai = new StreamReader(myfs, Encoding.UTF8);
           string re = hai.ReadToEnd();
           hai.Close();
           myfs.Close();
           return re;

       }
       private static bool deletefile(string fileName, string path) //删除聊天文件
       {
           string paths = path + fileName + ".txt";
           if (!File.Exists(paths))
           {
               return false;
           }
           else
           {
               File.Delete(paths);
               return true;
           }

       }
     }     
}
