/**
* oneXpath.aspx页面用到的JS
* @author 张浩春 2012-3-24
**/

var sig = 0;
$(function () {
$("#saveBt").hide();
$("#giveupBt").hide();
});
/*
* 显示所有的隐藏input
*/
function showHidden() {
    var sp = document.getElementsByTagName("span");
    if (sig == 0) {
//        $("[type=text]").css("display", 'block');
//        $("#weblanguage").css("display", "block");
//        $("#forbid").css("display", "block");
//        for (var i = 0; i < sp.length - 8; i++) {//隐藏所有span
//            sp[i].style.display = "none";
//        }
        $("#updateBt").hide();
        $("#saveBt").show();
        $("#giveupBt").show();
        sig = 1;
    } else {
//        $("[type=text]").css("display", 'none');
//        $("#weblanguage").css("display", "none");
//        $("#forbid").css("display", "none");
//        for (var i = 0; i < sp.length - 8; i++) {//显示所有span
//            sp[i].style.display = "block";
//        }
        sig = 0;
    }
}
/*
* 放弃修改，隐藏所有的input
*/
function giveup() {
alert()
//    $("#updateBt").show();
//    $("#saveBt").hide();
//    $("#giveupBt").hide();
  //  sig = 1;
   // showHidden();
   
  // window.location="onePath.aspx?Id="+rid;
}
/**
* 更新规则
**/
function update() {
    $.messager.confirm("确认", "确认保存当前修改？", function (val) {
        if (val) {
            $("#updateForm").submit();
        }
    });
}