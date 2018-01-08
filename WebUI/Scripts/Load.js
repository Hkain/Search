var Load;
var page = 0;
var callBack = false;
var isScrollEnd = false;
var search = false;
(function (Load) {
    

    Load.ScrollHand = function () {
        if (isScrollEnd == false && search == false && ($(document).scrollTop() <= $(document).height() - $(window).height())) {
            loadData(url);
        }
    }

    function loadData(loadUrl) {
        if (page > -1 && !callBack) {
            callBack = true;
            page++;
            var search = searchStr();
            $.ajax({
                type: "GET",
                data: "pageNum=" + page + "&searchStr=" + search,
                success: function (data, textstatus) {
                    if (data != '') {
                        
                        $('#myUL').append(data);
                    }
                    else {
                        page = -1;
                    }
                    callBack = false;
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(errorThrown);
                }
            });
        }
    }

    var timerRefresh = {};
    var timerFunctions = {};
    function refreshTimer() {
        for (var key in timerRefresh) {
            window.clearInterval(timerRefresh[key]);
            timerRefresh[key] = window.setTimeout(timerFunctions[key], 1000);
        }
    }

    $(document).ready(function () {
        $(document).on('mousemove keypress', refreshTimer);
    })

    Load.timerRefresh = function (id) {
        if (timerRefresh[id]) {
            window.clearTimeout(timerRefresh[id]);
        }
        else {
            for (var key in timerRefresh) {

            }
        }
        timerFunctions[id] = function () {
            Load.loadDataSearch();
            delete timerRefresh[id];
            delete timerFunctions[id];
        }

        timerRefresh[id] = window.setTimeout(timerFunctions[id], 200);
    }


    Load.loadDataSearch = function () {
        if (page == -1) { page = 0; }
        loadData(url)
    }

    function searchStr()
    {
        
        var str = $('#myInput').val();
        str = str.trim()
        if (str != '') {
            $('#myUL').empty();
            search = true;
        }
        else {
            search = false;
        }
        return str;
    } 

})(Load || (Load = {}))