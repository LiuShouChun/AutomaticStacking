using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WindowsFormsApplication1.Utils
{
     static  class  MyUtils
    {   
        public static string MidStrEx(string sourse, int startindex, string endstr)
        {
            string result = string.Empty;
            int endindex;
            try
            {
                //startindex = sourse.IndexOf(startstr);
                if (startindex == -1)
                    return result;
                string tmpstr = sourse.Substring(startindex + 1);
                endindex = tmpstr.IndexOf(endstr);
                if (endindex == -1)
                    return result;
                result = tmpstr.Remove(endindex);
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        /// <summary>
        /// 拷贝oldlab的文件到newlab下面
        /// </summary>
        /// <param name="sourcePath">lab文件所在目录(@"~\labs\oldlab")</param>
        /// <param name="savePath">保存的目标目录(@"~\labs\newlab")</param>
        /// <returns>返回:true-拷贝成功;false:拷贝失败</returns>
        public static bool CopyOldLabFilesToNewLab(string sourcePath, string savePath)
        {
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            #region //拷贝labs文件夹到savePath下
            try
            {
                string[] labDirs = Directory.GetDirectories(sourcePath);//目录
                string[] labFiles = Directory.GetFiles(sourcePath);//文件
                if (labFiles.Length > 0)
                {
                    for (int i = 0; i < labFiles.Length; i++)
                    {
                        if (Path.GetExtension(labFiles[i]) != ".lab")//排除.lab文件
                        {
                            File.Copy(sourcePath + "\\" + Path.GetFileName(labFiles[i]), savePath + "\\" + Path.GetFileName(labFiles[i]), true);
                        }
                    }
                }
                if (labDirs.Length > 0)
                {
                    for (int j = 0; j < labDirs.Length; j++)
                    {
                        Directory.GetDirectories(sourcePath + "\\" + Path.GetFileName(labDirs[j]));

                        //递归调用
                        CopyOldLabFilesToNewLab(sourcePath + "\\" + Path.GetFileName(labDirs[j]), savePath + "\\" + Path.GetFileName(labDirs[j]));
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
            #endregion
            return true;
        }

        public static string  MidStrEx(string sourse, string startStr, string endstr, int firstIndex)
        {
            string result = string.Empty;
            int startStrdex;
            int endindex;
            try
            {
                //大概位置
                sourse = sourse.Substring(firstIndex);
                startStrdex = sourse.IndexOf(startStr) + 1;
                string tmpstr = sourse.Substring(startStrdex + 1);
                endindex = tmpstr.IndexOf(endstr) - 1;
                if (endindex == -1)
                    return result;
                result = tmpstr.Remove(endindex);
            }
            catch (Exception ex)
            {

            }
            return result;
        }
         /// <summary>
        /// 查找sourse里面从startStr 到endstr 之间的字符串
         /// </summary>
         /// <param name="sourse"></param>
         /// <param name="startStr"></param>
         /// <param name="endstr"></param>
         /// <param name="firstIndex"></param>
         /// <param name="step"></param>
         /// <returns></returns>
        public static string MidStrEx(string sourse, string startStr, string endstr, int firstIndex,int step)
        {
            string result = string.Empty;
            int startStrdex;
            int endindex;
            try
            {
                //大概位置
                sourse = sourse.Substring(firstIndex);
                startStrdex = sourse.IndexOf(startStr) + 1;
                string tmpstr = sourse.Substring(startStrdex + 1);
                endindex = tmpstr.IndexOf(endstr)+step ;
                if (endindex == -1)
                    return result;
                result = tmpstr.Remove(endindex);
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        public static string selcectEntrance(string sourse, string startStr, string endstr, int firstIndex ,int flag)
        {
            string result = string.Empty;
            int startStrdex;
            int endindex;
            int step = 1;
            int startStep = 1;
            switch (flag)
            {
                case 0:
                    step = 8;
                    break;
                case 1:
                    step = 1;
                    startStep = 0;
                    break;

            }
            try
            {
                //大概位置
                sourse = sourse.Substring(firstIndex);
                startStrdex = sourse.IndexOf(startStr) + startStep;
                string tmpstr = sourse.Substring(startStrdex + step);
                endindex = tmpstr.IndexOf(endstr) ;
                if (endindex == -1)
                    return result;
                result = tmpstr.Remove(endindex);
            }
            catch (Exception ex)
            {

            }
            return result;
        }


        public static string replaceStr(string str, string oldStr, string newStr)
        {
            StringBuilder strBuffer = new StringBuilder();
            int start = 0;
            int tail = 0;

            //一旦找不到需要替换的字符串(第一次IndexOf返回-1)
            //就说明没有该关键字符串，可以直接返回之前的字符串
            if (str.IndexOf(oldStr) == -1)
            {
                return str;
            }

            //每次都不断循环，查找这个x
            //一旦找到了，就把它之前和上一个x之后的字符串拼接起来
            while (true)
            {
                start = str.IndexOf(oldStr, start);
                if (start == -1)
                {
                    break;
                }
                strBuffer.Append(str.Substring(tail, start - tail));
                strBuffer.Append(newStr);
                start += oldStr.Length;
                tail = start;
            }

            //查找到最后一个位置之后
            //还要把剩下的字符串拼接进去
            strBuffer.Append(str.Substring(tail));
            return strBuffer.ToString();
        }



    }
}
