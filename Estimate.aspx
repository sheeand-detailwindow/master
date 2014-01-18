<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Estimate.aspx.cs" Inherits="detailwindow.Estimate" validateRequest="false" %>
<%@ Register TagPrefix="uc1" TagName="header" Src="~/uc/header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="linksMemberProfile" Src="~/uc/linksMemberProfile.ascx" %>
<%@ Register TagPrefix="uc1" TagName="contentEst" Src="~/uc/contentEst.ascx" %>
<%@ Register TagPrefix="uc1" TagName="footer" Src="~/uc/footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="wxForcast" Src="~/uc/wxForcast.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<title>Detail Window Cleaning of Indianapolis - Request An Estimate</title>
	<link href="default.css" type="text/css" rel="stylesheet" />
</head>
<body>
<div id="page">
    <form id="Form2" runat="server">
        <uc1:header id="Header1" runat="server"></uc1:header>
        <uc1:wxForcast id="WxForcast" runat="server" />
		<uc1:linksMemberProfile id="linksMemberProfile1" runat="server"></uc1:linksMemberProfile>
        <uc1:contentEst ID="ContentEst1" runat="server"></uc1:contentEst>
	    <uc1:footer id="Footer1" runat="server"></uc1:footer>
	</form>
</div>
</body>
</html>
