<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContentContactUs.ascx.cs" Inherits="detailwindow.ContentContactUs1" %>
<div id="contentBox3">
<span id="heading2" class="heading2">Contact Us</span>
<p>Thank you for your interest in Detail Window Cleaning.</p>
    <p>Enter your message below.</p>
    <div id="emailAddress" runat="server">
        <p>
            Please enter your email address, so we may reply: <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            <br />
            <asp:requiredfieldvalidator id="RequiredFieldValidator10" runat="server" ErrorMessage="RequiredFieldValidator"
					ControlToValidate="txtEmail" Display="Dynamic">Please enter your email address</asp:requiredfieldvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator6" runat="server" ErrorMessage="Please enter a valid email address."
					ControlToValidate="txtEmail" Display="Dynamic" ValidationExpression="^([1-zA-Z0-1@.\s]{10,30})$"></asp:regularexpressionvalidator>
        </p>
    </div>
    <p>
        <asp:TextBox ID="txtBox" runat="server" Rows="6" TextMode="MultiLine" 
            Width="400px"></asp:TextBox></p>
    <p>
        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />&nbsp;</p>
    <p>
        <asp:Label ID="lblErrorMessage" runat="server" BackColor="Red" Font-Bold="True" ForeColor="White"></asp:Label>&nbsp;</p>
</div>
