<%@ Page Language="C#" AutoEventWireup="true" CodeFile="stu-baodao.aspx.cs" Inherits="view_stu_baodao" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>报到需知</title>
    <meta charset="UTF-8" content="编码" />
    <meta name="renderer" content="webkit" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="format-detection" content="telephone=no" />
    <style>
        .admin-main {
            width: 90%;
            background-color: #024F8A;
            color: #ffffff;
            margin: 10px 5%;
            padding: 10px;
        }

        h2 {
            width: 350px;
            text-align: center;
            margin: 0 auto;
        }

        p {
            text-indent: 2em;
        }

        .btn_panel {
            width: 100%;
            margin: 0 auto;
            font-weight: bold;
            text-align: center;
        }
        
        .btnConfirm {
            background-color: #ff6a00;
            color:#ffffff;
            border: none;
            width: 120px;
            margin-left: 12px;
            height: 28px;
            font-size: 1em;
        }

        .btn_disable{
            background-color:#808080;
            color:#ff6a00;
            border:none;
            width: 120px;
            margin-left: 12px;
            height: 28px;
            font-size: 1em;
        }
        #tip{
            position:fixed;
            bottom:5%;
            right:6%;
            width:38px;
            height:43px;
            padding-left:5px;
            color:#ffffff;
            display:none;
            background-color:#808080;
        }

        @media(max-width:480px){
            #tip{
                display:block;
            }
        }
    </style>
</head>
<body>
    <form id="form1" class="layui-form layui-form-pane" runat="server">
        <asp:HiddenField ID="server_msg" runat="server" />
        <div class="admin-main">
            <div class="container">
                <h2>成都工业职业技术学院</h2>
                <h2>新生网上报到须知</h2>
                <h3>亲爱的新同学，成都工业职业技术学院欢迎您！</h3>
                <p>为了使您到校后迅速开始校园生活，本系统将帮助您提前完成入校报到手续。现将本系统使用方法作如下介绍和说明。</p>
                <p>1、	请手机网页浏览器或电脑网页浏览器使用本系统。桌面电脑网页浏览器建议使用IE、谷歌等高版本浏览器。
</p>
                <p>2、	请您务必认真核实报到注册内容中的个人、录取专业信息是否与录取书内容一致。如果不一致，请根据系统提供的“辅导员”信息电话联系辅导员处理。</p>
                <p>3、	请您在第一次登录本系统后，及时修改登录密码。</p>
                <p>4、	您需要在“网上缴费——缴费项目选择”页面中选择入学时需缴纳的缴费款项。不需申请办理“助学贷款”的同学，在操作过程中选择“正常缴费”选项。家庭经济特别困难，需要办理“助学贷款”的同学，需要准备好相关材料到校后申请办理，并在操作过程中选择“助学贷款”选项。</p>
                <p>5、	“网上缴费——缴费信息查看”页面中将为您提供“待缴费”和“已缴费”信息。</p>
                <p>6、	需要您注意的是，在“网上缴费”支付过程中，使用手机网页浏览器的用户只能选择中国建设银行进行支付，使用电脑网页浏览器的用户可选择中国银行、中国建设银行、中国工商银行、中国农业银行、交通银行、中国邮政储蓄银行等进行支付。在支付过程中系统还会提示“学生数超过加密数”、“订单将在15分钟后作废，请尽快支付”等正常提示信息。</p>
                <p>7、	在“网上缴费”中缴纳了“住宿费”后，才能进行“寝室选择”操作。如果在“寝室选择”中没有可选择的“床位”，请您及时电话联系辅导员处理。</p>
                <p>8、	请您认真填写“信息完善”页面内容，务求准确。部分内容为您无需改动的基本信息，可填写项尽量不要留空。</p>
                <p>9、	“通知”页面将为您提供辅导员发送给您的通知。</p>
                <p>成都工业职业技术学院期待您的到来！</p>
            </div>

            <div class="btn_panel">
                <asp:CheckBox ID="checkCofirm" runat="server" Text="已阅读" />
                <asp:Button ID="btnConfirm" CssClass="btn_disable" runat="server" Text="10 秒" Enabled="false" OnClick="btnConfirm_Click" />
            </div>
            
        </div>        
    </form>
    <div id="tip"><span>向下滑动</span></div>
    <script type="text/javascript" src="../nradmingl/plugins/layui/layui.js"></script>
    <script src="../b_js/app/stu-baodao.js"></script>
    
</body>
</html>
