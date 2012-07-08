
//main 页面用

var countcreat=0;//统计创建多少个label 然后对应增加btn
var count=0;
var arr=new Array (100);
var  listt='';
var list_text='';
var Text_valuelist='';
var name='';
var HelloWorld = function(){
    var dialog, showBtn ,dialog_text, showBtn_text;
    return {
        init : function(){
           showBtn = Ext.get('show-dialog-btn');
           showBtn_text = Ext.get('show-dialog-btn_text');
           showBtn.on('click', this.showDialog, this);       
           showBtn_text.on('click', this.showDialog_text, this);   
               },
         showDialog : function(){
            if(!dialog){ // lazy initialize the dialog and only create it once
                dialog = new Ext.BasicDialog("hello-dlg", { 
                        autoTabs:true,
                        width:500,
                        height:300,
                        shadow:true,
                        minWidth:300,
                        minHeight:250,
                        proxyDrag: true
                });
                dialog.addKeyListener(27, dialog.hide, dialog);
                dialog.addButton({
                text:'提交',
                handler:function(){
                
                
                
                 if(Ext.get('title').dom.value!="")
                 {
                     if(Ext.get('title').dom.value.match(",")!=null||Ext.get('title').dom.value.match("，")!=null)
                     {
                        Ext.MessageBox.alert("警告！","标题名称中不能出现“，”和“,”！！");
                        return ;
                     }
                    var  title=Ext.get('title').dom.value;//获取标题名
                    var  Font_Names=Ext.get('Font-Names').dom.value; // 字体样式
                    var  Font_Size=Ext.get('Font-Size').dom.value;//大小
                    var  ForeColor=Ext.get('ForeColor').dom.value;//颜色
                    var  fold=Ext.get('fold').dom.value;//是否加粗
                    var con=0;
                    listtable=listtable.substring(0,listtable.length-1);
                    var arrlisttable=listtable.split(',');
                    for (var h=0;h<arrlisttable.length;h++)
                   { 
                     if ((title+"【"+username+"】")!=arrlisttable[h])
                     {
                     con++;
                     }
           
                   }
                if(con==arrlisttable.length)
                { 
                    list_title=title+','+Font_Names+','+Font_Size+','+ForeColor+','+fold;
                    listt=list_title;// 标题的信息 后台处理用
                   
                   addProduct(title,Font_Names,Font_Size,ForeColor,fold); // 输出到页面上
                   document.getElementById ("list").value=listt ; // 先将标题 的信息提交到表单中   
                    
                 //   Ext.get('title').dom.value="";
                     dialog.hide();
                   }
                   
                   else 
                   {
                      alert ("该标题已经被使用，请换个名称！");
                   return ;
                   }
              }
                     
               else 
               Ext.MessageBox.alert("警告！","标题名称不能为空！！");  
                }
                });
                dialog.addButton('取消', dialog.hide, dialog);
            }
            dialog.show(showBtn.dom);
        }, // 注意Ext结束标号
        
        
        
        
         showDialog_text : function(){
     
            if(!dialog_text){ 
                dialog_text = new Ext.BasicDialog("hello-dlg_text", { 
                        autoTabs:true,
                        width:500,
                        height:300,
                        shadow:true,
                        minWidth:300,
                        minHeight:250,
                        proxyDrag: true
                });
                dialog_text.addButton({
                text:'提交',
                handler:function(){
             if(Ext.get('title_text').dom.value!="")
                {
                  if(Ext.get('title_text').dom.value.match(",")!=null||Ext.get('title_text').dom.value.match("，")!=null)
                   {
                             Ext.MessageBox.alert("警告！","字段中不能出现“，”和“,”！！");
                                  return ;
                   }
                var i=count ;
                var  title_text=Ext.get('title_text').dom.value;
                var  Font_Names_text=Ext.get('Font-Names_text').dom.value;
                var  Font_Size_text=Ext.get('Font-Size_text').dom.value;
                var  ForeColor_text=Ext.get('ForeColor_text').dom.value;
                var  type=Ext.get('type').dom.value;//字段类型
                list=title_text+','+Font_Names_text+','+Font_Size_text+','+ForeColor_text +',' + type ;
                arr[i]=list ;   // 一个字段的所有信息保存到数组的一个列中
                list_text+=arr[i]+'_';// 所有的列用 _ 拼接
            
             var test=CheckGetText(title_text,count);
              if(test==true)
              
              {
           
                dialog_text.hide();
                createlabel(title_text,Font_Names_text,Font_Size_text,ForeColor_text,type,i); //显示在页面
       
               count ++; //每添加一个技术增加1 ，统计总共的个数
            
               Ext.get('title_text').dom.value="";
               Ext.get('Font-Names_text').dom.value="宋体";
               Ext.get('Font-Size_text').dom.value="20px";
               Ext.get('ForeColor_text').dom.value="black";
               Ext.get('type').dom.value="文本类型";}}
               else 
               Ext.MessageBox.alert("警告！","标题名称不能为空！！");
               
                        }
                });
                dialog_text.addButton('取消', dialog_text.hide, dialog_text);
            }
            dialog_text.show(showBtn_text.dom);
        }
    };
}();

Ext.onReady(HelloWorld.init, HelloWorld, true);


function GetText()  // 获取页面中的字段属性，这样做可以增加修改及删除之类的功能
{

var TextList=new Array (300);
for( var t=0;t<count ;t++)
{

var dd="Text_"+t;
var TextValue=document.getElementById (dd).value;
var TextfontFamily=document.getElementById (dd).style.fontFamily;
var TextfontSize=document.getElementById (dd).style.fontSize;
var Textcolor=document.getElementById (dd).style.color;
var Texttype=document.getElementById (dd).name;
TextList[t]=TextValue+","+TextfontFamily+","+TextfontSize+","+Textcolor +"," +Texttype;
Text_valuelist += TextList[t] + "_";

}
document .getElementById ("list_text").value = Text_valuelist;
    if (Text_valuelist!=null )
    {
     document.getElementById("Submit").click ();

    }

}





	function addProduct(name,Font_Names,Font_Size,ForeColor,fold)  //标题
	 {

  //  var label = document.createElement("lable");
    var label=document.getElementById("titleare");
    
    
    label.innerHTML=name;
    label.style.fontSize=Font_Size;
    label.style.fontFamily=Font_Names;
    label.style.color=ForeColor;
    label.style.width="100px";
    label .style .textAlign ="center";
    label .style.disabled ="disabled";
    if(fold =="Yes")
    {
     label.style.fontWeight = "Bold";
    }
    
    
//    var containerObj=document .getElementById ("titleare");//容器对象用来放置input对象
//    containerObj.appendChild(label);
}



   function createlabel(title,Font_Names,Font_Size,ForeColor,type,i)
    {
     var label = document.createElement("input");
     label.id ="Text_"+i;
     label.value=title;
     label.style.fontSize=Font_Size;
     label.style.fontFamily=Font_Names;
     label.style.color=ForeColor;
     label.name=type;
     label.style.width="100px";
    label .style .textAlign ="center";
    label .style.disabled ="disabled";
     var id="Text_"+i;
    label.ondblclick=function(){ Delinput(id);};
    var containerObj=document .getElementById ("containerObj");//容器对象用来放置input对象
    containerObj.appendChild(label);
    countcreat++;
    }
      
    
    
 function Dblclick()
    {

   document.getElementById("show-dialog-btn").click();   
    
    }
    
        
 function Dblclick_()
    {

	 document.getElementById("show-dialog-btn_text").click();     
    }
    
    
    
    function common(type)
    {
    
    alert(type);
    }
    
    
    
       function Delinput(id)      //
       {
  
       var o=document.getElementById(id);

       
       if(o==null)
       {
       alert("对象不存在！")
       return;
       
       }
       else {
       
       var t=window.confirm ("确定删除字段？")
    
       if(t==true)
       {
     o.parentNode.removeChild(o);
     count--;
        }
       }
       }
  
       
       function CheckGetText(name,count)  //2011-12-27
{

var TextList=new Array (300);
var test=false;
for( var t=0;t<count ;t++)
{
var dd="Text_"+t;
var TextValue=document.getElementById (dd).value;
if(TextValue==name)
 {
test=true;
 }
}
if(test==true )
{
alert ("字段列表中不允许有重复字段，请重新选择！")
return false;
}

else 
return true;

}

