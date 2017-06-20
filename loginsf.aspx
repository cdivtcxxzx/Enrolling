<%@ Page Language="C#" AutoEventWireup="true" CodeFile="loginsf.aspx.cs" Inherits="loginsf" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%
    
    
    string agent = (Request.UserAgent + "").ToLower().Trim();
   

    if (agent == "" ||
        agent.IndexOf("mobile") != -1 ||
        agent.IndexOf("mobi") != -1 ||
        agent.IndexOf("nokia") != -1 ||
        agent.IndexOf("samsung") != -1 ||
        agent.IndexOf("sonyericsson") != -1 ||
        agent.IndexOf("mot") != -1 ||
        agent.IndexOf("blackberry") != -1 ||
        agent.IndexOf("lg") != -1 ||
        agent.IndexOf("htc") != -1 ||
        agent.IndexOf("j2me") != -1 ||
        agent.IndexOf("ucweb") != -1 ||
        agent.IndexOf("opera mini") != -1 ||
        agent.IndexOf("mobi") != -1)
    {                                                                                                                                                                                                                                                                                                   
        //终端可能是手机
        Response.Write("<?xml version='1.0'?><!DOCTYPE html><HTML>");
    }
    else
    {
        Response.Write(" <!DOCTYPE html><html>");
    }
     %>
<head runat="server">
    

<meta http-equiv="Content-Type" content="text/html; charset=UTF-8"><meta name="description" content="。">
<meta name="keywords" content="">


    <link rel="stylesheet" href="b_css/login.css">
    
    <meta name="viewport" content="width=device-width,initial-scale=1.0,minimum-scale=1.0, maximum-scale=1.0,user-scalable=no">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge">
    <title>用户登录</title>

</head>
<body>
    <form id="form1" runat="server">
    <div class="wrapper">
      <div class="wrap" style="text-align:center">
        <div class="layout" id="layout">
          <!--表单输入登录-->
          <div class="mainbox" id="login-main" style="display: block;">
            <div><!--<a class="ercode" id="qrcode-trigger" href="javascript:void(0)"></a>--></div>
            <!-- header s -->
            <div class="lgnheader">
              <div class="header_tit t_c">
                
                <h4 class="header_tit_txt" id="login_title"  runat="server" style="font-family: arial,Hiragino Sans GB,Microsoft YaHei,微軟正黑體,儷黑 Pro!important">迎新系统用户登录选择</h4>
              <div class=" site_info"></div></div>
            </div>
            <!-- header e -->
            <div style=" text-align:center;">
              <div class="login_area">
              <div id="login-main-form">
                  <div class="loginbox c_b">

                    <div class="btns_bg">
                      <asp:Button
                          ID="Button1" class="btnadpt btn_orange" style="border-radius: 5px;" runat="server" Text="系统管理员登录" 
                            onclick="Button1_Click1" Font-Size="Large" />
                        <br />
                        <asp:Button
                          ID="Button2" class="btnadpt btn_orange" style="border-radius: 5px;" runat="server" Text="二级院系管理员登录" 
                            onclick="Button1_Click2" Font-Size="Large" />
                          <br />
                        <asp:Button
                          ID="Button3" class="btnadpt btn_orange" style="border-radius: 5px;" runat="server" Text="迎新引导员登录" 
                            onclick="Button1_Click3" Font-Size="Large" />
                          <br />
                        <asp:Button
                          ID="Button4" class="btnadpt btn_orange" style="border-radius: 5px;" runat="server" Text="辅导员（班主任）登录" 
                            onclick="Button1_Click4" Font-Size="Large" />

                      <span id="custom_display_8" class="sns-default-container sns_default_container" style="display: none;">
                      </span>
                      <asp:Label ID="Label1" runat="server" Font-Size="Medium" Font-Strikeout="False" 
                          ForeColor="#CC0000"></asp:Label>
                    </div>
                    <!-- 其他登录方式 s -->
                    <div style="display: none;" class="other_login_type sns-login-container" id="custom_display_16">
                      <fieldset class="oth_type_tit">
                        <legend align="center" class="oth_type_txt">其他方式登录</legend>
                      </fieldset>
                      <div class="oth_type_links" >
                        <a class="icon_type btn_qq sns-login-link" data-type="qq" href="#" title="QQ登录" target="_blank"><i class="btn_sns_icontype icon_default_qq"></i></a>
                       
                        <a class="icon_type btn_alipay sns-login-link" data-type="alipay" href="#" title="支付宝登录" target="_blank"><i class="btn_sns_icontype icon_default_alipay"></i></a>
                        <a class="icon_type btn_weixin sns-login-link" data-type="weixin" href="#" title="微信登录" style="display: "><i class="btn_sns_icontype icon_default_weixin"></i></a>
                      </div>
                    </div>
                  </div>
               
              </div>
              </div>
            </div>
            <!-- 其他登录方式 e -->
            <div class="n_links_area" id="custom_display_64">
			<a class="outer-link" href="/">返回首页</a><span>|</span>
              <a class="outer-link" href="#" onclick="layer.open({  type: 2,  title: '学生重置登陆密码',  area : ['90%' , '80%'],  content: '/view/xsxx_pwd_reset.aspx'  });">忘记密码？</a>
            </div>
          </div>

          <div class="ercode_area" id="login-qrcode" style="height: 484px; width: 400px; display: none;">
            <div class="ercode_pannel">
              <a class="code_close" href="javascript:void(0)" title="关闭" id="qrcode-close"><span class="icon_code_close"></span></a>
              <div class="ercode_box">
                
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
   <script src="b_js/jquery.min2.js" type="text/javascript"></script>
<script src="b_js/jquery.slideBox.min.js" type="text/javascript"></script>
  <script src="./layer/layer.js" type="text/javascript"></script>
<script src="b_js/weixin.js" type="text/javascript"></script>
    </form>
</body>
</html>

