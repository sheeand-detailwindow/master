<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="contentLogin.ascx.cs" Inherits="detailwindow.contentLogin" %>
<div id="contentBox3"><p><span id="heading2" class="heading2">Please log in</span></p> 
<p>If you are registered, please log in.</p>
<p>If you are not registered, please <asp:LinkButton ID="LinkButton1" runat="server" onclick="lnkRegister_Click">register as a new user</asp:LinkButton> 
so your requests can be submitted.</p>
<p>What you can do, if registered:<br />
	• GET A FREE ESTIMATE!<br />
	• Schedule a cleaning "on-line"<br />
	• Receive discount offers<br />
</p>
<p><span id="registerTextBlock">To log in, enter your username and password.<br /><br />
Check "Remember me", to skip the log-in in the future (on this computer).<br />
(You can log-out to cancel this feature at anytime.)<br /><br />
If you have no username / password, you are not registered.<br />Please 
    <asp:LinkButton ID="lnkRegister" 
        ToolTip="Click here to register as a new user" runat="server" 
        onclick="lnkRegister_Click">register as a new user</asp:LinkButton>       
</span>
</p>
<p><span id="registerTextBlock"></span></p></div>