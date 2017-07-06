<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Defaultczzt.aspx.cs" Inherits="view_Defaultczzt" %>



<!DOCTYPE html>
<html lang="zh-cn">
<head id="Head1">
    <title>迎新服务数据准备情况
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
        <blockquote class="layui-elem-quote">

            <i class="layui-icon">&#xe602;</i>迎新管理&gt;&gt;后台管理首页【迎新系统应用交流群：645341671】
            <span style="float: right">
                 <a href="/" class="layui-btn layui-btn-small">
                     <i class="layui-icon">&#xe603;</i>
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
                width: 48%;
            }

            .xsxx3 {
                width: 48%;
            }

            @media (max-width: 550px) {
                .xsxx2 {
                    width: 100%;
                    float: none;
                }

                .xsxx3 {
                    width: 100%;
                    float: none;
                }
            }

        .layui-form-select dl dd.layui-this {
            background-color: #196BAB;
            color: #fff;
        }
.layui-form select {
    display: inherit;
     height: 37px;
}
       

        </style>
        <div style="margin-top: 15px;">



            <form class="layui-form layui-form-pane" action="">
    <div>
            <div class="layui-inline">
                <label class="layui-form-label">迎新批次</label>
                <div class="layui-input-block">
                    <select name="freshbatch" id="freshbatch">
                    </select>
                </div>
            </div>
           <div class="layui-inline">
                <label class="layui-form-label">学院</label>
                <div class="layui-input-block">
                    <select name="collageList" id="collageList">
                    </select>
                </div>
            </div>
            <div class="layui-inline" style="margin-bottom:0px;">
                <a href="#" onclick="collagechange(null,null);" class="layui-btn layui-btn-small hidden-xs">
			    <i class="layui-icon">&#x1002;</i> 刷新
			    </a>
            </div> 

    </div>

        <div class="xsxx2">
                                <div class="layui-form-item" pane="" style="min-height: 56px">
                        <label class="layui-form-label" style="width: 150px;"><a class="layui-btn">&emsp;新生人数&emsp;</a></label>
                        <div class="layui-input-block" style="margin-left: 150px;">
                            <div class="layui-form-mid layui-word-aux-ts" style="margin-left: 40px; padding: 18px 0;">
                                <span id="xsxx_xh"><b>合计<span id="studentcount" style="color:red;"></span>人  男<span id="mancount" style="color:red;"></span>人   女<span id="womancount" style="color:red;"></span>人   </b></span>
                            </div>
                        </div>
                    </div>

                    <div class="layui-form-item" pane="" style="min-height: 56px">
                        <label class="layui-form-label" style="width: 150px;"><a class="layui-btn">&emsp;专业设置&emsp;</a></label>
                        <div class="layui-input-block" style="margin-left: 150px;">
                            <div class="layui-form-mid layui-word-aux-ts" style="margin-left: 40px; padding: 18px 0;">
                                <span id="xsxx_xh"><b>
<%--                                    <a href="#" onclick="detailclick('get_detail_batch_collage')">合计:<span id="collagecount" style="color:red;" ></span>个 </a>--%>
                                    <a href="#" onclick="detailclick('get_detail_batch_spe')">已导入专业<span id="specount" style="color:red;"></span>个</a>
                                </span></b>
                            </div>
                        </div>
                    </div>

<%--                    <div class="layui-form-item" pane="" style="min-height: 56px">
                        <label class="layui-form-label" style="width: 150px;"><a class="layui-btn" >专业数据</a></label>
                        <div class="layui-input-block" style="margin-left: 150px;">
                            <div class="layui-form-mid layui-word-aux-ts" style="margin-left: 40px; padding: 18px 0;">
                                <span id="xsxx_xh"><a href="#" onclick="detailclick('get_detail_batch_spe')">合计:<span id="specount" style="color:red;"></span>个</a></span>
                            </div>
                        </div>
                    </div>--%>



                    <div class="layui-form-item" pane="" style="min-height: 56px">
                        <label class="layui-form-label" style="width: 150px;"><a class="layui-btn" >&emsp;班级设置&emsp;</a></label>
                        <div class="layui-input-block" style="margin-left: 150px;">
                            <div class="layui-form-mid layui-word-aux-ts" style="margin-left: 40px; padding: 18px 0;">
                                <span id="xsxx_xh">
                                    <b>
<%--                                        校区数量:<span id="campuscount" style="color:red;"></span> --%>
                                        <a href="#" onclick="detailclick('get_batch_spe_hasclass')">已设置班级<span id="spehasclasscount" style="color:red;"></span>个</a>  
                                        <a  href="#" onclick="detailclick('get_batch_spe_nohasclass')">未设置班级专业<span id="spenohasclasscount" style="color:red;"></span>个</a>
                                    </b></span>
                            </div>
                        </div>
                    </div>

                                <div class="layui-form-item" pane="" style="min-height: 56px">
                        <label class="layui-form-label" style="width: 150px;"><a class="layui-btn" >缴费标准设置</a></label>
                        <div class="layui-input-block" style="margin-left: 150px;">
                            <div class="layui-form-mid layui-word-aux-ts" style="margin-left: 40px; padding: 18px 0;">
                                <span id="xsxx_xh"><b>
                                    <a href="#" onclick="detailclick('get_batch_college_financial')">已设置缴费项目<span id="collegefinancialcount"  style="color:red;"></span>项</a>  
                                 </b>
                            </span>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="xsxx3">

                    <div class="layui-form-item" pane="" style="min-height: 56px">
                        <label class="layui-form-label" style="width: 150px;"><a class="layui-btn" style="width: 120px" >新生分班设置</a></label>
                        <div class="layui-input-block" style="margin-left: 150px;">
                            <div class="layui-form-mid layui-word-aux-ts" style="margin-left: 40px; padding: 18px 0;">
                                <span id="xsxx_xh">
                                    <b>
                                    <a href="#" onclick="detailclick('get_batch_class_hasstudent')">已分班<span id="classhasstudentcount" style="color:red;"></span>人</a>  
                                    <a href="#" onclick="detailclick('get_batch_spe_nohasclassstudent')">未分班<span id="spenohasclassstudentcount" style="color:red;"></span>人</a>
<%--                                    <a href="#" onclick="detailclick('get_batch_class_hasstudent_buterror')">错分班人数:<span id="classhasstudent_buterrorcount" style="color:red;"></span></a>  --%>
                                    </b>
                                    </span>
                            </div>
                        </div>
                    </div>
                    <div class="layui-form-item" pane="" style="min-height: 56px">
                        <label class="layui-form-label" style="width: 150px;"><a class="layui-btn" style="width: 120px" >辅导员设置</a></label>
                        <div class="layui-input-block" style="margin-left: 150px;">
                            <div class="layui-form-mid layui-word-aux-ts" style="margin-left: 40px; padding: 18px 0;">
                                <span id="xsxx_xh">
                                    <b>
                                    <a href="#" onclick="detailclick('get_batch_hascounseller')">已设置辅导员班级<span id="hascounsellercount" style="color:red;"></span>个</a>  
                                    <a href="#" onclick="detailclick('get_batch_nohascounseller')">未设置辅导员班级<span id="nohascounsellercount" style="color:red;"></span>个</a>
                                    </b>
                                    </span>
                            </div>
                        </div>
                    </div>

                    <div class="layui-form-item" pane="" style="min-height: 56px">
                        <label class="layui-form-label" style="width: 150px;"><a class="layui-btn" style="width: 120px" >宿舍预分配</a></label>
                        <div class="layui-input-block" style="margin-left: 150px;">
                            <div class="layui-form-mid layui-word-aux-ts" style="margin-left: 40px; padding: 18px 0;">
                                <span id="xsxx_xh">
                                    <b>
                                    <a href="#" onclick="detailclick('get_batch_hasbed')">已提供床位<span id="hasbedcount" style="color:red;"></span>个</a>  
<%--                                    <a href="#" onclick="detailclick('get_batch_nohasbedclass')">缺少床位数:<span id="nohasbedcount" style="color:red;"></span></a>
                                    <a href="#" onclick="detailclick('get_batch_hasbed_buterror')">错分床位数:<span id="hasbed_buterrorcount" style="color:red;"></span></a>--%>
                                    <a href="#" onclick="detailclick('get_batch_nohasbedclass_boy')">缺少男生床位<span id="nohasbedcount_boy" style="color:red;"></span>个</a>
                                    <a href="#" onclick="detailclick('get_batch_nohasbedclass_girl')">缺少女生床位<span id="nohasbedcount_girl" style="color:red;"></span>个</a>                                    
                                    </b>
                                </span>
                            </div>
                        </div>
                    </div>
<%--                    <div class="layui-form-item" pane="" style="min-height: 56px">
                        <label class="layui-form-label" style="width: 150px;"><a class="layui-btn" style="width: 120px" >现场迎新设置数据</a></label>
                        <div class="layui-input-block" style="margin-left: 150px;">
                            <div class="layui-form-mid layui-word-aux-ts" style="margin-left: 40px; padding: 18px 0;">
                                <span id="xsxx_xh"><font color="red">
                                    <b>
                                    <a href="#" onclick="detailclick('get_batch_hascollageaffair')">已设置学院事务数量:<span id="hascollageaffairacount" style="color:red;"></span></a>
                                    <a href="#" onclick="detailclick('get_batch_nohascollageaffair')">未学院事务数量:<span id="nohascollageaffairacount" style="color:red;"></span></a>  
                                    </b>
                                 </font></span>
                            </div>
                        </div>
                    </div>--%>

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



         <script type="text/javascript" src="../b_js/jquery.min2.js"></script>
        <script type="text/javascript" src="../b_js/app/defaultczzt.js"></script>
        <script>load();</script>

</body>
</html>

