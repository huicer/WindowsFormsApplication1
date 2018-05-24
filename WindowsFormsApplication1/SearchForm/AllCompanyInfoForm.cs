using System;
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
    public partial class AllCompanyInfoForm : Form
    {
        public AllCompanyInfoForm()
        {
            InitializeComponent();
        }

        private void AllCompanyInfoForm_Load(object sender, EventArgs e)
        {

        }

        #region  显示“职工基本信息”表中的指定记录
        /// <summary>
        /// 动态读取指定的记录行，并进行显示.
        /// </summary>
        /// <param name="DGrid">DataGridView控件</param>
        /// <returns>返回string对象</returns> 
        //public string Grid_Inof(DataGridView DGrid)
        //{
        //    byte[] pic; //定义一个字节数组
        //    //当DataGridView控件的记录>1时，将当前行中信息显示在相应的控件上
        //    if (DGrid.RowCount > 1)
        //    {
        //        S_0.Text = DGrid[0, DGrid.CurrentCell.RowIndex].Value.ToString();
        //        S_1.Text = DGrid[1, DGrid.CurrentCell.RowIndex].Value.ToString();
        //        S_2.Text = Convert.ToString(DGrid[2, DGrid.CurrentCell.RowIndex].Value).Trim();
        //        S_3.Text = MyMC.Date_Format(Convert.ToString(DGrid[3, DGrid.CurrentCell.RowIndex].Value).Trim());
        //        S_4.Text = Convert.ToString(DGrid[4, DGrid.CurrentCell.RowIndex].Value).Trim();
        //        S_5.Text = DGrid[5, DGrid.CurrentCell.RowIndex].Value.ToString();
        //        S_6.Text = DGrid[6, DGrid.CurrentCell.RowIndex].Value.ToString();
        //        S_7.Text = DGrid[7, DGrid.CurrentCell.RowIndex].Value.ToString();
        //        S_8.Text = DGrid[8, DGrid.CurrentCell.RowIndex].Value.ToString();
        //        S_9.Text = DGrid[9, DGrid.CurrentCell.RowIndex].Value.ToString();
        //        S_10.Text = MyMC.Date_Format(Convert.ToString(DGrid[10, DGrid.CurrentCell.RowIndex].Value).Trim());
        //        S_11.Text = Convert.ToString(DGrid[11, DGrid.CurrentCell.RowIndex].Value).Trim();
        //        S_12.Text = DGrid[12, DGrid.CurrentCell.RowIndex].Value.ToString();
        //        S_13.Text = DGrid[13, DGrid.CurrentCell.RowIndex].Value.ToString();
        //        S_14.Text = DGrid[14, DGrid.CurrentCell.RowIndex].Value.ToString();
        //        S_15.Text = DGrid[15, DGrid.CurrentCell.RowIndex].Value.ToString();
        //        S_16.Text = DGrid[16, DGrid.CurrentCell.RowIndex].Value.ToString();
        //        S_17.Text = DGrid[17, DGrid.CurrentCell.RowIndex].Value.ToString();
        //        S_18.Text = DGrid[18, DGrid.CurrentCell.RowIndex].Value.ToString();
        //        S_19.Text = DGrid[19, DGrid.CurrentCell.RowIndex].Value.ToString();
        //        S_20.Text = DGrid[20, DGrid.CurrentCell.RowIndex].Value.ToString();
        //        S_21.Text = MyMC.Date_Format(Convert.ToString(DGrid[21, DGrid.CurrentCell.RowIndex].Value).Trim());
        //        S_22.Text = DGrid[22, DGrid.CurrentCell.RowIndex].Value.ToString();
        //        S_23.Text = DGrid[24, DGrid.CurrentCell.RowIndex].Value.ToString();
        //        S_24.Text = DGrid[25, DGrid.CurrentCell.RowIndex].Value.ToString();
        //        S_25.Text = Convert.ToString(DGrid[26, DGrid.CurrentCell.RowIndex].Value).Trim();
        //        S_26.Text = DGrid[27, DGrid.CurrentCell.RowIndex].Value.ToString();
        //        S_27.Text = MyMC.Date_Format(Convert.ToString(DGrid[28, DGrid.CurrentCell.RowIndex].Value).Trim());
        //        S_28.Text = MyMC.Date_Format(Convert.ToString(DGrid[29, DGrid.CurrentCell.RowIndex].Value).Trim());
        //        S_29.Text = Convert.ToString(DGrid[30, DGrid.CurrentCell.RowIndex].Value).Trim();
        //        try
        //        {
        //            //将数据库中的图片存入到字节数组中
        //            pic = (byte[])(MyDS_Grid.Tables[0].Rows[DGrid.CurrentCell.RowIndex][23]);
        //            MemoryStream ms = new MemoryStream(pic);			//将字节数组存入到二进制流中
        //            S_Photo.Image = Image.FromStream(ms);   //二进制流Image控件中显示
        //        }
        //        catch { S_Photo.Image = null; } //当出现错误时，将Image控件清空
        //        tem_ID = S_0.Text.Trim();   //获取当前职伯编号
        //        return DGrid[1, DGrid.CurrentCell.RowIndex].Value.ToString();   //返回当前职工的姓名
        //    }
        //    else
        //    {
        //        //使用MyMeans公共类中的Clear_Control()方法清空指定控件集中的相应控件
        //        MyMC.Clear_Control(tabControl1.TabPages[0].Controls);
        //        tem_ID = "";
        //        return "";
        //    }
        //}
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Img_Save_Click(object sender, EventArgs e)
        {

        }
    }
}
