<%@ Page Title="" Language="C#" MasterPageFile="~/navFoot.Master" AutoEventWireup="true" CodeBehind="Success.aspx.cs" Inherits="crocusProject.Success" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <style>
        body {
            margin: 0;
            font-family: Arial;
            background: #f3efe9;
        }

        .container {
            width: 90%;
            max-width: 1100px;
            margin: 60px auto;
            background: white;
            border-radius: 15px;
            display: flex;
            padding: 30px;
            box-shadow: 0 5px 20px rgba(0,0,0,0.1);
        }

        .left {
            flex: 1;
            padding: 20px;
        }

        .right {
            flex: 1;
            background: #fafafa;
            padding: 20px;
            border-radius: 10px;
        }

        h1 {
            font-size: 32px;
        }

        .btn {
            margin-top: 20px;
            padding: 12px 25px;
            background: #2d4d2f;
            color: white;
            border: none;
            border-radius: 25px;
            cursor: pointer;
        }

        .summary-title {
            font-size: 20px;
            font-weight: bold;
            margin-bottom: 15px;
        }

        .row {
            display: flex;
            justify-content: space-between;
            margin: 8px 0;
        }

        .total {
            font-size: 20px;
            font-weight: bold;
            margin-top: 15px;
        }

        .product {
            display: flex;
            align-items: center;
            margin: 10px 0;
        }

        .product img {
            width: 60px;
            height: 60px;
            margin-right: 10px;
            border-radius: 8px;
        }
        .track-container {
    margin: 30px 0;
    width: 100%;
}

.track-line {
    position: relative;
    height: 6px;
    background: #ddd;
    border-radius: 10px;
}

.progress {
    position: absolute;
    height: 6px;
    background: #6a8f4e;
    width: 0%;
    border-radius: 10px;
    transition: 0.5s;
}

.steps {
    display: flex;
    justify-content: space-between;
    margin-top: 10px;
}

.step {
    text-align: center;
    width: 25%;
}

.circle {
    width: 14px;
    height: 14px;
    background: #ccc;
    border-radius: 50%;
    margin: auto;
}

.step.active .circle {
    background: #6a8f4e;
}

.step p {
    font-size: 12px;
    margin-top: 5px;
}   
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">

        <!-- LEFT SIDE -->
        <div class="left">
            <h1>Thank you for your purchase!</h1>

            <p>Your order will be processed shortly.</p>

            <br />

            <asp:Label ID="lblOrderId" runat="server"></asp:Label><br />
            <asp:Label ID="lblDate" runat="server"></asp:Label><br />
            <asp:Label ID="lblPayment" runat="server"></asp:Label>

            <br />


                    <div class="track-container">

    <div class="track-line">
        <div class="progress" id="progressBar"></div>
    </div>

    <div class="steps">
        <div class="step active" id="step1">
            <div class="circle"></div>
            <p>Order Placed</p>
        </div>

        <div class="step" id="step2">
            <div class="circle"></div>
            <p>Processing</p>
        </div>

        <div class="step" id="step3">
            <div class="circle"></div>
            <p>Shipped</p>
        </div>

        <div class="step" id="step4">
            <div class="circle"></div>
            <p>Delivered</p>
        </div>
    </div>

</div>

            <asp:Button ID="btnTrack" runat="server" Text="Continue Shopping" CssClass="btn" OnClick="btnTrack_Click" />

        </div>

        <!-- RIGHT SIDE -->
        <div class="right">

            <div class="summary-title">Order Summary</div>

            <asp:Repeater ID="rptItems" runat="server">
                <ItemTemplate>
                    <div class="product">
                        <img src='<%# ResolveUrl(Eval("Image").ToString()) %>' />
                        <div>
                            <%# Eval("Name") %><br />
                            Qty: <%# Eval("Qty") %>
                        </div>
                        <div style="margin-left:auto;">
                            ₹ <%# Eval("Price") %>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <hr />

            <div class="row">
                <span>Total :  <span>₹ <asp:Label ID="lblTotal" runat="server"></asp:Label></span></span>
               
            </div>






        </div>


       

    </div>
    <script>
function setStatus(status) {

    let progress = document.getElementById("progressBar");

    // always first active
    document.getElementById("step1").classList.add("active");

    if (status == "Order Placed") {
        document.getElementById("step1").classList.add("active");

        progress.style.width = "25%";
    }

    // Processing ma step1 already active hoy che, but clarity mate
    if (status == "Processing") {
        document.getElementById("step1").classList.add("active");
        document.getElementById("step2").classList.add("active");
        progress.style.width = "50%";
    }

    //if (status == "Processing") {
    //    document.getElementById("step2").classList.add("active");
    //    progress.style.width = "50%";
    //}

    if (status == "Shipped") {
        document.getElementById("step2").classList.add("active");
        document.getElementById("step3").classList.add("active");
        progress.style.width = "75%";
    }

    if (status == "Delivered") {
        document.getElementById("step2").classList.add("active");
        document.getElementById("step3").classList.add("active");
        document.getElementById("step4").classList.add("active");
        progress.style.width = "100%";
    }
}
    </script>

</asp:Content>
