<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="contentMember.ascx.cs" Inherits="detailwindow.contentMember" %>
<div id="contentBoxOuterFrame">
<div id ="contentBoxInnerFrame">
<span id="heading2" class="heading2">DETAIL WINDOW CLEANING</span>
<br />
<object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0" width="480" height="360" id="myFirstFlash" align="middle">
<param name="allowScriptAccess" value="sameDomain" />
<param name="movie" value="Images/myFirstFlash.swf" />
<param name="quality" value="high" />
<param name="bgcolor" value="#ffffff" />
<embed src="Images/myFirstFlash.swf" quality="high" bgcolor="#ffffff" width="480" height="360" name="myFirstFlash2" align="middle" allowScriptAccess="sameDomain" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />
</object>
<br />
<p><span id="paragraphHeading3" class="paragraphHeading">Please keep your user profile current</span><br />
<span id="paragraphAngie2" class="paragraphHeading">
<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/profile.aspx">Edit your profile</asp:HyperLink>
    to check your profile information, and make any changes.</span></p>
</div>
</div>