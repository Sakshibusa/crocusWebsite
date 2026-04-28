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
    public partial class plantshrubs : System.Web.UI.Page
    {
        String strcon = ConfigurationManager.ConnectionStrings["db_crocus.mdf"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["subId"] != null)
                {
                    int subId = Convert.ToInt32(Request.QueryString["subId"]);
                    LoadProducts(subId);
                }
            }
        }

        void LoadProducts(int subId)
        {

            using (SqlConnection con = new SqlConnection(strcon))
            {
                string query = "SELECT * FROM Product WHERE subCategoryId=@SubCategoryId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@SubCategoryId", subId);

                con.Open();
                repet.DataSource = cmd.ExecuteReader();
                repet.DataBind();
            }
        }

    }
}