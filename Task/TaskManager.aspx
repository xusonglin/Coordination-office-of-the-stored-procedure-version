<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaskManager.aspx.cs" Inherits="JiaoShiXinXiTongJi.Task.TaskManager" %>

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
    var username="<%=username %>";
    var userid="<%=userid %>";
    var search="";
        var grid;       
        $(function() {                                                           
            grid = $("#tt").datagrid({
                url: "../Data/Handler.ashx?userid="+userid,
                height: $(window).height()-30,
                width:"auto",
                pagination: true,
                rownumbers: true,
                singleSelect: true,
                frozenColumns: [[
               { field: 'ck', checkbox: true }
                ]],
                columns: [[
            { field: 'TaskID', title: '任务', width: 100, align: 'left',hidden:true  },
            { field: 'TaskName', title: '任务名称', width: 400, align: 'left' },
            { field: 'TaskMaker', title: '任务创建者', width: 120, align: 'center' },  
            { field: 'DateTime', title: '创建日期', width: 120, align: 'center',formatter:function(value,rowData,rowindex){
            var date = new Date(value.replace(/-/g, "/"));  
            return date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate();  
            } },
            { field: 'State', title: '发布状态', width: 80, align: 'center' }                        
            ]],
                queryParams: { Action: "Query" }
                
                });

                //去掉下面分页栏上的combobox，combobox总是显示在最上层，不知道怎么设置才好，就让它消失吧！
                var pager = $('#tt').datagrid('getPager');
                $(pager).pagination({ showPageList: false });

                //根据tag属性来调用不同的函数。
                parent.$("#win").find("#btnSave").bind("click", function() {
                    var tag = jQuery.trim(jQuery(this).attr("tag"));
                    if (tag == "add") {
                        //关闭窗口
                        parent.$("#win").window("close");
                        Add();
                        return;
                    } 
                    if (tag == "edit") {
                        parent.$("#win").window("close");
                        Save();
                        return;
                    }

                });
                parent.$("#win").find("#btnExit").bind("click", function() {
                    parent.$("#win").window("close");
                });


                //搜索按钮点击时触发的事件
                $("#btnSearch").click(function() {
                    var UrlName = jQuery.trim($("#txtName").val());
                    grid.datagrid({ url: 'Handler.ashx', queryParams: { Action: "Query", name: UrlName} });
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
                            type: "POST",
                            url: "../Data/Handler.ashx",
                            data: "Action=Delete&TaskID=" + TaskID+"&TaskName="+TaskName+"&username="+username,
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
            // 撤销发布
               CancelPass = function() {
                var rows = grid.datagrid('getSelections');
                var num = rows.length;
                if (num == 0) {
                    parent.$.messager.alert("系统提示", "请选择您要撤销的任务！", "warning");
                    return;
                }
           
                var TaskID = rows[0].TaskID;
            
                var TaskName = rows[0].TaskName;
                var TaskState = rows[0].State;
                if(TaskState=="未发布")
                {
                  parent.$.messager.alert("系统提示", "任务未发布不能撤销发布！", "warning");
                 return;
                }
                parent.$.messager.confirm("系统提示", "是否确定撤销任务【" + TaskName + "】的发布?", function(r) {
                    if (r) {
                        jQuery.ajax({
                            type: "POST",
                            url: "../Data/Handler.ashx",
                            data: "Action=CancelPass&TaskID=" + TaskID,
                            success: function(result) {
                                if (result) {
                                    grid.datagrid('reload');
                                    grid.datagrid('clearSelections');
                                    return;
                                }
                                parent.$.messager.alert("【" + name + "】任务撤销发布失败。");
                            }
                        });
                    }
                });
            }
        
            
            
            
    //搜索        
    function searchtask(){  
                    
                     search=$("#search").val();
                     grid.datagrid({ url: '../Data/TeaHandler.ashx', queryParams: { Action: "Query", search:search, userid:userid} }); //grid刷新
                     
                    }
            
            
     //查看任务完成情况
      function viewtask()
      {   
         var rows = grid.datagrid('getSelections');
         var num = rows.length;
         if (num == 0) {
            parent.$.messager.alert("系统提示", "请选择一条记录进行操作。", "warning");
            return;
          }
         var TaskID = rows[0].TaskID;
         window.showModalDialog("ViewTask.aspx?TaskID="+TaskID,window,"dialogWidth:1300px;status:no;dialogHeight:600px"); 
      }      
      
       
        //任务导出
    function taskdownload()
    {
        var rows = grid.datagrid('getSelections');
        var num = rows.length;
        if (num == 0) {
        parent.$.messager.alert("系统提示", "请选择一条记录进行操作。", "warning");
        return;
         }
       var TaskID = rows[0].TaskID;
       window.open ("DownLoad.aspx?TaskID="+TaskID);   
     }    
    </script>

</head>
<body>
  <div  fit="true" >
            <div region="north" border="false" style="border-bottom:1px solid #ddd;height:25px;padding:2px 5px;background:#DEE7FF;">
            <div style="float:left;">
               关键字：<input type="text" id="search">
                <a href="javascript:searchtask()" class="easyui-linkbutton" plain="true" icon="icon-search">查询</a>
                <a href="javascript:viewtask()" class="easyui-linkbutton" plain="true" icon="icon-add">查看任务完成情况</a>
                <a href="javascript:CancelPass()" class="easyui-linkbutton" plain="true" icon="icon-edit">撤销发布</a>
                <a href="javascript:Delete()" class="easyui-linkbutton" plain="true" icon="icon-cancel">删除任务</a>
                <a href="javascript:taskdownload()" class="easyui-linkbutton" plain="true" icon="icon-undo">任务导出</a>
     
            
          
            </div>
        </div>
        <div region="center" border="false">
            <table id="tt"></table>
            
        </div>
        </div>

</body>
</html>
