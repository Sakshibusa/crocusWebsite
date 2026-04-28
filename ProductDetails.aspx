<%@ Page Title="" Language="C#" MasterPageFile="~/navFoot.Master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="crocusProject.ProductDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

        <style>
        .page-center {
            height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
/*            background: #f5f5f5;*/
        }

        .product-box {
            background: white;
            padding: 30px;
            border-radius: 8px;
/*            box-shadow: 0 0 10px rgba(0,0,0,0.1);*/
        }
       .qty-box {
    width: 120px;        /* પહેલા 250px હતું → હવે નાનું */
    border: 1px solid #ddd;
    border-radius: 5px;
    padding: 4px 8px;   /* padding ઓછું */
    background: #fff;
}

.qty-btn {
    font-size: 20px;     /* પહેલા 30px હતું */
    color: #999;
    text-decoration: none;
    font-weight: bold;
}

.qty-text {
    font-size: 16px;     /* number પણ નાનું */
    font-weight: bold;
    color: #333;
}

    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="page-center">
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-10 product-box">
                <div class="row">

                    <!-- Image -->
                    <div class="col-md-6">

                        <asp:Image ID="imgProduct" runat="server" CssClass="img-fluid rounded" Height="400px" />
                    </div>

                    <!-- Product Details -->
                    <div class="col-md-6">

                        <h3>
                            <asp:Label ID="lblName" runat="server"></asp:Label>
                        </h3>

                        <p class="text-muted">
                            <del>Rs.
                                <asp:Label ID="lblPrice" runat="server"></asp:Label>

                            </del>
                        </p>

                        <h4 class="text-dark">
                            Rs.
                            <asp:Label ID="lblSellingPrice" runat="server"></asp:Label>
                        </h4>
                       <asp:Button ID="btnWishlist" runat="server" Text="❤️ Add to Wishlist" OnClick="btnWishlist_Click" />
                        <hr>
                        <div class="d-flex align-items-center">
                        <div class="qty-box d-flex align-items-center justify-content-between me-3">

    <asp:LinkButton ID="btnMinus" runat="server" CssClass="qty-btn" OnClick="btnMinus_Click">-</asp:LinkButton>

    <asp:Label ID="txtQty" runat="server" Text="1" CssClass="qty-text"></asp:Label>

    <asp:LinkButton ID="btnPlus" runat="server" CssClass="qty-btn" OnClick="btnPlus_Click" >+</asp:LinkButton>

</div>
                            <asp:HiddenField ID="hfProductId" runat="server" Value='<%# Eval("ProductId") %>' />

<asp:Button ID="btnAdd" runat="server" Text="Add to Cart" class="btn btn-success" OnClick="btnAdd_Click" />
                            
                       <!-- <button class="btn btn-warning">ADD TO CART</button>-->
</div>
                        <hr>

                        <h5>Description</h5>
                        <p>
                            <asp:Label ID="lblDescription" runat="server"></asp:Label>
                        </p>

                       

                    </div>

                </div>
            </div>
        </div>
    </div>
</div>


</asp:Content>
