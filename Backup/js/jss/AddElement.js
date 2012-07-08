    
    
//Edite页面使用
    
    
 function CreateTitle(title,Font_Names,Font_Size,ForeColor,ISBold)   //标题
   {

     var label = document.createElement("label");
      label.id = "text";
      label.innerHTML=title;
      label.style.fontSize="30px";
      label.style.fontFamily=Font_Names;
      label.style.color=ForeColor;
      if (ISBold=="Yes"){
     label.style.fontWeight = "Bold";}
     var containerObj=document .getElementById ("title");
     containerObj.appendChild(label);

  
    }
    
 function CreateLabel(title,Font_Names,Font_Size,ForeColor)  //字段列名
   {  
     var label = document.createElement("input");
     
      label.id = "text";
      label.value=title;
      label.style.fontSize=Font_Size;
      label.style.fontFamily=Font_Names;
      label.style.color=ForeColor;
      label.style.background="#E3E9EE";
      label.style.width="100px";
      label .style.disabled ="disabled";    
      label .style .textAlign ="center";
      label.style.fontWeight = "Bold";
   var containerObj=document .getElementById ("LabelField");//
  containerObj.appendChild(label);
  
    }

       function AddDivEdite(a)   //添加显示已经填写信息的Div
       {
        var div=document.createElement("div");
        div.id="Div_text_"+a;
        div.style.textAlign ="center";
        document.getElementById("DivEdite").appendChild(div);
       }
    
      function addedite(a)    //操作 
      {
        var div=document.createElement("div");
        div.id="Div_text_edite"+a;
        div.style .textAlign ="right";
        document.getElementById("DivEdite").appendChild(div);
      
      }
    
   
        
    function CreatTextadd(t,type,tt)
    {
         var text = document.createElement("input");
          text.type="text";
    
          text.id ="Text_"+t+"_"+tt;
          text .name=type ;
         text.style.background="#99C0E4";
         text.style.width="100px";
         
         text .style .textAlign ="center";
         if(type=="日期类型")
         {
           text.onclick=function(){ ShowCalendar(this.id);};
         }
         var containerObj=document .getElementById ("Div_text_"+t)
         containerObj.appendChild(text);
    
    }
    
    
      function CreatButton(i,tt) //创建 按钮
    {
    
        var text = document.createElement("input");
        var text_ = document.createElement("input");
          text.type="button";
          text_.type="button";
    
          text.id ="Button_"+i;
          
          text_.id ="Button_"+i;
         text.value="修改";
         var id="Button_"+i;
         text.onclick=function(){ EditeData(i,tt);};
         text_.value="删除";
        text_.onclick=function(){DeleteData(i,tt);};
         text.style.width="60px";
         text_.style.width="60px";
//         text .disabled ="disabled";
         text .style .textAlign ="center";
          text_ .style .textAlign ="center";
      var containerObj=document .getElementById ("Div_text_edite"+tt)
         containerObj.appendChild(text);
         containerObj.appendChild(text_);
    
    }
    
    function EditeData(id,tt)
    {
    
    GetText("edite",id,tt);
    
    }
       function DeleteData(id,tt)
    {
    
    GetText("delete",id,tt);
    
    }
    
   
   
   //2012-2-4
       
     function Adddivrow(row)
     {
    
     var div=document.createElement("div");
     div.id="AddDiv_"+row;
     div.style.textAlign ="center";
     document.getElementById("AddNewDiv").appendChild(div);
   
     }
        



   function CreatTextNew(row,i,type) //2012-2-4增加添加多条记录
   {
    var text = document.createElement("input");
      text.type="text";
      text.id ="Text__"+row+"_"+i;
      text .name=type ;
      text.style.width="100px";
      text .style .textAlign ="center";
       if(type=="日期类型")
         {
           text.onclick=function(){ ShowCalendar(this.id);};
         }
      var containerObj=document .getElementById ("AddDiv_"+row)
      containerObj.appendChild(text);

   }


