<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PerCourse.aspx.cs" Inherits="JiaoShiXinXiTongJi.CourseRemind.PerCourse" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>个人课程表</title>
    <link href="../themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-1.4.2.js" type="text/javascript"></script>
    <script src="../js/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../js/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <link href="../css/content.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #Course table
        {
            width: 100%;
            height: 100%;
            border-collapse: collapse;
        }
        #Course table tr td
        {
            border: 1px solid #99BBE8;
            text-align: center;
        }
        #Course .c_title
        {
            height: 30px;
            background-color: #DAE6FC;
            font-weight: 600;
        }
        #Course .c_row
        {
            background-color: #DAE6FC;
        }
        #Course table tr td span
        {
            font-size: 15px;
        }
        #Course table .c_title td span
        {
            font-size: 13px;
        }
        #dd table tr
        {
            height: 30px;
        }
        .td_left
        {
            text-align: right;
        }
    </style>
    <script type="text/javascript">
        var is_sms = false;
        var is_seccess = false;
        var ZGXM="<%=ZGXM %>";
         
        $(document).ready(function () {
            //个人课表查询
            $('#ss').searchbox({
                width: 300,
                searcher: function (value, name) {
                    window.location = "PerCourse.aspx?name=" + name + "&value=" + value;
                },
                menu: '#mm',
                prompt: '请输入职工号或姓名'
            });
            //短信发送提示
            $('#w').window({
                title: "是否发送提醒短信？",
                collapsible: false,
                minimizable: false,
                maximizable: false,
                modal: true,
                closed: true
            });
            //选择课程提醒
            $('#dd').dialog({
                buttons: [{
                    text: '现在提醒',
                    iconCls: 'icon-remove',
                    handler: function () {
                        var value = $('#cc').combobox('getText');
                        if (value != "") {
                            $("#notice").show()
                            $("#abc").hide();
                            var arr = new Array();
                            arr = value.split(" ");
                            $("#st").val($("#Label2").html());

                            var sxw = (arr[2] == "1,2节课" || arr[2] == "3,4节课") ? "上午" : "下午";
                            var sms = $("#Label4").html() + " 老师你好! 你将在 " + arr[0] + " 的 " + sxw + " 有两节 " + arr[1] + " 课 ( " + arr[2] + "," + arr[3] + " ) ---湖师教师管理系统提醒您！";
                            $("#zhou").html(arr[0]);
                            $("#kcm").html(arr[1]);
                            $("#jie").html(arr[2]);
                            $("#addr").html(arr[3]);
                            $("#sxw").html(sxw);
                            $("#textarea").html(sms);
                            is_sms = true;
                        } else {
                            $("#notice").hide();
                            $("#abc").show();
                            $("#abc").html("请选择提醒课程！<br /><br />");
                            value = $('#cc').combobox('getText');
                        }
                        $('#w').window('open');
                    }
                }, {
                    text: '下次再说',
                    iconCls: 'icon-cancel',
                    handler: function () {
                        $('#dd').dialog('close');
                    }
                }]
            });
            $('#dd').dialog('close');
            //AJAX获得该老师一周中的所有课程
            var Staff_Num = $("#Label2").html();
            $('#cc').combobox({
                url: 'CourseRemindHandler_.ashx?sig=getClass&Staff_Num=' + Staff_Num+'&Staff_Name='+ZGXM,
                valueField: 'Id',
                textField: 'Class_Time',
                panelHeight: 'auto',
                onSelect: function () {
                    if (is_seccess) {
                        is_seccess = false;
                    }
                }
            });

            $('#fff').form({

                url: 'CourseRemindHandler_.ashx?sig=getSend',
                onSubmit: function () {
                    if (is_seccess) {
                        alert("已经发送，刷新页面后可以重新发送...");
                        return false;
                    }
                },

                success: function (data) {
                    is_seccess = true;
                    alert(data)

                }
            });

        });
        function open1() {
        document.getElementById("w").style.visibility="visible";
            $('#dd').dialog('open');
        }
        //发送短信请求
        function send() {
            if(is_sms){
                 // 提交 form   
                $('#fff').submit(); 
            }else{
                alert("请选择课程信息");
                $("#w").window("close");
            }
        }
        //取消发送
        function nosend() {
            $("#w").window("close");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="Course" class="easyui-panel" title="个人课表" style="padding: 0px; background: #fafafa;">
            <table>
                <tr class="c_title">
                    <td colspan="8" style="text-align: left; padding-left: 10px;">
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;个人课表查询:&nbsp;<input id="ss"></input>
                        <div id="mm" style="width: 120px">
                            <div name="Staff_num" iconcls="icon-ok">
                                按职工号</div>
                            <div name="Teacher_name" iconcls="icon-ok">
                                按姓名</div>
                        </div>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a href="List.aspx" class="easyui-linkbutton"
                            plain="true">查看所有课程信息</a>
                    </td>
                </tr>
                <tr class="c_title">
                    <td style="width: 40px;" class="c_row">
                        &nbsp;
                    </td>
                    <td style="width: 14%;">
                        周一
                    </td>
                    <td style="width: 14%;">
                        周二
                    </td>
                    <td style="width: 14%;">
                        周三
                    </td>
                    <td style="width: 14%;">
                        周四
                    </td>
                    <td style="width: 14%;">
                        周五
                    </td>
                    <td style="width: 14%;">
                        周六
                    </td>
                    <td style="width: 14%;">
                        周日
                    </td>
                </tr>
                <tr style="height: 110px;">
                    <td class="c_row">
                        1,2节
                    </td>
                    <td>
                        <asp:Label ID="Label1_1" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label1_2" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label1_3" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label1_4" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label1_5" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label1_6" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label1_7" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr style="height: 110px;">
                    <td class="c_row">
                        3,4节
                    </td>
                    <td>
                        <asp:Label ID="Label2_1" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label2_2" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label2_3" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label2_4" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label2_5" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label2_6" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label2_7" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr style="height: 20px">
                    <td colspan="8">
                    </td>
                </tr>
                <tr style="height: 110px;">
                    <td class="c_row">
                        5,6节
                    </td>
                    <td>
                        <asp:Label ID="Label3_1" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label3_2" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label3_3" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label3_4" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label3_5" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label3_6" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label3_7" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr style="height: 110px;">
                    <td class="c_row">
                        7,8节
                    </td>
                    <td>
                        <asp:Label ID="Label4_1" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label4_2" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label4_3" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label4_4" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label4_5" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label4_6" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label4_7" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr class="c_title" style="height: 40px;">
                    <td colspan="8">
                        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="open1()">课程提醒</a>
                    </td>
                </tr>
            </table>
        </div>
        <div id="dd" icon="icon-add" title="设置课程提醒" style="padding: 5px; width: 400px;">
            <table style="width: 100%">
                <tr>
                    <td style="width: 30%" class="td_left">
                        教师职工号：&nbsp;
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="td_left">
                        教师姓名：&nbsp;
                    </td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="td_left">
                        提醒课程：&nbsp;
                    </td>
                    <td>
                        <input id="cc" name="dept" value="" style="width: 200px;" />
                    </td>
                </tr>
                <tr>
                    <td class="td_left">
                        提醒方式：&nbsp;
                    </td>
                    <td>
                        短信
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="w" class="easyui-window" style="width: 400px; height: 300px; padding: 5px; visibility:hidden">
        <div class="easyui-layout" fit="true">
            <div region="center" border="false" style="padding: 10px; background: #fff; border: 1px solid #ccc;">
                <asp:TextBox ID="TextBox1" runat="server" Visible="False" name="Staff_Num"></asp:TextBox>
                <font style="font-weight: 600;">提醒短信预览：</font><br />
                ----------------------------------------------------------<br />
                <br />
                <div id="notice">
                    <asp:Label ID="Label4" runat="server" Text=""></asp:Label>老师 您好！<br />
                    您将在&nbsp; <span id="zhou"></span>&nbsp;<span id="sxw"></span> &nbsp;有两节 <span id="kcm">
                    </span>  课&nbsp; （<span id="jie"></span>,<span id="addr"></span>）<br />
                    亲，要记得上课哦！！！
                </div>
                <br />
                <div id="abc">
                </div>
                -----------------------------------------------------------<br />
                <font style="font-weight: 600; font-size: 11px;">湖师教师管理系统提醒您！</font>
            </div>
            <div region="south" border="false" style="text-align: right; padding: 5px 0;">
                <a class="easyui-linkbutton" iconcls="icon-ok" href="javascript:void(0)" onclick="send()">
                    是</a> <a class="easyui-linkbutton" iconcls="icon-cancel" href="javascript:void(0)"
                        onclick="nosend()">否</a>
            </div>
        </div>
    </div>
    </form>
    <form id="fff" method="post">
    <div style="display: none">
        <input type="hidden" id="st" name="Staff_Num" />
        <textarea name="sms" id="textarea" cols="5" rows="5"></textarea>
    </div>
    </form>
</body>
</html>
