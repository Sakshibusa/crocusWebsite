using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace crocusProject
{
    public partial class navFoot : System.Web.UI.MasterPage
    {
        String strcon = ConfigurationManager.ConnectionStrings["db_crocus.mdf"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategory();
            }
        }


        void BindCategory()
        {
            SqlConnection con = new SqlConnection(strcon);
            SqlDataAdapter da = new SqlDataAdapter("select * from Category", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            rptCategory.DataSource = dt;
            rptCategory.DataBind();
        }

        protected void rptCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int catId = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "CategoryId"));

                Repeater rptSub = (Repeater)e.Item.FindControl("rptSubCategory");

                SqlConnection con = new SqlConnection(strcon);
                SqlDataAdapter da = new SqlDataAdapter("select * from SubCategory where CategoryId=" + catId, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptSub.DataSource = dt;
                rptSub.DataBind();
            }
        }

    }
}