

//点击评论显示评论输入框和提交取消按钮
function btnpinglun(Countid)
{   document.getElementById("txt_pinglun"+Countid).style.display="block";
    document.getElementById("tr"+Countid).style.display="block";
}
//取消
function btnquxiao(Countid)
{


    document.getElementById("txt_pinglun"+Countid).style.display="none";
    document.getElementById("tr"+Countid).style.display="none";

}

//提交评论
function btntijiao(Countid)
{

        var Content=document.getElementById("txt_pinglun"+Countid).value;
        if(Content.indexOf("_")==-1&&Content.indexOf("&")==-1)
      {
        var type ="tijiao";
        
        jQuery.ajax({
            type: "POST",         
            url: "../Data/Forum.ashx",
            data: "Action=tijao&Countid=" + Countid + "&Content=" + Content+"&userid="+userid,
            success: function(result) {
                if (result == "true") {
                    
    document.getElementById("txt_pinglun"+Countid).style.display="none";
    document.getElementById("tr"+Countid).style.display="none";
                
LoadHf(); 
                }
                else {
                  alert("评论提交失败,请重试!");
                }
            }
        })
        }
        else
        {
             alert("评论内容中不能包含非法字符'&'和'_'!")
        }




}

function LoadHf()
{

    var arrlist=strs.split("&");
    for(var i=0;i<arrlist.length;i++)
    {

        var arr=arrlist[i].split("_");
        var name=arr[4];
        var content=arr[1];
        var Lydate=arr[2];
        var countid=arr[0];
        var lyuserid=arr[3]; 
         paras="id="+Math.random()
         paras = paras + "&CountIDD="+countid;
        url='HfUpdate.aspx'
        var myajax = new Ajax.Updater(
        {success:"Hf_"+countid},
         url,
        {method:'post', parameters:paras, onComplete:function(request){
        }}
    )
    }}
    
    
    
    //删除回复
    function deleteHf(hfid,hfuserid,liuyanuserid)
    {

    if(liuyanuserid==userid|| hfuserid==userid )
    {
    if(confirm("是否确定删除该回复？"))
          jQuery.ajax({
                    type: "POST",           //
                    url: "../Data/Forum.ashx",
                    data: "Action=deletehf&HfID=" + hfid ,
                    success: function(result) {
                        if (result == "true") {
                        LoadHf();
                        }
                        else {
                          
                        }
                    }
                })
                   
      }
      else
    {   
      alert("你无权进行此操作！")}
    }
    
    //发表留言
    function fabiao()
   {
   
   
   var liuyan=document.getElementById("liuyan").value;
   
   if(liuyan=="最近在忙些什么呢.......赶快和大家分享一下吧！")
   {
       alert("请填写你的留言内容！")
       return;
   }
   if(liuyan=="")
   {
       alert("留言内容不能为空！")
       return;
   }
    if(liuyan.indexOf("_")!=-1||liuyan.indexOf("&")!=-1)
    {
    
     alert("留言内容中不能包含非法字符'&'和'_'!")
     return
    }
    document.getElementById("type").value="add";
    document.getElementById("Content").value=liuyan;
    document.getElementById("submit").click();
   
   
   }
   //删除留言
   function deleteliuyan(countid,uid)
   {
   if(confirm("删除留言将同时删除该留言的所有评论，是否继续？"))
   {
     if(userid!=uid)
     {
       
       alert("你无权进行此操作！")
       return;
     }
     
    document.getElementById("type").value="deleteliuyan";
    document.getElementById("CountID").value=countid;
    document.getElementById("submit").click();
   
   }
   
   
   }