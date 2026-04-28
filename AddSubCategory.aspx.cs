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
    public partial class AddSubCategory : System.Web.UI.Page
    {
        String strcon = ConfigurationManager.ConnectionStrings["db_crocus.mdf"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategory();
                LoadsubCat();
                FillCategory();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {


            // Connection
            SqlConnection con = new SqlConnection(strcon);

            string query = "insert into subCategory(SubCategoryName,CategoryId) values(@sname,@cid)";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@sname", txtsubcategoryname.Text);
            cmd.Parameters.AddWithValue("@cid", ddcatid.SelectedValue);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            Response.Write("Category Saved Successfully");
        }

        private void BindCategory()
        {
            SqlConnection con = new SqlConnection(strcon);

            string query = "select * from Category";

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter sda= new SqlDataAdapter(cmd);
            DataTable dt=new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ddcatid.DataSource = dt;
                ddcatid.DataTextField = "CategoryName";
                ddcatid.DataValueField = "CategoryId";
                ddcatid.DataBind();
                //ddcatid.Items.Insert(0,"--Select Category--");
            }
        }

        private void LoadsubCat()
        {
            SqlConnection con = new SqlConnection(strcon);


            String query = "select s.SubCategoryId, s.SubCategoryName, c.CategoryId,c.CategoryName from subCategory s inner join Category c on c.CategoryId=s.CategoryId";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gv_subcat.DataSource = dt;
            gv_subcat.DataBind();

        }

        protected void gv_subcat_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(strcon);

            int id = Convert.ToInt32(gv_subcat.DataKeys[e.RowIndex].Value);

            SqlCommand cmd =
                new SqlCommand("delete from subCategory where subCategoryId=@id", con);

            cmd.Parameters.AddWithValue("@id", id);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            LoadsubCat();
        }

        protected void gv_subcat_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gv_subcat.EditIndex = e.NewEditIndex;
            LoadsubCat();
        }

        protected void gv_subcat_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gv_subcat.DataKeys[e.RowIndex].Value);

            GridViewRow row = gv_subcat.Rows[e.RowIndex];

            TextBox txt = (TextBox)row.FindControl("txtSubCat");
            DropDownList ddl = (DropDownList)row.FindControl("ddlCategory");

            SqlConnection con = new SqlConnection(strcon);

            string query = "update subCategory set SubCategoryName=@name,CategoryId=@cid where SubCategoryId=@id";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@name", txt.Text);
            cmd.Parameters.AddWithValue("@cid", ddl.SelectedValue);
            cmd.Parameters.AddWithValue("@id", id);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            gv_subcat.EditIndex = -1;
            LoadsubCat();
        }

       

        protected void gv_subcat_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_subcat.EditIndex = -1;
            LoadsubCat();
        }

        protected void gv_subcat_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gv_subcat.EditIndex == e.Row.RowIndex)
            {
                DropDownList ddl = (DropDownList)e.Row.FindControl("ddlCategory");

                SqlConnection con = new SqlConnection(strcon);

                SqlDataAdapter da = new SqlDataAdapter("select * from Category", con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                ddl.DataSource = dt;
                ddl.DataTextField = "CategoryName";
                ddl.DataValueField = "CategoryId";
                ddl.DataBind();
            }
        }







        public void FillCategory()
        {
            SqlConnection con = new SqlConnection(strcon);
            SqlDataAdapter da = new SqlDataAdapter("select * from Category", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlCategoryFilter.DataSource = dt;
            ddlCategoryFilter.DataTextField = "CategoryName";
            ddlCategoryFilter.DataValueField = "CategoryId";
            ddlCategoryFilter.DataBind();

            ddlCategoryFilter.Items.Insert(0, new ListItem("--Select Category--", "0"));
        }
        protected void ddlCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(strcon);

            string query = "select * from SubCategory";

            if (ddlCategoryFilter.SelectedValue != "0")
            {
                query = "select * from SubCategory where CategoryId=" + ddlCategoryFilter.SelectedValue;
            }

            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gv_search.DataSource = dt;
            gv_search.DataBind();
            lblTotal.Text = "Total SubCategory : " + dt.Rows.Count;
        }



       
    }
}