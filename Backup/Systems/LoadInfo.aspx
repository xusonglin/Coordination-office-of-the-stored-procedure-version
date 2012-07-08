<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoadInfo.aspx.cs" Inherits="JiaoShiXinXiTongJi.Systems.LoadInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>教师信息初始化</title>
</head>
<style type="text/css">
   
    .tdBlue
    {
        background-color: #ECF2FE;
        width: 280px;
        text-align: right;
        font-weight: 600;
    }
        .tdBluea
    {
      
        width: 300px;
        text-align: left;
       
    }
   
</style>
<body>
    <form id="Form1" runat="server">
    <table runat="server" align="center" id="Table1" cellpadding="0" cellspacing="0"  border="1" width="600px">
        <tr style="height: 30px">
            <td colspan="2">
            </td>
        </tr>
        <tr align="center">
            <td colspan="2">
                <span style="color: red">注意：</span> 教师信息初始化完成用户数据的批量导入 <span style="color: red">（操作前请确保Excel格式正确！）</span>
            </td>
        </tr>
        <tr  >
            <td class="tdBlue">
                选择学校：
            </td>
             <td class="tdBluea">
                <asp:DropDownList ID="DropDownLisxx" runat="server" OnSelectedIndexChanged="DropDownListxy_SelectedIndexChanged"
                    AutoPostBack="True" Width="200px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdBlue">
                选择学院：
            </td>
              <td class="tdBluea">
                <asp:DropDownList ID="DropDownListxy" runat="server" Width="200">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdBlue">
                默认角色：
            </td>
              <td class="tdBluea">
                <asp:DropDownList ID="DropDownListxyrole" runat="server" Width="200">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdBlue">
                选择导入数据的Excel：
            </td>
              <td class="tdBluea">
                <input type="file" id="UP_FILE" runat="server" accept="text/*" name="UP_FILE" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="Button1" runat="server" Width="100" OnClick="Button_Submit" Text="数据导入" />
            </td>
        </tr>
        <%--    <tr align="center">
          <td colspan="3">
              <asp:Label ID="tishixx" runat="server"  Visible="false" ForeColor="Red"></asp:Label>
           </td></tr>
           <tr align="center">
          <td colspan="3">
              <asp:Label ID="tishixy" runat="server"  Visible="false" ForeColor="Red"></asp:Label>
           </td></tr>
           <tr align="center">
          <td colspan="3">
              <asp:Label ID="tishirole" runat="server"  Visible="false" ForeColor="Red"></asp:Label>
           </td></tr>
           <tr align="center">
          <td colspan="3">
              <asp:Label ID="tishiexcel" runat="server"  Visible="false" ForeColor="Red"></asp:Label>
           </td></tr>--%>
        <tr align="center">
            <td colspan="2">
                <asp:Label ID="tishi" runat="server" Visible="false" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
