<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="contentProfile.ascx.cs" Inherits="detailwindow.contentProfile" %>
<div id="contentBox3">
    <asp:label id="lblHeading2" runat="server" CssClass="heading2"></asp:label><br />
	<asp:label id="lblErrorMessage" runat="server" Font-Bold="True" ForeColor="Red"></asp:label><br />
	<asp:label id="lblInfoMessage" runat="server" Font-Bold="True"></asp:label><br />
    <br />
    The information you submit is for the exclusive use of Detail Window Cleaning, and
    will not be divulged to any outside entities.<br />
	<br />
	<div style="color:red">* Required information</div><br />
	<table cellspacing="1" cellpadding="1" width="700" border="0" >
		<tr>
			<td style="WIDTH: 115px" align="right">First name <font color="red">*</font> :</td>
			<td><asp:textbox id="txtName" runat="server" TabIndex="1"></asp:textbox></td>
			<td><asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ErrorMessage="RequiredFieldValidator"
					ControlToValidate="txtName" Display="Dynamic">Please enter your first name.</asp:requiredfieldvalidator>
			<asp:regularexpressionvalidator id="RegularExpressionValidator3" runat="server" ErrorMessage="Please enter a name from 2 to 20 characters long with no special characters."
					ControlToValidate="txtName" Display="Dynamic" ValidationExpression="^([1-zA-Z0-1@.\s]{2,20})$"></asp:regularexpressionvalidator></td>
		</tr>
		<tr>
			<td style="WIDTH: 115px" align="right">Last name <font color="red">*</font> :</td>
			<td><asp:textbox id="txtLastName" runat="server" TabIndex="2"></asp:textbox></td>
			<td><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator"
					ControlToValidate="txtLastName" Display="Dynamic">Please enter your last name.</asp:requiredfieldvalidator>
			<asp:regularexpressionvalidator id="RegularExpressionValidator2" runat="server" ErrorMessage="Please enter a name from 2 to 20 characters long with no special characters."
					ControlToValidate="txtLastName" Display="Dynamic" ValidationExpression="^([1-zA-Z0-1@.\s]{2,20})$"></asp:regularexpressionvalidator></td>
		</tr>
		<tr>
			<td style="WIDTH: 115px" align="right">Company name :</td>
			<td><asp:textbox id="txtCompany" runat="server" TabIndex="3"></asp:textbox></td>
			<td><asp:regularexpressionvalidator id="RegularExpressionValidator8" runat="server" ErrorMessage="The company name must be less than 30 characters long with no special characters."
					ControlToValidate="txtCompany" ValidationExpression="^([1-zA-Z0-1@.\s]{4,30})$"></asp:regularexpressionvalidator></td>
		</tr>
		<tr>
			<td style="WIDTH: 115px" align="right">Account type :</td>
			<td>&nbsp;&nbsp;&nbsp;
				<asp:radiobutton id="rdoBusiness" runat="server" Text="Business"></asp:radiobutton>&nbsp;&nbsp;&nbsp;&nbsp;
				<asp:radiobutton id="rdoResidence" runat="server" Text="Residence" TabIndex="4"></asp:radiobutton></td>
			<td></td>
		</tr>
		<tr>
			<td style="WIDTH: 115px" align="right">Subdivision :</td>
			<td><asp:textbox id="txtSubdivision" runat="server" Width="200px" TabIndex="5"></asp:textbox></td>
			<td><asp:regularexpressionvalidator id="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter a subdivision no more than 30 characters long."
					ControlToValidate="txtSubdivision" Display="Dynamic" ValidationExpression="^([1-zA-Z0-1@.\s]{1,30})$"></asp:regularexpressionvalidator></td>
		</tr>
		<tr>
			<td style="WIDTH: 115px" align="right">Address <font color="red">*</font> :</td>
			<td><asp:textbox id="txtAddress" runat="server" Width="200px" TabIndex="6"></asp:textbox></td>
			<td><asp:requiredfieldvalidator id="RequiredFieldValidator7" runat="server" ErrorMessage="RequiredFieldValidator"
					ControlToValidate="txtAddress" Display="Dynamic">Please enter your address.</asp:requiredfieldvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator4" runat="server" ErrorMessage="Please enter an address no more than 30 characters long."
					ControlToValidate="txtAddress" Display="Dynamic" ValidationExpression="^([1-zA-Z0-1@.\s]{1,30})$"></asp:regularexpressionvalidator></td>
		</tr>
		<tr>
			<td align="right" class="style1">City <font color="red">*</font> :</td>
			<td><asp:textbox id="txtCity" runat="server" TabIndex="7"></asp:textbox></td>
			<td><asp:requiredfieldvalidator id="RequiredFieldValidator8" runat="server" ErrorMessage="RequiredFieldValidator"
					ControlToValidate="txtCity" Display="Dynamic">Please enter your city.</asp:requiredfieldvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator5" runat="server" ErrorMessage="Please enter a valid city."
					ControlToValidate="txtCity" Display="Dynamic" ValidationExpression="^([1-zA-Z0-1@.\s]{1,15})$"></asp:regularexpressionvalidator></td>
		</tr>
		<tr>
			<td style="WIDTH: 115px" align="right">Zip <font color="red">*</font> :</td>
			<td><asp:textbox id="txtZip" runat="server" Width="100px"  TabIndex="8"></asp:textbox></td>
			<td><asp:requiredfieldvalidator id="RequiredFieldValidator9" runat="server" ErrorMessage="RequiredFieldValidator"
					ControlToValidate="txtZip" Display="Dynamic">Please enter your 5-digit zip code.</asp:requiredfieldvalidator><asp:rangevalidator id="RangeValidator6" runat="server" ErrorMessage="RangeValidator" ControlToValidate="txtZip"
					Display="Dynamic" MinimumValue="40000" MaximumValue="50000">Please enter a valid, 5-digit zip code.</asp:rangevalidator></td>
		</tr>
		<tr>
			<td style="WIDTH: 115px" align="right">Email address <font color="red">*</font> :</td>
			<td><asp:textbox id="txtEmail" runat="server" Width="200px"  TabIndex="9"></asp:textbox></td>
			<td><asp:requiredfieldvalidator id="RequiredFieldValidator10" runat="server" ErrorMessage="RequiredFieldValidator"
					ControlToValidate="txtEmail" Display="Dynamic">Please enter your email address</asp:requiredfieldvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator6" runat="server" ErrorMessage="Please enter a valid email address."
					ControlToValidate="txtEmail" Display="Dynamic" ValidationExpression="^([1-zA-Z0-1@.\s]{10,30})$"></asp:regularexpressionvalidator></td>
		</tr>
		<tr>
			<td style="WIDTH: 115px" align="right">Home phone :</td>
			<td><asp:textbox id="txtPhone1" runat="server" TabIndex="10"></asp:textbox>
                <%--<asp:RadioButton ID="rbPhone1" runat="server" GroupName="PrimaryPhone" Text="Primary" />--%></td>
            <td>&nbsp;</td>
		</tr>
		<tr>
			<td style="WIDTH: 115px" align="right">Cell phone :</td>
			<td><asp:textbox id="txtPhone2" runat="server" TabIndex="11"></asp:textbox>
			<%--<asp:RadioButton ID="brPhone2" runat="server" GroupName="PrimaryPhone" Text="Primary" />--%></td>
			<td>&nbsp;</td>
		</tr>
        <tr>
            <td align="right" style="width: 115px">Work phone :&nbsp;</td>
            <td><asp:TextBox ID="txtPhone3" runat="server" TabIndex="12"></asp:TextBox>
            <%--<asp:RadioButton ID="rbPhone3" runat="server" GroupName="PrimaryPhone" Text="Primary" />--%>
                
            </td>
            <td>&nbsp;</td>
        </tr>
		<tr>
			<td style="WIDTH: 115px" align="right">Fax :</td>
			<td><asp:textbox id="txtFax" runat="server" TabIndex="13"></asp:textbox></td>
		    <td></td>
		</tr>
		<tr>
			<td style="WIDTH: 115px" align="right"><asp:label ID="lblReminderLabel" runat="server">Next reminder :</asp:label></td>
			<td colspan="2" >
                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
                    DataSourceID="SqlDataSource1" DataKeyNames="ID" CellPadding="2" ForeColor="White" BorderWidth="0px" ShowHeader="false"  >
                    <Columns>
                        <asp:TemplateField SortExpression="NextReminder" ShowHeader="False">
                            <ItemTemplate>
                                <asp:Literal ID="Literal2" runat="server" Text='<%# Eval("NextReminder", "{0:MM/d/yyyy}")%>'></asp:Literal>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("NextReminder", "{0:MM/d/yyyy}")%>'></asp:TextBox><br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="TextBox2"  Display="Dynamic"
                                    ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$" ForeColor="White" BackColor="Red" ErrorMessage="Improper format"></asp:RegularExpressionValidator>
                            </EditItemTemplate>
                            <ItemStyle Width="60px" />
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" ButtonType="Button" EditText="Edit Reminder" />
                   </Columns>
                    <EditRowStyle BackColor="#FFFFFF" />
                    <RowStyle ForeColor="Black"/>
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>
            </td>
		</tr>
	</table>
	<br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:detailConnectionString %>"
        ProviderName="<%$ ConnectionStrings:detailConnectionString.ProviderName %>"
        SelectCommand="SELECT [NextReminder], [ID] FROM [Customer] WHERE [ID] = ?" 
        UpdateCommand="UPDATE [Customer] SET [NextReminder] = ?  WHERE [ID] = ?">
        <SelectParameters>
            <asp:SessionParameter Name="ID" SessionField="ID" DbType="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:SessionParameter Name="ID" SessionField="ID" DbType="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
	<br />
	&nbsp;&nbsp;&nbsp;<asp:Button ID="btnHome" runat="server" OnClick="btnHome_Click"
        Text="Cancel and return to home page" ToolTip="Click to cancel and return to home page" UseSubmitBehavior="False" CausesValidation="False" />&nbsp;&nbsp;&nbsp;&nbsp;
	<asp:button id="btnUpdate" runat="server" Text="Submit profile" OnClick="btnUpdate_Click" TabIndex="13" ToolTip="Click to submit this profile"></asp:button><br /><br />
</div>