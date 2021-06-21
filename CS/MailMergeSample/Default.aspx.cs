using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.UI;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;

public partial class _Default : System.Web.UI.Page {
    RichEditDocumentServer documentServer = null;

    protected void Page_Load(object sender, EventArgs e) {
        documentServer = new RichEditDocumentServer();
        
        if (Request.QueryString.Count > 0 && Request.QueryString[0].StartsWith("preview")) {
            RefreshPreview(Request.QueryString[0]);
        }
    }

    private void RefreshPreview(string previewName) {
        Response.StatusCode = (int)HttpStatusCode.OK;
        Response.ContentType = "text/html";

        Stream outputStream = ExecuteMerge(previewName, DocumentFormat.Html);

        outputStream.Seek(0, SeekOrigin.Begin);
        StreamCopyHelper.Copy(outputStream, Response.OutputStream);

        Response.End();
    }

    private Stream ExecuteMerge(string templateName, DocumentFormat documentFormat) {
        Stream result = new MemoryStream();
        MailMergeOptions mailMergeOptions = documentServer.CreateMailMergeOptions();
        
        if (templateName == "preview1") {
            documentServer.LoadDocument(Page.MapPath("~/App_Data/InvoicesDetail.rtf"));

            List<Invoice> invoices = new List<Invoice>(10);

            invoices.Add(new Invoice(0, "Invoice1", 10.0m));
            invoices.Add(new Invoice(1, "Invoice2", 15.0m));
            invoices.Add(new Invoice(2, "Invoice3", 20.0m));

            mailMergeOptions.DataSource = invoices;
        }
        else if (templateName == "preview2") {
            documentServer.LoadDocument(Page.MapPath("~/App_Data/SamplesDetail.rtf"));

            mailMergeOptions.DataSource = ManualDataSet.CreateData().Tables[0];
        }
        else if (templateName == "all") {
            Stream part1 = ExecuteMerge("preview1", documentFormat);
            Stream part2 = ExecuteMerge("preview2", documentFormat);

            part1.Seek(0, SeekOrigin.Begin);
            part2.Seek(0, SeekOrigin.Begin);

            documentServer.LoadDocument(part1, documentFormat);
            documentServer.Document.AppendDocumentContent(part2, documentFormat);

            documentServer.SaveDocument(result, documentFormat);

            return result;
        }

        documentServer.Options.MailMerge.ViewMergedData = true;
        documentServer.Options.Export.Html.EmbedImages = true;
        mailMergeOptions.MergeMode = MergeMode.JoinTables;
        documentServer.MailMerge(mailMergeOptions, result, documentFormat);

        return result;
    }

    protected void btnDownload_Click(object sender, EventArgs e) {
        Stream outputStream = ExecuteMerge("all", DocumentFormat.Rtf);

        outputStream.Seek(0, SeekOrigin.Begin);
        StreamCopyHelper.Copy(outputStream, Response.OutputStream);

        Response.StatusCode = (int)HttpStatusCode.OK;
        Response.ContentType = "application/rtf";
        Response.AddHeader("Content-Disposition", "attachment; filename=RichEditMailMerge.rtf");
        Response.End();
    }
}