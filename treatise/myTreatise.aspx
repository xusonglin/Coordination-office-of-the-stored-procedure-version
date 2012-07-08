<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myTreatise.aspx.cs" Inherits="JiaoShiXinXiTongJi.treatise.myTreatise" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>我的论文列表</title>
    <link href="../themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-1.4.2.js" type="text/javascript"></script>
    <script src="../js/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../js/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <link href="../css/content.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
    
         var ZGXM="<%=ZGXM %>";
         var ZGBH="<%=ZGBH %>";
        var auth = new Array("一", "二", "三", "四", "五", "六", "七", "八", "九", "十");
        $(function () {
            $('#myTreatise').datagrid({
                url: 'TreadtieseHandler_.ashx',
                queryParams: { sig: "mytreatise",ZGXM:ZGXM,ZGBH:ZGBH},
//                title: '论文列表(双击某一行查看详细信息)',  
                fitColumns: true,
                striped: true,
                height: $(window).height()-30,
                width:"auto",
                loadMsg: '数据加载中，请稍微...',
                remoteSort: false,
        
            pagination: true,
            rownumbers: true,
            singleSelect: false,
            frozenColumns: [[
                { field: 'ck', checkbox: true }
            ]],
                
                
                columns: [[
                            { field: 'Id', title: '任务', width: 100, align: 'left', hidden: true },
                            { field: 'Cntitle', title: '中文标题', width: 80, resizable: false, sortable: true },
                            { field: 'Entitle', title: '英文标题', width: 80, resizable: false },
                            { field: 'Unitname', title: '单位名称', width: 200, align: 'left', resizable: false },
                            { field: 'Types', title: '文理科', width: 60, align: 'left', resizable: false },
                            { field: 'Author', title: '作者姓名', width: 150, resizable: false },
                            { field: 'Articletype', title: '论著类型', width: 60, align: 'center', resizable: false },
                            { field: 'Source', title: '课题来源', width: 80, align: 'left', resizable: false },
                            { field: 'Language', title: '语言种类', width: 60, align: 'left', resizable: false },
                            { field: 'Journalname', title: '刊物名称', width: 150, align: 'left', resizable: false },
                            { field: 'Publishtime', title: '发表时间', width: 100, align: 'center', resizable: false },
                            { field: 'Levels', title: '刊物级别', width: 80, align: 'left', resizable: false },
                            { field: 'Recordtype', title: '收录类型', width: 60, align: 'left', resizable: false },
                            { field: 'divide', title: '分工类型', width: 60, align: 'center', resizable: false, formatter: function (value, rowData, rowIndex) {
                                var author = rowData.Author;
                                if (author != null & author.length > 0) {
                                    var pos = findPos(author, '<%=Session["ZGXM"]%>');
                                    if (pos >= 0) {
                                        return "第" + auth[pos] + "作者";
                                    } else {
                                        return "无";
                                    }
                                }
                            }
                            }
                        ]],
                pagination: true,
                rownumbers: true,
                pageSize: 20,
                onDblClickRow: function (rowIndex, rowData) {
                    //alert("oneTreatise.aspx?Id=" + rowData.Id);
                    window.location = "oneTreatise.aspx?Id=" + rowData.Id;
                },
                rowStyler: function (value, rowData, rowIndex) {
                    return "cursor:pointer";
                },
                onLoadError: function () {
                    $.messager.alert('错误', '对不起，数据加载失败！请重试或者联系管理员', 'error');
                }
            });
            /**
            * 获得分工类型
            * @param src 字符串源文件
            * @param reg 目标字符串
            */
            function findPos(src, reg) {
                var arrays = src.split('，');
                var pos = -1;
                for (var i = 0; i < arrays.length; i++) {
                    if (arrays[i] == reg) {
                        pos = i;
                        break;
                    }
                }
                return pos;
            }
        });
        
        
        function viewdital()
        {
        
        var rows =  $('#myTreatise').datagrid('getSelections');
                var num = rows.length;
                if (num == 0) {
                    $.messager.alert("系统提示", "请选择一条记录进行操作。", "warning");
                    return;
                }
                if (num > 1) {
                    $.messager.alert("系统提示", "您选择了多条记录，只能选择一条记录进行修改。", "warning");
                    return;
                }
         window.location = "oneTreatise.aspx?Id=" + rows[0].Id;
        
        }
        
        
        function dele(){
        
         var rows =  $('#myTreatise').datagrid('getSelections');
                var num = rows.length;
                if (num == 0) {
                    $.messager.alert("系统提示", "请选择论文进行删除!", "warning");
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
                  
                  $.messager.confirm("系统提示", "是否确定删除论文信息?", function(r) {
                    if (r) {
                        jQuery.ajax({
                            type: "GET",
                            url: "TreadtieseHandler_.ashx",
                            data: "sig=Delete&&ids=" + ids,
                            success: function(result) {
                                if (result) {
                                    $('#myTreatise').datagrid('reload');
                                    $('#myTreatise').datagrid('clearSelections');
                                    return;
                                }
                                $.messager.alert("论文信息删除失败!");
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
            <div style="float:left;">
             
                <a href="javascript:viewdital()" class="easyui-linkbutton" plain="true" icon="icon-search">查看详细信息</a>
                  <a href="javascript:dele()" class="easyui-linkbutton" plain="true" icon="icon-cancel">删除记录</a>
               
            </div>
        </div>
        <div region="center" border="false">
            <table id="myTreatise"></table>
            
        </div>
        </div>


</body>
</html>

