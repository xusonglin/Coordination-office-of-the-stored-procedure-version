<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataShow.aspx.cs" Inherits="JiaoShiXinXiTongJi.DaoRu.DataShow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
    <title>jQuery EasyUI</title>
    <link rel="stylesheet" type="text/css" href="../Calendar/js/themes/default/easyui.css"/>
    <link rel="stylesheet" type="text/css" href="../Calendar/js/themes/icon.css"/>
    <script type="text/javascript" src="../Calendar/js/jquery-1.4.4.min.js"></script>
    <script type="text/javascript" src="../Calendar/js/jquery.easyui.min.js"></script>
    <script type="text/javascript">
    //页面加载完成时
        $.ready(function () {
            alert("zhang");
        });
        $(function () {
            $('#test').datagrid({
                title: 'My Title',
                iconCls: 'icon-save',
                width: 860,
                height: 350,
                nowrap: false,
                striped: true,
                collapsible: true,
                url: '../Calendar/json/datagrid_data.json',
                sortName: 'code',
                sortOrder: 'desc',
                remoteSort: false,
                idField: 'code',
                frozenColumns: [[
	                { field: 'ck', checkbox: true },
	                { field: 'workernum', title: '教师工号', width: 80, sortable: true }
				]],
                 columns: [[
            { field: 'teachername', title: '教师姓名', width: 120, align: 'center' },
            { field: 'cakssname', title: '课程名称', width: 200, align: 'center' },
            { field: 'schoolhour', title: '教师工号', width: 100, align: 'left' },
            { field: "schooltime", title: "课程时间", width: 200, align: 'center' },
            { field: 'schookaddess', title: '课程地点', width: 80, align: 'center' },
            { field: 'schoolteam', title: '开课班级', width: 200, align: 'center' }
            ]],
                pagination: true,
                rownumbers: true,
                toolbar: [{
                    id: 'btnadd',
                    text: 'Add',
                    iconCls: 'icon-add',
                    handler: function () {
                        $('#btnsave').linkbutton('enable');
                        alert('add')
                    }
                }, {
                    id: 'btncut',
                    text: 'Cut',
                    iconCls: 'icon-cut',
                    handler: function () {
                        $('#btnsave').linkbutton('enable');
                        alert('cut')
                    }
                }, '-', {
                    id: 'btnsave',
                    text: 'Save',
                    disabled: true,
                    iconCls: 'icon-save',
                    handler: function () {
                        $('#btnsave').linkbutton('disable');
                        alert('save')
                    }
                }]
            });
            var p = $('#test').datagrid('getPager');
            if (p) {
                $(p).pagination({
                    onBeforeRefresh: function () {
                        alert('before refresh');
                    }
                });
            }
        });
        function resize() {
            $('#test').datagrid('resize', {
                width: 700,
                height: 400
            });
        }
        function getSelected() {
            var selected = $('#test').datagrid('getSelected');
            if (selected) {
                alert(selected.code + ":" + selected.name + ":" + selected.addr + ":" + selected.col4);
            }
        }
        function getSelections() {
            var ids = [];
            var rows = $('#test').datagrid('getSelections');
            for (var i = 0; i < rows.length; i++) {
                ids.push(rows[i].code);
            }
            alert(ids.join(':'));
        }
        function clearSelections() {
            $('#test').datagrid('clearSelections');
        }
	</script>
</head>

<body>
	<h1>课程列表</h1>
	<div style="margin-bottom:10px;">
		<a href="#" onclick="resize()">resize</a>
		<a href="#" onclick="getSelected()">getSelected</a>
		<a href="#" onclick="getSelections()">getSelections</a>
	</div>
	
	<table id="test"></table>
	
</body>
</html>