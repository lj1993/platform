using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace GPFserver
{
    interface IClientcallback
    {
        [OperationContract(IsOneWay = true)]
        void Receive(string p1, string p2);//收到公频消息

        [OperationContract(IsOneWay = true)]
        void ReceiveWhisper(string p1, string p2);//收到私聊通知

        [OperationContract(IsOneWay = true)]
        void UserEnter(string p);//用户上线

        [OperationContract(IsOneWay = true)]
        void UserLeave(string p);//用户下线

        [OperationContract(IsOneWay = true)]
        void Announce(string a);//系统通知

        [OperationContract(IsOneWay = true)]
        void cfapply(string id);//好友申请

        [OperationContract(IsOneWay = true)]
        void cfacc(string id);//接受申请
    }
}
