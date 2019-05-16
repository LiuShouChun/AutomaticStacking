using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zip;

namespace ZipCompressTest.Model
{
    /// <summary>
    /// 解压方法
    /// </summary>
    public class UnZipFloClass
    {
        //public static string unZipFile(string TargetFile, string fileDir, ref string msg)
        //{
        //    string rootFile = "";
        //    msg = "";
        //    try
        //    {
        //        //读取压缩文件（zip文件），准备解压缩
        //        ZipInputStream inputstream = new ZipInputStream(File.OpenRead(TargetFile.Trim()));
        //        ZipEntry entry;
        //        string path = fileDir;
        //        //解压出来的文件保存路径
        //        string rootDir = "";
        //        //根目录下的第一个子文件夹的名称
        //        while ((entry = inputstream.GetNextEntry()) != null)
        //        {
        //            rootDir = Path.GetDirectoryName(entry.Name);
        //            //得到根目录下的第一级子文件夹的名称
        //            if (rootDir.IndexOf("\\") >= 0)
        //            {
        //                rootDir = rootDir.Substring(0, rootDir.IndexOf("\\") + 1);
        //            }
        //            string dir = Path.GetDirectoryName(entry.Name);
        //            //得到根目录下的第一级子文件夹下的子文件夹名称
        //            string fileName = Path.GetFileName(entry.Name);
        //            //根目录下的文件名称
        //            if (dir != "")
        //            {
        //                //创建根目录下的子文件夹，不限制级别
        //                if (!Directory.Exists(fileDir + "\\" + dir))
        //                {
        //                    path = fileDir + "\\" + dir;
        //                    //在指定的路径创建文件夹
        //                    Directory.CreateDirectory(path);
        //                }
        //            }
        //            else if (dir == "" && fileName != "")
        //            {
        //                //根目录下的文件
        //                path = fileDir;
        //                rootFile = fileName;
        //            }
        //            else if (dir != "" && fileName != "")
        //            {
        //                //根目录下的第一级子文件夹下的文件
        //                if (dir.IndexOf("\\") > 0)
        //                {
        //                    //指定文件保存路径
        //                    path = fileDir + "\\" + dir;
        //                }
        //            }
        //            if (dir == rootDir)
        //            {
        //                //判断是不是需要保存在根目录下的文件
        //                path = fileDir + "\\" + rootDir;
        //            }

        //            //以下为解压zip文件的基本步骤
        //            //基本思路：遍历压缩文件里的所有文件，创建一个相同的文件
        //            if (fileName != String.Empty)
        //            {
        //                FileStream fs = File.Create(path + "\\" + fileName);
        //                int size = 2048;
        //                byte[] data = new byte[2048];
        //                while (true)
        //                {
        //                    size = inputstream.Read(data, 0, data.Length);
        //                    if (size > 0)
        //                    {
        //                        fs.Write(data, 0, size);
        //                    }
        //                    else
        //                    {
        //                        break;
        //                    }
        //                }
        //                fs.Close();
        //            }
        //        }
        //        inputstream.Close();
        //        msg = "解压成功！";
        //        return rootFile;
        //    }
        //    catch (Exception ex)
        //    {
        //        msg = "解压失败，原因：" + ex.Message;
        //        return "1;" + ex.Message;
        //    }
        //}
        public static void UnZip(string zipPath, string outPath)
        {
            try
            {
                using (ZipFile zip = ZipFile.Read(zipPath))
                {
                    foreach (ZipEntry entry in zip)
                    {
                        entry.Extract(outPath, ExtractExistingFileAction.OverwriteSilently);
                    }
                }
            }
            catch (Exception ex)
            {
               
            }
        } 

    }
}