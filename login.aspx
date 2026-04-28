<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="crocusProject.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background: #f2f4f7;
        }

        .login-box {
            width: 350px;
            margin: 100px auto;
            padding: 30px;
            background: #ffffff;
            border-radius: 8px;
            box-shadow: 0px 0px 10px rgba(0,0,0,0.1);
        }

        .login-box h2 {
            text-align: center;
            margin-bottom: 20px;
            color: #2e7d32;
        }

        .login-box label {
            font-weight: bold;
        }

        .login-box input {
            width: 100%;
            padding: 10px;
            margin: 8px 0 15px 0;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .login-box .btn-login {
            width: 100%;
            padding: 10px;
            background: #2e7d32;
            border: none;
            color: white;
            font-size: 16px;
            border-radius: 5px;
            cursor: pointer;
        }

        .login-box .btn-login:hover {
            background: #1b5e20;
        }

        .login-box .link {
            text-align: center;
            margin-top: 15px;
        }

        .error {
            color: red;
            text-align: center;
        }



        .login-box input[type="checkbox"] {
    width: auto;
    margin: 0;
}


    </style>
</head>
<body>
    <form id="form1" runat="server">
       

        <div class="login-box">
            <h2>User Login</h2>

            <asp:Label Text="Email" runat="server" />
            <asp:TextBox ID="txtEmail" runat="server" placeholder="Enter Email" />

            <asp:Label Text="Password" runat="server" />
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Enter Password" />
            <div style="display:flex; align-items:center; gap:8px;">
             <asp:CheckBox ID="CheckBox1" runat="server" Text="Remember me" />
                </div>
           <!--  <asp:Label ID="Label3" CssClass =" control-label " runat="server" Text="Remember me"></asp:Label>-->

            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn-login" OnClick="btnLogin_Click"  />

           <!-- <asp:Label ID="lblMsg" runat="server" CssClass="error" />-->
             <asp:Label ID="lblError" CssClass ="text-danger " runat="server" ></asp:Label>

            <div class="link">
                Don’t have an account?
                <a href="Register.aspx">Register</a>
            </div>
        </div>
 
    </form>
</body>
</html>
