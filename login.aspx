<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="admin_Default" %>

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
                
                <h4 class="header_tit_txt" id="login_title"  runat="server" style="font-family: arial,Hiragino Sans GB,Microsoft YaHei,微軟正黑體,儷黑 Pro!important">用户登录</h4>
              <div class=" site_info"></div></div>
            </div>
            <!-- header e -->
            <div style=" text-align:center;">
              <div class="login_area">
              <div id="login-main-form">
                  <div class="loginbox c_b">
                    <!-- 输入框 -->
                    <div class="lgn_inputbg c_b" style="text-align:left">
                        <!--验证用户名--><div class="single_imgarea" id="account-info">
                        <div class="na-img-area" id="account-avator" style="display:none">
                          <div class="na-img-bg-area" id="account-avator-con"></div>
                        </div>
                        <p class="us_name" runat="server" id="account_displayname" style="font-size:18px; text-align:left">
                            批次选择:<asp:DropDownList ID="pc" runat="server" 
                                DataSourceID="SqlDataSource1" DataTextField="Batch_Name" 
                                DataValueField="PK_Batch_NO" Font-Size="Large" Visible="False">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
                                SelectCommand="SELECT Batch_Name, PK_Batch_NO FROM Fresh_Batch WHERE (Welcome_Begin < GETDATE()) AND (Welcome_End > GETDATE()) ORDER BY [Batch_Name] desc">
                            </asp:SqlDataSource>
                          </p>
                            <p class="us_name">
                                &nbsp;</p>
                        <p class="us_id"></p>
                      </div>
                      <label id="region-code" class="labelbox login_user c_b" for="">
                        <div class="turn_area"><a class="btn_turn" id="manual_code" href="javascript:void(0);" title="关闭国家码"></a></div>
                        <div class="country_list">
                          <div class="animation countrycode_selector" id="countrycode_selector">
                            <span class="country_code"><tt class="countrycode-value" id="countrycode_value"></tt><i class="icon_arrow_down"></i></span>
                          </div>
                        </div>
                        <input　runat="server" class="item_account" autocomplete="off" type="text" name="txt_name" id="txt_name" placeholder="请输入用户帐号" />
                      </label>
                      <div class="country-container" id="countrycode_container" style="display: none;">
                        <div class="country_container_con" id="countrycode_container_con"></div>
                      </div>
                      <label class="labelbox pwd_panel c_b">
                        <div class="eye_panel pwd-visiable">
                          <i class="eye pwd-eye">
                          <svg width="100%" height="100%" version="1.1" xmlns="http://www.w3.org/2000/svg">
                            <path class="eye_outer" d="M0 8 C6 0,14 0,20 8, 14 16,6 16, 0 8 z"></path>
                            <circle class="eye_inner" cx="10" cy="8" r="3"></circle>
                          </svg>
                          </i>
                        </div>
                        <input type="password" runat="server" placeholder="请输入密码" autocomplete="off" name="txt_pwd" id="txt_pwd">

                        &nbsp;<input type="text" placeholder="请输入密码" autocomplete="off" id="visiablePwd" style="display:none">
                      </label>

                        <label id="region-code" class="labelbox" for="">
                            <div style="float:left;width:250px">
                                <input runat="server"  class="item_account" autocomplete="off" type="text" name="txt_validate" id="txt_validate" placeholder="请输入验证码" />
                            </div>
                            <div style="float:right;width:100px">
                                <image src="nradmingl/yanzhengma.aspx" id="img_yzm" style="width:100px;height:45px;cursor:pointer" />
                                
                            </div>
                        
                      </label>
                        <script type="text/javascript">
                            var img = document.getElementById('img_yzm');
                            img.onclick = function () {
                                img.src = '../nradmingl/yanzhengma.aspx?temp=' + Date.now();
                            }
                         </script>
                        
</div>
                    <div class="security_Controller" style="display: none;">
                      <label class="checkbox_area"><input type="checkbox" id="trustSecurityController" class="checkbox">使用安全控件</label>
                    </div>
                    <div class="lgncode" id="captcha">
                    </div>
                    <!-- 错误信息 -->
                    <div class="err_tip" id="error-outcon">
                      <div class="dis_box"><em class="icon_error"></em><span class="error-con"></span></div>
                    </div>
                    <!-- 登录频繁 -->
                    <div id="error-forbidden" class="err_forbidden">您的操作频率过快，请稍后再试。</div>
                    <div class="btns_bg">
                      <asp:Button
                          ID="Button1" class="btnadpt btn_orange" style="border-radius: 5px;" runat="server" Text="立即登录" 
                            onclick="Button1_Click1" />
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
              <a class="outer-link" href="#" onclick="layer.open({  type: 2,  title: '学生重置登陆密码',  maxmin: true,  shadeClose: true,  area : ['90%' , '80%'],  content: '/view/xsxx_pwd_reset.aspx'  });">忘记密码？</a>
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
   <script src="b_js/jquery.min2.js"></script>
<script src="b_js/jquery.slideBox.min.js" type="text/javascript"></script>

   

  <script src="./layer/layer.js"></script>
  

    </form>
</body>
</html>
