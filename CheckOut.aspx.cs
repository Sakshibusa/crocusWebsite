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
    public partial class CheckOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCart();
            }
        }


        private void LoadCart()
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            int userId = Convert.ToInt32(Session["UserId"]);

            string cs = ConfigurationManager.ConnectionStrings["db_crocus.mdf"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"
        SELECT P.ProductName AS Name, P.SellingPrice AS Price, C.Quantity AS Qty
        FROM Cart C
        INNER JOIN Product P ON C.ProductId = P.ProductID
        WHERE C.UserID=@UserID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserID", userId);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                rptSummary.DataSource = dr;
                rptSummary.DataBind();
            }

            CalculateTotal(); // 👈 add this
        }



        //private void LoadCart()
        //{
        //    if (Session["cart"] != null)
        //    {
        //        List<CartItem> cart = (List<CartItem>)Session["cart"];
        //        rptSummary.DataSource = cart;
        //        rptSummary.DataBind();

        //        decimal total = 0;
        //        foreach (var item in cart)
        //        {
        //            total += item.Price * item.Qty;
        //        }

        //        lblTotal.Text = "₹ " + total.ToString();
        //        Session["Total"] = total;
        //    }
        //}



        private void CalculateTotal()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            string cs = ConfigurationManager.ConnectionStrings["db_crocus.mdf"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
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

                decimal total = 0;
                if (result != DBNull.Value)
                {
                    total = Convert.ToDecimal(result);
                }

                lblTotal.Text = "₹ " + total.ToString("0.00");
                Session["Total"] = total;
            }
        }

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            int userId = Convert.ToInt32(Session["UserId"]);
            string cs = ConfigurationManager.ConnectionStrings["db_crocus.mdf"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                string paymentMethod = Request.Form["pay"]; // COD / UPI

                // 👉 1. Get total from DB
                decimal total = 0;
                string totalQuery = @"
        SELECT SUM(P.SellingPrice * C.Quantity)
        FROM Cart C
        INNER JOIN Product P ON C.ProductId = P.ProductID
        WHERE C.UserID=@UserID";

                SqlCommand cmdTotal = new SqlCommand(totalQuery, con);
                cmdTotal.Parameters.AddWithValue("@UserID", userId);

                object result = cmdTotal.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    total = Convert.ToDecimal(result);
                }

                // 👉 2. Insert Order
                string orderQuery = @"
        INSERT INTO [Order] (UserId, OrderDate, TotalAmount, Status)
        OUTPUT INSERTED.OrderId
        VALUES (@UserId, @Date, @Total, @Status)";

                SqlCommand cmdOrder = new SqlCommand(orderQuery, con);
                cmdOrder.Parameters.AddWithValue("@UserId", userId);
                cmdOrder.Parameters.AddWithValue("@Date", DateTime.Now);
                cmdOrder.Parameters.AddWithValue("@Total", total);
                cmdOrder.Parameters.AddWithValue("@Status", "Pending");

                int orderId = (int)cmdOrder.ExecuteScalar();

                // 👉 3. Get cart items
                string cartQuery = "SELECT ProductId, Quantity FROM Cart WHERE UserID=@UserID";
                SqlCommand cmdCart = new SqlCommand(cartQuery, con);
                cmdCart.Parameters.AddWithValue("@UserID", userId);

                SqlDataReader dr = cmdCart.ExecuteReader();

                List<Tuple<int, int>> cartItems = new List<Tuple<int, int>>();

                while (dr.Read())
                {
                    cartItems.Add(new Tuple<int, int>(
                        Convert.ToInt32(dr["ProductId"]),
                        Convert.ToInt32(dr["Quantity"])
                    ));
                }
                dr.Close();

                // 👉 4. Insert OrderDetails
                foreach (var item in cartItems)
                {
                    string detailQuery = @"
            INSERT INTO OrderDetails (OrderId, ProductId, Quantity)
            VALUES (@OrderId, @ProductId, @Qty)";

                    SqlCommand cmdDetail = new SqlCommand(detailQuery, con);
                    cmdDetail.Parameters.AddWithValue("@OrderId", orderId);
                    cmdDetail.Parameters.AddWithValue("@ProductId", item.Item1);
                    cmdDetail.Parameters.AddWithValue("@Qty", item.Item2);

                    cmdDetail.ExecuteNonQuery();
                }

                // 👉 5. Insert Payment
                string payQuery = @"
        INSERT INTO Payment (OrderId, PaymentMode)
        VALUES (@OrderId, @PaymentMode)";

                SqlCommand cmdPay = new SqlCommand(payQuery, con);
                cmdPay.Parameters.AddWithValue("@OrderId", orderId);
                cmdPay.Parameters.AddWithValue("@PaymentMode", paymentMethod);

                cmdPay.ExecuteNonQuery();

                // 👉 6. Clear Cart (DB)
                string clearCart = "DELETE FROM Cart WHERE UserID=@UserID";
                SqlCommand cmdClear = new SqlCommand(clearCart, con);
                cmdClear.Parameters.AddWithValue("@UserID", userId);
                cmdClear.ExecuteNonQuery();

                // 👉 7. Redirect
                Response.Redirect("Success.aspx?orderId=" + orderId + "&pay=" + paymentMethod);
            }
        }

        //protected void btnPlaceOrder_Click(object sender, EventArgs e)
        //{

        //    string cs = ConfigurationManager.ConnectionStrings["db_crocus.mdf"].ConnectionString;

        //    using (SqlConnection con = new SqlConnection(cs))
        //    {
        //        con.Open();

        //        string paymentMethod = Request.Form["pay"]; // COD / UPI

        //        // 👉 1. Order insert
        //        string orderQuery = @"INSERT INTO [Order] (UserId, OrderDate, TotalAmount, Status)
        //                      OUTPUT INSERTED.OrderId
        //                      VALUES (@UserId, @Date, @Total, @Status)";

        //        SqlCommand cmd = new SqlCommand(orderQuery, con);

        //        cmd.Parameters.AddWithValue("@UserId", 1);
        //        cmd.Parameters.AddWithValue("@Date", DateTime.Now);
        //        cmd.Parameters.AddWithValue("@Total", Session["Total"]);
        //        cmd.Parameters.AddWithValue("@Status", "Pending");

        //        int orderId = (int)cmd.ExecuteScalar();

        //        // 👉 2. OrderDetails insert
        //        var cart = (List<CartItem>)Session["Cart"];

        //        foreach (var item in cart)
        //        {
        //            string detailQuery = @"INSERT INTO OrderDetails 
        //                          (OrderId, ProductId, Quantity, Price)
        //                          VALUES (@OrderId, @ProductId, @Qty, @Price)";

        //            SqlCommand cmd2 = new SqlCommand(detailQuery, con);

        //            cmd2.Parameters.AddWithValue("@OrderId", orderId);
        //            cmd2.Parameters.AddWithValue("@ProductId", item.ProductId);
        //            cmd2.Parameters.AddWithValue("@Qty", item.Qty);
        //            cmd2.Parameters.AddWithValue("@Price", item.Price);

        //            cmd2.ExecuteNonQuery();
        //        }

        //        // 👉 3. Payment insert
        //        string payQuery = @"INSERT INTO Payment (OrderId,PaymentMode)
        //                    VALUES (@OrderId, @PaymentMode)";

        //        SqlCommand cmd3 = new SqlCommand(payQuery, con);

        //        cmd3.Parameters.AddWithValue("@OrderId", orderId);
        //        cmd3.Parameters.AddWithValue("@PaymentMode", paymentMethod);

        //        cmd3.ExecuteNonQuery();

        //        //// 👉 4. Cart clear
        //        //Session["cart"] = null;

        //        Session["FinalCart"] = Session["cart"];  // 👈 save for success page
        //        Session["cart"] = null;                 // 👈 clear cart

        //        // 👉 5. Redirect
        //        Response.Redirect("Success.aspx?orderId=" + orderId + "&pay=" + paymentMethod);
        //    }

        //}

    }
}