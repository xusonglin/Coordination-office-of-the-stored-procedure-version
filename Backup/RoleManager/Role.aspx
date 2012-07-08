<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Role.aspx.cs" Inherits="JiaoShiXinXiTongJi.RoleManager.Role" %>

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
    var roleid="<%=roleid %>";
    var userid="<%=userid %>";
    
    var XX="<%=XX %>";
    var XY="<%=XY %>";
    var roleid_="";     //标记选中的角色

        function GetNode()
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
                        var pnode = $('#div_tree').tree('getParent', node[i].target);
                        if (prevNode != pnode.text) {
                            parantsnodes += pnode.text + ',';
                            prevNode = pnode.text;
                           
                        }
                    }}
             
        strids= chilenodesid.substring(0,chilenodesid.length-1);        
        strtext= chilenodes.substring(0,chilenodes.length-1);           
            
        if(strids.length==1)
        alert ("请选中功能！！")
        else (strids.length>1)
        {
        var result=confirm("确定将权限分配给角色？")
        if(result==true)
        {
   jQuery.ajax({
                    type: "POST",
                    url: "../Data/Handler.ashx",
                    data: "Action=UpdateRole&roleID=" + roleid_ + "&functions=" + strids,
                    success: function(result) {
                        if (result == "true") {
                            $.messager.alert('系统提示', "角色授权成功！", "info");
                            grid.datagrid('reload');
                        }
                        else {
                            $.messager.alert('系统提示', "角色授权失败！", "info");
                        }
                    }
                });
    $("#tree").window("close");}
          }

        }
       
        
        function quxiao()
        {
         $("#tree").window("close");
        }
        
        
         // 角色信息显示
        var grid;       
        $(function() {                                                           
            grid = $("#tt").datagrid({
                url: "../Data/Handler.ashx",
                height: $(window).height()-10,
                width:"auto",
                pagination: true,
                rownumbers: true,
                singleSelect: true,
                frozenColumns: [[
               { field: 'ck', checkbox: true }
                ]],
                columns: [[
            { field: 'ID', title: 'ID', width: 100, align: 'left',hidden:true  },
            { field: 'Role', title: '角色名称', width: 300, dataIndex:'TaskID',align: 'left' },
            { field: 'Desc', title: '角色描述', width: 400, align: 'left' }
            ]],
                queryParams: { Action: "roleQuery" ,userid:userid},    //当请求远程数据时，发送的额外参数。

                 toolbar: [{
                    text: '添加',
                    iconCls: 'icon-add',
                    handler: function() {
                        $("#txtBH").val("");
                        $("#txtMC").val("");
                        $("#txtDesc").val("");
                        $("#win").window({
                            closed: false,
                            title: "添加角色信息",
                            collapsible: false,
                            top:($(window).height() - 420) * 0.5,   
                            left:($(window).width() - 450) * 0.5,
                            minimizable: false,
                            maximizable: false,
                            iconCls: "icon-add",
                            resizable: false,
                            width: 360,
                            height: 238
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
                    handler: function() {
                        grid.datagrid('reload');
                    }
}, '-', {
                    text: '角色授权',
                    iconCls: 'icon-undo',
                    handler: function(){
                 var rows = grid.datagrid('getSelections');
                var num = rows.length;
                if (num == 0) {
                    $.messager.alert("系统提示", "请选择一条记录进行操作。", "warning");
                    return;
                }
                if (num > 1) {
                    $.messager.alert("系统提示", "您选择了多条记录，只能选择一条记录进行修改。", "warning");
                    return;
                }
                
                  roleid_ = rows[0].ID;
                  
            $("#div_tree").tree({
                url: '../Data/GetFunction.ashx?XX='+XX+'&XY='+XY+'&roleid='+roleid_
            });

                  
                  
            $("#tree").window({
                            title: "角色授权",
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
                             istop:true  

                        });
                    
                    }
                }]
                });


                //去掉下面分页栏上的combobox，combobox总是显示在最上层，不知道怎么设置才好，就让它消失吧！
                var pager = $('#tt').datagrid('getPager');
                $(pager).pagination({ showPageList: false });

                $("#win").find("#btnSave").bind("click", function() {
                    var tag = $.trim(jQuery(this).attr("tag"));
                    if (tag == "add") {
                 var test=true;
                       $('#win input').each(function (){
                         if (!$(this).validatebox('isValid')) 
                         {  test=false }
                         });
                      if(test==true)
                       {    
                          $("#win").window("close");
                          Add();
                          return;      
                        }  

                     }
                    if (tag == "edit") {
                    var test=true;
                     $('#win input').each(function ()
                     {
                      if (!$(this).validatebox('isValid')){test=false }
                     });
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

                jQuery("#btnSearch").click(function() {
                    var txtMC = jQuery.trim(jQuery("#txtName").val());
                    grid.datagrid({ url: '../Data/SchHandler.ashx', queryParams: { Action: "Query", MC: txtMC} });
                });

            });

            // 获取数据
            Edit = function() {
                $("#win").find("#btnSave").attr("tag", "edit");
                var rows = grid.datagrid('getSelections');
                var num = rows.length;
                if (num == 0) {
                    $.messager.alert("系统提示", "请选择一条记录进行操作。", "warning");
                    return;
                }
                if (num > 1) {
                    $.messager.alert("系统提示", "您选择了多条记录，只能选择一条记录进行修改。", "warning");
                    return;
                }
                $("#win").window({
                    closed: false,
                    title: "修改角色信息",
                    collapsible: false,
                    minimizable: false,
                    maximizable: false,
                    iconCls: "icon-add",
                    resizable: false,
                    width: 360,
                    height: 238
                });
                    $("#win").find("#btnSave").attr("tag", "edit");  //给添加按钮设置tag属性，以便区分
                        $('#win input').each(function() {
                            if ($(this).attr('required') || $(this).attr('validType'))
                                $(this).validatebox();
                        });
            
                jQuery("#txtMC").val(rows[0].Role);
                jQuery("#txtDesc").val(rows[0].Desc);
            }
            // 添加学校信息
            Add = function() {
            
                var txtMC = jQuery.trim(jQuery("#txtMC").val());
                var txtDesc = jQuery.trim(jQuery("#txtDesc").val());
                jQuery.ajax({
                    type: "POST",
                      url: "../Data/Handler.ashx",
                    data: "Action=Addrole&MC=" + txtMC + "&Desc=" + txtDesc+"&userid="+userid,
                    success: function(result) {
                        if (result == "true") {
                            $.messager.alert('系统提示', "【" + txtMC + "】角色信息添加成功。", "info");
                            grid.datagrid('reload');
                        }
                        else {
                            $.messager.alert('系统提示', "【" + txtMC + "】角色信息添加成功。", "info");
                        }
                    }
                });
            }

            // 修改学校信息
            Save = function() {
                var rows = grid.datagrid('getSelections');
                var txtMC = jQuery.trim(jQuery("#txtMC").val());
                var txtDesc = jQuery.trim(jQuery("#txtDesc").val());
                var txtID = rows[0].ID
                jQuery.ajax({
                    type: "POST",
                    url: "../Data/Handler.ashx",
                    data: "Action=Editrole&MC=" + txtMC + "&Desc=" + txtDesc + "&roleID=" + txtID,
                    success: function(result) {
                        if (result == "true") {
                            jQuery.messager.alert('系统提示', "【" + txtMC + "】角色信息修改成功。", "info");
                            grid.datagrid('reload');
                        }
                        else {
                            jQuery.messager.alert('系统提示', "【" + txtMC + "】角色信息修改失败。", "info");
                        }
                    }
                });
            }
            // 删除
            Delete = function() {
                var rows = grid.datagrid('getSelections');
                var num = rows.length;
                if (num == 0) {
                    jQuery.messager.alert("系统提示", "请选择您要删除的角色信息。", "warning");
                    return;
                }
                var txtID = rows[0].ID;
                var txtMC = rows[0].Role;
                jQuery.messager.confirm("系统提示", "是否确定删除【" + txtMC + "】角色信息?", function(r) {
                    if (r) {
                        jQuery.ajax({
                            type: "GET",
                             url: "../Data/Handler.ashx",
                            data: "Action=Deleterole&roleID=" + txtID,
                            success: function(result) {
                                if (result) {
                                    grid.datagrid('reload');
                                    grid.datagrid('clearSelections');
                                    return;
                                }
                                jQuery.messager.alert("【" + txtMC + "】角色信息删除失败。");
                            }
                        });
                    }
                });
            }

    </script>

</head>
<body>
    <div>
        <div class="center" style="margin: 5px;">
        <%-- 添加让表格向下靠的填充 --%>
            <table id="tt">
            </table>
            <div id="win" class="easyui-window" modal="true" closed="true">
                <table>
                    <tr>
                        <td style="width: 80px; text-align: right; padding-right: 5px;">
                            角色名称：
                        </td>
                        <td>
                            <input type="text" id="txtMC"  required="true"  />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 80px; text-align: right; padding-right: 5px;">
                            角色描述信息：
                        </td>
                        <td colspan="3">
                            <textarea id="txtDesc" style="width: 200px; height: 80px;"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="3" style="text-align: right; padding-right: 10px;">
                            <a href="javascript:void(0)" id="btnSave" class="easyui-linkbutton" iconcls="icon-ok">
                                保 存</a> <a href="javascript:void(0)" id="btnExit" class="easyui-linkbutton" iconcls="icon-no">
                                    关 闭</a>
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
                        <td style="padding-right: 20px">
                            <input type="button" value="确定" onclick="GetNode()" />
                        </td>
                        <td style="padding-right: 10px">
                            <input type="button" value="取消" onclick="quxiao()" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</body>
</html>
