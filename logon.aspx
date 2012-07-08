<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="logon.aspx.cs" Inherits="JiaoShiXinXiTongJi.logon" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>用户登录</title>
<link href="css/denglu.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">


function checkform()
{
var username=document.getElementById("username").value;
var pass=document.getElementById("password").value;
 if(username==""&&pass=="")
  {
    alert("用户名、密码不能为空！")
    return false;
  }
 if(username=="")
  {
    alert("用户名不能为空！")
    return false;
  }
 if(pass=="")
{
   alert("密码不能为空！")
    return false;
}
else 
return true;

}

</script>
</head>

<body>
<div id="total">
<form id="form1" method="post" action="logon.aspx" onsubmit="return checkform()"   runat="server">
<div class="thdiv">
<div class="theline">
<div class="username">用户名：</div>
<div class="biaodan"><input name="username" id="username"  type="text" class="theput"  /></div>
</div>

<div class="theline">
<div class="username">密&nbsp;&nbsp;&nbsp;&nbsp;码：</div>
<div class="biaodan"><input name="password"  id="password"  type="password" class="theput" style=" width:160px"/></div>
</div>



<div class="theline">
<div class="username"></div>
<div class="biaodan3"><input type="submit" value=""  class="thebut"/></div>
<div class="biaodan3"><input type="button" value=""  class="thebutt"/></div>

</div>
</div>
</form>
<div class="dengbott">
<p>版权所有：湖北师范学院</p>
<p>技术支持：湖北师范学院计算机科学与技术学院</p>
</div>
</div>
</body>
</html>
