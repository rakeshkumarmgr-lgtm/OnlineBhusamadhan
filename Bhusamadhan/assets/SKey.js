/**
* http://www.openjs.com/scripts/events/keyboard_shortcuts/
* Version : 2.01.B
* By Binny V A
* License : BSD
*/

shortcut = {
    'all_shortcuts': {}, //All the shortcuts are stored in this array
    'add': function (shortcut_combination, callback, opt) {
        //Provide a set of default options
        var default_options = {
            'type': 'keydown',
            'propagate': false,
            'disable_in_input': false,
            'target': document,
            'keycode': false
        }
        if (!opt) opt = default_options;
        else {
            for (var dfo in default_options) {
                if (typeof opt[dfo] == 'undefined') opt[dfo] = default_options[dfo];
            }
        }

        var ele = opt.target;
        if (typeof opt.target == 'string') ele = document.getElementById(opt.target);
        var ths = this;
        shortcut_combination = shortcut_combination.toLowerCase();

        //The function to be called at keypress
        var func = function (e) {
            e = e || window.event;

            if (opt['disable_in_input']) { //Don't enable shortcut keys in Input, Textarea fields
                var element;
                if (e.target) element = e.target;
                else if (e.srcElement) element = e.srcElement;
                if (element.nodeType == 3) element = element.parentNode;

                if (element.tagName == 'INPUT' || element.tagName == 'TEXTAREA') return;
            }

            //Find Which key is pressed
            if (e.keyCode) code = e.keyCode;
            else if (e.which) code = e.which;
            var character = String.fromCharCode(code).toLowerCase();

            if (code == 188) character = ","; //If the user presses , when the type is onkeydown
            if (code == 190) character = "."; //If the user presses , when the type is onkeydown

            var keys = shortcut_combination.split("+");
            //Key Pressed - counts the number of valid keypresses - if it is same as the number of keys, the shortcut function is invoked
            var kp = 0;

            //Work around for stupid Shift key bug created by using lowercase - as a result the shift+num combination was broken
            var shift_nums = {
                "`": "~",
                "1": "!",
                "2": "@",
                "3": "#",
                "4": "$",
                "5": "%",
                "6": "^",
                "7": "&",
                "8": "*",
                "9": "(",
                "0": ")",
                "-": "_",
                "=": "+",
                ";": ":",
                "'": "\"",
                ",": "<",
                ".": ">",
                "/": "?",
                "\\": "|"
            }
            //Special Keys - and their codes
            var special_keys = {
                'esc': 27,
                'escape': 27,
                'tab': 9,
                'space': 32,
                'return': 13,
                'enter': 13,
                'backspace': 8,

                'scrolllock': 145,
                'scroll_lock': 145,
                'scroll': 145,
                'capslock': 20,
                'caps_lock': 20,
                'caps': 20,
                'numlock': 144,
                'num_lock': 144,
                'num': 144,

                'pause': 19,
                'break': 19,

                'insert': 45,
                'home': 36,
                'delete': 46,
                'end': 35,

                'pageup': 33,
                'page_up': 33,
                'pu': 33,

                'pagedown': 34,
                'page_down': 34,
                'pd': 34,

                'left': 37,
                'up': 38,
                'right': 39,
                'down': 40,

                'f1': 112,
                'f2': 113,
                'f3': 114,
                'f4': 115,
                'f5': 116,
                'f6': 117,
                'f7': 118,
                'f8': 119,
                'f9': 120,
                'f10': 121,
                'f11': 122,
                'f12': 123
            }

            var modifiers = {
                shift: { wanted: false, pressed: false },
                ctrl: { wanted: false, pressed: false },
                alt: { wanted: false, pressed: false },
                meta: { wanted: false, pressed: false }	//Meta is Mac specific
            };

            if (e.ctrlKey) modifiers.ctrl.pressed = true;
            if (e.shiftKey) modifiers.shift.pressed = true;
            if (e.altKey) modifiers.alt.pressed = true;
            if (e.metaKey) modifiers.meta.pressed = true;

            for (var i = 0; k = keys[i], i < keys.length; i++) {
                //Modifiers
                if (k == 'ctrl' || k == 'control') {
                    kp++;
                    modifiers.ctrl.wanted = true;

                } else if (k == 'shift') {
                    kp++;
                    modifiers.shift.wanted = true;

                } else if (k == 'alt') {
                    kp++;
                    modifiers.alt.wanted = true;
                } else if (k == 'meta') {
                    kp++;
                    modifiers.meta.wanted = true;
                } else if (k.length > 1) { //If it is a special key
                    if (special_keys[k] == code) kp++;

                } else if (opt['keycode']) {
                    if (opt['keycode'] == code) kp++;

                } else { //The special keys did not match
                    if (character == k) kp++;
                    else {
                        if (shift_nums[character] && e.shiftKey) { //Stupid Shift key bug created by using lowercase
                            character = shift_nums[character];
                            if (character == k) kp++;
                        }
                    }
                }
            }

            if (kp == keys.length &&
						modifiers.ctrl.pressed == modifiers.ctrl.wanted &&
						modifiers.shift.pressed == modifiers.shift.wanted &&
						modifiers.alt.pressed == modifiers.alt.wanted &&
						modifiers.meta.pressed == modifiers.meta.wanted) {
                callback(e);

                if (!opt['propagate']) { //Stop the event
                    //e.cancelBubble is supported by IE - this will kill the bubbling process.
                    e.cancelBubble = true;
                    e.returnValue = false;

                    //e.stopPropagation works in Firefox.
                    if (e.stopPropagation) {
                        e.stopPropagation();
                        e.preventDefault();
                    }
                    return false;
                }
            }
        }
        this.all_shortcuts[shortcut_combination] = {
            'callback': func,
            'target': ele,
            'event': opt['type']
        };
        //Attach the function with the event
        if (ele.addEventListener) ele.addEventListener(opt['type'], func, false);
        else if (ele.attachEvent) ele.attachEvent('on' + opt['type'], func);
        else ele['on' + opt['type']] = func;
    },

    //Remove the shortcut - just specify the shortcut and I will remove the binding
    'remove': function (shortcut_combination) {
        shortcut_combination = shortcut_combination.toLowerCase();
        var binding = this.all_shortcuts[shortcut_combination];
        delete (this.all_shortcuts[shortcut_combination])
        if (!binding) return;
        var type = binding['event'];
        var ele = binding['target'];
        var callback = binding['callback'];

        if (ele.detachEvent) ele.detachEvent('on' + type, callback);
        else if (ele.removeEventListener) ele.removeEventListener(type, callback, false);
        else ele['on' + type] = false;
    }
};

function init() {
    shortcut.add("Alt+h", function () {
        window.open("../New_V/Default.aspx", "_self")
    });
    
    shortcut.add("Alt+y", function () {
        window.open("../New_V/objective.aspx?Objective=1", "_self")
    });
      shortcut.add("Alt+a", function () {
       window.open("Documents.aspx?Acts=ActsRules.aspx", "_self")
    });
       shortcut.add("Alt+j", function () {
       window.open("AssetDeclaration.aspx", "_self")
    });
        shortcut.add("Alt+d", function () {
        window.open("Documents.aspx?Circulars=Circulars.aspx", "_self")
    });
     shortcut.add("Alt+n", function () {
     window.open("Documents.aspx?Notice=Notice.aspx", "_self")
    });
       shortcut.add("Alt+o", function () {
       window.open("Documents.aspx?OfficeOrder=OfficeOrder.aspx", "_self")
    });
    shortcut.add("Alt+r", function () {
        window.open("Documents.aspx?RTI=RTI.aspx", "_self")
    });
      shortcut.add("Alt+t", function () {
        window.open("Documents.aspx?TransferOrder=TransferOrder.aspx", "_self")
    });
        shortcut.add("Alt+g", function () {
        window.open("../New_V/Gallery.aspx", "_self")
    });
    shortcut.add("Alt+p", function () {
    window.open("http://elabharthi.bih.nic.in/Public/DigitalSignPdfReport.aspx", "_blank")
    });

  
    shortcut.add("Alt+m", function () {
       window.open("http://elabharthi.bih.nic.in/PaymentReports/CheckBefeficiaryStatus.aspx", "_blank")
   });

    shortcut.add("Alt+k", function () {
       window.open("http://elabharthi.bih.nic.in/Public/BenStatuslist.aspx", "_blank")
   });
    shortcut.add("Alt+l", function () {
       window.open("http://elabharthi.bih.nic.in/PaymentReports/CheckPaymentStatus.aspx", "_blank")
   });

   
 
   shortcut.add("Alt+b", function () {
       window.open("http://elabharthi.bih.nic.in/Public/BeneficiaryList.aspx", "_blank")
   });
    shortcut.add("Alt+c", function () {
        window.open("ContactUs.aspx", "_self")
    });
    shortcut.add("Alt+e", function () {
        window.open("Enquiry.aspx", "_self")
    });
    shortcut.add("Alt+s", function () {
        window.open("../New_V/ShortcutKeys.aspx", "_self")
    });
}



//$(window).scroll(function () {
//    var sticky = $('.sticky'),
//        scroll = $(window).scrollTop();

//    if (scroll >= 50) sticky.addClass('fixed');
//    else sticky.removeClass('fixed');
//});

$(document).ready(function () {
    var resize = new Array('body', 'li', 'p', 'a', 'h2', 'h3', 'span');
    resize = resize.join(',');
    var menu = '.jetmenu > li > a';
    var topmenuli = '.top_head ul li';
    var topmenu = '.top_head ul li a';
    //resets the font size when "reset" is clicked
    var resetFont = $(resize).css('font-size');
    $(".reset").click(function () {
        $(resize).css('font-size', resetFont);
        $(topmenu).css('font-size', '11px');
        $(topmenuli).css('font-size', '11px');
          $(menu).css('padding', '5px 10px');
    });

    $('.Aplus').on('click', function () {
        $(resize).animate({ 'font-size': '+=1' });
        $(menu).css('padding', '15px 18px');
    });

    $('.Aminus').on('click', function () {
        $(resize).animate({ 'font-size': '-=1' });
        $(menu).css('padding', '5px 10px');
    });
});

var setCookie = function (n, val) {
    var minutes = 5;
    var d = new Date();
    d.setTime(d.getTime() + (minutes * 60 * 1000));
    var expires = "expires=" + d.toGMTString();
    document.cookie = n + "=" + val + "; " + expires;
};

var getCookie = function (n) {
    var name = n + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1);
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
};

document.onclick = function (e) {
    if (e.target.className == 'themebtn') {
        var favColor = e.target.style.backgroundColor;
        var ForeColor = e.target.style.color;
        setCookie('color', favColor);
        setCookie('ForeColor', ForeColor);
        document.body.style.backgroundColor = favColor;
        document.body.style.color = ForeColor;
        var logoText = document.querySelector('.text-logo').children;
        for (var i = 0; i < logoText.length; i++) {
            logoText[i].style.color = ForeColor;
        }
        console.log(favColor);
        console.log(ForeColor);
        back();
    }
};

window.onload = function () {
    back();
    init();
    Googleinput();
};

function back() {
  
    var favColor = document.body.style.backgroundColor;
    var ForeColor = document.body.style.Color;
    
    var color = getCookie('color');
    var Frontcolor = getCookie('ForeColor');
    if (color === '') {
        document.body.style.backgroundColor = favColor;
        document.body.style.color = ForeColor;
        //document.html.style.backgroundColor = favColor;
        //document.html.style.color = ForeColor;
        var logoText = document.querySelector('.text-logo').children;
        for (var i = 0; i < logoText.length; i++) {
            logoText[i].style.color = ForeColor;
        }
    } else {
        document.body.style.backgroundColor = color;
        document.body.style.color = Frontcolor;
        //document.html.style.backgroundColor = color;
        //document.html.style.color = Frontcolor;
        var logoText = document.querySelector('.text-logo').children;
        for (var i = 0; i < logoText.length; i++) {
            logoText[i].style.color = Frontcolor;
        }
    }
};
function storeid() {
    var Id = document.getElementsByClassName('imagId');
    var IdStore = new Array();
    var modalBox = $(this).attr('data-modal-id');
    var iDD = $('#' + modalBox);
    for (var i = 0; i < Id.length; i++) {
        var eleId = Id[i].getAttribute("data-modal-id");
        IdStore[i] = (eleId);
    }

    return IdStore;
}


$(function () {
    var appendthis = ("<div class='modal-overlay js-modal-close'></div>");
    $('a[data-modal-id]').click(function (e) {
        e.preventDefault();
        $("body").append(appendthis);
        var modalBox = $(this).attr('data-modal-id');
        $('#' + modalBox).fadeIn($(this).data());
        $('#' + modalBox).css({ display: "block" });
        $(".modal-overlay").fadeTo(500, 0.7);

        $(".previous").click(function (e) {
            var idStore = storeid();
            idStore.sort(function (a, b) { return a - b; });
            var strids = '#' + modalBox;
            var strid = strids.slice(1);
            var currentIndex = idStore.indexOf(strid)
            if (currentIndex == 0) {

            }
            else {
                idStore[currentIndex - 1];
            }
            alert(eee);


        });

        $(".Next").click(function (e) {
            var idStore = storeid();
            idStore.sort(function (a, b) { return a - b; });
            var strids = '#' + modalBox;
            var strid = strids.slice(1);
            var currentIndex = idStore.indexOf(strid)
            if (currentIndex == idStore.length) {

            }
            else {
                idStore[currentIndex + 1];
            }

            document.getElementById(currentIndex).style.display = "none";
            document.getElementById(currentIndex + 1).style.display = "block";

        });
    });
    $(".js-modal-close, .modal-overlay").click(function () {
        $(".modal-box, .modal-overlay").fadeOut(500, function () {
            $(".modal-overlay").remove();
        });

    });

    $(window).resize(function () {
        $(".modal-box").css({
            top: ($(window).height() - $(".modal-box").outerHeight()) / 2,
            left: ($(window).width() - $(".modal-box").outerWidth()) / 2,

        });
    });

    $(window).resize();

});