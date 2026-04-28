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
    public partial class Adminpage : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["db_crocus.mdf"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategory();
                ddsubcat.Enabled = false;
                ShowProducts();
            }
        }

        // Add Product
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string folderPath = Server.MapPath("~/Images/");
            if (!System.IO.Directory.Exists(folderPath)) System.IO.Directory.CreateDirectory(folderPath);

            string imgPath = "~/Images/" + FileUpload1.FileName;
            FileUpload1.SaveAs(Server.MapPath(imgPath));

            SqlConnection con = new SqlConnection(strcon);
            string query = "INSERT INTO Product(CategoryId,subCategoryId,ProductName,Price,SellingPrice,Description,Image) VALUES(@cid,@sid,@pname,@price,@sprice,@desc,@img)";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@cid", ddcat.SelectedValue);
            cmd.Parameters.AddWithValue("@sid", ddsubcat.SelectedValue);
            cmd.Parameters.AddWithValue("@pname", txtProductName.Text);
            cmd.Parameters.AddWithValue("@price", txtPrice.Text);
            cmd.Parameters.AddWithValue("@sprice", txtsellprice.Text);
            cmd.Parameters.AddWithValue("@desc", txtDescription.Text);
            cmd.Parameters.AddWithValue("@img", imgPath);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            ShowProducts();
        }

        // Bind Categories
        private void BindCategory()
        {
            SqlConnection con = new SqlConnection(strcon);
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Category", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddcat.DataSource = dt;
            ddcat.DataTextField = "CategoryName";
            ddcat.DataValueField = "CategoryId";
            ddcat.DataBind();
            ddcat.Items.Insert(0, new ListItem("--Select Category--", "0"));
        }

        // Load SubCategory based on Category
        protected void ddcat_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddsubcat.Enabled = true;
            SqlConnection con = new SqlConnection(strcon);
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM subCategory WHERE CategoryId=@cid", con);
            da.SelectCommand.Parameters.AddWithValue("@cid", ddcat.SelectedValue);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddsubcat.DataSource = dt;
            ddsubcat.DataTextField = "subCategoryName";
            ddsubcat.DataValueField = "subCategoryId";
            ddsubcat.DataBind();
        }

        // Show Products in Grid
        private void ShowProducts()
        {
            SqlConnection con = new SqlConnection(strcon);
            string query = @"SELECT p.ProductId, c.CategoryName, c.CategoryId, s.subCategoryName, s.subCategoryId, 
                             p.ProductName, p.Price, p.SellingPrice, p.Description, p.Image
                             FROM Product p
                             INNER JOIN Category c ON p.CategoryId = c.CategoryId
                             INNER JOIN subCategory s ON p.subCategoryId = s.subCategoryId";

            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gv_product.DataSource = dt;
            gv_product.DataBind();
        }

        // GridView RowDataBound - Bind dropdowns in Edit mode
        protected void gv_product_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gv_product.EditIndex == e.Row.RowIndex)
            {
                DropDownList ddcatEdit = (DropDownList)e.Row.FindControl("ddcatEdit");
                DropDownList ddsubcatEdit = (DropDownList)e.Row.FindControl("ddsubcatEdit");

                SqlConnection con = new SqlConnection(strcon);
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Category", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddcatEdit.DataSource = dt;
                ddcatEdit.DataTextField = "CategoryName";
                ddcatEdit.DataValueField = "CategoryId";
                ddcatEdit.DataBind();
                ddcatEdit.SelectedValue = DataBinder.Eval(e.Row.DataItem, "CategoryId").ToString();

                // SubCategory
                SqlCommand cmd = new SqlCommand("SELECT * FROM subCategory WHERE CategoryId=@cid", con);
                cmd.Parameters.AddWithValue("@cid", ddcatEdit.SelectedValue);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt2 = new DataTable();
                sda.Fill(dt2);

                ddsubcatEdit.DataSource = dt2;
                ddsubcatEdit.DataTextField = "subCategoryName";
                ddsubcatEdit.DataValueField = "subCategoryId";
                ddsubcatEdit.DataBind();
                ddsubcatEdit.SelectedValue = DataBinder.Eval(e.Row.DataItem, "subCategoryId").ToString();
            }
        }

        // GridView Edit, Cancel, Delete, Update
        protected void gv_product_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gv_product.EditIndex = e.NewEditIndex;
            ShowProducts();
        }

        protected void gv_product_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_product.EditIndex = -1;
            ShowProducts();
        }

        protected void gv_product_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gv_product.DataKeys[e.RowIndex].Value);
            SqlConnection con = new SqlConnection(strcon);
            SqlCommand cmd = new SqlCommand("DELETE FROM Product WHERE ProductId=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ShowProducts();
        }

        protected void gv_product_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gv_product.DataKeys[e.RowIndex].Value);
            GridViewRow row = gv_product.Rows[e.RowIndex];

            DropDownList ddcatEdit = (DropDownList)row.FindControl("ddcatEdit");
            DropDownList ddsubcatEdit = (DropDownList)row.FindControl("ddsubcatEdit");

            string pname = ((TextBox)row.FindControl("txtProductName")).Text;
            string price = ((TextBox)row.FindControl("txtPrice")).Text;
            string sprice = ((TextBox)row.FindControl("txtsellprice")).Text;
            string desc = ((TextBox)row.FindControl("txtDescription")).Text;

            SqlConnection con = new SqlConnection(strcon);
            SqlCommand cmd = new SqlCommand(@"UPDATE Product SET 
                                                CategoryId=@cid,
                                                subCategoryId=@sid,
                                                ProductName=@pname,
                                                Price=@price,
                                                SellingPrice=@sprice,
                                                Description=@desc
                                               WHERE ProductId=@id", con);
            cmd.Parameters.AddWithValue("@cid", ddcatEdit.SelectedValue);
            cmd.Parameters.AddWithValue("@sid", ddsubcatEdit.SelectedValue);
            cmd.Parameters.AddWithValue("@pname", pname);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@sprice", sprice);
            cmd.Parameters.AddWithValue("@desc", desc);
            cmd.Parameters.AddWithValue("@id", id);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            gv_product.EditIndex = -1;
            ShowProducts();
        }

        // Optional: Category dropdown change in Edit mode
        protected void ddcatEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddcatEdit = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddcatEdit.NamingContainer;
            DropDownList ddsubcatEdit = (DropDownList)row.FindControl("ddsubcatEdit");

            SqlConnection con = new SqlConnection(strcon);
            SqlCommand cmd = new SqlCommand("SELECT * FROM subCategory WHERE CategoryId=@cid", con);
            cmd.Parameters.AddWithValue("@cid", ddcatEdit.SelectedValue);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            ddsubcatEdit.DataSource = dt;
            ddsubcatEdit.DataTextField = "subCategoryName";
            ddsubcatEdit.DataValueField = "subCategoryId";
            ddsubcatEdit.DataBind();
        }
    }
}