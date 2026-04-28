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
    public partial class UserHome : System.Web.UI.Page
    {
        String strcon = ConfigurationManager.ConnectionStrings["db_crocus.mdf"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write(Session["UserId"]);  

            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                LoadProducts();
            }
        }
        //void LoadProducts()
        //{
        //    SqlConnection con = new SqlConnection(strcon);
        //    SqlDataAdapter da = new SqlDataAdapter("select ProductId,ProductName,Price,SellingPrice,Description,Image from Product", con);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);

        //    repet.DataSource = dt;
        //    repet.DataBind();
        //    //Response.Write("Rows = " + dt.Rows.Count);
        //}


        void LoadProducts()
        {
            SqlConnection con = new SqlConnection(strcon);

            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT TOP 8 ProductId, ProductName, Price, SellingPrice, Description, Image FROM Product",
                con);

            DataTable dt = new DataTable();
            da.Fill(dt);

            repet.DataSource = dt;
            repet.DataBind();
        }
    }
}