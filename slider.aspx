<%@ Page Title="" Language="C#" MasterPageFile="~/navFoot.Master" AutoEventWireup="true" CodeBehind="slider.aspx.cs" Inherits="crocusProject.slider" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-5">

<!-- Slider Wrapper -->
<div class="d-flex overflow-auto gap-4 pb-3" style="scroll-behavior:smooth;">

<asp:Repeater ID="repet" runat="server">
<ItemTemplate>

<div style="min-width:300px;">

<div class="card border-0 shadow-sm position-relative overflow-hidden"
     style="height:300px;
     background-image:url('<%# ResolveUrl(Eval("Image").ToString()) %>');
     background-size:cover;
     background-position:center;
     border-radius:12px;">

    <!-- Discount -->
    <div class="position-absolute top-0 start-0 m-3 bg-white px-3 py-2 rounded shadow-sm">
        Up to <b class="fs-5 text-success"><%# Eval("Price") %>%</b> off
    </div>

    <!-- Content -->
    <div class="position-absolute bottom-0 start-0 m-3 bg-white p-3 rounded shadow"
         style="width:70%; text-align:left;">

        <small class="text-muted">
            <%# Eval("SellingPrice") %>
        </small>

        <h6 class="fw-semibold mt-1">
            <%# Eval("Description") %>
        </h6>

    </div>

</div>

</div>

</ItemTemplate>
</asp:Repeater>

</div>
</div>
</asp:Content>
