var XF_SY_NAN_thead = new Array();// 西服上衣  男
XF_SY_NAN_thead[0] = '身高/Height';
XF_SY_NAN_thead[1] = '前身长/FrontLength';
XF_SY_NAN_thead[2] = '净胸围/NetBust';
XF_SY_NAN_thead[3] = "成品胸围/FinishedBust";
XF_SY_NAN_thead[4] = "中腰/InWaist";
XF_SY_NAN_thead[5] = "成品下摆【不开叉】/FinishedHem_NoFork";
XF_SY_NAN_thead[6] = "成品下摆【开叉】/FinishedHem_SplitEnds";
XF_SY_NAN_thead[7] = "肩宽/ShoulderWidth";
XF_SY_NAN_thead[8] = "袖长/Sleecve_Show";


var XF_SY_NU_thead = new Array(); //西服上衣  女
XF_SY_NU_thead[0] = '身高/Height';
XF_SY_NU_thead[1] = '前身长/FrontLength';
XF_SY_NU_thead[2] = '净胸围/NetBust';
XF_SY_NU_thead[3] = "成品胸围/FinishedBust";
XF_SY_NU_thead[4] = "中腰/InWaist";
XF_SY_NU_thead[5] = "成品下摆【不开叉】/FinishedHem_NoFork";
XF_SY_NU_thead[6] = "袖肥/SleeveWidth";
XF_SY_NU_thead[7] = "肩宽/ShoulderWidth";
XF_SY_NU_thead[8] = "袖长/Sleecve_Show";


var XF_KZ_NAN_thead = new Array(); //西服裤子  男
XF_KZ_NAN_thead[0] = '编码/Code';
XF_KZ_NAN_thead[1] = '单褶成品臀围/DZ_HipLength_CP';
XF_KZ_NAN_thead[2] = '双褶成品臀围/SZ_HipLength_CP';
XF_KZ_NAN_thead[3] = "横档/Crosspiece";
XF_KZ_NAN_thead[4] = "腿肥 浪下10CM/LegWidth_UnderTheWaves";
XF_KZ_NAN_thead[5] = "前浪连腰/FrontRise_EvenWaist";
XF_KZ_NAN_thead[6] = "后浪连腰/AfterTheWaves_EvenWaist";
XF_KZ_NAN_thead[7] = "净臀围/NetHip";
XF_KZ_NAN_thead[8] = "成品腰围/CP_WaistWidth";
XF_KZ_NAN_thead[9] = "身高/Height";
XF_KZ_NAN_thead[10] = "裤长/LongPants";
XF_KZ_NAN_thead[11] = "净腰围/NetWaist";

var HanderDataForXF_SY_thead = new Array(); //处理后西服上衣 
HanderDataForXF_SY_thead[0] = '订单编号/OrderCode';
HanderDataForXF_SY_thead[1] = '项次/option';
HanderDataForXF_SY_thead[2] = '姓名/Name';
HanderDataForXF_SY_thead[3] = "处理前数据/RtnQCode";
HanderDataForXF_SY_thead[4] = "处理后数据/RtnHCode";
HanderDataForXF_SY_thead[5] = "身高/Height";
HanderDataForXF_SY_thead[6] = "数量/Number";
HanderDataForXF_SY_thead[7] = "衣长/Yichang";
HanderDataForXF_SY_thead[8] = "胸围/Bust";
HanderDataForXF_SY_thead[9] = "袖长/Sleeve";

var HanderDataForXF_KZ_thead = new Array(); //西服裤子  男
HanderDataForXF_KZ_thead[0] = '序号/Index';
HanderDataForXF_KZ_thead[1] = '姓名/Name';
HanderDataForXF_KZ_thead[2] = "身高/Height";
HanderDataForXF_KZ_thead[3] = "处理前数据/RtnQCode";
HanderDataForXF_KZ_thead[4] = '腰围/waistWidth';
HanderDataForXF_KZ_thead[5] = "单褶臀围/DZ_Hipline";
HanderDataForXF_KZ_thead[6] = "双褶臀围/SZ_Hipline";
HanderDataForXF_KZ_thead[7] = "数量/Number";
 

//显示弹出框
function show_dig() {

    $("#myModal").modal('show');
    $('#myModal').modal({
        keyboard: true
    })
}

//隐藏弹出框
function hide_dig() {

    $("#myModal").modal('hide');

}


//加载弹出框
//表头    数据   按钮html      是否是输入框
function loadPopup(thead, data, buttonHtml, isInput, zdyTop) {
    $("#myModal").remove();
    var PopupHtml = "";
    PopupHtml+='<div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">'
    PopupHtml +='<div class="modal-dialog" style="width: 70%">'
    PopupHtml +='<div class="modal-content">'
    PopupHtml +='<div class="modal-header">'
    PopupHtml +='<button type="button" class="close" data-dismiss="modal" aria-hidden="true">'
    PopupHtml +='&times;'
    PopupHtml +='</button>'
    PopupHtml +='<h4 class="modal-title" id="myModalLabel">'
    PopupHtml +=' 检视'
    PopupHtml +='</h4>'
    PopupHtml += '</div>'
    PopupHtml += zdyTop;
    PopupHtml +='<div class="modal-body">'
    PopupHtml +='<div class="x_panel">'
    PopupHtml +='<div class="x_title">'
    PopupHtml +='<div class="clearfix"></div>'
    PopupHtml +='</div>'
    PopupHtml +='<div class="x_content">'
    PopupHtml +='<div class="table-responsive" style="height: 400px; overflow: auto" id="scroll-1">'
    PopupHtml +='<form action="/Adm/Size/UpdateCode" method="post" enctype="multipart/form-data" id="updateID">'
    PopupHtml +='<input type="hidden" name="Action" />'
    PopupHtml +='<table class="table table-striped custom-table table-hover" id="Popup_Table">'
    PopupHtml += '<thead>'
    PopupHtml +='</thead>'
    PopupHtml += '<tbody>';
    PopupHtml += '</tbody > '
    PopupHtml +='</table>'
    PopupHtml +='</form>'
    PopupHtml +='</div>'
    PopupHtml +='</div>'
    PopupHtml +='</div>'
    PopupHtml +='</div>'
    PopupHtml += '<div class="modal-footer">'
    PopupHtml += buttonHtml;
    PopupHtml +='</div>'
    PopupHtml +='</div>'
    PopupHtml +='</div>'
    PopupHtml +='</div>'


    $("body").append(PopupHtml);

    var th_html = "";
 
    for (var i = 0; i < thead.length; i++) {
        th_html += '<th class="hidden-xs">' + thead[i].split("/")[0] + ' </th>';
    }
    $("#Popup_Table thead th").remove();
    $("#Popup_Table thead").append(th_html);
    var html = "";
 
    for (var i = 0; i < data.length; i++) {
        html += "<tr class='even pointer insert_excel_list' >"

        if (isInput) {
            html += "<input name='ID' type='hidden' value='" + data[i]["ID"] + "'   />";
        }  
        for (var a = 0; a < thead.length; a++) {

            var txt = data[i][thead[a].split("/")[1]];
         
            if (isInput) {
                html += "<td style='padding:2px 0px'><input style='border:0px;width:105px' type='text' name='" + thead[a].split("/")[1] + "' value='" + txt + "'/></td>";
            } else {
                html += "<td style='padding:2px 0px'>" + txt+"</td>";
            }
        }

        html += "</tr>";
    }
    $("#Popup_Table tbody tr").remove();
    $("#Popup_Table tbody").append(html)

}