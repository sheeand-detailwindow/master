<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Admin.aspx.cs" Inherits="detailwindow.Admin" %>
<%@ Register TagPrefix="uc1" TagName="footer" Src="~/uc/footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="contentAdmin" Src="~/uc/contentAdmin.ascx" %>
<%@ Register TagPrefix="uc1" TagName="header" Src="~/uc/header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="linksMember" Src="~/uc/linksMember.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<title>Detail Window Cleaning - Administrator Page</title>
	<link href="admin.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.10.1.min.js"></script>
</head>
<body>
	<div id="page">
    <form id="Form2" runat="server">
		<uc1:header id="Header1" runat="server"></uc1:header>
		<uc1:contentAdmin id="ContentAdmin1" runat="server"></uc1:contentAdmin>
	</form>
	</div>
</body>
</html>
