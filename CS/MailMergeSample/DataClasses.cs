using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

public class Invoice {
    private int id;

    public int Id {
        get { return id; }
        set { id = value; }
    }
    private string description;

    public string Description {
        get { return description; }
        set { description = value; }
    }
    private decimal price;

    public decimal Price {
        get { return price; }
        set { price = value; }
    }

    public Invoice(int id, string description, decimal price) {
        this.Id = id;
        this.Description = description;
        this.Price = price;
    }
}

public class ManualDataSet : DataSet {
    public ManualDataSet()
        : base() {
        DataTable table = new DataTable("table");

        DataSetName = "ManualDataSet";

        table.Columns.Add("ID", typeof(Int32));
        table.Columns.Add("MyDateTime", typeof(DateTime));
        table.Columns.Add("MyRow", typeof(string));
        table.Columns.Add("MyData", typeof(double));
        table.Constraints.Add("IDPK", table.Columns["ID"], true);

        Tables.AddRange(new DataTable[] { table });
    }

    public static ManualDataSet CreateData() {
        ManualDataSet ds = new ManualDataSet();
        DataTable table = ds.Tables["table"];

        table.Rows.Add(new object[] { 0, DateTime.Today, "A", 103 });
        table.Rows.Add(new object[] { 1, DateTime.Today, "B", 200 });
        table.Rows.Add(new object[] { 2, DateTime.Today, "C", 446 });

        return ds;
    }
}
