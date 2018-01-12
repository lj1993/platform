using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using GameplatformDAl;
using Entity;

namespace GPFserver
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class GPFservice:IGPFservice
    {
        private static Object syncObj = new Object();////定义一个静态对象用于线程部份代码块的锁定，用于lock操作
        IClientcallback cb = null;
        public delegate void ChatEventHandler(object sender, ChatEventArgs e);//定义用于把处理程序赋予给事件的委托。
        public static event ChatEventHandler ChatEvent;//定义事件
        static Dictionary<string, ChatEventHandler> chatters = new Dictionary<string, ChatEventHandler>();//创建一个静态Dictionary（表示键和值）集合(字典)，用于记录在线成员
        private ChatEventHandler myEventHandler = null;
        private string id;

        public List<xianshi> getuser()
        {
            return db.chakan();
        }
        public void sendmessage(string user, string msg)
        {
            ChatEventArgs e = new ChatEventArgs();
            e.msgType = MessageType.ReceiveWhisper;
            e.name = this.id;
            e.message = msg;
            try
            {
                ChatEventHandler chatterTo;//创建一个临时委托实例
                lock (syncObj)
                {
                    chatterTo = chatters[user]; //查找成员字典中，找到要接收者的委托调用
                }
                chatterTo.BeginInvoke(this, e, new AsyncCallback(EndAsync), null);//异步方式调用接收者的委托调用
            }
            catch (KeyNotFoundException)
            {
            }
        }//私聊

        public void fapply(string user)
        {
            ChatEventArgs e = new ChatEventArgs();
            e.msgType = MessageType.friendapply;
            e.name = this.id;
            try
            {
                ChatEventHandler chatterTo;//创建一个临时委托实例
                lock (syncObj)
                {
                    chatterTo = chatters[user]; //查找成员字典中，找到要接收者的委托调用
                }
                chatterTo.BeginInvoke(this, e, new AsyncCallback(EndAsync), null);//异步方式调用接收者的委托调用
            }
            catch (KeyNotFoundException)
            {
            }
        }//好友申请

        public void faccess(string user)
        {
            ChatEventArgs e = new ChatEventArgs();
            e.msgType = MessageType.friendaccess;
            e.name = this.id;
            db.firendadd(this.id, user);
            try
            {
                ChatEventHandler chatterTo;//创建一个临时委托实例 
                lock (syncObj)
                {
                    chatterTo = chatters[user]; //查找成员字典中，找到要接收者的委托调用
                }
                chatterTo.BeginInvoke(this, e, new AsyncCallback(EndAsync), null);//异步方式调用接收者的委托调用
            }
            catch (KeyNotFoundException)
            {
            }
        }//同意好友申请

        public LoginTag login(string id, string pwd)//登陆
        {
            if (!db.Enter(id, pwd))
            {
                return null;
            }
            myEventHandler = new ChatEventHandler(MyEventHandler);//将MyEventHandler方法作为参数传递给委托            
                if (!chatters.ContainsKey(id) && id != "" && id != null)
                {
                    this.id = id;
                    chatters.Add(id, MyEventHandler);
                }
            LoginTag lt = new LoginTag();
            lt.singe = Guid.NewGuid().ToString();
            lt.userid = id;
            db.sing(lt);
            cb = OperationContext.Current.GetCallbackChannel<IClientcallback>();//获取当前操作客户端实例的通道给IChatCallback接口的实例callback,此通道是一个定义为IChatCallback类型的泛类型,通道的类型是事先服务契约协定好的双工机制。
            ChatEventArgs e = new ChatEventArgs();//实例化事件消息类ChatEventArgs
            e.msgType = MessageType.UserEnter;
            e.name = id;
            return lt;
        }

        public bool logout(LoginTag l)
        {
            Leave();            
            return db.ging(l);
        }//下线

        public bool register(info i)
        {
            if (!db.Register(i))
            {
                return false;
            }
            return true;
        }//注册

        public List<string> getfriend(string id)
        {
            return db.getfriend(id);
        }//获取好友列表

        public bool updateinfo(info i)
        {
            return db.updateUser(i);
        }//更改个人信息

        public bool uodatepwd(string npwd, string opwd, string id)
        {
            return db.updatepwd(id, npwd);
        }//更改密码

        public List<roomer> getroom()
        {
            List<roomer> r = db.Roomchakan();
            return r;
        }//获取房间列表
        public info PersonalInformation(string id)
        {
            return db.PersonalInformation(id);
        }
        public roomer TheRoomInformation(string roomid)
        {
            return db.TheRoomInformation(roomid);
        }
        public string createroom(roomer rm)
        {
            db.updataroom(rm);
            db.CreateXwspFile(rm.roomid.ToString());
            db.UPdateXwspFile(rm.roomid.ToString(), id);
            return rm.roomid.ToString();
        }//创建游戏房间

        public string[] getplinroom(string roomid)
        {
            return db.chakanXwspFile(roomid);
        }//获取房间信息

        public void Leave()
        {
            if (this.id == null)
                return;

            lock (syncObj)
            {
                chatters.Remove(this.id);
            }
            ChatEvent -= myEventHandler;
            ChatEventArgs e = new ChatEventArgs();
            e.msgType = MessageType.UserLeave;
            e.name = this.id;
            this.id = null;
        }

        private void MyEventHandler(object sender, ChatEventArgs e)
        {
            try
            {
                switch (e.msgType)
                {
                    case MessageType.Receive:
                        cb.Receive(e.name, e.message);
                        break;
                    case MessageType.ReceiveWhisper:
                        cb.ReceiveWhisper(e.name, e.message);
                        break;
                    case MessageType.UserEnter:
                        cb.UserEnter(e.name);
                        break;
                    case MessageType.UserLeave:
                        cb.UserLeave(e.name);
                        break;
                    case MessageType.friendapply:
                        cb.cfapply(e.name);
                        break;
                    case MessageType.friendaccess:
                        cb.cfacc(e.name);
                        break;
                }
            }
            catch
            {
                Leave();
            }
        }

        private void EndAsync(IAsyncResult ar)
        {
            ChatEventHandler d = null;
            try
            {
                //封装异步委托上的异步操作结果
                System.Runtime.Remoting.Messaging.AsyncResult asres = (System.Runtime.Remoting.Messaging.AsyncResult)ar;
                d = ((ChatEventHandler)asres.AsyncDelegate);
                d.EndInvoke(ar);
            }
            catch
            {
                ChatEvent -= d;
            }
        }
    }
    public enum MessageType { Receive, UserEnter, UserLeave, ReceiveWhisper,friendapply,friendaccess };
    /// <summary>
    /// 定义一个本例的事件消息类. 创建包含有关事件的其他有用的信息的变量，只要派生自EventArgs即可。
    /// </summary>
    public class ChatEventArgs : EventArgs
    {
        public MessageType msgType;
        public string name;
        public string message;
    }


}
