<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="JiaoShiXinXiTongJi.DaoRu.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
        <table style="text-align: center; width: 700px" border="0" id="selecttable" runat="server">
            <tr id="trselet">
                <td style="width: 300px; text-align: right">
                    选择课表Excel：
                </td>
                <td style="width: 400px; text-align: left">
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    <asp:Button ID="Button1" runat="server" Text="数据导入" OnClick="Button1_Click" />
                </td>
            </tr>
               <tr id="tishitr" style="text-align:center"  visible="false">
             <td colspan="2"><span style="color:Red"  >预览数据：请确定数据正确！</span></td>
             </tr>
            <tr>
          
            <td colspan="2">
                <asp:Button ID="queding" runat="server" Text="确定"  Visible="false" 
                    onclick="queding_Click"/>  
                <asp:Button ID="fanhui" runat="server" Text="返回" Visible="false" 
                    onclick="fanhui_Click"/></td>
            
            
            
            
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
