    
    
//Edite页面使用
    
    
 function CreateTitle(title,Font_Names,Font_Size,ForeColor,ISBold)
   {
// alert(title);
  
     var label = document.createElement("label");
     
      label.id = "text";
   
      label.innerHTML=title;

     label.style.fontSize=Font_Size;

     label.style.fontFamily=Font_Names;

      label.style.color=ForeColor;
      if (ISBold=="Yes"){
   label.style.fontWeight = "Bold";}

var containerObj=document .getElementById ("title");//容器对象用来放置input对象

containerObj.appendChild(label);

  
    }
    
 function CreateLabel(title,Font_Names,Font_Size,ForeColor)
   {
  // alert(title);
  
     var label = document.createElement("input");
     
      label.id = "text";
//      label.innerHTML=title;
     label.value=title;
//label.nodeValue.fontColor ="red";
     label.style.fontSize=Font_Size;

     label.style.fontFamily=Font_Names;

      label.style.color=ForeColor;
     label.style.width="100px";
 label .style.disabled ="disabled";    
     label .style .textAlign ="center";
var containerObj=document .getElementById ("content_label");//容器对象用来放置input对象
//alert (22);
containerObj.appendChild(label);

  
    }
    
    
    
    function CreatText(i,type)
    {
//       alert(i);
//       alert(type);
        var text = document.createElement("input");
          text.type="text";
    
          text.id ="Text_"+i;
          text .name=type ;
   
         text.style.width="100px";
//         text .disabled ="disabled";
         text .style .textAlign ="center";
      var containerObj=document .getElementById ("content_text")
        containerObj.appendChild(text);
    
    }
    
    
