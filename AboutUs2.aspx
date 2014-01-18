<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AboutUs2.aspx.cs" Inherits="detailwindow.AboutUs2" %>
<%@ Register TagPrefix="uc1" TagName="footer" Src="~/uc/footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="contentAboutUs" Src="~/uc/contentAboutUs.ascx" %>
<%@ Register TagPrefix="uc1" TagName="header" Src="~/uc/header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="linksMember" Src="~/uc/linksMember.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<title>Detail Window Cleaning of Indianapolis - About us</title>
	<link href="default.css" type="text/css" rel="stylesheet" />
</head>
<body>
	<div id="page">
    <form id="Form2" runat="server">
		<uc1:header id="Header1" runat="server"></uc1:header>
		<div id="wxForcastPanel"></div>
		<uc1:linksMember id="LinksMember1" runat="server"></uc1:linksMember>
		<uc1:contentAboutUs id="ContentAboutUs1" runat="server"></uc1:contentAboutUs>
		<uc1:footer id="Footer1" runat="server"></uc1:footer>
	</form>
	</div>
</body>
</html>
