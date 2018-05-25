using Helpers;
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
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                MessageBox.Show("请选择导入类型和时间并重试！", "错误信息");
                return;
            }

            //对月报表进行处理
            if (comboBox1.SelectedItem.ToString() == "月份报表")
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                openFileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx|Excel97-2003文件(*.xls)|*.xls";
                if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    string fileName = openFileDialog.FileName;

                    string importType = comboBox2.SelectedItem.ToString();

                    //判断数据库中是否存在所导入的时间类型（某月、某季、某年），
                    //如果存在则显示”已经存在，不能导入“

                    List<string> list = new List<string>();
                    string sql = "select distinct 报表时间类型 from 月报表数据信息";
                    list = SQLHelper.ExecuteReaderToString(sql, CommandType.Text, null);
                    if (list.Contains(importType))
                    {
                        MessageBox.Show("该报表时间类型在数据库中已经存在！无法导入！");
                        return;
                    }

                    //首先将数据内容导入到单位基本信息表中（这只是在程序初始化阶段完成）

                    DataTable dt = null;

                    dt = NpoiExcel.ExcelToTable(fileName);//数据源表
                    if (dt == null)
                    {
                        return;
                    }


                    //将数据保存到“单位基本信息”表中
                    // result :将数据导入到数据表中影响到的行数
                    int result=MyFunction.DataTableToDatabase(dt, "单位基本信息");
                    
                    //当该导入没有成功时，返回错误信息，后面就不再导入
                    if (result<=0 )
                    {                       
                        return;
                    }

                    //添加一新列，其值为默认值
                    DataColumn dc1 = new DataColumn("报表时间类型", typeof(string));
                    dc1.DefaultValue = importType;
                    dc1.AllowDBNull = false;//这在初始表的时候，其作用，在为已有表新增列的时候，不起作用
                    dt.Columns.Add(dc1);

                    //将数据保存到“月报表数据信息”表中
                    int result2 = MyFunction.DataTableToDatabase(dt, "月报表数据信息");
                    if (result2 <= 0)
                    {
                        return;
                    }
                    //将数据保存到“月报表全部数据信息”表中
                    int result3 = MyFunction.DataTableToDatabase(dt, "月报表全部数据信息");

                    //当该导入没有成功时，返回错误信息，并且将“月报表数据信息”刚导入的表中数据删除
                    if (result3 <= 0)
                    {
                        sql = "delete from 月报表数据信息 where 报表时间类型='" + importType + "'";
                        MySqlHelper.ExecuteNonQuery(0,sql,null);
                        return;
                    }
                    dataGridView1.DataSource = dt.DefaultView;

                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ImportForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            dataGridView1.Width = this.Width;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "月份报表")
            {
                comboBox2.Items.Add("2017年1月");
                comboBox2.Items.Add("2017年2月");
                comboBox2.Items.Add("2017年3月");
                comboBox2.Items.Add("2017年4月");
                comboBox2.Items.Add("2017年5月");
                comboBox2.Items.Add("2017年6月");
                comboBox2.Items.Add("2017年7月");
                comboBox2.Items.Add("2017年8月");
                comboBox2.Items.Add("2017年9月");
                comboBox2.Items.Add("2017年10月");
                comboBox2.Items.Add("2017年11月");
                comboBox2.Items.Add("2017年12月");
                comboBox2.Items.Add("2018年1月");
                comboBox2.Items.Add("2018年2月");
                comboBox2.Items.Add("2018年3月");
                comboBox2.Items.Add("2018年4月");
                comboBox2.Items.Add("2018年5月");
                comboBox2.Items.Add("2018年6月");
            }
            else if (comboBox1.SelectedItem.ToString() == "季度报表")
            {
                comboBox2.Items.Add("2017年3季度");
                comboBox2.Items.Add("2017年4季度");
                comboBox2.Items.Add("2018年1季度");
                comboBox2.Items.Add("2018年2季度");
                comboBox2.Items.Add("2018年3季度");
                comboBox2.Items.Add("2018年4季度");
            }
            else
            {
                comboBox2.Items.Add("2015年");
                comboBox2.Items.Add("2016年");
                comboBox2.Items.Add("2017年");
                comboBox2.Items.Add("2018年");
                comboBox2.Items.Add("2019年");
                comboBox2.Items.Add("2020年");
            }
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;//不允许修改
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;//不允许修改
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                MessageBox.Show("请选择导入类型和时间并重试！", "错误信息");
                return;
            }

            //对月报表进行处理
            if (comboBox1.SelectedItem.ToString() == "月份报表")
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                openFileDialog.Filter = "CSV文件(*.csv)|*.csv";
                if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    string fileName = openFileDialog.FileName;

                    //导入的时间类型（某月、某季、某年）
                    string importType = comboBox2.SelectedItem.ToString();

                    //判断数据库中是否存在所导入的时间类型（某月、某季、某年），
                    //如果存在则显示”已经存在，不能导入“

                    List<string> list = new List<string>(); 
                    string sql = "select distinct 报表时间类型 from 月报表数据信息";
                        list = SQLHelper.ExecuteReaderToString(sql, CommandType.Text, null);
                        if (list.Contains(importType ))
                        {
                            MessageBox.Show("该报表时间类型在数据库中已经存在！无法导入！");
                            return;
                        }
                 

                    //首先将数据内容导入到单位基本信息表中（这只是在程序初始化阶段完成）

                    DataTable dt = null;

                    dt = CSVFileHelper.OpenCSV(fileName);//数据源表
                    if (dt == null)
                    {
                        return;
                    }

                    //将数据保存到“单位基本信息”表中
                    // result :将数据导入到数据表中影响到的行数
                    int result = MyFunction.DataTableToDatabase(dt, "单位基本信息");

                    //当该导入没有成功时，返回错误信息，后面就不再导入
                    if (result <= 0)
                    {
                        return;
                    }

                    //添加一新列，其值为默认值
                    DataColumn dc1 = new DataColumn("报表时间类型", typeof(string));
                    dc1.DefaultValue = importType;
                    dc1.AllowDBNull = false;//这在初始表的时候，其作用，在为已有表新增列的时候，不起作用
                    dt.Columns.Add(dc1);

                    //将数据保存到“月报表数据信息”表中
                    int result2 = MyFunction.DataTableToDatabase(dt, "月报表数据信息");
                    if (result2 <= 0)
                    {
                        return;
                    }
                    //将数据保存到“月报表全部数据信息”表中
                    int result3 = MyFunction.DataTableToDatabase(dt, "月报表全部数据信息");

                    //当该导入没有成功时，返回错误信息，并且将“月报表数据信息”刚导入的表中数据删除
                    if (result3 <= 0)
                    {
                        sql = "delete from 月报表数据信息 where 报表时间类型='" + importType + "'";
                        MySqlHelper.ExecuteNonQuery(0, sql, null);
                        return;
                    }
                    dataGridView1.DataSource = dt.DefaultView;

                }
            }
            }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
