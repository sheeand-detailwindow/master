<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="detailwindow.Login" %>
<%@ Register TagPrefix="uc1" TagName="header" Src="~/uc/header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="linksLogin" Src="~/uc/linksLogin.ascx" %>
<%@ Register TagPrefix="uc1" TagName="contentLogin" Src="~/uc/contentLogin.ascx" %>
<%@ Register TagPrefix="uc1" TagName="footer" Src="~/uc/footer.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<title>Detail Window Cleaning of Indianapolis - Login</title>
	<link href="default.css" type="text/css" rel="stylesheet" />
 </head>
<body>
    <form id="form2" runat="server">
		<div id="page">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<div id="wxForcastPanel"></div>
			<uc1:linksLogin id="LinksLogin1" runat="server"></uc1:linksLogin>
			<uc1:contentLogin id="ContentLogin1" runat="server"></uc1:contentLogin>
			<uc1:footer id="Footer1" runat="server"></uc1:footer>
		</div>
	</form>
</body>
</html>
