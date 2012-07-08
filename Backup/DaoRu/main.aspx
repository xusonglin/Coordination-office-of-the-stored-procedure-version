<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="JiaoShiXinXiTongJi.DaoRu.main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>jQuery EasyUI</title>
    <link rel="stylesheet" type="text/css" href="../Calendar/js/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../Calendar/js/themes/icon.css" />
    <script type="text/javascript" src="../Calendar/js/jquery-1.4.4.min.js"></script>
    <script type="text/javascript" src="../Calendar/js/jquery.easyui.min.js"></script>
    <script type="text/javascript">
        //设置tab的属性
        $(function () {
            $('#tt').tabs({
                tools: [{
                    iconCls: 'icon-add',
                    handler: function () {
                        alert('add');
                    }
                }, {
                    iconCls: 'icon-save',
                    handler: function () {
                        alert('save');
                    }
                }]
            });
        });

        //添加table
        function addTab(title, url) {
            var contents = "<iframe scrolling='yes' frameborder='0'  src='" + url + " ' style='width:100%;height:100%;'></iframe>"
            //alert(contents);
            $('#tt').tabs('add', {
                title: title,
                content: contents,
                iconCls: 'icon-save',
                closable: true
            });


        }

    </script>
</head>
<body class="easyui-layout">
    <div id="mymenu" style="width: 150px;">
        <div>
            item1</div>
        <div>
            item2</div>
    </div>
    <div region="north" title="North Title" split="true" style="height: 100px; padding: 10px;">
        <center>
            <h1 style="color: Red;">
                Calendar Test</h1>
        </center>
    </div>
    <div region="south" title="South Title" split="true" style="height: 100px; padding: 10px;
        background: #efefef;">
        <div class="easyui-layout" fit="true" style="background: #ccc;">
            <div region="center">
                sub center
            </div>
            <div region="east" split="true" style="width: 200px;">
                sub center
            </div>
        </div>
    </div>
    <div region="east" split="true" title="East" style="width: 100px; padding: 10px;">
        east region
    </div>
    <div region="west" split="true" title="West Menu" style="width: 280px; padding1: 1px;
        overflow: hidden;">
        <div class="easyui-accordion" fit="true" border="false">

            <div title="数据库导入" style="overflow: auto;">
                <a class="easyui-linkbutton" icon="icon-add" href="javascript:void(0)" onclick="addTab('数据库导入','Default.aspx' )">
                    数据库导入 </a>
            </div>

            <div title="发送短信按钮" style="overflow: auto;">
                <a class="easyui-linkbutton" icon="icon-add" href="javascript:void(0)" onclick="addTab('发送短信','Default.aspx' )">
                    发送短信按钮 </a>
                    <a class="easyui-linkbutton" icon="icon-add" href="javascript:void(0)" onclick="addTab('线程测试','Default.aspx' )">
                    线程测试按钮 </a>
            </div>

            <div title="查看所有课表" style="overflow: auto;">
                <a class="easyui-linkbutton" icon="icon-add" href="javascript:void(0)" onclick="addTab('查看所有课表','DataShow.aspx' )">
                    查看所有课表 </a>
            </div>

        </div>
    </div>
    <div region="center" title="Main Title" style="overflow: hidden;">

        <div id="tt" class="easyui-tabs" fit="true" border="false">
            <div title="查看所有课表" closable="true" style="padding: 20px;">
                <iframe scrolling='yes' frameborder='0' src='DataShow.aspx ' style='width: 100%; height: 100%;'>
                </iframe>
            </div>
        </div>

    </div>
</body>
</html>

