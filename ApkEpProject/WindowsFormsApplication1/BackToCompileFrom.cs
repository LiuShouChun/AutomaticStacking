using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using WindowsFormsApplication1.Utils;

namespace WindowsFormsApplication1
{
    public partial class BackToCompileFrom : Form
    {
        private string signalPwd = "123456";
        Process process;
        public BackToCompileFrom()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
             process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.WorkingDirectory = ".";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            process.Start();
            //process.StandardInput.WriteLine("ipconfig");
            //process.StandardInput.WriteLine("E:");
            //process.StandardInput.WriteLine("cd apktool");
             process.StandardInput.WriteLine("apktool b "+Constant.apkName);
             process.StandardInput.WriteLine("jarsigner -verbose -keystore F:\\ApkEpProject\\WindowsFormsApplication1\\bin\\Debug\\test.keystore -signedjar  F:\\ApkEpProject\\WindowsFormsApplication1\\bin\\Debug\\DemoChange.apk  F:\\ApkEpProject\\WindowsFormsApplication1\\bin\\Debug\\"+Constant.apkName+"\\dist\\"+Constant.apkName+".apk test");        
             process.StandardInput.WriteLine("123456");
             process.BeginOutputReadLine();
        }


        private void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (!String.IsNullOrEmpty(outLine.Data))
            {
                StringBuilder sb = new StringBuilder(this.tb_back.Text);
                this.tb_back.Text = sb.AppendLine(outLine.Data).ToString();

            }
       
            if (process != null)
            {
                process.Close();

            }

        }
    }
}
