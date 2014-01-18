<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="linksMemberProfile.ascx.cs" Inherits="detailwindow.uc.linksMemberProfile" %>
    <div id="linkBox"> 
        <asp:ImageButton ID="btnHome" runat="server" ImageUrl="../Images/HomeNormal.gif"  
        OnClick="btnHome_Click" /><br />
        <asp:ImageButton ID="btnAboutUs" runat="server" ImageUrl="../Images/AboutNormal.gif"  
        OnClick="btnAboutUs_Click" /><br />    
        <asp:ImageButton ID="btnEstimate" runat="server" ImageUrl="../Images/GetNormal.gif"  
        OnClick="btnEstimate_Click" /><br />    
        <asp:ImageButton ID="btnAppointment" runat="server" ImageUrl="../Images/ScheduleNormal.gif"  
        OnClick="btnAppointment_Click" /><br />    
        <asp:ImageButton ID="btnEditProfile" runat="server" ImageUrl="../Images/EditNormal.gif"  
        OnClick="btnEditProfile_Click" /><br />    
        <asp:ImageButton ID="btnContactUs" runat="server" ImageUrl="../Images/ContactNormal.gif"  
        OnClick="btnContactUs_Click" /><br />
        <asp:ImageButton ID="btnAdmin" runat="server" ImageUrl="../Images/CustomerNormal.gif"  
        OnClick="btnAdmin_Click" /><br />
        <asp:ImageButton ID="btnDeAutoLogin" runat="server" ImageUrl="../Images/LogoutNormal.gif"  
        OnClick="btnDeAutoLogin_Click" /><br />
        <asp:ImageButton ID="btnLogOut" runat="server" ImageUrl="../Images/LogoutNormal.gif"  
        OnClick="btnLogOut_Click" /><br /><br /><br />
        <div id="nudge" style="margin-left:10px;">
            <div id="redText" class="redText"><asp:Label id="lblName" runat="server" /> <asp:Label id="lblLastName" runat="server" /><br />
	        <asp:Label id="lblProfile" runat="server" /><br />
	        </div>
	        <div id="inquireProfile">
	        If any of the above is incorrect, please <a id="redirectProfile" href="../Profile.aspx">edit your profile</a>.<br /><br /><br />
	        </div>
	    </div>  
    </div>
    <asp:Label ID="lblErrorMessage" runat="server" BackColor="Red" Font-Bold="True" ForeColor="White"></asp:Label>
