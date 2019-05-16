using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApplication1.Utils;
using System.Xml;
using System.IO;
using System.Threading;
using System.Diagnostics;
using ZipCompressTest.Model;
using Go._50AK.ConsoleProgram.C1;

namespace WindowsFormsApplication1
{
    public partial class shellForm : Form
    {
        private string soureApkName = "demo";
        private string sourceApkFile = "";
        private string shellApkFile = "";
        private string shellPackageName = "demon.myunshell";
        private bool runFlage = false;
        private string basedPath = "";
        public shellForm()
        {  
            InitializeComponent();
            basedPath = Application.StartupPath;
            sourceApkFile = basedPath + "\\" + soureApkName + "\\" + "AndroidManifest.xml";
            shellApkFile = basedPath + "\\shell\\AndroidManifest.xml";
            runFlage = false;
            progressBar1.Value = 0;
            progressBar1.Maximum = 5;
            backgroundWorker1.RunWorkerAsync(5);
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
    
                                
        }
        private void amendMinfest()
        {
            //反编译shellapk
            Process process1 = new Process();
            process1.StartInfo.FileName = "cmd.exe";
            process1.StartInfo.WorkingDirectory = ".";
            process1.StartInfo.UseShellExecute = false;
            process1.StartInfo.RedirectStandardInput = true;
            process1.StartInfo.RedirectStandardOutput = true;
            process1.StartInfo.CreateNoWindow = true;
            process1.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            process1.Start();
            //process.StandardInput.WriteLine("ipconfig");
            //process.StandardInput.WriteLine("E:");
            process1.StandardInput.WriteLine("apktool d shell.apk");           
            process1.BeginOutputReadLine();
            Thread.Sleep(2000);
            backgroundWorker1.ReportProgress(1);
            //反编译源apk
            Process process2 = new Process();
            process2.StartInfo.FileName = "cmd.exe";
            process2.StartInfo.WorkingDirectory = ".";
            process2.StartInfo.UseShellExecute = false;
            process2.StartInfo.RedirectStandardInput = true;
            process2.StartInfo.RedirectStandardOutput = true;
            process2.StartInfo.CreateNoWindow = true;
            process2.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            process2.Start();
            //process.StandardInput.WriteLine("ipconfig");
            //process.StandardInput.WriteLine("E:");
            process2.StandardInput.WriteLine("apktool d "+ soureApkName+".apk");
            process2.BeginOutputReadLine();
            Thread.Sleep(5000);
            backgroundWorker1.ReportProgress(2);
            //将源apk'中的配置文件先复制到克程序中
            System.IO.File.Copy(sourceApkFile, shellApkFile, true);
            //读取克程序配置文件已便修改
            string shellManifest = File.ReadAllText(shellApkFile);
            StringBuilder strs = new StringBuilder();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(shellApkFile);
            //查找<manifest>
            XmlNode root = xmlDoc.SelectSingleNode("manifest");
            String packageName = MyUtils.selcectEntrance(root.OuterXml, "package=\"", "\"", 1, 0);
            //替换包名
            string oldstr = MyUtils.MidStrEx(shellManifest, "<manifest", packageName, 1, packageName.Length);
            //1.第一步先替换str里面的包名
            string newStr = MyUtils.replaceStr(oldstr, packageName, shellPackageName);
            //2.再替换整个配置文件字符串 目的就是只改变这一部分的包名
            shellManifest = MyUtils.replaceStr(shellManifest, oldstr, newStr);
            //添加meta节点
            //先找到application节点对应的>便于插入
            string oldMetaStr = MyUtils.MidStrEx(shellManifest, "<application", ">", 0, 1);
            string newMetaStr =Constant.metadataStr;
            //配置文件修改完成
            shellManifest = MyUtils.replaceStr(shellManifest, oldMetaStr, newMetaStr);
            File.WriteAllText(shellApkFile, shellManifest);    
           
              
              
        }
        //任务处理
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!File.ReadAllText(basedPath+"\\重要文件勿删.txt").Equals("1"))
            {              
                amendMinfest();
                backgroundWorker1.ReportProgress(3);
                //第二步复制res文件
                MyUtils.CopyOldLabFilesToNewLab(basedPath+"\\demo\\res", basedPath+"\\shell\\res");
                //第三步骤会编译shellApk   
                Control.CheckForIllegalCrossThreadCalls = false;
                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.WorkingDirectory = ".";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
                process.Start();
                process.StandardInput.WriteLine("apktool b shell");
                //签名shellAfter为shell.apk
                //process.StandardInput.WriteLine("jarsigner -verbose -keystore DeMon.jks -storepass 123456 -keypass 123456 -sigfile CERT -digestalg SHA1 -sigalg MD5withRSA -signedjar signatureShell.apk NosignatureShell.apk key");
                process.BeginOutputReadLine();
                Thread.Sleep(10000);
                backgroundWorker1.ReportProgress(2);
                System.IO.File.Copy(basedPath+"\\shell\\dist\\shell.apk", basedPath+"\\Copyshell.apk", true);
                System.IO.File.Copy(basedPath+"\\demo.apk", basedPath+"\\shareSpace\\demo.apk", true);
                Thread.Sleep(1000);
                backgroundWorker1.ReportProgress(4);
                //置换原来的class.dex
                ZipFloClass.addToZip(basedPath+"\\Copyshell.apk", basedPath+"\\shareSpace\\classes.dex");
                Control.CheckForIllegalCrossThreadCalls = false;
                Process process1 = new Process();
                process1.StartInfo.FileName = "cmd.exe";
                process1.StartInfo.WorkingDirectory = ".";
                process1.StartInfo.UseShellExecute = false;
                process1.StartInfo.RedirectStandardInput = true;
                process1.StartInfo.RedirectStandardOutput = true;
                process1.StartInfo.CreateNoWindow = true;
                process1.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
                process1.Start();
                //process.StandardInput.WriteLine("ipconfig");
                //process.StandardInput.WriteLine("E:");
                //process.StandardInput.WriteLine("cd apktool");               
                process1.StandardInput.WriteLine("jarsigner -verbose -keystore DeMon.jks -storepass 123456 -keypass 123456 -sigfile CERT -digestalg SHA1 -sigalg MD5withRSA -signedjar last.apk Copyshell.apk key");
                process1.BeginOutputReadLine();
                backgroundWorker1.ReportProgress(5);
                File.WriteAllText(basedPath+"\\重要文件勿删.txt", "1");   
                
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {   
            List<PictureBox> lists = new List<PictureBox> { pictureBox1, pictureBox2, pictureBox3, pictureBox4 };
            progressBar1.Value = e.ProgressPercentage;
            if (e.ProgressPercentage < 5)
            {
                PictureBox current = lists[e.ProgressPercentage - 1];
                Bitmap bitmap = new Bitmap("C:\\Users\\Administrator\\Desktop\\drawable-xhdpi-v4\\abc_btn_check_to_on_mtrl_015.png");
                current.Image = bitmap;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void shellForm_Load(object sender, EventArgs e)
        {
         
        }
        private void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
           

        }
    }
}
