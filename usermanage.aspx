<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="usermanage.aspx.cs" Inherits="crocusProject.usermanage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
    /* Header */
.header { 
    background:white; 
    padding:10px; 
    box-shadow:0 2px 5px rgba(0,0,0,0.1); 
}

h2{ 
    text-align:center; 
}
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="header"><h2>User History</h2></div>
    <br /><br /><br /><br />
    <center>
    <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="false" Width="100%" style="text-align:center;"    CssClass="table table-bordered table-striped">

    <Columns>
        <asp:BoundField DataField="Id" HeaderText="ID" />
        <asp:BoundField DataField="FullName" HeaderText="Name" />
        <asp:BoundField DataField="Email" HeaderText="Email" />
        <asp:BoundField DataField="Password" HeaderText="Password" />
        <asp:BoundField DataField="Address" HeaderText="Address" />
        <asp:BoundField DataField="PhoneNo" HeaderText="Phone" />
        <asp:BoundField DataField="Status" HeaderText="Status" />
    </Columns>

</asp:GridView>
        </center>

</asp:Content>
