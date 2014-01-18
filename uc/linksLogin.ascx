<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="linksLogin.ascx.cs" Inherits="detailwindow.linksLogin" %>
<asp:Panel ID="pnlLinksLogin" DefaultButton="btnLogin" runat="server">
<div id="linkBox">
    <asp:ImageButton ID="btnHome" runat="server" ImageUrl="../Images/HomeNormal.gif"  
    OnClick="btnHome_Click" /><br />
    <asp:ImageButton ID="btnAboutUs" runat="server" ImageUrl="../Images/AboutNormal.gif"  
    OnClick="btnAboutUs_Click" /><br />    
    <asp:ImageButton ID="btnRegister" runat="server" ImageUrl="../Images/RegisterNormal.gif"  
    OnClick="btnRegister_Click" /><br />    
    <asp:ImageButton ID="btnContactUs" runat="server" ImageUrl="../Images/ContactNormal.gif"  
    OnClick="btnContactUs_Click" /><br />
	<br />
	LOG IN:<br />
	Username:<br />
	<asp:textbox id="txtUserName" TabIndex="1" runat="server" Columns="12" ></asp:textbox>
    <br />
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
        ControlToValidate="txtUserName" Display="Dynamic" 
        ErrorMessage="Please enter a username no more than 30 characters long." 
        ValidationExpression="^([1-zA-Z0-1@.\s]{1,30})$"></asp:RegularExpressionValidator>
    <br />
    <br />
	Password:<br />
	<asp:textbox id="txtPassword" TabIndex="2" runat="server" TextMode="Password" Columns="12"></asp:textbox>
    <br />
    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
        ControlToValidate="txtPassword" Display="Dynamic" 
        ErrorMessage="Please enter a password no more than 30 characters long." 
        ValidationExpression="^([1-zA-Z0-1@.\s]{1,30})$"></asp:RegularExpressionValidator>
    <br />
    <br />
	<asp:button id="btnLogin" tabIndex="3" runat="server" Text="Login" onclick="btnLogin_Click"></asp:button>&nbsp;
	<asp:checkbox id="chkAutoLogin" runat="server" Text="Remember me" ToolTip="Check this box to allow your computer to automatically log-in everytime"></asp:checkbox><br />
<asp:Label id="lblErrorMessage" runat="server" BackColor="Red" ForeColor="White" Font-Bold="True"></asp:Label>
<asp:Label id="lblErrorMessage2" runat="server" BackColor="Red" ForeColor="White" Font-Bold="True"></asp:Label>
	<br />
    <br />
    <asp:LinkButton ID="btnForgot" runat="server" Text="Forgot password?" 
        onclick="btnForgot_Click"></asp:LinkButton>
	<br />
	<br />
	Not registered?<br />
    <asp:LinkButton ID="lnkRegister" runat="server" onclick="lnkRegister_Click">Register as a new user</asp:LinkButton>
	<br />
	<br />
</div></asp:Panel>