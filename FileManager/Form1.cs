using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //选择磁盘目录的 模式窗口对象 
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();//打开模式窗口
            this.txtPath.Text = fbd.SelectedPath;
            if (this.txtPath.Text == "") return; //判断没有选择不执行

            TreeNode rootNode = new TreeNode(this.txtPath.Text);//创建根节点名字
            rootNode.Tag = this.txtPath.Text;//根节点路径

            //在tv里面添加根节点
            this.tvFolder.Nodes.Add(rootNode);
            //递归读取文件夹，自己调用自己
            LoadFolder(rootNode);
        }

        private TreeNode LoadFolder(TreeNode ChildNode,DirectoryInfo ChildDirectory =null)
        {
            try
            {
                //创建好文件夹
                DirectoryInfo directoryInfo = new DirectoryInfo(ChildNode.Tag.ToString());
                if (directoryInfo.GetDirectories().Count() <= 0) return null;
                DirectoryInfo[] list = directoryInfo.GetDirectories();//获取很多个文件夹
                foreach (DirectoryInfo item in list)
                {
                    TreeNode node = new TreeNode(item.Name);//文件夹名
                    node.Tag = item.FullName;//全路径
                    ChildNode.Nodes.Add(node);
                    LoadFolder(node, item);
                }
            }
            catch (Exception)
            {

            }
            return null;
        }
        /// <summary>
        /// 选择节点触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvFolder_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.lvFiles.Items.Clear();//清楚
            //获取选中的节点信息
            TreeNode  selectNode =this.tvFolder.SelectedNode;
            //查询文件夹里面的文件
            LoadFiles(selectNode.Tag.ToString());

        }
        /// <summary>
        /// 根据文件夹路径读取加载文件信息
        /// </summary>
        /// <param name="Path"></param>
        private void LoadFiles(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            FileInfo[] files = directoryInfo.GetFiles();
            foreach (var file in files)
            {
                ListViewItem item = new ListViewItem(file.Name);
                item.SubItems.AddRange(new string[] 
                { (file.Length / 1024).ToString(),file.Extension,  file.FullName});
                this.lvFiles.Items.Add(item);

            }
        }
        private ListViewItem selectItem;
        private string filePath; //选中的文件路径
        private bool isDelete = false;  //false复制， true 剪切
        private void tsmiOpen_Click(object sender, EventArgs e)
        {
            string path = selectItem.SubItems[3].Text;
            Process.Start(path);
        }

        private void lvFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvFiles.SelectedItems.Count > 0)
            {
                selectItem = this.lvFiles.SelectedItems[0];
            }

        }

        private void lvFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string path = selectItem.SubItems[3].Text;
            Process.Start(path);
        }

        private void tsmiCopy_Click(object sender, EventArgs e)
        {
            filePath = selectItem.SubItems[3].Text;
            isDelete = false;
        }

        private void tsmiCut_Click(object sender, EventArgs e)
        {
            filePath = selectItem.SubItems[3].Text;
            isDelete = true;
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            //提示删除
            DialogResult result=    MessageBox.Show("确定要删除？", "提示", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                File.Delete(selectItem.SubItems[3].Text);
                this.lvFiles.SelectedItems[0].Remove();
                selectItem = null;
            } 
        }

        private void tmsiPaste_Click(object sender, EventArgs e)
        {
            //Path.Combine 合并保存路径
            string newPath = Path.Combine(this.tvFolder.SelectedNode.Tag.ToString(),selectItem.Text);//选中文件夹路径
            if (isDelete)
            {
                File.Move(filePath,newPath);
            }
            else
            {
                File.Copy(filePath, newPath,true);
            }
            //刷新数据 ，建议单独添加一条
            FileInfo fileInfo = new FileInfo(newPath);
            ListViewItem item = new ListViewItem(fileInfo.Name);
            item.SubItems.AddRange(new string[] { (fileInfo.Length / 1024).ToString(), fileInfo.Extension, fileInfo.FullName });

            this.lvFiles.Items.Add(item);
        }

        private void tvFolder_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button== MouseButtons.Right)
            {
                //获取右键选中的节点
                TreeNode node  = tvFolder.GetNodeAt(new Point(e.X, e.Y));
                if (node!=null)
                {
                    this.tvFolder.SelectedNode = node;
                }
            }
        }
        private FileSystemWatcher fsw;

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.lbMessage.Text = "监控已打开";
            this.btnStart.Enabled = false;
            fsw = new FileSystemWatcher();//创建文件观察者
            fsw.Path = "E:/file";//设置监控的路径
            fsw.IncludeSubdirectories = true; //监控包括子目录
            fsw.NotifyFilter = NotifyFilters.Size | NotifyFilters.FileName;//触发条件
            //fsw.Filter = "*.txt";//默认不写 就是监控全部 写就只监控TXT
            fsw.Changed += Fsw_Changed;//注册处理事件
            fsw.EnableRaisingEvents = true; 

        }

        private void Fsw_Changed(object sender, FileSystemEventArgs e)
        {
            MessageBox.Show("FILE IS CHANGED");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            fsw.EnableRaisingEvents = false;
            this.lbMessage.Text = "监控已关闭";
            this.btnStart.Enabled = true;
        }
    }
}
