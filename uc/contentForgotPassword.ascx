<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="contentForgotPassword.ascx.cs" Inherits="detailwindow.uc.contentForgotPassword" %>
<div id="contentBox3">
    <br />
    An email with your password will be sent to you.<br />
    Please enter the following:
	<br /><br />
	<font color="red">* Required information</font><br />
    <br /><br />
	<table cellspacing="1" cellpadding="1" width="700" border="0">
		<tr>
			<td style="WIDTH: 115px" align="right">First name <font color="red">*</font> :</td>
			<td><asp:textbox id="txtName" runat="server" ></asp:textbox></td>
			<td><asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ErrorMessage="RequiredFieldValidator"
					ControlToValidate="txtName" Display="Dynamic">Please enter your first name.</asp:requiredfieldvalidator>
			<asp:regularexpressionvalidator id="RegularExpressionValidator3" runat="server" ErrorMessage="Please enter a name from 2 to 20 characters long with no special characters."
					ControlToValidate="txtName" Display="Dynamic" ValidationExpression="^([1-zA-Z0-1@.\s]{2,20})$"></asp:regularexpressionvalidator></td>
		</tr>
		<tr>
			<td style="WIDTH: 115px" align="right">Last name <font color="red">*</font> :</td>
			<td><asp:textbox id="txtLastName" runat="server" ></asp:textbox></td>
			<td><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator"
					ControlToValidate="txtLastName" Display="Dynamic">Please enter your last name.</asp:requiredfieldvalidator>
			<asp:regularexpressionvalidator id="RegularExpressionValidator2" runat="server" ErrorMessage="Please enter a name from 2 to 20 characters long with no special characters."
					ControlToValidate="txtLastName" Display="Dynamic" ValidationExpression="^([1-zA-Z0-1@.\s]{2,20})$"></asp:regularexpressionvalidator></td>
		</tr>
		<tr>
			<td style="WIDTH: 115px" align="right">Zip <font color="red">*</font> :</td>
			<td><asp:textbox id="txtZip" runat="server" Width="100px" ></asp:textbox></td>
			<td><asp:requiredfieldvalidator id="RequiredFieldValidator9" runat="server" ErrorMessage="RequiredFieldValidator"
					ControlToValidate="txtZip" Display="Dynamic">Please enter your 5-digit zip code.</asp:requiredfieldvalidator><asp:rangevalidator id="RangeValidator6" runat="server" ErrorMessage="RangeValidator" ControlToValidate="txtZip"
					Display="Dynamic" MinimumValue="40000" MaximumValue="50000">Please enter a valid, 5-digit zip code.</asp:rangevalidator></td>
		</tr>
	</table>
	<br />
	<asp:button id="btnVerify" runat="server" Text="Send Email" 
        ToolTip="Click to check user profile" onclick="btnVerify_Click"></asp:button><br /><br />
    <asp:Label ID="lblInvalid" runat="server" Visible="false" ForeColor="red" Text="The information you entered is not in our database. Please try again."></asp:Label>
</div>