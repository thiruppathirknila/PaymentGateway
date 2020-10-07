var settingComp = $("<div class='ActionBtn position-absolute'><a href='javascript:;' class='bg-primary btn btn-sm px-1 rounded-circle text-white'><i class='fas fa-tools'></i></a><a class='bg-danger btn btn-sm px-1 rounded-circle text-white deleteEle' href='javascript:;'><i class='fas fa-backspace'></i></a></div></div>");
$(document).ready(function () {
    $(".leftBarToggle, .leftBarClose").off('click').on('click', function (e) {
       showLeftBar();
    }); 
});
    
function showLeftBar(){
    $(".leftBar").toggleClass("show");
    $(".leftBarToggle").toggleClass("hide");
}