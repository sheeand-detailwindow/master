<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default2.aspx.cs" Inherits="detailwindow.Default2" %>
<%@ Register TagPrefix="uc1" TagName="footer" Src="~/uc/footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="linksMember" Src="~/uc/linksMember.ascx" %>
<%@ Register TagPrefix="uc1" TagName="header" Src="~/uc/header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="contentMember" Src="~/uc/contentMember.ascx" %>
<%@ Register TagPrefix="uc1" TagName="wxForcast" Src="~/uc/wxForcast.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<title>Detail Window Cleaning of Indianapolis - Home page</title>
	<link href="default.css" type="text/css" rel="stylesheet" />
	<link rel="shortcut icon" href="images/favicon.ico" />
    <script type="text/javascript" src="swfobject.js"></script>
    <script type="text/javascript">
        var flashvars = {};
        var params = {};
        params.menu = "false";
        params.loop = "false";
        params.scale = "default";
        var attributes = {};
        swfobject.embedSWF("detailwindologo.swf", "headerImage", "200", "82", "9.0.0", "expressInstall.swf", flashvars, params, attributes);
    </script>
</head>
<body>
	<div id="page">
    <form id="Form2" runat="server">
		    <uc1:header id="Header1" runat="server"></uc1:header>
            <uc1:wxForcast id="WxForcast" runat="server" />
			<uc1:linksMember id="LinksMember1" runat="server"></uc1:linksMember>
			<uc1:contentMember id="ContentMember1" runat="server"></uc1:contentMember>
			<uc1:footer id="Footer1" runat="server"></uc1:footer>
	</form>
	<form id="Form1" action="https://www.paypal.com/cgi-bin/webscr" method="post">
        <input type="hidden" name="cmd" value="_s-xclick" />
        <input type="hidden" name="hosted_button_id" value="SSTDHF8UKXDMA" />
        <input type="image" src="images/imgMakePymt2.gif" 
            style="border-style: none; border-color: inherit; border-width: 0px; position:relative; top:-350px; margin-left:15px; left:160px;" 
            name="submit" alt="PayPal - The safer, easier way to pay online!" />
        <img alt="" src="https://www.paypalobjects.com/en_US/i/scr/pixel.gif" width="1" height="1" style="border:0px;" />
        <div id="donation" style="text-align:center; position:relative; top:-340px; left:162px; margin-left:18px; width:150px; font-weight:normal; color:#CC3333; font-size:10px;" >Payment will show as made to RJJK Inc</div>
    </form>
	</div>
</body>
</html>
