<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addXPath.aspx.cs" Inherits="JiaoShiXinXiTongJi.treatise.addXPath" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>论文抓取网站添加</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../themes/icon.css" />
    <script type="text/javascript" src="../js/jquery/jquery-1.4.4.min.js"></script>
    <script type="text/javascript" src="../js/jquery/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../js/easyui-lang-zh_CN.js"></script>
    <style type="text/css">
        table tr td{
            line-height:25px;
         }
        input{
            width:200px;
            margin-left:10px;
       }
    </style>
    <script type="text/javascript">
        /**
        * 提交表单
        */
        function submitForm() {
            $("#xpathForm").form("submit", {//提交表单
                url: "TreadtieseHandler.ashx?sig=insertXPath",
                onSubmit: function () {
                    var name = $("#websitename").val();
                    var url = $("#baseurl").val();
                    var unit = $("#unit").val();
                    var author = $("#author").val();
                    if (name.length == 0) {
                        $.messager.alert("提示", "网站名称不能为空", "warning");
                        return false;
                    }
                    if (url.length == 0) {
                        $.messager.alert("提示", "网站基本链接不能为空", "warning");
                        return false;
                    }
                    if (unit.length == 0) {
                        $.messager.alert("提示", "单位名称XPath不能为空", "warning");
                        return false;
                    }
                    if (author.length == 0) {
                        $.messager.alert("提示", "作者姓名XPath不能为空", "warning");
                        return false;
                    }
                },
                success: function (data) {
                    $.messager.alert("提示", data, "info");
                    $("#xpathForm").form("clear"); //清除表单
                }
            });
        }
    </script>
</head>
<body>
 <div class="easyui-panel" title="网站论文抓取规则添加" align="center">
 <div>
   <div style="width:70px;margin-left:20px;margin-top:20px;float:left;color:Red;font-weight:bold;">温馨提示：</div>
   <div style="margin-top:20px;float:left;line-height:20px;text-align:left;">
   1、网站链接为网站的网址，例如：http://www.cnki.net，请不要再有多余的参数；<br />
   2、以下填写内容中包含“XPath”的即为对应内容的抓取规则，您需要使用第三方工具来获得XPath，推荐使用Firefox浏览器；<br />
   3、请认真选择网站的语言类型，因为在系统无法获取老师论文的语言类型时，会使用对应网站的缺省语言；<br />
   4、<a href="" title="点击学习如何使用Firefox获得XPath">如何使用Firefox获得XPath?</a>
   </div>
 </div>
    <form id="xpathForm" method="post" action="" style="clear:both;">
    <fieldset style="width: 500px;">
        <legend>论文抓取网站添加</legend>
    <table align="center">
    <tr>
        <td align="right">网站名称</td>
        <td align="left"><input type="text" id="websitename" name="websitename" class="easyui-validatebox" required="true" missingmessage="网站名称不能为空" /></td>
    </tr>
    <tr>
    <td align="right">
        网站语言类型
    </td>
    <td align="left">
        <select name="weblanguage" style="width:200px;margin-left:10px;">
            <option value="中文">中文</option>
            <option value="英语">英语</option>
            <option value="德语">德语</option>
            <option value="俄语">俄语</option>
            <option value="法语">法语</option>
            <option value="日语">日语</option>
            <option value="韩语">韩语</option>
            <option value="阿尔巴尼亚语">阿尔巴尼亚语</option>
            <option value="阿拉伯语">阿拉伯语</option>
            <option value="爱尔兰语">爱尔兰语</option>
            <option value="爱沙尼亚语">爱沙尼亚语</option>
            <option value="白俄罗斯语">白俄罗斯语</option>
            <option value="保加利亚语">保加利亚语</option>
            <option value="冰岛语">冰岛语</option>
            <option value="波兰语">波兰语</option>
            <option value="波斯语">波斯语</option>
            <option value="布尔语(南非荷兰语)">布尔语(南非荷兰语)</option>
            <option value="丹麦语">丹麦语</option>
            <option value="芬兰语">芬兰语</option>
            <option value="格鲁吉亚语">格鲁吉亚语</option>
            <option value="古吉拉特语">古吉拉特语</option>
            <option value="海地克里奥尔语">海地克里奥尔语</option>
            <option value="荷兰语">荷兰语</option>
            <option value="加利西亚语">加利西亚语</option>
            <option value="加泰罗尼亚语">加泰罗尼亚语</option>
            <option value="捷克语">捷克语</option>
            <option value="韩语克罗">韩语克罗</option>
            <option value="意大利语">意大利语</option>
            <option value="越南语">越南语</option>
        </select>
    </td>
    </tr>
    <tr>
    <td align="right">网站链接</td>
    <td align="left">
        <input type="text" id="baseurl" name="baseurl" class="easyui-validatebox" required="true"
            missingmessage="网站链接不能为空" validtype="url" invalidmessage="请输入有效的网站链接" /></td>
    </tr>
    <tr>
    <td align="right">
        中文标题XPath</td>
    <td align="left">
        <input type="text" id="cntitle" name="cntitle" />
    </td>
    </tr>
    <tr>
    <td align="right">英文标题XPath</td>
    <td align="left">
        <input type="text" id="entitle" name="entitle" />
        </td>
    </tr>
     <tr>
    <td align="right">单位名称XPath</td>
    <td align="left">
        <input type="text" id="unit" name="unit" class="easyui-validatebox" required="true"
            missingmessage="单位名称的XPaht规则不能为空" />
         </td>
    </tr>
     <tr>
    <td align="right">文理科XPath</td>
    <td align="left">
        <input type="text" id="types" name="types" />
         </td>
    </tr>
     <tr>
    <td align="right">作者姓名XPath</td>
    <td align="left">
        <input type="text" id="author" name="author" class="easyui-validatebox" required="true"
            missingmessage="作者的XPaht规则不能为空" />
         </td>
    </tr>
     <tr>
    <td align="right">论著类型XPath</td>
    <td align="left">
        <input type="text" id="articletype" name="articletype"/>
         </td>
    </tr>
     <tr>
    <td align="right">课题来源XPath</td>
    <td align="left">
        <input type="text" id="source" name="source"/>
         </td>
    </tr>
     <tr>
    <td align="right">刊物名称XPath</td>
    <td align="left">
        <input type="text" id="journalname" name="journalname"/>
         </td>
    </tr>
     <tr>
    <td align="right">发表时间XPath</td>
    <td align="left">
        <input type="text" id="publishtime" name="publishtime" />
         </td>
    </tr>
     <tr>
    <td align="right">刊物级别XPath</td>
    <td align="left">
        <input type="text" id="levels" name="levels" />
         </td>
    </tr>
     <tr>
    <td align="right">收录类型XPath</td>
    <td align="left">
        <input type="text" id="recordtype" name="recordtype" />
         </td>
    </tr>
    <tr>
    <td colspan="2" align="center">
        <a href="javascript:void(0);" onclick="submitForm();" class="easyui-linkbutton" iconCls="icon-add">确认添加</a>
        </td>
    </tr>
    </table>
    </fieldset>
    </form>
        </div>
</body>
</html>
