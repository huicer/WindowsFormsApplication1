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

namespace WindowsFormsApplication1.SearchForm
{
    public partial class DetailForm : Form
    {
        private string companyID;

        public string CompanyID
        {
            get
            {
                return companyID;
            }

            set
            {
                companyID = value;
            }
        }

        public DetailForm()
        {
            InitializeComponent();
        }

        public DetailForm(string companyID)
        {
            InitializeComponent();
            this.CompanyID = companyID;
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DetailForm_Load(object sender, EventArgs e)
        {
            string sql = "select * from 月报表数据信息";
            DataSet ds = Helpers.MySqlHelper.ExecuteDataSet(0, sql, null);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            //string searchType = comboBox1.SelectedValue.ToString();
            string searchType = comboBox1.SelectedItem.ToString();
            if (searchType == "按月份查询")
            {
                string sql = "select distinct 报表时间类型 from 月报表数据信息";
                list = SQLHelper.ExecuteReaderToString(sql, CommandType.Text, null);
                comboBox2.DataSource = list;

            }
            else if (comboBox1.SelectedValue == "按季度查询")
            {

            }
            else if (comboBox1.SelectedValue == "按年度查询")
            {

            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {


        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }
    }
}
