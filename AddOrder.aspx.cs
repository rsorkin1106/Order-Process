using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace infragistics
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        TestEntities db;
        DataTable dt;
        OdbcDataAdapter adapt;
        OdbcConnection con;
        string connectionstring = "Driver={SQL Server}; Server=ROBS-LAPTOP\\SQLEXPRESS; UID=BmSys; PWD=Robert99; Database=Test";
        //To keep track of delete buttons
        protected void Page_Load(object sender, EventArgs e)
        {
            con = new OdbcConnection(connectionstring);
            con.Open();
            db = new TestEntities();


            //Gets next identity number by seeing how many orders are in the system
            using (OdbcCommand command = new OdbcCommand("SELECT COUNT(*) FROM dbo.InfraOrders", con))
            {
                using (OdbcDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    string temp = reader.GetString(0);
                    //If there are 0 rows in the table, the first number is 1
                    lblOrderNum.Text = (Int32.Parse(temp) + 1).ToString();
                }
            }


            //Creates temp table to store order lines before order is placed
            if (!IsPostBack)
            {
                try
                {
                    using (OdbcCommand command = new OdbcCommand("CREATE TABLE ##temp([Line #] int IDENTITY(1,1)," +
                        "[F Code] varchar(10), Description varchar(MAX), [Quantity Per] int, [# of Cont.] int, [Cont. Type] varchar(50)," +
                        "UOM varchar(10), Priority int, [Total RQ] int)", con))
                    {

                        command.ExecuteNonQuery();
                    }
                }
                catch
                {
                    using (OdbcCommand command = new OdbcCommand("DELETE dbo.##temp; DBCC CHECKIDENT('##temp', RESEED, 0)", con))
                    {

                        command.ExecuteNonQuery();
                    }
                }
            }
            //Must recreate buttons if it is a post back
            else
            {
                PlaceHolder1.Controls.Clear();
                double btnHeight = gv1.RowStyle.Height.Value + 27;
                Unit height = new Unit(btnHeight);
                for (int i = 0; i < gv1.Rows.Count; ++i)
                {
                    Button btn = new Button();
                    btn.Text = "Delete";
                    btn.Height = gv1.RowStyle.Height;
                    btn.ID = i.ToString();
                    btn.Click += new EventHandler(btnDelete);
                    PlaceHolder1.Controls.Add(btn);
                    btn.Visible = true;
                    Literal lit = new Literal();
                    lit.Text = "<br/>";
                    PlaceHolder1.Controls.Add(lit);
                }
            }

        }


        private void btnDelete(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            int idx = Int32.Parse(b.ID);
            using (OdbcCommand command = new OdbcCommand("DELETE FROM dbo.##temp WHERE [Line #] = ?", con))
            {
                var row = gv1.Rows[idx];
                command.Parameters.AddWithValue("@id", row.Cells[0].Text);
                command.ExecuteNonQuery();
            }


            ShowData();
            PlaceHolder1.Controls.Clear();
            double btnHeight = gv1.RowStyle.Height.Value + 27;
            Unit height = new Unit(btnHeight);
            for (int i = 0; i < gv1.Rows.Count; ++i)
            {
                Button btn = new Button();
                btn.Text = "Delete";
                btn.Height = gv1.RowStyle.Height;
                btn.ID = i.ToString();
                btn.Click += new EventHandler(btnDelete);
                PlaceHolder1.Controls.Add(btn);
                btn.Visible = true;
                Literal lit = new Literal();
                lit.Text = "<br/>";
                PlaceHolder1.Controls.Add(lit);
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ShowData()
        {
            dt = new DataTable();
            adapt = new OdbcDataAdapter("SELECT [Line #] as ID, [F Code] AS F_Code, [Description]," +
                "[Quantity Per] AS Quantity_Per, [# of Cont.] AS #_of_Cont, [Cont. Type] AS Cont_Type," +
                "[Total RQ] AS Total_RQ FROM dbo.##temp ORDER BY [Line #]", con);

            adapt.Fill(dt);
            gv1.DataSource = dt;
            gv1.DataBind();
        }


        protected void gv1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (OdbcCommand command = new OdbcCommand("INSERT INTO dbo.##temp([F Code], Description, [Quantity Per]," +
                    "[# of Cont.], [Cont. Type], UOM, Priority, [Total RQ]) VALUES (?, ?, ?, ?, ?, ?, ?, ?)", con))
                {
                    command.Parameters.AddWithValue("@code", tbFCode.Text);
                    command.Parameters.AddWithValue("@desc", "");
                    command.Parameters.AddWithValue("@qtyPer", tbQtyPer.Text);
                    command.Parameters.AddWithValue("@numCont", ddlNum.SelectedValue);
                    command.Parameters.AddWithValue("@contType", ddlContType.SelectedValue);
                    command.Parameters.AddWithValue("@uom", ddlUOM.SelectedValue);
                    command.Parameters.AddWithValue("@prio", ddlPrio.SelectedValue);
                    command.Parameters.AddWithValue("@totRQ", (Int32.Parse(tbQtyPer.Text) * Int32.Parse(ddlNum.SelectedValue)));

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception exc)
            {
                string script = "alert(\"Please make sure all Next Line fields are filled in\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                      "ServerControlScript", script, true);
            }

            tbFCode.Text = "";
            tbQtyPer.Text = "";
            ddlNum.SelectedIndex = -1;
            ddlContType.SelectedIndex = -1;
            ddlUOM.SelectedIndex = -1;
            ddlPrio.SelectedIndex = -1;
            ShowData();
            PlaceHolder1.Controls.Clear();
            double btnHeight = gv1.RowStyle.Height.Value + 27;
            Unit height = new Unit(btnHeight);
            for (int i = 0; i < gv1.Rows.Count; ++i)
            {
                Button btn = new Button();
                btn.Text = "Delete";
                btn.Height = gv1.RowStyle.Height;
                btn.ID = i.ToString();
                btn.Click += new EventHandler(btnDelete);
                PlaceHolder1.Controls.Add(btn);
                btn.Visible = true;
                Literal lit = new Literal();
                lit.Text = "<br/>";
                PlaceHolder1.Controls.Add(lit);
            }
        }

        protected void btnSubmit_Click1(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(tbOp.Text))
            {
                string script = "alert(\"Please enter a customer name\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                      "ServerControlScript", script, true);
                return;
            }

            //Gets customer ID from Operator name
            var customers = db.Customers.ToList();
            var idList = from customer in customers
                     where customer.Name == tbOp.Text
                     select customer.ID;

            List<int> temp = idList.ToList();


            //If customer doesnt exist
            int id;
            try
            {
                id = temp[0];
            }

            catch
            {
                string script = "alert(\"Please enter an existing customer name\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                      "ServerControlScript", script, true);
                return;
            }

            //Adds new order to dbo.InfraOrders
            using(OdbcCommand command = new OdbcCommand("INSERT INTO dbo.InfraOrders([Customer ID], [Update Date]) VALUES (?, ?)", con))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@time", DateTime.Now);

                command.ExecuteNonQuery();
            }

            //Adds new order lines to dbo.InfraOrderLines 
            using(OdbcCommand command = new OdbcCommand("INSERT INTO dbo.InfraOrderLines([F Code], Description, [Quantity Per]," +
                "[# of Cont.], [Cont. Type], UOM, Priority, [Total RQ]) SELECT [F Code], Description, [Quantity Per], [# of Cont.]," +
                "[Cont. Type], UOM, Priority, [Total RQ] FROM dbo.##temp", con))
            {
                command.ExecuteNonQuery();
            }

            //Sets Order # in recently added order lines
            using (OdbcCommand command = new OdbcCommand("UPDATE dbo.InfraOrderLines SET [Order #] = ? where [Order #] is NULL", con))
            {
                command.Parameters.AddWithValue("@num", lblOrderNum.Text);
                command.ExecuteNonQuery();
            }


            con.Close();
            Response.Redirect("~/Home.aspx");
        }

    }
}