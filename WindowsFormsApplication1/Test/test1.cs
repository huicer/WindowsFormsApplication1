using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class test1
    {
        static void Main1(string[] args)
        {
            //TestConnection();  
            //InsertTest();  
            //ExecuteDataTableTest();  

            string sql = "select count(1) from user";
            int rowCount = Convert.ToInt32(MySqlHelper1.ExecuteScalar(sql));
            Console.WriteLine(rowCount);

            Console.Read();
        }

        private static void ExecuteDataTableTest()
        {
            string sql = "select * from user";
            DataTable dt = MySqlHelper1.ExecuteDataTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    Console.Write("{0}:{1}\t", dc.ColumnName, dr[dc.ColumnName].ToString());
                }
                Console.WriteLine();
            }
        }

        private static void InsertTest()
        {
            string sql = "insert into user(Id,Name) values(?Id,?Name);";
            MySqlParameter[] spArr = new MySqlParameter[]{
                new MySqlParameter("Id",2),
                new MySqlParameter("Name","leaf")
            };
            int r = MySqlHelper1.ExecuteNonQuery(sql, spArr);
            Console.WriteLine(r);
        }

        private static void TestConnection()
        {
            //测试连接是否正常  
            using (MySqlConnection conn = MySqlHelper1.GetConnection)
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("打开 My SQL 连接成功！");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("打开 MySQL 连接失败，错误原因：" + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
