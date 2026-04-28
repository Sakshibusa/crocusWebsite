<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="crocusProject.list" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        

        
body{
    font-family: Arial;
    background:#f5f5f5;
    text-align:center;
}
/* Image */
.product-img{
    width:100%;
    height:200px;
    object-fit:cover;
}
.category-grid {
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    gap: 20px;
    max-width: 900px;
    margin: 40px auto;
}

.card {
    position: relative;
/*    width:100px;*/
    height: 300px;
    background-size: cover;
    background-position: center;
    border-radius: 12px;
    overflow: hidden;
    transition: transform 0.3s ease;
}

    

.badge {
    position: absolute;
    top: 15px;
    left: 15px;
    background: rgba(255,255,255,0.9);
    padding: 10px 14px;
    border-radius: 8px;
    font-size: 14px;
    color: #3e4a2f;
}

    .badge b {
        font-size: 22px;
        color: #3e4a2f;
    }

.card-content {
    position: absolute;
    bottom: 20px;
    left: 20px;
    background: white;
    padding: 18px;
    border-radius: 10px;
    width: 70%;
}
.card-content {
    text-align: left;
}

    .card-content h4 {
        margin: 5px 0;
        font-weight:600;
        font-size:16px;
    }

    .card-content a {
        text-decoration: none;
        color: black;
        font-weight: 100;
        border-bottom: 1px solid black;
    }

    </style>
</head>
<body>
    <form id="form1" runat="server">
     
  <div class="category-grid">

<asp:Repeater ID="repet" runat="server">
<ItemTemplate>

<div class="card" style="background-image:url('<%# ResolveUrl(Eval("Image").ToString()) %>')">
    <div class="badge">
        Up to <b><%# Eval("Price") %>%</b> off
    </div>

    <div class="card-content">
        <small><%# Eval("SellingPrice") %></small>
        <h4><%# Eval("Description") %></h4>
    </div>
</div>

</ItemTemplate>
</asp:Repeater>

</div>
    </form>
</body>
</html>
