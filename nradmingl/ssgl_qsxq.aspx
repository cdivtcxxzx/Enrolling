<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ssgl_qsxq.aspx.cs" Inherits="nradmingl_qsxq" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>寝室详情</title>
    <!--引用ＬＡＹＵＩ前端必须ＣＳＳ-->

        <link rel="stylesheet" href="plugins/layui/css/layui.css" media="all" />
		<link rel="stylesheet" href="plugins/global.css" media="all">
         <!--引用ＬＡＹＵＩ前端字体图标ＣＳＳ-->
		<link rel="stylesheet" href="plugins/font-awesome/css/font-awesome.min.css">
         <!--引用ＬＡＹＵＩ前端表格ＣＳＳ,使用表格时才引用-->
		<link rel="stylesheet" href="plugins/table.css" />
    
     <!--引用ＬＡＹＵＩ前端必须ＣＳＳ OVER-->
    

</head>
<body>
    <style>
        .layui-tab-brief > .layui-tab-more li.layui-this::after, .layui-tab-brief > .layui-tab-title .layui-this::after {
            border-radius: 0;
            border-bottom: 3px solid #196BAB;
            border-top: 3px;
        }
        .layui-tab-brief > .layui-tab-title .layui-this {
            color: #023d6b;
        }

        .layui-tab-title .layui-this {
            background: #e2e2e2;
            border-top: 3px;
            border-left: 1px;
            border-right: 1px;
        }

        .layui-tab-title .layui-this {
            background: #e2e2e2;
            border-top: 3px;
            border-left: 1px;
            border-right: 1px;
            border-bottom: 3px;
        }

        .layui-tab-title li:hover {
            background: #e2e2e2;
            border-bottom: 3px solid #196BAB;
            border-top: 3px;
        }

        element.style {
            height: 100%;
        }

        .layui-elem-field {
            margin-bottom: 5px;
            padding: 0;
            border: 1px solid #e2e2e2;
        }

        .qszp img {
            height: 250px;
        }

    </style>    
    <form id="form1" class="layui-form layui-form-pane" runat="server">
        <div class="admin-main">
            <fieldset class="layui-elem-field layui-field-title" style="margin-top: 5px;">
                <legend style="font-size: 14px;">寝室基础信息及预分配信息</legend>

                <asp:GridView  CssClass="site-table table-hover"  ID="GridView2" runat="server" AutoGenerateColumns="true" 
                    DataSourceID="SqlDataSource1">
                    
                </asp:GridView>





                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
                    SelectCommand="select row_number() over (order by  已分配院系)  AS 序号,* from (SELECT DISTINCT                       TOP (10) Base_Campus.Campus_Name AS 校区, Fresh_Dorm.Name AS 公寓楼名称, Fresh_Room.Floor AS 楼层, Fresh_Room.Room_NO AS 房间编号,                       Fresh_Room_Type.Type_Name AS 房间类型, Fresh_Room.Gender AS 性别, Base_College.Name AS 已分配院系, Fresh_Class.Name AS 已分配班级 FROM         Fresh_Class RIGHT OUTER JOIN                      Fresh_Bed_Class_Log ON Fresh_Class.PK_Class_NO = Fresh_Bed_Class_Log.FK_Class_NO RIGHT OUTER JOIN                      Base_College RIGHT OUTER JOIN                      Fresh_Bed ON Base_College.College_NO = Fresh_Bed.College_NO ON Fresh_Bed_Class_Log.FK_Bed_NO = Fresh_Bed.PK_Bed_NO LEFT OUTER JOIN                      Base_Campus RIGHT OUTER JOIN                      Fresh_Dorm RIGHT OUTER JOIN                      Fresh_Room_Type RIGHT OUTER JOIN                      Fresh_Room ON Fresh_Room_Type.PK_Room_Type = Fresh_Room.FK_Room_Type ON Fresh_Dorm.PK_Dorm_NO = Fresh_Room.FK_Dorm_NO ON                       Base_Campus.Campus_NO = Fresh_Dorm.Campus_NO ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO WHERE     (Fresh_Room.Room_NO = @Room_NO)) t order by  已分配院系">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="Room_NO" QueryStringField="roomno" 
                            Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>





              <%--  <table class="site-table table-hover" cellspacing="0" rules="all" border="1" id="GridView1" style="border-collapse: collapse;">
                    <tbody>
                        <tr align="center">
                            <th scope="col">校区名称</th>
                            <th class="hidden-xs" scope="col">楼栋名称</th>
                            <th scope="col">楼层</th>
                            <th class="hidden-xs" scope="col">房间编号</th>
                            <th scope="col">房间类型</th>
                            <th class="hidden-xs">房间人数</th>
                            <th class="hidden-xs" scope="col">性别</th>
                            <th>空余床位数</th>
                        </tr>
                        <tr>
                            <td>天府校区</td>
                            <td class="hidden-xs">1栋</td>
                            <td>1楼</td>
                            <td>1101</td>
                            <td class="hidden-xs">普通六人间</td>
                            <td class="hidden-xs">6</td>
                            <td class="hidden-xs">女</td>
                            <td>2</td>
                        </tr>
                    </tbody>
                </table>--%>
            </fieldset>
            <style>
            .layui-form input[type=checkbox]
            {
                display:block;
            }
            </style>
            <div class="layui-tab layui-tab-brief" lay-filter="docDemoTabBrief">
                <ul class="layui-tab-title">
                    <li class="layui-this">床位及入住学生信息</li>
                    <li>寝室缩影</li>
                </ul>
                <div class="layui-tab-content" style="height: 100%;">
                    <div class="layui-tab-item layui-show">
                    <asp:HiddenField ID="hdfWPBH" runat="server" /><asp:HiddenField ID="ssh" runat="server" />
                     <asp:GridView  CssClass="site-table table-hover"  ID="GridView3" runat="server" AutoGenerateColumns="false" 
                    DataSourceID="SqlDataSource2">
                   <Columns>
    <asp:TemplateField>
                <HeaderTemplate>
                      <input type="checkbox"  id="BoxIdAll"  name="BoxIdAll" onclick="onclicksel();" />  
                </HeaderTemplate>
                <ItemTemplate>
                     <input id="BoxId" name="BoxId"  class="icheck" value='<%#(Convert.ToString(Eval("房间id")))%>' type="checkbox" /> 
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Width="2%"  HorizontalAlign="Center" />
            </asp:TemplateField>

             <asp:BoundField DataField="房间编号" HeaderText="房间编号" SortExpression="房间编号"/>
              <asp:BoundField DataField="床位编号" HeaderText="床位编号" SortExpression="床位编号"/>
               <asp:BoundField DataField="床位位置描述" HeaderText="床位位置描述" SortExpression="床位位置描述"/>
                <asp:BoundField DataField="已分配院系" HeaderText="已分配院系" SortExpression="已分配院系"/>
                 <asp:BoundField DataField="已分配班级" HeaderText="已分配班级" SortExpression="已分配班级"/>
                  <asp:BoundField DataField="学生学号" HeaderText="学生学号" SortExpression="学生学号"/>
                   <asp:BoundField DataField="学生姓名" HeaderText="学生姓名" SortExpression="学生姓名"/>
                    <asp:BoundField DataField="联系电话" HeaderText="联系电话" SortExpression="联系电话"/>

                    <asp:TemplateField HeaderText="" >
                    <HeaderTemplate>

                <a onclick="return batchAudit(this.id);" class="layui-btn layui-btn-mini"  id="btnDelete" href="javascript:__doPostBack('btnDelete','')"><span id="plcz" runat="server">批量调整</span></a>
                </HeaderTemplate>
                <ItemTemplate>

             <a href="javascript: " onclick="parent.layer.open({  type: 2,  title: '寝室调整－【当前：<%# Eval("房间编号").ToString() %>第<%# Eval("床位编号").ToString() %>床位】',  shadeClose: true,  shade: 0.8,  area: ['80%', '80%'],  content: 'ssgl_qstz.aspx?roomno=<%# Eval("房间编号").ToString() %>&cwid=<%# Eval("房间id").ToString() %>',cancel: function(index, layero){ location.reload(true)  }});"  txttop="txttop" class="layui-btn layui-btn-mini"  title="将该寝室床位调整到其它班级">调整寝室</a>  
             
            </ItemTemplate>
                
                </asp:TemplateField>
             </Columns>
                </asp:GridView>





                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
                    SelectCommand="SELECT     TOP (20) Fresh_Room.Room_NO AS 房间编号, Fresh_Bed.Bed_NO AS 床位编号,Fresh_Bed.Bed_Name AS 床位位置描述,  Base_College.Name AS 已分配院系, 
                      Fresh_Class.Name AS 已分配班级, Fresh_Bed_Log.FK_SNO AS 学生学号, Base_STU.Name AS 学生姓名, Base_STU.Phone AS 联系电话,Fresh_Bed.PK_Bed_NO AS 房间id
FROM         Fresh_Bed LEFT OUTER JOIN
                      Fresh_Bed_Log LEFT OUTER JOIN
                      Base_STU ON Fresh_Bed_Log.FK_SNO = Base_STU.PK_SNO ON Fresh_Bed.PK_Bed_NO = Fresh_Bed_Log.FK_Bed_NO LEFT OUTER JOIN
                      Base_College ON Fresh_Bed.College_NO = Base_College.College_NO LEFT OUTER JOIN
                      Fresh_Class RIGHT OUTER JOIN
                      Fresh_Bed_Class_Log ON Fresh_Class.PK_Class_NO = Fresh_Bed_Class_Log.FK_Class_NO ON 
                      Fresh_Bed.PK_Bed_NO = Fresh_Bed_Class_Log.FK_Bed_NO LEFT OUTER JOIN
                      Fresh_Room ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO
WHERE     (Fresh_Room.Room_NO = @Room_NO)
ORDER BY 床位编号">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="Room_NO" QueryStringField="roomno" 
                            Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                     
                    </div>
                    <div class="layui-tab-item">

                        <div class="layui-input-block" style="margin-left: 10px!important">
                            <div class="layui-form-mid layui-word-aux-ts qszp" style="margin-left: 10px; text-align: center; float: none!important">
                                <img src="../images/xstp/qs.jpg" runat="server" />
                            </div>
                        </div>


                    </div>

                </div>
            </div>
            <div>
                <div style="text-align: center">
                    <span style="padding: 8px;">
                       <%-- <a href="" class="layui-btn layui-btn-small" id="">
                            <i class="layui-icon">&#x1006;</i> 关闭
                        </a>--%>
                    </span>
                </div>
            </div>
        </div>
    </form>
        <script type="text/javascript">

            function onclicksel() {
                var chkobj = document.getElementById("BoxIdAll");
                if (chkobj.checked == true) {
                    selAll();
                }
                else {
                    removeAll();
                }
            }
            function selAll() {
                var selobj = document.getElementsByName("BoxId");
                for (var i = 0; i < selobj.length; i++) {
                    if (!selobj[i].disabled) {
                        selobj[i].checked = true;
                    }
                }
            }

            function removeAll() {
                var selobj = document.getElementsByName("BoxId");
                for (var i = 0; i < selobj.length; i++) {
                    selobj[i].checked = false;
                }
            }
            //批量操作

            function batchAudit(id) {
                var AuditVal = "";
                //var roomno = document.getElementsByName("roomno")[0].value;
                var bid = document.getElementsByName("BoxId");
                for (var i = 0; i < bid.length; i++) {
                    if (bid[i].checked == true) {
                        AuditVal = AuditVal + bid[i].value + ",";
                    }
                }
                if (AuditVal.length <= 0) {
                    parent.layer.msg("请先选择一条记录,在记录前打勾!");

                    return false;
                }
                else {
                    if (id == "btnDelete") {
                        parent.layer.open({ type: 2, title: '寝室调整－【多床位同时调整】' + AuditVal, shadeClose: true, shade: 0.8, area: ['80%', '80%'], content: 'ssgl_qstz.aspx?cwid=' + AuditVal, cancel: function (index, layero) { location.reload(true) } });
                        return true;
                        document.getElementById("hdfWPBH").value = AuditVal;
                        return true;
                        //                             layer.open({ content: '您确认要批量删除这' + String(AuditVal.length / 4) + '条记录吗？'
                        //                                      , btn: ['确认', '取消']
                        //                                      , yes: function (index, layero) {
                        //                                          document.getElementById("hdfWPBH").value = AuditVal;
                        //                                          //此处写传给删除页面的参数
                        //                                          return true;
                        //                                          //使用AJAX回调删除
                        //                                          layer.close(index);

                        //                                      }, btn2: function (index, layero) {
                        //                                          return false;
                        //                                      }
                        //                                      , cancel: function () {
                        //                                          return false;
                        //                                      }
                        //                             });
                        return false;
                    }
                }
            }  
    </script>
    <script type="text/javascript" src="plugins/layui/layui.js"></script>
    <script>
layui.use('element', function(){
  var $ = layui.jquery
  ,element = layui.element(); //Tab的切换功能，切换事件监听等，需要依赖element模块
  
  //触发事件
  var active = {
    tabAdd: function(){
      //新增一个Tab项
      element.tabAdd('demo', {
        title: '新选项'+ (Math.random()*1000|0) //用于演示
        ,content: '内容'+ (Math.random()*1000|0)
        ,id: new Date().getTime() //实际使用一般是规定好的id，这里以时间戳模拟下
      })
    }
    ,tabDelete: function(othis){
      //删除指定Tab项
      element.tabDelete('demo', '44'); //删除：“商品管理”
      
      
      othis.addClass('layui-btn-disabled');
    }
    ,tabChange: function(){
      //切换到指定Tab项
      element.tabChange('demo', '22'); //切换到：用户管理
    }
  };
  
  $('.site-demo-active').on('click', function(){
    var othis = $(this), type = othis.data('type');
    active[type] ? active[type].call(this, othis) : '';
  });
  
  //Hash地址的定位
  var layid = location.hash.replace(/^#test=/, '');
  element.tabChange('test', layid);
  
  element.on('tab(test)', function(elem){
    location.hash = 'test='+ $(this).attr('lay-id');
});


  
});
</script>

   


</body>

</html>
