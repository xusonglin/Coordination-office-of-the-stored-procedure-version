<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaskDesing.aspx.cs" Inherits="JiaoShiXinXiTongJi.Task.TaskDesing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            margin-left: 0px;
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
        }
        html, body
        {
            font-size: 12px;
            margin: 0px;
            height: 100%;
        }
        .mesWindow
        {
            border: #666 1px solid;
            background: #fff;
        }
        .mesWindowTop
        {
            border-bottom: #eee 1px solid;
            margin-left: 4px;
            padding: 3px;
            font-weight: bold;
            text-align: left;
            font-size: 12px;
        }
        .mesWindowContent
        {
            margin: 4px;
            font-size: 12px;
        }
        .mesWindow .close
        {
            height: 15px;
            width: 28px;
            border: none;
            cursor: pointer;
            text-decoration: underline;
            background: #fff;
        }
    </style>

    <script type="text/javascript">
   var listtable="<%=strtable %>"; // 接受参数在判断数据库中是否有同名表时用
   var username= "<%=usernam %>";
    </script>

    <link href="../Ext/resources/css/ext-all.css" rel="stylesheet" type="text/css" />
    <link href="../themes/default/easyui.css" rel="Stylesheet" />
    <link href="../themes/icon.css" rel="Stylesheet" />

    <script src="../Ext/ext-base.js" type="text/javascript"></script>

    <script src="../Ext/ext-all.js" type="text/javascript"></script>

    <script src="../js/jss/GetValues.js" type="text/javascript"></script>

    <script src="../js/jquery/jquery-1.4.4.min.js" type="text/javascript"></script>

    <script src="../js/jquery/jquery.easyui.min.js" type="text/javascript"></script>

    <style type="text/css ">
        input.buttonface
        {
            visibility: hidden;
        }
    </style>

    <script type="text/javascript">   //  拖动效果
		var data = {"total":0,"rows":[]};
		var totalCost = 0;
		$(function () {

		    $('#cartcontent').datagrid({
		        singleSelect: true
		    });


///实现拖动
		    $('.item').draggable({

		        revert: true,
		        proxy: 'clone',
		        onStartDrag: function () {
		            $(this).draggable('options').cursor = 'not-allowed1';
		            $(this).draggable('proxy').css('z-index', 10);

		        },
		        onStopDrag: function () {
		            $(this).draggable('options').cursor = 'move';
		            //					 document.getElementById("show-dialog-btn").click(); 

		        }
		    });

		    $('.item_text').draggable({
		        revert: true,
		        proxy: 'clone',
		        onStartDrag: function () {
		            $(this).draggable('options').cursor = 'not-allowed';
		            $(this).draggable('proxy').css('z-index', 10);


		        },
		        onStopDrag: function () {
		            $(this).draggable('options').cursor = 'move';
		            //				document.getElementById("show-dialog-btn_text").click(); 
		        }
		    });


		    $('.cart').droppable({
		        onDragEnter: function (e, source) {
		            $(source).draggable('options').cursor = 'auto';
		        },
		        onDragLeave: function (e, source) {
		            $(source).draggable('options').cursor = 'not-allowed';
		        },
		        onDrop: function (e, source) {
   
//		            var name = $(source).find('p:eq(0)').html();
//		            var id = $(source).find("img").attr("id")
		             var id = $(source).attr("id");
		            if (id == "bt") {
		                document.getElementById("show-dialog-btn").click();
		            }
		        }
		    });
		    $('.cart_text').droppable({
		        onDragEnter: function (e, source) {
		            $(source).draggable('options').cursor = 'auto';
		        },
		        onDragLeave: function (e, source) {
		            $(source).draggable('options').cursor = 'not-allowed';
		        },
		        onDrop: function (e, source) {
                    /////////////////////////////////////////////////////修改，获取当前a标签id////////////////////////////////////////////////////////////
		            var id = $(source).attr("id");
		            if (id == "zd")
		            { document.getElementById("show-dialog-btn_text").click(); }

		            if (id == "xm") {
		                var test = CheckGetText("姓名", count);
		                if (test == true) {
		                    createlabel("姓名", "宋体", "20px", "black", "文本类型", count); //显示在页面
		                    count++;
		                }

		            }

		            if (id == "csny") {
		                var test = CheckGetText("出生日期", count);
		                if (test == true) {
		                    createlabel("出生日期", "宋体", "20px", "black", "日期类型", count); //显示在页面
		                    count++;
		                }

		            }

		            if (id == "gh") {
		                var test = CheckGetText("职工编号", count);
		                if (test == true) {
		                    createlabel("职工编号", "宋体", "20px", "black", "文本类型", count); //显示在页面
		                    count++;
		                }
		            }

		            if (id == "xb") {
		                var test = CheckGetText("性别", count);
		                if (test == true) {
		                    createlabel("性别", "宋体", "20px", "black", "文本类型", count); //显示在页面
		                    count++;

		                } 
		            }


		            if (id == "nl") {
		                var test = CheckGetText("年龄", count);
		                if (test == true) {
		                    createlabel("年龄", "宋体", "20px", "black", "数值类型", count); //显示在页面
		                    count++;
		                }

		            }


		            if (id == "zcjb") {
		                var test = CheckGetText("职称级别", count);
		                if (test == true) {
		                    createlabel("职称级别", "宋体", "20px", "black", "文本类型", count); //显示在页面
		                    count++;
		                }
		            }


		            if (id == "xl") {
		                var test = CheckGetText("学历", count);
		                if (test == true) {
		                    createlabel("学历", "宋体", "20px", "black", "文本类型", count); //显示在页面
		                    count++;
		                }

		            }


		            if (id == "zw") {
		                var test = CheckGetText("职务", count);
		                if (test == true) {
		                    createlabel("职务", "宋体", "20px", "black", "文本类型", count); //显示在页面
		                    count++;
		                }

		            }



		            if (id == "cid") {
		                var test = CheckGetText("身份证号", count);
		                if (test == true)
		                    createlabel("身份证号", "宋体", "20px", "black", "数值类型", count); //显示在页面
		                count++;

		            }


		            if (id == "xy") {
		                var test = CheckGetText("学院", count);
		                if (test == true) {
		                    createlabel("学院", "宋体", "20px", "black", "文本类型", count); //显示在页面
		                    count++;
		                }

		            }



		            if (id == "yx") {
		                var test = CheckGetText("系别", count);
		                if (test == true) {
		                    createlabel("系别", "宋体", "20px", "black", "文本类型", count); //显示在页面
		                    count++;
		                }

		            }


		            if (id == "dzjz") {
		                var test = CheckGetText("党政兼职", count);
		                if (test == true) {
		                    createlabel("党政兼职", "宋体", "20px", "black", "文本类型", count); //显示在页面
		                    count++;
		                }
		            }

		            if (id == "rzsj") {
		                var test = CheckGetText("任职时间", count);
		                if (test == true) {
		                    createlabel("任职时间", "宋体", "20px", "black", "日期类型", count); //显示在页面
		                    count++;
		                }
		            }


		        }
		    });
		});


    </script>

    <%--     主界面引用--%>
</head>
<body >

    <script language="javascript" type="text/javascript">

//菜单缩进
 function displayinfo(obj)
      {
	    if(document.getElementById("frma").value=="Close") {
            obj.innerHTML = "<<<";
		    document.getElementById("c_left").style.display="none";
		    document.getElementById("frma").value="Open"
		    document.getElementById("imga").title ="菜单展开"
		    document.getElementById("imga").src="images/mid_mid_mid_left.gif";
		    document.getElementById("imga").border="0";
        }else
	      {
            obj.innerHTML = ">>>";
		    document.getElementById("c_left").style.display="";
		    document.getElementById("frma").value="Close"
		    document.getElementById("imga").title ="菜单缩进"
		    document.getElementById("imga").src="images/mid_mid_mid.gif";
		    document.getElementById("imga").border="0"
		   }
       } 
    </script>

    <div style="width: 100%; height: 80%">
        <table border="0" cellpadding="0" cellspacing="0" style="height: 80%; width: 100%;">
            <tr style="width: 100%">
                <td id="c_left" valign="top" style="width: 30%">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td colspan="3" style="width: 200px">
                                <table width="200px" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td style="width: 10px; background: url(images/mid_mid_left_1.gif);">
                                            <img src="../images/mid_mid_left.gif" width="10px" height="1px" alt="" />
                                        </td>
                                        <td style="height: 500px; width: 200px;" valign="top" align="left">
                                            <input id="frma" type="hidden" value="Close" />
                                            <%-- 添加修改部分 --%>
                                            <div style="width: 192px; height: 500px; border: 2px solid #36789E;">
                                                <div style="height: 15px;">
                                                </div>
                                                <table style="text-align: center;">
                                                    <tr>
                                                        <td colspan="2">
                                                            <ul style="height: 40px; padding-left: 5px">
                                                                <li><a href="#" id="bt" class="easyui-linkbutton item" ondblclick="Dblclick()" iconcls="">
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;标题选择&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a></li>
                                                            </ul>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <ul style="height: 40px; padding-left: 5px">
                                                                <li><a href="#" id="zd" class="easyui-linkbutton item" ondblclick="Dblclick_() "
                                                                    iconcls="">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;字段选择&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a></li>
                                                            </ul>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <ul style="height: 40px; padding-left: 5px">
                                                                <li><a href="#" id="xm" class="easyui-linkbutton item" ondblclick="Dblclick_() "
                                                                    iconcls="">姓名</a></li>
                                                            </ul>
                                                        </td>
                                                        <td>
                                                            <ul style="height: 40px; padding-left: 5px">
                                                                <li><a href="#" id="gh" class="easyui-linkbutton item" ondblclick="Dblclick_() "
                                                                    iconcls="">工号</a></li>
                                                            </ul>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <ul style="height: 40px; padding-left: 5px">
                                                                <li><a href="#" id="csny" class="easyui-linkbutton item" ondblclick="Dblclick_() "
                                                                    iconcls="">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;出生年月&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a></li>
                                                            </ul>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <ul style="height: 40px; padding-left: 5px">
                                                                <li><a href="#" id="xb" class="easyui-linkbutton item" ondblclick="Dblclick_() "
                                                                    iconcls="">性别</a></li>
                                                            </ul>
                                                        </td>
                                                        <td>
                                                            <ul style="height: 40px; padding-left: 5px">
                                                                <li><a href="#" id="nl" class="easyui-linkbutton item" ondblclick="Dblclick_() "
                                                                    iconcls="">年龄</a></li>
                                                            </ul>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <ul style="height: 40px; padding-left: 5px">
                                                                <li><a href="#" id="zcjb" class="easyui-linkbutton item" ondblclick="Dblclick_() "
                                                                    iconcls="">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;职称级别&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a></li>
                                                            </ul>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <ul style="height: 40px; padding-left: 5px">
                                                                <li><a href="#" id="xl" class="easyui-linkbutton item" ondblclick="Dblclick_() "
                                                                    iconcls="">学历</a></li>
                                                            </ul>
                                                        </td>
                                                        <td>
                                                            <ul style="height: 40px; padding-left: 5px">
                                                                <li><a href="#" id="zw" class="easyui-linkbutton item" ondblclick="Dblclick_() "
                                                                    iconcls="">职务</a></li>
                                                            </ul>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <ul style="height: 40px; padding-left: 5px">
                                                                <li><a href="#" id="cid" class="easyui-linkbutton item" ondblclick="Dblclick_() "
                                                                    iconcls="">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;身份证号&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a></li>
                                                            </ul>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <ul style="height: 40px; padding-left: 5px">
                                                                <li><a href="#" id="xy" class="easyui-linkbutton item" ondblclick="Dblclick_() "
                                                                    iconcls="">学院</a></li>
                                                            </ul>
                                                        </td>
                                                        <td>
                                                            <ul style="height: 40px; padding-left: 5px">
                                                                <li><a href="#" id="yx" class="easyui-linkbutton item" ondblclick="Dblclick_() "
                                                                    iconcls="">系别</a></li>
                                                            </ul>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <ul style="height: 40px; padding-left: 5px">
                                                                <li><a href="#" id="dzjz" class="easyui-linkbutton item" ondblclick="Dblclick_() "
                                                                    iconcls="">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;党政兼职&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a></li>
                                                            </ul>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <ul style="height: 40px; padding-left: 5px">
                                                                <li><a href="#" id="rzsj" class="easyui-linkbutton item" ondblclick="Dblclick_() "
                                                                    iconcls="">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;任职时间&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a></li>
                                                            </ul>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <%-- 添加修改部分 --%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="; width: 10px; height: 100px;"" >
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="center" valign="middle">
                                <%-- 修改触发事件 --%>
                                <span onclick="displayinfo(this);" style="cursor: pointer;"></span>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" style="width: 70%">
                    <table border="0" cellspacing="10px" cellpadding="0" style="overflow: auto; width: 800px">
                        <tr>
                            <td valign="top" style="height: 200px">
                                <%--添加设计页面主区域--%>
                                <div class="cart" style="width: 100%; height: 80px; text-align: center; border: 0px dotted #2F4D67;background-color:#D5DEE8">
                                    <div>
                                        <span style="width: 100%; border: 1px dotted #BDDCFA; height: 100px" id="titleare">
                                        </span>
                                    </div>
                                    <input type="button" id="show-dialog-btn" value="标题区" style="text-align: center;
                                        border: 0px dotted #BDDCFA; visibility: hidden" />
                                    <div>
                                        <asp:Label ID="Labebbitaoti" runat="server" Text="标题区"></asp:Label></div>
                                </div>
                                <div style="width: 100%">
                                </div>
                                <div class="cart_text" style="width: 100%; height: 50px; text-align: center; border: 0px dotted #2F4D67 ;background-color:#D5DEE8">
                                    <input type="button" id="show-dialog-btn_text" value="字段区" style="text-align: center;
                                        border: 0px dotted #BDDCFA; visibility: hidden" />
                                    <div>
                                        <asp:Label ID="Labelzidu" runat="server" Text="字段区"></asp:Label></div>
                                </div>
                                <div style="width: 800px; height: 80%; border: 0px dotted #BDDCFA; overflow: auto;white-space:nowrap; ">
                                    <table style="width: 100%; height: 80%; border: 1px dotted #2F4D67; overflow: auto;">
                                        <tr>
                                            <td id="containerObj" style=" height: 80%; border: 1px dotted #BDDCFA; overflow: auto">
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <%--显示字段的区域--%>
                                <div id="hello-dlg" style="visibility: hidden; position: absolute; top: 0px;">
                                    <div class="x-dlg-hd">
                                        标题设计</div>
                                    <div class="x-dlg-bd">
                                        <div class="x-dlg-tab" title="标题设计">
                                            <div class="inner-tab">
                                                <table>
                                                    <tr>
                                                        <td style="padding-left: 20px">
                                                            标题名称：
                                                        </td>
                                                        <td style="text-align: center; vertical-align: middle" colspan="4">
                                                            <input type="text" id="title" style="width: 300px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 20px">
                                                            字体样式：
                                                        </td>
                                                        <td>
                                                            <select id="Font-Names">
                                                                <option value="宋体">宋体</option>
                                                                <option value="楷体_GB2312">楷体_GB2312</option>
                                                                <option value="华文行楷">华文行楷</option>
                                                            </select>
                                                        </td>
                                                        <td style="text-align: right">
                                                            标题加粗：
                                                        </td>
                                                        <td>
                                                            <select id="fold">
                                                                <option value="No">正常</option>
                                                                <option value="Yes">加粗</option>
                                                            </select>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 20px">
                                                            字体大小：
                                                        </td>
                                                        <td>
                                                            <select id="Font-Size">
                                                                <option value="20px">小号</option>
                                                                <option value="60px">中号</option>
                                                                <option value="70px">大号</option>
                                                            </select>
                                                        </td>
                                                        <td style="text-align: right">
                                                            字体颜色：
                                                        </td>
                                                        <td>
                                                            <select id="ForeColor">
                                                                <option value="black">黑色</option>
                                                                <option value="blue">蓝色</option>
                                                                <option value="red">红色</option>
                                                            </select>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="hello-dlg_text" style="visibility: hidden; position: absolute; top: 0px;">
                                    <div class="x-dlg-hd">
                                        Excel字段设计</div>
                                    <div class="x-dlg-bd">
                                        <div class="x-dlg-tab" title="Excel字段设计">
                                            <div class="inner-tab">
                                                <table>
                                                    <tr>
                                                        <td style="padding-left: 20px">
                                                            标题名称：
                                                        </td>
                                                        <td style="text-align: center; vertical-align: middle" colspan="3">
                                                            <input type="text" id="title_text" style="width: 300px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right">
                                                            字段类型：
                                                        </td>
                                                        <td>
                                                            <select id="type">
                                                                <option value="文本类型">文本类型</option>
                                                                <option value="数值类型">数值类型</option>
                                                                <option value="日期类型">日期类型</option>
                                                                <option value="货币类型">货币类型</option>
                                                            </select>
                                                        </td>
                                                        <td style="padding-left: 20px">
                                                            字体样式：
                                                        </td>
                                                        <td>
                                                            <select id="Font-Names_text">
                                                                <option value="宋体">宋体</option>
                                                                <option value="楷体_GB2312">楷体_GB2312</option>
                                                                <option value="华文行楷">华文行楷</option>
                                                            </select>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 20px">
                                                            字体大小：
                                                        </td>
                                                        <td>
                                                            <select id="Font-Size_text">
                                                                <option value="20px">小号</option>
                                                                <option value="60px">中号</option>
                                                                <option value="70px">大号</option>
                                                            </select>
                                                        </td>
                                                        <td style="text-align: right">
                                                            字体颜色：
                                                        </td>
                                                        <td>
                                                            <select id="ForeColor_text">
                                                                <option value="black">黑色</option>
                                                                <option value="blue">蓝色</option>
                                                                <option value="red">红色</option>
                                                            </select>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%-- 添加设计页面主区域  end--%>
                            </td>
                        </tr>
                    </table>
                    <form id="form2" action="TaskDesing.aspx" method="post" runat="server">
                    <div style="text-align: right; padding-right: 60px">
                        <input name="list" type="hidden" value="" id="list" />
                        <input name="list_text" type="hidden" value="" id="list_text" />
                        <input type="submit" id="Submit" value="完成设计" style="visibility: hidden" />
                        <a href="#" class="easyui-linkbutton" iconcls="" onclick="GetText()">完成设计</a>
                        <%--<input id="Button1" type="button" value="完成设计" onclick="GetText()" />--%>
                        <%--  <input type="button" onclick="javascript:window.location.href='TaskDesing.aspx'"
                                    value="重新设计" />--%>
                        <%--   <input type="button" onclick="view_()" value="退出设计" />--%>
                    </div>
                    </form>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
