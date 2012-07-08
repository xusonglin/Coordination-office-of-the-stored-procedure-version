<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Department.aspx.cs" Inherits="JiaoShiXinXiTongJi.USER._Department" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-1.4.2.js" type="text/javascript"></script>
    <script src="../js/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../js/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <link href="../css/content.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
      var XYID="<%=YxID%>";
      var XXID="<%=XyID%>";
      var userid="<%=userid%>"
        var grid;
        $(function() {
            var win_html = jQuery("#win").html();
            $('#win').html(win_html);

            grid = jQuery("#tt").datagrid({
                url: "../Data/DepHandler.ashx?XXID="+XXID+"&XYID="+XYID,
                height: $(window).height()-30,
                width: "auto",
                pagination: true,
                rownumbers: true,
                singleSelect: false,
                frozenColumns: [[
                    { field: 'ck', checkbox: true }
                ]],
                columns: [[
            { field: 'ID', title: '编号', width: 100, align: 'left',hidden:true },
            { field: 'BH', title: '编号', width: 100, align: 'left' },
            { field: 'MC', title: '学院名称', width: 250, align: 'center' },
            { field: 'pmc', title: '所属学校', width: 250, align: 'center' },
            { field: 'Desc', title: '备注', width: 300, align: 'center' }
            ]],
                queryParams: { Action: "Query" }
               
                });

                //去掉下面分页栏上的combobox，combobox总是显示在最上层，不知道怎么设置才好，就让它消失吧！
                var pager = $('#tt').datagrid('getPager');
                $(pager).pagination({ showPageList: false });
    
                $("#win").find("#btnSave").bind("click", function() {
              
            
                var tag = $.trim(jQuery(this).attr("tag"));
                if (tag == "add")
                 {
                 
                    var test=true;
                   if (!$("#txtBH").validatebox('isValid')) 
                     {  test=false }
                   if (!$("#txtMC").validatebox('isValid')) 
                     {  test=false }
                
                   
                   var xxID = $('#DropDownLisxx').combobox('getValue');
         
                   if(xxID=="")
                    { $.messager.alert('系统提示', "请选择学校！", "info"); test=false}
                    
                   if(test==true)
                       {    
                          $("#win").window("close");
                          Add();
                          return;      
                        }  
                    } 
                    if (tag == "edit") {
                      var test=true;
                     if (!$("#txtBH").validatebox('isValid')) 
                     {  test=false }
                     if (!$("#txtMC").validatebox('isValid')) 
                     {  test=false }
           
                   
                    var xxID = $('#DropDownLisxx').combobox('getValue');
          
                    if(xxID=="")
                    { $.messager.alert('系统提示', "请选择学校！", "info"); test=false}
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
                    grid.datagrid({ url: '../Data/DepHandler.ashx', queryParams: { Action: "Query", MC: txtMC} });
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
                    title: "修改院系信息",
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
                jQuery("#txtBH").val(rows[0].BH);
                jQuery("#txtMC").val(rows[0].MC);
                jQuery("#txtDesc").val(rows[0].Desc);
         
                 
                
            }
            // 添加院系信息
            Add = function() {
                var txtBH = jQuery.trim(jQuery("#txtBH").val());
                var txtMC = jQuery.trim(jQuery("#txtMC").val());
                var txtDesc = jQuery.trim(jQuery("#txtDesc").val());
                var xxID = $('#DropDownLisxx').combobox('getValue');        
                jQuery.ajax({
                    type: "POST",
                    url: "../Data/DepHandler.ashx",
                    data: "Action=Add&BH=" + txtBH + "&MC=" + txtMC + "&Desc=" + txtDesc + "&ParentID=" + xxID,
                    success: function(result) {
                        if (result == "true") {
                            $.messager.alert('系统提示', "【" + txtMC + "】院系信息添加成功。", "info");
                            grid.datagrid('reload');
                        }
                        else {
                            $.messager.alert('系统提示', "【" + txtMC + "】院系信息添加失败。", "info");
                        }
                    }
                });
            }

            // 修改院系信息
            Save = function() {
                var rows = grid.datagrid('getSelections');
                var txtMC = jQuery.trim(jQuery("#txtMC").val());
                var txtBH = jQuery.trim(jQuery("#txtBH").val());
                var txtDesc = jQuery.trim(jQuery("#txtDesc").val());
                var xxID = $('#DropDownLisxx').combobox('getValue');
                var txtID = rows[0].ID
                jQuery.ajax({
                    type: "GET",
                    url: "../Data/DepHandler.ashx",
                    data: "Action=Edite&BH=" + txtBH + "&MC=" + txtMC + "&Desc=" + txtDesc + "&ParentID=" + xxID + "&ID=" + txtID,
                    success: function(result) {
                        if (result == "true") {
                            jQuery.messager.alert('系统提示', "【" + txtMC + "】院系信息修改成功。", "info");
                            grid.datagrid('reload');
                        }
                        else {
                            jQuery.messager.alert('系统提示', "【" + txtMC + "】院系信息修改失败。", "info");
                        }
                    }
                });
            }
            // 删除
            Delete = function() {
                var rows = grid.datagrid('getSelections');
                var num = rows.length;
                if (num == 0) {
                    jQuery.messager.alert("系统提示", "请选择您要删除的院系信息。", "warning");
                    return;
                }
                var txtID = rows[0].ID;
               
                jQuery.messager.confirm("系统提示", "是否确定删除【" + txtMC + "】院系信息?", function(r) {
                    if (r) {
                        jQuery.ajax({
                            type: "GET",
                            url: "../Data/DepHandler.ashx",
                            data: "Action=Delete&ID=" + txtID,
                            success: function(result) {
                                if (result) {
                                    grid.datagrid('reload');
                                    grid.datagrid('clearSelections');
                                    return;
                                }
                                jQuery.messager.alert("【" + txtMC + "】院系信息删除失败。");
                            }
                        });
                    }
                });
            }


//搜索
function searchrenyuan() {
        
     search=$("#search").val();
     grid.datagrid({ url: '../Data/DepHandler.ashx', queryParams: { Action: "Query", search:search, XYID: XYID ,XXID:XXID} }); //grid刷新
     
    }
//添加
function tianjia() {
      
        $("#win").window({
            closed: false,
            title: "添加院系信息",
            collapsible: false,
            minimizable: false,
            maximizable: false,
            iconCls: "icon-add",
            resizable: false,
            width: 360,
            height: 288
        });
         $("#win").find("#btnSave").attr("tag", "add");  //给添加按钮设置tag属性，以便区分
          $('#win input').each(function() {
        if ($(this).attr('required') || $(this).attr('validType'))
        
            $(this).validatebox();
    });
         $('#DropDownLisxx').combobox({   
         valueField:'id',   
         textField:'text',   
         url:'../Data/HandlerCom.ashx?type=School&userid='+userid+'&XX='+XXID+'&XY='+XYID
       
        });

 }
                    
refesh=function(){
 grid.datagrid('reload');
 grid.datagrid('clearSelections');
}

    </script>

</head>
<body >
   
  <form id="Form1" runat="server">
       
          <div  fit="true" >
            <div region="north" border="false" style="border-bottom:1px solid #ddd;height:25px;padding:2px 5px;background:#DEE7FF;">
            <div style="float:left;">
               关键字：<input type="text" id="search">
                <a href="javascript:searchrenyuan()" class="easyui-linkbutton" plain="true" icon="icon-search">搜索</a>
                <a href="javascript:tianjia()" class="easyui-linkbutton" plain="true" icon="icon-add">添加</a>
                <a href="javascript:Edit()" class="easyui-linkbutton" plain="true" icon="icon-edit">修改</a>
                <a href="javascript:Delete()" class="easyui-linkbutton" plain="true" icon="icon-cancel">删除</a>
                <a href="javascript:refesh()" class="easyui-linkbutton" plain="true" icon="icon-reload">刷新</a>
            
          
            </div>
        </div>
        <div region="center" border="false">
            <table id="tt"></table>
            
        </div>
        </div>
      
</form>
      <div id="win" class="easyui-window" modal="true" closed="true">
            <table >
                <tr>
                    <td style="width: 80px; text-align: right; padding-right: 5px;">
                        院系编号：
                    </td>
                    <td>
                        <input type="text" id="txtBH" required="true" validtype="loginName" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px; text-align: right; padding-right: 5px;">
                        院系名称：
                    </td>
                    <td>
                        <input type="text" id="txtMC" required="true" />
                    </td>
                </tr>
                <tr>
                 <td style="width: 80px; text-align: right; padding-right: 5px;">
                    所属学校：
                 </td>
                 <td>
              
                 <select id="DropDownLisxx"  name="DropDownLisxx" style="width:150px;" required="true"></select>   
                  
                </td>
                </tr>
                <tr>
                    <td style="width: 80px; text-align: right; padding-right: 5px;">
                        备注信息：
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
</body>
</html>
