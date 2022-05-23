using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_Ortner_Szuesz.DAL.Configuration;
using TourPlanner_Ortner_Szuesz.Models;

namespace TourPlanner_Ortner_Szuesz.BL.PDF_Generation
{
    public class TourReportPDF
    {
        public void PrintTourReport(Tour tourItem, ObservableCollection<TourLog> tourLogs)
        {
            if(tourItem == null)
            {
                return;
            }

            // get file path
            var config = TourPlannerConfigurationManager.GetConfig();
            string reportPath = Path.Combine(Directory.GetCurrentDirectory(), config.ReportLocation, $"TourReport_{tourItem.Name.ToString()}.pdf");
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), tourItem.RouteImagePath);

            // calculate values
            CalculateTourAttributes calcValues = new CalculateTourAttributes();

            // delete item if exists -> instead of using new process to save new one
            if(File.Exists(reportPath))
            {
                File.Delete(reportPath);
            }

            PdfWriter pdfWriter = new PdfWriter(reportPath);
            PdfDocument tourReport = new PdfDocument(pdfWriter);
            Document tourDocument = new Document(tourReport);

            // header
            Paragraph header = new Paragraph("Tour Report")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(20);
            tourDocument.Add(header);

            // sub header -> tour name
            Paragraph subHeader = new Paragraph(tourItem.Name)
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(16);
            tourDocument.Add(subHeader);

            // separte content
            LineSeparator ls = new LineSeparator(new SolidLine());
            tourDocument.Add(ls);

            // space
            tourDocument.Add(new Paragraph());

            // tour description
            Table table = new Table(2, true);
            Cell cell11 = new Cell(1, 1)
                  .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                  .Add(new Paragraph("Name"));
            Cell cell12 = new Cell(1, 1)
               .Add(new Paragraph(tourItem.Name));

            Cell cell21 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .Add(new Paragraph("Description"));
            Cell cell22 = new Cell(1, 1)
               .Add(new Paragraph(tourItem.Description));

            Cell cell31 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .Add(new Paragraph("Start"));
            Cell cell32 = new Cell(1, 1)
               .Add(new Paragraph(tourItem.StartLocation));

            Cell cell41 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .Add(new Paragraph("Destination"));
            Cell cell42 = new Cell(1, 1)
               .Add(new Paragraph(tourItem.EndLocation));

            Cell cell51 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .Add(new Paragraph("Transport Type"));
            Cell cell52 = new Cell(1, 1)
               .Add(new Paragraph(tourItem.TransportType.ToString()));

            Cell cell61 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .Add(new Paragraph("Distance (km)"));
            Cell cell62 = new Cell(1, 1)
               .Add(new Paragraph(tourItem.Distance.ToString()));

            Cell cell71 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .Add(new Paragraph("Estimated Time (min)"));
            Cell cell72 = new Cell(1, 1)
               .Add(new Paragraph((tourItem.EstimatedTime/60).ToString()));

            Cell cell81 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .Add(new Paragraph("Popularity"));
            Cell cell82 = new Cell(1, 1)
               .Add(new Paragraph((calcValues.CalculatePopularity(tourLogs).ToString())));

            Cell cell91 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .Add(new Paragraph("Child-Friendliness"));
            Cell cell92 = new Cell(1, 1)
               .Add(new Paragraph((calcValues.CalculateChildFriedliness(tourItem, tourLogs) ? "Yes" : "No")));

            table.AddCell(cell11);
            table.AddCell(cell12);
            table.AddCell(cell21);
            table.AddCell(cell22);
            table.AddCell(cell31);
            table.AddCell(cell32);
            table.AddCell(cell41);
            table.AddCell(cell42);
            table.AddCell(cell51);
            table.AddCell(cell52);
            table.AddCell(cell61);
            table.AddCell(cell62);
            table.AddCell(cell71);
            table.AddCell(cell72);
            table.AddCell(cell81);
            table.AddCell(cell82);
            table.AddCell(cell91);
            table.AddCell(cell92);
            tourDocument.Add(table);

            // space
            tourDocument.Add(new Paragraph());

            // tour map
            Image img = new Image(ImageDataFactory
            .Create(imagePath))
            .SetTextAlignment(TextAlignment.CENTER);

            img.ScaleAbsolute(300, 300);
            img.SetHorizontalAlignment(HorizontalAlignment.CENTER);
            tourDocument.Add(img);

            // space
            tourDocument.Add(new Paragraph());

            // sub header -> tour name
            Paragraph subHeaderTourLogs = new Paragraph("Tour Logs")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(16);
            tourDocument.Add(subHeaderTourLogs);

            // space
            tourDocument.Add(new Paragraph());

            // no tourlogs
            if (tourLogs.Count <= 0)
            {
                tourDocument.Add(new Paragraph("There are no tour logs at this time!").SetTextAlignment(TextAlignment.CENTER));
            }
            else
            {
                // tour log table
                Table tableTourLogs = new Table(5, true);
                Cell header1 = new Cell(1, 1)
                     .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                     .Add(new Paragraph("Date"));

                Cell header2 = new Cell(1, 1)
                     .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                     .Add(new Paragraph("Difficulty"));

                Cell header3 = new Cell(1, 1)
                     .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                     .Add(new Paragraph("Total Time"));

                Cell header4 = new Cell(1, 1)
                     .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                     .Add(new Paragraph("Rating"));

                Cell header5 = new Cell(1, 1)
                     .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                     .Add(new Paragraph("Comment"));

                tableTourLogs.AddCell(header1);
                tableTourLogs.AddCell(header2);
                tableTourLogs.AddCell(header3);
                tableTourLogs.AddCell(header4);
                tableTourLogs.AddCell(header5);

                foreach(TourLog tourLog in tourLogs)
                {
                    Cell content1 = new Cell(1, 1)
                         .Add(new Paragraph(tourLog.Date.ToString("d")));

                    Cell content2 = new Cell(1, 1)
                         .Add(new Paragraph(tourLog.Difficulty.ToString()));

                    Cell content3 = new Cell(1, 1)
                         .Add(new Paragraph(tourLog.TotalTime.ToString()));

                    Cell content4 = new Cell(1, 1)
                         .Add(new Paragraph(tourLog.Rating.ToString()));

                    Cell content5 = new Cell(1, 1)
                         .Add(new Paragraph(tourLog.Comment));

                    tableTourLogs.AddCell(content1);
                    tableTourLogs.AddCell(content2);
                    tableTourLogs.AddCell(content3);
                    tableTourLogs.AddCell(content4);
                    tableTourLogs.AddCell(content5);
                }
                // add generated table to document
                tourDocument.Add(tableTourLogs);
            }

            tourDocument.Close();
        }
    }
}
