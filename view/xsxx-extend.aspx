<%@ Page Language="C#" AutoEventWireup="true" CodeFile="xsxx-extend.aspx.cs" Inherits="view_xsxx_extend" %>

<!DOCTYPE html>

<html lang="zh-cn">
<head runat="server">
    <title></title>
    <meta charset="UTF-8" content="编码" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
     <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <!--引用ＬＡＹＵＩ前端必须ＣＳＳ-->
        <link rel="stylesheet" href="../nradmingl/plugins/layui/css/layui-qiu.css" media="all" />
		<%--<link rel="stylesheet" href="../nradmingl/plugins/global.css" media="all" />--%>
         <!--引用ＬＡＹＵＩ前端字体图标ＣＳＳ-->
		<link rel="stylesheet" href="../nradmingl/plugins/font-awesome/css/font-awesome.min.css" />
         <!--引用ＬＡＹＵＩ前端表格ＣＳＳ,使用表格时才引用-->
		<link rel="stylesheet" href="../nradmingl/plugins/table.css" />
    
     <!--引用ＬＡＹＵＩ前端必须ＣＳＳ OVER-->
   
   
</head>
<body>
    <div class="admin-main" style="    margin: 5px 15px 10px 15px;">
        <blockquote class="layui-elem-quote">
            <i class="layui-icon">&#xe602;</i>自助报到<i class="layui-icon">&#xe602;</i>完善个人信息
            <span style="float: right" id="btnback">
                 <a href="xxzz_xsindex.aspx" class="layui-btn layui-btn-small">
                    <i class="layui-icon">&#xe603;</i>
                </a>
            </span>
        </blockquote>
        <fieldset class="layui-elem-field layui-field-title" style="margin-top: 20px;">
            <legend style="font-size:16px;">请完善如下信息</legend>
        </fieldset>
        <form id="form1" class="layui-form" runat="server">
            <asp:HiddenField ID="hidden_pk_sno" Value="" runat="server" />
            <asp:HiddenField ID="pk_batch_no" Value="" runat="server" />
            <asp:HiddenField ID="pk_affair_no" Value="" runat="server" />
            <asp:HiddenField ID="pk_staff_no" Value="" runat="server" />
            <asp:HiddenField ID="server_msg" Value="" runat="server" />

            <!--姓名-->
            <div class="layui-form-item">
                <label class="layui-form-label">姓名：</label>
                <div class="layui-input-inline">
                    <input type="text" name="xsxx_xm" id="xsxx_xm" value="" lay-verify="required" autocomplete="off" class="layui-input" readonly="true">
                </div>
            </div>
            <!--性别-->
            <div class="layui-form-item">
                <label class="layui-form-label">性别：</label>
                <div class="layui-input-inline">
                    <input type="text" name="xsxx_xb" id="xsxx_xb" value="" lay-verify="required" autocomplete="off" class="layui-input" readonly="true">
                </div>
            </div>
            <!--身份证号-->
            <div class="layui-form-item">
                <label class="layui-form-label">身份证号：</label>
                <div class="layui-input-inline">
                    <input type="text" name="xsxx_sfz" id="xsxx_sfz" value="" lay-verify="required" autocomplete="off" class="layui-input" readonly="true">
                </div>
            </div>
            <!--政治面貌和名族-->
            <div class="layui-form-item">
            <div class="layui-inline" style="">
                <label class="layui-form-label">政治面貌：</label>
                <div class="layui-input-inline">
                    <select name="xsxx_zzmm" id="xsxx_zzmm" runat="server"  lay-search="">
                        <option value="">请选择</option>
                        <option value="1">中共党员</option>
                    </select>
                </div>
            </div>

            <div class="layui-inline" style="">
                <label class="layui-form-label">民族：</label>
                <div class="layui-input-inline">
                    <select name="xsxx_mz" id="xsxx_mz" runat="server"  lay-search="">
                        <option value="">请选择</option>
                        <option value="1">汉族</option>                      
                    </select>
                </div>
            </div>
</div>
            <!--身高和体重-->
            <div class="layui-form-item">
            <div class="layui-inline" style="">
                <label class="layui-form-label">身高(cm)：</label>
                <div class="layui-input-inline">
                    <select name="xsxx_sg" id="xsxx_sg" runat="server" lay-search="">
                        <option value="">请选择</option>
                        <option value="150">160</option>

                    
                    </select>
                </div>

            </div>

            <div class="layui-inline" style="">
                <label class="layui-form-label">体重(kg)：</label>
                <div class="layui-input-inline">
                    <select name="xsxx_tz" id="xsxx_tz" runat="server" lay-search="">
                        <option value="">请选择</option>
                        <option value="50">50</option>
                    
                    </select>
                </div>
            </div>
</div>
            <!--户籍-->
            <div class="layui-form-item">
                <div class="layui-inline">
                    <label class="layui-form-label">户籍信息：</label>
                    <div class="layui-input-inline">
                        <select name="input_province" id="input_province">
                            <option value="-1">请选择省</option>
                            <option value="四川省" >四川省</option>
                        </select>
                    </div>
                    <div class="layui-input-inline">
                        <select name="input_city" id="input_city">
                            <option value="-1">请选择市</option>
                            <option value="成都市">成都市</option>

                        </select>
                    </div>
                    <div class="layui-input-inline">
                        <select name="input_area" id="input_area">
                            <option value="-1">请选择县/区</option>
                            <option value="锦江区">锦江区</option>
                        </select>
                    </div>
                </div>
            </div> 
            <!--家庭住址-->
            <div class="layui-form-item">
                <label class="layui-form-label">家庭住址：</label>
                <div class="layui-input-block">
                    <input type="text" name="xsxx_addr" id="xsxx_addr" lay-verify="address" autocomplete="off" placeholder="请输入现住址" class="layui-input">
                </div>
            </div>
            <!--手机号和QQ-->
            <div class="layui-form-item">
                <div class="layui-inline">
                    <label class="layui-form-label"><span style="color:#ff0000;font-size:16px">*</span>手机号：</label>
                    <div class="layui-input-inline">
                        <input type="tel" name="phone" id="phone"  lay-verify="phone"  placeholder="必填" autocomplete="off" class="layui-input">
                    </div>
                </div>
                <div class="layui-inline">
                    <label class="layui-form-label"><span style="color:#ff0000;font-size:16px">*</span>QQ号：</label>
                    <div class="layui-input-inline">
                        <input type="tel" name="qqnum" id="qqnum" lay-verify="qqnum" placeholder="必填" autocomplete="off" class="layui-input">
                    </div>
                </div>
            </div>
            <div style="text-align: center; float: left; margin-top: 10px; margin-left: 115px">
                <span>
                    <a href="" class="layui-btn layui-btn-small" lay-submit="" lay-filter="demo1" id="">
                        <i class="layui-icon">&#xe605;</i> 提交
                    </a>
                </span>
            </div>
            <div style="text-align: center; float: left;margin-top:10px;margin-left:5px">
                <span>
                    <a href="" type="reset" class="layui-btn layui-btn-small" id="">
                        <i class="layui-icon">ဂ</i> 撤销
                        
                    </a>
                </span>
            </div>          
        </form>

    </div>
    <script type="text/javascript" src="../nradmingl/plugins/layui/layui.js"></script>
    <script src="../b_js/app/city.js"></script>
    <script src="../b_js/app/xsxx-extend.js"></script>
</body>
</html>
