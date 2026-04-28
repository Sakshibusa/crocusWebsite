using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace crocusProject
{
    public partial class Success : System.Web.UI.Page
    {



        String strcon = ConfigurationManager.ConnectionStrings["db_crocus.mdf"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int orderId = Convert.ToInt32(Request.QueryString["orderId"]);

                lblOrderId.Text = "Order ID: " + orderId;
                lblDate.Text = "Date: " + DateTime.Now.ToString("dd MMM yyyy");
                lblPayment.Text = "Payment: " + Request.QueryString["pay"];

                LoadOrderItems(orderId);
                LoadTotal(orderId);
                LoadStatus(orderId);

            }
}
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        // Order Id
        //        lblOrderId.Text = "Order ID: " + Request.QueryString["orderId"];

        //        // Date
        //        lblDate.Text = "Date: " + DateTime.Now.ToString("dd MMM yyyy");

        //        // Payment
        //        lblPayment.Text = "Payment: " + Request.QueryString["pay"];

        //        // Cart items
        //        if (Session["FinalCart"] != null)
        //        {
        //            var cart = (List<CartItem>)Session["FinalCart"];
        //            rptItems.DataSource = cart;
        //            rptItems.DataBind();
        //        }

        // Total
        //        if (Session["Total"] != null)
        //        {
        //            lblTotal.Text = Session["Total"].ToString();
        //        }
        //    }
        //}





        private void LoadOrderItems(int orderId)
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                string query = @"
        SELECT P.ProductName AS Name, P.SellingPrice AS Price, 
               OD.Quantity AS Qty, P.Image
        FROM OrderDetails OD
        INNER JOIN Product P ON OD.ProductId = P.ProductID
        WHERE OD.OrderId=@OrderId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@OrderId", orderId);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                rptItems.DataSource = dr;
                rptItems.DataBind();
            }
        }


        private void LoadTotal(int orderId)
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                string query = "SELECT TotalAmount FROM [Order] WHERE OrderId=@OrderId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@OrderId", orderId);

                con.Open();
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    lblTotal.Text = Convert.ToDecimal(result).ToString("0.00");
                }
            }
        }
        protected void btnTrack_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserHome.aspx");
        }


        private void LoadStatus(int orderId)
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                string query = "SELECT Status FROM [Order] WHERE OrderId=@OrderId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@OrderId", orderId);

                con.Open();
                object result = cmd.ExecuteScalar();

                string status = "";

                if (result != null && result != DBNull.Value)
                {
                    status = result.ToString();
                }
                else
                {
                    status = "Order Placed";
                }

                // 🔥 IMPORTANT LINE
                ClientScript.RegisterStartupScript(this.GetType(), "status",
                    $"setStatus('{status}');", true);
            }
        }


        //private void LoadStatus(int orderId)
        //{
        //    using (SqlConnection con = new SqlConnection(strcon))
        //    {
        //        string query = "SELECT Status FROM [Order] WHERE OrderId=@OrderId";
        //        SqlCommand cmd = new SqlCommand(query, con);
        //        cmd.Parameters.AddWithValue("@OrderId", orderId);

        //        con.Open();
        //        object result = cmd.ExecuteScalar();

        //        string status = "";

        //        if (result != null && result != DBNull.Value)
        //        {
        //            status = result.ToString();
        //        }
        //        else
        //        {
        //            status = "Order Placed"; // default
        //        }
        //    }
        //}
    }
}