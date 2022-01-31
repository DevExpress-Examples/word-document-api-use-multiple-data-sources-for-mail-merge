Imports System.Data

Public Class Invoice

    Private idField As Integer

    Public Property Id As Integer
        Get
            Return idField
        End Get

        Set(ByVal value As Integer)
            idField = value
        End Set
    End Property

    Private descriptionField As String

    Public Property Description As String
        Get
            Return descriptionField
        End Get

        Set(ByVal value As String)
            descriptionField = value
        End Set
    End Property

    Private priceField As Decimal

    Public Property Price As Decimal
        Get
            Return priceField
        End Get

        Set(ByVal value As Decimal)
            priceField = value
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
        Dim table As DataTable = New DataTable("table")
        DataSetName = "ManualDataSet"
        table.Columns.Add("ID", GetType(Integer))
        table.Columns.Add("MyDateTime", GetType(Date))
        table.Columns.Add("MyRow", GetType(String))
        table.Columns.Add("MyData", GetType(Double))
        table.Constraints.Add("IDPK", table.Columns("ID"), True)
        Tables.AddRange(New DataTable() {table})
    End Sub

    Public Shared Function CreateData() As ManualDataSet
        Dim ds As ManualDataSet = New ManualDataSet()
        Dim table As DataTable = ds.Tables("table")
        table.Rows.Add(New Object() {0, Date.Today, "A", 103})
        table.Rows.Add(New Object() {1, Date.Today, "B", 200})
        table.Rows.Add(New Object() {2, Date.Today, "C", 446})
        Return ds
    End Function
End Class
