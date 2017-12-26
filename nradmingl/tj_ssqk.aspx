<%@ Page Language="C#" AutoEventWireup="true"  EnableEventValidation = "false"  CodeFile="tj_ssqk.aspx.cs" Inherits="nradmingl_tj_ssqk" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>寝室使用情况</title>
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
        .layui-form-select dl dd.layui-this {
            background-color: #196BAB;
            color: #fff;
        }
        select
        {
            display:inline-block;
            height: 37px;
        }

    </style>
    <form id="form1"  runat="server">
    <div class="admin-main">
      <blockquote class="layui-elem-quote">&nbsp;<span class=" hidden-xs">
          <i class="layui-icon">&#xe602;</i>寝室准备及使用情况统计<i class="layui-icon">&#xe602;</i>按院系班级</span>
           <span style="float:right">

            <!--调用C#原生按钮设置样式举例OVER-->
 <%--               <a href="#" class="layui-btn layui-btn-small hidden-xs">
					<i class="layui-icon">&#xe630;</i> 一卡通更新
				</a>
             --%><a href="tj_ssqk.aspx" class="layui-btn layui-btn-small">
					<i class="layui-icon">&#x1002;</i> 刷新
				</a>

               
                <asp:LinkButton CssClass="layui-btn layui-btn-small" name="exportexcel1" onclick="exportexcel"  txttop="txttop" ToolTip="数据导出" ID="LinkButton13" runat="server"    Text='' ><i class="layui-icon">&#xe61e;</i>导出<span class=" hidden-xs">统计数据</span></asp:LinkButton>
                <asp:LinkButton CssClass="layui-btn layui-btn-small" name="exportexcel2" onclick="exportexcel2"  txttop="txttop" ToolTip="导出寝室详情" ID="LinkButton1" runat="server"    Text='' ><i class="layui-icon">&#xe61e;</i>导出<span class=" hidden-xs">寝室详情</span></asp:LinkButton>

		  </span>       
      </blockquote>

           
        
        <div>
            <div class="layui-form-item">
              
                
                    统计筛选：
                      <asp:DropDownList ID="yx"  Font-Size="Medium" runat="server" DataSourceID="SqlDataSource6" DataTextField="yxmc" DataValueField="yxdm" AutoPostBack="True" OnSelectedIndexChanged="yx_SelectedIndexChanged">
                <asp:ListItem Selected="True">全部院系</asp:ListItem>
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnString %>" SelectCommand="select '0' yxdm,'全部院系' yxmc union(SELECT [YXDM], [YXMC] FROM [DM_YUANXI] WHERE (([isjx] = @isjx) AND ([zt] = @zt))) ORDER BY [YXDM]">
                <SelectParameters>
                    <asp:Parameter DefaultValue="true" Name="isjx" Type="Boolean" />
                    <asp:Parameter DefaultValue="true" Name="zt" Type="Boolean" />
                </SelectParameters>
            </asp:SqlDataSource>

                     
                      <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
                          Text="包含3+2学生" />

                     
                    <asp:DropDownList ID="bj" runat="server" DataSourceID="SqlDataSource5" 
                        DataTextField="Name" DataValueField="PK_Class_NO" AutoPostBack="True" 
                        onselectedindexchanged="bj_SelectedIndexChanged" Font-Size="Medium" 
                          Visible="False">
                        <asp:ListItem Selected="True" Value=" ">全部班级</asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
                        SelectCommand="select ' ' PK_Class_NO,'全部班级' Name union ( SELECT DISTINCT Fresh_Class.PK_Class_NO, Fresh_Class.Name FROM         Fresh_Class LEFT OUTER JOIN                      Fresh_SPE ON Fresh_Class.FK_SPE_NO = Fresh_SPE.PK_SPE  where Fresh_SPE.FK_College_Code=@yxdm)  ORDER BY [PK_Class_NO]">
                     <SelectParameters>
                            <asp:ControlParameter ControlID="yx" Name="yxdm" 
                                PropertyName="SelectedValue" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="g_ts" runat="server" Font-Size="Larger"></asp:Label>
                  

            </div>
        </div>    
  <div>   
  
                      
  <asp:HiddenField ID="hdfWPBH" runat="server" />
    <asp:GridView ID="GridView1" CssClass="site-table table-hover" runat="server" AutoGenerateColumns="False" 
                        EmptyDataText="未获取到数据!" 
           >

              <Columns>

               <asp:BoundField DataField="序号" HeaderText="序号" SortExpression="序号"/>
                <asp:BoundField DataField="院系名称" HeaderText="院系名称" SortExpression="院系名称"/>
                 <asp:BoundField DataField="性别" HeaderText="性别" SortExpression="性别"/>
                  <asp:BoundField DataField="录取人数" HeaderText="录取人数" SortExpression="录取人数"/>
                   <asp:BoundField DataField="缴费学生数" HeaderText="缴费学生数" SortExpression="缴费学生数"/>
                    <asp:BoundField DataField="准备床位" HeaderText="准备床位" SortExpression="准备床位"/>
                     <asp:BoundField DataField="已选床位" HeaderText="已选床位" SortExpression="已选床位"/>
                      <asp:TemplateField HeaderText="剩余床位" SortExpression="剩余床位">
        
            <ItemTemplate>
            <%# sycw(Eval("剩余床位").ToString())%>
            </ItemTemplate>

            <ItemStyle  />
            </asp:TemplateField>
  
   <asp:TemplateField HeaderText="床位预警"    SortExpression="床位预警">
        
            <ItemTemplate>
          <%# cwyj(Eval("剩余床位").ToString(), Eval("准备床位").ToString(), Eval("已选床位").ToString(), Eval("录取人数").ToString(), Eval("缴费学生数").ToString())%>
            </ItemTemplate>

            <ItemStyle  />
            </asp:TemplateField>
  



              </Columns>
    </asp:GridView>
<table  class="site-table table-hover" style="display:none" cellspacing="0" rules="all" border="1" style="border-collapse: collapse;">

                    <thead>
<tr><th scope="col">序号</th><th scope="col">院系</th><th scope="col">性别</th><th scope="col">学生数</th><th scope="col">准备床位数</th><th scope="col">已缴费学生数</th><th scope="col">已选寝室学生数</th><th scope="col">剩余床位</th><th scope="col">预警提示</th></tr></thead>
    
    <tbody>
<tr><td rowspan="3">1</td><td rowspan="3">信息工程学院</td><td>男</td><td>50</td><td>45</td><td>40</td><td>40</td><td>5</td><td><font color=blue>床位充足</font></td></tr>
<tr><td>女</td><td>40</td><td>35</td><td>32</td><td>32</td><td>3</td><td><font color=red>准备床位已不足<b>3</b>个</font></td></tr>
<tr><td>合计</td><td>90</td><td>95</td><td>72</td><td>72</td><td>5</td><td><font color=red>准备床位已不足<b>8</b>个</font></td></tr>
       
        </tbody></table> 

                      <div  id="bjlist" runat="server" style="display:none">


        <asp:GridView ID="GridView2"  CssClass="site-table table-hover" AutoGenerateColumns="False" 
                        EmptyDataText="未获取到数据!"   runat="server">

                        
              <Columns>

               <asp:BoundField DataField="序号" HeaderText="序号" SortExpression="序号"  HeaderStyle-Width="70px" HeaderStyle-BackColor="#f2f2f2" ItemStyle-Width="70px" />
                <asp:BoundField DataField="班级名称" HeaderText="班级名称" SortExpression="班级名称" HeaderStyle-Width="15%" HeaderStyle-BackColor="#f2f2f2"  ItemStyle-Width="15%"  />
                 <asp:BoundField DataField="性别" HeaderText="性别" SortExpression="性别"  HeaderStyle-Width="5%" HeaderStyle-BackColor="#f2f2f2" ItemStyle-Width="5%"  />
                  <asp:BoundField DataField="录取人数" HeaderText="录取人数" SortExpression="录取人数"  HeaderStyle-Width="12%" HeaderStyle-BackColor="#f2f2f2"  ItemStyle-Width="12%" />
                   <asp:BoundField DataField="缴费学生数" HeaderText="缴费学生数" SortExpression="缴费学生数" HeaderStyle-Width="12%" HeaderStyle-BackColor="#f2f2f2" ItemStyle-Width="12%"  />
                    <asp:BoundField DataField="准备床位" HeaderText="准备床位" SortExpression="准备床位" HeaderStyle-Width="12%" HeaderStyle-BackColor="#f2f2f2"  ItemStyle-Width="12%" />
                     <asp:BoundField DataField="已选床位" HeaderText="已选床位" SortExpression="已选床位" HeaderStyle-Width="12%" HeaderStyle-BackColor="#f2f2f2" ItemStyle-Width="12%"  />
                      <asp:TemplateField HeaderText="剩余床位" SortExpression="剩余床位" HeaderStyle-Width="12%" HeaderStyle-BackColor="#f2f2f2" ItemStyle-Width="12%"  >
        
            <ItemTemplate>
            <%# sycw(Eval("剩余床位").ToString())%>
            </ItemTemplate>

            <ItemStyle  />
            </asp:TemplateField>
  
   <asp:TemplateField HeaderText="床位预警"    SortExpression="床位预警" HeaderStyle-Width="15%" HeaderStyle-BackColor="#f2f2f2" ItemStyle-Width="15%"  >
        
            <ItemTemplate>
           <%# cwyj(Eval("剩余床位").ToString(), Eval("准备床位").ToString(), Eval("已选床位").ToString(), Eval("录取人数").ToString(), Eval("缴费学生数").ToString())%>
            </ItemTemplate>

            <ItemStyle  />
            </asp:TemplateField>
  



              </Columns>


        </asp:GridView> 

                      </div>


   

      
             








      <!--与后台配合的提示信息隐藏域-->
            <input id="tsxx" type="text" runat="server" value="" style="display:none" />
            <input id="tsbox" type="text" runat="server" value="" style="display:none" />
			<!--与后台配合的提示信息隐藏域OVER-->
            
			<div class="admin-table-page" style="display:none">
				<div id="page" class="page">
				</div>
			</div>
		</div>
     
		<!--引发ＬＡＹＵＩ前端必须ＪＳ-->
    <script type="text/javascript" src="plugins/layui/layui.js"></script>
  
    <!--引发ＬＡＹＵＩ前端必须ＪＳ　ＯＶＥＲ-->
   



	<script>
	    // $('#code').qrcode(window.location.href);
	    //鼠标滑过图片及文字提示时显示titop样式
	    //	    $(document).tooltip({
	    //	        items: "img, [titop], [title]", content: function () {
	    //	            var element = $(this);
	    //	            if (element.is("[titop]")) {	                var text = element.attr("alt");	                return "<img class='map'  src='" + text + "'>";	            }
	    //	            if (element.is("[txttop]")) {return element.attr("title");             }

	    //	        }  });
	    //一般直接写在一个js文件中
	    layui.use(['layer', 'form'], function () {
	        var layer = layui.layer
  , form = layui.form();

	        //layer.msg('Hello World');
	        //layer.open({ type: 2, title: 'layer mobile页', shadeClose: true, shade: 0.8, area: ['380px', '90%'], content: 'http://layer.layui.com/mobile/' });
	    });


	    layui.config({
	        base: 'plugins/layui/modules/'
	    });

	    layui.use(['icheck', 'laypage'], function () {
	        var $ = layui.jquery,
					laypage = layui.laypage;
	        $('input').iCheck({
	            checkboxClass: 'icheckbox_square-blue'
	        });

	        //page
	        laypage({
	            cont: 'page',
	            pages: 10 //总页数
						,
	            groups: 5 //连续显示分页数
						,
	            jump: function (obj, first) {
	                //得到了当前页，用于向服务端请求对应数据
	                var curr = obj.curr;
	                if (!first) {
	                    //layer.msg('第 '+ obj.curr +' 页');
	                }
	            }
	        });

	        $('#search').on('click', function () {
	            parent.layer.alert('你点击了搜索按钮')
	        });


	        $('.site-table tbody tr').on('click', function (event) {
	            var $this = $(this);
	            var $input = $this.children('td').eq(0).find('input');
	            $input.on('ifChecked', function (e) {
	                $this.css('background-color', '#EEEEEE');
	            });
	            $input.on('ifUnchecked', function (e) {
	                $this.removeAttr('style');
	            });
	            $input.iCheck('toggle');
	        }).find('input').each(function () {
	            var $this = $(this);
	            $this.on('ifChecked', function (e) {
	                $this.parents('tr').css('background-color', '#EEEEEE');
	            });
	            $this.on('ifUnchecked', function (e) {
	                $this.parents('tr').removeAttr('style');
	            });
	        });
	        $('#selected-all').on('ifChanged', function (event) {
	            var $input = $('.site-table tbody tr td').find('input');
	            $input.iCheck(event.currentTarget.checked ? 'check' : 'uncheck');
	        });
	    });
		</script>

             <script type="text/javascript">
                 //后台操作提示配合
                 if (document.all("tsxx").value != "") {
                     parent.layer.msg(document.all("tsxx").value);
                     document.all("tsxx").value = "";
                 }
                 //tsxx纯为提示,tsbox 需点关闭或延时1分钟关闭
                 if (document.all("tsbox").value != "") {
                     parent.layer.open({ content: document.all("tsbox").value, title: '提示信息', btn: ['关闭'], time: 60000 });
                     document.all("tsbox").value = "";
                 }
                 //批量操作

                 function batchAudit(id) {
                     var AuditVal = "";
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

<script type="text/javascript" src="http://lib.sinaapp.com/js/jquery/1.10.2/jquery-1.10.2.min.js"></script> 

<script type="text/javascript">

    table = document.getElementById('GridView2');

    var tds = table.getElementsByTagName('tr');
    //alert(tds.length);
    for (var i = tds.length - 1; i >= 0; i--) {
        var td = tds[i];
        if (i == 0) {
            td.id = "fixedMenu";
        }
        if (i == 1) {
            td.id = "cankao";
        }
    }

    $(document).ready(function (e) {
        //预加载固定方法
        adsorption_top();
        /*当窗口大小调整时也执行顶部固定修复*/
        $(window).resize(function () {
            var ie6 = document.all;
            var dv = $('#fixedMenu'), st, tr_kd;
            st = Math.max(document.body.scrollTop || document.documentElement.scrollTop);
            if (st > parseInt(dv.attr('otop'))) {
                if ($(document).width() < 755) { tr_kd = $("#cankao").width(); }
                else { tr_kd = $("#cankao").width() + 1; }
                dv.css({ 'position': 'fixed', top: 0, 'width': '98%' });
            }
        });
    });

    function adsorption_top() {
        var ie6 = document.all;
        var dv = $('#fixedMenu'), st, tr_kd;
        dv.attr('otop', dv.offset().top); //存储原来的距离顶部的距离 
        $(window).scroll(function () {
            st = Math.max(document.body.scrollTop || document.documentElement.scrollTop);
            var isChrome = window.navigator.userAgent.indexOf("Chrome") !== -1;
            //检查GoogleChrome，如果是则宽度+1,修复不对齐问题，否则设置与下面的tr宽度一致
            if (isChrome) { tr_kd = $("#cankao").width() + 1; }
            else { tr_kd = $("#cankao").width(); }
            if (st > parseInt(dv.attr('otop'))) {
                if (ie6) {//IE6不支持fixed属性，所以只能靠设置position为absolute和top实现此效果 
                    dv.css({ position: 'absolute', top: st });
                }
                else if (dv.css('position') != 'fixed'); dv.css({ 'position': 'fixed', top: 0, 'width': '98%' });
            } else if (dv.css('position') != 'static') dv.css({ 'position': 'static' });
        });
    }; 
</script>


    </form>
</body>
</html>
