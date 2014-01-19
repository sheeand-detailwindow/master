<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="contentAdmin.ascx.cs" Inherits="detailwindow.contentAdmin" %>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 300px;
        }
    </style>
    <span id="header3" class="heading2">Customer List</span><br /><br />
This is a complete list of customers who use (or have used) the web site.<br />
The customers who most recently visited the web site are at the top of the list.<br /><br /><br />
THINGS YOU CAN DO:<br />
    <ul>
        <li>Click on a column heading to sort the list (or reverse the sort order) by that column.</li>
        <li>Click Delete to permanently delete that customer from the database. (If the customer returns, he or she will need to register again.)</li>
        <li>You can copy and paste the list into an Excel spreadsheet.</li></ul>
    <br />
<table class="auto-style1">
    <tr>
        <td class="auto-style2">
        <asp:Button ID="btnHome1" runat="server" OnClick="btnHome_Click" Text="Return to home page" /></td>
        <td class="auto-style2">&nbsp;</td>
        <td class="auto-style2">
            <asp:Button ID="btnEmailService" runat="server" Text="Email Console" />
        </td>
    </tr>
</table>
<br />
<asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
    <asp:View ID="View1" runat="server">
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
        SelectCommand="SELECT [NextReminder], [Recurrency], [Name], [LastName], [CompanyName], [Subdivision], [Address], [City], [Zip], [Email], [Phone1], [Phone2], [Phone3], [LastLogin], [ID] FROM [Customer] WHERE [ReminderOptOut] = NULL OR [ReminderOptOut] = FALSE ORDER BY [LastLogin] DESC" 
        DeleteCommand="DELETE FROM [Customer] WHERE [ID] = ?" 
        UpdateCommand="UPDATE [Customer] SET [NextReminder] = ?, [Recurrency] = ?  WHERE [ID] = ?">
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="Int32" />
        </DeleteParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:detailConnectionString %>"
        ProviderName="<%$ ConnectionStrings:detailConnectionString.ProviderName %>"
        SelectCommand="SELECT [NextReminder], [Recurrency], [Name], [LastName], [CompanyName], [Subdivision], [Address], [City], [Zip], [Email], [Phone1], [Phone2], [Phone3], [LastLogin], [ID] FROM [Customer] WHERE [ReminderOptOut] = TRUE ORDER BY [LastLogin] DESC" 
        DeleteCommand="DELETE FROM [Customer] WHERE [ID] = ?" >
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:Parameter Name="ReminderOptOut" Type="Boolean" />
        </DeleteParameters>
    </asp:SqlDataSource>
    <br />&nbsp;
    <asp:Button ID="btnHome" runat="server" OnClick="btnHome_Click" Text="Return to home page" />
    </asp:View>
    <asp:View ID="View2" runat="server">

    </asp:View>
</asp:MultiView>

<script>

    // Types: Reminder, JanFeb, March, July
    // Renditions: Live, WebmasterTest, AdministratorTest

    function SendEmail(type, rendition) {
        switch (rendition) {
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
                }
        }
        var postData = {
            Type: type,
            Rendition: rendition,
            Row: "1"
        }
        $.ajax({
            type: "POST",
            url: 'http://www.detailwindow.com/api/EmailService.asmx/SendEmail',
            data: JSON.stringify(postData),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (obj) {
                var msg = obj.d[0];
                if (msg.indexOf("EmailApi") != -1) {
                    $("#lblErrorMessage").text(msg)
                }
                else {
                    switch (rendition) {
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
                            if (msg.indexOf("Done") == -1) {
                                var row = obj.d[1];

                                // Do the next row
                                SendAnotherEmail(type, rendition, row);
                            }
                            break;
                    }
                }
            },
            error: function (obj) {
                var msg = obj.d[1];
                $("#lblErrorMessage").text(msg)
            }
        });
    }

    function SendAnotherEmail(type, rendition, row) {
        var postData = {
            Type: type,
            Rendition: rendition,
            Row: row
        }
        $.ajax({
            type: "POST",
            url: 'http://www.detailwindow.com/api/EmailService.asmx/SendEmail',
            data: JSON.stringify(postData),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (obj) {
                var msg = obj.d[0];
                if (msg.indexOf("EmailApi") != -1) {
                    $("#lblErrorMessage").text(msg)
                }
                else {
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
                    if (msg.indexOf("***Done***") == -1) {
                        var row = obj.d[1];
                        SendAnotherEmail(type, rendition, row);
                    }
                }
            },
            error: function (obj) {
                var msg = obj.d[1];
                $("#lblErrorMessage").text(msg)
            }
        });
    }

</script>