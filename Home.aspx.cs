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

    /// <summary>
    /// Program to Create/Edit/Delete Orders and Order Lines
    /// 
    /// Tables Used:
    /// 
    /// 1) dbo.InfraContainers
    /// Field       Type        Description
    /// Name        varchar(10) Name of container for dropdown list
    /// 
    /// 2) dbo.InfraOrders
    /// Field       Type        Description
    /// Order ID    int         Identity
    /// Customer ID int         ID of customer who order is for
    /// Update Time DateTime    Time of most recent CRUD to order
    /// 
    /// 3) dbo.InfraOrderLines
    /// Field       Type        Description
    /// Order #     int         The order which the line is for
    /// F Code      varchar(10) Code of item
    /// Description varchar(MAX) Description of what that order line is
    /// Quantity Per int        How much of item needed per container
    /// # of Cont.  int         Number of containers holding above quantity
    /// Cont. Type  varchar(50) Type of container to hold item
    /// UOM         varchar(10) Unit of Measure of item
    /// Priority    int         How urgent the item is
    /// Total RQ    int         Total amount of item required (Quantity per * # of Cont.)
    /// </summary>
    public partial class Home : System.Web.UI.Page
    {
        DataTable dt;
        OdbcDataAdapter adapt;
        string connectionstring = "Driver={SQL Server}; Server=ROBS-LAPTOP\\SQLEXPRESS; UID=BmSys; PWD=Robert99; Database=Test";
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowData();
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }

        protected void gvOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
        }

        //Displays most updated version of InfraOrders
        private void ShowData()
        {
            dt = new DataTable();
            using (OdbcConnection con = new OdbcConnection(connectionstring))
            {
                con.Open();
                adapt = new OdbcDataAdapter("SELECT * FROM dbo.InfraOrders ORDER BY [Order ID]", con);
                adapt.Fill(dt);
                gvOrders.DataSource = dt;
                gvOrders.DataBind();
                con.Close();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AddOrder.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(gvOrders.SelectedRow.Cells[1].Text);
            using(OdbcConnection con = new OdbcConnection(connectionstring))
            {
                con.Open();

                using(OdbcCommand command = new OdbcCommand($"DELETE FROM dbo.InfraOrderLines WHERE [Order #] = {id}", con))
                {
                    command.ExecuteNonQuery();
                }

                using (OdbcCommand command = new OdbcCommand($"DELETE FROM dbo.InfraOrders WHERE [Order ID] = {id}", con))
                {
                    command.ExecuteNonQuery();
                }

                con.Close();
            }
            ShowData();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Server.Transfer("EditOrder.aspx");
        }
    }
}