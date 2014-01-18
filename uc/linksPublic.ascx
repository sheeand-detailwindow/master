<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="linksPublic.ascx.cs" Inherits="detailwindow.linksPublic" %>
<script type="text/javascript">
    function homeButtonMouseOver() {
        var buttonHome = document.getElementById("buttonHome");
        buttonHome.src = "../Images/HomeBright.gif";
    }
    function homeButtonMouseOut() {
        var buttonHome = document.getElementById("buttonHome");
        buttonHome.src = "../Images/HomeNormal.gif";
    }
    function aboutUsButtonMouseOver() {
        var buttonAboutUs = document.getElementById("buttonAboutUs");
        buttonAboutUs.src = "../Images/AboutBright.gif";
    }
    function aboutUsButtonMouseOut() {
        var buttonAboutUs = document.getElementById("buttonAboutUs");
        buttonAboutUs.src = "../Images/AboutNormal.gif";
    }
</script>

<div id="linkBox">
    <a href="../Default.aspx"><img id="buttonHome" alt="" class="noBorder" src="../Images/HomeNormal.gif" onmouseover="homeButtonMouseOver()" onmouseout="homeButtonMouseOut()" /></a><br />
    <a href="../AboutUs.aspx"><img id="buttonAboutUs" alt="" class="noBorder" src="../Images/AboutNormal.gif" onmouseover="aboutUsButtonMouseOver()" onmouseout="aboutUsButtonMouseOut()" /></a><br />
    <asp:ImageButton ID="btnRegister" runat="server" ImageUrl="../Images/RegisterNormal.gif"  
    OnClick="btnRegister_Click" /><br />    
    <asp:ImageButton ID="btnEstimate" runat="server" ImageUrl="../Images/GetNormal.gif"  
    OnClick="btnEstimate_Click" /><br />    
    <asp:ImageButton ID="btnAppointment" runat="server" ImageUrl="../Images/ScheduleNormal.gif"  
    OnClick="btnAppointment_Click" /><br />    
    <asp:ImageButton ID="btnLogin" runat="server" ImageUrl="../Images/LoginNormal.gif"  
    OnClick="btnLogin_Click" /><br />    
    <asp:ImageButton ID="btnContactUs" runat="server" ImageUrl="../Images/ContactNormal.gif"  
    OnClick="btnContactUs_Click" /><br /><br />
</div>
<asp:label id="lblErrorMessage" runat="server" Font-Bold="True" ForeColor="White" BackColor="Red"></asp:label><asp:label id="lblErrorMessage2" runat="server" Font-Bold="True" ForeColor="White" BackColor="Red"></asp:label>
 