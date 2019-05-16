using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WindowsFormsApplication1.Utils;

namespace WindowsFormsApplication1
{
    public partial class FileDisplayForm : Form
    {
        private string filePath = "";
        string content = "";
        StringBuilder str_pu;
        StringBuilder str_pri;
        StringBuilder str_pro;
        bool clickable = false;
        public FileDisplayForm()
        {
            InitializeComponent();
            clickable = false;

        }

        private void FileDisplayForm_Load(object sender, EventArgs e)
        {
            //实例化TreeNode类 TreeNode(string text,int imageIndex,int selectImageIndex)              
            TreeNode rootNode = new TreeNode("我的电脑",
                IconIndexes.MyComputer, IconIndexes.MyComputer);  //载入显示 选择显示  
            //rootNode.Tag = "我的电脑";                            //树节点数据  
            //rootNode.Text = "我的电脑";                           //树节点标签内容  
            //this.tv_display.Nodes.Add(rootNode);               //树中添加根目录  

            ////显示MyDocuments(我的文档)结点  
            //var myDocuments = Environment.GetFolderPath           //获取计算机我的文档文件夹  
            //    (Environment.SpecialFolder.MyDocuments);
            //TreeNode DocNode = new TreeNode(myDocuments);
            //DocNode.Tag = "我的文档";                            //设置结点名称  
            //DocNode.Text = "我的文档";
            //DocNode.ImageIndex = IconIndexes.MyDocuments;         //设置获取结点显示图片  
            //DocNode.SelectedImageIndex = IconIndexes.MyDocuments; //设置选择显示图片  
            //rootNode.Nodes.Add(DocNode);                          //rootNode目录下加载节点  
            //DocNode.Nodes.Add("");

            //循环遍历计算机所有逻辑驱动器名称(盘符)  
            foreach (string drive in Environment.GetLogicalDrives())
            {
                //实例化DriveInfo对象 命名空间System.IO  
                var dir = new DriveInfo(drive);
                switch (dir.DriveType)           //判断驱动器类型  
                {
                    case DriveType.Fixed:        //仅取固定磁盘盘符 Removable-U盘   
                        {
                            if (dir.Name == "E:\\")
                            {

                                //Split仅获取盘符字母  
                                TreeNode tNode = new TreeNode(dir.Name.Split(':')[0]);
                                tNode.Name = dir.Name;
                                tNode.Tag = tNode.Name;
                                tNode.ImageIndex = IconIndexes.FixedDrive;         //获取结点显示图片  
                                tNode.SelectedImageIndex = IconIndexes.FixedDrive; //选择显示图片  
                                tv_display.Nodes.Add(tNode);                    //加载驱动节点  
                                tNode.Nodes.Add("");
                            }
                        }
                        break;
                }
            }
            rootNode.Expand();                  //展开树状视图  
        }
        //==============================================================================================================================
        private void tv_display_AfterExpand(object sender, TreeViewEventArgs e)
        {
            e.Node.Expand();
        }

        private void tv_display_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeViewItems.Add(e.Node);
        }



        private void tv_display_MouseDoubleClick(object sender, MouseEventArgs e)
        {   int  onCreateIndex = 0;
            StringBuilder sb = new StringBuilder();
            filePath = (string)tv_display.SelectedNode.Tag;


            //DirectoryInfo di = new DirectoryInfo(string.Format(@"{0}..\..\", filePath));
            ////上级目录
            //string mainDoc = di.FullName;
            ////程序入口activty
            //String mainActivtyPath = mainDoc + "MainActivity.smali";
            //string mainActivtyStr = File.ReadAllText(mainActivtyPath)
            //加入校验代码
            string selectStr = File.ReadAllText(filePath);
             string finalStr = "";
            string verifyFlag  = selectStr.Substring(selectStr.Length-1, 1);

            sb.Append(selectStr);
            sb.Append("\r\n");
            content = sb.ToString();
            this.ll_smali.Text = sb.ToString();
            string temp_pu = ".method public";
            string temp_pri = ".method private";
            string temp_pro = ".method protected";
            int index_pu = 0;
            int index_pri = 0;
            int index_pro = 0;
            List<int> arrayList_pu = new List<int>();
            List<int> arrayList_pri = new List<int>();
            List<int> arrayList_pro = new List<int>();
            while (index_pu != -1 || index_pri != -1 || index_pro != -1)
            {
                index_pu = content.IndexOf(temp_pu, index_pu + 20);
                index_pri = content.IndexOf(temp_pri, index_pri + 20);
                index_pro = content.IndexOf(temp_pro, index_pro + 20);
                if (index_pu != -1 && index_pu != 0 )
                {
                    if (!arrayList_pu.Contains(index_pu))
                    {
                        arrayList_pu.Add(index_pu + 14);
                    }
                }
                if (index_pri != -1 && index_pri != 0 )
                {
                    if (!arrayList_pri.Contains(index_pri))
                    {
                        arrayList_pri.Add(index_pri + 15);
                    }
                  
                }
                if (index_pro != -1 && index_pro != 0 )
                {
                   
                    if (!arrayList_pro.Contains(index_pro))
                    {
                        arrayList_pro.Add(index_pro + 17);
                    }
                }
               

            }
            for (int i = 0; i < arrayList_pu.Count; i++)  //外循环是循环的次数
            {
                for (int j = arrayList_pu.Count - 1; j > i; j--)  //内循环是 外循环一次比较的次数
                {

                    if (arrayList_pu[i] == arrayList_pu[j])
                    {
                        arrayList_pu.RemoveAt(j);
                    }

                }
            }
            for (int i = 0; i < arrayList_pri.Count; i++)  //外循环是循环的次数
            {
                for (int j = arrayList_pri.Count - 1; j > i; j--)  //内循环是 外循环一次比较的次数
                {

                    if (arrayList_pri[i] == arrayList_pri[j])
                    {
                        arrayList_pri.RemoveAt(j);
                    }

                }
            }
            for (int i = 0; i < arrayList_pro.Count; i++)  //外循环是循环的次数
            {
                for (int j = arrayList_pro.Count - 1; j > i; j--)  //内循环是 外循环一次比较的次数
                {

                    if (arrayList_pro[i] == arrayList_pro[j])
                    {
                        arrayList_pro.RemoveAt(j);
                    }

                }
            }
           
             // string a = ".method public onClick(Landroid/view/View;)V";
             //string b =  MidStrEx(a, 14, "(");
            //string method_public = MidStrEx(content,(int)arrayList_pu.Get(0),")");
             string strs = "";
             str_pu = new StringBuilder();
             str_pri = new StringBuilder();
             str_pro = new StringBuilder(); 
            foreach(int temp in arrayList_pu){
                str_pu.Append(temp+"@"+MyUtils.MidStrEx(content, temp, "(")+"#");
            }
            foreach (int temp in arrayList_pri)
            {
                str_pri.Append(temp + "@" + MyUtils.MidStrEx(content, temp, "(") + "#");
            }
            foreach (int temp in arrayList_pro)
            {
                str_pro.Append(temp + "@" + MyUtils.MidStrEx(content, temp, "(") + "#");
                if (MyUtils.MidStrEx(content, temp, "(").Equals("onCreate"))
                {
                    onCreateIndex = temp;
                }
            }
            if (!verifyFlag.Equals("$"))
            {
                finalStr = selectStr + Constant.verifyStr + "\r\n#   $";
                File.WriteAllText(filePath, finalStr);
                string replaceContent = MyUtils.MidStrEx(finalStr, "\r\n", ".end method", onCreateIndex);
                int checkIndex = replaceContent.IndexOf("invoke") - 1;
                string repaleced = replaceContent.Insert(checkIndex, "\r\n\r\n invoke-direct {p0}, Lcom/xiaoyuan/jnitestsample/MainActivity;->check()V\r\n");
                string raplaceStr = MyUtils.replaceStr(finalStr, replaceContent, repaleced);
                File.WriteAllText(filePath, raplaceStr);     
            }      
            clickable = true;
        }

       


        private void bt_bundle_Click(object sender, EventArgs e)
        {
            if (clickable)
            {
                MethodsForm mainform = new MethodsForm(str_pu, str_pri, str_pro, content, filePath);
      
                mainform.Owner = this;
                mainform.ShowDialog();
            }
            else
            {
                MessageBox.Show("请先选择文件，再点击按钮！");
            }
            
        }
   

    }
    
}
