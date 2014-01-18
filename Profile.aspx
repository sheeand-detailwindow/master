<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="detailwindow.Profile" %>
<%@ Register TagPrefix="uc1" TagName="footer" Src="~/uc/footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="contentProfile" Src="~/uc/contentProfile.ascx" %>
<%@ Register TagPrefix="uc1" TagName="linksMember" Src="~/uc/linksMember.ascx" %>
<%@ Register TagPrefix="uc1" TagName="header" Src="~/uc/header.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<title>Detail Window Cleaning of Indianapolis - Edit Your Profile</title>
	<link href="default.css" type="text/css" rel="stylesheet" />
</head>
<body>
	<div id="page">
        <form id="Form2" runat="server">
		<uc1:header id="Header1" runat="server"></uc1:header>
		<div id="wxForcastPanel"></div>
		<uc1:contentProfile id="contentProfile1" runat="server"></uc1:contentProfile>
		<uc1:footer id="Footer1" runat="server"></uc1:footer>
        </form>
	</div>
</body>
</html>
