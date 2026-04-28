using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace crocusProject
{
    public partial class order : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["db_crocus.mdf"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadOrders("");
            }
        }
        void LoadOrders(string search)
        {
            SqlConnection con = new SqlConnection(cs);

            string query = @"
SELECT 
    o.OrderId,
    ISNULL(p.ProductName, 'No Product') AS ProductName,
    ISNULL(p.Image, 'noimage.png') AS Image,
    ISNULL(pay.PaymentMode, 'COD') AS PaymentMode,
    o.Status,
o.OrderDate,
    o.TotalAmount
FROM [Order] o
LEFT JOIN OrderDetails od ON o.OrderId = od.OrderId
LEFT JOIN Product p ON od.ProductId = p.ProductId
LEFT JOIN Payment pay ON o.OrderId = pay.OrderId";

            SqlCommand cmd = new SqlCommand(query, con);
            //cmd.Parameters.AddWithValue("@search", search);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gvOrders.DataSource = dt;
            gvOrders.DataBind();
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //LoadOrders(txtSearch.Text);
        }

        public string GetStatusClass(string status)
        {
            if (status == "Pending") return "status-pending";
            if (status == "Cancelled") return "status-cancel";
            if (status == "Delivered") return "status-delivered";
            return "";
        }
        protected void gvOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string status = DataBinder.Eval(e.Row.DataItem, "Status").ToString();

                DropDownList dd = (DropDownList)e.Row.FindControl("ddStatus");

                if (dd != null)
                {
                    dd.SelectedValue = status;

                    // color apply 🔥
                    dd.CssClass = "status-dropdown " + GetStatusClass(status);
                }
            }
        }
        protected void ddStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList dd = (DropDownList)sender;
            GridViewRow row = (GridViewRow)dd.NamingContainer;

            int orderId = Convert.ToInt32(gvOrders.DataKeys[row.RowIndex].Value);
            string status = dd.SelectedValue;

            string connStr = ConfigurationManager.ConnectionStrings["db_crocus.mdf"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connStr))
            {
             string query = "UPDATE [Order] SET Status=@Status WHERE OrderId=@OrderId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@OrderId", orderId);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            // reload grid
            LoadOrders("");
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string search = txtSearch.Text.Trim();
            string status = ddFilterStatus.SelectedValue;

            string connStr = ConfigurationManager.ConnectionStrings["db_crocus.mdf"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"
        SELECT 
            o.OrderId,
            o.OrderDate,
            o.TotalAmount,
            o.Status,
            p.ProductName,
            p.Image,
            od.Quantity,
            pay.PaymentMode
        FROM [Order] o
        INNER JOIN OrderDetails od ON o.OrderId = od.OrderId
        INNER JOIN Product p ON od.ProductId = p.ProductId
        LEFT JOIN Payment pay ON o.OrderId = pay.OrderId
        WHERE 1=1";

                if (!string.IsNullOrEmpty(search))
                {
                    query += " AND (CAST(o.OrderId AS VARCHAR) LIKE @search OR p.ProductName LIKE @search)";
                }

                if (!string.IsNullOrEmpty(status))
                {
                    query += " AND o.Status = @status";
                }

                SqlCommand cmd = new SqlCommand(query, con);

                if (!string.IsNullOrEmpty(search))
                    cmd.Parameters.AddWithValue("@search", "%" + search + "%");

                if (!string.IsNullOrEmpty(status))
                    cmd.Parameters.AddWithValue("@status", status);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvOrders.DataSource = dt;
                gvOrders.DataBind();
            }
        }
    }
}
