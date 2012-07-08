<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="JiaoShiXinXiTongJi.CourseRemind.List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>全部课程信息</title>
 <link href="../themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-1.4.2.js" type="text/javascript"></script>
    <script src="../js/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../js/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <link href="../css/content.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            var is = false;
            load(is, "", "");

        });

        function load(is, n, v) {
            var par = "";
            if (is) {
                par = { sig: 'getSearch', name: n, value: v };
            } 
       
            else {
                par = { sig: 'getList' };
            }
            $("#cr").datagrid({
                url: 'CourseRemindHandler_.ashx',
                queryParams: par,
              //  title: '课程表（双击某一行编辑具体信息）',
                fitColumns: true,
                striped: true,
                loadMsg: '数据加载中，请稍微...',
                remoteSort: false,
                 rownumbers: true,
                singleSelect: false,
                 height: $(window).height()-30,
                width:"auto",
                 frozenColumns: [[
                { field: 'ck', checkbox: true }
            ]],
                columns: [[
                { field: 'Id', title: 'ID', width: 40, resizable: false, sortable: true,hidden:true },
                            {field: 'Course_Name', title: '课程名称', width: 80, resizable: false, sortable: true },
                            { field: 'Staff_num', title: '职工号', width: 30, resizable: false, sortable: true },
                            { field: 'Teacher_Name', title: '教师姓名', width: 30, align: 'left', resizable: false, sortable: true },
                            { field: 'Class_Time', title: '周', width: 30, resizable: false },
                            { field: 'Class_Week', title: '节', width: 30, align: 'left', resizable: false },
                            { field: 'Total_Week', title: '周数', width: 30, align: 'left', resizable: false },
                            { field: 'Is_Week', title: '单双周', width: 30, align: 'left', resizable: false },
                            { field: 'Class_Addr', title: '上课地点', width: 60, align: 'left', resizable: false },
                            { field: 'Hours', title: '总学时', width: 30, align: 'left', resizable: false },
                            { field: 'Department', title: '开课学院', width: 120, align: 'left', resizable: false },
                            { field: 'Classes', title: '教学班组成', width: 60, align: 'left', resizable: false }

                        ]],
                pagination: true,
                rownumbers: true,
                pageSize: 20
//                toolbar: [{
//                    iconCls: "icon-add",
//                    text: "添加",
//                    handler: function () {
//                        // var row = $("#cr").datagrid("getSelected");
//                        // alert(row.Id);
//                        window.location = "Add.aspx";
//                    }
//                }, {
//                    iconCls: "icon-remove",
//                    text: "查看个人课表",
//                    handler: function () {
//                        var row = $("#cr").datagrid("getSelected");
//                        var name = "Staff_num";
//                        var value = row.Staff_num;
//                        window.location = "PerCourse.aspx?name=" + name + "&value=" + value;
//                    }
//                }, {

//                    text: '<input id="ss"></input><div id="mm" style="width:100px"><div name="Teacher_Name">教师姓名</div><div name="Staff_num">教师职工号</div><div name="Course_Name">课程名称</div></div>'

//                }],
               , onDblClickRow: function (rowIndex, rowData) {

                    window.location = "Edit.aspx?Id=" + rowData.Id;
                },
                rowStyler: function (value, rowData, rowIndex) {
                    return "cursor:pointer";
                },
                onLoadError: function () {
                    $.messager.alert('错误', '对不起，数据加载失败！请重试或者联系管理员', 'error');
                }

            });
//            $("#ss").searchbox({
//                width: 300,
//                searcher: function (value, name) {
//                    var iss = true;
//                    load(iss, name, value);
//                },
//                menu: "#mm",
//                prompt: "请输入查询条件"
//            });
        }
        
        
       //添加 
        function add(){ window.location = "Add.aspx";}
        //查看
        function viewperson()
        {
             var row = $("#cr").datagrid("getSelected");
             var name = "Staff_num";
             var value = row.Staff_num;
             window.location = "PerCourse.aspx?name=" + name + "&value=" + value;
        }
        //搜索
        function search()
        {
            var name=$("#name").val();
            var value=$("#value").val();
             var iss = true;
             load(iss, name, value);
        }
        //修改
        function edti()
        {
            var rows =  $('#cr').datagrid('getSelections');
            var num = rows.length;
            if (num == 0) {
                $.messager.alert("系统提示", "请选择一条记录进行操作。", "warning");
                return;
            }
            if (num > 1) {
                $.messager.alert("系统提示", "您选择了多条记录，只能选择一条记录进行修改。", "warning");
                return;
            }
        
        window.location = "Edit.aspx?type=edite&id="+rows[0].Id;
        
        }
        //删除
        function delet()
        {
             var rows =  $('#cr').datagrid('getSelections');
                var num = rows.length;
                if (num == 0) {
                    $.messager.alert("系统提示", "请选择记录进行删除!", "warning");
                    return;
                }
            
                var userid=rows[0].Id;
                var i=0;
                for(;i<rows.length;i++)
                   {
                    if(i==0)
                     ids="'"+rows[i].Id+"'";
                   else
                    ids=ids+","+"'"+rows[i].Id+"'";
                   }
                 
       $.messager.confirm("系统提示", "是否确定删除选中的记录?", function(r) {
                    if (r) {
                        jQuery.ajax({
                            type: "POST",
                            url: "CourseRemindHandler_.ashx",
                            data: "sig=deletedata&&ids=" + ids,
                            success: function(result) {
                                if (result) {
                                    $('#cr').datagrid('reload');
                                    $('#cr').datagrid('clearSelections');
                                    return;
                                }
                                $.messager.alert("记录删除失败!");
                            }
                        });
                    }
                });
        
        }
        
    </script>
</head>
<body>

 <div  fit="true" >
            <div region="north" border="false" style="border-bottom:1px solid #ddd;height:25px;padding:2px 5px;background:#DEE7FF;">
            <div style="float:left;" >
               <span style="font-size:15px">教师姓名&nbsp;</span><input type="text" id="name"   style="height:15px"><span style="font-size:15px">&nbsp;&nbsp;&nbsp;职工编号&nbsp;   </span><input type="text" id="value" style="height:15px">
                <a href="javascript:search()" class="easyui-linkbutton" plain="true" icon="icon-search">搜索</a>
                <a href="javascript:add()" class="easyui-linkbutton" plain="true" icon="icon-add">添加</a>
                   <a href="javascript:edti()" class="easyui-linkbutton" plain="true" icon="icon-edit">修改</a>
                      <a href="javascript:delet()" class="easyui-linkbutton" plain="true" icon="icon-cancel">删除</a>
                <a href="javascript:viewperson()" class="easyui-linkbutton" plain="true" icon="icon-reload">查看个人课表</a>
            </div>
        </div>
        <div region="center" border="false">
        <table id="cr">
    </table>
            
        </div>
        </div>


</body>
</html>

