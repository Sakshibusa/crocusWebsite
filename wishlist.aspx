<%@ Page Title="" Language="C#" MasterPageFile="~/navFoot.Master" AutoEventWireup="true" CodeBehind="wishlist.aspx.cs" Inherits="crocusProject.wishlist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
.wishlist-container {
    padding: 40px 20px;
}

.wishlist-title {
    font-size: 28px;
    
    margin-bottom: 20px;
}

.wishlist-card {
    border: 1px solid #eee;
    border-radius: 10px;
    padding: 15px;
    transition: 0.3s;
    background: #fff;
}

.wishlist-card:hover {
    box-shadow: 0 4px 15px rgba(0,0,0,0.1);
}

.product-img {
    height: 200px;
    object-fit: cover;
    border-radius: 8px;
}

.price {
    font-weight: bold;
    color: #333;
}

.old-price {
    text-decoration: line-through;
    color: #999;
}

.btn-cart {
    background: #2d4d2f;
    color: #fff;
}

.btn-remove {
    color: red;
    border: none;
    background: none;
}
.btn-cart {
    background: #2d4d2f;
    color: #fff;
    border: none;
    transition: 0.3s;
}

.btn-cart:hover {
    background: #1f3a22;
    color: #fff !important;
}

</style>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container wishlist-container">

    <div class="wishlist-title"> My Wishlist</div>

    <asp:Repeater ID="rptWishlist" runat="server">
        <ItemTemplate>

            <div class="row mb-4 wishlist-card align-items-center">

                <!-- Image -->
                <div class="col-md-3 text-center">
                    <img src='<%# ResolveUrl(Eval("Image").ToString()) %>' class="img-fluid product-img" />
                </div>

                <!-- Details -->
                <div class="col-md-5">
                    <h5><%# Eval("ProductName") %></h5>

                    <p class="old-price">Rs. <%# Eval("Price") %></p>
                    <p class="price">Rs. <%# Eval("SellingPrice") %></p>
                </div>

                <!-- Actions -->
                <div class="col-md-4 text-end">

                    <asp:Button ID="btnAddCart" runat="server" Text="Add to Cart"
                        CssClass="btn btn-cart me-2"
                        CommandArgument='<%# Eval("ProductID") %>'
                        OnClick="btnAddCart_Click" />

                    <asp:Button ID="btnRemove" runat="server" Text="Remove"
                        CssClass="btn-remove"
                        CommandArgument='<%# Eval("ProductID") %>'
                        OnClick="btnRemove_Click" />

                </div>

            </div>

        </ItemTemplate>
    </asp:Repeater>

</div>
   
</asp:Content>
