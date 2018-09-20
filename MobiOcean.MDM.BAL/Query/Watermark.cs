using iTextSharp.text;
using iTextSharp.text.pdf;
using MobiOcean.MDM.BAL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Watermark
/// </summary>
namespace MobiOcean.MDM.BAL.Query
{
    public class Watermark : IPdfPageEvent
    {
        string watermarkText = string.Empty;

        public Watermark(string watermark)
        {
            watermarkText = watermark;
        }

        public void OnOpenDocument(PdfWriter writer, Document document) { }
        public void OnCloseDocument(PdfWriter writer, Document document) { }
        public void OnStartPage(PdfWriter writer, Document document)
        {
            try
            {
                PdfContentByte cb = writer.DirectContentUnder;
                Image watermark = Image.GetInstance(Constant.MobiURL + "images/logo-beta.png");
                watermark.ScaleToFit(1700, 800);
                watermark.Alignment = iTextSharp.text.Image.UNDERLYING;
                watermark.SetAbsolutePosition(50, 250);
                watermark.ScaleAbsoluteHeight(170);
                watermark.ScaleAbsoluteWidth(500);
                watermark.RotationDegrees = 45;
                cb.SaveState();
                PdfGState state = new PdfGState();
                state.FillOpacity = 0.3f;
                cb.SetGState(state);
                cb.AddImage(watermark);
                cb.RestoreState();                
            }
            catch (Exception)
            {               
            }
        }
        public void OnEndPage(PdfWriter writer, Document document) { }
        public void OnParagraph(PdfWriter writer, Document document, float paragraphPosition) { }
        public void OnParagraphEnd(PdfWriter writer, Document document, float paragraphPosition) { }
        public void OnChapter(PdfWriter writer, Document document, float paragraphPosition, Paragraph title) { }
        public void OnChapterEnd(PdfWriter writer, Document document, float paragraphPosition) { }
        public void OnSection(PdfWriter writer, Document document, float paragraphPosition, int depth, Paragraph title) { }
        public void OnSectionEnd(PdfWriter writer, Document document, float paragraphPosition) { }
        public void OnGenericTag(PdfWriter writer, Document document, Rectangle rect, String text) { }

    }
}