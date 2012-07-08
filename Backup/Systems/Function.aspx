<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Function.aspx.cs" Inherits="JiaoShiXinXiTongJi.Systems.Function" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>教师信息统计系统</title>
    <link href="../css/main.css" rel="stylesheet" type="text/css" />
    <link href="../css/content.css" rel="stylesheet" type="text/css" />
    <link href="../themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-1.4.2.js" type="text/javascript"></script>
    <script src="../js/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../js/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script type="text/javascript">
       var functionid="";
       var XY="<%=XY%>";
       var XX="<%=XX%>";
       var roleid="<%=roleid %>";
        $().ready(function() {
            // 自适应
               $("#div_tree").tree({
                url: '../Data/GetFunction.ashx?XX='+XX+'&XY='+XY+'&roleid='+roleid,
                onClick: function(node) {
                    var title = node.text;
                    var id = node.id;
                    functionid=id;
                    grid.datagrid({ url: '../Data/Handler.ashx', queryParams: { Action: "myQueryfun", parentid:id} }); //grid刷新
           
                }
            });

        });
        // 添加选项卡
        AddTab = function(title, href) {
            if (href) {
                var content = '<iframe scrolling="no" frameborder="0" src="' + href + '" style="width:100%;height:100%;"></iframe>';
            } else {
                var content = '未实现';
            }
            $('#main-center').tabs('add', {
                title: title,
                closable: true,
                content: content
            });
        }
      
        var grid;       
        $(function() {                                                           
            grid = $("#tt").datagrid({
                url: "../Data/Handler.ashx",
               height: $(window).height()-30,
                width:"auto",
                pagination: true,
                rownumbers: true,
                singleSelect: true,
                  frozenColumns: [[
                    { field: 'ck', checkbox: true }
                ]],
                columns: [[
            { field: 'FunctionID', title: '任务', width: 100, align: 'left',hidden:true  },
            { field: 'FunctionName', title: '功能名称', width: 360, dataIndex:'TaskID',align: 'left' },
            { field: 'FunctionDesc', title: '功能描述', width: 200, align: 'center' },  
            { field: 'Url', title: '页面链接', width: 200, align: 'center' }
                                     
            ]],
                queryParams: { Action: "myQueryfun" },    //当请求远程数据时，发送的额外参数。
                toolbar: [  {
                    text: '添加',
                    iconCls: 'icon-add',
                    handler: function() {
                        $("#win").window({
                            title: "添加功能信息",
                            closed: false,
                            collapsible: false,
                           top:($(window).height() - 420) * 0.5,   
                            left:($(window).width() - 450) * 0.5,
                            minimizable: false,
                            maximizable: false,
                            iconCls: "icon-add",
                            resizable: false,
                            width: 286,
                            height: 220
                        });
                        $("#win").find("#btnSave").attr("tag", "add");  //给添加按钮设置tag属性，以便区分
                        $('#win input').each(function() {
                            if ($(this).attr('required') || $(this).attr('validType'))
                                $(this).validatebox();
                        });
                    }
                }, '-', {
                    text: '修改',
                    iconCls: 'icon-edit',
                    handler: Edit
                }, '-', {
                    text: '删除',
                    iconCls: 'icon-cancel',
                    handler: Delete
}, '-', {
                    text: '刷新',
                    iconCls: 'icon-reload',
                    handler: refesh
                }]
                });

                //去掉下面分页栏上的combobox，combobox总是显示在最上层，不知道怎么设置才好，就让它消失吧！
                var pager = $('#tt').datagrid('getPager');
                $(pager).pagination({ showPageList: false });

                //根据tag属性来调用不同的函数。
                $("#win").find("#btnSave").bind("click", function() {
                    var tag = jQuery.trim(jQuery(this).attr("tag"));
                    if (tag == "add") {
                    var test=true;
                    
                    
                
                     if (!$("#funname").validatebox('isValid')) 
                     {  test=false }
                     
                      if(test==true)
                       {    
                          $("#win").window("close");
                          Add();
                          return;      
                        }  
                               
                    }
                    if (tag == "edit") {
                        var test=true;
                
                     if (!$("#funname").validatebox('isValid')) 
                     {  test=false }
                     if(test==true)
                     {
                      $("#win").window("close");
                          Save();
                        return;
                        }
                    }

                });
                $("#win").find("#btnExit").bind("click", function() {
                   $("#win").window("close");
                });

                //搜索按钮点击时触发的事件
                $("#btnSearch").click(function() {
                    var UrlName = jQuery.trim($("#txtName").val());
                    grid.datagrid({ url: 'Handler.ashx', queryParams: { Action: "Query", name: UrlName} });
                });
            });

            Add = function() {                                                //添加系统功能
                var Name = $.trim($("#funname").val());
                var desc = $.trim($("#fundesc").val());
                var url = $.trim($("#funurl").val());
                jQuery.ajax({
                    type: "POST",           //用GET方式提交显示在数据库中的中文字符是乱码，换成POST就没问题了。
                    url: "../Data/Handler.ashx",
                    data: "Action=Addfun&funname=" + Name + "&fundesc=" + desc + "&funurl=" + url + "&funid=" + functionid,
                    success: function(result) {
                        if (result == "true") {
                            parent.$.messager.alert('系统提示', "添加成功！");
                            grid.datagrid('reload');
                        }
                        else {
                            parent.$.messager.alert('系统提示', "已经存在,重新添加！");
                        }
                    }
                });
            }

            Edit = function() {                                            //修改系统功能
             $("#win").find("#btnSave").attr("tag", "edit");
                var rows = grid.datagrid('getSelections');
                var num = rows.length;
                if (num == 0) {
                    parent.$.messager.alert("系统提示", "请选择一条记录进行操作。", "warning");
                    return;
                }
                if (num > 1) {
                    parent.$.messager.alert("系统提示", "您选择了多条记录，只能选择一条记录进行修改。", "warning");
                    return;
                }
               $("#win").window({
                    title: "修改功能信息",
                    closed: false,
                    collapsible: false,
                    top:($(window).height() - 420) * 0.5,   
                    left:($(window).width() - 450) * 0.5,
                    minimizable: false,
                    maximizable: false,
                    iconCls: "icon-edit",
                    resizable: false,
                    width: 286,
                    height: 220
                });
                     $("#win").find("#btnSave").attr("tag", "edit");  //给添加按钮设置tag属性，以便区分
                        $('#win input').each(function() {
                            if ($(this).attr('required') || $(this).attr('validType'))
                                $(this).validatebox();
                        });
              $("#funname").val(rows[0].FunctionName);
              $("#fundesc").val(rows[0].FunctionDesc);
              $("#funurl").val(rows[0].Url);

            }

            // 修改信息保存
            Save = function() {
                var rows = grid.datagrid('getSelections');
                var selefunid = rows[0].FunctionID;
                var Name = $.trim($("#funname").val());
                var desc = $.trim($("#fundesc").val());
                var url = $.trim($("#funurl").val());
                jQuery.ajax({
                    type: "POST",
                    url: "../Data/Handler.ashx",
                    data: "Action=Editfun&funname=" + Name + "&fundesc=" + desc + "&funurl=" + url + "&funid=" +selefunid ,
                    success: function(result) {
                        if (result == "true") {
                            parent.$.messager.alert('系统提示', "信息修改成功。", "info");
                            grid.datagrid('reload');
                        }
                        else {
                            parent.$.messager.alert('系统提示', "信息修改失败。", "info");
                        }
                    }
                });
            }

            //删除信息
            Delete = function() {
                var rows = grid.datagrid('getSelections');
                var num = rows.length;
                if (num == 0) {
                    parent.$.messager.alert("系统提示", "请选择您要删除的功能！", "warning");
                    return;
                }
                var functionid=rows[0].FunctionID;
                var functionname = rows[0].FunctionName;
                parent.$.messager.confirm("系统提示", "是否确定删除选中的功能?", function(r) {
                    if (r) {
                        jQuery.ajax({
                            type: "GET",
                            url: "../Data/Handler.ashx",
                            data: "Action=DeleteFun&functionid=" + functionid,
                            success: function(result) {
                                if (result) {
                                    grid.datagrid('reload');
                                    grid.datagrid('clearSelections');
                                    return;
                                }
                                parent.$.messager.alert("功能信息删除失败。");
                            }
                        });
                    }
                });
            }
    
           
refesh=function(){
 grid.datagrid('reload');
 grid.datagrid('clearSelections');
}
 
    </script>

</head>
<body class="easyui-layout">
    <div region="west" hide="true" split="true" title="功能列表" style="width: 180px;" id="west">
        <div id="nav" class="easyui-accordion" fit="true" border="false" style="height: 300px">
            <table>
                <tr>
                    <td>
                        <div id="div_tree">
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="mainPanle" region="center" style="background: #eee; overflow-y: hidden">
        <div id="tabs" class="easyui-tabs" fit="true" border="false">
            <table id="tt">
            </table>
        </div>
        <div id="win" class="easyui-window" modal="true" closed="true">
            <table>
                <tr>
                    <td style="width: 80px; text-align: right; padding-right: 5px;">
                        功能名称：
                    </td>
                    <td>
                        <input type="text" id="funname" required="true"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px; text-align: right; padding-right: 5px;">
                        功能描述：
                    </td>
                    <td>
                        <input type="text" id="fundesc"  required="true"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px; text-align: right; padding-right: 5px;">
                        页面链接：
                    </td>
                    <td>
                        <input type="text" id="funurl"  />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <a href="javascript:void(0)" id="btnSave" class="easyui-linkbutton" iconcls="icon-ok">
                            保 存</a> <a href="javascript:void(0)" id="btnExit" class="easyui-linkbutton" iconcls="icon-no">
                                关 闭</a>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</body>
</html>
