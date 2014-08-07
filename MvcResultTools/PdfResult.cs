using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MvcResultTools
{
    /// <summary>
    /// Pdf result
    /// </summary>
    public class PdfResult : ActionResult
    {
        public string SourceFilename { get; set; }
        public MemoryStream SourceStream { get; set; }
        public string ContentType { get; set; }
        public PdfResult(string sourceFilename)
        {
            SourceFilename = sourceFilename;
            ContentType = FileTypeHelper.GetContentType(SourceFilename);

            if (ContentType != "application/pdf")
            {
                throw new Exception("This is not a pdf file.");
            }
        }

        public PdfResult(MemoryStream sourceStream, string contentType)
        {
            SourceStream = sourceStream;
            ContentType = contentType;
        }

        public byte[] _ss { get; set; }
        public PdfResult(byte[] sourceStream)
        {
            _ss = sourceStream;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var res = context.HttpContext.Response;
            res.Clear();
            res.Cache.SetCacheability(HttpCacheability.NoCache);
            res.ContentType = "application/pdf";

            if (_ss != null)
            {
                res.AddHeader("Content-Type", "application/pdf");
                res.AddHeader("Content-Disposition",
                        string.Format("attachment; filename={0}; size={1}",
                        "generated.PDF", _ss.Length));


                res.BinaryWrite(_ss);
                res.End();
            }
            else if (SourceStream != null)
            {
                SourceStream.WriteTo(res.OutputStream);
            }
            else
            {
                res.TransmitFile(SourceFilename);
            }
        }
    }
}
