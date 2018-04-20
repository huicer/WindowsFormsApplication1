using Helpers;
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
            openFileDialog.Filter = "Excel97-2003文件(*.xls)|*.xls|Excel文件(*.xlsx)|*.xlsx|所有文件(*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                DataTable dt = null;
                listBox1.DataSource=  MySqlHelper.ExecuteArrayList("mytest", "student");
                //NpoiExcel.ExcelImport(fileName);
               // dt = NpoiExcel.ExcelToTable(fileName);
               // dt.TableName = "t_photo";
               //dataGridView1.DataSource = dt.DefaultView;
               // MySqlHelper.BulkInsert(dt);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
