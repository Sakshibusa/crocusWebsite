<%@ Page Title="" Language="C#" MasterPageFile="~/navFoot.Master" AutoEventWireup="true" CodeBehind="CheckOut.aspx.cs" Inherits="crocusProject.CheckOut" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <style>
/*        body {
            font-family: Arial;
            background: #f5f5f5;
        }
*/
  body {
    font-family: 'Poppins', sans-serif;
}
        .container {
            max-width: 1100px;
            margin: 40px auto;
            display: flex;
            gap: 30px;
        }

        .left, .right {
            background: #fff;
            padding: 20px;
            border-radius: 12px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.1);
        }

        .left { flex: 2; }
        .right { flex: 1; }

        h2 {
            margin-bottom: 15px;
        }

        input, textarea {
            width: 100%;
            padding: 10px;
            margin-bottom: 12px;
            border-radius: 6px;
            border: 1px solid #ccc;
        }
.payment-row input { width: auto }

        .btn {
            width: 100%;
            padding: 12px;
            background: #4a5d3a;
            color: #fff;
            border: none;
            border-radius: 8px;
/*            cursor: pointer;*/
        }

        .summary-item {
/*            display: flex;
            justify-content: space-between;*/
            margin-bottom: 10px;
        }

        .total {
            font-weight: bold;
            font-size: 18px;
        }
        h5 {
    font-size: 17px;
}
        small{
            font-size:12px;
        }
      
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
<div class="container">

    <!-- LEFT: BILLING -->
    <div class="left">
        <h5>Billing Details</h5>

        <asp:TextBox ID="txtName" runat="server" placeholder="Full Name"></asp:TextBox>
        <asp:TextBox ID="txtPhone" runat="server" placeholder="Phone"></asp:TextBox>
        <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" placeholder="Address"></asp:TextBox>

        <h5>Payment</h5>
        
      
<div class="payment-row">
    <input type="radio" name="pay" value="COD" checked />
    <small>Cash on Delivery</small>
</div>

<div class="payment-row">
    <input type="radio" name="pay" value="UPI" disabled />
    <small>UPI</small>
</div>

        <br /><br />

        <asp:Button ID="btnPlaceOrder" runat="server" Text="Place Order" CssClass="btn" OnClick="btnPlaceOrder_Click" />
    </div>

    <!-- RIGHT: SUMMARY -->
    <div class="right">
        <h5>Order Summary :</h5>

        <asp:Repeater ID="rptSummary" runat="server">
            <ItemTemplate>
                <div class="summary-item">
                    <span><%# Eval("Name") %> x <%# Eval("Qty") %></span>:
                    <span>₹ <%# Eval("Price") %></span>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <hr />

        <div class="summary-item total">
                    <h5>Total : </h5>
            <asp:Label ID="lblTotal" runat="server"></asp:Label>
        </div>
    </div>

</div>
</asp:Content>
