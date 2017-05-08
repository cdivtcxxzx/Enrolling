﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Defaultczzt.aspx.cs" Inherits="view_Defaultczzt" %>



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

            <i class="layui-icon">&#xe602;</i>迎新管理&gt;&gt;后台管理首页
            <span style="float: right">当前迎新年度：2017年&nbsp;&nbsp;&nbsp;
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
            <div class="layui-form-item">
                <label class="layui-form-label">迎新批次</label>
                <div class="layui-input-block">
                    <select name="freshbatch" id="freshbatch">
                    </select>
                </div>
            </div>
           <div class="layui-form-item">
                <label class="layui-form-label">学院</label>
                <div class="layui-input-block">
                    <select name="collageList" id="collageList">
                    </select>
                </div>
            </div>

        <div class="xsxx2">
                                <div class="layui-form-item" pane="" style="min-height: 56px">
                        <label class="layui-form-label" style="width: 150px;"><a class="layui-btn" onclick="javascript:location.href='xszz-index.aspx';">迎新学生</a></label>
                        <div class="layui-input-block" style="margin-left: 150px;">
                            <div class="layui-form-mid layui-word-aux-ts" style="margin-left: 40px; padding: 18px 0;">
                                <span id="xsxx_xh"><font color="red"><b>总数:<span id="studentcount"></span>  男:<span id="mancount"></span>   女:<span id="womancount"></span>   </b></font></span>
                            </div>
                        </div>
                    </div>

                    <div class="layui-form-item" pane="" style="min-height: 56px">
                        <label class="layui-form-label" style="width: 150px;"><a class="layui-btn" onclick="javascript:location.href='xszz-index.aspx';">迎新学院</a></label>
                        <div class="layui-input-block" style="margin-left: 150px;">
                            <div class="layui-form-mid layui-word-aux-ts" style="margin-left: 40px; padding: 18px 0;">
                                <span id="xsxx_xh"><font color="red"><a href="#" onclick="detailclick('get_detail_batch_collage')">数量:<span id="collagecount"></span> </a></font></span>
                            </div>
                        </div>
                    </div>

                    <div class="layui-form-item" pane="" style="min-height: 56px">
                        <label class="layui-form-label" style="width: 150px;"><a class="layui-btn" onclick="javascript:location.href='xszz-index.aspx';">迎新专业</a></label>
                        <div class="layui-input-block" style="margin-left: 150px;">
                            <div class="layui-form-mid layui-word-aux-ts" style="margin-left: 40px; padding: 18px 0;">
                                <span id="xsxx_xh"><font color="red"><a href="#" onclick="detailclick('get_detail_batch_spe')">数量:<span id="specount"></span></a></font></span>
                            </div>
                        </div>
                    </div>



                    <div class="layui-form-item" pane="" style="min-height: 56px">
                        <label class="layui-form-label" style="width: 150px;"><a class="layui-btn" onclick="javascript:location.href='xszz-index.aspx';">迎新班级</a></label>
                        <div class="layui-input-block" style="margin-left: 150px;">
                            <div class="layui-form-mid layui-word-aux-ts" style="margin-left: 40px; padding: 18px 0;">
                                <span id="xsxx_xh"><font color="red">
                                    <b>
                                        校区数量:<span id="campuscount"></span> 
                                        <a href="#" onclick="detailclick('get_batch_spe_hasclass')">班级数量:<span id="spehasclasscount"></span></a>  
                                        <a  href="#" onclick="detailclick('get_batch_spe_nohasclass')">遗漏班级专业数量：<span id="spenohasclasscount"></span></a>
                                    </b></font></span>
                            </div>
                        </div>
                    </div>

                                <div class="layui-form-item" pane="" style="min-height: 56px">
                        <label class="layui-form-label" style="width: 150px;"><a class="layui-btn" onclick="javascript:location.href='xszz-index.aspx';">收费设置</a></label>
                        <div class="layui-input-block" style="margin-left: 150px;">
                            <div class="layui-form-mid layui-word-aux-ts" style="margin-left: 40px; padding: 18px 0;">
                                <span id="xsxx_xh"><font color="red">
                                    <a href="#" onclick="detailclick('get_batch_college_financial')">交费项目数量:<span id="collegefinancialcount"></span></a>  
                                 </font></span>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="xsxx3">

                    <div class="layui-form-item" pane="" style="min-height: 56px">
                        <label class="layui-form-label" style="width: 150px;"><a class="layui-btn" style="width: 120px" onclick="javascript:location.href='xszz-index.aspx';">新生分班</a></label>
                        <div class="layui-input-block" style="margin-left: 150px;">
                            <div class="layui-form-mid layui-word-aux-ts" style="margin-left: 40px; padding: 18px 0;">
                                <span id="xsxx_xh"><font color="red">
                                    <b>
                                    <a href="#" onclick="detailclick('get_batch_class_hasstudent')">已分班人数:<span id="classhasstudentcount"></span></a>  
                                    <a href="#" onclick="detailclick('get_batch_spe_nohasclassstudent')">未分班人数:<span id="spenohasclassstudentcount"></span></a>
                                    <a href="#" onclick="detailclick('get_batch_class_hasstudent_buterror')">错分班人数:<span id="classhasstudent_buterrorcount"></span></a>  
                                    </b>
                                    </font></span>
                            </div>
                        </div>
                    </div>
                    <div class="layui-form-item" pane="" style="min-height: 56px">
                        <label class="layui-form-label" style="width: 150px;"><a class="layui-btn" style="width: 120px" onclick="javascript:location.href='xszz-index.aspx';">辅导员设置</a></label>
                        <div class="layui-input-block" style="margin-left: 150px;">
                            <div class="layui-form-mid layui-word-aux-ts" style="margin-left: 40px; padding: 18px 0;">
                                <span id="xsxx_xh"><font color="red">
                                    <b>
                                    <a href="#" onclick="detailclick('get_batch_hascounseller')">已设置班级数量:<span id="hascounsellercount"></span></a>  
                                    <a href="#" onclick="detailclick('get_batch_nohascounseller')">未设置班级数量:<span id="nohascounsellercount"></span></a>
                                    </b>
                                    </font></span>
                            </div>
                        </div>
                    </div>

                    <div class="layui-form-item" pane="" style="min-height: 56px">
                        <label class="layui-form-label" style="width: 150px;"><a class="layui-btn" style="width: 120px" onclick="javascript:location.href='xszz-index.aspx';">预分配宿舍</a></label>
                        <div class="layui-input-block" style="margin-left: 150px;">
                            <div class="layui-form-mid layui-word-aux-ts" style="margin-left: 40px; padding: 18px 0;">
                                <span id="xsxx_xh"><font color="red">
                                    <b>
                                    <a href="#" onclick="detailclick('get_batch_hasbed')">已预分床位人数:<span id="hasbedcount"></span></a>  
                                    <a href="#" onclick="detailclick('get_batch_nohasbedclass')">未预分床位人数:<span id="nohasbedcount"></span></a>
                                    <a href="#" onclick="detailclick('get_batch_hasbed_buterror')">错分床位人数:<span id="hasbed_buterrorcount"></span></a>
                                    </b>
                                 </font></span>
                            </div>
                        </div>
                    </div>
                    <div class="layui-form-item" pane="" style="min-height: 56px">
                        <label class="layui-form-label" style="width: 150px;"><a class="layui-btn" style="width: 120px" onclick="javascript:location.href='xszz-index.aspx';">操作员设置</a></label>
                        <div class="layui-input-block" style="margin-left: 150px;">
                            <div class="layui-form-mid layui-word-aux-ts" style="margin-left: 40px; padding: 18px 0;">
                                <span id="xsxx_xh"><font color="red">
                                    <b>
                                    <a href="#" onclick="detailclick('get_batch_hascollageaffair')">已设置学院的事务数量:<span id="hascollageaffairacount"></span></a>
                                    <a href="#" onclick="detailclick('get_batch_nohascollageaffair')">漏设学院的事务数量:<span id="nohascollageaffairacount"></span></a>  
                                    </b>
                                 </font></span>
                            </div>
                        </div>
                    </div>

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

