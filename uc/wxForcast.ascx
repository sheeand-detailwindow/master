<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wxForcast.ascx.cs" Inherits="detailwindow.uc.wxForcast" %>

<div id="wxForcastPanel">
</div>
<div id="wxExtendedPanel">
<div id="WxExtendedPanel" runat="server">
<div style="font-size:12px;">For your scheduling convenience,<br />
here's your local daytime forecast:<br /></div>
<p><asp:Label ID="lblFirstDayPeriod" runat="server"></asp:Label>
<asp:Label ID="lblFirstDayCond" runat="server"></asp:Label><br />
High: <asp:Label ID="lblFirstDayTemp" runat="server"></asp:Label>&nbsp;degrees<br />
Precip: <asp:Label ID="lblFirstDayPrecip" runat="server"></asp:Label>&#037; chance<br /></p>
<p><asp:Label ID="lblSecondDayPeriod" runat="server"></asp:Label><br />
<asp:Label ID="lblSecondDayCond" runat="server"></asp:Label><br />
High: <asp:Label ID="lblSecondDayTemp" runat="server"></asp:Label>&nbsp;degrees<br />
Precip: <asp:Label ID="lblSecondDayPrecip" runat="server"></asp:Label>&#037; chance<br /></p>
<p><asp:Label ID="lblThirdDayPeriod" runat="server"></asp:Label><br />
<asp:Label ID="lblThirdDayCond" runat="server"></asp:Label><br />
High: <asp:Label ID="lblThirdDayTemp" runat="server"></asp:Label>&nbsp;degrees<br />
Precip: <asp:Label ID="lblThirdDayPrecip" runat="server"></asp:Label>&#037; chance<br /></p>
<p><asp:Label ID="lblForthDayPeriod" runat="server"></asp:Label><br />
<asp:Label ID="lblForthDayCond" runat="server"></asp:Label><br />
High: <asp:Label ID="lblForthDayTemp" runat="server"></asp:Label>&nbsp;degrees<br />
Precip: <asp:Label ID="lblForthDayPrecip" runat="server"></asp:Label>&#037; chance<br /></p>
<p><asp:Label ID="lblFifthDayPeriod" runat="server"></asp:Label><br />
<asp:Label ID="lblFifthDayCond" runat="server"></asp:Label><br />
High: <asp:Label ID="lblFifthDayTemp" runat="server"></asp:Label>&nbsp;degrees<br />
Precip: <asp:Label ID="lblFifthDayPrecip" runat="server"></asp:Label>&#037; chance<br /></p>
</div>
</div>
