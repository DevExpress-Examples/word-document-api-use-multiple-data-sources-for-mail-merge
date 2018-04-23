Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Data

Public Class Invoice
	Private id_Renamed As Integer

	Public Property Id() As Integer
		Get
			Return id_Renamed
		End Get
		Set(ByVal value As Integer)
			id_Renamed = value
		End Set
	End Property
	Private description_Renamed As String

	Public Property Description() As String
		Get
			Return description_Renamed
		End Get
		Set(ByVal value As String)
			description_Renamed = value
		End Set
	End Property
	Private price_Renamed As Decimal

	Public Property Price() As Decimal
		Get
			Return price_Renamed
		End Get
		Set(ByVal value As Decimal)
			price_Renamed = value
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
