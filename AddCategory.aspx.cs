using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace crocusProject
{
    public partial class AddCategory : System.Web.UI.Page
    {
        String strcon = ConfigurationManager.ConnectionStrings["db_crocus.mdf"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowCat();
                LoadGrid();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
           

            // Connection
            SqlConnection con = new SqlConnection(strcon);

            //string query = "insert into CategoryId(CategoryName) values(@cname)";
            String query = "insert into Category(CategoryName) values(@cname)";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@cname", txtCategoryName.Text);
         
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            Response.Write("Category Saved Successfully");
        }

        private void ShowCat()
        {
            SqlConnection con = new SqlConnection(strcon);

            
            String query = "select * from Category";
            SqlDataAdapter da=new SqlDataAdapter(query, con);
            DataTable dt=new DataTable();
            da.Fill(dt);
            rptcat.DataSource=dt;
            rptcat.DataBind();

        }


        public void LoadGrid()
        {
            SqlConnection con = new SqlConnection(strcon);

            SqlDataAdapter da = new SqlDataAdapter(
            "select * from Category", con);

            DataTable dt = new DataTable();
            da.Fill(dt);

            gv_cate.DataSource = dt;
            gv_cate.DataBind();

        }

        //protected void gv_cate_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GridViewRow r = gv_cate.SelectedRow;

        //    txtCategoryName.Text= r.Cells[2].Text;

        //}

        protected void gv_cate_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(strcon);

            int id = Convert.ToInt32(gv_cate.DataKeys[e.RowIndex].Value);

            SqlCommand cmd =
                new SqlCommand("delete from Category where CategoryId=@id", con);

            cmd.Parameters.AddWithValue("@id", id);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            LoadGrid();
        }

        protected void gv_cate_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gv_cate.EditIndex = e.NewEditIndex;
            LoadGrid();
        }

        protected void gv_cate_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_cate.EditIndex = -1;
            LoadGrid();
        }

        protected void gv_cate_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gv_cate.DataKeys[e.RowIndex].Value);

            TextBox txt = (TextBox)gv_cate.Rows[e.RowIndex].Cells[2].Controls[0];

            SqlConnection con = new SqlConnection(strcon);

            SqlCommand cmd = new SqlCommand(
            "update Category set CategoryName=@name where CategoryId=@id", con);

            cmd.Parameters.AddWithValue("@name", txt.Text);
            cmd.Parameters.AddWithValue("@id", id);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            gv_cate.EditIndex = -1;

            LoadGrid();




            //int id = Convert.ToInt32(gv_cate.DataKeys[e.RowIndex].Value);

            //TextBox txt = (TextBox)gv_cate.Rows[e.RowIndex].FindControl("txtCategoryName");

            //SqlConnection con = new SqlConnection(strcon);

            //SqlCommand cmd = new SqlCommand("update Category set CategoryName=@name where CategoryId=@id", con);

            //cmd.Parameters.AddWithValue("@name", txt.Text);
            //cmd.Parameters.AddWithValue("@id", id);

            //con.Open();
            //cmd.ExecuteNonQuery();
            //con.Close();

            //gv_cate.EditIndex = -1;

            //LoadGrid();
        }

        protected void gv_cate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "Total Category : " + gv_cate.Rows.Count.ToString();
                //e.Row.Cells[2].Text = gv_cate.Rows.Count.ToString();
            }
        }
    }
}