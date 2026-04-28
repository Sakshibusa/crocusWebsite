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
    public partial class slider : System.Web.UI.Page
    {
        String strcon = ConfigurationManager.ConnectionStrings["db_crocus.mdf"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProducts();
            }
        }

        void LoadProducts()
        {
            SqlConnection con = new SqlConnection(strcon);
            SqlDataAdapter da = new SqlDataAdapter("select ProductName,Price,SellingPrice,Description,Image from Product", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            repet.DataSource = dt;
            repet.DataBind();
            //Response.Write("Rows = " + dt.Rows.Count);
        }
    }
}