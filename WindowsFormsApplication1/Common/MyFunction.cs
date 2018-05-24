
using Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class MyFunction
    {
        /// <summary>
        /// 返回＊.exe.config文件中appSettings配置节的value项
        /// </summary>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static string GetAppConfig(string strKey)
        {
            foreach (string key in ConfigurationManager.AppSettings)
            {
                if (key == strKey)
                {
                    return ConfigurationManager.AppSettings[strKey];
                }
            }
            return null;
        }

        /// <summary>
        /// 将DataTable中的内容保存到指定的数据库中
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public static int DataTableToDatabase(DataTable dt, string databaseName)
        {
            int result=0;
            DataTable dtTemp = null;

            //将"单位基本信息"表中的字段读取到listBox1中
            //ArrayList arr = MySqlHelper.ExecuteArrayList("单位基本信息");
            // listBox1.DataSource=  arr;
            //NpoiExcel.ExcelImport(fileName,arr);

            //dtTemp = dt.DefaultView.ToTable(false, new string[] { "组织机构代码", "单位名称","行政区划","行业代码","管理机构" });
            //dataGridView1.DataSource = dtTemp.DefaultView;
            //dtTemp.TableName = "单位基本信息";
            //MySqlHelper.BulkInsert(dtTemp);

            ArrayList arr = MySqlHelper.ExecuteArrayList(databaseName);
                    

            //  注意，在数据表中添加了ID字段，但在Excel中没有ID字段，所以需要去掉0索引
            //  还要在主界面中添加选择年月的日期控件，用以加入到数据表中
            string[] fieldNames = new string[arr.Count - 1];
            for (int i = 0; i < arr.Count - 1; i++)
            {
                fieldNames[i] = arr[i + 1].ToString();
            }

            try
            {
                dtTemp = dt.DefaultView.ToTable(false, fieldNames);
            }
            catch (Exception ex)
            {
                MessageBox.Show("所导入的文件的字段与数据库中的字段不匹配，请选择其他文件并重试！" + ex.Message, "错误信息");
                return -1;
            }

            //dataGridView1.DataSource = dtTemp.DefaultView;
            dtTemp.TableName = databaseName;
            int ii = 0;
            foreach (var item in fieldNames)
            {
                fieldNames[ii++] = "`" + item + "`";
            }
            List<string> dinosaurs = new List<string>(fieldNames);

            result =MySqlHelper.BulkInsert(dtTemp, dinosaurs);
            
            return result;
        }


        /// <summary>
        /// 将DataTable中的内容保存到指定的数据库中（目前还没有用处）
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="databaseName"></param>
        /// <param name="dc">添加一个指定的列</param>
        /// <returns></returns>
        public static int DataTableToDatabase(DataTable dt, string databaseName, DataColumn dc)
        {
            int result = 0;
            DataTable dtTemp = null;

            //将"单位基本信息"表中的字段读取到listBox1中
            //ArrayList arr = MySqlHelper.ExecuteArrayList("单位基本信息");
            // listBox1.DataSource=  arr;
            //NpoiExcel.ExcelImport(fileName,arr);

            //dtTemp = dt.DefaultView.ToTable(false, new string[] { "组织机构代码", "单位名称","行政区划","行业代码","管理机构" });
            //dataGridView1.DataSource = dtTemp.DefaultView;
            //dtTemp.TableName = "单位基本信息";
            //MySqlHelper.BulkInsert(dtTemp);

            ArrayList arr = MySqlHelper.ExecuteArrayList(databaseName);


            //  注意，在数据表中添加了ID字段，但在Excel中没有ID字段，所以需要去掉0索引
            //  还要在主界面中添加选择年月的日期控件，用以加入到数据表中
            string[] fieldNames = new string[arr.Count - 1];
            for (int i = 0; i < arr.Count - 1; i++)
            {
                fieldNames[i] = arr[i + 1].ToString();
            }

            try
            {
                dtTemp = dt.DefaultView.ToTable(false, fieldNames);
            }
            catch (Exception ex)
            {
                MessageBox.Show("所导入的文件的字段与数据库中的字段不匹配，请选择其他文件并重试！" + ex.Message, "错误信息");
                return -1;
            }

            //dataGridView1.DataSource = dtTemp.DefaultView;
            dtTemp.TableName = databaseName;
            int ii = 0;
            foreach (var item in fieldNames)
            {
                fieldNames[ii++] = "`" + item + "`";
            }
            List<string> dinosaurs = new List<string>(fieldNames);

            result = MySqlHelper.BulkInsert(dtTemp, dinosaurs);

            return result;
        }

    }
}
