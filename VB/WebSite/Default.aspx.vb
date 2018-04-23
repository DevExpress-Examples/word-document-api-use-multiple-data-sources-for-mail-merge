Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Net
Imports System.Web.UI
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraRichEdit.API.Native

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Private documentServer As RichEditDocumentServer = Nothing

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		documentServer = New RichEditDocumentServer()

		If Request.QueryString.Count > 0 AndAlso Request.QueryString(0).StartsWith("preview") Then
			RefreshPreview(Request.QueryString(0))
		End If
	End Sub

	Private Sub RefreshPreview(ByVal previewName As String)
		Response.StatusCode = CInt(Fix(HttpStatusCode.OK))
		Response.ContentType = "text/html"

		Dim outputStream As Stream = ExecuteMerge(previewName, DocumentFormat.Html)

		outputStream.Seek(0, SeekOrigin.Begin)
		StreamCopyHelper.Copy(outputStream, Response.OutputStream)

		Response.End()
	End Sub

	Private Function ExecuteMerge(ByVal templateName As String, ByVal documentFormat As DocumentFormat) As Stream
		Dim result As Stream = New MemoryStream()
		Dim mailMergeOptions As MailMergeOptions = documentServer.CreateMailMergeOptions()

		If templateName = "preview1" Then
			documentServer.LoadDocument(Page.MapPath("~/App_Data/InvoicesDetail.rtf"))

			Dim invoices As New List(Of Invoice)(10)

			invoices.Add(New Invoice(0, "Invoice1", 10.0D))
			invoices.Add(New Invoice(1, "Invoice2", 15.0D))
			invoices.Add(New Invoice(2, "Invoice3", 20.0D))

			mailMergeOptions.DataSource = invoices
		ElseIf templateName = "preview2" Then
			documentServer.LoadDocument(Page.MapPath("~/App_Data/SamplesDetail.rtf"))

			mailMergeOptions.DataSource = ManualDataSet.CreateData().Tables(0)
		ElseIf templateName = "all" Then
			Dim part1 As Stream = ExecuteMerge("preview1", documentFormat)
			Dim part2 As Stream = ExecuteMerge("preview2", documentFormat)

			part1.Seek(0, SeekOrigin.Begin)
			part2.Seek(0, SeekOrigin.Begin)

			documentServer.LoadDocument(part1, documentFormat)
			documentServer.Document.AppendDocumentContent(part2, documentFormat)

			documentServer.SaveDocument(result, documentFormat)

			Return result
		End If

		documentServer.Options.MailMerge.ViewMergedData = True
		documentServer.Options.Export.Html.EmbedImages = True
		mailMergeOptions.MergeMode = MergeMode.JoinTables
		documentServer.MailMerge(mailMergeOptions, result, documentFormat)

		Return result
	End Function

	Protected Sub btnDownload_Click(ByVal sender As Object, ByVal e As EventArgs)
		Dim outputStream As Stream = ExecuteMerge("all", DocumentFormat.Rtf)

		outputStream.Seek(0, SeekOrigin.Begin)
		StreamCopyHelper.Copy(outputStream, Response.OutputStream)

		Response.StatusCode = CInt(Fix(HttpStatusCode.OK))
		Response.ContentType = "application/rtf"
		Response.AddHeader("Content-Disposition", "attachment; filename=RichEditMailMerge.rtf")
		Response.End()
	End Sub
End Class