

 //发布任务时弹出选择框

var HelloWorld = function(){
      var dialog, showBtn,dialog_user, showBtn_user;
     
    // return a public interface
    return {
        init : function(){
              showBtn = Ext.get('selecttask');
              showBtn_user = Ext.get('selectuser');
              showBtn.on('click', this.showDialog, this);   
              showBtn_user.on('click', this.showDialog_user, this)
     
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
//                dialog.addButton('Submit', dialog.hide, dialog);
                dialog.addButton({
                text:'提交',
                handler:function(){
                  select();
                 dialog.hide();
                
                }
                });
                
//          
                dialog.addButton('取消', dialog.hide, dialog);
             
            }
            
            dialog.show(showBtn.dom);
        },
            
            
 showDialog_user : function(){
            if(!dialog_user){ // lazy initialize the dialog and only create it once
                dialog_user = new Ext.BasicDialog("hello-dlg_user", { 
                        autoTabs:true,
                        width:500,
                        height:300,
                        shadow:true,
                        minWidth:300,
                        minHeight:250,
                        proxyDrag: true
                });
                dialog_user.addKeyListener(27, dialog_user.hide, dialog_user);
//                dialog.addButton('Submit', dialog.hide, dialog);
                dialog_user.addButton({
                text:'提交',
                handler:function(){

                  selectuser();
                 dialog_user.hide();
//                
                }
                });
                
//          
                dialog_user.addButton('取消', dialog_user.hide, dialog_user);
             
            }
            
            dialog_user.show(showBtn_user.dom);
        }
            
            
           
    };
}();

Ext.onReady(HelloWorld.init, HelloWorld, true);









// 数据导出时弹出选择框
/*
 * Ext JS Library 1.1
 * Copyright(c) 2006-2007, Ext JS, LLC.
 * licensing@extjs.com
 * 
 * http://www.extjs.com/license
 */

// create the HelloWorld application (single instance)
//var HelloWorld = function(){
//    // everything in this space is private and only accessible in the HelloWorld block
//    
//    // define some private variables
//    var dialog, showBtn;
//    
//    // return a public interface
//    return {
//        init : function(){
//             showBtn = Ext.get('selecttask');
//             // attach to click event
//             showBtn.on('click', this.showDialog, this);
//        },
//       
//        showDialog : function(){
//        alert()
//            if(!dialog){ // lazy initialize the dialog and only create it once
//            alert(1)
//                dialog = new Ext.BasicDialog("hello-dlg", { 
//                        autoTabs:true,
//                        width:500,
//                        height:300,
//                        shadow:true,
//                        minWidth:300,
//                        minHeight:250,
//                        proxyDrag: true
//                });
//                alert (2)
//                dialog.addKeyListener(27, dialog.hide, dialog);
//                dialog.addButton('Submit', dialog.hide, dialog).disable();
//                dialog.addButton('Close', dialog.hide, dialog);
//            }
//            dialog.show(showBtn.dom);
//        }
//    };
//}();

//// using onDocumentReady instead of window.onload initializes the application
//// when the DOM is ready, without waiting for images and other resources to load
//Ext.onReady(HelloWorld.init, HelloWorld, true);