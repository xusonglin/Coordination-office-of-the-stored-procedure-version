<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="JiaoShiXinXiTongJi.Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>出错了！！</title>
    <script type="text/javascript">
    
    
    </script>
</head>
<body>
    <form id="form1" runat="server" style="text-align:center;vertical-align:middle" >

    <div style=" text-align:center;vertical-align:middle;  margin-top:50px  " >
    <table  style="vertical-align:middle; margin-top:100px; text-align:center" align="center"  >
    <tr>
        <img src="images/image/ERROR.jpg" /></tr>
    <tr ><td colspan="2" >
        <asp:Label ID="Label1" runat="server" Text="提示：操作超时，请退出系统重新登录！" ForeColor="red"  Font-Size="Small"></asp:Label></td></tr>
   
    </table>

    </div>
    </form>
</body>
</html>
