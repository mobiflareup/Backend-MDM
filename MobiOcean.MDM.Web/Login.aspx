<%@Page Title="" Language="C#"  AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MobiOcean.MDM.Web.Login" MaintainScrollPositionOnPostback="true" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" >
    <title>Login Page</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/> 
        <meta name="description" content="Login and Registration Form with HTML5 and CSS3" />
        <meta name="keywords" content="html5, css3, form, switch, animation, :target, pseudo-class" />
        <meta name="author" content="Codrops" />
         <link rel="shortcut icon" href="../favicon.ico"/> 
        <link rel="stylesheet" type="text/css" href="css/demo.css" />
        <link rel="stylesheet" type="text/css" href="css/style1.css" />
		<link rel="stylesheet" type="text/css" href="css/animate-custom.css" />
    <style>
        .custom-main-login{width:400px;border:3px solid #fff;margin:10% auto;padding:25px;text-align:center;}
    </style>

</head>
<body  id="bg" runat="server" style="background: -webkit-linear-gradient(90deg, #FF512F 10%, #DD2476 90%);
    background: -moz-linear-gradient(90deg, #FF512F 10%, #DD2476 90%);
    background: -ms-linear-gradient(90deg, #FF512F 10%, #DD2476 90%);
    background: -o-linear-gradient(90deg, #FF512F 10%, #DD2476 90%);
    background: linear-gradient(90deg, #2f65ff 10%, #24ddd4 90%);color:#fff;">  
  
    <div id="container_demo" class="custom-main-login">
        <a class="hiddenanchor" id="toregister"></a>
         <a class="hiddenanchor" id="tologin"></a>
    <div id="wrapper" >
    
        <div id="login" class="animate form">
           
     <form id="Form1"  autocomplete="on" runat="server"> 
       
     <h1>Log in</h1> 
        <p>
        <asp:Label ID="lblusername" runat="server"></asp:Label>
        
         <asp:TextBox ID="txtusername" runat="server" Height="22px" placeholder="UserName" ></asp:TextBox>
       
       </p>
         <p>
        <asp:Label ID="lblpassword"  runat="server"></asp:Label>
        
        <asp:TextBox ID="txtpassword" runat="server" Height="22px" placeholder="Password" TextMode="Password"></asp:TextBox></p>
       <p class="login button"  >
           <asp:Label ID="lblmsg" runat="server" Text="Invalid User ID or Password" Visible="false"></asp:Label>
           <a ID="btnForgot" runat="server" href="ForgotPasswrd.aspx" style="color:#fff;">Forgot Password?</a>
        <asp:Button ID="btnlogin" runat="server" Text="Login" OnClick="btnlogin_Click" />
           </p>
        
        <p class="change_link">&nbsp;</p>
    
    </form>
       </div>  
   
    </div>
    </div>
              
</body>
</html>
