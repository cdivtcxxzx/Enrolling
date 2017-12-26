<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ssgl_qstz.aspx.cs" Inherits="nradmingl_ssgl_qstz" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>寝室调整</title>
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
  <div>
          
                             <asp:DropDownList ID="yx"  Font-Size="Medium" runat="server" DataSourceID="SqlDataSource6" DataTextField="yxmc" DataValueField="yxdm" AutoPostBack="True" OnSelectedIndexChanged="yx_SelectedIndexChanged">
                
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnString %>" SelectCommand="SELECT [YXDM], [YXMC] FROM [DM_YUANXI] WHERE (([isjx] = @isjx) AND ([zt] = @zt)) ORDER BY [YXDM]">
                <SelectParameters>
                    <asp:Parameter DefaultValue="true" Name="isjx" Type="Boolean" />
                    <asp:Parameter DefaultValue="true" Name="zt" Type="Boolean" />
                </SelectParameters>
            </asp:SqlDataSource>

                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                            DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="PK_Class_NO" 
                            Font-Size="Medium" 
                            onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
                                    SelectCommand="SELECT     TOP (1000) Fresh_Class.PK_Class_NO ,Fresh_Class.Name FROM         Fresh_Class LEFT OUTER JOIN                      Fresh_SPE ON Fresh_Class.FK_SPE_NO = Fresh_SPE.PK_SPE where Fresh_SPE.FK_College_Code=@yxdm">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="yx" Name="yxdm" PropertyName="SelectedValue" />
                            </SelectParameters>
                        </asp:SqlDataSource>

                        
                        
                        
                        
                        
                            
                        
                        
                        
                        
                        <asp:Button ID="Button1"  CssClass="layui-btn"  runat="server" Text="确认调整" 
                            onclick="Button1_Click1" /> <asp:Button ID="Button2"  CssClass="layui-btn"  runat="server" Text="清空班级" 
                            onclick="Button1_Click2" /><br /><br />
                        <asp:Label ID="g_ts" runat="server" Text="<font color=red>请选择院和班级后，点击确认调整</font>" Font-Size="Larger"></asp:Label>
                   
                       
                          
        
  <div>   
                     
             

    <br />清空班级：将寝室分配到班级状态清空，作为二级学院的预留寝室床位

            <style>

            .layui-form input[type="checkbox"], .layui-form input[type="radio"], .layui-form select {
    display: inline;
}
 select
        {
            display:inline-block;
            height: 37px;
        }
            </style> 
            <div class="layui-tab layui-tab-brief" lay-filter="docDemoTabBrief">
                <ul class="layui-tab-title">
                    <li class="layui-this">需调整床位相关信息</li>
                    
                </ul>
                <div class="layui-tab-content" style="height: 100%;">
                    <div class="layui-tab-item layui-show">
                    <asp:HiddenField ID="hdfWPBH" runat="server" />
                     <asp:GridView  CssClass="site-table table-hover"  ID="GridView3" runat="server" AutoGenerateColumns="false" 
                    DataSourceID="SqlDataSource2">
                   <Columns>
 

             <asp:BoundField DataField="房间编号" HeaderText="房间编号" SortExpression="房间编号"/>
              <asp:BoundField DataField="床位编号" HeaderText="床位编号" SortExpression="床位编号"/>
               <asp:BoundField DataField="床位位置描述" HeaderText="床位位置描述" SortExpression="床位位置描述"/>
                <asp:BoundField DataField="已分配院系" HeaderText="已分配院系" SortExpression="已分配院系"/>
                 <asp:BoundField DataField="已分配班级" HeaderText="已分配班级" SortExpression="已分配班级"/>
                  <asp:BoundField DataField="学生学号" HeaderText="学生学号" SortExpression="学生学号"/>
                   <asp:BoundField DataField="学生姓名" HeaderText="学生姓名" SortExpression="学生姓名"/>
                    <asp:BoundField DataField="联系电话" HeaderText="联系电话" SortExpression="联系电话"/>


             </Columns>
                </asp:GridView>





                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
                    SelectCommand="">
                    
                </asp:SqlDataSource>
                     
                    </div>
                    <div class="layui-tab-item">

                        <div class="layui-input-block" style="margin-left: 10px!important">
                            <div class="layui-form-mid layui-word-aux-ts qszp" style="margin-left: 10px; text-align: center; float: none!important">
                                <img id="Img1" src="../images/xstp/qs.jpg" runat="server" />
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
                        parent.layer.open({ type: 2, title: '寝室调整－' + AuditVal, shadeClose: true, shade: 0.8, area: ['80%', '80%'], content: 'ssgl_qstz.aspx?roomno=' + AuditVal });
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
        layui.use('element', function () {
            var $ = layui.jquery
  , element = layui.element(); //Tab的切换功能，切换事件监听等，需要依赖element模块

            //触发事件
            var active = {
                tabAdd: function () {
                    //新增一个Tab项
                    element.tabAdd('demo', {
                        title: '新选项' + (Math.random() * 1000 | 0) //用于演示
        , content: '内容' + (Math.random() * 1000 | 0)
        , id: new Date().getTime() //实际使用一般是规定好的id，这里以时间戳模拟下
                    })
                }
    , tabDelete: function (othis) {
        //删除指定Tab项
        element.tabDelete('demo', '44'); //删除：“商品管理”


        othis.addClass('layui-btn-disabled');
    }
    , tabChange: function () {
        //切换到指定Tab项
        element.tabChange('demo', '22'); //切换到：用户管理
    }
            };

            $('.site-demo-active').on('click', function () {
                var othis = $(this), type = othis.data('type');
                active[type] ? active[type].call(this, othis) : '';
            });

            //Hash地址的定位
            var layid = location.hash.replace(/^#test=/, '');
            element.tabChange('test', layid);

            element.on('tab(test)', function (elem) {
                location.hash = 'test=' + $(this).attr('lay-id');
            });



        });
</script>

   


</body>

</html>
