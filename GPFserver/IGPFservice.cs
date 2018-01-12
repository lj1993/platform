using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace GPFserver
{
    [ServiceContract(CallbackContract=typeof(IClientcallback))]
    public interface IGPFservice
    {
        [OperationContract]
        roomer TheRoomInformation(string roomid);
        [OperationContract]
         info PersonalInformation(string id);
        [OperationContract]
        List<xianshi> getuser();

        [OperationContract]
        bool register(info i);

        [OperationContract]
        LoginTag login(string id,string pwd);

        [OperationContract]
        void sendmessage(string user,string msg);

        [OperationContract]
        bool logout(LoginTag l);

        [OperationContract]
        bool updateinfo(info i);

        [OperationContract]
        bool uodatepwd(string npwd, string opwd, string id);

        [OperationContract]
        List<string> getfriend(string id);

        [OperationContract]
        List<roomer> getroom();

        [OperationContract]
        string createroom(roomer rm);

        [OperationContract]
        void fapply(string user);

        [OperationContract]
        void faccess(string user);

            [OperationContract]
        string[] getplinroom(string roomid);
    }
}
