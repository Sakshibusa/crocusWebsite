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
    public partial class wishlist : System.Web.UI.Page
    {
        String strcon = ConfigurationManager.ConnectionStrings["db_crocus.mdf"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            
                if (!IsPostBack)
                {
                    LoadWishlist();
                }
            }

        void LoadWishlist()
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            SqlConnection con = new SqlConnection(strcon);

            string query = @"SELECT W.ProductID, P.ProductName, P.Price, P.SellingPrice, P.Image
                     FROM Wishlist W
                     INNER JOIN Product P ON W.ProductID = P.ProductID
                     WHERE W.Id = @uid";

            SqlDataAdapter da = new SqlDataAdapter(query, con);
            da.SelectCommand.Parameters.AddWithValue("@uid", userId);

            DataTable dt = new DataTable();
            da.Fill(dt);

            rptWishlist.DataSource = dt;
            rptWishlist.DataBind();
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            int productId = Convert.ToInt32((sender as Button).CommandArgument);
            int userId = Convert.ToInt32(Session["UserID"]);

            SqlConnection con = new SqlConnection(strcon);
            SqlCommand cmd = new SqlCommand("DELETE FROM Wishlist WHERE ID=@u AND ProductID=@p", con);

            cmd.Parameters.AddWithValue("@u", userId);
            cmd.Parameters.AddWithValue("@p", productId);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            LoadWishlist();
        }

        protected void btnAddCart_Click(object sender, EventArgs e)
        {
            int productId = Convert.ToInt32((sender as Button).CommandArgument);
            int userId = Convert.ToInt32(Session["UserId"]);

            SqlConnection con = new SqlConnection(strcon);

            string query = "INSERT INTO Cart(UserID,ProductId,Quantity) VALUES(@u,@p,1)";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@u", userId);
            cmd.Parameters.AddWithValue("@p", productId);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            Response.Redirect("Cart.aspx");
        }
    }
}