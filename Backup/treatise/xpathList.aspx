<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xpathList.aspx.cs" Inherits="JiaoShiXinXiTongJi.treatise.xpathList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>论文抓取规则</title>
  <link href="../themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-1.4.2.js" type="text/javascript"></script>
    <script src="../js/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../js/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <link href="../css/content.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //将数组拼装成JSON格式字符串  
        //此处需要修改，有错误
        function jsonString(rows) {
            var str = "[";
            for (var i = 0; i < rows.length; i++) {
                if (i < rows.length - 1) {
                    str += "{\"Id\":\"" + rows[i].Id + "\"},";
                } else {
                    str += "{\"Id\":\"" + rows[i].Id + "\"}";
                }
            }
            str += "]";
            return str;
        }
        function loadData(){
            $("#pathList").datagrid({
                url: 'TreadtieseHandler_.ashx',
                queryParams: { sig: "pathList" },
                title: "论文抓取规则",
                fitColumns: true,
                striped: true,
                height: $(window).height(),
                width:"auto",
                loadMsg: '数据加载中，请稍微...',
                remoteSort: false,
                columns: [[
                         { field: 'Id', width: 10, resizable: true, checkbox: true },
                         { field: 'Websitename', title: '网站名称', width: 100, resizable: true },
                         { field: 'Weblanguage', title: '网站语言', width: 100, resizable: true },
                         { field: 'Baseurl', title: '网站地址', width: 100, resizable: true },
                         { field: 'Cntitle', title: '中文标题', width: 100, resizable: true },
                         { field: 'Entitle', title: '英文标题', width: 100, resizable: true },
                         { field: 'Unit', title: '单位名称', align: 'left', width: 100, resizable: true },
                         { field: 'Types', title: '文理科', align: 'left', width: 100, resizable: true },
                         { field: 'Author', title: '作者姓名', width: 100, resizable: true },
                         { field: 'Articletype', title: '论著类型', width: 100, align: 'left', resizable: true },
                         { field: 'Source', title: '课题来源', width: 100, align: 'left', resizable: true },
                         { field: 'Language', title: '语言种类', width: 100, align: 'left', resizable: true },
                         { field: 'Journalname', title: '刊物名称', width: 100, align: 'left', resizable: true },
                         { field: 'Publishtime', title: '发表时间', width: 100, align: 'left', resizable: true },
                         { field: 'Levels', title: '刊物级别', width: 100, align: 'left', resizable: true },
                         { field: 'Recordtype', title: '收录类型', width: 100, align: 'left', resizable: true },
                         { field: 'Forbid', title: '状态', width: 60, align: 'center', resizable: false, formatter: function (value, rowData, rowIndex) {
                             return value == 0 ? "<img src='../themes/icons/no.png'/>" : "<img src='../themes/icons/ok.png'/>";
                         }
                         },
                         { field: 'tt', title: '操作', align: 'center', width: 50, resizable: true, formatter: function (value, rowData, rowIndex) {
                             return "<a href='javascript:void(0);' class='easyui-linkbutton' onclick=\"javascript:window.location='onePath.aspx?Id=" + rowData.Id + "'\">查看</a>";
                         }
                         }
                   ]],
                toolbar: [{
                    iconCls: 'icon-add',
                    text: '添加规则',
                    handler: function () {
                        window.location = "addXPath.aspx";
                    }
                }, '-', {
                    iconCls: 'icon-no',
                    text: '禁用规则',
                    handler: function () {
                        var rows = $("#pathList").datagrid('getSelections');
                        if (rows.length == 0) {
                            $.messager.alert('提示', '请先选择要禁用的规则！', 'warning');
                            return false;
                        }
                        $.messager.confirm('提示', '确认禁用' + rows.length + '个网站的抓取规则?', function (b) {//友好提示管理员是否真的禁用
                            if (b) {
                                var str = jsonString(rows);
                                var path = "TreadtieseHandler_.ashx";
                                $.post(path, { ids: str, sig: "forbid" }, function (returnData, status) {//AJAX提交数据到后台进行处理
                                    $.messager.alert('提示', returnData, 'info');
                                    loadData();
                                });
                            }
                        });
                    }
                }, '-', {
                    iconCls: 'icon-ok',
                    text: '启用规则',
                    handler: function () {
                        var rows = $("#pathList").datagrid('getSelections');
                        if (rows.length == 0) {
                            $.messager.alert('提示', '请先选择要启用的规则！', 'warning');
                            return false;
                        }
                        $.messager.confirm('提示', '确认启用' + rows.length + '个网站的抓取规则?', function (b) {//友好提示管理员是否真的禁用
                            if (b) {
                                var str = jsonString(rows);
                                var path = "TreadtieseHandler_.ashx";
                                $.post(path, { ids: str, sig: "forbid", isaccept: "accept" }, function (returnData, status) {//AJAX提交数据到后台进行处理
                                    $.messager.alert('提示', returnData, 'info');
                                    loadData();
                                });
                            }
                        });
                    }
                }
                ],
                pagination: true,
                rownumbers: true,
                pageSize: 20,
                onLoadError: function () {
                    $.messager.alert('错误', '对不起，数据加载失败！请重试或者联系管理员', 'error');
                }
            });
        }
    </script>
</head>
<body onload="loadData()">
    <form id="form1" runat="server">
    <table id="pathList">
    </table>
    </form>
</body>
</html>
