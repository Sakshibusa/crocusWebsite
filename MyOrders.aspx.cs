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
    public partial class MyOrders : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["db_crocus.mdf"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadOrders();
            }
        }

        private void LoadOrders()
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            int userId = Convert.ToInt32(Session["UserId"]);

            using (SqlConnection con = new SqlConnection(strcon))
            {
                string query = "SELECT OrderId, OrderDate, TotalAmount, Status FROM [Order] WHERE UserId=@UserId ORDER BY OrderId DESC";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserId", userId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptOrders.DataSource = dt;
                rptOrders.DataBind();
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            int orderId = Convert.ToInt32(((System.Web.UI.WebControls.Button)sender).CommandArgument);

            Response.Redirect("Success.aspx?orderId=" + orderId);
        }
    }
}