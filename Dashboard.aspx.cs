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
    public partial class Dashboard : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["db_crocus.mdf"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCounts();
            }
        }
        private void LoadCounts()
        {
            SqlConnection con = new SqlConnection(strcon);
            con.Open();

            // Total Products
            SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM Product", con);
            lblProducts.Text = cmd1.ExecuteScalar().ToString();

            // Total Categories
            SqlCommand cmd2 = new SqlCommand("SELECT COUNT(*) FROM Category", con);
            lblCategories.Text = cmd2.ExecuteScalar().ToString();

            // Total Subcategories
            SqlCommand cmd3 = new SqlCommand("SELECT COUNT(*) FROM subCategory", con);
            lblSubCategories.Text = cmd3.ExecuteScalar().ToString();

            SqlCommand cmd4 = new SqlCommand("SELECT COUNT(*) FROM [Order]", con);
            lblorder.Text = cmd4.ExecuteScalar().ToString();
            con.Close();
        }
    }
}