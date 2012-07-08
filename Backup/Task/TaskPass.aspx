<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaskPass.aspx.cs" Inherits="JiaoShiXinXiTongJi.Task.TaskPass" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="../css/update_info.css" />
    <link href="../themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-1.4.2.js" type="text/javascript"></script>
    <script src="../js/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../js/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <link href="../css/content.css" rel="stylesheet" type="text/css" />
    <script src="../js/jss/Date.js" type="text/javascript"></script>
    <script type="text/javascript">
             var  XX="<%=XX %>";
             var  XY="<%=XY %>";
        $().ready(function() {
            // 自适应
            $("#div_tree").tree({
                url: '../Data/GetTask_.ashx?userid='+userid
            });
              $("#div_user").tree({
                 url: '../Data/GetSchool.ashx?XX='+XX+'&XY='+XY
            });

        });
             
        function setCalendar(obj, ID, format){
			popUpCalendar(obj, ID, format);
			return false;
             }

       function dateformat(value){
            var date = new Date(value);  
            return date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate();  
                         }

      function PassTask()
         {
                var startdate=document.getElementById('starttime').value;
                var enddate=document.getElementById('endtime').value;
                var arr=startdate.split("-");    
                var starttime=new Date(arr[0],arr[1],arr[2]);    
                var starttimes=starttime.getTime();   
                var arrs=enddate.split("-");    
                var lktime=new Date(arrs[0],arrs[1],arrs[2]);    
                var lktimes=lktime.getTime(); 
                if(starttimes>lktimes)    
                {   
                alert("任务开始时间不能大于任务结束时间！！") 
                }  
                else 
                document.getElementById("Submit").click ();
      }
    </script>

    <script type="text/javascript">
      var userid="<%=userid %>";
        function showwin()
       {
       
    $("#tree_user").window({
                            title: "用户列表",
                            closed: false,
                            collapsible: false,
                            top:($(window).height() - 420) * 0.5,   
                            left:($(window).width() - 450) * 0.5,
                            minimizable: false,
                            maximizable: false,
                            iconCls: "icon-add",
                            resizable: false,
                            width: 300,
                            height: 400,
                            hasbackdiv : true,   
                            istop:true  ,
                            expanded:false

                        });
       
  
          }

    function showwintask()
     {

      $("#tree").window({
                            title: "任务列表",
                            closed: false,
                            collapsible: false,
                           top:($(window).height() - 420) * 0.5,   
                            left:($(window).width() - 450) * 0.5,
                            minimizable: false,
                            maximizable: false,
                            iconCls: "icon-add",
                            resizable: false,
                            width: 320,
                            height: 450,
                            hasbackdiv : true,   
                             istop:true  

                        });
          
     }
        
        function GetNode(t)
        {
        
            if(t==1)
            {
             var node = $('#div_tree').tree('getChecked');
                var chilenodes = '';
                var chilenodesid = '';
                var parantsnodes = '';
                var prevNode = '';
                for (var i = 0; i < node.length; i++) {
                    if ($('#div_tree').tree('isLeaf', node[i].target)) {
                        chilenodes += node[i].text + ',';
                         chilenodesid += node[i].id + ',';
                     
                    }}
             
             
             }
                      if(t==2)
            {
             var node = $('#div_user').tree('getChecked');
                var chilenodes = '';
                var chilenodesid = '';
                var parantsnodes = '';
                var prevNode = '';
                for (var i = 0; i < node.length; i++) {
                    if ($('#div_user').tree('isLeaf', node[i].target)) {
                        chilenodes += node[i].text + ',';
                         chilenodesid += node[i].id + ',';
                    }}
             
             
             }
             
        strids= chilenodesid.substring(0,chilenodesid.length-1);        
        strtext= chilenodes.substring(0,chilenodes.length-1);           
                    
         var str=strids+"$"+strtext;

        if(str.length==1)
        alert ("请选中用户！！")
        else {
          var arr=str.split("$");
          var strid=arr[0];
          var strtext=arr[1];
          if(t==1)
          {
           document .getElementById ("taskname").value =strtext ;
           document .getElementById ("taskid").value =strid ;
                $("#tree").window("close");
           }
           
            if(t==2)
           { document .getElementById ("username").value =strtext ;
            document .getElementById ("userid").value =strid ;}
          $("#tree_user").window("close");
          }

        }

     function quxiao(t)
        {
        if(t==1)
        {
   $("#tree").window("close");
        }
        if(t==2)
        {
   $("#tree_user").window("close");}
   
        }

    </script>

</head>
<body  style="background-color:#D5DEE8">
    <form id="form1" runat="server" action="TaskPass.aspx">
    <%-- 顶部填充 --%>
    <div style="height:40px;"></div>
    <div>
        <asp:Label ID="userid_" runat="server" Visible="false"></asp:Label>
        <div style="width:100%">
            <table id="table" >
                <tr>
                    <td style="width: 80px; text-align: left">
                        <asp:Label ID="tta" CssClass="clsc" runat="server" Text="选择任务："></asp:Label>
                    </td>
                    <td style="width: 520px; text-align: left">
                        <input id="taskname" name="taskname" type="text" style="width: 400px; background-color: #FFFFCC;"
                            readonly="readonly" />
                        <input id="taskid" name="taskid" type="hidden" />
                        <a href="javascript:void(0);" class="easyui-linkbutton" iconCls="icon-add"  onclick="showwintask()">选择任务</a>
                        <%--<input id="selecttask" type="button" value="选择任务" onclick="showwintask()"  />--%>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px; text-align: left">
                        <asp:Label ID="syo" runat="server" Text=" 选择用户："></asp:Label>
                    </td>
                    <td style="width: 520px; text-align: left">
                        <input type="text" name="username" id="username" style="width: 400px" readonly="readonly"
                            style="background-color:#FFFFCC;" />
                        <input type="hidden" name="userid" id="userid" />
                        <a href="javascript:void(0);" class="easyui-linkbutton" iconCls="icon-add"  onclick="showwin()">选择用户</a>
                        <%--<input id="Button4" type="button" value="选择用户" onclick="showwin()" />--%>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px; text-align: left">
                        <asp:Label ID="Label1" runat="server" Text="任务类型："></asp:Label>
                    </td>
                    <td style="width: 300px; text-align: left">
                        <asp:DropDownList ID="DropDownList_lx" runat="server" Style="background-color: #FFFFCC;">
                            <asp:ListItem Value="1">临时任务</asp:ListItem>
                            <asp:ListItem Value="2">年度任务</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px; text-align: left">
                        <asp:Label ID="starttime11" runat="server" Text="开始时间："></asp:Label>
                    </td>
                    <td style="width: 320px; text-align: left">
                        <input type="text" name="starttime" id="starttime" style="width: 200px; background-color: #FFFFCC;"
                            readonly="readonly" onclick="javascript:ShowCalendar(this.id)" />
                            <a href="javascript:void(0);" class="easyui-linkbutton" iconCls="icon-add"  onclick="javascript:ShowCalendar('starttime')">选择时间</a>
                        <%--<input id="Button1" type="button" value="选择时间" onclick="javascript:ShowCalendar('starttime')" />--%>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px; text-align: left">
                        <asp:Label ID="endtime22" runat="server" Text="结束时间："></asp:Label>
                    </td>
                    <td style="width: 320px; text-align: left">
                        <input type="text" name="endtime" id="endtime" style="width: 200px; background-color: #FFFFCC;"
                            readonly="readonly" onclick="javascript:ShowCalendar(this.id)" />
                            <a href="javascript:void(0);" class="easyui-linkbutton" iconCls="icon-add"  onclick="javascript:ShowCalendar('endtime')">选择时间</a>
                            <%--<input id="Button3" type="button" value="选择时间" onclick="javascript:ShowCalendar('endtime')" />--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="desedd" runat="server" Text="任务描述："></asp:Label>
                        <input type="submit" id="Submit1" value="发布任务" style="visibility: hidden" />
                 
                        <%--<input id="Button5" type="button" value="发布任务" onclick="PassTask()" /> --%>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <textarea id="taskdes" name="taskdes" style="width:600px; height: 160px" cols="20"
                            rows="1"></textarea>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="right">
                        <%--填充
                        <div style="height:20px;"></div>
                        <input type="submit" id="Submit" value="发布任务" style="visibility: hidden" />
                        <a href="javascript:void(0);" class="easyui-linkbutton" iconCls="icon-add"  onclick="PassTask()">发布任务</a>
                        <%--<input id="Button5" type="button" value="发布任务" onclick="PassTask()" /> --%>
                        <input type="submit" id="Submit" value="发布任务" style="visibility: hidden" />
                        <a href="javascript:void(0);" class="easyui-linkbutton" iconCls="icon-ok"  onclick="PassTask()">发布任务</a>
                 
                    </td>
                </tr>
            </table>
        </div>


        <div id="tree" class="easyui-window" modal="true" closed="true">
            <table>
                <tr>
                    <td>
                        <div id="div_tree" checkbox="true">
                        </div>
                    </td>
                </tr>
                <tr align="right">
                    <td style="padding-right: 10px">
                        <input type="button" value="确定" onclick="GetNode(1)" />
                        <input type="button" value="取消" onclick="quxiao()" />
                    </td>
                </tr>
            </table>
        </div>

        <div id="tree_user" class="easyui-window" modal="true" closed="true" >
            <table>
                <tr>
                    <td>
                        <div id="div_user" checkbox="true">
                        </div>
                    </td>
                </tr>
                <tr align="right">
                    <td style="padding-right:10px;">
                        <input type="button" value="确定" onclick="GetNode(2)" />
                        <input type="button" value="取消" onclick="quxiao()" />
                    </td>
                </tr>
            </table>
        </div>
        </div>
    </form>
</body>
</html>
