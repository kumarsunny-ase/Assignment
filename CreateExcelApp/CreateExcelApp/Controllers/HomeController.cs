using ClosedXML.Excel;
using CreateExcelApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CreateExcelApp.Controllers
{
    public class HomeController : Controller
    {

        List<Sample> samples = new List<Sample>();

        public HomeController()
        {
            for (int i = 0; i <= 1; i++)
            {
                samples.Add(new Sample()
                {
                    Id = i,
                    Name = "Name",
                    Country = "Country",
                    Diligence = "Diligence",
                    Status = "Approval",
                    Data = "Data",
                    QStatus = "QStatus",
                    TStatus = "TStatus"


                });
            }
        }

        public IActionResult Index()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Samples");
                var  currentRow = 1;

              //  worksheet.Cell(currentRow, 1).Value = "Id";
                worksheet.Cell(currentRow, 2).Value = "TRP Name";
                worksheet.Cell(currentRow, 3).Value = "Counrty";
                worksheet.Cell(currentRow, 4).Value = "Diligence Level";
                worksheet.Cell(currentRow, 5).Value = "Approval Status";
                worksheet.Cell(currentRow, 6).Value = "Data Approved";
                worksheet.Cell(currentRow, 7).Value = "Questionnaire Status";
                worksheet.Cell(currentRow, 8).Value = "Training Status";


                foreach (var Sample in samples)
                {
                    currentRow++;
                   // worksheet.Cell(currentRow, 1).Value = Sample.Id;
                    worksheet.Cell(currentRow, 2).Value = Sample.Name;
                    worksheet.Cell(currentRow, 3).Value = Sample.Country;
                    worksheet.Cell(currentRow, 4).Value = Sample.Diligence;
                    worksheet.Cell(currentRow, 5).Value = Sample.Status;
                    worksheet.Cell(currentRow, 6).Value = Sample.Data;
                    worksheet.Cell(currentRow, 7).Value = Sample.QStatus;
                    worksheet.Cell(currentRow, 8).Value = Sample.TStatus;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Samples.xlsx"
                        );
                }
            }
            
        }

       
    }
}
