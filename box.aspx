<%@ Page Title="" Language="C#" MasterPageFile="~/navFoot.Master" AutoEventWireup="true" CodeBehind="box.aspx.cs" Inherits="crocusProject.box" %>

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

                            <img src="p4.png" class="img-fluid rounded" />

                        </div>


                        <!-- Product Details -->

                        <div class="col-md-6">

                            <h3>Peter England Blue Casual Shirt</h3>

                            <p class="text-muted"><del>Rs.1200</del> (210 OFF)</p>

                            <h4 class="text-dark">Rs.1000</h4>

                            <hr>


                           

                            <br>

                            <button class="btn btn-warning">ADD TO CART</button>

                            <hr>

                            <h5>Description</h5>

                            <p>
                                A pair of sneakers and slim denim are the perfect additions to this blue piece when you're going outside for a day in the park.
                            </p>

                            <h6>Material & Care</h6>

                            <p>100% cotton Machine wash</p>

                        </div>

                    </div>

                </div>

            </div>

        </div>

    </div>

</asp:Content>
