<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="oneTreatise.aspx.cs" Inherits="JiaoShiXinXiTongJi.treatise.oneTreatise" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../themes/icon.css" />
    <script type="text/javascript" src="../js/jquery/jquery-1.4.4.min.js"></script>
    <script type="text/javascript" src="../js/jquery/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../js/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript">
        var title = '<%=title %>';
        $(function () {
            $("#details").panel('setTitle', title);
        });
    </script>
    <style type="text/css">
    html { 
    min-height: 100%; 
    height:100%; 
    } 
    body { 
    margin: 0; 
    padding: 0; 
    min-height: 100%; 
    height:100%; 
   } 
   .tdWidth{
       background-color:White;
       padding:5px;
       text-align:left;
       }
       .tdBlue{
           background-color:#ECF2FE;
           width:60px;
           margin-left:5px;
           padding:5px;
           text-align:right;
           }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="easyui-panel" id="details" title="treatise",style="width:100%;height:100%;">
       <table border="0" style="background-color: #BCD1EB; margin: 5px 5px 5px 5px;" width="99%">
           <tr>
               <td class="tdBlue">论文来源</td>
               <td class="tdWidth">
                   <asp:HyperLink ID="sourceWebSite" runat="server" Target="_blank"></asp:HyperLink>
               </td>
           </tr>
           <tr>
               <td class="tdBlue">
                   论文链接
               </td>
               <td class="tdWidth">
                   <asp:HyperLink ID="sourceUrl" runat="server" Target="_blank"></asp:HyperLink>
               </td>
           </tr>
           <tr>
               <td class="tdBlue">
                   中文标题
               </td>
               <td class="tdWidth">
                   <asp:Label ID="cnTitle" runat="server"></asp:Label>
               </td>
           </tr>
           <tr>
               <td class="tdBlue">
                   英文标题
               </td>
               <td class="tdWidth">
                   <asp:Label ID="enTitle" runat="server" ></asp:Label>
               </td>
           </tr>
           <tr>
               <td class="tdBlue">
                   文理科
               </td>
               <td class="tdWidth">
                   <asp:Label ID="types" runat="server"></asp:Label>
               </td>
           </tr>
           <tr>
               <td class="tdBlue">
                   作者
               </td>
               <td class="tdWidth">
                   <asp:Label ID="author" runat="server"></asp:Label>
               </td>
           </tr>
           <tr>
               <td class="tdBlue">
                   论著类型
               </td>
               <td class="tdWidth">
                   <asp:Label ID="articleType" runat="server"></asp:Label>
               </td>
           </tr>
           <tr>
               <td class="tdBlue">
                   课题来源
               </td>
               <td class="tdWidth">
                   <asp:Label ID="source" runat="server"></asp:Label>
               </td>
           </tr>
           <tr>
               <td class="tdBlue">
                   语言
               </td>
               <td class="tdWidth">
                   <asp:Label ID="language" runat="server"></asp:Label>
               </td>
           </tr>
           <tr>
               <td class="tdBlue">
                   刊物名称
               </td>
               <td class="tdWidth">
                   <asp:Label ID="journalname" runat="server"></asp:Label>
               </td>
           </tr>
           <tr>
               <td class="tdBlue">
                   发表时间
               </td>
               <td class="tdWidth">
                   <asp:Label ID="publishTime" runat="server"></asp:Label>
               </td>
           </tr>
           <tr>
               <td class="tdBlue">
                   刊物级别
               </td>
               <td class="tdWidth">
                   <asp:Label ID="levels" runat="server"></asp:Label>
               </td>
           </tr>
           <tr>
               <td class="tdBlue">
                   收录类型
               </td>
               <td class="tdWidth">
                   <asp:Label ID="recordType" runat="server"></asp:Label>
               </td>
           </tr>
           <tr>
               <td class="tdBlue">
                   分工类型
               </td>
               <td class="tdWidth">
                   <asp:Label ID="divideType" runat="server"></asp:Label>
               </td>
           </tr>
       </table>
       <div style="width:100%;text-align:right;"><a href="javascript:void(0);" onclick="javascript:window.location='myTreatise.aspx'" class="easyui-linkbutton" iconcls="icon-back">返回列表</a>&nbsp;</div>
    </div>
    </form>
</body>
</html>
