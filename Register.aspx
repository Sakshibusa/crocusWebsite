<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="crocusProject.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <style>
        body {
            font-family: Arial, sans-serif;
            background: #f2f4f7;
        }

        .register-box {
            width: 400px;
            margin: 80px auto;
            padding: 30px;
            background: #ffffff;
            border-radius: 8px;
            box-shadow: 0px 0px 10px rgba(0,0,0,0.1);
        }

        .register-box h2 {
            text-align: center;
            margin-bottom: 20px;
            color: #2e7d32;
        }

        .register-box label {
            font-weight: bold;
        }

        .register-box input {
            width: 100%;
            padding: 10px;
            margin: 8px 0 15px 0;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .register-box .btn-register {
            width: 100%;
            padding: 10px;
            background: #2e7d32;
            border: none;
            color: white;
            font-size: 16px;
            border-radius: 5px;
            cursor: pointer;
        }

        .register-box .btn-register:hover {
            background: #1b5e20;
        }

        .register-box .link {
            text-align: center;
            margin-top: 15px;
        }

        .error {
            color: red;
            text-align: center;
        }
    </style>
</head>
<body>
   <form id="form1" runat="server">
        <div class="register-box">
            <h2>User Registration</h2>

            <asp:Label Text="Full Name" runat="server" />
            <asp:TextBox ID="txtFullName" runat="server" placeholder="Enter Full Name" />

            <asp:Label Text="Email" runat="server" />
            <asp:TextBox ID="txtEmail" runat="server" placeholder="Enter Email" />

            <asp:Label Text="Password" runat="server" />
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Enter Password" />

            <asp:Label Text="Confirm Password" runat="server" />
            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" placeholder="Confirm Password" />

            <asp:Label Text="Address" runat="server" />
            <asp:TextBox ID="textAddress" runat="server"  placeholder="Enter Address" />

             

               <asp:Label Text="Phone No" runat="server" />
            <asp:TextBox ID="textPhoneno" runat="server"  placeholder="Enter Phone no" />

               <!--<asp:Label Text="User Type" runat="server" /><br />
               <asp:DropDownList ID="dd1type" runat="server">
                   <asp:ListItem>Admin</asp:ListItem>
                   <asp:ListItem>User</asp:ListItem>
            </asp:DropDownList>-->


            <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn-register" OnClick="btnRegister_Click" />

            <asp:Label ID="lblMsg" runat="server" CssClass="error" />

            <div class="link">
                Already have an account?
                <a href="Login.aspx">Login</a>
            </div>
        </div>
    </form>
</body>
</html>
