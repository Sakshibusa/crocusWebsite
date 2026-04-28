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
    public partial class login : System.Web.UI.Page
    {
        String strcon = ConfigurationManager.ConnectionStrings["db_crocus.mdf"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["txtEmail"] != null && Request.Cookies["txtPassword"] != null)
                {
                    txtEmail.Text = Request.Cookies["txtEmail"].Value;
                    txtPassword.Text = Request.Cookies["txtPassword"].Value;
                    //CheckBox1.Checked = true;

                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {


            using (SqlConnection con = new SqlConnection(strcon))
            {
                SqlCommand cmd = new SqlCommand("Select * from tblUser where Email=@Email AND Password=@Password", con);
                con.Open();

                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);

                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    if (CheckBox1.Checked)
                    {
                        Response.Cookies["txtEmail"].Value = txtEmail.Text;
                        Response.Cookies["txtPassword"].Value = txtPassword.Text;

                        Response.Cookies["txtEmail"].Expires = DateTime.Now.AddDays(10);

                        Response.Cookies["txtPassword"].Expires = DateTime.Now.AddDays(10);

                    }
                    else
                    {
                        Response.Cookies["txtEmail"].Expires = DateTime.Now.AddDays(-1);

                        Response.Cookies["txtPassword"].Expires = DateTime.Now.AddDays(-1);
                    }

                    string Utype = dt.Rows[0][6].ToString().Trim();

                    // ✅ IMPORTANT: UserId lo (maan ke column 0 hai)
                    string userId = dt.Rows[0][0].ToString();
                    string email = dt.Rows[0]["Email"].ToString();
                    string name = dt.Rows[0][1].ToString();
                    if (Utype == "User")
                    {
                        Session["UserId"] = userId;   // 🔥 important
                        Session["Email"] = email;     // optional
                        Session["Username"] = name;

                        Response.Redirect("~/UserHome.aspx");
                    }
                    else if (Utype == "Admin")
                    {
                        Session["AdminId"] = userId;
                        Session["Adminname"] = email;

                        Response.Redirect("AddProducts.aspx");
                    }
                
            }
                else
                {
                    lblError.Text = "Invalid Username and password";
                }

                //Response.Write("<script> alert('Login Successfully done');  </script>");
                clr();
                con.Close();

                //lblMsg.Text = "Registration Successfully done";
                //lblMsg.ForeColor = System.Drawing.Color.Green;

                //if (txtEmail.Text == "" || txtPassword.Text == "")
                //{
                //    lblMsg.Text = "Email and Password required!";
                //    lblMsg.ForeColor = System.Drawing.Color.Red;
                //    return;
                //}

                //SqlConnection con = new SqlConnection(
                //    ConfigurationManager.ConnectionStrings["db_crocus.mdf"].ConnectionString
                //);

                //string query = "SELECT UserId, FullName FROM [tblUser] WHERE Email=@Email AND Password=@Password";

                //SqlCommand cmd = new SqlCommand(query, con);
                //cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                //cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                //con.Open();
                //SqlDataReader dr = cmd.ExecuteReader();

                //if (dr.Read())
                //{
                //    // LOGIN SUCCESS
                //    Session["UserId"] = dr["UserId"].ToString();
                //    Session["FullName"] = dr["FullName"].ToString();

                //    Response.Redirect("Dashboard.aspx"); // or Home.aspx
                //}
                //else
                //{
                //    lblMsg.Text = "Invalid Email or Password!";
                //    lblMsg.ForeColor = System.Drawing.Color.Red;
                //}

                //con.Close();

            }
        }

        private void clr()
        {
            txtPassword.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtEmail.Focus();

        }
    }
}