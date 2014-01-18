<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AboutUs.aspx.cs" Inherits="detailwindow.AboutUs" %>
<%@ Register TagPrefix="uc1" TagName="header" Src="~/uc/header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="linksPublic" Src="~/uc/linksPublic.ascx" %>
<%@ Register TagPrefix="uc1" TagName="contentAboutUs" Src="~/uc/contentAboutUs.ascx" %>
<%@ Register TagPrefix="uc1" TagName="footer" Src="~/uc/footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<title>Detail Window Cleaning of Indianapolis - About us</title>
	<link href="default.css" type="text/css" rel="stylesheet" />
    <meta name="google-site-verification" content="fGwnGmCp8Fvd76vEISrD6U3502xGnPtC4yxwKOoUUa4" />
</head>
<body>
	<div id="page">
    <form id="Form2" runat="server">
		<uc1:header id="Header1" runat="server"></uc1:header>
		<div id="wxForcastPanel"></div>
		<uc1:linksPublic id="LinksPublic1" runat="server"></uc1:linksPublic>
		<uc1:contentAboutUs id="ContentAboutUs1" runat="server"></uc1:contentAboutUs>
		<uc1:footer id="Footer1" runat="server"></uc1:footer>
	</form>
	</div>
</body>
</html>
