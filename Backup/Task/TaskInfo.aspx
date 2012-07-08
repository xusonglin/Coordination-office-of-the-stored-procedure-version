<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaskInfo.aspx.cs" Inherits="JiaoShiXinXiTongJi.Task.TaskInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
</head>
<body style="background-color: #D7E6ED">
    <form id="form1" runat="server" style="background-color: #D7E6ED">
    <style type="text/css">
        h3, ul
        {
            margin: 0px;
            padding: 0px;
        }
        h3
        {
            font-size: 16px;
        }
        ul
        {
            background-color: #f7f7f7;
            color: 000;
        }
        li
        {
            border-top: 1px solid #fff;
            border-bottom: 1px solid #e8e8e8;
            list-style: none;
            padding: 5px 0px 5px 18px;
        }
    </style>
    <div style="width: 100%;">
        <table width="100%">
            <tr align="center">
                <td >
                    <asp:Label ID="title" runat="server" Text="Label" Font-Bold="True" Font-Size="X-Large"></asp:Label>
                </td>
            </tr>
            <tr><td align="center">
                  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  <asp:Label ID="editer" runat="server" Text="Label"></asp:Label> <asp:Label ID="time" runat="server" Text="Label"></asp:Label>
              </td>
            </tr>
        
                <tr>
                <td style="width: 100%; text-align: left">
                      【任务要求】
                </td> 
            </tr>
        </table>
    </div>

    <hr />
    <div style="width: 100%;">
        <div id="content" runat="server">
        </div>
    </div>
    <hr />
    <div style="text-align: right; padding-right: 100px">
        【任务】 <a href="EditeTask.aspx?Id=<%= strID %>">
            <%= strname %></a>
    </div>
    </form>
</body>
</html>
