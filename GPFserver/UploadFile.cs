using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace GPFserver
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class UploadFile : IUploadFile
    {
        private string savePath = @"D:\servicetemp\";
        public static int nlen = 0;
        public static int vlen = 0;
        /// <summary>
        /// 上传文件
        /// </summary>
        public void UploadFileMethod(FileUploadMessage myFileMessage)
        {
            if (!Directory.Exists(savePath))//文件存放的默认文件夹是否存在
            {
                Directory.CreateDirectory(savePath);//不存在则创建
            }
            string fileName = myFileMessage.FileName;//文件名
            string fileFullPath = Path.Combine(savePath, fileName);//合并路径生成文件存放路径
            Stream sourceStream = myFileMessage.FileData;
            if (sourceStream == null) { return; }
            if (!sourceStream.CanRead) { return; }
            //创建文件流，读取流中的数据生成文件
            using (FileStream fs = new FileStream(fileFullPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                try
                {
                    const int bufferLength = 4096;
                    byte[] myBuffer = new byte[bufferLength];//数据缓冲区
                    int count;
                    while ((count = sourceStream.Read(myBuffer, 0, bufferLength)) > 0)
                    {
                        fs.Write(myBuffer, 0, count);
                    }
                    fs.Close();
                    sourceStream.Close();
                }
                catch { return; }
            }
        }
        /// <summary>
        /// 获取文件列表
        /// </summary>
        public void uploadvideo(UpFile filedata)
        {
            string path = savePath;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            byte[] buffer = new byte[filedata.FileSize];
            FileStream fs = new FileStream(path + filedata.FileName, FileMode.Create, FileAccess.Write);
            int count = 0;
            while ((count = filedata.FileStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                fs.Write(buffer, 0, count);
            }
            //清空缓冲区
            fs.Flush();
            //关闭流
            fs.Close();
        }
        public void vlenup(int vlen0)
        {
            vlen = vlen0;
        }
        public int[] getvnum()
        {
            int[] temp = {nlen,vlen };
            return temp;
        }
        public string[] GetFilesList()
        {
            if (!Directory.Exists(savePath))//判断文件夹路径是否存在
            {
                return null;
            }
            DirectoryInfo myDirInfo = new DirectoryInfo(savePath);
            FileInfo[] myFileInfoArray = myDirInfo.GetFiles("*.zip");
            string[] myFileList = new string[myFileInfoArray.Length];
            //文件排序
            for (int i = 0; i < myFileInfoArray.Length - 1; i++)
            {
                for (int j = i + 1; j < myFileInfoArray.Length; j++)
                {
                    if (myFileInfoArray[i].LastWriteTime > myFileInfoArray[j].LastWriteTime)
                    {
                        FileInfo myTempFileInfo = myFileInfoArray[i];
                        myFileInfoArray[i] = myFileInfoArray[j];
                        myFileInfoArray[j] = myTempFileInfo;
                    }
                }
            }
            for (int i = 0; i < myFileInfoArray.Length; i++)
            {
                myFileList[i] = myFileInfoArray[i].Name;
            }
            return myFileList;
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        public DownFileResult DownLoadFile(DownFile filedata)
        {

            DownFileResult result = new DownFileResult();

            string path = savePath + filedata.FileName;

            if (!File.Exists(path))
            {
                result.IsSuccess = false;
                result.FileSize = 0;
                result.Message = "服务器不存在此文件";
                result.FileStream = new MemoryStream();
                return result;
            }
            Stream ms = new MemoryStream();
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            fs.CopyTo(ms);
            ms.Position = 0;  //重要，不为0的话，客户端读取有问题
            result.IsSuccess = true;
            result.FileSize = ms.Length;
            result.FileStream = ms;

            fs.Flush();
            fs.Close();
            return result;
        }
    }

    [MessageContract]
    public class DownFile
    {
        [MessageHeader]
        public string FileName { get; set; }
    }

    [MessageContract]
    public class UpFileResult
    {
        [MessageHeader]
        public bool IsSuccess { get; set; }
        [MessageHeader]
        public string Message { get; set; }
    }

    [MessageContract]
    public class UpFile
    {
        [MessageHeader]
        public long FileSize { get; set; }
        [MessageHeader]
        public string FileName { get; set; }
        [MessageBodyMember]
        public Stream FileStream { get; set; }
    }

    [MessageContract]
    public class DownFileResult
    {
        [MessageHeader]
        public long FileSize { get; set; }
        [MessageHeader]
        public bool IsSuccess { get; set; }
        [MessageHeader]
        public string Message { get; set; }
        [MessageBodyMember]
        public Stream FileStream { get; set; }
    }
}
