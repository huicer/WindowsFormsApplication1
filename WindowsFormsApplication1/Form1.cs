using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 插入操作（insert）
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public int Add(int userId, string name)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO T_Photo(Name,UserID) ");
            sb.Append("VALUES(?Name,?UserID) ");
            MySqlParameter[] parameters = {
                                     new MySqlParameter("?Name", MySqlDbType.String),
                                     new MySqlParameter("?UserID", MySqlDbType.Int32)
                                 };
            parameters[0].Value = name;
            parameters[1].Value = userId;
            return SQLHelper.ExecuteNonQuery(sb.ToString(), CommandType.Text, parameters);
        }

        /// <summary>
        /// 删除操作（delete）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int Delete(long id, int userId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM T_Photo WHERE ID = ?ID AND UserID = ?UserID");
            MySqlParameter[] parameters = {
                                     new MySqlParameter("?ID", MySqlDbType.Int64),
                                     new MySqlParameter("?UserID", MySqlDbType.Int32)
                                 };
            parameters[0].Value = id;
            parameters[1].Value = userId;
            return SQLHelper.ExecuteNonQuery(sb.ToString(), CommandType.Text, parameters);
        }

        /// <summary>
        /// 修改操作（update）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public int EditName(long id, int userId, string name)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE T_Photo SET Name = ?Name WHERE ID = ?ID AND UserID = ?UserID");
            MySqlParameter[] parameters = {
                                     new MySqlParameter("?ID", MySqlDbType.Int64),
                                     new MySqlParameter("?UserID", MySqlDbType.Int32),
                                     new MySqlParameter("?Name", MySqlDbType.String)
                                 };
            parameters[0].Value = id;
            parameters[1].Value = userId;
            parameters[2].Value = name;
            return SQLHelper.ExecuteNonQuery(sb.ToString(), CommandType.Text, parameters);
        }

        /// <summary>
        /// 查询操作（select）
        /// </summary>
        /// <param name="orderCode"></param>
        /// <returns></returns>
        public MySqlDataReader GetListByOrderCode(string orderCode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ID,OrderCount,Subtotal,ProductID, ");
            sb.Append("FROM t_orderdetail  ");

            //筛选条件
            sb.Append("WHERE OrderCode = ?OrderCode ");

            //排序
            sb.Append("ORDER BY ID DESC ");

            MySqlParameter[] parameters = {
                                     new MySqlParameter("?OrderCode", MySqlDbType.String)
                                 };

            parameters[0].Value = orderCode;
            return SQLHelper.ExecuteReader(sb.ToString(), CommandType.Text, parameters);
        }

        /// <summary>
        /// 调用存储过程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int BackOrder(long id)
        {
            StringBuilder sb = new StringBuilder();
            
            sb.Append("BackOrder ");//存储过程名称
            
            MySqlParameter[] parameters = {
                                     new MySqlParameter("?OrderId", MySqlDbType.Int64)//OrderId必须与存储过程参数名、数据类型一致
                                 };
            parameters[0].Value = id;
            return SQLHelper.ExecuteNonQuery(sb.ToString(), CommandType.StoredProcedure, parameters);
        }
        
   

        private void 测试数据库连接_Click(object sender, EventArgs e)
        {
             SQLHelper.ConnectionOK();
        }

        private void 导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FileSvr fileSvr = new FileSvr();
            //System.Data.DataTable dt = fileSvr.GetExcelDatatable("C:\\Users\\NewSpring\\Desktop\\Demo\\InExcelOutExcel\\InExcelOutExcel\\excel\\ExcelToDB.xlsx", "mapTable");
            //fileSvr.InsetData(dt);
            Form child = new Form();

            child.MdiParent = this;

            child.Show();

        }
    }
}
