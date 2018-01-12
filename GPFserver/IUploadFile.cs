using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GPFserver
{
    [ServiceContract]
    interface IUploadFile
    {
        [OperationContract]
        void UploadFileMethod(FileUploadMessage myFileMessage);
        /// <summary>
        /// 获取文件列表
        /// </summary>
        [OperationContract]
        DownFileResult DownLoadFile(DownFile filedata);
        [OperationContract]
        void vlenup(int vlen);
        [OperationContract]
        string[] GetFilesList();
        /// <summary>
        /// 下载文件
        /// </summary>
        [OperationContract]
        void uploadvideo(UpFile filedata);
        [OperationContract]
        int[] getvnum();
    }
    [MessageContract]
    public class FileUploadMessage
    {
        [MessageHeader(MustUnderstand = true)]
        public string FileName;
        [MessageBodyMember(Order = 1)]
        public Stream FileData;
    }
    [MessageContract]
    public class fmsg
    {
        [MessageHeader]
        public int nlen;
        [MessageBodyMember]
        public Stream FileData;
    }
}
