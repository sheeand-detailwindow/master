<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPublic.aspx.cs" Inherits="detailwindow.ErrorPublic" %>
<%@ Register TagPrefix="uc1" TagName="header" Src="~/uc/header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="linksPublic" Src="~/uc/linksPublic.ascx" %>
<%@ Register TagPrefix="uc1" TagName="contentError" Src="~/uc/contentError.ascx" %>
<%@ Register TagPrefix="uc1" TagName="footer" Src="~/uc/footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<title>Detail Window Cleaning of Indianapolis - Error</title>
	<link href="default.css" type="text/css" rel="stylesheet" />
</head>
<body>
	<div id="page">
    <form id="Form2" runat="server">
		<uc1:header id="Header1" runat="server"></uc1:header>
		<div id="wxForcastPanel"></div>
		<uc1:linksPublic id="LinksPublic1" runat="server"></uc1:linksPublic>
		<uc1:contentError id="ContentAboutUs1" runat="server"></uc1:contentError>
		<uc1:footer id="Footer1" runat="server"></uc1:footer>
	</form>
	</div>
</body>
</html>
