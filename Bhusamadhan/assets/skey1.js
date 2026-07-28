function init() {
    shortcut.add("Alt+h", function () {
        window.open("Default.aspx", "_self")
    });
    shortcut.add("Alt+d", function () {
        window.open("DepartmentLogin.aspx", "_self")
    });
    shortcut.add("Alt+r", function () {
        window.open("ComplainRegistration.aspx", "_self")
    });
    shortcut.add("Alt+i", function () {
        window.open("BenInfo.aspx", "_self")
    });
    shortcut.add("Alt+e", function () {
        window.open("Helpdesk.aspx", "_self")
    });
    shortcut.add("Alt+q", function () {
        window.open("RequestQuery.aspx", "_self")
    });


    //    shortcut.add("Alt+s", function () {
    //        window.open("ContactUs/ShortcutKeys.aspx", "_self")
    //    });
}
window.onload = init;