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
    public partial class ProductDetails : System.Web.UI.Page
    {
        String strcon = ConfigurationManager.ConnectionStrings["db_crocus.mdf"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

            //Response.Write(Session["UserId"]);  //  yaha likho
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {

               
                txtQty.Text = "1";  // default set
                string productId = Request.QueryString["ProductID"];

                if (!string.IsNullOrEmpty(productId))
                {
                    string query = "SELECT * FROM Product WHERE ProductID=@ProductID";
                    using (SqlConnection con = new SqlConnection(strcon))
                    {
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@ProductID", productId);
                            con.Open();
                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.Read())
                            {
                                
                                    lblName.Text = dr["ProductName"].ToString();
                                    lblSellingPrice.Text = dr["SellingPrice"].ToString();
                                    lblPrice.Text = dr["Price"].ToString();

                                    lblDescription.Text = dr["Description"].ToString();

                                    imgProduct.ImageUrl = dr["Image"].ToString();
                                
                            }
                        }
                    }
                }
            }
        }


        protected void btnMinus_Click(object sender, EventArgs e)
        {
            int qty = Convert.ToInt32(txtQty.Text);

            if (qty > 1)
            {
                qty--;
                txtQty.Text = qty.ToString();
            }
        }

        protected void btnPlus_Click(object sender, EventArgs e)
        {
            int qty = Convert.ToInt32(txtQty.Text);
            qty++;
            txtQty.Text = qty.ToString();
        }




        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            int userId = Convert.ToInt32(Session["UserId"]);
            int productId = Convert.ToInt32(Request.QueryString["ProductID"]); // ✅ FIXED
            int qty = Convert.ToInt32(txtQty.Text);

            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();

                string checkQuery = "SELECT COUNT(*) FROM Cart WHERE UserID=@UserID AND ProductId=@ProductId";
                SqlCommand cmdCheck = new SqlCommand(checkQuery, con);
                cmdCheck.Parameters.AddWithValue("@UserID", userId);
                cmdCheck.Parameters.AddWithValue("@ProductId", productId);

                int count = (int)cmdCheck.ExecuteScalar();

                if (count > 0)
                {
                    string updateQuery = "UPDATE Cart SET Quantity = Quantity + @Qty WHERE UserID=@UserID AND ProductId=@ProductId";
                    SqlCommand cmdUpdate = new SqlCommand(updateQuery, con);
                    cmdUpdate.Parameters.AddWithValue("@Qty", qty);
                    cmdUpdate.Parameters.AddWithValue("@UserID", userId);
                    cmdUpdate.Parameters.AddWithValue("@ProductId", productId);
                    cmdUpdate.ExecuteNonQuery();
                }
                else
                {
                    string insertQuery = "INSERT INTO Cart(UserID, ProductId, Quantity) VALUES(@UserID,@ProductId,@Qty)";
                    SqlCommand cmdInsert = new SqlCommand(insertQuery, con);
                    cmdInsert.Parameters.AddWithValue("@UserID", userId);
                    cmdInsert.Parameters.AddWithValue("@ProductId", productId);
                    cmdInsert.Parameters.AddWithValue("@Qty", qty);
                    cmdInsert.ExecuteNonQuery();
                }
            }

            Response.Redirect("Cart.aspx");
        }



        //protected void btnAdd_Click(object sender, EventArgs e)
        //{
        //    // 🔹 Login check
        //    if (Session["UserId"] == null)
        //    {
        //        Response.Redirect("Login.aspx");
        //        return;
        //    }

        //    // 🔹 ProductId URL se lo
        //    int pid = Convert.ToInt32(Request.QueryString["ProductID"]);

        //    // 🔹 Quantity lo
        //    int qty = Convert.ToInt32(txtQty.Text);

        //    // 🔹 Product details (label se)
        //    string name = lblName.Text;
        //    //int price = Convert.ToInt32(lblSellingPrice.Text);
        //    //string priceText = lblSellingPrice.Text.Replace("Rs.", "").Trim();
        //    //int price = Convert.ToInt32(priceText);

        //    string image = imgProduct.ImageUrl;


        //    // sirf numbers extract karo
        //    //string priceText = new string(lblSellingPrice.Text.Where(char.IsDigit).ToArray());

        //    //int.TryParse(priceText, out price);
        //    decimal price = 0;
        //    decimal.TryParse(lblSellingPrice.Text.Replace("₹", "").Trim(), out price);

        //    List<CartItem> cart;

        //    // 🔹 Session create/get
        //    if (Session["cart"] == null)
        //    {
        //        cart = new List<CartItem>();
        //    }
        //    else
        //    {
        //        cart = (List<CartItem>)Session["cart"];
        //    }

        //    // 🔍 check already exist
        //    CartItem existing = cart.Find(x => x.ProductId == pid);

        //    if (existing != null)
        //    {
        //        existing.Qty += qty;
        //    }
        //    else
        //    {
        //        CartItem item = new CartItem()
        //        {
        //            ProductId = pid,
        //            Name = name,
        //            Price = price,
        //            Qty = qty,
        //                Image = image   // ✅ ADD THIS
        //        };

        //        cart.Add(item);
        //    }

        //    // 🔹 save
        //    Session["cart"] = cart;

        //    Response.Redirect("Cart.aspx");
        //}



        //protected void btnMinus_Click(object sender, EventArgs e)
        //{
        //    int qty = Convert.ToInt32(txtQty.Text);

        //    if (qty > 1)
        //    {
        //        qty--;
        //        txtQty.Text = qty.ToString();
        //    }
        //}

        //protected void btnPlus_Click(object sender, EventArgs e)
        //{
        //    int qty = Convert.ToInt32(txtQty.Text);
        //    qty++;
        //    txtQty.Text = qty.ToString();
        //}


        //protected void btnPlus_Click(object sender, EventArgs e)
        //{
        //    int qty = Convert.ToInt32(txtQty.Text);
        //    qty++;
        //    txtQty.Text = qty.ToString();
        //}

        //protected void btnMinus_Click(object sender, EventArgs e)
        //{
        //    int qty = Convert.ToInt32(txtQty.Text);

        //    if (qty > 1)
        //    {
        //        qty--;
        //        txtQty.Text = qty.ToString();
        //    }
        //}

        protected void btnWishlist_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Write("<script>alert('Please login first');</script>");
                return;
            }

            int userId = Convert.ToInt32(Session["UserId"]);

            int productId;
            if (!int.TryParse(Request.QueryString["ProductID"], out productId))
            {
                Response.Write("<script>alert('Invalid Product ID');</script>");
                return;
            }

            SqlConnection con = new SqlConnection(strcon);
            SqlCommand cmd = new SqlCommand("INSERT INTO Wishlist (ID, ProductID) VALUES (@u,@p)", con);

            cmd.Parameters.AddWithValue("@u", userId);
            cmd.Parameters.AddWithValue("@p", productId);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            Response.Write("<script>alert('Added to Wishlist');</script>");
        }

        //protected void btnWishlist_Click(object sender, EventArgs e)
        //{
        //    int userId = Convert.ToInt32(Session["UserID"]); // login user
        //    int productId = Convert.ToInt32((sender as Button).CommandArgument);

        //    SqlConnection con = new SqlConnection(strcon);
        //    SqlCommand cmd = new SqlCommand("INSERT INTO Wishlist (ID, ProductID) VALUES (@u, @p)", con);

        //    cmd.Parameters.AddWithValue("@u", userId);
        //    cmd.Parameters.AddWithValue("@p", productId);

        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    con.Close();

        //    Response.Write("<script>alert('Added to Wishlist');</script>");
        //}

    }
}