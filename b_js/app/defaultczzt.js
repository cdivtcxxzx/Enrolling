function load() {
    //迎新批次数据
    $.ajax({
        url: "/nradmingl/appserver/manager.aspx",
        type: "get",
        dataType: "text",
        data: {"cs": "get_freshbatch_welcome_list"},
        success: function (data) {
            var json_data = JSON.parse(data);
            if (json_data.code == 'success') {
                for (i = 0; json_data.data != null && i < json_data.data.length; i++) {
                    var item = json_data.data[i];
                    var str = '';
                    if (i == 0) {
                        str = str + '<option value="' + item.PK_Batch_NO + '" selected="">' + item.Batch_Name + '</option>';
                        batchchange(item.PK_Batch_NO);
                    } else {
                        str = str + '<option value="' + item.PK_Batch_NO + '" >' + item.Batch_Name + '</option>';
                    }
                    $('#freshbatch').append(str);
                }

                $('#freshbatch').change(function () {
                    batchchange($(this).children('option:selected').val());
                });
            } else {
                alert(json_data.message);
            }
        },
        error: function (data) {
            alert("错误");
        }
    });
}

function batchchange(value) {
    var pk_batch_no = value;
    $('#collageList option').remove();
    $.ajax({
        url: "/nradmingl/appserver/manager.aspx",
        type: "get",
        dataType: "text",
        data: {"cs": "get_batch_collage", "pk_batch_no": pk_batch_no},
        success: function (data) {
            var json_data = JSON.parse(data);
            if (json_data.code == 'success') {
                for (i = 0; json_data.data != null && i < json_data.data.length; i++) {
                    var item = json_data.data[i];
                    var str = '';
                    if (i == 0) {
                        str = '<option value="all" selected="">所有</option>';
                        $('#collageList').append(str);
                        collagechange('all', pk_batch_no);
                    }
                    str = '<option value="' + item.College_NO + '" >' + item.collage + '</option>';
                    $('#collageList').append(str);
                }
                $('#collageList').change(function () {
                    collagechange($(this).children('option:selected').val(), pk_batch_no);
                });
            } else {
                alert(json_data.message);
            }
        },
        error: function (data) {
            alert("错误");
        }
    });
}

function clear() {
    $('#studentcount').html('0');
    $('#mancount').html('0');
    $('#womancount').html('0');

    $('#collagecount').html('0');
    $('#specount').html('0');
    $('#spehasclasscount').html('0');
    $('#spenohasclasscount').html('0');
    $('#campuscount').html('0');
    $('#classhasstudentcount').html('0');
    $('#spenohasclassstudentcount').html('0');
    $('#classhasstudent_buterrorcount').html('0');
    $('#hasbedcount').html('0');
    $('#nohasbedcount').html('0');
    $('#hasbed_buterrorcount').html('0');
    $('#hascollageaffairacount').html('0');
    $('#nohascollageaffairacount').html('0');
    $('#collegefinancialcount').html('0');

}

function collagechange(pk_collage_no, pk_batch_no) {
    clear();
    if (pk_collage_no === 'all') {
        pk_collage_no = '';
    }

    $.ajax({
        url: "/nradmingl/appserver/manager.aspx",
        type: "get",
        dataType: "text",
        data: {"cs": "get_batch_collage_outline_status", "pk_batch_no": pk_batch_no, "pk_collage_no": pk_collage_no},
        success: function (data) {
            //console.log(data);
            var json_data = JSON.parse(data);
            if (json_data.code == 'success') {
                var i = 0;

                var student_gender = json_data.data.student_gender;//迎新学生
                if (student_gender) {
                    var studentcount = 0;
                    var mancount = 0;
                    for (i = 0; i < student_gender.length; i++) {
                        var item = student_gender[i];
                        if (item.gender == '男') {
                            $('#mancount').html(item.gendercount);
                            mancount = mancount + parseInt(item.gendercount);
                        }
                        if (item.gender == '女') {
                            $('#womancount').html(item.gendercount);
                            mancount = mancount + parseInt(item.gendercount);
                        }
                    }
                    $('#studentcount').html(mancount);
                }

/*                var collage = json_data.data.collage;//迎新学院
                if (collage) {
                    $('#collagecount').html(collage.length);
                }*/

                var spe = json_data.data.spe;//迎新专业
                if (spe) {
                    $('#specount').html(spe.length);
                }

                var spehasclass = json_data.data.spehasclass;//已设置的迎新班级
                if (spehasclass) {
                    $('#spehasclasscount').html(spehasclass.length);
                    var campus=new Array();
                    for(i=0;i<spehasclass.length;i++){
                        var item = spehasclass[i];
                        var Campus_Name=item.Campus_Name;
                        if(campus.length>=0){
                            var has=false;
                            for(var j=0;j<campus.length;j++){
                                if(campus[j]==Campus_Name){
                                    has=true;
                                }
                            }
                            if(!has){
                                campus[campus.length]=Campus_Name;
                            }
                        }
                    }
                    $('#campuscount').html(campus.length);
                }
                var spenohasclass = json_data.data.spenohasclass;//未设置班级的迎新专业
                if (spenohasclass) {
                    if(spenohasclass.length==0){
                        $('#spenohasclasscount').parent("a").hide();
                    }else{
                        $('#spenohasclasscount').html(spenohasclass.length);
                        $('#spenohasclasscount').parent("a").show();
                    }
                }

                var classhasstudent = json_data.data.classhasstudent;//班级中有学生的班级数据
                if (classhasstudent) {
                    var studentcount=0;
                    for(i=0;i<classhasstudent.length;i++){
                        var item = classhasstudent[i];
                        studentcount=studentcount+parseInt(item.studentcount);
                    }
                    $('#classhasstudentcount').html(studentcount);
                }
                var spenohasclassstudent = json_data.data.spenohasclassstudent;//存在未分班学生的专业数据
                if (spenohasclassstudent) {
                    var studentcount=0;
                    for(i=0;i<spenohasclassstudent.length;i++){
                        var item = spenohasclassstudent[i];
                        studentcount=studentcount+parseInt(item.studentcount);
                    }
                    if(studentcount==0){
                        $('#spenohasclassstudentcount').parent("a").hide();
                    }else{
                        $('#spenohasclassstudentcount').parent("a").show();
                        $('#spenohasclassstudentcount').html(studentcount);
                    }
                }
/*                var classhasstudent_buterror = json_data.data.classhasstudent_buterror;//分错专业的学生数据
                if (classhasstudent_buterror) {
                    $('#classhasstudent_buterrorcount').html(classhasstudent_buterror.length);
                }*/

                var hasbed = json_data.data.hasbed;//已预分床位数据
                if (hasbed) {
                    var bedcount=0;
                    for(i=0;i<hasbed.length;i++){
                        var item = hasbed[i];
                        bedcount=bedcount+parseInt(item.bedcount);
                    }
                    $('#hasbedcount').html(bedcount);
                }

                var nohasbedclass = json_data.data.nohasbedclass;//未预分床位人数数据
                if (nohasbedclass) {
                    var requirebedcount=0;
                    for(i=0;i<nohasbedclass.length;i++){
                        var item = nohasbedclass[i];
                        requirebedcount=requirebedcount+parseInt(item.requirebedcount);
                    }
                    $('#nohasbedcount').html(requirebedcount);
                }

                var hasbed_buterror = json_data.data.hasbed_buterror;//已预分床位，但年或校区错误的数据
                if (hasbed_buterror) {
                    var bedcount=0;
                    for(i=0;i<hasbed_buterror.length;i++){
                        var item = hasbed_buterror[i];
                        bedcount=bedcount+parseInt(item.bedcount);
                    }
                    $('#hasbed_buterrorcount').html(bedcount);
                }

                var hascounseller = json_data.data.hascounseller;//已设置班主任数据
                if (hascounseller) {
                    $('#hascounsellercount').html(hascounseller.length);
                }
                var nohascounseller = json_data.data.nohascounseller;//未设置班主任数据
                if (nohascounseller) {
                    if(nohascounseller.length==0){
                        $('#nohascounsellercount').parent("a").hide();
                    }else{
                        $('#nohascounsellercount').parent("a").show();
                        $('#nohascounsellercount').html(nohascounseller.length);
                    }
                }

                var hascollageaffair = json_data.data.hascollageaffair;//已设置现场迎新事务的事务数据
                if (hascollageaffair) {
                    var count=0;
                    var tmp=new Array();
                    for(i=0;i<hascollageaffair.length;i++){
                        var item = hascollageaffair[i];
                        var find=false;
                        for(var j=0;j<tmp.length;j++){
                            if(tmp[j]==item.pk_affair_no){
                                find=true;
                                break;
                            }
                        }
                        if(find==false){
                            tmp[count]=item.pk_affair_no;
                            count=count+1;
                        }
                    }
                    $('#hascollageaffairacount').html(tmp.length);
                }

                var nohascollageaffair = json_data.data.nohascollageaffair;//未设置现场迎新事务的事务数据
                if (nohascollageaffair) {
                    var count=0;
                    var tmp=new Array();
                    for(i=0;i<nohascollageaffair.length;i++){
                        var item = nohascollageaffair[i];
                        var find=false;
                        for(var j=0;j<tmp.length;j++){
                            if(tmp[j]==item.pk_affair_no){
                                find=true;
                                break;
                            }
                        }
                        if(find==false){
                            tmp[count]=item.pk_affair_no;
                            count=count+1;
                        }
                    }
                    $('#nohascollageaffairacount').html(tmp.length);
                }

                //console.log(json_data.data.collegefinancial);
                var collegefinancial = json_data.data.collegefinancial;//专业财务交费项目数据
                if (collegefinancial) {
                    var count=0;
                    var tmp=new Array();
                    for(i=0;i<collegefinancial.length;i++){
                        var item = collegefinancial[i];
                        var find=false;
                        for(var j=0;j<tmp.length;j++){
                            if(tmp[j]==item.Fee_Code){
                                find=true;
                                break;
                            }
                        }
                        if(find==false  && $.trim(item.Fee_Code).length>0){
                            tmp[count]=item.Fee_Code;
                            count=count+1;
                        }
                    }
                    $('#collegefinancialcount').html(tmp.length);
                }



            } else {
                alert(json_data.message);
            }
        },
        error: function (data) {
            alert("错误");
        }
    });
}

function detailclick(cs){
    var pk_batch_no=$('#freshbatch').children('option:selected').val();
    var pk_collage_no=$('#collageList').children('option:selected').val();
    if (pk_collage_no === 'all') {
        pk_collage_no = '';
    }

    parent.layer.open({  type: 2,  title: '详细信息',  shadeClose: true,  shade: 0.8,  area: ['98%', '98%'],  content: '/view/czzt_detail.aspx?cs='+cs+'&pk_batch_no='+pk_batch_no+'&pk_collage_no='+pk_collage_no,btn:'关闭'})

}



