<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="crocusProject.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>

        .navbar-brand{
    font-size: 30px !important;
    letter-spacing: 4px;
    color: #3e4a2f !important;
}
        .navbar-nav {
    display: flex;
    justify-content: center;
    gap: 15px;
    padding: 15px;
    font-size: 16px;
}
        /* menu */
    .navbar-item .nav-link{
    color:#333 !important;
    font-weight:500;
}
    .dropdown-toggle::after {
    display: none !important;
}
    .nav-item.dropdown:hover .dropdown-menu{
    display:block;
}
        .dropdown-menu{
    border: none !important;
    /* shadow pan remove thase */
    /*       left: -80px;*/
}
        /* only last menu dropdown right side thi open thase */
.navbar-nav .nav-item:last-child .dropdown-menu {
    right: 0 !important;
    left: auto !important;
}
</style>


    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
              <nav class="navbar navbar-expand-lg">
  <div class="container-fluid">
    <a class="navbar-brand " href="#">CROCUS</a>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNavDropdown" aria-controls="navbarNavDropdown" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarNavDropdown">
     <ul class="navbar-nav ms-auto">

<asp:Repeater ID="rptCategory" runat="server" OnItemDataBound="rptCategory_ItemDataBound">
<ItemTemplate>

<li class="nav-item dropdown">

<a class="nav-link dropdown-toggle" href="#" data-bs-toggle="dropdown">
<%# Eval("CategoryName") %>
</a>

<ul class="dropdown-menu">
<asp:Repeater ID="rptSubCategory" runat="server">
<ItemTemplate>
<li>
<a class="dropdown-item" href="#">
<%# Eval("SubCategoryName") %>
</a>
</li>
</ItemTemplate>
</asp:Repeater>
</ul>

</li>

</ItemTemplate>
</asp:Repeater>

</ul>
    </div>
  </div>
</nav>

    </form>
</body>
</html>