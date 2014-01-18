<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="contentContactUs2.ascx.cs" Inherits="detailwindow.uc.contentContactUs2" %>
<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.DynamicData" TagPrefix="cc1" %>

<div id="contentBox3">
<span id="heading2" class="heading2">Contact Us</span>
<p>Thank you for your interest in Detail Window Cleaning.</p>
    <p>Enter your message below.</p>
    <div id="emailAddress" runat="server" visible="false">
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
            Width="400px"></asp:TextBox>&nbsp;</p>
    <p>
        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />&nbsp;</p>
    <p>
        <asp:Label ID="lblErrorMessage" runat="server" BackColor="Red" Font-Bold="True" ForeColor="White"></asp:Label>&nbsp;</p>
</div>