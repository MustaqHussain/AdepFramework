using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

using Dwp.Adep.Framework.Resources.DataContracts;

using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocToPDFConverter;
using Syncfusion.Pdf;

namespace Dwp.Adep.Framework.Resources.ServiceContracts.DocIo
{
    public class DocIoHelper
    {
        #region Private Fields
        private  IWordDocument _documentTemplate;
        #endregion

        #region Constants
        const string PdfExtension = "PDF";
        const string DocExtension = "DOC";
        #endregion

        #region constructor

        /// <summary>
        /// constructors
        /// </summary>
        public DocIoHelper()
        {
            //this.documentTemplate = documentTemplate;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// genearate document 
        /// </summary>
        /// <param name="fileByte"></param>
        /// <param name="documentContent"></param>
        /// <returns></returns>
        public byte[] GenerateDocument(byte[] fileByte, List<DocContent> documentContent,string outputFileType)
        {
            #region logging
            //System.IO.File.AppendAllText(@"C:\ErrorLog.txt", "\r\n Date : " + DateTime.Now.ToString());
            //System.IO.File.AppendAllText(@"C:\ErrorLog.txt", "\r\n Input FileBytes : " +  fileByte.Length.ToString());
            //System.IO.File.AppendAllText(@"C:\ErrorLog.txt", "\r\n Memory created : " + fileMemoryStream.Length.ToString());
            //System.IO.File.AppendAllText(@"C:\ErrorLog.txt", "\r\n Add parameters and value" + documentTemplate.Sections.Count.ToString());
            //System.IO.File.AppendAllText(@"C:\ErrorLog.txt", "\r\n Saving docio : " + documentTemplate.Document.Count.ToString());
            ///System.IO.File.AppendAllText(@"C:\ErrorLog.txt", "\r\n Save success memory stream length : " + docIoMemoryStream.Length.ToString());
            //System.IO.File.AppendAllText(@"C:\ErrorLog.txt", "\r\n Save success pdf memory stream length : " + docIoPdfMemoryStream.Length.ToString());
            #endregion

            byte[] docIoFileByte = null;
            if (fileByte != null)
            {
                MemoryStream fileMemoryStream = new MemoryStream();
                fileMemoryStream.Write(fileByte, 0, fileByte.Length);
                _documentTemplate = new WordDocument(fileMemoryStream);

                //read the text parameters values and placeholders to be replacedin document
                foreach (DocContent itemDocContent in documentContent)
                {
                    _documentTemplate.Replace(itemDocContent.ContentKey, itemDocContent.ContentText, true, true);
                }
                MemoryStream docIoMemoryStream = null;
                if (outputFileType.ToUpper() == PdfExtension.ToUpper())
                {
                    docIoMemoryStream = GetPDFStream();
                }
                else if (outputFileType.ToUpper() == DocExtension.ToUpper())
                {
                    docIoMemoryStream = GetWordStream();
                }
                docIoMemoryStream.Seek(0, SeekOrigin.Begin);
                docIoFileByte = docIoMemoryStream.ToArray();
                docIoMemoryStream.Close();
            }
            return docIoFileByte;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Get pdf stream for creating pdf dcoument
        /// </summary>
        /// <returns></returns>
        private MemoryStream GetPDFStream()
        {
            MemoryStream docIoWordMemoryStream = new MemoryStream();
            MemoryStream docIoPdfMemoryStream = new MemoryStream();

            _documentTemplate.Save(docIoWordMemoryStream, FormatType.Doc);
            DocToPDFConverter converter = new DocToPDFConverter();
            Syncfusion.Pdf.PdfDocument pdfdoc = converter.ConvertToPDF(docIoWordMemoryStream);
            pdfdoc.Save(docIoPdfMemoryStream);
            return docIoPdfMemoryStream;
        }

        /// <summary>
        /// Get Word Stream for creating word dcoument
        /// </summary>
        /// <returns></returns>
        private MemoryStream GetWordStream()
        {
            MemoryStream docIoWordMemoryStream = new MemoryStream();
            _documentTemplate.Save(docIoWordMemoryStream, FormatType.Doc);
            return docIoWordMemoryStream;
        }

        #endregion

    }
}