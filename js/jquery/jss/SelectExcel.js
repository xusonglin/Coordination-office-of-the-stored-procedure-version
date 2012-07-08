
// 数据导出时弹出选择框

var HelloWorld = function(){ 
    var dialog, showBtn;

    return {
        init : function(){
             showBtn = Ext.get('select');
             // attach to click event
             showBtn.on('click', this.showDialog, this);
                     
             
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

              var selectvalue='';
              var selectvalueid='';
              var list=document.getElementsByName ("checkbo");

              for (var t=0;t<list .length ;t++)
              
              {
              
              if (list [t].checked){
              selectvalue +=list [t].value + ",";
              selectvalueid +=list [t].id + ",";
              
                  }  
              
              }
              
              
           var value=selectvalue.substring (0,selectvalue .length -1);
//           alert (value );
            var arr=value.split(',');
            if (value=="" )
                {
                alert("请先选中要导出的Excel！！");
                return 
                }
            if (arr.length==1)

               { 
              var valueid=selectvalueid.substring (0,selectvalueid .length -1);
              document.getElementById("taskname").value =value ;
              document.getElementById("taskid").value =valueid ;         
              dialog.hide();
                
              }
              else 
             alert("一次只能选择一个导出Excel！！");
             return 
                 
     
 
                }
                });          
    
                dialog.addButton('取消', dialog.hide, dialog);
                
            }
            dialog.show(showBtn.dom);
        }
    };
}();


Ext.onReady(HelloWorld.init, HelloWorld, true);




// function select()
//  {
//  var selectvalue='';
//  var selectvalueid='';
//  var list=document.getElementsByName ("checkbo");

//  for (var t=0;t<list .length ;t++)
//  
//  {
//  
//  if (list [t].checked){
//  
//  selectvalue +=list [t].value + ",";
//  selectvalueid +=list [t].id + ",";
//  
//      }  
//  
//  }
//  
//  
//  var value=selectvalue.substring (0,selectvalue .length -1);
////  alert (value );

//var arr=value.split(',');
//if (value==null )
//    {
//    alert("请先选中要导出的Excel！！");
//    }
//if (arr.length==1)

//   { 
//  var valueid=selectvalueid.substring (0,selectvalueid .length -1);
//  document.getElementById("taskname").value =value ;
//  document.getElementById("taskid").value =valueid ;
//  return true
//  }
//  else 
// alert("一次只能选择一个导出Excel！！");
//// return false 
//  
//  }