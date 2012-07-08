


$(function(){
	InitLeftMenu();
	tabClose();
	tabCloseEven();
	$('#tabs').tabs('add',{
		title:'用户首页',
		content:createFrame("/Task/myTask.aspx")
	}).tabs({
        onSelect: function (title) {
            var currTab = $('#tabs').tabs('getTab', title);
            var iframe = $(currTab.panel('options').content);
			var src = iframe.attr('src');
			if(src)
				$('#tabs').tabs('update', { tab: currTab, options: { content: createFrame(src)} });
        }
    });

})

//初始化左侧
function InitLeftMenu() {
var arrid=sysFuns.split(',');

var arrname_=sysFunsname.split(',');
 Funs=Funs.substring(0,Funs.length-2);
for(var t=0;t<arrid.length;t++)
{

    var arrname="";
    
    if(Funs.match(arrid[t])!=null)
    {
    arrname = {"menus":[
						{"menuid":"1","icon":"icon-sys","menuname":arrname_[t],
							"menus":[
			              ]
						  }
				] };
	  }
  
   var myinfos="";
 
   var arrfun=Funs.split('&#');
   $("#nav").accordion({animate:true});
	    
if(arrname!="")
{
    $.each(arrname.menus, function (i, n) {
        var menulist = '';
        menulist += '<ul>';
        for (var i = 0; i < arrfun.length; i++) {

            var myinfo = "";
            var arr = arrfun[i].split('%#')
            functionname = arr[0];
            url = arr[1];
            parentid = arr[2];
            icon = "icon-users";
            menuid = 10;

            if (parentid == arrid[t]) {
                menulist += '<li><div><a ref="' + menuid + '" href="#" rel="' + url + '" ><span class="icon ' + icon + '" >&nbsp;</span><span class="nav">' + functionname + '</span></a></div></li> ';
            }
        }
        menulist += '</ul>';
        $('#nav').accordion('add', {
            title: n.menuname,
            content: menulist,
            iconCls: 'icon ' + n.icon
        });

    });}
               
              
}

    
    

	$('.easyui-accordion li a').click(function(){

		var tabTitle = $(this).children('.nav').text();

		var url = $(this).attr("rel");
		var menuid = $(this).attr("ref");
		
		 var  icon="icon-users";
		
		$('.tabs-inner span').each(function(i,n){   //关闭其他所有的
			var t = $(n).text();
			$('#tabs').tabs('close',t);
		});
		
		

		addTab(tabTitle,url,icon);  // 添加
	
		$('.easyui-accordion li div').removeClass("selected");
		$(this).parent().addClass("selected");
	}).hover(function(){
	
		$(this).parent().addClass("hover");
	},function(){
		$(this).parent().removeClass("hover");
	});

	//选中第一个
	var panels = $('#nav').accordion('panels');
	var t = panels[0].panel('options').title;
	
    $('#nav').accordion('select', t);
}
//获取左侧导航的图标


function addTab(subtitle,url,icon){
	if(!$('#tabs').tabs('exists',subtitle)){
	
		$('#tabs').tabs('add',{
	    	title:subtitle,
			content:createFrame(url),
		    closable:true,
		    icon:icon
		});
		
	}else{
		$('#tabs').tabs('select',subtitle);
		$('#mm-tabupdate').click();
	}
	tabClose();
}

/***
* 修改：--------------------------------------修改iframe的宽度为：96%;margin-left:2%;在其他用到datagrid的页面，让其width为100%，宽度自适应
*/
function createFrame(url)
{
	var s = '<iframe scrolling="auto" frameborder="0"  src="'+url+'" style="width:100%;height:100%;"></iframe>'; //内容区样式
	return s;
}

function tabClose()
{
	/*双击关闭TAB选项卡*/
	$(".tabs-inner").dblclick(function(){
	
		var subtitle = $(this).children(".tabs-closable").text();
		$('#tabs').tabs('close',subtitle);
	})
	/*为选项卡绑定右键*/
	$(".tabs-inner").bind('contextmenu',function(e){
		$('#mm').menu('show', {
			left: e.pageX,
			top: e.pageY
		});

		var subtitle =$(this).children(".tabs-closable").text();

		$('#mm').data("currtab",subtitle);
		$('#tabs').tabs('select',subtitle);
		return false;
	});
}

//绑定右键菜单事件
function tabCloseEven()
{
	//刷新
	$('#mm-tabupdate').click(function(){
		var currTab = $('#tabs').tabs('getSelected');
		var url = $(currTab.panel('options').content).attr('src');
		$('#tabs').tabs('update',{
			tab:currTab,
			options:{
				content:createFrame(url)
			}
		})
	})
	//关闭当前
	$('#mm-tabclose').click(function(){
		var currtab_title = $('#mm').data("currtab");
		$('#tabs').tabs('close',currtab_title);
	})
	//全部关闭
	$('#mm-tabcloseall').click(function(){
		$('.tabs-inner span').each(function(i,n){
			var t = $(n).text();
			$('#tabs').tabs('close',t);
		});
	});
	//关闭除当前之外的TAB
	$('#mm-tabcloseother').click(function(){
		$('#mm-tabcloseright').click();
		$('#mm-tabcloseleft').click();
	});
	//关闭当前右侧的TAB
	$('#mm-tabcloseright').click(function(){
		var nextall = $('.tabs-selected').nextAll();
		if(nextall.length==0){
			//msgShow('系统提示','后边没有啦~~','error');
			alert('后边没有啦~~');
			return false;
		}
		nextall.each(function(i,n){
			var t=$('a:eq(0) span',$(n)).text();
			$('#tabs').tabs('close',t);
		});
		return false;
	});
	//关闭当前左侧的TAB
	$('#mm-tabcloseleft').click(function(){
		var prevall = $('.tabs-selected').prevAll();
		if(prevall.length==0){
			alert('到头了，前边没有啦~~');
			return false;
		}
		prevall.each(function(i,n){
			var t=$('a:eq(0) span',$(n)).text();
			$('#tabs').tabs('close',t);
		});
		return false;
	});

	//退出
	$("#mm-exit").click(function(){
		$('#mm').menu('hide');
	})
}

//弹出信息窗口 title:标题 msgString:提示信息 msgType:信息类型 [error,info,question,warning]
function msgShow(title, msgString, msgType) {
	$.messager.alert(title, msgString, msgType);
}
