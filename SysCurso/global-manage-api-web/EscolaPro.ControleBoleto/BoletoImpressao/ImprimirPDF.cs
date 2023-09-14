using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EscolaPro.ControleBoleto.BoletoImpressao
{
    public class ImprimirPDF
    {
        public static void ConvertHTMLtoPDF(string htmlFullPath)
        {
            try
            {
                //StringBuilder sbHtmlText = new StringBuilder();
                //sbHtmlText.Append("Employee Info");
                //sbHtmlText.Append("Hi This is Employee Info");

                Document document = new Document();
                PdfWriter.GetInstance(document, new FileStream("c:\\shared\\MySamplePDF.pdf", FileMode.Create));
                document.Open();
                iTextSharp.text.html.simpleparser.HTMLWorker hw =
                new iTextSharp.text.html.simpleparser.HTMLWorker(document);
                hw.Parse(new StringReader(htmlFullPath.ToString()));
                document.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
