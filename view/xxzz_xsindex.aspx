<%@ Page Language="C#" AutoEventWireup="true" CodeFile="xxzz_xsindex.aspx.cs" Inherits="view_xxzz_xsindex" %>



<!DOCTYPE html>
<html lang="zh-cn">
<head id="Head1">
    <title>学生自助首页
    </title>
    <meta charset="UTF-8" content="编码" />
    <meta name="renderer" content="webkit" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="format-detection" content="telephone=no" />

    <!--引用ＬＡＹＵＩ前端必须ＣＳＳ-->
    <link rel="stylesheet" href="../nradmingl/plugins/layui/css/layui.css" media="all" />
    <link rel="stylesheet" href="../nradmingl/plugins/global.css" media="all" />
    <!--引用ＬＡＹＵＩ前端字体图标ＣＳＳ-->
    <link rel="stylesheet" href="../nradmingl/plugins/font-awesome/css/font-awesome.min.css" />
    <!--引用ＬＡＹＵＩ前端表格ＣＳＳ,使用表格时才引用-->
    <link rel="stylesheet" href="../nradmingl/plugins/table.css" />

    <!--引用ＬＡＹＵＩ前端必须ＣＳＳ OVER-->


</head>
<body>
    <!--展示响应式CSS块-->
    <style>
        .responsive-utilities-test .col-xs-6 {
            margin-bottom: 10px;
        }

        .responsive-utilities-test .col-xs-6 {
            margin-bottom: 10px;
        }

        .col-xs-6 {
            width: 50%;
            float: left;
        }

        .visible-on .col-xs-6 .hidden-xs, .visible-on .col-xs-6 .hidden-sm, .visible-on .col-xs-6 .hidden-md, .visible-on .col-xs-6 .hidden-lg, .hidden-on .col-xs-6 .hidden-xs, .hidden-on .col-xs-6 .hidden-sm, .hidden-on .col-xs-6 .hidden-md, .hidden-on .col-xs-6 .hidden-lg {
            color: #999;
            border: 1px solid #ddd;
        }

        .visible-on .col-xs-6 .visible-xs-block, .visible-on .col-xs-6 .visible-sm-block, .visible-on .col-xs-6 .visible-md-block, .visible-on .col-xs-6 .visible-lg-block, .hidden-on .col-xs-6 .visible-xs-block, .hidden-on .col-xs-6 .visible-sm-block, .hidden-on .col-xs-6 .visible-md-block, .hidden-on .col-xs-6 .visible-lg-block {
            color: #468847;
            background-color: #dff0d8;
            border: 1px solid #d6e9c6;
        }

        .responsive-utilities-test span {
            display: block;
            padding: 15px 10px;
            font-size: 14px;
            font-weight: 700;
            line-height: 1.1;
            text-align: center;
            border-radius: 4px;
        }
    </style>
    <!--展示响应式CSS块over-->
    <!--页面开始全范围框架-->
    <div class="admin-main">
        <!--顶部提示及导航-->
        <blockquote class="layui-elem-quote" style="border-left: 0px solid #005CA3;border-radius: 5px 5px 5px 5px;">

           <span class="hidden-xs">
               <i class="layui-icon">&#xe613;</i>
               <asp:Label ID="xsxx_bj" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
           </span> 
            <i class="layui-icon">&#xe612;</i>
            <asp:Label ID="xsxx_bzr" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp; 
            <span class="hidden-xs"><i class="layui-icon">&#xe63a;</i>
                <asp:Label ID="xsxx_bzrdh" runat="server" Text=""></asp:Label></span>
            <span style="float: right">
                 <a id="xsxxurl" runat="server" href="xsjbxx.aspx" class="layui-btn layui-btn-small">
                    <i class="layui-icon">&#xe63c;</i>查看详细
                </a>
            </span>

        </blockquote>
        <!--顶部提示及导航OVER-->

        <!--标签框架-->
        <style>
            .xszp img {
                width: 100%;
                max-width: 220px;
                max-height: 360px;
            }

            .xsxx1 {
                float: left;
                margin-right: 10px;
                width: 32%;
            }

            .xsxx2 {
                float: left;
                margin-right: 10px;
                width: 32%;
            }

            .xsxx3 {
                float: left;
                width: 32%;
            }

            .xsxx1 {
                float: none;
                margin-right: 10px;
                width: 100%;
            }

            .xsxx2 {
                width: 96%;
            }

            .xsxx3 {
                width: 96%;
            }

            @media (max-width: 550px) {
                .xsxx2 {
                    width: 96%;
                    float: none;
                }

                .xsxx3 {
                    width: 96%;
                    float: none;
                }
            }
        </style>
        <div style="margin-top: 15px;">



            <form class="layui-form layui-form-pane" action="" runat="server">
                <asp:HiddenField ID="pk_sno" Value="" runat="server" />

               <div class="xsxx2">
                  <br />

                   <table style="width:100%;">

                         <tr><td style="vertical-align:bottom;width:60px;padding-bottom:5px;"><img  id="zttp1" runat="server" style="width:50px" src="../images/xszt/1-1.png" /></td><td style="vertical-align:top;"><div class="layui-form-item" pane="" style="min-height: 56px"> 
                    
                    <label class="layui-form-label" style="width: 100px;"><a class="layui-btn" style="width:100%;" href="javascript:void(0)" id="xscz_bdzc" runat="server" >报到注册</a></label>
                    <div class="layui-input-block" style="margin-left: 100px;"><div class="layui-form-mid layui-word-aux-ts" style="margin-left: 40px; padding: 18px 0;"><span id="xszt_bdzc" runat="server"><font color="green"><b>已完成</b></font></span> <span  id="xsztxq_bdzc" runat="server"></span></div></div></div></td></tr>

                       
                         <tr><td style="vertical-align:bottom;width:60px;padding-bottom:5px;"><img  id="zttp2" runat="server" style="width:50px" src="../images/xszt/2-2.png" /></td><td style="vertical-align:top;"><div class="layui-form-item" pane="" style="min-height: 56px"> 
                    
                    <label class="layui-form-label" style="width: 100px;"><a class="layui-btn" style="width:100%;" href="javascript:void(0)" id="xscz_bdxz" runat="server" >报到须知</a></label>
                    <div class="layui-input-block" style="margin-left: 100px;"><div class="layui-form-mid layui-word-aux-ts" style="margin-left: 40px; padding: 18px 0;"><span id="xszt_bdxz" runat="server"><font color="green"><b>已阅读</b></font></span> <span  id="xsztxq_bdxz" runat="server"></span></div></div></div></td></tr>

                   

                       <tr><td style="vertical-align: top;width:60px;padding-bottom:5px;"><img id="zttp3" runat="server" style="width:50px"  src="../images/xszt/3.png" /></td><td style="vertical-align:top;"><div class="layui-form-item" pane="" style="min-height: 56px"> 
                    
                    <label class="layui-form-label" style="width: 100px;"><a class="layui-btn" style="width:100%;" href="xswsjf.aspx"  id="xscz_wsjf" runat="server" >网上缴费</a></label>
                    <div class="layui-input-block" style="margin-left: 100px;"><div class="layui-form-mid layui-word-aux-ts" style="margin-left: 40px; padding: 18px 0;"><span  id="xszt_wsjf" runat="server"><font color="green"><b>已缴费</b></font></span> <span  id="xsztxq_wsjh" runat="server" ></span></div></div></div></td></tr>

                       

                       <tr><td style="vertical-align: top;width:60px;padding-bottom:5px;"><img  id="zttp4" runat="server"  style="width:50px"  src="../images/xszt/4-4.png" /></td><td style="vertical-align:top;"><div class="layui-form-item" pane="" style="min-height: 56px"> 
                    
                    <label class="layui-form-label" style="width: 100px;"><a class="layui-btn" style="width:100%;" href="javascript:void(0)"  id="xscz_xzqs" runat="server" >寝室选择</a></label>
                    <div class="layui-input-block" style="margin-left: 100px;"><div class="layui-form-mid layui-word-aux-ts" style="margin-left: 40px; padding: 18px 0;"><span  id="xszt_czqs" runat="server"><font color="red"><b>未完成</b></font></span> <span  id="xsztxq_xzqs" runat="server"></span></div></div></div></td></tr>


                       <tr><td style="vertical-align: top;width:60px;padding-bottom:5px;"><img  id="zttp5" runat="server"  style="width:50px"  src="../images/xszt/5-5.png" /></td><td style="vertical-align:top;"><div class="layui-form-item" pane="" style="min-height: 56px"> 
                    
                    <label class="layui-form-label" style="width: 100px;"><a class="layui-btn" style="width:100%;" href="javascript:void(0)"  id="xscz_xxws" runat="server">信息完善</a></label>
                    <div class="layui-input-block" style="margin-left: 100px;"><div class="layui-form-mid layui-word-aux-ts" style="margin-left: 40px; padding: 18px 0;"><span id="xszt_xxws" runat="server"><font color="red"><b>未完成</b></font></span> <span  id="xsztxq_xxws" runat="server"></span></div></div></div></td></tr>

                       <tr><td style="vertical-align: top;width:60px;padding-bottom:5px;"><img  id="zttp6" runat="server"  style="width:50px"  src="../images/xszt/w6-6.png" /></td><td style="vertical-align:top;"><div class="layui-form-item" pane="" style="min-height: 56px"> 
                    
                    <label class="layui-form-label" style="width: 100px;"><a class="layui-btn" style="width:100%;" href="javascript:void(0)"  id="xscz_tzgg" runat="server" >通知</a></label>
                    <div class="layui-input-block" style="margin-left: 100px;"><div class="layui-form-mid layui-word-aux-ts" style="margin-left: 40px; padding: 18px 0;"><span id="xszt_tzgg" runat="server"><font color="red"><b></b></font></span> <span  id="xsztxq_tzgg" runat="server" ></span></div></div></div></td></tr>
                      


                   </table>





                <div class="xsxx3">

                </div>
                <!--
        <div class="layui-form-item" style="text-align:center">
          <button class="layui-btn" onClick="javascript:">返回操作首页</button>
        </div>-->
        </div>



        </form>
    </div>


    <!--标签框架over-->

    </div>
  
        		



</body>
</html>
