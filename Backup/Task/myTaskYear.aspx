<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myTaskYear.aspx.cs" Inherits="JiaoShiXinXiTongJi.Task.myTaskYear" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-1.4.2.js" type="text/javascript"></script>
    <script src="../js/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../js/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <link href="../css/content.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
    var search="";
     var userid="<%=userid %>";
    function dateformat(value){
                var date = new Date(value);  
                return date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate();  
       }
    
        var grid;       
        $(function() {                                                           
            grid = $("#tt").datagrid({
                url: "../Data/Handler.ashx?userid="+userid,
                height: $(window).height()-30,
                width:"auto",
                pagination: true,
                rownumbers: true,
                singleSelect: true,
                columns: [[
            { field: 'TaskID', title: '任务', width: 100, align: 'left',hidden:true  },
            { field: 'TaskName', title: '任务名称', width: 360, dataIndex:'TaskID',align: 'left',formatter:function(value,rowData,rowindex)
            {
            
                   var	startime =	new	Date(rowData.StartTime.replace(/-/g,"/"));
				var	endtime =	new	Date(rowData.EndTime.replace(/-/g,"/"));
                    var now= new Date();
                    var year = now.getFullYear();
                    var month=now.getMonth()+1;
                    var day=now.getDate();
                    var hour=now.getHours();
                    var minute=now.getMinutes();
                    var second=now.getSeconds();
                 
                    var nowtime=year+"-"+month+"-"+day;
                    
                    nowtime=new	Date(nowtime.replace(/-/g,"/"));
                  
			      var	diff = parseInt((endtime - nowtime)/ (1000*60*60*24))+1;
				
				  var   result="<a href='TaskInfo.aspx?id="+rowData.TaskID+"' ><font color=blue>"+value+"</font></a>" ;
				    if(diff<1)
				    {
				  	     result="<a onclick=javasctipt:alert('任务已结束，如有问题请联系管理员！！') ><font color='red'>"+value+"</font></a>"  			
				    }
            
                    return result
            
            
            }
            
            
             },
            
            { field: 'TaskMaker', title: '任务发布者', width: 120, align: 'center' },  
            { field: 'StartTime', title: '开始时间', width: 100, align: 'center',formatter:function(value,rowData,rowindex){
            var date = new Date(value.replace(/-/g, "/"));  
            return date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate();  
            } },
            { field: 'EndTime', title: '结束时间', width: 100, align: 'center',formatter:function(value,rowData,rowindex){
            var date = new Date(value.replace(/-/g, "/"));  
            return date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate();  
            } },
            { field: 'State_', title: '完成情况', width: 80, align: 'center',formatter:function(value,rowData,rowindex)
            {
            
                   var	startime =	new	Date(rowData.StartTime.replace(/-/g,"/"));
				var	endtime =	new	Date(rowData.EndTime.replace(/-/g,"/"));
				
                    var now= new Date();
                    var year = now.getFullYear();
                    var month=now.getMonth()+1;
                    var day=now.getDate();
                    var hour=now.getHours();
                    var minute=now.getMinutes();
                    var second=now.getSeconds();
                 
                    var nowtime=year+"-"+month+"-"+day;
                    
                    nowtime=new	Date(nowtime.replace(/-/g,"/"));
                  
			      var	diff = parseInt((startime - nowtime)/ (1000*60*60*24))+1;
				
				  var   result="<a href='TaskInfo.aspx?id="+rowData.TaskID+"' ><font color=blue>"+value+"</font></a>" ;
				    if(diff>1)
				    {
				  	         result= "<font  color=blue> 任务未开始</font >";   	
				    }
              var	difff = parseInt((endtime - nowtime)/ (1000*60*60*24))+1;
                    if(difff<1)
				    {
				  	          result= "<font  color=red> 任务已结束</font >";   
				    }
            
                    return result
                        } }                          
            ]],
                queryParams: { Action: "myQueryyear" }

                });

            });

         
            //删除信息
            Delete = function() {
                var rows = grid.datagrid('getSelections');
                var num = rows.length;
                if (num == 0) {
                    parent.$.messager.alert("系统提示", "请选择您要删除的任务！", "warning");
                    return;
                }
                var TaskID = rows[0].TaskID;
                var TaskName = rows[0].TaskName;
                parent.$.messager.confirm("系统提示", "是否确定删除任务【" + TaskName + "】的信息?", function(r) {
                    if (r) {
                        jQuery.ajax({
                            type: "GET",
                            url: "../Data/Handler.ashx",
                            data: "Action=myyearDelete&TaskID=" + TaskID+ "&userid=" + userid,
                            success: function(result) {
                                if (result) {
                                    grid.datagrid('reload');
                                    grid.datagrid('clearSelections');
                                    return;
                                }
                                parent.$.messager.alert("【" + TaskName + "】学生信息删除失败。");
                            }
                        });
                    }
                });
            }

           
           
 refesh=function(){
 grid.datagrid('reload');
 grid.datagrid('clearSelections');
}


function searchrenyuan() {
             search = $("#search").val();
             grid.datagrid({ url: '../Data/Handler.ashx', queryParams: { Action: "myQueryyear", search: search, userid: userid} }); //grid刷新

   }
   
//$(window).resize(function(){
//$('#tt').datagrid('resize', {
//width:function(){return document.body.clientWidth;},
//height:function(){return document.body.clientHeight;}
//});
//});

 
    </script>

</head>
<body>
 
 <div  fit="true" >
            <div region="north" border="false" style="border-bottom:1px solid #ddd;height:25px;padding:2px 5px;background:#DEE7FF;">
            <div style="float:left;">
               <span  style="font-size:15px">关键字(任务名称)：</span><input type="text" id="search"  style=" height:15px ; width:200px">
                <a href="javascript:searchrenyuan()" class="easyui-linkbutton" plain="true" icon="icon-search">搜索</a>
                <a href="javascript:Delete()" class="easyui-linkbutton" plain="true" icon="icon-cancel">删除</a>
                <a href="javascript:refesh()" class="easyui-linkbutton" plain="true" icon="icon-reload">刷新</a>
            </div>
        </div>
        <div region="center" border="false">
            <table id="tt"></table>
            
        </div>
        </div>

</body>
</html>
