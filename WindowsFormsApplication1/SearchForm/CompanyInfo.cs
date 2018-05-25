using Helpers;
using MySql.Data.MySqlClient;
using System;
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
    public partial class CompanyInfo : Form
    {
        public CompanyInfo()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void CompanyInfo_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            dataGridView1.Width = this.Width;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //搜索按钮

            //List<string> sqlparam = null;
            //if (zuzhijigou!="")
            //{
            //    sqlparam.Add("zuzhijigou);
            //}
            //string sql = "select * from 单位基本信息";

            string sql = @"select * from 单位基本信息 where 组织机构代码 like ?t1 and 单位名称 like ?t2 and
                                    行政区划 like ?t3 and 行业代码 like ?t4 and 管理机构 like ?t5";
            //string sql = @"select * from 单位基本信息 where 组织机构代码 like ?t1 and 单位名称  like ?t2";
            //string sql = @"select * from 单位基本信息 where 组织机构代码 like ?t1";
            MySqlParameter[] parameters = {
                   new MySqlParameter("?t1", MySqlDbType.String),
                   new MySqlParameter("?t2", MySqlDbType.String),
                   new MySqlParameter("?t3", MySqlDbType.String),
                   new MySqlParameter("?t4", MySqlDbType.String),
                   new MySqlParameter("?t5", MySqlDbType.String)
                   };
            parameters[0].Value ="%"+ textBox1.Text+"%";
            parameters[0].Direction = ParameterDirection.Input;
            parameters[1].Value = "%" + textBox2.Text + "%";
            parameters[1].Direction = ParameterDirection.Input;
            parameters[2].Value = "%" + textBox3.Text + "%";
            parameters[2].Direction = ParameterDirection.Input;
            parameters[3].Value = "%" + textBox4.Text + "%";
            parameters[3].Direction = ParameterDirection.Input;
            parameters[4].Value = "%" + textBox5.Text + "%";
            parameters[4].Direction = ParameterDirection.Input;

            DataSet ds = Helpers.MySqlHelper.ExecuteDataSet(0, sql, parameters);
            dataGridView1.DataSource = ds.Tables[0];      


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string zuzhijigou = textBox1.Text;
            string danweimingcheng = textBox2.Text;
            string xingzhengquhua = textBox3.Text;
            string hangyedaima = textBox4.Text;
            string guanlijigou = textBox5.Text;
            //string sql = string.Format("select * from 单位基本信息 where 组织机构代码 like '%{0}%'", textBox1.Text);
            //DataSet ds = Helpers.MySqlHelper.ExecuteDataSet(0, sql, null);
            //dataGridView1.DataSource = ds.Tables[0];
            //string sql = @"select * from 单位基本信息 where 组织机构代码 like ?t1 or 单位名称 like ?t2 or
            //                         行政区划 like ?t3 or 行业代码 like ?t4 or 管理机构 like ?t5";
            // string sql = @"select * from 单位基本信息 where 组织机构代码 like ?t1 or 单位名称  like ?t2";
            //string sql = @"select * from 单位基本信息 where 组织机构代码 like ?t1";
            string sql = @"select * from 单位基本信息 where";
            if (zuzhijigou!="")
            {
                sql += " 组织机构代码 like ?t1 or";
            }
            if (danweimingcheng != "")
            {
                sql += " 单位名称 like ?t2 or";
            }
            if (zuzhijigou != "")
            {
                sql += " 行政区划 like ?t3 or";
            }
            if (hangyedaima != "")
            {
                sql += " 行业代码 like ?t4 or";
            }
            if (guanlijigou != "")
            {
                sql += " 管理机构 like ?t5 or";
            }
            sql = sql.Substring(0,sql.Length - 2);
            MySqlParameter[] parameters = {
                   new MySqlParameter("?t1", MySqlDbType.String),
                   new MySqlParameter("?t2", MySqlDbType.String),
                   new MySqlParameter("?t3", MySqlDbType.String),
                   new MySqlParameter("?t4", MySqlDbType.String),
                   new MySqlParameter("?t5", MySqlDbType.String)
                   };
            parameters[0].Value = "%" + textBox1.Text + "%";
            parameters[0].Direction = ParameterDirection.Input;
            parameters[1].Value = "%" + textBox2.Text + "%";
            parameters[1].Direction = ParameterDirection.Input;
            parameters[2].Value = "%" + textBox3.Text + "%";
            parameters[2].Direction = ParameterDirection.Input;
            parameters[3].Value = "%" + textBox4.Text + "%";
            parameters[3].Direction = ParameterDirection.Input;
            parameters[4].Value = "%" + textBox5.Text + "%";
            parameters[4].Direction = ParameterDirection.Input;

            DataSet ds = Helpers.MySqlHelper.ExecuteDataSet(0, sql, parameters);
            dataGridView1.DataSource = ds.Tables[0];

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string cell1 =dataGridView1.CurrentRow.Cells[1].Value.ToString();
            //MessageBox.Show("选中的当前行为"+cell1);
            Form childForm = new SearchForm.DetailForm(cell1);
            childForm.MdiParent = this.ParentForm;
            childForm.Text = "窗口 " ;
            childForm.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
