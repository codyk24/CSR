using System.IO;
using System.Collections.Generic;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using UnityEngine;
using PdfSharp;
using PdfSharp.Pdf;

public class ConvertInputsToPDF : MonoBehaviour
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

    private void Start()
    {
        
    }

    public void ConvertHtmlToPdf()
    {
        TextAsset htmlDoc = Resources.Load<TextAsset>("HTML/RefrigerationInspection");

        string htmlString = htmlDoc.text;
        // Replace the placeholders in the html string with the form values

        //value="REPLACE_InspectorName"
        htmlString.Replace("REPLACE_InspectorName", m_inspectorName.text);

        //value="REPLACE_date"
        htmlString.Replace("REPLACE_date", m_dateOfInspection.text);

        //value="REPLACE_RefrigerationUnitID"
        htmlString.Replace("REPLACE_RefrigerationUnitID", m_refrigerationUnitID.text);

        //value="REPLACE_Location"
        htmlString.Replace("REPLACE_Location", m_location.text);

        //value="REPLACE_AmbientTemp"
        htmlString.Replace("REPLACE_AmbientTemp", m_ambientTemperature.text);

        //value="REPLACE_UnitTemp"
        htmlString.Replace("REPLACE_UnitTemp", m_unitTemperature.text);

        //value="REPLACE_AdditionalComments"
        htmlString.Replace("REPLACE_AdditionalComments", m_additionalComments.text);

        // Use PDFSharp to generate a PDF
        PdfDocument pdf = PdfGenerator.GeneratePdf(htmlString, PageSize.Letter);
        pdf.Save(Path.Combine(DebugSavePath, "testDocument.pdf"));

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
