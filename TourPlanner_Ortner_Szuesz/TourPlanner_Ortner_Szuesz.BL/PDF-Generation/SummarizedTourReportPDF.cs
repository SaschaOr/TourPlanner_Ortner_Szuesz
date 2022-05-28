using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.Extensions.Logging;
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
    public class SummarizedTourReportPDF
    {
        public ILogger Logger { get; }

        public SummarizedTourReportPDF(ILogger logger)
        {
            Logger = logger;
        }

        public bool PrintSummarizedTourReport(ObservableCollection<Tour> tours)
        {
            if(tours.Count <= 0)
            {
                return false;
            }

            // get file path
            var config = TourPlannerConfigurationManager.GetConfig();
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), config.ReportLocation);

            if(!Directory.Exists(directoryPath))
            {
                Logger.LogWarning($"{DateTime.Now}: [WARNING] directory path for summarized tour report does not exist.");
                return false;
            }

            string reportPath = Path.Combine(directoryPath, "SummarizedTourReport.pdf");

            // calculate values
            CalculateTourAttributes calcValues = new CalculateTourAttributes();

            PdfWriter pdfWriter = new PdfWriter(reportPath);
            PdfDocument tourReport = new PdfDocument(pdfWriter);
            Document tourDocument = new Document(tourReport);

            // header
            Paragraph header = new Paragraph("Summarized Tour Report")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(20);
            tourDocument.Add(header);

            // separte content
            LineSeparator ls = new LineSeparator(new SolidLine());
            tourDocument.Add(ls);

            // space
            tourDocument.Add(new Paragraph());

            // tour log table
            Table tableTourLogs = new Table(6, true);
            Cell header1 = new Cell(1, 1)
                    .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                    .Add(new Paragraph("Tour Name"));

            Cell header2 = new Cell(1, 1)
                    .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                    .Add(new Paragraph("Start"));

            Cell header3 = new Cell(1, 1)
                    .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                    .Add(new Paragraph("Destination"));

            Cell header4 = new Cell(1, 1)
                    .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                    .Add(new Paragraph("Average Time"));

            Cell header5 = new Cell(1, 1)
                    .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                    .Add(new Paragraph("Average Distance"));

            Cell header6 = new Cell(1, 1)
                    .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                    .Add(new Paragraph("Average Rating"));

            tableTourLogs.AddCell(header1);
            tableTourLogs.AddCell(header2);
            tableTourLogs.AddCell(header3);
            tableTourLogs.AddCell(header4);
            tableTourLogs.AddCell(header5);
            tableTourLogs.AddCell(header6);

            foreach (Tour tour in tours)
            {
                ObservableCollection<TourLog> logs = FillTourLogList(tour.Id);

                Cell content1 = new Cell(1, 1)
                        .Add(new Paragraph(tour.Name));

                Cell content2 = new Cell(1, 1)
                        .Add(new Paragraph(tour.StartLocation));

                Cell content3 = new Cell(1, 1)
                        .Add(new Paragraph(tour.EndLocation));

                double averageTime = calcValues.CalculateAverageTime(logs);

                Cell content4 = new Cell(1, 1)
                        .Add(new Paragraph(double.IsNaN(averageTime) ? "---" : averageTime.ToString()));

                Cell content5 = new Cell(1, 1)
                        .Add(new Paragraph(tour.Distance.ToString()));

                double averageRating = calcValues.CalculateAverageRating(logs);

                Cell content6 = new Cell(1, 1)
                        .Add(new Paragraph(double.IsNaN(averageRating) ? "---" : averageRating.ToString()));

                tableTourLogs.AddCell(content1);
                tableTourLogs.AddCell(content2);
                tableTourLogs.AddCell(content3);
                tableTourLogs.AddCell(content4);
                tableTourLogs.AddCell(content5);
                tableTourLogs.AddCell(content6);
            }
            // add generated table to document
            tourDocument.Add(tableTourLogs);
            

            tourDocument.Close();

            return true;
        }

        private ObservableCollection<TourLog> FillTourLogList(int tourId)
        {
            ObservableCollection<TourLog> tourLogs = new ObservableCollection<TourLog>();
            foreach (TourLog tourLog in TourManagerFactory.GetTourLogFactoryManager(Logger).GetItems(tourId))
            {
                tourLogs.Add(tourLog);
            }

            return tourLogs;
        }
    }
}
