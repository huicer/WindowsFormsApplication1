using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class NpoiExcel
    {


        /// <summary>
        /// Excel导入成Datable
        /// </summary>
        /// <param name="file">导入路径(包含文件名与扩展名)</param>
        /// <returns></returns>
        public static DataTable ExcelToTable(string file)
        {
            DataTable dt = new DataTable();
            IWorkbook workbook;
            string fileExt = Path.GetExtension(file).ToLower();
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
                if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(fs); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(fs); } else { workbook = null; }
                if (workbook == null) { return null; }
                ISheet sheet = workbook.GetSheetAt(0);

                //表头  
                IRow header = sheet.GetRow(sheet.FirstRowNum);
                List<int> columns = new List<int>();
                for (int i = 0; i < header.LastCellNum; i++)
                {
                    object obj = GetValueType(header.GetCell(i));
                    if (obj == null || obj.ToString() == string.Empty)
                    {
                        dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                    }
                    else
                        dt.Columns.Add(new DataColumn(obj.ToString()));
                    columns.Add(i);
                }
                //数据  
                for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                {
                    DataRow dr = dt.NewRow();
                    bool hasValue = false;
                    foreach (int j in columns)
                    {
                        dr[j] = GetValueType(sheet.GetRow(i).GetCell(j));
                        if (dr[j] != null && dr[j].ToString() != string.Empty)
                        {
                            hasValue = true;
                        }
                    }
                    if (hasValue)
                    {
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// Excel导入成Datable
        /// </summary>
        /// <param name="file">导入路径(包含文件名与扩展名)</param>
        /// <returns></returns>
        public static DataTable ExcelToTable(string file,ArrayList arr)
        {
            DataTable dt = new DataTable();
            IWorkbook workbook;
            string fileExt = Path.GetExtension(file).ToLower();
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
                if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(fs); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(fs); } else { workbook = null; }
                if (workbook == null) { return null; }
                ISheet sheet = workbook.GetSheetAt(0);

                //表头  
                IRow header = sheet.GetRow(sheet.FirstRowNum);
                List<int> columns = new List<int>();
                for (int i = 0; i < header.LastCellNum; i++)
                {
                    object obj = GetValueType(header.GetCell(i));
                    if (obj == null || obj.ToString() == string.Empty)
                    {
                        dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                    }
                    else
                        dt.Columns.Add(new DataColumn(obj.ToString()));
                    columns.Add(i);
                }
                //数据  
                for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                {
                    DataRow dr = dt.NewRow();
                    bool hasValue = false;
                    foreach (int j in columns)
                    {
                        dr[j] = GetValueType(sheet.GetRow(i).GetCell(j));
                        if (dr[j] != null && dr[j].ToString() != string.Empty)
                        {
                            hasValue = true;
                        }
                    }
                    if (hasValue)
                    {
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }


        /// <summary>
        /// Datable导出成Excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="file">导出路径(包括文件名与扩展名)</param>
        public static void TableToExcel(DataTable dt, string file)
        {
            IWorkbook workbook;
            string fileExt = Path.GetExtension(file).ToLower();
            if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(); } else { workbook = null; }
            if (workbook == null) { return; }
            ISheet sheet = string.IsNullOrEmpty(dt.TableName) ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet(dt.TableName);

            //表头  
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].ColumnName);
            }

            //数据  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                }
            }

            //转为字节数组  
            MemoryStream stream = new MemoryStream();
            workbook.Write(stream);
            var buf = stream.ToArray();

            //保存为Excel文件  
            using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                fs.Write(buf, 0, buf.Length);
                fs.Flush();
            }
        }

        /// <summary>
        /// 获取单元格类型
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static object GetValueType(ICell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.Blank: //BLANK:  
                    return null;
                case CellType.Boolean: //BOOLEAN:  
                    return cell.BooleanCellValue;
                case CellType.Numeric: //NUMERIC:  
                    return cell.NumericCellValue;
                case CellType.String: //STRING:  
                    return cell.StringCellValue;
                case CellType.Error: //ERROR:  
                    return cell.ErrorCellValue;
                case CellType.Formula: //FORMULA:  
                default:
                    return "=" + cell.CellFormula;
            }
        }

        /// <summary>
        /// 将Excel文件导入到数据库系统中
        /// </summary>
        /// <param name="fileName"></param>
        public static void ExcelImport(string fileName)
        {
            IWorkbook workbook = null;  //新建IWorkbook对象  
            //string fileName = "E:\\Excel2003.xls";
            //FileStream fileStream = new FileStream(@"E:\Excel2003.xls", FileMode.Open, FileAccess.Read);
            FileStream fileStream = null;
            try
            {
                fileStream =  new FileStream(@fileName, FileMode.Open, FileAccess.Read);
            }
            catch (Exception)
            {

                throw;
            }
          

           if (fileName.IndexOf(".xlsx") > 0) // 2007版本  
            {
                workbook = new XSSFWorkbook(fileStream);  //xlsx数据读入workbook  
            }
            else if (fileName.IndexOf(".xls") > 0) // 2003版本  
            {
                workbook = new HSSFWorkbook(fileStream);  //xls数据读入workbook  
            }
            ISheet sheet = workbook.GetSheetAt(0);  //获取第一个工作表  
            IRow row;// = sheet.GetRow(0);            //新建当前工作表行数据  

            for (int i = 0; i < sheet.LastRowNum; i++)  //对工作表每一行  
            {
                row = sheet.GetRow(i);   //row读入第i行数据  
                if (row != null)
                {
                    for (int j = 0; j < row.LastCellNum; j++)  //对工作表每一列  
                    {
                        string cellValue = row.GetCell(j).ToString(); //获取i行j列数据  
                        //Console.WriteLine(cellValue);

                    }
                }
            }
            //Console.ReadLine();
            fileStream.Close();
            workbook.Close();
        }

        /// <summary>
        /// 将Excel文件导入到数据库系统中
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="arr"></param>
        public static void ExcelImport(string fileName, ArrayList arr)
        {
            IWorkbook workbook = null;  //新建IWorkbook对象  
            //string fileName = "E:\\Excel2003.xls";
            //FileStream fileStream = new FileStream(@"E:\Excel2003.xls", FileMode.Open, FileAccess.Read);
            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(@fileName, FileMode.Open, FileAccess.Read);
            }
            catch (Exception)
            {
                MessageBox.Show("所导入的文件正在被其他程序使用，请关闭其他程序并重试！","错误信息");
                //throw;
            }


            if (fileName.IndexOf(".xlsx") > 0) // 2007版本  
            {
                workbook = new XSSFWorkbook(fileStream);  //xlsx数据读入workbook  
            }
            else if (fileName.IndexOf(".xls") > 0) // 2003版本  
            {
                workbook = new HSSFWorkbook(fileStream);  //xls数据读入workbook  
            }

            ISheet sheet = workbook.GetSheetAt(0);  //获取第一个工作表  

            //将表头数据与数据库中的字段做对比，并记录下Excel中的相应的列

            //IRow row= sheet.GetRow(0);            //获取当前工作表第一行表头数据  
            //List<int> list1 = new List<int>();
            //新增数据            list1.Add(123);
            //修改数据            list1[0] = 345;
            //移除数据            list1.RemoveAt(0);
            //for (int j = 0; j < row.LastCellNum; j++)  //对工作表每一列  
            //{
            //    int temp;
            //    string cellValue = row.GetCell(j).ToString(); //获取表头j列数据  
            //    Console.WriteLine(cellValue);
            //    temp = arr.IndexOf(cellValue);//temp为-1表示没有搜索到
            //    if (temp>0)
            //    {
            //        list1.Add(j);
            //        arr.RemoveAt(temp); //将ArrsyList中的数据删除掉匹配的项
            //    }


            //可改进的办法
            //考虑arr中的元素为空，（有可能只剩Excel中没有的元素时，退出循环）
            //或者 将Excel表头中的字段名放入List中，对arr中的元素进行筛选

            IRow row = sheet.GetRow(0);            //获取当前工作表第一行表头数据  
            List<int> list1 = new List<int>();
            List<string> excellist = new List<string>();
            List<string> tablelist = new List<string>();
            //将Excel表头数据放到List中
            for (int j = 0; j < row.LastCellNum; j++)  //对工作表每一列  
            {
                  string cellValue = row.GetCell(j).ToString(); //获取表头j列数据
                excellist.Add(cellValue);
            }
            //将数据表的字段名放入List中
            foreach (var item in arr)
            {
                tablelist.Add(item.ToString());
                Console.WriteLine(item.ToString());
            }
            int temp=-1;

            //将与数据表中的字段匹配的Excel中的列的位置放入List中
            for (int i = 0; i < tablelist.Count; i++)
            {
                temp = excellist.IndexOf(tablelist[i]);//temp为-1表示没有搜索到
                    //将ArrsyList中的数据删除掉匹配的项
                    //excellist.RemoveAt(temp); 
                    list1.Add(temp);
              
            }

            //list的遍历 
            string strTest="";
            foreach (int i in list1)
                strTest += i.ToString() + " ";
            //格式化后输出 
            Console.Write(string.Format("Out:{0} nCount:{1}n", strTest, list1.Count));
          

            //将Excel中与数据库所指定的表相对应的字段匹配的内容，导入到数据库中
            for (int i = 1; i < sheet.LastRowNum; i++)  //对工作表每一行  ,从1 行开始，0行为表头
            {
                 ArrayList rowlist = new ArrayList();
                row = sheet.GetRow(i);   //row读入第i行数据 

                //将row中的数据存放到数据库表的相应字段中，多余的舍弃
                if (row != null)
                {
                    //for (int j = 0; j < row.LastCellNum; j++)  //对工作表每一列  
                    //{
                    //    string cellValue = row.GetCell(j).ToString(); //获取i行j列数据  
                    //    //Console.WriteLine(cellValue);
                    //}
                    foreach (int j in list1)
                    {
                        if (j==-1)
                        {
                            continue;
                        }
                        string cellValue = row.GetCell(j).ToString(); //获取i行j列数据  
                        rowlist.Add(cellValue);
                        //Console.WriteLine(cellValue);
                    }


                }
            }
           // Console.ReadLine();
            fileStream.Close();
            workbook.Close();
        }


        /// <summary>
        /// 将数据库中的数据导出到Excel文件中
        /// </summary>
        /// <param name="fileName"></param>
        public static void ExcelExport(string fileName)
        {
            //HSSF可以读取xls格式的Excel文件
            IWorkbook workbook = new HSSFWorkbook();
            //XSSF可以读取xlsx格式的Excel文件
            //IWorkbook workbook = new XSSFWorkbook();

            //Excel文件至少要有一个工作表sheet
            ISheet sheet = workbook.CreateSheet("工作表");
            //创建行
            for (int i = 0; i < 10; i++)
            {
                IRow row = sheet.CreateRow(i); //i表示了创建行的索引，从0开始
                //创建单元格
                for (int j = 0; j < 5; j++)
                {
                    ICell cell = row.CreateCell(j);  //同时这个函数还有第二个重载，可以指定单元格存放数据的类型
                    cell.SetCellValue(i.ToString() + j.ToString());
                }
            }

            //表格制作完成后，保存
            //创建一个文件流对象
            using (FileStream fs = File.Open("test.xls", FileMode.OpenOrCreate))
            {
                workbook.Write(fs);
                //最后记得关闭对象
                workbook.Close();
            }

        }
    
    }
    
}
