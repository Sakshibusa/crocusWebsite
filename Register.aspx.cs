using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
namespace crocusProject
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {

            if (isFormValid())
            {
                string cs = ConfigurationManager.ConnectionStrings["db_crocus.mdf"].ConnectionString;

                using (SqlConnection con = new SqlConnection(cs))
                {
                    string query = @"INSERT INTO [tblUser]
                            (FullName, Email, Password, Address,PhoneNo,Status)
                            VALUES
                            (@FullName, @Email, @Password, @Address, @PhoneNo,@type)";

                    SqlCommand cmd = new SqlCommand(query, con);
                    string type = "User";

                    cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text); // hashing later
                    cmd.Parameters.AddWithValue("@Address", textAddress.Text);
                    //cmd.Parameters.AddWithValue("@Town", textTown.Text);
                    //cmd.Parameters.AddWithValue("@City", textCity.Text);
                    cmd.Parameters.AddWithValue("@PhoneNo", textPhoneno.Text);
                    cmd.Parameters.AddWithValue("@type", type);


                    con.Open();
                    cmd.ExecuteNonQuery();
                    clearForm();
                    con.Close();

                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = "Registration Successful!";
                }


            }
            else
            {
                Response.Write("<script>alert('Registration Not Successful !');</script>");
            }
            //lblMsg.Text = "Button working!";
            //if (txtPassword.Text != txtConfirmPassword.Text)
            //{
            //    lblMsg.Text = "Password and Confirm Password do not match";
            //    return;
            //}

            //string cs = ConfigurationManager.ConnectionStrings["db_crocus.mdf"].ConnectionString;

            //using (SqlConnection con = new SqlConnection(cs))
            //{
            //    string query = @"INSERT INTO [User]
            //                (FullName, Email, Password, Address, Town, City, PhoneNo)
            //                VALUES
            //                (@FullName, @Email, @Password, @Address, @Town, @City, @PhoneNo)";

            //    SqlCommand cmd = new SqlCommand(query, con);

            //    cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);
            //    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
            //    cmd.Parameters.AddWithValue("@Password", txtPassword.Text); // hashing later
            //    cmd.Parameters.AddWithValue("@Address", textAddress.Text);
            //    //cmd.Parameters.AddWithValue("@Town", textTown.Text);
            //    //cmd.Parameters.AddWithValue("@City", textCity.Text);
            //    cmd.Parameters.AddWithValue("@PhoneNo", textPhoneno.Text);

            //    con.Open();
            //    cmd.ExecuteNonQuery();
            //    con.Close();

            //    lblMsg.ForeColor = System.Drawing.Color.Green;
            //    lblMsg.Text = "Registration Successful!";
            //}
        
    }
        private Boolean isFormValid()
        {
            if (txtFullName.Text == "")
            {
                Response.Write("<script>alert('user name is required');</script>");
                txtFullName.Focus();
                return false;
            }else if (txtEmail.Text == "")
            {
                Response.Write("<script>alert('email is required');</script>");
                txtEmail.Focus();
                return false;
            }
            else if(txtPassword.Text == "")
            {
                Response.Write("<script>alert('password is required');</script>");
                txtPassword.Focus();
                return false;
            }
            else if (txtPassword.Text != txtConfirmPassword.Text)
            {
                Response.Write("<script>alert('Conform password and password not match');</script>");
                txtPassword.Focus();
                return false;
            }
            else if (textAddress.Text =="")
            {
                Response.Write("<script>alert('Address is requuired');</script>");
                textAddress.Focus();
                return false;
            }
            else if (textPhoneno.Text =="")
            {
                Response.Write("<script>alert('phone no is required');</script>");
                textPhoneno.Focus();
                return false;
            }
            return true;
        }
        private void clearForm()
        {
            txtFullName.Text=string.Empty;
            txtEmail.Text=string.Empty;     
            txtPassword.Text=string.Empty;
            txtConfirmPassword.Text=string.Empty;
            textAddress.Text=string.Empty;
            textPhoneno.Text=string.Empty;

        }

    }
}