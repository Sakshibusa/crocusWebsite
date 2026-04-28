<%@ Page Title="" Language="C#" MasterPageFile="~/navFoot.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="crocusProject.Cart"  EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        body {
            background-color: #f6f6f6;
        }

        .card {
            border-radius: 12px;
        }

        h6 {
            font-weight: 600;
        }
        .remove-btn {
    color: #3d4b2f;
    font-size: 20px;
    text-decoration: none;
    font-weight: bold;
    cursor: pointer;
}

.remove-btn:hover {
    color: darkred;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="container mt-5" style="max-width: 1100px;">
        <div class="row">

            <!-- LEFT SIDE -->
            <div class="col-md-8">

<asp:Repeater ID="rptCart" runat="server" OnItemCommand="rptCart_ItemCommand">
                    <itemtemplate>

                        <div class="card p-3 mb-3 shadow-sm">
                            <div class="d-flex">

                                <!-- Image -->
                                <img src='<%# ResolveUrl(Eval("Image").ToString()) %>'
                                    style="width: 120px; height: 120px; object-fit: cover;"
                                    class="rounded me-3" />

                                <!-- Details -->
                                <div class="flex-grow-1">
                                    <h6><%# Eval("ProductName") %></h6>
                                      <div class="">
                                         <div>₹ <%# Eval("Price") %></div>
                                      </div>
                                    <small class="text-muted">Qty: <%# Eval("Quantity") %></small>
                                     
                                    <!-- Qty Box -->
                                    <div class="mt-2 border p-1 d-inline-flex align-items-center">
                                        <asp:Button ID="btnMinus" runat="server" Text="-"    CssClass="btn btn-light btn-sm" CommandName="minus"   CommandArgument='<%# Eval("ProductId") %>' />

                                        <span class="mx-2"><%# Eval("Quantity") %></span>

                                        <asp:Button ID="btnPlus" runat="server" Text="+"   CssClass="btn btn-light btn-sm"  CommandName="plus" CommandArgument='<%# Eval("ProductId") %>' />
                                    </div>
                                </div>

                                <!-- Price -->
                             
                                                     <asp:LinkButton ID="btnRemove" runat="server"  CssClass="remove-btn" CommandArgument='<%# Eval("ProductId") %>' OnClick="btnRemove_Click">x</asp:LinkButton>
                            </div>
                        </div>
                     
                    </itemtemplate>
                </asp:Repeater>

            </div>

            <!-- RIGHT SIDE (SUMMARY) -->
            <div class="col-md-4">

                <div class="card p-4 shadow-sm">

                    <h5>Summary</h5>
                    <hr />

                    <div class="d-flex justify-content-between">
                        <span>Sub-total</span>
                        <span>₹
                            <asp:Label ID="lblSubtotal" runat="server"></asp:Label>
                        </span>
                    </div>

                  <!--  <div class="text-success mt-2">50% off - ₹ 100</div>
                    <div class="text-success">30% off - ₹ 50</div>-->

                    <hr />

                    <div class="d-flex justify-content-between">
                        <strong>Total</strong>
                        <strong>₹
                            <asp:Label ID="lblTotal" runat="server"></asp:Label>
                        </strong>
                    </div>

                    <asp:Button ID="btnCheckout" runat="server" Text="Checkout now" CssClass="btn w-100 mt-3 text-white" Style="background-color: #3d4b2f;" OnClick="btnCheckOut_Click" />

                </div>

            </div>

        </div>
    </div>



</asp:Content>
