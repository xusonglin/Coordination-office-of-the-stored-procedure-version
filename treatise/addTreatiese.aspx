<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addTreatiese.aspx.cs" Inherits="JiaoShiXinXiTongJi.treatise.addTreatiese" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
        <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css"/>
        <link rel="stylesheet" type="text/css" href="../themes/icon.css"/>
         <script type="text/javascript" src="../js/jquery/jquery-1.4.4.min.js"></script>
        <script type="text/javascript" src="../js/jquery/jquery.easyui.min.js"></script>
        <script type="text/javascript" src="../js/easyui-lang-zh_CN.js"></script>
        <title></title>
        <script type="text/javascript">
            $(function () {
                $('#website').combobox({//远程加载数据
                    url: "TreadtieseHandler_.ashx?sig=websiteList",
                    valueField: 'Id',
                    textField: 'Websitename'
                });
                $("#publishtime").datebox({//格式化日期选择的日期
                    formatter: function (date) {
                        var year = date.getFullYear();
                        var month = (date.getMonth() + 1 < 10 ? ("0" + (date.getMonth() + 1).toString()) : date.getMonth() + 1) ;
                        var day = (date.getDate() < 10 ? ("0" + date.getDate().toString()) : date.getDate());
                        return year + "-" + month + "-" + day;
                    }
                });
            });
          
          //自动搜索论文
         function searchTreatise() {
             var id = $("#website").combobox("getValue");
             var url = $("#url").val();
             if (url.lenght == 0) {
                 $.messager.alert("警告", "文章链接不能为空", "warning");
                 return false;
             }
             var reg = /^(https?|ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(\#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$/i;
             if (!reg.test(url)) {
                 $.messager.alert("警告", "请输入有效的文章链接", "warning");
                 return false;
             }
             if (id.length == 0) {
                 $.messager.alert("警告", "请选择文章的来源网站", "warning");
                 return false;
             }
             $("#loadingDiv").css("display","block");
             $('#loadingDiv').window({
                 title: '',
                 width: 200,
                 height: 60,
                 modal: true,
                 showType:'fade',
                 shadow: false,
                 minimizable: false,
                 collapsible: false,
                 maximizable: false,
                 closed: false
             });
             $.post("TreadtieseHandler_.ashx", { sig: "searchTreatise", id: id, treatiseURL: url }, function (returnData, status) {
                 $('#loadingDiv').window("close");
                 if (returnData == "error") {
                     $.messager.alert("错误", "请确认您的文章链接是否来源您选择的网站！", "error");
                 } else if (returnData.toString().substring(0, 5) == "EXIST") {//文章在数据库中已经存在
                     $.messager.alert("提示", returnData.substring("EXIST:".length), "info");
                 } else {
                     $("#cntitle").val(returnData.Cntitle);
                     $("#entitle").val(returnData.Entitle);
                     $("#unitname").val(returnData.Unitname);
                     $("#types").combobox("setValue",returnData.types);
                     $("#author").val(returnData.Author);
                     $("#articletype").val(returnData.Articletype);
                     $("#source").val(returnData.Source);
                     $("#languageIn").val(returnData.Language);
                     $("#journalname").val(returnData.Journalname);
                     $("#publishtime").datebox("setValue", returnData.Publishtime);
                     $("#levels").val(returnData.Levels);
                     $("#recordtype").val(returnData.Recordtype);
                 }
             });
         }
         /**
         * 提交表单
         */
         function submitForm() { 
           var title=$("#cntitle").val()+$("#entitle").val();
           var author = $("#author").val();
           var unit = $("#unitname").val();
           if (title.length == 0) {
               $.messager.alert("提示", "中文和英文标题至少填写一个", "warning");
               return false;
           }
           if (unit.length == 0) {
               $.messager.alert("提示", "单位名称不能为空", "warning");
               return false;
           }
           if (author.length == 0) {
               $.messager.alert("提示", "论文作者不能为空", "warning");
               return false;
           }
           $.post("TreadtieseHandler_.ashx", { sig: "exist", cntitle: $("#cntitle").val(), entitle: $("#entitle").val() }, function (returnData, status) {
               if (returnData == "EXIST") {
                   $('#confirmAdd').css("display","block");
                   $('#confirmAdd').window({
                       title: '提示',
                       width: 400,
                       height:120,
                       modal: true,
                       showType: 'fade',
                       shadow: false,
                       minimizable: false,
                       collapsible: false,
                       maximizable: false,
                       closed: false
                   });
               } else {
                   $("#treatiseForm").form("submit", {//提交表单
                       url: "TreadtieseHandler_.ashx?sig=insert",
                       success: function (data) {
                           $.messager.alert("提示", data, "info");
                           $("#treatiseForm").form("clear"); //清除表单
                       }
                   });
               }
           });
       }
       /**
       * 已存在，但确定不是当前用户的文章，则继续添加 
       */
       function addContinue() {
           $("#treatiseForm").form("submit", {//提交表单
               url: "TreadtieseHandler_.ashx?sig=insert",
               success: function (data) {
                   $.messager.alert("提示", data, "info");
                   $("#treatiseForm").form("clear"); //清除表单
               }
           });
       }
       /**
       * 当前用户确认该文章已经存在，则不继续添加，关闭确认窗体
       */
       function back() {
           $("#confirmAdd").window("close");
       }
        </script>
        <style type="text/css">
        #mainDiv{
            font-size:14px;
        }
        input{
          width:300px;
        }
        table tr
        {
            height:27px;
         }
        </style>
</head>
<body> 
<%--    <div id="mainDiv" class="easyui-panel" title="论文添加">--%>
    <form id="treatiseForm" runat="server">
    <div style="width:100%" align="center">
    <div style="margin-top:20px;margin-left:20px;line-height:22px;">
    <div style="text-align:left;font-weight:bold;color:Red;float:left">温馨提示:</div>
    <div style="text-align:left;float:left;">
    1、自动搜索目前仅支持“文章来源”中的网站。若您的文章在其它网站上，您只能通过手工填写，对此给您带来的不便，我们深表歉意，您可以联系管理员，为该网站添加自动搜索；<br />2、自动搜索的内容可能存在一定的错误或误差，若您发现与实际情况有出入，麻烦您手工修正后再提交；<br />3、文章链接必须为完整路径，必须包含：http、https...这些协议类型，否则系统无法正常获取内容；<br />
    4、自动搜索的内容可能有部分多余内容，例如：在《中国知网》获取的内容在作者前会加上“【作者】”字样字符串，请您手工删除后再提交；
    </div>
    </div>
    <fieldset style="width:400px;clear:both;margin-top:50px;">
    <legend>论文自动搜索</legend>
     <table align="center">
        <tr>
        <td align="right">文章链接</td>
        <td align="left"><input id="url" name="weburl" class="easyui-validatebox" required="true" missingMessage="文章链接不能为空" validType="url" invalidMessage="请输入有效的文章链接"/></td>
        </tr>
        <tr>
        <td align="right">文章来源</td>
        <td align="left">
           <select name="webId" id="website" style="width:205px;">
               
           </select>
           </td>
        </tr>
        <tr>
        <td colspan="2" align="center"><a href="javascript:void(0);" class="easyui-linkbutton" onclick="searchTreatise();" iconCls="icon-search">开始搜索</a></td>
        </tr>
      </table>
     </fieldset>
     <br />
     <fieldset style="width:400px;">
     <legend>论文详细内容</legend>
     <table align="center">
       <tr>
         <td align="right">中文标题</td>
         <td align="left"><asp:TextBox runat="server" ID="cntitle"/></td>
       </tr>
       <tr>
       <td align="right">英文标题</td>
       <td align="left"><asp:TextBox runat="server" ID="entitle"/></td>
       </tr>
       <tr>
       <td align="right">单位名称</td>
       <td align="left"><asp:TextBox runat="server" ID="unitname" class="easyui-validatebox" required="true" missingMessage="单位名称不能为空"/></td>
       </tr>
       <tr>
       <td align="right">文理科</td>
       <td align="left">
       <select name="types" id="types" class="easyui-combobox">
           <option value="文科">文科</option>
           <option value="理科">理科</option>
           <option value="工科">工科</option>
       </select>
       </td>
       </tr>
       <tr>
       <td align="right">作者</td>
       <td align="left"><asp:TextBox runat="server" ID="author" class="easyui-validatebox" required="true" missingMessage="作者不能为空"/></td>
       </tr>
       <tr>
       <td align="right">论著类型</td>
       <td align="left"><asp:TextBox runat="server" ID="articletype"/></td>
       </tr>
       <tr>
       <td align="right">课题来源</td>
       <td align="left"><asp:TextBox runat="server" ID="source"/></td>
       </tr>
       <tr>
       <td align="right">语言种类</td>
       <td align="left">
       <select name="language" id="language" class="easyui-combobox">
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
       <!-- 拉丁语, 拉脱维亚语, 立陶宛语, 罗马尼亚语, 马耳他语, 马来语, 马其顿语, 孟加拉语, 挪威语, 葡萄牙语, , 瑞典语, 塞尔维亚语, 世界语, 斯洛伐克语, 斯洛文尼亚语, 斯瓦希里语, 泰语, 土耳其语, 威尔士语, 乌克兰语, 希伯来语, 希腊语, 西班牙的巴斯克语, 西班牙语, 匈牙利语, 亚美尼亚语, , 意第绪语, 印地语, 印度的卡纳达语, 印度的泰卢固语, 印度的泰米尔语, 印度乌尔都语, 印尼语, , -->
       </tr>
       <tr>
       <td align="right">刊物名称</td>
       <td align="left"><asp:TextBox runat="server" ID="journalname" /></td>
       </tr>
       <tr>
       <td align="right">发表时间</td>
       <td align="left"><asp:TextBox runat="server" ID="publishtime" class="easyui-datebox"/></td>
       </tr>
       <tr>
       <td align="right">刊物级别</td>
       <td align="left">
       <select name="levels" id="levels" class="easyui-combobox">
           <option value="核1">核1</option>
           <option value="省级">省级</option>
           <option value="国家级">国家级</option>
           <option value="国际会议">国际会议</option>
       </select>
       </td>
       </tr>
       <tr>
       <td align="right">收录类型</td>
       <td align="left"><asp:TextBox runat="server" ID="recordtype"/></td>
       </tr>
       <tr>
       <td colspan="2" align="center"><a href="javascript:void(0);" onclick="submitForm();" class="easyui-linkbutton" iconCls="icon-ok">确认添加</a></td>
       </tr>
     </table>
     </fieldset>
     </div>
    </form>
    </div>
    <div id="loadingDiv" style="display:none;line-height:40px;text-align:center;">
    系统正在分析数据，请稍微...<img src="../images/loading.gif" border="0" />
    </div>
    <div id="confirmAdd" style="display:none;width:400px;height:200px;font-size:14px;text-align:left;line-height:22px;" align="center">
       您添加的文章在数据中存在标题和作者相同的文章，请确认是否该文章其它作者已经添加文章！<br />
       <div style="width:180px;text-align:center;float:left;"><a href="javascript:void(0);" class="easyui-linkbutton" onclick="addContinue();" iconCls="icon-ok">不是我的，继续添加</a> </div>
       <div style="width:180px;text-align:center;float:right;"><a href="javascript:void(0);" class="easyui-linkbutton" onclick="back();" iconCls="icon-cancel">是我的，不添加</a> </div>
<%--    </div>--%>
</body>
</html>
