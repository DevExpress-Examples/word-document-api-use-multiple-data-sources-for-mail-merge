Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Data

Public Class Invoice
'INSTANT VB NOTE: The field id was renamed since Visual Basic does not allow fields to have the same name as other class members:
	Private id_Conflict As Integer

	Public Property Id() As Integer
		Get
			Return id_Conflict
		End Get
		Set(ByVal value As Integer)
			id_Conflict = value
		End Set
	End Property
'INSTANT VB NOTE: The field description was renamed since Visual Basic does not allow fields to have the same name as other class members:
	Private description_Conflict As String

	Public Property Description() As String
		Get
			Return description_Conflict
		End Get
		Set(ByVal value As String)
			description_Conflict = value
		End Set
	End Property
'INSTANT VB NOTE: The field price was renamed since Visual Basic does not allow fields to have the same name as other class members:
	Private price_Conflict As Decimal

	Public Property Price() As Decimal
		Get
			Return price_Conflict
		End Get
		Set(ByVal value As Decimal)
			price_Conflict = value
		End Set
	End Property

	Public Sub New(ByVal id As Integer, ByVal description As String, ByVal price As Decimal)
		Me.Id = id
		Me.Description = description
		Me.Price = price
	End Sub
End Class

Public Class ManualDataSet
	Inherits DataSet

	Public Sub New()
		MyBase.New()
		Dim table As New DataTable("table")

		DataSetName = "ManualDataSet"

		table.Columns.Add("ID", GetType(Int32))
		table.Columns.Add("MyDateTime", GetType(DateTime))
		table.Columns.Add("MyRow", GetType(String))
		table.Columns.Add("MyData", GetType(Double))
		table.Constraints.Add("IDPK", table.Columns("ID"), True)

		Tables.AddRange(New DataTable() { table })
	End Sub

	Public Shared Function CreateData() As ManualDataSet
		Dim ds As New ManualDataSet()
		Dim table As DataTable = ds.Tables("table")

		table.Rows.Add(New Object() { 0, DateTime.Today, "A", 103 })
		table.Rows.Add(New Object() { 1, DateTime.Today, "B", 200 })
		table.Rows.Add(New Object() { 2, DateTime.Today, "C", 446 })

		Return ds
	End Function
End Class
