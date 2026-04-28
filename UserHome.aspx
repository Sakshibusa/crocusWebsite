<%@ Page Title="" Language="C#" MasterPageFile="~/navFoot.Master" AutoEventWireup="true" CodeBehind="UserHome.aspx.cs" Inherits="crocusProject.UserHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        

  

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid p-0">
        <img src="/Images/background.png" class="img-fluid w-100" style="height: 350px; object-fit: cover;" />
    </div>
    <br />
    <div style="text-align: center" class="py-2">
        <h5>The joy is here with 20% off all plants, outdoor pots, summer bulbs and more.</h5>
        <small>Spring's just a few weeks away</small>
    </div>
    <br />
    <div class="container my-5 px-5">
        <div class="row g-4">





            <asp:Repeater ID="repet" runat="server">
                <ItemTemplate>
                    <div class="col-lg-3 col-md-6 col-sm-12 mb-4">
                        <!-- Make the whole card clickable -->
                        <a href='ProductDetails.aspx?ProductID=<%# Eval("ProductID") %>' style="text-decoration: none; color: inherit; display: block;">
                            <div class="card border-0 shadow-sm position-relative overflow-hidden"
                                style="height: 300px; background-image: url('<%# ResolveUrl(Eval("Image").ToString()) %>'); background-size: cover; background-position: center; border-radius: 12px;">

                                <!-- Discount Badge -->
                                <div class="position-absolute top-0 start-0 m-3 bg-white px-3 py-2 rounded shadow-sm">
                                    Up to <b class="fs-5 text-success">20%</b> off
                                </div>

                                <!-- Card Content -->
                                <div class="position-absolute bottom-0 start-0 m-3 bg-white p-3 rounded shadow"
                                    style="width: 70%; text-align: left;">

                                    <small class="text-muted">
                                        <%# Eval("SellingPrice") %>
                                    </small>

                                    <h6 class="fw-semibold mt-1">
                                        <%# Eval("ProductName") %>
                                    </h6>

                                </div>
                            </div>
                        </a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

</asp:Content>
