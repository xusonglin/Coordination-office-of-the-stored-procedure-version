<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="JiaoShiXinXiTongJi.CourseRemind.Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>修改课程信息</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../themes/icon.css" />
    <script type="text/javascript" src="../js/jquery/jquery-1.4.4.min.js"></script>
    <script type="text/javascript" src="../js/jquery/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../js/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#details").panel('setTitle', "修改课程信息（请谨慎修改）");
        });
    </script>
    <style type="text/css">
        html
        {
            min-height: 100%;
            height: 100%;
        }
        body
        {
            margin: 0;
            padding: 0;
            min-height: 100%;
            height: 100%;
        }
        .tdWidth
        {
            width: 220px;
            background-color: White;
            padding: 5px;
            text-align: left;
        }
        .tdWidtha
        {
            background-color: White;
            padding: 5px;
            text-align: left;
        }
        .tdBlue
        {
            background-color: #ECF2FE;
            width: 80px;
            margin-left: 5px;
            padding: 5px;
            text-align: right;
            font-weight: 600;
        }
        #Save
        {
            padding: 3px 10px 3px 10px;
        }
        #details input, #details select
        {
            border: 1px solid #BCD1EB;
            padding: 3px 5px;
            width: 200px;
        }
        #details input:focus, #details select:focus
        {
            border: 1px solid #69BBE8;
            background-color: #DEE9FB;
        }
        .table_title
        {
            font-weight: 600;
            background-color: #ECF2FE;
        }
        #details .week input
        {
            width: 70px;
        }
        #SaveInfo
        {
            color: Red;
            font-size: 13px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="easyui-panel" id="details" fit="false" title="treatise" style="padding: 10px;
        margin: 0 auto;">
        <table border="0" style="background-color: #BCD1EB; margin: 5px;" width="99%">
            <tr>
                <td class="tdBlue table_title">
                    项目名称
                </td>
                <td class="tdWidth table_title">
                    当前值<asp:TextBox ID="Id_Edit" runat="server" Visible="false"></asp:TextBox>
                </td>
                <td class="tdWidtha table_title">
                    修改值<font style="font-weight: 500;">（请参照“当前值”修改）</font>
                </td>
            </tr>
            <tr>
                <td class="tdBlue">
                    课程名称
                </td>
                <td class="tdWidth">
                    <asp:Label ID="Course_Name" runat="server"></asp:Label>
                </td>
                <td class="tdWidtha">
                    <asp:TextBox ID="Course_Name_Edit" runat="server" class="easyui-validatebox" required="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdBlue">
                    职工号
                </td>
                <td class="tdWidth">
                    <asp:Label ID="Staff_num" runat="server"></asp:Label>
                </td>
                <td class="tdWidtha">
                    <asp:TextBox ID="Staff_num_Edit" runat="server" class="easyui-validatebox" required="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdBlue">
                    教师姓名
                </td>
                <td class="tdWidth">
                    <asp:Label ID="Teacher_Name" runat="server"></asp:Label>
                </td>
                <td class="tdWidtha">
                    <asp:TextBox ID="Teacher_Name_Edit" runat="server" class="easyui-validatebox" required="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdBlue">
                    开课学院
                </td>
                <td class="tdWidth">
                    <asp:Label ID="Department" runat="server"></asp:Label>
                </td>
                <td class="tdWidtha">
                    <asp:TextBox ID="Department_Edit" runat="server" class="easyui-validatebox" required="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdBlue">
                    总学时
                </td>
                <td class="tdWidth">
                    <asp:Label ID="Hours" runat="server"></asp:Label>
                </td>
                <td class="tdWidtha">
                    <asp:TextBox ID="Hours_Edit" runat="server" class="easyui-validatebox" required="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdBlue">
                    周
                </td>
                <td class="tdWidth">
                    <asp:Label ID="Class_Time" runat="server"></asp:Label>
                </td>
                <td class="tdWidtha">
                    <asp:DropDownList ID="Class_Time_Edit" runat="server" class="easyui-validatebox"
                        required="true">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>周日</asp:ListItem>
                        <asp:ListItem>周一</asp:ListItem>
                        <asp:ListItem>周二</asp:ListItem>
                        <asp:ListItem>周三</asp:ListItem>
                        <asp:ListItem>周四</asp:ListItem>
                        <asp:ListItem>周五</asp:ListItem>
                        <asp:ListItem>周六</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="tdBlue">
                    节
                </td>
                <td class="tdWidth">
                    <asp:Label ID="Class_Week" runat="server"></asp:Label> 节课
                </td>
                <td class="tdWidtha">
                    <asp:DropDownList ID="Class_Week_Edit" runat="server" class="easyui-validatebox"
                        required="true">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>1,2</asp:ListItem>
                        <asp:ListItem>3,4</asp:ListItem>
                        <asp:ListItem>5,6</asp:ListItem>
                        <asp:ListItem>7.8</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="tdBlue">
                    周数
                </td>
                <td class="tdWidth">
                    <asp:Label ID="Total_Week" runat="server"></asp:Label>
                </td>
                <td class="tdWidtha week">
                    <asp:TextBox ID="Total_Week_Edit1" runat="server" class="easyui-validatebox" required="true"></asp:TextBox>
                    到
                    <asp:TextBox ID="Total_Week_Edit2" runat="server" class="easyui-validatebox" required="true"></asp:TextBox>
                    周
                </td>
            </tr>
            <tr>
                <td class="tdBlue">
                    单双周
                </td>
                <td class="tdWidth">
                    <asp:Label ID="Is_Week" runat="server"></asp:Label>
                </td>
                <td class="tdWidtha">
                    <asp:DropDownList ID="Is_Week_Edit" runat="server" class="easyui-validatebox" required="true">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>每周</asp:ListItem>
                        <asp:ListItem>单周</asp:ListItem>
                        <asp:ListItem>双周</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="tdBlue">
                    上课地点
                </td>
                <td class="tdWidth">
                    <asp:Label ID="Class_Addr" runat="server"></asp:Label>
                </td>
                <td class="tdWidtha">
                    <asp:TextBox ID="Class_Addr_Edit" runat="server" class="easyui-validatebox" required="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdBlue">
                    教学班组成
                </td>
                <td class="tdWidth">
                    <asp:Label ID="Classes" runat="server"></asp:Label>
                </td>
                <td class="tdWidtha">
                    <asp:TextBox ID="Classes_Edit" runat="server" class="easyui-validatebox" required="true"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div style="height: 20px;">
    </div>
    <div style="width: 100%; text-align: center;">
        <asp:Button ID="Save" runat="server" Text=" 保 存 修 改 " OnClick="Save_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="SaveInfo" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
