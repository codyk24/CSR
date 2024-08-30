using System.IO;
using System;
//using DinkToPdf;
using SAS;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using UnityEngine;
using TheArtOfDev.HtmlRenderer.Core;
using UnityEngine.EventSystems;
using SAS.UI;
using PdfSharp;
using PdfSharp.Pdf;
using System.Net.Mail;
using System.Net;
using UnityEngine.XR;

public class PDFEventArgs : EventArgs
{
    #region Properties

    public string path { get; protected set; }

    #endregion

    #region Constructors

    public PDFEventArgs(string filePath)
    {
        path = filePath;
    }

    #endregion
}

public delegate void PDFEventArgsHandler(object sender, PDFEventArgs e);

public class ConvertInputsToPDF : BaseMonoSingleton<ConvertInputsToPDF>
{
    [SerializeField]
    TMPro.TMP_InputField m_inspectorName;

    [SerializeField]
    TMPro.TMP_InputField m_dateOfInspection;

    [SerializeField]
    TMPro.TMP_InputField m_refrigerationUnitID;

    [SerializeField]
    TMPro.TMP_InputField m_location;

    [SerializeField]
    TMPro.TMP_InputField m_ambientTemperature;

    [SerializeField]
    TMPro.TMP_InputField m_unitTemperature;

    [SerializeField]
    TMPro.TMP_InputField m_additionalComments;

    private Texture2D logoTexture;

    [SerializeField]
    string DebugSavePath;

    public event PDFEventArgsHandler PdfBuildStarted;
    public event PDFEventArgsHandler PdfBuildFinished;

    private void Start()
    {
        
    }

    public void ConvertHtmlToPdf()
    {
        TextAsset htmlDoc = Resources.Load<TextAsset>("HTML/RefrigerationInspection");

        string htmlString = htmlDoc.text;

        // Replace the placeholders in the html string with the form values
        htmlString = htmlString.Replace("REPLACE_InspectorName", m_inspectorName.text);
        htmlString = htmlString.Replace("REPLACE_date", m_dateOfInspection.text);
        htmlString = htmlString.Replace("REPLACE_RefrigerationUnitID", m_refrigerationUnitID.text);
        htmlString = htmlString.Replace("REPLACE_Location", m_location.text);
        htmlString = htmlString.Replace("REPLACE_AmbientTemp", m_ambientTemperature.text);
        htmlString = htmlString.Replace("REPLACE_UnitTemp", m_unitTemperature.text);
        htmlString = htmlString.Replace("REPLACE_AdditionalComments", m_additionalComments.text);

        Debug.LogFormat("m_inspectorName.text: {0}", m_inspectorName.text);
        Debug.LogFormat("m_dateOfInspection.text: {0}", m_dateOfInspection.text);
        Debug.LogFormat("m_refrigerationUnitID.text: {0}", m_refrigerationUnitID.text);
        Debug.LogFormat("m_location.text: {0}", m_location.text);
        Debug.LogFormat("m_ambientTemperature.text: {0}", m_ambientTemperature.text);
        Debug.LogFormat("m_unitTemperature.text: {0}", m_unitTemperature.text);
        Debug.LogFormat("m_additionalComments.text: {0}", m_additionalComments.text);

        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential("swissarmysoftware@gmail.com", "onqy zcmx lcfo qwey"),
            EnableSsl = true
        };

        MailAddress addressFrom = new MailAddress("swissarmysoftware@gmail.com", "Swiss Army Software");
        MailAddress addressTo = new MailAddress("codykairis24@gmail.com", "Cody Kairis");

        MailMessage message = new MailMessage(addressFrom, addressTo);
        message.Subject = "Test Subject";
        message.IsBodyHtml = true;

        message.Body = htmlString;
        client.Send(message);

        // Convert HTML string to PDF using DinkToPDF
        //var converter = new BasicConverter(new PdfTools());

        //var doc = new HtmlToPdfDocument()
        //{
        //    GlobalSettings = {
        //                 ColorMode = ColorMode.Color,
        //                Orientation = Orientation.Portrait,
        //                PaperSize = PaperKind.A4
        //                },
        //    Objects = {
        //                new DinkToPdf.ObjectSettings() {
        //                PagesCount = true,
        //                HtmlContent = htmlString,
        //                WebSettings = { DefaultEncoding = "utf-8" },
        //                HeaderSettings = {
        //                   FontSize = 14
        //                 }
        //            }
        //        }
        //};

        //byte[] pdf = converter.Convert(doc);
        //File.WriteAllBytes(Path.Combine(DebugSavePath, "testPDF.pdf"), pdf);

        //string tempCachePDFPath = Path.Combine(Application.temporaryCachePath, "testPDF.pdf");
        ////File.WriteAllBytes(tempCachePDFPath, pdf);

        //// Use PDFSharp to generate a PDF
        //PdfDocument pdf = PdfGenerator.GeneratePdf(htmlString, PageSize.Letter);
        //pdf.Save(Path.Combine(DebugSavePath, "testDocument.pdf"));
        ////pdf.Save(Path.Combine(tempCachePDFPath));

        //Debug.LogFormat("DEBUG... PDF build finished...");

        //DialogCanvas.Show("PDF Generated",
        //    "A PDF has been generated with the content of this form.",
        //    Accent.Informational,
        //    "Share",
        //    "Cancel",
        //    () => { PdfBuildFinished.Invoke(this, new PDFEventArgs(tempCachePDFPath)); },
        //    null,
        //    false);

        // Create a new PDF document
        //PdfDocument document = new PdfDocument();
        //document.Info.Title = "Complex PDF Example";

        //// Create an empty page
        //PdfPage page = document.AddPage();
        //XGraphics gfx = XGraphics.FromPdfPage(page);

        //// Set up fonts
        //XFont headerFont = new XFont("Verdana", 20, XFontStyle.Bold);
        //XFont subHeaderFont = new XFont("Verdana", 14, XFontStyle.Bold);
        //XFont bodyFont = new XFont("Verdana", 12, XFontStyle.Regular);

        //// Set up layout margins and positions
        //double margin = 40;
        //double yPosition = margin;

        //// Draw a logo at the top-left corner
        //if (logoTexture != null)
        //{
        //    MemoryStream stream = new MemoryStream(logoTexture.EncodeToPNG());
        //    XImage logo = XImage.FromStream(stream);

        //    gfx.DrawImage(logo, margin, yPosition, 100, 50);
        //    yPosition += 60; // Move the y position down after placing the logo
        //}

        //// Draw the document title centered at the top
        //gfx.DrawString("Complex PDF Report", headerFont, XBrushes.Black,
        //               new XRect(0, yPosition, page.Width, 50), XStringFormats.TopCenter);
        //yPosition += 50;

        //// Draw a horizontal line under the header
        //gfx.DrawLine(XPens.Black, margin, yPosition, page.Width - margin, yPosition);
        //yPosition += 20;

        //// Add a sub-header
        //gfx.DrawString("User Data", subHeaderFont, XBrushes.Black, new XPoint(margin, yPosition));
        //yPosition += 30;

        //// Draw some text
        //gfx.DrawString("This is an example of a more complex PDF layout.", bodyFont, XBrushes.Black,
        //               new XPoint(margin, yPosition));
        //yPosition += 30;

        //// Draw a table-like structure for data
        //double tableTop = yPosition;
        //double tableLeft = margin;
        //double columnWidth = (page.Width - 2 * margin) / 3;

        //// Define table header
        //gfx.DrawRectangle(XPens.Black, XBrushes.LightGray, tableLeft, tableTop, columnWidth, 20);
        //gfx.DrawRectangle(XPens.Black, XBrushes.LightGray, tableLeft + columnWidth, tableTop, columnWidth, 20);
        //gfx.DrawRectangle(XPens.Black, XBrushes.LightGray, tableLeft + 2 * columnWidth, tableTop, columnWidth, 20);

        //gfx.DrawString("Field", bodyFont, XBrushes.Black, new XPoint(tableLeft + 5, tableTop + 5));
        //gfx.DrawString("Value", bodyFont, XBrushes.Black, new XPoint(tableLeft + columnWidth + 5, tableTop + 5));
        //gfx.DrawString("Details", bodyFont, XBrushes.Black, new XPoint(tableLeft + 2 * columnWidth + 5, tableTop + 5));

        //// Define table rows
        //tableTop += 20;
        //for (int i = 0; i < 5; i++)
        //{
        //    gfx.DrawRectangle(XPens.Black, tableLeft, tableTop, columnWidth, 20);
        //    gfx.DrawRectangle(XPens.Black, tableLeft + columnWidth, tableTop, columnWidth, 20);
        //    gfx.DrawRectangle(XPens.Black, tableLeft + 2 * columnWidth, tableTop, columnWidth, 20);

        //    gfx.DrawString($"Field {i + 1}", bodyFont, XBrushes.Black, new XPoint(tableLeft + 5, tableTop + 5));
        //    gfx.DrawString($"Value {i + 1}", bodyFont, XBrushes.Black, new XPoint(tableLeft + columnWidth + 5, tableTop + 5));
        //    gfx.DrawString($"Details {i + 1}", bodyFont, XBrushes.Black, new XPoint(tableLeft + 2 * columnWidth + 5, tableTop + 5));

        //    tableTop += 20;
        //}

        //// Draw another horizontal line to separate sections
        //gfx.DrawLine(XPens.Black, margin, tableTop + 10, page.Width - margin, tableTop + 10);
        //tableTop += 30;

        //// Add another section with bullet points
        //gfx.DrawString("Key Points:", subHeaderFont, XBrushes.Black, new XPoint(margin, tableTop));
        //tableTop += 20;

        //string[] bulletPoints = { "Point 1: Explanation.", "Point 2: More details.", "Point 3: Final thoughts." };
        //foreach (string point in bulletPoints)
        //{
        //    gfx.DrawString("• " + point, bodyFont, XBrushes.Black, new XPoint(margin + 20, tableTop));
        //    tableTop += 20;
        //}

        //// Save the document
        //document.Save(filePath);
        //Debug.Log("Complex PDF saved at: " + filePath);

        //// Optionally, dispose of the document when done
        //document.Close();

    }
}
