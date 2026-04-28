<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="order.aspx.cs" Inherits="crocusProject.order"    MaintainScrollPositionOnPostBack="true"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  
    <style>
body { 
    margin:0; 
    font-family:Segoe UI; 
    background:#f5f5f5; 
}

/* Common Container */
.container { 
    width: 90%; 
    margin: 100px auto; 
    padding: 30px; 
}

/* Header */
.header { 
    background:white; 
    padding:10px; 
    box-shadow:0 2px 5px rgba(0,0,0,0.1); 
}

h2{ 
    text-align:center; 
}

/* Inputs (AddProducts mathi lidhu) */
input[type=text], textarea { 
    width:100%; 
    padding:8px; 
    margin-top:5px; 
}

/* Buttons */
button, input[type=submit]{ 
    width:100%; 
    padding:10px; 
    background:#4f6f3c; 
    color:white; 
    border:none; 
    margin-top:10px; 
    font-size:16px; 
    cursor:pointer; 
}

/* Table/Grid */
.grid, .table {
    background: white;
    border-radius: 10px;
    padding: 10px;
}

/* Status Colors */
.status-paid { color: green; }
.status-pending { color: orange; }
.status-cancel { color: red; }
.status-delivered { color: green; }

/* Search Box */
.search-box {
    margin: 20px 0;
}

.search-box input {
    padding: 10px;
    width: 250px;
    border-radius: 20px;
    border: 1px solid #ccc;
}

/* Tabs */
.tabs span {
    margin-right: 20px;
    cursor: pointer;
    color: #555;
}

.tabs span.active {
    color: #5a67ff;
    font-weight: bold;
}

/* Invoice */
.invoice-btn {
    color: blue;
    cursor: pointer;
}

.status-dropdown {
    padding: 6px 10px;
    border-radius: 20px;
    border: none;
    font-weight: 600;
    cursor: pointer;
}

/* Colors */
.status-paid {
    background: #d4edda;
    color: #155724;
}

.status-pending {
    background: #fff3cd;
    color: #856404;
}

.status-cancel {
    background: #f8d7da;
    color: #721c24;
}

.status-delivered {
    background: #d1ecf1;
    color: #0c5460;
}

.search-box {
    display: flex;
    gap: 10px;
    justify-content: center;
    margin-bottom: 20px;
}

.search-box input {
    padding: 10px;
    width: 250px;
    border-radius: 20px;
    border: 1px solid #ccc;
}

.search-box select {
    padding: 10px;
    border-radius: 20px;
}

.search-box button {
    padding: 10px 20px;
    border-radius: 20px;
    background: #4f6f3c;
    color: white;
    border: none;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="header"><h2>Order History</h2></div>

   <br /><br /><br />
    <div class="search-box">
    <asp:TextBox ID="txtSearch" runat="server" placeholder="Search OrderId or Product"></asp:TextBox>

    <asp:DropDownList ID="ddFilterStatus" runat="server">
        <asp:ListItem Value="">All Status</asp:ListItem>
        <asp:ListItem>Order Placed</asp:ListItem>
        <asp:ListItem>Processing</asp:ListItem>
        <asp:ListItem>Shipped</asp:ListItem>
        <asp:ListItem>Delivered</asp:ListItem>
    </asp:DropDownList>

    <asp:Button ID="btnSearch" runat="server" Text="Search"
        OnClick="btnSearch_Click" />
</div>
    <br /><br />
        <asp:GridView ID="gvOrders" runat="server" DataKeyNames="OrderId"  AutoGenerateColumns ="False" Width="100%" style="text-align:center;" OnRowDataBound="gvOrders_RowDataBound" >

    <Columns>

        <asp:TemplateField HeaderText="Id">
            <ItemTemplate>
                #<%# Eval("OrderId") %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Product Name">
    <ItemTemplate>
       
            <div style="display:flex; justify-content:center; align-items:center;">
 <img src='<%# ResolveUrl(Eval("Image").ToString()) %>' 
     style="width:100px;height:100px;border-radius:8px;" />
        </div>
    </ItemTemplate>
</asp:TemplateField>

        <asp:TemplateField HeaderText="Product Name">
            <ItemTemplate>
                <%# Eval("ProductName") %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Payment">
            <ItemTemplate>
                <%# Eval("PaymentMode") %>
            </ItemTemplate>
        </asp:TemplateField>

      <asp:TemplateField HeaderText="Status">
    <ItemTemplate>
        <asp:DropDownList ID="ddStatus" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddStatus_SelectedIndexChanged"
            CssClass="status-dropdown">

            <asp:ListItem>Order Placed</asp:ListItem>
            <asp:ListItem>Processing</asp:ListItem>
            <asp:ListItem>Shipped</asp:ListItem>
            <asp:ListItem>Delivered</asp:ListItem>

        </asp:DropDownList>
    </ItemTemplate>
</asp:TemplateField>

        <asp:TemplateField HeaderText="Total">
            <ItemTemplate>
                Rs. <%# Eval("TotalAmount") %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Invoice">
            <ItemTemplate>
                📥
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Order Date">
    <ItemTemplate>
        <%# Eval("OrderDate", "{0:dd-MM-yyyy}") %>
    </ItemTemplate>
</asp:TemplateField>
    </Columns>
</asp:GridView>



</asp:Content>
