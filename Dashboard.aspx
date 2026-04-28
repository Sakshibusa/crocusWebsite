<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="crocusProject.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
        .dashboard-container
        { display: flex; justify-content: space-around; margin-top: 50px; }
        .card 
        {
            background: #4f6f3c; color: white; padding: 30px; width: 200px; text-align: center; border-radius: 10px;
            box-shadow: 0 2px 5px rgba(0,0,0,0.2);
        }
        .card h2
        { margin: 0; font-size: 40px; }
        .card p
        { margin: 5px 0 0; font-size: 18px;color:black; }
        
/* Heading */
h2{
    text-align:center;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


        <div class="header">
    <h2>Dashboard</h2>
</div>    <br /><br /><br />
    <div class="dashboard-container">
        <div class="card">
            <a href="AddProducts.aspx" style="text-decoration:none;">
            <h2><asp:Label ID="lblProducts" runat="server" Text="0"></asp:Label></h2>
            <p>Products</p>
            </a>
        </div>
        <div class="card">
            <a href="AddCategory.aspx" style="text-decoration:none;">
            <h2><asp:Label ID="lblCategories" runat="server" Text="0"></asp:Label></h2>
            <p>Categories</p>
            </a>
        </div>
        <div class="card">
            <a href="AddSubCategory.aspx" style="text-decoration:none;">
            <h2><asp:Label ID="lblSubCategories" runat="server" Text="0"></asp:Label></h2>
            <p>Subcategories</p>
            </a>
        </div>
         <div class="card">
     <a href="order.aspx" style="text-decoration:none;">
     <h2><asp:Label ID="lblorder" runat="server" Text="0"></asp:Label></h2>
     <p>Orders</p>
     </a>
 </div>
    </div>
</asp:Content>
