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

    public partial class usermanage : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["db_crocus.mdf"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadUsers();
        }

        void LoadUsers()
        {
            SqlConnection con = new SqlConnection(strcon);

            string query = "SELECT * FROM tblUser";

            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gvUsers.DataSource = dt;
            gvUsers.DataBind();
        }
    }
}