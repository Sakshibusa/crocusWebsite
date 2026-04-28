<%@ Page Title="" Language="C#" MasterPageFile="~/navFoot.Master" AutoEventWireup="true" CodeBehind="MyOrders.aspx.cs" Inherits="crocusProject.MyOrders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container mt-5">
    <h3>My Orders</h3>
    <hr />

    <asp:Repeater ID="rptOrders" runat="server">
        <ItemTemplate>
            <div style="border:1px solid #ddd; padding:15px; margin-bottom:15px; border-radius:10px;">

                <b>Order ID:</b> <%# Eval("OrderId") %><br />
                <b>Date:</b> <%# Eval("OrderDate","{0:dd MMM yyyy}") %><br />
                <b>Total:</b> ₹ <%# Eval("TotalAmount") %><br />
                <b>Status:</b> <%# Eval("Status") %><br /><br />

                <asp:Button ID="btnView" runat="server" Text="View Details"
                    CssClass="btn btn-success"
                    CommandArgument='<%# Eval("OrderId") %>'
                    OnClick="btnView_Click" />

            </div>
        </ItemTemplate>
    </asp:Repeater>

</div>
</asp:Content>
