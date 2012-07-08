<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EditeTask.aspx.cs" Inherits="JiaoShiXinXiTongJi.Task.EditeTask" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>

    <script src="../js/jss/AddElement.js" type="text/javascript"></script>

    <script src="../js/jss/Reg.js" type="text/javascript"></script>
    
    <script src="../js/jss/Date.js" type="text/javascript"></script>

</head>
<body style="background-color: #D7E6ED">
    <form id="form1"  runat="server" style="background-color: #D7E6ED;  ">
    
        <div style=" text-align: center; width:100%" id="title">
        </div>
        <div style=" padding-left:60%"> <table > <tr><td >
            <asp:Label ID="Label1" runat="server" Text="【任务创建者：】"></asp:Label><asp:Label ID="taskmaker"
                runat="server" Text="Label" Font-Bold="true"></asp:Label></td></tr></table></div>
        <div style=" text-align: center;white-space:nowrap; " id="LabelField"></div> <%--列名--%>
       
        <div id="DivEdite" style="text-align: center;white-space:nowrap"></div><%--显示已填写数据--%>
        <div style="text-align:right"> <input type="button" value="添加记录" onclick="addnewtext()" /></div>
         
         <div id="AddNewDiv" style="width: 100%;white-space:nowrap;"></div><%--添加新纪录--%>
        
         




        <script type="text/javascript">
        
         var list="<%=str %>";   
         var list_text="<%=str_text %>";    //字段信息
         var GeRenXi="<%=GeRenXi %>";
         var GeRenXiInfo="<%=GeRenXiInfo %>";
  
         var stringdata="<%=stringdata %>"; //已经填写的信息
     
           var row=0;
         stringdata=stringdata.substring(0,stringdata.length-1);
         
         var title;
         var Font_Names;
         var Font_Size;
         var ForeColor;
         var valuelist;
         var titlelist='';//在验证的时候显示列名
　       var list1=list.substring (0,list .length-1);
         var arr=list1.split('_');
         var len=arr.length;
        for(var i=0;i<arr.length ;i++)
        {
             var arr_=arr[i].split(',');
             for (var j=0;j<arr_.length;j++)
             {
                 title =arr_[0];
                 Font_Names=arr_[1];
                 Font_Size=arr_[2];
                 ForeColor=arr_[3];
                 ISBold=arr_[4];
             }
           
            CreateTitle(title,Font_Names,Font_Size,ForeColor,ISBold); //标题写在页面上
        
        }
       
       
        
      var datacount=stringdata.split("&").length;  // 已经填写的信息
       for(var tt=0;tt<datacount;tt++)
       {
        if(stringdata!=""&&stringdata!=null)
          {
  
               AddDivEdite(tt);
               addedite(tt)
             var arrdata=stringdata.split("&");
             
             var px=arrdata[tt].substring(0,arrdata[tt].length-1).split("#")[arrdata[tt].substring(0,arrdata[tt].length-1).split("#").length-1]
             
                CreatButton(px,tt);//对应创建一个按钮 ，填写信息
          }
       }





        var list1_text=list_text.substring (0,list_text .length-1);  //字段信息h
        var arr_text=list1_text.split('_');
        var len_text=arr_text.length;
        for(var t=0;t<arr_text.length ;t++)
        {
             var  text;
             var Text_Font_Names;
             var Text_Font_Size;
             var Text_ForeColor;
             var arr_text_=arr_text[t].split(',');
               for (var k=0;k<arr_text_.length;k++)
                {
                 text =arr_text_[0];
                 Text_Font_Names=arr_text_[1];
                 Text_Font_Size=arr_text_[2];
                 Text_ForeColor=arr_text_[3];
                 type=arr_text_[4];
                  }
           titlelist+=arr_text_[0]+"_";
          CreateLabel(text,Text_Font_Names,Text_Font_Size,Text_ForeColor); // 字段信息

           for(var tt=0;tt<datacount;tt++)
            {
              if(stringdata!=""&&stringdata!=null)
              {
       
                CreatTextadd(tt,type,t );//对应创建一个text ，填写信息
               var arrdata=stringdata.split("&");
               document .getElementById ("Text_"+tt+"_"+t).value=arrdata[tt].substring(0,arrdata[tt].length-1).split("#")[t];
               }
             } 


            }

 
    function addnewtext()
    {

      document.getElementById("tijiao").style.visibility="visible";
        Adddivrow(row);//创建一个div
   
        var add_list_text=list_text.substring (0,list_text .length-1);  //字段信息
         add_list_text=add_list_text.split('_');
        
        var add_len_text=add_list_text.length;
        
        for(var t=0;t<add_len_text ;t++)
        {
        
             var  text;
             var Text_Font_Names;
             var Text_Font_Size;
             var Text_ForeColor;
             var arr_text_=add_list_text[t].split(',');
               for (var k=0;k<arr_text_.length;k++)
                {
                 text =arr_text_[0];
                 Text_Font_Names=arr_text_[1];
                 Text_Font_Size=arr_text_[2];
                 Text_ForeColor=arr_text_[3];
                 type=arr_text_[4];
                  }
              
           CreatTextNew(row,t,type );//对应创建一个text ，填写信息,增加row加以区别
         
             var arr_GeRenXiInfo=GeRenXiInfo.split("&"); //个人信息填充
             var arr_GeRenXi=GeRenXi.split("&");
             var countt=0;
             for(var r=0;r<arr_GeRenXi.length;r++)
                {
                  if(text!=arr_GeRenXi[r])
                     {
                       countt ++;
                     }
                     else
                      {
                      counttt=countt+1;
                     document .getElementById ("Text__"+row+"_"+t).value=arr_GeRenXiInfo [countt];
                     }
                 }

            }
    
           row++;
 
    }





//提交数据
function GetText(type,i,ttt) // 提交数据时的操作
{

 var add_list_text=list_text.substring (0,list_text .length-1);  //字段信息
         add_list_text=add_list_text.split('_');
        
        var add_len_text=add_list_text.length;
if(type=="add")
{
    var count=0;
    var Text_valuelist='';
    for(var w=0;w<row;w++)
    {
    
    
    var textrow="";
     for(t=0;t<add_len_text ;t++)
       {
        var dd="Text__"+w+"_"+t;
        var TextValue=document.getElementById (dd).value;
        var type=document.getElementById (dd).name;//取类型
        if( Regnum(type,TextValue,t,titlelist )==false )break;  //验证数据格式
        textrow += TextValue + "_";
        count++; //做个标记，以判断是不是全都正确
         }
    
   Text_valuelist +=textrow+"&";
    
    
    }
   
    if(count>=add_len_text*(row))
    {
  
        document .getElementById ("type").value = "add";
        document .getElementById ("dataid").value = i;
        document .getElementById ("Text_valuelist").value = Text_valuelist;
    document.getElementById("Submit").click ();
        return true;
    }
   else 
    return false;
}



if(type=="edite")
{

var tt=ttt;
var count=0;
 var Text_valuelist='';
for(t=0;t<len_text ;t++)
{

var dd="Text_"+tt+"_"+t;
var TextValue=document.getElementById (dd).value;

var type=document.getElementById (dd).name;//取类型

 if( Regnum(type,TextValue,t,titlelist )==false )break;  //验证数据格式

Text_valuelist += TextValue + "&";

count++; //做个标记，以判断是不是全都正确
         
}
if(count>=len_text)
{
    document .getElementById ("type").value = "edite";
    document .getElementById ("dataid").value = i;

    document .getElementById ("Text_valuelist").value = Text_valuelist;
    document.getElementById("Submit").click ();
    return true;
}
else 
    return false;

}




if(type=="delete")
{



    document .getElementById ("type").value = "delete";
    document .getElementById ("dataid").value = i;
    document .getElementById ("Text_valuelist").value = Text_valuelist;
    document.getElementById("Submit").click ();


}


 }
// 
// 
//document.getElementById("form1").style.width=len_text*120
 
 document.form1.style.width=len_text*120;

 
 
        </script>

        <div id="Div1" runat="server">
            <form id="form2" action="EditeTask.aspx" method="post">
                <div style="text-align: right">
                    <input name="Text_valuelist" type="hidden" id="Text_valuelist" />
                    <input name="type" type="hidden" id="type" />
                    <input name="dataid" type="hidden" id="dataid" />
                    <input id="Submit" type="submit" value="submit" visible="false" style="visibility: hidden" />
                   
                    <input type="button" value="提交数据" onclick="GetText('add')" id="tijiao" style="visibility: hidden" />
                    <%--注意提交前的验证，和onsubmit--%>
                    
                </div>
            </form>
        </div>
    </form>
</body>
</html>
