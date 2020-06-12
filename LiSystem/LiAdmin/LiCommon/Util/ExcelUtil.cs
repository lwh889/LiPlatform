using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using NPOI.POIFS.FileSystem;
using NPOI;
using NPOI.OpenXml4Net.OPC;

namespace LiCommon.Util
{
    /// <summary>
    /// Excel工具类
    /// </summary>
    public class ExcelUtil
    {
        
        private int insertRowIndex;
        private int insertRowCount;
        private Dictionary<int, string> insertData;
        private IWorkbook workbook;

        public ExcelUtil(int insertRowIndex, int insertRowCount,Dictionary<int,string> insertData=null)
        {
            if (insertData!=null)
            {
                this.insertData = insertData;
            }
            this.insertRowIndex = insertRowIndex;
            this.insertRowCount = insertRowCount;
        }

        public ExcelUtil()
        { }
        private IWorkbook NPOIOpenExcel(string filename)
        {
            IWorkbook myworkBook;
            Stream excelStream = OpenResource(filename);
            if (POIFSFileSystem.HasPOIFSHeader(excelStream))
                return new HSSFWorkbook(excelStream);
            if (POIXMLDocument.HasOOXMLHeader(excelStream))
            {
                return new XSSFWorkbook(OPCPackage.Open(excelStream));
            }
            if (filename.EndsWith(".xlsx"))
            {
                return new XSSFWorkbook(excelStream);
            }
            if (filename.EndsWith(".xls"))
            {
                new HSSFWorkbook(excelStream);
            }
            throw new Exception("Your InputStream was neither an OLE2 stream, nor an OOXML stream");
        }

        private Stream OpenResource(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            return fs;
        }

        private void InsertRow(ISheet sheet,int insertRowIndex,int insertRowCount,IRow formatRow)
        {
            ICellStyle styleText= sheet.Workbook.CreateCellStyle();
            IDataFormat dataformat = sheet.Workbook.CreateDataFormat();
            styleText.DataFormat = dataformat.GetFormat("@");
            sheet.ShiftRows(insertRowIndex, sheet.LastRowNum, insertRowCount, true, false);
            for (int i = insertRowIndex; i < insertRowIndex+insertRowCount; i++)
            {
                IRow targetRow = null;
                ICell sourceCell = null;
                ICell targetCell = null;
                targetRow = sheet.CreateRow(i);
                for (int m = formatRow.FirstCellNum; m < formatRow.LastCellNum; m++)
                {
                    sourceCell = formatRow.GetCell(m, MissingCellPolicy.CREATE_NULL_AS_BLANK);
                    if (sourceCell==null)
                    {
                        continue;
                    }
                    targetCell = targetRow.CreateCell(m);
                }
            }

            for (int i = insertRowIndex; i < insertRowIndex + insertRowCount; i++)
            {
                IRow firstTargetRow = sheet.GetRow(i);
                ICell firstSourceCell = null;
                ICell firstTargetCell = null;

                for (int m = formatRow.FirstCellNum; m < formatRow.LastCellNum; m++)
                {
                    firstSourceCell = formatRow.GetCell(m, MissingCellPolicy.CREATE_NULL_AS_BLANK);
                    if (firstSourceCell == null)
                    {
                        continue;
                    }
                    firstTargetCell = firstTargetRow.GetCell(m, MissingCellPolicy.CREATE_NULL_AS_BLANK);
                    if (this.insertData!=null&&this.insertData.Count>0)
                    {
                        firstTargetCell.SetCellValue(insertData[m]);
                    }
                    firstTargetCell.SetCellValue("test");
                }
            }


           
        }

        public void WriteToFile(IWorkbook workbook,string filename)
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            using (FileStream fs=new FileStream(filename,FileMode.OpenOrCreate,FileAccess.Write))
            {
                workbook.Write(fs);
                fs.Close();
            }
        }

        public void OpenExcel(string filename)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = filename;
            process.StartInfo.ErrorDialog = true;
            process.Start();
        }

        public void EditorExcel(string savePath, string readPath, ExcelUtil oe)
        {
            try
            {
                IWorkbook workbook = oe.NPOIOpenExcel(readPath);
                if (workbook == null)
                {
                    return;
                }
                int sheetNum = workbook.NumberOfSheets;
                for (int i = 0; i < sheetNum; i++)
                {
                    ISheet mysheet = workbook.GetSheetAt(i);
                    IRow mySourceRow = mysheet.GetRow(insertRowIndex);
                    oe.InsertRow(mysheet, insertRowIndex, insertRowCount, mySourceRow);
                    
                }

                oe.WriteToFile(workbook, savePath);
                oe.OpenExcel(savePath);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
           
        }


        public int DataTableToExcel(DataTable data, string sheetName, bool isColumnWritten,string fileName)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;
            if (File.Exists(fileName))
                File.Delete(fileName);
            var fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            if (fileName.IndexOf(".xlsx") > 0) 
                workbook = new XSSFWorkbook();
            else if (fileName.IndexOf(".xls") > 0) 
                workbook = new HSSFWorkbook();

            try
            {
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    return -1;
                }

                if (isColumnWritten == true) 
                {
                    IRow row = sheet.CreateRow(0);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                    }
                    count = 1;
                }
                else
                {
                    count = 0;
                }

                for (i = 0; i < data.Rows.Count; ++i)
                {
                    IRow row = sheet.CreateRow(count);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                    }
                    ++count;
                }
                workbook.Write(fs); 
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return -1;
            }
        }

     
        public static DataTable ExcelToDataTable(string fileName, string sheetName = null, bool isFirstRowColumn=true)
        {
            IWorkbook workbook = null;
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
               var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0)
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) 
                    workbook = new HSSFWorkbook(fs);

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) 
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; 

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                 
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; 　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null)
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }
    }
}
