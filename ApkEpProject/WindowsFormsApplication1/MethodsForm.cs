using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using WindowsFormsApplication1.Utils;

namespace WindowsFormsApplication1
{
    public partial class MethodsForm : Form
    {    
        //寄存器数量
        private int registerNum = 5;
        //参数个数
        private int parametersConut = 3;
        //包名
        private String packageName = " Lcom/xiaoyuan/jnitestsample/JNIUtil";
        //返回值类型
        private String returnType = "Ljava/lang/String";
        //jniUtil
        private String jniStr = ".class public Lcom/xiaoyuan/jnitestsample/JNIUtil;\r\n.super Ljava/lang/Object;\r\n.source \"JNIUtil.java\"\r\n\r\n\r\n#direct methods\r\n.method static constructor  <clinit>()V\r\n    .locals 1\r\n\r\n    .line 11\r\n    const-string v0, \"JNITestSample\"\r\n\r\n    invoke-static {v0}, Ljava/lang/System;->loadLibrary(Ljava/lang/String;)V\r\n\r\n    .line 12\r\n    return-void\r\n.end method\r\n\r\n.method public constructor <init>()V\r\n    .locals 0\r\n\r\n    .line 7\r\n    invoke-direct {p0}, Ljava/lang/Object;-><init>()V\r\n\r\n    return-void\r\n.end method\r\n\r\n\r\n# virtual methods\r\n.method public native getWorld()Ljava/lang/String;\r\n.end method\r\n";
        private String demoStr = "";
        private String apkName = "false";
        private StringBuilder str_pu;
        private StringBuilder str_pri;
        private StringBuilder str_pro;
        string filePath;
        int maxNumber = 0;
        string[] strs_pu;
        string[] strs_pri;
        string[] strs_pro;
        private string content;
        public MethodsForm()
        {
            InitializeComponent();
            InitDgv(ref dv_methods);
        }

        public MethodsForm(StringBuilder str_pu, StringBuilder str_pri, StringBuilder str_pro, string content, string filePath)
        {
         
            // TODO: Complete member initialization
            this.str_pu = str_pu;
            this.str_pri = str_pri;
            this.str_pro = str_pro;
            this.content = content;
            this.filePath = filePath;
            InitializeComponent();
            initData();
            createJniFile();
            InitDgv(ref dv_methods);
        }
        private void createJniFile()
        {
            DirectoryInfo di = new DirectoryInfo(string.Format(@"{0}..\..\", filePath));
            string jniPath = di.FullName;
            string path = "F:\\ApkEpProject\\WindowsFormsApplication1\\bin\\Debug\\"+Constant.apkName+"\\";
            MyUtils.CopyOldLabFilesToNewLab("G:\\so库", path);
            if (File.Exists(jniPath + "\\JniUtils.smali"))
            {
                MessageBox.Show("文件已存在！");
            }
            else
            {
                FileStream fs = new FileStream(jniPath + "\\JniUtils.smali", FileMode.CreateNew);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(jniStr); //这里是写入的内容
                sw.Flush();
            }
        }

        private void initData()
        {
            strs_pu = str_pu.ToString().Split('#');
            strs_pri = str_pri.ToString().Split('#');
            strs_pro = str_pro.ToString().Split('#');
            int[] max = new int[] { strs_pu.Length, strs_pri.Length, strs_pro.Length };
            maxNumber = max.Max();
            for (int i = 0; i < maxNumber; i++)
            {
                if (strs_pu.Length < maxNumber)
                {
                    str_pu.Append("@#");
                }
                if (strs_pri.Length < maxNumber)
                {
                    str_pri.Append("@#");
                }
                if (strs_pro.Length < maxNumber)
                {
                    str_pro.Append("@#");
                }
                strs_pu = str_pu.ToString().Split('#');
                strs_pri = str_pri.ToString().Split('#');
                strs_pro = str_pro.ToString().Split('#');
            }
            strs_pu = removeNullStr(str_pu.ToString().Split('#'));
            strs_pri = removeNullStr(str_pri.ToString().Split('#'));
            strs_pro = removeNullStr(str_pro.ToString().Split('#'));
        }
        private void InitDgv(ref DataGridView dgv)
        {
            dgv.Columns.Clear();
            DataGridViewTextBoxColumn[] ColunmArrary = new DataGridViewTextBoxColumn[3];
            for (int i = 0; i < ColunmArrary.Length; i++)
            {
                ColunmArrary[i] = new DataGridViewTextBoxColumn();
            }
            ColunmArrary[0].Name = "public method";
            ColunmArrary[0].HeaderText = "public method";
            ColunmArrary[1].Name = "private method";
            ColunmArrary[1].HeaderText = "private method";
            ColunmArrary[2].Name = "protected method";
            ColunmArrary[2].HeaderText = "protected method";
            for (int i = 0; i < ColunmArrary.Length; i++)
            {
                dgv.Columns.Add(ColunmArrary[i]);
            }
            dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.MultiSelect = false;
            dgv.RowTemplate.Height = 20;
            DisPlayDataToDGV(ref dv_methods);
        }
        private void DisPlayDataToDGV(ref DataGridView dgv)
        {
            dgv.Rows.Clear();
            for (int i = 0; i <= strs_pu.Length - 1; i++)
            {
                dgv.Rows.Add(strs_pu[i].Split('@')[1].ToString(), strs_pri[i].Split('@')[1].ToString(), strs_pro[i].Split('@')[1].ToString());

            }
        }

        private string[] removeNullStr(string[] str)
        {
            string[] strArray = str;


            strArray = strArray.Where(s => !string.IsNullOrEmpty(s)).ToArray();
            return strArray;

        }


        private void dv_methods_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string[] displayStrs = new string[maxNumber];
            switch (e.ColumnIndex)
            {
                case 0://public
                    displayStrs = strs_pu;
                    break;
                case 1://private
                    displayStrs = strs_pri;
                    break;
                case 2://protected
                    displayStrs = strs_pro;
                    break;
            }
            if (File.Exists(filePath))
            {
                String[] parameterName = new String[] { "\"a\"", "\"b\"", "\"c\"", "\"d\"", "\"e\"", "\"f\"", "\"g\"", "\"h\"", "\"i\"", "\"j\"" };
                int sortFlag = 1;
                switch (sortFlag)
                {
                    case 0://无参无返回值
                        demoStr = "\r\n    .registers 10\r\n\r\n    .prologue\r\n    .line 25\r\n    new-instance v0, " + packageName + ";\r\n\r\n    invoke-direct {v0}, " + packageName + ";-><init>()V\r\n\r\n    invoke-virtual {v0}, " + packageName + ";->getWorld()"+returnType+";\r\n\r\n    .line 27\r\n    return-void\r\n";
                        break;
                    case 1://无参有返回值
                        demoStr = "\r\n    .registers 10\r\n\r\n    .prologue\r\n    .line 25\r\n    new-instance v0, " + packageName + ";\r\n\r\n    invoke-direct {v0}, " + packageName + ";-><init>()V\r\n\r\n    invoke-virtual {v0}, " + packageName + ";->getWorld()" + returnType + ";\r\n\r\n    move-result-object v0\r\n\r\n    return-object v0";
                        break;
                    case 2://有参无返回值
                        StringBuilder parameters = new StringBuilder();
                       
                        if (parametersConut < parameterName.Length)
                        {
                            for (int i = 0; i < parametersConut; i++)
                            {
                                parameters.Append(".param p1,"+parameterName[i]+  "\r\n");
                            }
                            demoStr = "\r\n    .registers 10\r\n\r\n" + parameters.ToString() + "\r\n\r\n    .prologue\r\n    .line 25\r\n    new-instance v0," + packageName + ";\r\n\r\n    invoke-direct {v0}," + packageName + ";-><init>()V\r\n\r\n    invoke-virtual {v0}, " + packageName + ";->getWorld()" + returnType + ";\r\n\r\n    .line 26\r\n    return-void\r\n";
                        }
                        else
                        {
                            MessageBox.Show("参数数量不能超过10个");
                        }
                       
                        break;
                    case 3://有参有返回值
                        StringBuilder parameterStrs = new StringBuilder();

                        if (parametersConut < parameterName.Length)
                        {
                            for (int i = 0; i < parametersConut; i++)
                            {
                                parameterStrs.Append(".param p1," + parameterName[i] + "\r\n");
                            }
                            demoStr = "\r\n    .registers 10\r\n\r\n" + parameterStrs.ToString() + "\r\n\r\n    .prologue\r\n    .line 25\r\n    new-instance v0," + packageName + ";\r\n\r\n    invoke-direct {v0}," + packageName + ";-><init>()V\r\n\r\n    invoke-virtual {v0}, " + packageName + ";->getWorld()" + returnType + ";\r\n\r\n  move-result-object v0\r\n\r\n  return-object v0";
                        }
                        else
                        {
                            MessageBox.Show("参数数量不能超过10个");
                        }
                        break;
                }
                string strContent = File.ReadAllText(filePath);
                string a = displayStrs[e.RowIndex].Split('@')[0];
                int index = int.Parse(a);
                string replaceContent = MyUtils.MidStrEx(strContent, "\r\n", ".end method", index);
                string raplaceStr = MyUtils.replaceStr(strContent, replaceContent, demoStr);
                File.WriteAllText(filePath, raplaceStr);
            }

            MessageBox.Show("核心代码加固成功！");
        }

        private void backtocompile_Click(object sender, EventArgs e)
        {
            BackToCompileFrom form = new BackToCompileFrom();
            form.ShowDialog();
            this.Hide();
        }
      

     


     
    }
}
