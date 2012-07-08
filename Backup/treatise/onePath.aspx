<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="onePath.aspx.cs" Inherits="JiaoShiXinXiTongJi.treatise.onePath" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="xpathHead" runat="server">
    <title></title>
      <link href="../themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-1.4.2.js" type="text/javascript"></script>
    <script src="../js/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../js/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <link href="../css/content.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">

    
 var rid = '<%=rid%>';
 function giveupp()
 {

 window.location="onePath.aspx?Id="+rid;
 
 }

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
            width: 250px;
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
            width: 120px;
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
    <script src="../js/jss/xpath.js" type="text/javascript"></script>
</head>
<body>
    <form id="updateForm" runat="server" action="TreadtieseHandler_.ashx">
 
      <table border="0" style="background-color: #BCD1EB; margin: 5px;" width="99%">
            <tr>
                 <td class="tdBlue">
                    网站名称
                </td>
               <td class="tdWidtha">
                    <asp:TextBox ID="websitename" runat="server" BackColor="#EEEEE8" Width="250px"></asp:TextBox>
                  <%--  <asp:Label ID="websitenameLa" runat="server" />--%>
                </td>
            </tr>
            <tr>
                 <td class="tdBlue">
                    网站语言
                </td>
                <td class="tdWidtha">
                   <%-- <asp:Label ID="weblanguageLa" runat="server" />--%>
                    <asp:DropDownList ID="weblanguage" runat="server">
                        <asp:ListItem Value="中文">中文</asp:ListItem>
                        <asp:ListItem Value="英语">英语</asp:ListItem>
                        <asp:ListItem Value="德语">德语</asp:ListItem>
                        <asp:ListItem Value="俄语">俄语</asp:ListItem>
                        <asp:ListItem Value="法语">法语</asp:ListItem>
                        <asp:ListItem Value="日语">日语</asp:ListItem>
                        <asp:ListItem Value="韩语">韩语</asp:ListItem>
                        <asp:ListItem Value="阿尔巴尼亚语">阿尔巴尼亚语</asp:ListItem>
                        <asp:ListItem Value="阿拉伯语">阿拉伯语</asp:ListItem>
                        <asp:ListItem Value="爱尔兰语">爱尔兰语</asp:ListItem>
                        <asp:ListItem Value="爱沙尼亚语">爱沙尼亚语</asp:ListItem>
                        <asp:ListItem Value="白俄罗斯语">白俄罗斯语</asp:ListItem>
                        <asp:ListItem Value="保加利亚语">保加利亚语</asp:ListItem>
                        <asp:ListItem Value="冰岛语">冰岛语</asp:ListItem>
                        <asp:ListItem Value="波兰语">波兰语</asp:ListItem>
                        <asp:ListItem Value="波斯语">波斯语</asp:ListItem>
                        <asp:ListItem Value="布尔语(南非荷兰语)">布尔语(南非荷兰语)</asp:ListItem>
                        <asp:ListItem Value="丹麦语">丹麦语</asp:ListItem>
                        <asp:ListItem Value="芬兰语">芬兰语</asp:ListItem>
                        <asp:ListItem Value="格鲁吉亚语">格鲁吉亚语</asp:ListItem>
                        <asp:ListItem Value="古吉拉特语">古吉拉特语</asp:ListItem>
                        <asp:ListItem Value="海地克里奥尔语">海地克里奥尔语</asp:ListItem>
                        <asp:ListItem Value="荷兰语">荷兰语</asp:ListItem>
                        <asp:ListItem Value="加利西亚语">加利西亚语</asp:ListItem>
                        <asp:ListItem Value="加泰罗尼亚语">加泰罗尼亚语</asp:ListItem>
                        <asp:ListItem Value="捷克语">捷克语</asp:ListItem>
                        <asp:ListItem Value="韩语克罗">韩语克罗</asp:ListItem>
                        <asp:ListItem Value="意大利语">意大利语</asp:ListItem>
                        <asp:ListItem Value="越南语">越南语</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="tdBlue">
                    网站链接
                </td>
                <td class="tdWidtha">
                    <asp:TextBox ID="baseurl" runat="server" Width="250px"></asp:TextBox>
                    <asp:Label ID="baseurlLa" runat="server" Width="250px"/>
                </td>
            </tr>
            <tr>
                <td class="tdBlue">
                    中文标题XPath
                </td>
                <td class="tdWidtha">
                    <asp:TextBox ID="cntitle" runat="server" Width="250px"></asp:TextBox>
                    <asp:Label ID="cntitleLa" runat="server" Width="250px" />
                </td>
            </tr>
            <tr>
                 <td class="tdBlue">
                    英文标题XPath
                </td>
                <td class="tdWidtha">
                    <asp:TextBox ID="entitle" runat="server" Width="250px"></asp:TextBox>
                    <asp:Label ID="entitleLa" runat="server" Width="250px" />
                </td>
            </tr>
            <tr>
                 <td class="tdBlue">
                    单位名称XPath
                </td>
               <td class="tdWidtha">
                    <asp:TextBox ID="unit" runat="server" Width="250px"></asp:TextBox>
                    <asp:Label ID="unitLa" runat="server"  Width="250px"/>
                </td>
            </tr>
            <tr>
                <td class="tdBlue">
                    文理科XPath
                </td>
                <td class="tdWidtha">
                    <asp:TextBox ID="types" runat="server" Width="250px"></asp:TextBox>
                    <asp:Label ID="typesLa" runat="server" Width="250px" />
                </td>
            </tr>
            <tr>
                <td class="tdBlue">
                    作者姓名XPath
                </td>
                <td class="tdWidtha">
                    <asp:TextBox ID="author" runat="server" Width="250px"></asp:TextBox>
                    <asp:Label ID="authorLa" runat="server" Width="250px" />
                </td>
            </tr>
            <tr>
                <td class="tdBlue">
                    论著类型XPath
                </td>
                <td class="tdWidtha">
                    <asp:TextBox ID="articletype" runat="server" Width="250px"></asp:TextBox>
                    <asp:Label ID="articletypeLa" runat="server" Width="250px" />
                </td>
            </tr>
            <tr>
               <td class="tdBlue">
                    课题来源XPath
                </td>
               <td class="tdWidtha">
                    <asp:TextBox ID="source" runat="server" Width="250px"></asp:TextBox>
                    <asp:Label ID="sourceLa" runat="server"  Width="250px"/>
                </td>
            </tr>
            <tr>
                 <td class="tdBlue">
                    语言种类XPath
                </td>
               <td class="tdWidtha">
                    <asp:TextBox ID="language" runat="server" Width="250px"></asp:TextBox>
                    <asp:Label ID="laguageLa" runat="server" Width="250px" />
                </td>
            </tr>
            <tr>
                 <td class="tdBlue">
                    刊物名称XPath
                </td>
                <td class="tdWidtha">
                    <asp:TextBox ID="journalname" runat="server" Width="250px"></asp:TextBox>
                    <asp:Label ID="journalnameLa" runat="server" Width="250px" />
                </td>
            </tr>
            <tr>
                <td class="tdBlue">
                    发表时间XPath
                </td>
               <td class="tdWidtha">
                    <asp:TextBox ID="publishtime" runat="server" Width="250px"></asp:TextBox>
                    <asp:Label ID="publishtimeLa" runat="server" Width="250px" />
                </td>
            </tr>
            <tr>
                <td class="tdBlue">
                    刊物级别XPath
                </td>
                <td class="tdWidtha">
                    <asp:TextBox ID="levels" runat="server" Width="250px"></asp:TextBox>
                    <asp:Label ID="levelsLa" runat="server" Width="250px"/>
                </td>
            </tr>
            <tr>
              <td class="tdBlue">
                    收录类型XPath
                </td>
                <td class="tdWidtha">
                    <asp:TextBox ID="recordtype" runat="server" Width="250px"></asp:TextBox>
                    <asp:Label ID="recordtypeLa" runat="server"  Width="250px"/>
                </td>
            </tr>
            <tr>
                <td class="tdBlue">
                    是否启用规则
                </td>
                <td class="tdWidtha">
                    <asp:DropDownList ID="forbid" runat="server">
                        <asp:ListItem Value="0">禁用</asp:ListItem>
                        <asp:ListItem Value="1">启用</asp:ListItem>
                    </asp:DropDownList>
                  <%--  <asp:Label ID="forbidLa" runat="server" />--%>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:HyperLink ID="updateBt" runat="server" class="easyui-linkbutton" iconCls="icon-edit"
                        onclick="showHidden()">修改规则</asp:HyperLink>
                    <asp:HyperLink ID="saveBt" runat="server" class="easyui-linkbutton" iconCls="icon-save"
                        onclick="update()">保存</asp:HyperLink>
                    <asp:HyperLink ID="giveupBt" runat="server" class="easyui-linkbutton" iconCls="icon-cancel"
                        onclick="giveupp()">放弃</asp:HyperLink>
                    <asp:HyperLink ID="goback" runat="server" class="easyui-linkbutton" iconCls="icon-back"
                        onclick="javascript:window.location='xpathList.aspx'">返回列表</asp:HyperLink>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="id" runat="server" />
        <asp:HiddenField ID="sig" runat="server" Value="updateXPath" />
 
    </form>
    <script type="text/javascript">
        var notice = '<%=notice%>';
        if (notice != null && notice.length > 0) $.messager.alert('提示', notice, 'info');
    </script>
</body>
</html>

