<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeAutoLoginConfirm.aspx.cs" Inherits="detailwindow.DeAutoLoginConfirm" %>
<%@ Register TagPrefix="uc1" TagName="linksMember" Src="~/uc/linksMember.ascx" %>
<%@ Register TagPrefix="uc1" TagName="contentDeAutoLoginConfirm" Src="~/uc/contentDeAutoLoginConfirm.ascx" %>
<%@ Register TagPrefix="uc1" TagName="footer" Src="~/uc/footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="header" Src="~/uc/header.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<title>Detail Window Cleaning of Indianapolis - Profile Updated</title>
	<link href="default.css" type="text/css" rel="stylesheet" />
</head>
<body>
		<div id="page"><uc1:header id="Header1" runat="server"></uc1:header>
			<uc1:linksMember id="LinksMember1" runat="server"></uc1:linksMember>
			<uc1:contentDeAutoLoginConfirm id="contentDeAutoLoginConfirm1" runat="server"></uc1:contentDeAutoLoginConfirm>				
			<uc1:footer id="Footer1" runat="server"></uc1:footer>
		</div>
</body>
</html>
