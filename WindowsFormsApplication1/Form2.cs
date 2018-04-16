﻿using System;
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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //FileSvr fileSvr = new FileSvr();
            //System.Data.DataTable dt = fileSvr.GetExcelDatatable("C:\\Users\\NewSpring\\Desktop\\Demo\\InExcelOutExcel\\InExcelOutExcel\\excel\\ExcelToDB.xlsx", "mapTable");
            //fileSvr.InsetData(dt);
           
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Excel97-2003文件(*.xls)|*.xls|Excel文件(*.xlsx)|*.xlsx|所有文件(*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                DataTable dt = null;
                //NpoiExcel.ExcelImport(fileName);
                dt = NpoiExcel.ExcelToTable(fileName);
                dt.TableName = "t_photo";
               dataGridView1.DataSource = dt.DefaultView;
                SQLHelper.BulkInsert(dt);



            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
