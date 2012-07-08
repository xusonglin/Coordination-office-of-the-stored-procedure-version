<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myinfo.aspx.cs" Inherits="JiaoShiXinXiTongJi.USER.myinfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script src="../js/jss/Date.js" type="text/javascript"></script>

    <link rel="Stylesheet" href="../css/update_info.css" />
  

    <script type="text/javascript">
           function setCalendar(obj, ID, format){
			popUpCalendar(obj, ID, format);
			return false;
             }
    </script>

</head>

<body style=" background-color:#D5DEE8">
    <center>
        <form id="form1" runat="server">
        <table id="Table1" runat="server">
            <img src="../images/image/photo.gif" />
            <tr>
                <td colspan="4" style="">
                    <asp:Label ID="Label10" runat="server" Text="个人信息维护" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr style="height:20px"></tr>
            <tr align="right">
                <td colspan="2">
                    <table>
                        <tr  align="center">
                            <td style="height: 27px">
                                <asp:Label ID="zgxm_" runat="server" Text="职工姓名："></asp:Label>
                            </td>
                            <td style="height: 27px">
                                <asp:TextBox ID="zgxm" runat="server"></asp:TextBox>
                                <span style="color: Red">*</span>
                            </td>
                        </tr>
                        <tr  align="left">
                            <td style="height: 27px">
                                <asp:Label ID="zgbh_" runat="server" Text="职工编号："></asp:Label>
                            </td>
                            <td style="height: 27px">
                                <asp:TextBox ID="zgbh" runat="server" ReadOnly="true"></asp:TextBox>
                                <span style="color: Red">*</span>
                            </td>
                        </tr>
                        <tr align="left">
                            <td>
                                <asp:Label ID="xb_" runat="server" Text="性&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;别："></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="xb" runat="server" Width="60px">
                                    <asp:ListItem Value="男" Text="男"></asp:ListItem>
                                    <asp:ListItem Value="女" Text="女"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
                <td colspan="2" style=" text-align:center">
                    <%--<img alt="" src="../images/image/photo.gif" style="width: 80px; height: 100px" />--%>
                </td>
            </tr>
             <tr  align="left">
                <td style="height: 25px">
                    <asp:Label ID="xy_" runat="server" Text="学&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;校："></asp:Label>
                </td>
                <td style="height: 25px; width: 170px;">
                    <asp:DropDownList ID="DropDownList_xx" runat="server" Width="155px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="yx_" runat="server" Text="学&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;院："></asp:Label>
                </td>
                <td style="height: 25px; width: 170px;">
                    <asp:DropDownList ID="DropDownList_xy" runat="server" Width="155px">
                    </asp:DropDownList>
                </td>
            </tr>
             <tr  align="left">
                <td>
                    <asp:Label ID="role_" runat="server" Text="角&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;色："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="role" runat="server" ReadOnly="true"></asp:TextBox>
                    <span style="color: Red">*</span>
                </td>
            </tr>
      <%--      <tr style="padding-left: 90px">
                <td colspan="4" style="" align="center">
                    <div style="height: 8px; margin-top: 8px; border-top: 5px dotted #15428B;">
                    </div>
                </td>
            </tr>--%>
            <%--       <tr style="padding-left: 90px">
                <td colspan="4" style="" align="center">
                    <asp:Label ID="Label1" Font-Bold="true" runat="server" Text="个人信息"></asp:Label>
                </td>
            </tr--%>>
            <tr>
            </tr>
            <tr  align="left">
                <td style="height: 25px">
                    <asp:Label ID="csrq_" runat="server" Text="出生日期："></asp:Label>
                </td>
                <td style="height: 25px">
                    <asp:TextBox ID="csrq" runat="server" BackColor="#F8F3AC" onclick="javascript:ShowCalendar('csrq')"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="nl_" runat="server" Text="年&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;龄："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="nl" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr  align="left">
                <td style="height: 25px">
                    <asp:Label ID="mz_" runat="server" Text="民&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;族："></asp:Label>
                </td>
                <td style="height: 25px">
                    <asp:TextBox ID="mz" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="jg_" runat="server" Text="籍&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;贯："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="jg" runat="server"></asp:TextBox>
                </td>
            </tr>
             <tr  align="left">
                <td style="height: 25px; text-align: left">
                    <asp:Label ID="xl_" runat="server" Text="学&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;历："></asp:Label>
                </td>
                <td style="height: 25px">
                    <%--   // <asp:TextBox ID="xl" runat="server"></asp:TextBox>--%>
                    <asp:DropDownList ID="DropDownList_xl" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="zw_" runat="server" Text="职&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;务："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="zw" runat="server"></asp:TextBox>
                </td>
            </tr>
           <tr  align="left">
                <td style="height: 25px">
                    <asp:Label ID="zcjb_" runat="server" Text="职称级别："></asp:Label>
                </td>
                <td style="height: 25px">
                    <%--<asp:TextBox ID="zcjb" runat="server"></asp:TextBox>--%>
                    <asp:DropDownList ID="DropDownList_zc" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="dzjz_" runat="server" Text="党政兼职："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="dzjz" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr  align="left">
                <td style="height: 25px">
                    <asp:Label ID="rzsj_" runat="server" Text="任职时间："></asp:Label>
                </td>
                <td style="height: 25px">
                    <asp:TextBox ID="rzsj" runat="server" BackColor="#F8F3AC" onclick="javascript:ShowCalendar('rzsj')"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="cid_" runat="server" Text="身份证号："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="cid" runat="server"></asp:TextBox>
                </td>
            </tr>
           <tr  align="left">
                <td>
                    <asp:Label ID="lxdh_" runat="server" Text="联系电话："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="lxdh" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="email_" runat="server" Text="Email："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="email" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:Button ID="submit" runat="server" Text="确定修改" OnClick="submit_Click" CommandName="submit"
                        ValidationGroup="submit" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <%-- <a href="javascript:void(0)" onclick="javascript:window.location.href='editeUser.aspx'" class="easyui-linkbutton" iconCls="icon-redo">重新填写</a>--%>
                    <input type="button" value=" 重新填写" width="80px" onclick="javascript:window.location.href='editeUser.aspx' " />
                </td>
            </tr>
        </table>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator_sl" runat="server" ValidationGroup="submit"
            ErrorMessage="职工编号不能为空！" ControlToValidate="zgbh" SetFocusOnError="True" Display="None" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="submit"
            ErrorMessage="职工姓名不能为空！" ControlToValidate="zgxm" SetFocusOnError="True" Display="None" />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="年龄格式错误！"
            ControlToValidate="nl" Display="None" ValidationGroup="submit" ValidationExpression="^[0-9]*[1-9][0-9]*$"
            SetFocusOnError="True" />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="联系电话格式错误！"
            ControlToValidate="lxdh" ValidationExpression="((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)"
            Display="None" ValidationGroup="submit"></asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="身份证号格式错误！"
            ControlToValidate="cid" ValidationExpression="^(\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$"
            Display="None" ValidationGroup="submit"></asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Email格式错误！"
            ControlToValidate="email" ValidationExpression="^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
            Display="None" ValidationGroup="submit"></asp:RegularExpressionValidator>
        <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="submit"
            ShowSummary="True" ShowMessageBox="true" />
        </form>
    </center>
</body>
</html>
