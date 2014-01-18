<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="linksRegister.ascx.cs" Inherits="detailwindow.linksRegister" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:Panel ID="pnlLinksLogin" DefaultButton="btnLogin" runat="server">
<div id="linkBox">
    <asp:ImageButton ID="btnHome" runat="server" ImageUrl="../Images/HomeNormal.gif"  
    OnClick="btnHome_Click" /><br />
    <asp:ImageButton ID="btnAboutUs" runat="server" ImageUrl="../Images/AboutNormal.gif"  
    OnClick="btnAboutUs_Click" /><br />    
    <asp:ImageButton ID="btnContactUs" runat="server" ImageUrl="../Images/ContactNormal.gif"  
    OnClick="btnContactUs_Click" /><br />
	<br />
	Username:<br />
	<asp:textbox id="txtUserName" TabIndex="1" runat="server" Columns="12"  ></asp:textbox>
    <br />
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter a username no more than 30 characters long."
        ControlToValidate="txtUserName" Display="Dynamic" ValidationExpression="^([1-zA-Z0-1@.\s]{1,30})$"></asp:RegularExpressionValidator>
    <br />
<br />
	Password:<br />
	<asp:textbox id="txtPassword" TabIndex="2" runat="server" TextMode="Password" Columns="12"></asp:textbox>
    <br />
    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please enter a password no more than 30 characters long."
        ControlToValidate="txtPassword" Display="Dynamic" ValidationExpression="^([1-zA-Z0-1@.\s]{1,30})$"></asp:RegularExpressionValidator>
    <br />
<br />
Confirm password:<br />
<asp:TextBox ID="txtConfirmPassowrd" TabIndex="3" runat="server" TextMode="Password" Columns="12"></asp:TextBox>
<br />
<cc1:PasswordStrength ID="PasswordStrength1" runat="server" TargetControlID="txtPassword" 
    StrengthIndicatorType="Text" PreferredPasswordLength="8" PrefixText="Strength:" 
    TextStrengthDescriptions="Very Poor;Weak;Average;Good" MinimumNumericCharacters="0" 
    TextCssClass="LoginStrengthIndicatorText" RequiresUpperAndLowerCaseCharacters="false">
</cc1:PasswordStrength>
    <asp:CompareValidator ID="cvConfirmationValidation" runat="server" 
        BackColor="Red" ControlToCompare="txtPassword" 
        ControlToValidate="txtConfirmPassowrd" Display="Dynamic" 
        EnableClientScript="false" 
        ErrorMessage="Your password confirmation is incorrect.  Please try again." 
        Font-Bold="True" ForeColor="White" Operator="Equal" Type="String"></asp:CompareValidator>
    <br />
<asp:Label id="lblErrorMessage" runat="server" BackColor="Red" ForeColor="White" Font-Bold="True"></asp:Label>
<asp:Label id="lblErrorMessage2" runat="server" BackColor="Red" ForeColor="White" Font-Bold="True"></asp:Label>
<br />
	<asp:checkbox id="chkAutoLogin" runat="server" Text="Remember me"
	ToolTip="Check this box to allow your computer to automatically log-in everytime"></asp:checkbox>
<br />
<br />
	<asp:button id="btnLogin" tabIndex="4" runat="server" Text="Login" onclick="btnLogin_Click"></asp:button>
	<br />
	<br />
</div>
</asp:Panel>