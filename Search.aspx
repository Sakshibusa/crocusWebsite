<%@ Page Title="" Language="C#" MasterPageFile="~/navFoot.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="crocusProject.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Search Results</h3>

<asp:Repeater ID="rptProducts" runat="server">
    <ItemTemplate>

        <div style="border:1px solid #ddd; padding:10px; margin:10px;">
            <h4><%# Eval("ProductName") %></h4>
            <p>₹ <%# Eval("Price") %></p>
        </div>

    </ItemTemplate>
</asp:Repeater>
</asp:Content>
