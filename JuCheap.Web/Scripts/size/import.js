$(function () {


	$("input[name='file_name']").click(function () {
		$("input[name='up_file']").click();

	})

	$("input[name='up_file']").change(function () {
		$("input[name='file_name']").val($(this).val());
	})
})


 

//提交数据
function analysis() {

	if (!formvalidation()) {
		return;
	}

	var action = $("select[name='size_banxing']").val() + "_" + $("select[name='size_type']").val() + "_" + $("select[name='size_gender']").val();
	$("input[name='action']").val(action);
	$("input[name='import']").val("false");
	$("#size_form").ajaxSubmit({
		type: 'post',
		url: '/Adm/Size/Import',
		success: function (data) {
 
			if (data.state != "1") {
				alert(data.msg);
				return;
			}
			var th_html = "";
			switch (action) {
				case "XF_SY_NAN":


					for (var i = 0; i < XF_SY_NAN_thead.length; i++) {
						th_html += '<th class="column-title">' + XF_SY_NAN_thead[i].split("/")[0] + ' </th>';
					}
					$(".headings").append(th_html);
					break;
				case "XF_SY_NU":
 
					for (var i = 0; i < XF_SY_NU_thead.length; i++) {
						var th=XF_SY_NU_thead[i]+"";
						th_html += '<th class="column-title">' + th.split("/")[0] + ' </th>';
					}

					break;


			}
			$(".headings th").remove();
			$(".headings").append(th_html);

	
			var json = data.msg;
		
			show_dig();
			$(".insert_excel_list").remove();

			var html = "";
 
			switch (action) {
				case "XF_SY_NAN":

					for (var i = 0; i < json.length; i++) {
					  html += "<tr class='even pointer insert_excel_list' >"

						for (var a = 0; a < XF_SY_NAN_thead.length; a++) {
							html += "<td>" + json[i][XF_SY_NAN_thead[a].split("/")[1]] +"</td>";
						}

						html += "</tr>";
					}
					$("#excel_data_list").append(html)
					break;
				case "XF_SY_NU":

					for (var i = 0; i < json.length; i++) {
						html += "<tr class='even pointer insert_excel_list' >"

						for (var a = 0; a < XF_SY_NU_thead.length; a++) {
							html += "<td>" + json[i][XF_SY_NU_thead[a].split("/")[1]] + "</td>";
						}
						html += "</tr>";
					}
					$("#excel_data_list").append(html)
					break;
 
		 
			}



		},
		error: function (XmlHttpRequest, textStatus, errorThrown) {
			console.log(XmlHttpRequest);
			console.log(textStatus);
			console.log(errorThrown);
		}
	});
}

//导入数据库
function _import() {
	$("input[name='import']").val("true");
	$("#size_form").ajaxSubmit({
		type: 'post',
		url: '/Adm/Size/Import',
		success: function (data) {


			if (data.state == "1") {
				alert("导入成功。");
				hide_dig();
			} else {
				alert("导入出错：" + data.msg);
				hide_dig();
			}
		},
		error: function (XmlHttpRequest, textStatus, errorThrown) {
			console.log(XmlHttpRequest);
			console.log(textStatus);
			console.log(errorThrown);
		}
	});
}

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


function formvalidation() {
	if (!$("input[name='Size_Code']").val()) {
		alert("请输入尺码表编号。");
		return false;
	}

	if (!$("input[name='file_name']").val()) {
		alert("请输入上传尺码表。");
		return false;
	}
	return true;
}