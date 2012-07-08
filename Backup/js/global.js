//---------------------------------
//自动给表单上的文本框加上动态效果Js
//Author： Huacn Lee
//Version:1.0.0
//http://www.wathon.com
//=================================
var __txtArray;
function initAutoFormStyle()
{
	var inputArry = document.getElementsByTagName("input");
    var	textArry = document.getElementsByTagName("textarea");
	var selectArry=document .getElementsByTagName ("select");
	
	//对input应用样式
	for(var i=0;i<inputArry.length;i++)
	{
		var tmpInput = null;
		if(i< inputArry.length)
		{
			tmpInput = inputArry[i];
			if(tmpInput.type == 'text' || tmpInput.type == 'password' || tmpInput.type == 'file')
			{
				setEvent(tmpInput);
			}
		}
		else
		{
		    
			tmpInput = textArry[inputArry.length - i];
			setEvent(tmpInput);
		
			
			
		}
	}
	//对textarea应用样式
	for(var i=0;i<textArry.length;i++)
	{
	  setEvent (textArry [i]);
	}
	//对select应用样式
	for(var i=0;i<selectArry.length;i++)
	{
	  setEvent (selectArry [i]);
	}
	//对button应用样式
//	for(var i=0;i<inputArry.length;i++)
//	{
//	
//	setEvent (inputArry[i]);
//	
//	}
}

function setEvent(obj)
{
	var newClassName = 'text';
	var focusClassName = 'focus';
	if(obj.style.className != undefined)
	{
		newClassName = obj.style.className + ' ' + newClassName;
		focusClassName =  obj.style.className + ' ' + focusClassName;
	}
	
	
	if(obj.type == "button" || obj.type == "submit" || obj.type == "reset")
	{
	obj.className='btnStyle';
	}
	else
	{
	obj.className = newClassName;
	obj.onblur = function(){
		this.className = newClassName;
	}
	obj.onfocus =function(){
		this.className = focusClassName;
	}
	}
	
}
//举报弹出窗口
function dakai(){

document.getElementById('light').style.display='block';
document.getElementById('fade').style.display='block'
}

function closeoff()
{
document.getElementById('light').style.display='none';
document.getElementById('fade').style.display='none'

}
//举报弹出窗口结束

//最新发布控件滚动效果

  



//最新发布控件滚动效果结束

//统计限制文本框输入字数
function limitChars(textid,limit,infodiv)
{
 var text=document .getElementById ('#'+textid).val();
 var textlength=text .length;
 if(textlength >limit )
 {
   document .getElementById ('#'+infodiv ).html('输入内容不能超过'+limit+'个字符!');
   document .getElementById ('#'+textid ).val(text .substr(0,limit ));
   return false ;
 }
  else 
   {
     document .getElementById ('#'+infodiv ).html('你还可输入'+(limit-length)+'个字符!');
     return true;
   }
}
//兼容IE和FF的复制粘贴功能
function copyToClipboard(txt) {
 if(txt==null||txt=='undefined')
            return;
if(window.clipboardData) {
window.clipboardData.clearData();
window.clipboardData.setData("Text", txt);
}
else if(navigator.userAgent.indexOf("Opera") != -1) {
window.location = txt;
} else if (window.netscape) {
try {
netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
} catch (e) {
alert("该功能被当前浏览器禁用！\n请在浏览器地址栏输入'about:config'并回车\n然后将'signed.applets.codebase_principal_support'设置为'true'即可");
}
var clip = Components.classes['@mozilla.org/widget/clipboard;1'].createInstance(Components.interfaces.nsIClipboard);
if (!clip)
return;
var trans = Components.classes['@mozilla.org/widget/transferable;1'].createInstance(Components.interfaces.nsITransferable);
if (!trans)
return;
trans.addDataFlavor('text/unicode');
var str = new Object();
var len = new Object();
var str = Components.classes['@mozilla.org/supports-string;1'].createInstance(Components.interfaces.nsISupportsString);
var copytext = txt;
str.data = copytext;
trans.setTransferData("text/unicode",str,copytext.length*2);
var clipid = Components.interfaces.nsIClipboard;
if (!clip)
return false;
clip.setData(trans,null,clipid.kGlobalClipboard);
alert("复制成功，请粘贴到你的QQ/MSN上推荐给你的好友")
}
}