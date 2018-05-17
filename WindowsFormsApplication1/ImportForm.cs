﻿using Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class ImportForm : Form
    {
        public ImportForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                      
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx|Excel97-2003文件(*.xls)|*.xls|所有文件(*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;

                //首先将数据内容导入到单位基本信息表中（这只是在程序初始化阶段完成）

                DataTable dt = null;
                DataTable dtTemp = null;
                //将"单位基本信息"表中的字段读取到listBox1中
                //ArrayList arr = MySqlHelper.ExecuteArrayList("单位基本信息");
               // listBox1.DataSource=  arr;
                //NpoiExcel.ExcelImport(fileName,arr);
                dt = NpoiExcel.ExcelToTable(fileName);//数据源表               
                

                 dtTemp = dt.DefaultView.ToTable(false, new string[] { "组织机构代码", "单位名称","行政区划","行业代码","管理机构" });
                dataGridView1.DataSource = dtTemp.DefaultView;
                dtTemp.TableName = "单位基本信息";
                MySqlHelper.BulkInsert(dtTemp);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
