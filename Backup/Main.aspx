<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="JiaoShiXinXiTongJi._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>湖北师范学院协办同公系统</title>
    <link href="css/main.css" rel="stylesheet" type="text/css" />
    <link href="css/content.css" rel="stylesheet" type="text/css" />
    <link href="themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.4.2.js" type="text/javascript"></script>
    <script src="js/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="js/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="js/jss/MainBuju.js" type="text/javascript"></script>


    <script type="text/javascript">
            
         var role="<%=role %>";
         var Funs="<%=Funs %>";
         var sysFuns="<%=sysFuns %>"
         var sysFunsname = "<%=sysFunsname %>";
         
         var userid="<%=userid %>";
         var username="<%=username %>";
         var taskcount="<%=taskcount %>";

         $(function () {
             $.messager.show({
                 title: '提示',
                 msg: '你好:<span style="color:blue">'+username+'</span>&nbsp;&nbsp;，你有<span style="color:red">'+taskcount+'</span>条代办任务！'
             });
         })

		function tuichu()
		{
        
             window.location.href="Session.aspx";
		}
		function openPwd()
		{
		document.getElementById("win").style.visibility="visible";
		  $("#win").window({
                            closed: false,
                            title: "修改密码",
                            collapsible: false,
                            minimizable: false,
                            maximizable: false,
                            iconCls: "icon-add",
                            resizable: false,
                            width: 360,
                            height: 200
                        });}
                        
          function Change() {
          
		        var ypass = jQuery.trim(jQuery("#ypass").val());
                var xpass = jQuery.trim(jQuery("#xpass").val());
                var xxpass = jQuery.trim(jQuery("#xxpass").val());
                if(ypass=="")
                {     $.messager.alert('系统提示', "原密码不能为空！");
                return
                 }
                if(xpass=="")
                {     $.messager.alert('系统提示', "新密码不能为空！");
                return
                }
                if(xxpass=="")
                {     $.messager.alert('系统提示', "确认新密码不能为空！");
                return
                }
                if(xpass!=xxpass)
                {
                      $.messager.alert('系统提示', "确认新密码和新密码不相同！");
                      return
                }
                jQuery.ajax({
                    type: "POST",
                    url: "../Data/Handler.ashx",
                    data: "Action=ChangePass&ypass=" + ypass + "&xpass=" + xpass + "&userid=" + userid,
                    success: function(result) {
                  
                        if (result == "True") {
                            $.messager.alert('系统提示', "密码修改成功！");
                             $("#win").window("close");
                        }
                        else {
                            $.messager.alert('系统提示', "操作失败，请重试！");
                        }
                    }
                });
		
		}
		
		function closewin()
		{
            $("#win").window("close");
		}

    </script>
</head>
<body class="easyui-layout" style="background-color: #D7E6ED;" >
    <div id="header" region="north" border="false" style="height: 50px; overflow: hidden; width:100%; background-image:url(images/head.jpg); background-repeat: repeat-x; vertical-align:bottom" >
      
        <div style="float: right; margin: 6px auto; vertical-align:bottom">
            <table style="text-align: right; vertical-align:bottom">
            <tr  style="height:10px"><td colspan="3"></td></tr>
                <tr>
             
                    <td >
                        <span style="color: White; margin-right: 30px;">欢迎【<asp:Label ID="labRealName" runat="server"
                            ForeColor="black" Text="ADMIN"></asp:Label>】登录后台管理系统</span>
                    </td>
                    <td>
                        <a href="javascript:openPwd()" style="color: black">修改密码</a>
                      
                    </td>
                    <td style="padding-right: 20px">
                        <a href="javascript:tuichu()" style="color: black">安全退出</a>
                 
                    </td>
                </tr>
            </table>
        </div>
    </div>
 

    <div region="west" hide="true" split="true" title="导航菜单" style="width: 180px; "
        id="west">
        <div id="nav" class="easyui-accordion" fit="true" border="false" style="background-color:#E3E9EE"
            height: 300px">
        </div>
    </div>
    <div id="mainPanle" region="center" style="background: #eee; overflow-y:hidden;">
    
        <div id="tabs" class="easyui-tabs" fit="true" border="false" >
        </div>
        
    </div>
       <div id="south" region="south" split="true" hide="true">
            <span style=" color:Black">湖北师范学院协作办公系统管理平台 &nbsp;&nbsp;技术支持@ 湖北师范学院计算机科学与技术学院</span>
    </div>
    <div  class="easyui-window" modal="true" closed="true" id="win" style="text-align:center; visibility:hidden"  >
        <table>
            <tr>
                <td style="width: 100px; text-align: right; ">
                    原密码：
                </td>
                <td>
                    <input type="password" id="ypass" required="true" validtype="safepass"  style=" width:150px"/>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; text-align: right; ">
                    新密码：
                </td>
                <td>
                    <input type="password" id="xpass" required="true" validtype="safepass" style=" width:150px"/>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; text-align: right; ">
                    确认新密码：
                </td>
                <td>
                    <input type="password" id="xxpass" validtype="equalTo['#txtMC']" required="true" style=" width:150px"/>
                </td>
            </tr>
            <tr>
               
                <td colspan="2">
                   &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <a href="javascript:Change()" id="btnSave" class="easyui-linkbutton" iconcls="icon-ok">
                        保 存</a> <a href="javascript:closewin()" id="btnExit" class="easyui-linkbutton" iconcls="icon-no">
                            关 闭</a>
                </td>
            </tr>
        </table>
    </div>
   
    <form id="form1" runat="server" action="Main.aspx">
    <input type="submit" id="Submit" value="安全退出" style="visibility: hidden" />
    </form>

</body>
</html>
