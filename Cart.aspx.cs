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
    public partial class Cart : System.Web.UI.Page
    {
        String strcon = ConfigurationManager.ConnectionStrings["db_crocus.mdf"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCart();
            }
            //    if (Session["cart"] != null)
            //    {
            //        List<CartItem> cart = (List<CartItem>)Session["cart"];

            //        // IMPORTANT: bind every time (not only first load)
            //        rptCart.DataSource = cart;
            //        rptCart.DataBind();

            //        CalculateTotal(cart);
            //    }
        }

        private void LoadCart()
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            int userId = Convert.ToInt32(Session["UserId"]);

            using (SqlConnection con = new SqlConnection(strcon))
            {
                string query = @"
                SELECT C.ProductId, P.ProductName, P.SellingPrice AS Price, 
                       C.Quantity, P.Image
                FROM Cart C
                INNER JOIN Product P ON C.ProductId = P.ProductID
                WHERE C.UserID = @UserID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserID", userId);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                rptCart.DataSource = dr;
                rptCart.DataBind();
            }

            CalculateTotal();
        }

        private void CalculateTotal()
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            using (SqlConnection con = new SqlConnection(strcon))
            {
                string query = @"
        SELECT SUM(P.SellingPrice * C.Quantity) 
        FROM Cart C
        INNER JOIN Product P ON C.ProductId = P.ProductID
        WHERE C.UserID=@UserID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserID", userId);

                con.Open();

                object result = cmd.ExecuteScalar();

                decimal subtotal = 0;
                if (result != DBNull.Value)
                {
                    subtotal = Convert.ToDecimal(result);
                }

                lblSubtotal.Text = subtotal.ToString("0.00");
                lblTotal.Text = subtotal.ToString("0.00");
            }
        }


        //void CalculateTotal(List<CartItem> cart)
        //{
        //    decimal subtotal = 0;

        //    foreach (var item in cart)
        //    {
        //        subtotal += item.Price * item.Qty;
        //    }

        //    lblSubtotal.Text = subtotal.ToString("0.00");

        //    //decimal discount = 150; // example
        //    //decimal total = subtotal - discount;

        //    //decimal discount = 150; // example
        //    decimal total = subtotal;


        //    lblTotal.Text = total.ToString("0.00");
        //}


        protected void rptCart_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int productId = Convert.ToInt32(e.CommandArgument);
            int userId = Convert.ToInt32(Session["UserId"]);

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();

                if (e.CommandName == "plus")
                {
                    string q = "UPDATE Cart SET Quantity = Quantity + 1 WHERE UserID=@UserID AND ProductId=@ProductId";
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.ExecuteNonQuery();
                }
                else if (e.CommandName == "minus")
                {
                    string q = "UPDATE Cart SET Quantity = CASE WHEN Quantity>1 THEN Quantity-1 ELSE 1 END WHERE UserID=@UserID AND ProductId=@ProductId";
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.ExecuteNonQuery();
                }
            }

            LoadCart();
        }

        //protected void rptCart_ItemCommand(object source, RepeaterCommandEventArgs e)
        //{
        //    List<CartItem> cart = (List<CartItem>)Session["cart"];

        //    int index = Convert.ToInt32(e.CommandArgument);

        //    if (e.CommandName == "plus")
        //    {
        //        cart[index].Qty++;
        //    }
        //    else if (e.CommandName == "minus")
        //    {
        //        if (cart[index].Qty > 1)
        //            cart[index].Qty--;
        //    }

        //    Session["cart"] = cart;

        //    rptCart.DataSource = cart;
        //    rptCart.DataBind();

        //    CalculateTotal(cart);
        //}




        protected void btnRemove_Click(object sender, EventArgs e)
        {
            int productId = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            int userId = Convert.ToInt32(Session["UserId"]);

            using (SqlConnection con = new SqlConnection(strcon))
            {
                string query = "DELETE FROM Cart WHERE UserID=@UserID AND ProductId=@ProductId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@ProductId", productId);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            LoadCart();
        }

        //protected void btnRemove_Click(object sender, EventArgs e)
        //{
        //    int productId = Convert.ToInt32(((LinkButton)sender).CommandArgument);

        //    List<CartItem> cart = (List<CartItem>)Session["cart"];

        //    if (cart != null)
        //    {
        //        cart.RemoveAll(x => x.ProductId == productId);
        //        Session["cart"] = cart;
        //    }

        //    Response.Redirect("Cart.aspx");
        //}


        protected void btnCheckOut_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            using (SqlConnection con = new SqlConnection(strcon))
            {
                string query = "SELECT COUNT(*) FROM Cart WHERE UserID=@UserID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserID", userId);

                con.Open();
                int count = (int)cmd.ExecuteScalar();

                if (count == 0)
                {
                    Response.Write("<script>alert('Cart is empty');</script>");
                    return;
                }
            }

            Response.Redirect("CheckOut.aspx");
        }

        //protected void btnCheckOut_Click(object sender, EventArgs e)
        //{
        //    if (Session["cart"] == null)
        //    {
        //        Response.Write("<script>alert('Cart is empty');</script>");
        //        return;
        //    }

        //    Response.Redirect("CheckOut.aspx");
        //}
    }
}