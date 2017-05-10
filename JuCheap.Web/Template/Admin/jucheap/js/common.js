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