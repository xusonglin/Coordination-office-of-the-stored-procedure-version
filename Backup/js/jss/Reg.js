 
 
 function JudgeDate(val)
{  titlelist=titlelist.substring(0,titlelist.length-1);
var arrtitle=titlelist.split('_');

    var re=/[1-9]\d{3}(-|\.)?(10|11|12|((0)?(1|2|3|4|5|6|7|8|9)))(-|\.)?((30|31)|((0|1|2)?(1|2|3|4|5|6|7|8|9)))/
    var isdate = false 
    if(val !=undefined  && val!='')
    {
        var m = val.match(re);
        if(m!=null)
        {
            var isrun = false
            var val2 = val.replace(/-|\./g,'')
            var year = val2.substring(0,4)
            var monthday =  val2.replace(year,'')
            var month = monthday.length == 4 ? monthday.substring(0,2) : monthday.substring(0,1)
            var day = monthday.length == 4 ? monthday.substr(2,2) : monthday.substr(1)
            if(day.length==2 && day.substring(0,1)=="0")
                day = day.substr(1)
            if(parseInt(year)/4==0)
                isrun = true
            if("2|02".indexOf(month)>=0 )
                isdate = isrun ? (parseInt(day)<=29 ? true : false) : (parseInt(day)<=28 ? true : false)
            else if("1|01|3|03|5|05|7|07|8|08|10|12".indexOf(month)>=0 )
                isdate = parseInt(day)<=31 ? true : false
            else
                isdate = parseInt(day)<=30 ? true : false
        }
        
       else 
       
       alert(arrtitle[t] + " ： 应输入日期类型的值！！！");
    }
    return isdate

}  
    
    function strDataFormat( val,t)
{ 
//debugger
    var strRet="";
    
    if (JudgeDate(val)==true)
    {
    var    str=val.replace("-","")
        
        if (str.length==8)
        {
            //4位年2位月2位日
            strRet=str
        }
        else if (str.length==7)
        {
            var tmp=str.substr(4,1)
            if (tmp=="0")
                strRet=str.substr(0,6) + "0" + str.substr(6,1)
            else if (tmp=="1")
            {
                if (str.substr(4,2)=="10" || str.substr(4,2)=="11" || str.substr(4,2)=="12")
                    strRet=str.substr(0,6) + "0" + str.substr(6,1)
                else
                    strRet=str.substr(0,4) + "0" + str.substr(4,3)
            }
            else
                strRet=str.substr(0,4) + "0" + str.substr(4,3)
        }
        else if (str.length==6)
        {
            strRet=str.substr(0,4) + "0" + str.substr(4,1) + "0" + str.substr(5,1)
        }
        else
        {
            strRet=""
        }
    }
    
    if (strRet!="")
        strRet=strRet.substr(0,4) + "-" + strRet.substr(4,2) + "-" + strRet.substr(6,2)
//      alert(strRet)  

//     document.getElementById("text_"+t).value=strRet;
    return strRet
   
}


function Regnum(type,num,t,titlelist)
{

titlelist=titlelist.substring(0,titlelist.length-1);
var arrtitle=titlelist.split('_');

if(type=="数值类型")
{

                if(num.match("&")!=null)
              {
              
    alert(arrtitle[t] + " ： 中不能出现“&”！！！");
                      return false ;
              
              }





        if(num=="")

    {alert(arrtitle[t] + " ： 值不能为空！！！");
   return  false

            }
    var reg="^([+-]?)\\d*\\.?\\d+$";
    if(num.match(reg)==null)
    {
    alert(arrtitle[t] + " ： 应输入数值类型的值！！！");

    return  false
    }

    else 

    return true

}

if(type=="日期类型")
{
        if(num.match("&")!=null)
              {
              
    alert(arrtitle[t] + " ： 中不能出现“&”！！！");
                      return false ;
              
              }




  if(num=="")
{
    alert(arrtitle[t] + " ： 值不能为空！！！");
    
       return  false
    
    
    }
      var re=/[1-9]\d{3}(-|\.)?(10|11|12|((0)?(1|2|3|4|5|6|7|8|9)))(-|\.)?((30|31)|((0|1|2)?(1|2|3|4|5|6|7|8|9)))/
   
    if(num.match(re)==null)
    {
    
    alert(arrtitle[t] + " ： 应输入日期类型的值！！！");
    return  false
    }
    
    else 

    

    
  
        return true
}



if(type=="文本类型")

    {

            if(num.match("&")!=null)
              {
              
    alert(arrtitle[t] + " ： 中不能出现“&”！！！");
                      return false ;
              
              }

    
     if(num=="")
    {
    alert(arrtitle[t] + " ： 值不能为空！！！");
      return  false
   
   } 
    return true

    }
    
    
    
    if(type=="货币类型")
    
{  
        if(num.match("&")!=null)
              {
              
    alert(arrtitle[t] + " ： 中不能出现“&”！！！");
                      return false ;
              
              }



     if(num=="")
    {
    alert(arrtitle[t] + " ： 值不能为空！！！");
      return  false
   
   } 
 
      var reg="^([+-]?)\\d*\\.?\\d+$";
   
    if(num.match(re)==null)
    {
    
    alert(arrtitle[t] + " ： 应输入货币类型的值！！！");
    return  false
    }
    
    else 
    return true
}








}
