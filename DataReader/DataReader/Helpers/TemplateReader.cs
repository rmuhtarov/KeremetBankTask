using DataReader.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataReader.Helpers
{
    public class TemplateReader
    {
        public static void ReadAndCreateFile(string templateFilePath, string resultFilePath, Client client)
        {
            try
            {
                IWorkbook workbook;
                FileStream fs = new FileStream(templateFilePath, FileMode.Open, FileAccess.Read);
                if (Path.GetExtension(templateFilePath).ToLower().Equals(".xlsx"))
                    workbook = new XSSFWorkbook(fs);
                else if (Path.GetExtension(templateFilePath).ToLower().Equals(".xls"))
                    workbook = new HSSFWorkbook(fs);
                else
                    throw new Exception("Excel file not found.");

                //First sheet
                ISheet sheet = workbook.GetSheetAt(0);
                if (sheet != null)
                {
                    int rowCount = sheet.LastRowNum; // This may not be valid row count.
                                                     // If first row is table head, i starts from 1
                    for (int i = 1; i <= rowCount; i++)
                    {
                        IRow curRow = sheet.GetRow(i);
                        // Works for consecutive data. Use continue otherwise 
                        if (curRow == null)
                        {
                            // Valid row count
                            rowCount = i - 1;
                            break;
                        }
                        // Get data from the 4th column (4th cell of each row)
                        var idCells = curRow.Cells.FindAll(c => c.ToString().ToUpper() == "[ID]");
                        var nameCells = curRow.Cells.FindAll(c => c.ToString().ToUpper() == "[NAME]");
                        var birthDateCells = curRow.Cells.FindAll(c => c.ToString().ToUpper() == "[BIRTHDATE]");
                        var phoneCells = curRow.Cells.FindAll(c => c.ToString().ToUpper() == "[PHONENUMBER]");
                        var addressCells = curRow.Cells.FindAll(c => c.ToString().ToUpper() == "[ADDRESS]");
                        var innCells = curRow.Cells.FindAll(c => c.ToString().ToUpper() == "[SOCIALNUMBER]");
                        
                        if(idCells.Count > 0)
                        {
                            foreach(var cell in idCells)
                            {
                                cell.SetCellValue(client.Id);
                            }
                        }
                        if (nameCells.Count > 0)
                        {
                            foreach (var cell in nameCells)
                            {
                                cell.SetCellValue(client.Fio);
                            }
                        }
                        if (birthDateCells.Count > 0)
                        {
                            foreach (var cell in birthDateCells)
                            {
                                cell.SetCellValue(client.BirthDate.ToString("dd.MM.yyyy"));
                            }
                        }
                        if (phoneCells != null)
                        {
                            foreach (var cell in phoneCells)
                            {
                                cell.SetCellValue(client.PhoneNumber);
                            }
                        }
                        if (addressCells != null)
                        {
                            foreach (var cell in addressCells)
                            {
                                cell.SetCellValue(client.Address);
                            }
                        }
                        if (innCells != null)
                        {
                            foreach (var cell in innCells)
                            {
                                cell.SetCellValue(client.SocialNumber.ToString());
                            }
                        }

                    }
                }

                using (FileStream resFs = new FileStream(resultFilePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    workbook.Write(resFs);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
