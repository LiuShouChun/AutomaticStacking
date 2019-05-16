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
using System.Xml;

namespace WindowsFormsApplication1
{
    public partial class MainForm1 : Form
    {

      StringBuilder sb = null;
        private bool clickable = true;
        public Process process = null;
        public MainForm1()
        {
            InitializeComponent();
           
        }

        private void readFormMinfest()
        {
            try
            {
                StringBuilder strs = new StringBuilder();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("F:\\ApkEpProject\\WindowsFormsApplication1\\bin\\Debug\\false\\AndroidManifest.xml");

                //查找<users>
                XmlNode root = xmlDoc.SelectSingleNode("manifest");
                String packageName = MyUtils.selcectEntrance(root.OuterXml, "package=\"", "\"", 1, 0);
                GlobalBean.PackageName = packageName;
                strs.Append("识别出来包名为 : " +" "+ GlobalBean.PackageName+"\r\n");
                   
                //获取到所有<users>的子节点
                XmlNodeList nodeList = xmlDoc.SelectSingleNode("manifest").ChildNodes;
                //遍历所有子节点
                foreach (XmlNode xn in nodeList)
                {
                    if(xn.Name == "application"){
                    //XmlElement xe = (XmlElement)xn;

                    XmlNodeList subList = xn.ChildNodes;

                    foreach (XmlNode xmlNode in subList)
                    {
                        XmlNodeList subList1 = xmlNode.ChildNodes;
                            foreach (XmlNode xmlNode1 in subList1)
                            {
                                //XmlElement xe2 = (XmlElement)xmlNode1;
                                XmlNodeList subList2 = xmlNode1.ChildNodes;
                                foreach (XmlNode xmlNode2 in subList2)
                                {
                                    if (xmlNode2.OuterXml == "<action android:name=\"android.intent.action.MAIN\" xmlns:android=\"http://schemas.android.com/apk/res/android\" />")
                                    {
                                        XmlNode parent = xmlNode2.ParentNode.ParentNode;
                                        string activity = MyUtils.selcectEntrance(parent.OuterXml, "\"", "\"", 0, 1);
                                         string[] temp =  activity.Split('.');
                                         string activityName = temp[temp.Length-1];
                                         GlobalBean.Activty_entrance = activityName;
                                         strs.Append("入口activity : " + " " + GlobalBean.Activty_entrance + "\r\n");
                                    }
                                }
                                
                                
                            }
                        }
                    
                    }
                }
                this.tv_content_n.Text = strs.ToString();
            }
            catch (Exception e)
            {  
              
            }
        }

        private void btn_compile_Click(object sender, EventArgs e)
        {
            if (clickable)
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
                process.StandardInput.WriteLine("apktool d "+Constant.apkName+".apk");
                process.BeginOutputReadLine();

                


            }

        }
        private void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (!String.IsNullOrEmpty(outLine.Data))
            {
                StringBuilder sb = new StringBuilder(this.tv_content_n.Text);
                this.tv_content_n.Text = sb.AppendLine(outLine.Data).ToString();

            }
            else
            {
                System.Threading.Thread.Sleep(1000);
                readFormMinfest();
            }
          
            
            clickable = false;
            if (process != null)
            {
                process.Close();

                clickable = false;
            }

        }

        private void MainForm1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (process != null)
            {
                process.Close();

            }

        }

        private void btn_file_display_Click(object sender, EventArgs e)
        {
            if (!clickable)
            {

                FileDisplayForm_new form = new FileDisplayForm_new();
                form.ShowDialog();
                this.Hide();
            }
            else
            {
                MessageBox.Show("请先反编译");
            }
        }
    }
}
