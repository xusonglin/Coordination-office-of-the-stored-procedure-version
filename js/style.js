$(function () {
    //给获取输入焦点的文本框加效果强调
    $('input[type="text"]').focus(function () {
        $(this).addClass("focus");
    });

    $('input[type="text"]').blur(function () {
        $(this).removeClass("focus");
    });

    $('input[type="password"]').focus(function () {
        $(this).addClass("focus");
    });

    $('input[type="password"]').blur(function () {
        $(this).removeClass("focus");
    });

  //切换网页字体文字大小
        $('span').click(function () {
            var ourText = $('body');
            var currFontSize = ourText.css('fontSize');
            var finalNum = parseFloat(currFontSize, 10);
            var stringEnding = currFontSize.slice(-2);
            if (this.id == 'spanLarge') {
                finalNum *= 1.2;
            }
            else if (this.id == 'spanSmall') {
                finalNum /= 1.2;
            }
            ourText.css('fontSize', finalNum + stringEnding);
        });
    });
