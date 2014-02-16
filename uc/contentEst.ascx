<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="contentEst.ascx.cs" Inherits="detailwindow.contentEst" %>
<div id="contentBox3"><span class="heading2" >Get a FREE ESTIMATE</span>
<asp:label id="lblErrorMessage" ForeColor="Red" Font-Bold="True" runat="server"></asp:label><br />
    <br />
<strong>Cancellations?&nbsp; Need to reschedule?&nbsp; Call (317) 842-5326.</strong><br />
<br /><br />
<div id="leftjustify" style="text-align:left; margin-left:10px;">
<h4>What needs to be done?</h4>
<table cellspacing="9px" cellpadding="1px" width="450px" border="0px">
	<tr>
		<td>
			<asp:CheckBox id="chkOutside" runat="server" Text="Windows<br />(outside only)"></asp:CheckBox>
		</td>
		<td>
			<asp:CheckBox id="chkAllWindows" runat="server" Text="Windows<br />(inside and outside)" /></td>
		<td>
			<asp:CheckBox id="chkBlinds" runat="server" Text="Wood blinds"></asp:CheckBox>
		</td>
	</tr>
	<tr>
		<td>
			<asp:CheckBox id="chkChandelier" runat="server" Text="Chandelier(s)"></asp:CheckBox>
            </td>
		<td>
			<asp:CheckBox id="chkIntLighting" runat="server" Text="Interior light fixtures"></asp:CheckBox>
            </td>
		<td>
			<asp:CheckBox id="chkFans" runat="server" Text="Ceiling Fan(s)"></asp:CheckBox>
		</td>
	</tr>
	<tr>
		<td>
			<asp:CheckBox id="chkGutters" runat="server" Text="Rain gutters"></asp:CheckBox>
		</td>
		<td>
			<asp:CheckBox id="chkExtLighting" runat="server" Text="Exterior light fixtures"></asp:CheckBox>
		</td>
	</tr>
</table>
<table cellspacing="9px" cellpadding="1px" width="450px" border="0px">
	<tr>
		<td>&nbsp;
        </td>
    </tr>
	<tr>
		<td>
			<asp:CheckBox id="chkDeepClean" runat="server" Text="Interior deep cleaning (January through March only)"></asp:CheckBox>
        </td>
    </tr>
</table>
<br />
<br />
<h4>What date(s) or day(s) of the week do you prefer?</h4>
<asp:textbox id="TextBox1" runat="server" Rows="5" Height="50px" Width="400px" MaxLength="100"
    TextMode="MultiLine"></asp:textbox><br />
<br />
<br />
<h4>Comments, questions:</h4>
<asp:textbox id="TextBox2" runat="server" Rows="5" Height="50px" Width="400px" MaxLength="100"
					TextMode="MultiLine"></asp:textbox><br />
<br />
<br />
<h4>How do you prefer to be contacted?</h4>
<table cellspacing="1" cellpadding="1" width="50%" border="0">
    <tr>
		<td><asp:radiobutton id="rdoPhone" runat="server" Text="By telephone" GroupName="contact"></asp:radiobutton></td>
		<td><asp:radiobutton id="rdoEmail" runat="server" Text="By email" GroupName="contact"></asp:radiobutton></td>
	</tr>
</table><br /><br />
</div>
<h4>Your request will be submitted to a customer service representative.</h4>
    <br />
<asp:button id="btnSubmit" runat="server" Text="Submit your request" onclick="btnSubmit_Click"></asp:button><br /><br />
</div>
