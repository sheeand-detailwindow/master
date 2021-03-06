﻿<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="contentAdmin.ascx.cs" Inherits="detailwindow.contentAdmin" %>
    <style type="text/css">
        .auto-style1 {
            width: 1200px;
        }
        .consoleLink {
            height: 20px;
        }
        .consoleLinkTitle {
            width: 275px;
            height: 30px;
            font-weight: bold;
            font-size: 16px;
        }
        .auto-style3 {
            width: 300px;
        }
        .auto-style4 {
            height: 50px;
        }
   </style>

<link type="text/css" rel="stylesheet" href="../Scripts/jquery-ui-1.10.4.custom/css/smoothness/jquery-ui-1.10.4.custom.min.css" />
<script type="text/javascript" src="../Scripts/jquery-ui-1.10.4.custom/js/jquery-1.10.2.js"></script>
<script type="text/javascript" src="../Scripts/jquery-ui-1.10.4.custom/js/jquery-ui-1.10.4.custom.min.js"></script>

<asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">

    <asp:View ID="View0" runat="server">
    <span id="header4" class="heading2">Administrator Access</span><br /><br />
            <table class="auto-style1">
                <tr>
                    <td colspan="3" class="auto-style4">
                        </td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Button ID="btnHome1" runat="server" OnClick="btnHome_Click" Text="Return to home page" />
                    </td>
                    <td class="auto-style3">
                        <asp:Button ID="btnList" runat="server" commandargument="View1" commandname="SwitchViewByID" Text="Customer List" />
                    </td>
                    <td class="auto-style3">
                        <asp:Button ID="btnEmail" runat="server" commandargument="View2" commandname="SwitchViewByID" Text="Email Console" />
                    </td>
                </tr>
            </table>
    </asp:View>

<asp:View ID="View1" runat="server">

<span id="header3" class="heading2">Customer List</span><br /><br />
            <table class="auto-style1">
                <tr>
                    <td colspan="3" class="auto-style4">
                        </td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Button ID="Button1" runat="server" OnClick="btnHome_Click" Text="Return to home page" />
                    </td>
                    <td class="auto-style3">
                        <asp:Button ID="Button3" runat="server" commandargument="View2" commandname="SwitchViewByID" Text="Email Console" />
                    </td>
                </tr>
            </table>
This is a complete list of customers who use (or have used) the web site.<br />
The customers who most recently visited the web site are at the top of the list.<br /><br /><br />
THINGS YOU CAN DO:<br />
    <ul>
        <li>Click on a column heading to sort the list (or reverse the sort order) by that column.</li>
        <li>Click Delete to permanently delete that customer from the database. (If the customer returns, he or she will need to register again.)</li>
        <li>You can copy and paste the list into an Excel spreadsheet.</li>
    </ul>
<br />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <br /><br />
    <div class="heading2">Not receiving reminders:</div>
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlDataSource2" DataKeyNames="ID" CellPadding="2" 
        ForeColor="#333333" AllowSorting="True" Font-Size="Smaller">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" CausesValidation="false" ButtonType="Button" DeleteText="Delete Client" />
            <asp:BoundField DataField="Name" HeaderText="First Name" SortExpression="Name" ReadOnly="true">
                <ItemStyle Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" ReadOnly="true">
                <ItemStyle Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="CompanyName" HeaderText="Company Name" SortExpression="CompanyName" ReadOnly="true">
                <ItemStyle Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="Subdivision" HeaderText="Subdivision" SortExpression="Subdivision" ReadOnly="true">
                <ItemStyle Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" ReadOnly="true">
                <ItemStyle Width="150px" />
            </asp:BoundField>
            <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" ReadOnly="true">
                <ItemStyle Width="80px" />
            </asp:BoundField>
            <asp:BoundField DataField="Zip" HeaderText="Zip" SortExpression="Zip" ReadOnly="true">
                <ItemStyle Width="30px" />
            </asp:BoundField>
            <asp:BoundField DataField="Email" HeaderText="Email Address" SortExpression="Email" ReadOnly="true">
                <ItemStyle Width="150px" />
            </asp:BoundField>
            <asp:BoundField DataField="Phone1" HeaderText="Home Phone" SortExpression="Phone1" ReadOnly="true">
                <ItemStyle Width="80px" />
            </asp:BoundField>
            <asp:BoundField DataField="Phone2" HeaderText="Cell Phone" SortExpression="Phone2" ReadOnly="true">
                <ItemStyle Width="80px" />
            </asp:BoundField>
            <asp:BoundField DataField="Phone3" HeaderText="Work Phone" SortExpression="Phone3" ReadOnly="true">
                <ItemStyle Width="80px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Most Recent Visit" SortExpression="LastLogin">
                <ItemTemplate>
                    <%# Eval("LastLogin", "{0:MM/d/yyyy}") %>
                </ItemTemplate>
                <ItemStyle Width="100px"  />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Promo Sent" SortExpression="PromoSent">
                <ItemTemplate>
                    <%# Eval("PromoSent", "{0:MM/d/yyyy}") %>
                </ItemTemplate>
                <ItemStyle Width="100px"  />
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <EditRowStyle BackColor="#999999" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
    <br />
    <div class="heading2">Receiving reminders:</div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlDataSource1" DataKeyNames="ID" CellPadding="2" 
        ForeColor="#333333" AllowSorting="True" Font-Size="Smaller">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" CausesValidation="false" ButtonType="Button" DeleteText="Delete Client" />
            <asp:CommandField ShowEditButton="True" ButtonType="Button" EditText="Edit Reminder" />

            <asp:TemplateField HeaderText="Next Reminder" SortExpression="NextReminder" >
                <ItemTemplate>
                    <asp:Literal ID="Literal2" runat="server" Text='<%# Eval("NextReminder", "{0:MM/d/yyyy}")%>'></asp:Literal>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("NextReminder", "{0:MM/d/yyyy}")%>'></asp:TextBox><br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBox2"  Display="Dynamic"
                        ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$" ForeColor="White" BackColor="Red" ErrorMessage="Improper format"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <ItemStyle Width="60px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Months Between Reminders" SortExpression="Recurrency">
                <ItemTemplate>
                    <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("Recurrency")%>'></asp:Literal>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Recurrency")%>'></asp:TextBox><br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBox1"  Display="Dynamic"
                        ValidationExpression="^\d{1,2}$" ForeColor="White" BackColor="Red" ErrorMessage="Improper format"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <ItemStyle Width="60px" />
            </asp:TemplateField>

            <asp:BoundField DataField="Name" HeaderText="First Name" SortExpression="Name" ReadOnly="true">
                <ItemStyle Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" ReadOnly="true">
                <ItemStyle Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="CompanyName" HeaderText="Company Name" SortExpression="CompanyName" ReadOnly="true">
                <ItemStyle Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="Subdivision" HeaderText="Subdivision" SortExpression="Subdivision" ReadOnly="true">
                <ItemStyle Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" ReadOnly="true">
                <ItemStyle Width="150px" />
            </asp:BoundField>
            <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" ReadOnly="true">
                <ItemStyle Width="80px" />
            </asp:BoundField>
            <asp:BoundField DataField="Zip" HeaderText="Zip" SortExpression="Zip" ReadOnly="true">
                <ItemStyle Width="30px" />
            </asp:BoundField>
            <asp:BoundField DataField="Email" HeaderText="Email Address" SortExpression="Email" ReadOnly="true">
                <ItemStyle Width="150px" />
            </asp:BoundField>
            <asp:BoundField DataField="Phone1" HeaderText="Home Phone" SortExpression="Phone1" ReadOnly="true">
                <ItemStyle Width="80px" />
            </asp:BoundField>
            <asp:BoundField DataField="Phone2" HeaderText="Cell Phone" SortExpression="Phone2" ReadOnly="true">
                <ItemStyle Width="80px" />
            </asp:BoundField>
            <asp:BoundField DataField="Phone3" HeaderText="Work Phone" SortExpression="Phone3" ReadOnly="true">
                <ItemStyle Width="80px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Most Recent Visit" SortExpression="LastLogin">
                <ItemTemplate>
                    <%# Eval("LastLogin", "{0:MM/d/yyyy}") %>
                </ItemTemplate>
                <ItemStyle Width="100px"  />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Promo Sent" SortExpression="PromoSent">
                <ItemTemplate>
                    <%# Eval("PromoSent", "{0:MM/d/yyyy}") %>
                </ItemTemplate>
                <ItemStyle Width="100px"  />
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <EditRowStyle BackColor="#999999" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:detailConnectionString %>"
        ProviderName="<%$ ConnectionStrings:detailConnectionString.ProviderName %>"
        SelectCommand="SELECT [NextReminder], [Recurrency], [Name], [LastName], [CompanyName], [Subdivision], [Address], [City], [Zip], [Email], [Phone1], [Phone2], [Phone3], [LastLogin], [PromoSent], [ID] FROM [Customer] WHERE [ReminderOptOut] = NULL OR [ReminderOptOut] = 0 ORDER BY [LastLogin] DESC" 
        DeleteCommand="DELETE FROM [Customer] WHERE [ID] = @ID" 
        UpdateCommand="UPDATE [Customer] SET [NextReminder] = @NextReminder, [Recurrency] = ?  WHERE [ID] = @ID">
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="Int32" />
        </DeleteParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:detailConnectionString %>"
        ProviderName="<%$ ConnectionStrings:detailConnectionString.ProviderName %>"
        SelectCommand="SELECT [NextReminder], [Recurrency], [Name], [LastName], [CompanyName], [Subdivision], [Address], [City], [Zip], [Email], [Phone1], [Phone2], [Phone3], [LastLogin], [PromoSent], [ID] FROM [Customer] WHERE [ReminderOptOut] = 1 ORDER BY [LastLogin] DESC" 
        DeleteCommand="DELETE FROM [Customer] WHERE [ID] = @ID" >
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:Parameter Name="ReminderOptOut" Type="Boolean" />
        </DeleteParameters>
    </asp:SqlDataSource>
    <br />&nbsp;
    <asp:Button ID="btnHome" runat="server" OnClick="btnHome_Click" Text="Return to home page" />

</asp:View>
<asp:View ID="View2" runat="server">

    <span class="heading2">Email Console</span><br /><br />
    <table class="auto-style1">
        <tr>
            <td colspan="3" class="auto-style4">
                </td>
        </tr>
        <tr>
            <td class="auto-style3">
                <asp:Button ID="Button6" runat="server" OnClick="btnHome_Click" Text="Return to home page" />
            </td>
            <td>
                Don't send promo if one was sent on <input type="text" id="datepicker" name="datepicker"> or later.
            </td>
            <td class="auto-style3">
                <asp:Button ID="Button7" runat="server" commandargument="View1" commandname="SwitchViewByID" Text="Customer List" />
            </td>
        </tr>
    </table>

<table class="auto-style1">
    <tr>
        <td class="auto-style2" colspan="4">&nbsp;</td>
    </tr>
    <tr>
        <td class="consoleLinkTitle">Reminders</td>
        <td class="consoleLinkTitle">January-February Promotion</td>
        <td class="consoleLinkTitle">March Promotion</td>
        <td class="consoleLinkTitle">July Promotion</td>
    </tr>
    <tr>
        <td class="consoleLink"><a id="lnkSendReminderEmailToWebmaster" href="#" onclick="SendEmail('Reminder', 'WebmasterTest')">Send a Test Email To Dave</a></td>
        <td class="consoleLink"><a id="lnkSendJanFebEmailToWebmaster" href="#" onclick="SendEmail('JanFeb', 'WebmasterTest')">Send a Test Email To Dave</a></td>
        <td class="consoleLink"><a id="lnkSendMarchEmailToWebmaster" href="#" onclick="SendEmail('March', 'WebmasterTest')">Send a Test Email To Dave</a></td>
        <td class="consoleLink"><a id="lnkSendJulyEmailToWebmaster" href="#" onclick="SendEmail('July', 'WebmasterTest')">Send a Test Email To Dave</a></td>
    </tr>
    <tr>
        <td class="consoleLink"><label id="lblTestReminderEmailToWebmaster"> </label></td>
        <td class="consoleLink"><label id="lblTestJanFebEmailToWebmaster"> </label></td>
        <td class="consoleLink"><label id="lblTestMarchEmailToWebmaster"> </label></td>
        <td class="consoleLink"><label id="lblTestJulyEmailToWebmaster"> </label></td>

    </tr>
    <tr>
        <td class="consoleLink"><a id="lnkSendReminderEmailToAdministrator" href="#" onclick="SendEmail('Reminder', 'AdministratorTest')">Send a Test Email To Janice</a></td>
        <td class="consoleLink"><a id="lnkSendJanFebEmailToAdministrator" href="#" onclick="SendEmail('JanFeb', 'AdministratorTest')">Send a Test Email To Janice</a></td>
        <td class="consoleLink"><a id="lnkSendMarchEmailToAdministrator" href="#" onclick="SendEmail('March', 'AdministratorTest')">Send a Test Email To Janice</a></td>
        <td class="consoleLink"><a id="lnkSendJulyEmailToAdministrator" href="#" onclick="SendEmail('July', 'AdministratorTest')">Send a Test Email To Janice</a></td>
    </tr>
    <tr>
        <td class="consoleLink"><label id="lblTestReminderEmailToAdministrator"> </label></td>
        <td class="consoleLink"><label id="lblTestJanFebEmailToAdministrator"> </label></td>
        <td class="consoleLink"><label id="lblTestMarchEmailToAdministrator"> </label></td>
        <td class="consoleLink"><label id="lblTestJulyEmailToAdministrator"> </label></td>
    </tr>
    <tr>
        <td class="consoleLink">&nbsp;</td>
        <td class="consoleLink"><a id="lnkSendJanFebEmails" href="#" onclick="SendEmail('JanFeb', 'Live')">Send Emails To All Customers</a></td>
        <td class="consoleLink"><a id="lnkSendMarchEmails" href="#" onclick="SendEmail('March', 'Live')">Send Emails To All Customers</a></td>
        <td class="consoleLink"><a id="lnkSendJulyEmails" href="#" onclick="SendEmail('July', 'Live')">Send Emails To All Customers</a></td>
    </tr>
    <tr>
        <td class="consoleLink">&nbsp;</td>
        <td class="consoleLink"><label id="lblJanFebEmails" ></label></td>
        <td class="consoleLink"><label id="lblMarchEmails"> </label></td>
        <td class="consoleLink"><label id="lblJulyEmails"> </label></td>
    </tr>
    <tr>
        <td class="consoleLink" id="lblErrorMessage" colspan="4">&nbsp;</td>
    </tr>
</table>

    <br />

</asp:View>
</asp:MultiView>


<script type="text/javascript">
    $(document).ready ( function () {
        $("#datepicker").datepicker()
    })

    // Types: Reminder, JanFeb, March, July
    // Renditions: Live, WebmasterTest, AdministratorTest
    var path = '<%= Path %>';

    function SendEmail(type, rendition) {
        var thisRendition = rendition;
        switch (thisRendition) {

            case 'WebmasterTest':

                switch (type) {
                    case 'Reminder':
                        $("#lblTestReminderEmailToWebmaster").text('Email in progress')
                        break;
                    case 'JanFeb':
                        $("#lblTestJanFebEmailToWebmaster").text('Email in progress')
                        break;
                    case 'March':
                        $("#lblTestMarchEmailToWebmaster").text('Email in progress')
                        break;
                    case 'July':
                        $("#lblTestJulyEmailToWebmaster").text('Email in progress')
                        break;
                }

                break;

            case 'AdministratorTest':

                switch (type) {
                    case 'Reminder':
                        $("#lblTestReminderEmailToAdministrator").text('Email in progress')
                        break;
                    case 'JanFeb':
                        $("#lblTestJanFebEmailToAdministrator").text('Email in progress')
                        break;
                    case 'March':
                        $("#lblTestMarchEmailToAdministrator").text('Email in progress')
                        break;
                    case 'July':
                        $("#lblTestJulyEmailToAdministrator").text('Email in progress')
                        break;
                }

                break;

            case 'Live':

                var r = confirm("You are about to send hundreds of emails! DO NOT close this browser window until all emails have been sent. Are you sure you want to do this?");
                if (r == false) { return; }
                else {
                    switch (type) {
                        case 'Reminder':
                            $("#lblReminderEmails").text('Email in progress')
                            break;
                        case 'JanFeb':
                            $("#lblJanFebEmails").text('Email in progress')
                            break;
                        case 'March':
                            $("#lblMarchEmails").text('Email in progress')
                            break;
                        case 'July':
                            $("#lblJulyEmails").text('Email in progress')
                            break;
                    }

                    break;
            }
        }
        var date = $("#datepicker").datepicker("getDate");
        var postData = {
            Type: type,
            Rendition: thisRendition,
            Row: "0",
            Count: "0",
            CutoffDate: date
    }
        $.ajax({
            type: "POST",
            url: path,
            data: JSON.stringify(postData),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (obj) {
                if (obj.d != null) {
                    if (obj.d.length < 1) {
                        $("#lblErrorMessage").text(msg)
                    }
                    else {
                        var msg = obj.d[0];

                        switch (thisRendition) {

                            case 'WebmasterTest':

                            switch (type) {
                                case 'Reminder':
                                    $("#lblTestReminderEmailToWebmaster").text(msg)
                                    break;
                                case 'JanFeb':
                                    $("#lblTestJanFebEmailToWebmaster").text(msg)
                                    break;
                                case 'March':
                                    $("#lblTestMarchEmailToWebmaster").text(msg)
                                    break;
                                case 'July':
                                    $("#lblTestJulyEmailToWebmaster").text(msg)
                                    break;
                            }

                            break;

                            case 'AdministratorTest':

                                switch (type) {
                                    case 'Reminder':
                                        $("#lblTestReminderEmailToAdministrator").text(msg)
                                        break;
                                    case 'JanFeb':
                                        $("#lblTestJanFebEmailToAdministrator").text(msg)
                                        break;
                                    case 'March':
                                        $("#lblTestJanFebEmailToAdministrator").text(msg)
                                        break;
                                    case 'July':
                                        $("#lblTestJulyEmailToAdministrator").text(msg)
                                        break;
                                }

                                break;

                            case 'Live':

                            switch (type) {
                                case 'Reminder':
                                    $("#lblReminderEmails").text(msg)
                                    break;
                                case 'JanFeb':
                                    $("#lblJanFebEmails").text(msg)
                                    break;
                                case 'March':
                                    $("#lblMarchEmails").text(msg)
                                    break;
                                case 'July':
                                    $("#lblJulyEmails").text(msg)
                                    break;
                            }

                            if ((obj.d.length == 3) || (msg.indexOf("**Done**") == -1)) {
                                var row = obj.d[1];
                                var count = obj.d[2];
                                SendAnotherEmail(type, thisRendition, row, count);
                            }

                            break;
                        }
                    }
                }
            },
            error: function (msg) {
                $("#lblErrorMessage").text(msg)
            }
        });
    }

    function SendAnotherEmail(type, rendition, row, count) {
        var myRendition = rendition;
        var date = $("#datepicker").datepicker("getDate");
        var postData = {
            Type: type,
            Rendition: myRendition,
            Row: row,
            Count: count,   
            CutoffDate: date
    }
        $.ajax({
            type: "POST",
            url: path,
            data: JSON.stringify(postData),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (obj) {
                if (obj.d != null) {
                    if (obj.d.length.count < 1) {
                        $("#lblErrorMessage").text(msg)
                    }
                    else {
                        var msg = obj.d[0];
                        switch (type) {
                            case 'JanFeb':
                                $("#lblJanFebEmails").text(msg)
                                break;
                            case 'March':
                                $("#lblMarchEmails").text(msg)
                                break;
                            case 'July':
                                $("#lblJulyEmails").text(msg)
                                break;
                        }
                        if ((obj.d.length == 3) || (msg.indexOf("**Done**") == -1)) {
                            var row = obj.d[1];
                            var count = obj.d[2];
                            SendAnotherEmail(type, myRendition, row, count);
                        }
                    }
                }
            },
            error: function (msg) {
                $("#lblErrorMessage").text(msg)
            }
        });
    }

</script>
