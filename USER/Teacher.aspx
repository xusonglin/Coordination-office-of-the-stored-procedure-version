<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Teacher.aspx.cs" Inherits="JiaoShiXinXiTongJi.USER._Teacher" %>

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
    var search=""
    var ZGBH='';
    var userid="<%=userid %>";
    var XY="<%=XY %>";
    var XX="<%=XX%>";
   
    function GetNode()
      {
        var node = $('#div_tree').tree('getChecked');
        var chilenodes = '';
        var parantsnodes = '';
        var prevNode = '';
        for (var i = 0; i < node.length; i++) {
            if ($('#div_tree').tree('isLeaf', node[i].target)) {
                chilenodes += node[i].id+ ',';
                var pnode = $('#div_tree').tree('getParent', node[i].target);
            }
                    }
        strtext= chilenodes.substring(0,chilenodes.length-1);           
       if(strtext.length==1)
         alert("请选中角色！！")
       if(node.length>1)
         alert("只能给用户授一个角色！")
       else
        {
         jQuery.ajax({
                    type: "POST",
                    url: "../Data/TeaHandler.ashx",
                    data: "Action=UpdateRole&ZGBH=" + ZGBH + "&roleids=" + strtext,
                    success: function(result) {
                        if (result == "true") {
                          $.messager.alert('系统提示', "授权成功！", "info");
                            grid.datagrid('reload');
                        }
                        else {
                            $.messager.alert('系统提示', "授权失败!", "info");
                        }
                    }
                });
       $("#tree").window("close");
        }
    }
      
      
      
    function quxiao()
    {
      $("#tree").window("close");
    }


    var win_html = jQuery("#win").html();
    $('#win').html(win_html);
    var grid;
    $(function() {
        grid = $("#tt").datagrid({
            url: "../Data/TeaHandler.ashx?XX="+XX+"&XY="+XY,
            height: $(window).height()-30,
            width:"auto",
            pagination: true,
            rownumbers: true,
            singleSelect: false,
            frozenColumns: [[
                { field: 'ck', checkbox: true }
            ]],
            columns: [[
        { field: 'ZGBH', title: '职工编号', width: 100, align: 'left' },
        { field: 'ZGXM', title: '姓名', width: 100, align: 'center' },
        { field: 'XY_', title: '学校', width: 200, align: 'center' },
        { field: "YX_", title: "院系", width: 200, align: 'center' },
        { field: 'Tel', title: '联系电话', width: 80, align: 'center' },
        { field: 'Email', title: 'Email', width: 200, align: 'center' },
        { field: 'role_', title: '角色', width: 100, align: 'center' }
        ]],
            queryParams: { Action: "Query"}
       });

    $( '#tt' ).datagrid( 'getPager' ).pagination( {
        t: function( pageNumber, pageSize ) {
            $( '#tt' ).datagrid( 'reload', $.extend( $( '#tt' ).datagrid( 'options' ).queryParams, {
                'pageNumber': pageNumber,
                'pageSize': pageSize
               
            } ) );
        }
    } );
    
    
    
          
    $('#DropDownLisxx').combobox({   
        valueField:'id',   
        textField:'text',   
        url:'../Data/HandlerCom.ashx?type=School&userid='+userid+'&XX='+XX+'&XY='+XY,  
       onChange:function(record){  
          $('#DropDownListxy').combobox('reload', '../Data/HandlerCom.ashx?type=Dpt&userid='+userid+'&PID='+record+'&XX='+XX+'&XY='+XY);
               
        }   
    });   
     $('#DropDownListxy').combobox({   
        valueField:'id',   
        textField:'text',   
        url:'../Data/HandlerCom.ashx?type=Dpt&userid='+userid+'&XX='+XX+'&XY='+XY

    });


    //根据tag属性来调用不同的函数。
    $("#win").find("#btnSave").bind("click", function() {
        var tag = jQuery.trim(jQuery(this).attr("tag"));
        if (tag == "add")
         {
           var test=true;
           if (!$("#txtBH").validatebox('isValid')) 
             {  test=false }
           if (!$("#txtMC").validatebox('isValid')) 
             {  test=false }
           if (!$("#txtPasw").validatebox('isValid')) 
             {  test=false }
           if (!$("#txtPasws").validatebox('isValid')) 
             {  test=false }
           var xxID = $('#DropDownLisxx').combobox('getValue');
           var xyID = $('#DropDownListxy').combobox('getValue');
           if(xxID=="")
            { $.messager.alert('系统提示', "请选择学校！", "info"); test=false}
           if(xyID=="")
            {
              if(userid=="00000000-0000-0000-0000-000000000000")
             {
                      
                    
//                 $.messager.confirm("系统提示", "是否确定删除用户信息?", function(r) {
//                    if (r=="yes") {
//                    alert(r)
//                    debugger
//                      xyID="00000000-0000-0000-0000-000000000000"; 
//                 

//                    }
//                    else
//                    {
//                        $.messager.alert('系统提示', "请选择学院！", "info");
//                         test=false
//                    
//                    
//                    }
//                    })
                    
//                   $.messager.confirm("系统提示", "是否确定删除用户信息?", function(r) {
//  if (r){   
//         xyID="00000000-0000-0000-0000-000000000000"; 
//   }   
//});  

                        
//                 if($.messager.confirm("系统提示", "是否创建学校管理员用户?"))
//                  {
//                     xyID="00000000-0000-0000-0000-000000000000"; 
//                  }
//                 else
//                 {
//                    $.messager.alert('系统提示', "请选择学院！", "info");
//                     test=false
//                  }
            }
                else
                {
                 $.messager.alert('系统提示', "请选择学院！", "info");
                   test=false
                 }
               }
            if(test==true)
             {    
                  $("#win").window("close");
                  Add();
                  return;      
              }  
          } 
                    
        
        
        if (tag == "edit") 
        {
          var test=true;
          if (!$("#txtBH").validatebox('isValid')) 
             {  test=false }
          if (!$("#txtMC").validatebox('isValid')) 
             {  test=false }
          if (!$("#txtPasw").validatebox('isValid')) 
             {  test=false }
          if (!$("#txtPasws").validatebox('isValid')) 
             {  test=false }
                         
        var xxID = $('#DropDownLisxx').combobox('getValue');
        var xyID = $('#DropDownListxy').combobox('getValue');
        if(xxID=="")
          { $.messager.alert('系统提示', "请选择学校！", "info"); test=false}
       if(xyID=="")
        {
            if(userid=="00000000-0000-0000-0000-000000000000")
         {
             if(confirm("是否创建学校管理员用户？"))
              {
                 xyID="00000000-0000-0000-0000-000000000000"; 
              }
             else
             {
                $.messager.alert('系统提示', "请选择学院！", "info");
                 test=false
              }
        }
        else
        {
         $.messager.alert('系统提示', "请选择学院！", "info");
                 test=false
        }
       }
                 
     if(test==true)
     {
      $("#win").window("close");
          Save();
        return;
      }
     }
     if (tag == "updaterole") {
        $("#tree").window("close");
        UpdateRole();
        return;
      }
                });
    $("#win").find("#btnExit").bind("click", function() {
        $("#win").window("close");
    });
             
  });



            Add = function() {
                var txtBH = $.trim($("#txtBH").val());
                var txtMC = $.trim($("#txtMC").val());
                var txtPasw = $.trim($("#txtPasw").val());
                var xxID = $('#DropDownLisxx').combobox('getValue');
                var xyID = $('#DropDownListxy').combobox('getValue');
                jQuery.ajax({
                    type: "POST",           //用GET方式提交显示在数据库中的中文字符是乱码，换成POST就没问题了。
                    url: "../Data/TeaHandler.ashx",
                    data: "Action=Add&ZGBH=" + txtBH + "&ZGXM=" + txtMC + "&Pass=" + txtPasw+"&xxID="+xxID+"&xyID="+xyID,
                    success: function(result) {
                        if (result == "true") {
                            $.messager.alert('系统提示', "添加成功。", "info");
                            grid.datagrid('reload');
                        }
                        else {
                            $.messager.alert('系统提示', "【" + Num + "】已经存在,重新添加。", "info");
                        }
                    }
                });
            }

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
                    title: "修改用户信息",
                    closed: false,
                    collapsible: false,
                    minimizable: false,
                    maximizable: false,
                    iconCls: "icon-edit",
                    resizable: false,
                    width: 320,
                    height: 280
                });
                     $("#win").find("#btnSave").attr("tag", "edit");  //给添加按钮设置tag属性，以便区分
                        $('#win input').each(function() {
                            if ($(this).attr('required') || $(this).attr('validType'))
                                $(this).validatebox();
                        });
                $("#txtPasw").val(rows[0].Pass);
                $("#txtBH").val(rows[0].ZGBH);
                $("#txtMC").val(rows[0].ZGXM);


            }

            // 修改信息
            Save = function() {
                var rows = grid.datagrid('getSelections');
                var UserID = rows[0].UserID;
                var txtBH = $.trim($("#txtBH").val());
                var txtMC = $.trim($("#txtMC").val());
                var txtPasw = $.trim($("#txtPasw").val());
                
               var xxID = $('#DropDownLisxx').combobox('getValue');
                var xyID = $('#DropDownListxy').combobox('getValue');

                jQuery.ajax({
                    type: "POST",
                    url: "../Data/TeaHandler.ashx",
                    data: "Action=Edite&ZGBH=" + txtBH + "&ZGXM=" + txtMC + "&Pass=" + txtPasw+"&xxID="+xxID+"&xyID="+xyID+"&userid="+UserID,
                    success: function(result) {
                        if (result == "true") {
                            $.messager.alert('系统提示', "信息修改成功!", "info");
                            grid.datagrid('reload');
                        }
                        else {
                            $.messager.alert('系统提示', "信息修改失败!", "info");
                        }
                    }
                });
            }


            //删除信息
            Delete = function() {
                var rows = grid.datagrid('getSelections');
                var num = rows.length;
                if (num == 0) {
                    $.messager.alert("系统提示", "请选择用户进行删除!", "warning");
                    return;
                }
                var Xm = rows[0].ZGXM;
                var userid=rows[0].UserID;
                var i=0;
                for(;i<rows.length;i++)
                   {
                    if(i==0)
                     ids="'"+rows[i].UserID+"'";
                   else
                    ids=ids+","+"'"+rows[i].UserID+"'";
                   }
                  
                  $.messager.confirm("系统提示", "是否确定删除用户信息?", function(r) {
                    if (r) {
                        jQuery.ajax({
                            type: "GET",
                            url: "../Data/TeaHandler.ashx",
                            data: "Action=Delete&&ids=" + ids,
                            success: function(result) {
                                if (result) {
                                    grid.datagrid('reload');
                                    grid.datagrid('clearSelections');
                                    return;
                                }
                                $.messager.alert("用户信息删除失败!");
                            }
                        });
                    }
                });
            }
                       
        refesh=function(){
         grid.datagrid('reload');
         grid.datagrid('clearSelections');
        }


function UserRole() {
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
                       $("#tree").window({
                            title: "用户授权",
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
                      
       
                    ZGBH = rows[0].ZGBH;
                    
                    var roleid = rows[0].Role;
           
            $("#div_tree").tree({
                url: '../Data/GetRole.ashx?userid='+userid+'&roleid='+roleid
           
            });}
            
            
            
            
function PassRest() {
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
                var ZGBH = rows[0].ZGBH;
                $.messager.confirm("系统提示", "是否确定将用户【" + ZGBH + "】的密码重置为123456?", function(r) {
                    if (r) {
                        jQuery.ajax({
                            type: "POST",
                            url: "../Data/TeaHandler.ashx",
                            data: "Action=replace&&ZGBH=" + ZGBH,
                            success: function(result) {
                                if (result) {
                                    grid.datagrid('reload');
                                    grid.datagrid('clearSelections');
                                    return;
                                }
                                $.messager.alert("【" + ZGBH + "】用户密码重置失败。");
                            }
                        });
                    }
                });
                      
                    } 



//搜索

  function searchrenyuan() 
    {
     search=$("#search").val();
     grid.datagrid({ url: '../Data/TeaHandler.ashx', queryParams: { Action: "Query", search:search, XX: XX ,XY:XY} }); //grid刷新
   }
//添加

 function tianjia()
   {
     $("#win").window({
        title: "添加用户信息",
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        iconCls: "icon-add",
        resizable: false,
        width: 320,
        height: 280
     });
    $("#win").find("#btnSave").attr("tag", "add");  //给添加按钮设置tag属性，以便区分
     $('#win input').each(function() {
        if ($(this).attr('required') || $(this).attr('validType'))
        
            $(this).validatebox();
    });
    }

//$(window).resize(function(){
//$('#tt').datagrid('resize', {
//width:function(){return document.body.clientWidth;},
//height:function(){return (document.body.clientHeight-100);}
//});
//});

    </script>

</head>
<body>
    <form id="Form1" runat="server">
       <div  fit="true" >
            <div region="north" border="false" style="border-bottom:1px solid #ddd;height:25px;padding:2px 5px;background:#DEE7FF;">
            <div style="float:left;">
                <span  style="font-size:15px">关键字(姓名)：</span><input type="text" id="search"  style=" height:15px ; width:200px">
                <a href="javascript:searchrenyuan()" class="easyui-linkbutton" plain="true" icon="icon-search">搜索</a>
                <a href="javascript:tianjia()" class="easyui-linkbutton" plain="true" icon="icon-add">添加</a>
                <a href="javascript:Edit()" class="easyui-linkbutton" plain="true" icon="icon-edit">修改</a>
                <a href="javascript:Delete()" class="easyui-linkbutton" plain="true" icon="icon-cancel">删除</a>
                <a href="javascript:UserRole()" class="easyui-linkbutton" plain="true" icon="icon-undo">用户授权</a>
                <a href="javascript:PassRest()" class="easyui-linkbutton" plain="true" icon="icon-back">密码重置</a>
                <a href="javascript:refesh()" class="easyui-linkbutton" plain="true" icon="icon-reload">刷新</a>
            
          
            </div>
        </div>
        <div region="center" border="false">
            <table id="tt"></table>
            
        </div>
        </div>
    
    </form>
    
        <div id="win" class="easyui-window" modal="true" closed="true">
        <table>
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
                    所属学院：
                </td>
                <td>
             
                
           <select id="DropDownListxy"  name="DropDownListxy" style="width:150px;" ></select> 
                                   
                </td>
            </tr>
            <tr>
                <td style="width: 80px; text-align: right; padding-right: 5px;">
                    职工编号：
                </td>
                <td>
                    <input type="text" id="txtBH" required="true" validtype="loginName"  Width="110px"/>
                </td>
            </tr>
            <tr>
                <td style="width: 80px; text-align: right; padding-right: 5px;">
                    职工姓名：
                </td>
                <td>
                    <input type="text" id="txtMC" required="true" validtype="loginName"  Width="110px"/>
                </td>
            </tr>
            <tr>
                <td style="width: 80px; text-align: right; padding-right: 5px;">
                    密&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;码：
                </td>
                <td>
                    <input type="password" id="txtPasw" required="true" validtype="safepass"  Width="160px"/>
                </td>
            </tr>
            <tr>
                <td style="width: 80px; text-align: right; padding-right: 5px;">
                    确认密码：
                </td>
                <td>
                    <input type="password" id="txtPasws" validtype="equalTo['#txtPasw']" required="true"  Width="160px"/>
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
    
</body>
</html>
