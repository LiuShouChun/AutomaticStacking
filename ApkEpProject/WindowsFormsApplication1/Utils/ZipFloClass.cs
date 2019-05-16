using System;
using System.Collections.Generic;
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace Go._50AK.ConsoleProgram.C1
{
    /// <summary>
    /// 压缩包内容
    /// </summary>
    public class ZipModel
    {
        /// <summary>
        /// 文件Stream
        /// </summary>
        public MemoryStream MStream { get; set; }

        /// <summary>
        /// ZipEntry
        /// </summary>
        public ZipEntry ZipEntry { get; set; }
    }

    public class ZipFloClass
    {
        /// <summary>
        /// Main
        /// </summary>
        /// <param name="packageName">压缩包地址</param>
        public static void addToZip(string packageName,string fileName)
        {
            List<ZipModel> list = OpenZipInfo(packageName);
            try
            {
                //生成的压缩文件为test.zip
                using (FileStream fsOut = File.Open(packageName, FileMode.Open))
                {
                    //ZipOutputStream类的构造函数需要一个流，文件流、内存流都可以，压缩后的内容会写入到这个流中。
                    using (ZipOutputStream zipStream = new ZipOutputStream(fsOut))
                    {
                        byte[] buffer = new byte[4096];
                        foreach (var item in list)
                        {
                            zipStream.PutNextEntry(item.ZipEntry);
                            item.MStream.Position = 0;
                            StreamUtils.Copy(item.MStream, zipStream, buffer);
                            zipStream.CloseEntry();
                        }
                        //准备把桌面的testImg文件添加到压缩包中。
                        FileInfo fi = new FileInfo(fileName);
                        //ZipEntry类代表了一个压缩包中的一个项，可以是一个文件，也可以是一个目录。
                        ZipEntry newEntry = new ZipEntry(fi.Name)//fi.Name可以自定义 表示压缩包内指定文件的名称
                        {
                            DateTime = fi.LastWriteTime,
                            Size = fi.Length
                        };
                        //把压缩项的信息添加到ZipOutputStream中。
                        zipStream.PutNextEntry(newEntry);
                        //把需要压缩文件以文件流的方式复制到ZipOutputStream中。

                        using (FileStream streamReader = File.OpenRead(fileName))
                        {
                            StreamUtils.Copy(streamReader, zipStream, buffer);
                        }
                        zipStream.CloseEntry();
                        //使用流操作时一定要设置IsStreamOwner为false。否则很容易发生在文件流关闭后的异常。
                        zipStream.IsStreamOwner = false;
                        zipStream.Finish();
                        zipStream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogService.WriteLog(ex, "Main");
            }
            finally
            {
                foreach (var item in list)
                {
                    //item.MStream?.Dispose();
                    //item.MStream?.Close();
                }
            }
        }

        /// <summary>
        /// 获取压缩包原有文件
        /// </summary>
        /// <param name="packageName">压缩包地址</param>
        /// <returns>Info</returns>
        private static List<ZipModel> OpenZipInfo(string packageName)
        {
            List<ZipModel> infoList = new List<ZipModel>();
            try
            {
                using (ZipInputStream zStream = new ZipInputStream(File.OpenRead(packageName)))
                {
                    ZipEntry theEntry;
                    while ((theEntry = zStream.GetNextEntry()) != null)
                    {
                        MemoryStream streamWriter = new MemoryStream();
                        ZipEntry newEntry = new ZipEntry(theEntry.Name)
                        {
                            DateTime = theEntry.DateTime,
                            Size = theEntry.Size
                        };
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            int size = zStream.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                        infoList.Add(new ZipModel
                        {
                            MStream = streamWriter,
                            ZipEntry = newEntry
                        });
                    }
                }
            }
            catch (Exception ex)
            {
             
            }
            return infoList;
        }
    }
}
