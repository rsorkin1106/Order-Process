using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace infragistics
{
    public partial class EditOrder : System.Web.UI.Page
    {
        TestEntities db;
        DataTable dt;
        OdbcDataAdapter adapt;
        OdbcConnection con;
        Page mainPage;
        GridView gvOrder;
        static int custID;
        static int orderNum;
        string connectionstring = "Driver={SQL Server}; Server=ROBS-LAPTOP\\SQLEXPRESS; UID=BmSys; PWD=Robert99; Database=Test";
        protected void Page_Load(object sender, EventArgs e)
        {
            db = new TestEntities();
            con = new OdbcConnection(connectionstring);
            con.Open();


            


            //Creates temp table to store order lines before order is placed
            if (!IsPostBack)
            {
                mainPage = (Page)Context.Handler;
                gvOrder = (GridView)mainPage.FindControl("gvOrders");
                orderNum = Int32.Parse(gvOrder.SelectedRow.Cells[1].Text);
                lblOrderNum.Text = orderNum.ToString();
                custID = Int32.Parse(gvOrder.SelectedRow.Cells[2].Text);


                //Gets Customer Name for Operator Label

                var customers = db.Customers.ToList();
                var nameList = from customer in customers
                             where customer.ID == custID
                             select customer.Name;

                List<string> temp = nameList.ToList();

                lbOp.Text = temp[0];

                try
                {
                    using (OdbcCommand command = new OdbcCommand("CREATE TABLE ##editTemp([Line #] int IDENTITY(1,1)," +
                        "[F Code] varchar(10), Description varchar(MAX), [Quantity Per] int, [# of Cont.] int, [Cont. Type] varchar(50)," +
                        "UOM varchar(10), Priority int, [Total RQ] int)", con))
                    {

                        command.ExecuteNonQuery();
                    }

                    
                }
                catch
                {
                    using (OdbcCommand command = new OdbcCommand("DELETE dbo.##editTemp; DBCC CHECKIDENT('##editTemp', RESEED, 0)", con))
                    {

                        command.ExecuteNonQuery();
                    }
                }

                //Fills ##editTemp with the selected order's order lines
                using (OdbcCommand command = new OdbcCommand($"INSERT INTO dbo.##editTemp([F Code], Description, [Quantity Per]," +
                $"[# of Cont.], [Cont. Type], UOM, Priority, [Total RQ]) SELECT [F Code], Description, [Quantity Per], [# of Cont.]," +
                $"[Cont. Type], UOM, Priority, [Total RQ] FROM dbo.InfraOrderLines WHERE [Order #] = {orderNum}", con))
                {

                    command.ExecuteNonQuery();
                }

            }
            /*else
            {
                using (OdbcCommand command = new OdbcCommand("DELETE dbo.##editTemp; DBCC CHECKIDENT('##editTemp', RESEED, 0)", con))
                {

                    command.ExecuteNonQuery();
                }
            } */



            

            ShowData();

            //Recreates buttons from original order
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


        private void btnDelete(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            int idx = Int32.Parse(b.ID);
            using (OdbcCommand command = new OdbcCommand("DELETE FROM dbo.##editTemp WHERE [Line #] = ?", con))
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


        private void ShowData()
        {
            dt = new DataTable();
            adapt = new OdbcDataAdapter("SELECT [Line #] as ID, [F Code] AS F_Code, [Description]," +
                "[Quantity Per] AS Quantity_Per, [# of Cont.] AS #_of_Cont, [Cont. Type] AS Cont_Type," +
                "[Total RQ] AS Total_RQ FROM dbo.##editTemp ORDER BY [Line #]", con);

            adapt.Fill(dt);
            gv1.DataSource = dt;
            gv1.DataBind();
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (OdbcCommand command = new OdbcCommand("INSERT INTO dbo.##editTemp([F Code], Description, [Quantity Per]," +
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

            //Adds new order to dbo.InfraOrders
            using (OdbcCommand command = new OdbcCommand("UPDATE dbo.InfraOrders SET [Update Date] = ? WHERE [Customer ID] = ?", con))
            { 
                command.Parameters.AddWithValue("@time", DateTime.Now);
                command.Parameters.AddWithValue("@id", custID);

                command.ExecuteNonQuery();
            }


            //Clears existing order before updating
            using(OdbcCommand command = new OdbcCommand($"DELETE FROM dbo.InfraOrderLines WHERE [Order #] = {orderNum}", con))
            {
                command.ExecuteNonQuery();
            }


            //Adds new order lines to dbo.InfraOrderLines with same order #
            using (OdbcCommand command = new OdbcCommand("INSERT INTO dbo.InfraOrderLines([F Code], Description, [Quantity Per]," +
                "[# of Cont.], [Cont. Type], UOM, Priority, [Total RQ]) SELECT [F Code], Description, [Quantity Per], [# of Cont.]," +
                "[Cont. Type], UOM, Priority, [Total RQ] FROM dbo.##editTemp", con))
            {
                command.ExecuteNonQuery();
            }

            //Sets Order # in recently added order lines
            using (OdbcCommand command = new OdbcCommand("UPDATE dbo.InfraOrderLines SET [Order #] = ? WHERE [Order #] is NULL", con))
            {
                command.Parameters.AddWithValue("@num", orderNum);
                command.ExecuteNonQuery();
            }


            con.Close();
            Response.Redirect("~/Home.aspx");
        }
    }
}