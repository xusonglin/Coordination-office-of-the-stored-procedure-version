<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HfUpdate.aspx.cs" Inherits="JiaoShiXinXiTongJi.Forum.HfUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    
<script type="text/javascript" src="../js/jss/Forum.js"></script>
</head>
<body>

    <table  style="width: 100%">
         <%   char[] c = {'&' };
              if (strs.Length > 0)
              {
                  string[] arrlist = strs.Split(c);
                  for (int i = 0; i < arrlist.Length; i++)
                  {
                      char[] d = { '_' };
                      string[] arr = arrlist[i].Split(d);
                         %>
        <tr>
            <td style="text-align: left" colspan="2">
              <span style="color: Blue"><%=arr[4]%></span>评论：<%=arr[1]%>
            </td>
          
        </tr>
        <tr>
        <td align=left ><%=arr[2]%></td>
            <td style="text-align: right" >
                <a href="javascript:void(0)"  onclick="deleteHf('<%=arr[0] %>','<%=arr[3] %>','<%=arr[5] %>')">删除</a>
            </td>
        </tr>
           <%}
              }%>
    </table>
    
</body>
</html>
