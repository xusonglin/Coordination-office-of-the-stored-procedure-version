<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForumTest.aspx.cs" Inherits="JiaoShiXinXiTongJi.Forum.ForumTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="../themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-1.4.2.js" type="text/javascript"></script>
    <script src="../js/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../js/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <link href="../css/content.css" rel="stylesheet" type="text/css" />
    <script src="../js/prototype.js" type="text/javascript"></script>

    <script type="text/javascript">
    var  strs="<%=strs%>";
   
    var  userid="<%=userid%>";
    
    </script>

</head>
<body style="text-align: center;" >
    <form id="form1">
    <table style="width: 80%; text-align:center" border="1" align="center" >
    <tr><td> 
        <img src="../images/Forum/Forumlogo.gif" /></td></tr>
        <tr><td align="left"><span ></span></td></tr>
        <tr>
            <td style="width: 100%">
                <input type="text"  id="liuyan" style=" height:40px; width:100%" value="最近在忙些什么呢.......赶快和大家分享一下吧！" onclick="javascript:this.value=''"/>
            </td>
        </tr>
        <tr>
            <td align="right">
               <a href="#"   onclick="fabiao()">发表</a>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 100%">
                            <table border="0" style="width: 100%">
                                <%   char[] c = { '&' };
                                     if (strs.Length > 0)
                                     {
                                         string[] arrlist = strs.Split(c);
                                         for (int i = 0; i < arrlist.Length; i++)
                                         {
                                             char[] d = { '_' };
                                             string[] arr = arrlist[i].Split(d);
                                 %>
                                <tr style="width: 100%; height: 100%;">
                                    <td style="width: 20%; height: 30%; vertical-align: top; text-align: left" colspan="2">
                                        <span id="lyname" style="color: Blue">
                                            <%=arr[4]%></span>留言： <%=arr[1]%>
                                    </td>
                                
                                </tr>
                                <tr>
                                    <td style="text-align: left">
                                        <%=arr[2]%>
                                    </td>
                                    <td style="text-align: right">
                                     <a href="javascript:void(0)" onclick="btnpinglun('<%=arr[0] %>')"  >评论</a>
                         
                                     <a href="javascript:void(0)" onclick="deleteliuyan('<%=arr[0] %>','<%=arr[3] %>')" >删除</a>
                               
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-left: 20px; width: 100%">
                                        <input  type="text"   id="txt_pinglun<%=arr[0] %>"  style=" display:none; height:30px; width:100%"></input>
                                    </td>
                                </tr>
                                <tr id="tr<%=arr[0] %>" style=" display:none" >
                                    <td align="right" colspan="2"  >
                                 
                                              <a href="javascript:void(0)" onclick="btntijiao('<%=arr[0] %>')"   id="bt_tijiao<%=arr[0] %>">提交</a>
                                               <a href="javascript:void(0)" onclick="btnquxiao('<%=arr[0] %>')"  id="bt_quxiao<%=arr[0] %>">取消</a>
                                  
                                    </td>
                                </tr>
                                <tr>
                                    <td id="Hf_<%=arr[0] %>" colspan="2" style=" padding-left:20px">
                                    </td>
                                </tr>
                                
                                <tr style="height:20px"> <td colspan="2">  <hr /></td></tr>
                                <%}
                                     }%>
                            </table>
                        </td>
                    </tr>
             
                    
                </table>
            </td>
        </tr>
    </table>

    </form>
  <form action="ForumTest.aspx"  method="post" runat="server">

    <input  type="hidden" id="Content" name="Content" value=""/>
    
    <input  type="hidden" id="CountID" name="CountID" value=""/>
    
    <input  type="hidden" id="type" name="type" value=""/>

    <input type="submit" id="submit" style="display:none"/>
   
    
    </form>
</body>
</html>

<script type="text/javascript" src="../js/jss/Forum.js"></script>

<script type="text/javascript">

 LoadHf()
</script>

