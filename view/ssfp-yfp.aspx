<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ssfp-yfp.aspx.cs" Inherits="view_ssfp_yfp" %>


<!DOCTYPE html>
<html lang="zh-cn">
<head runat="server">
    <title>宿舍分配-预分配</title>
    <meta charset="UTF-8" content="编码" />
        <meta name="renderer" content="webkit">
		<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
		<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
		<meta name="apple-mobile-web-app-status-bar-style" content="black">
		<meta name="apple-mobile-web-app-capable" content="yes">
		<meta name="format-detection" content="telephone=no">

    <!--引用ＬＡＹＵＩ前端必须ＣＳＳ-->
        <link rel="stylesheet" href="../nradmingl/plugins/layui/css/layui.css" media="all" />
		<link rel="stylesheet" href="../nradmingl/plugins/global.css" media="all" />
         <!--引用ＬＡＹＵＩ前端字体图标ＣＳＳ-->
		<link rel="stylesheet" href="../nradmingl/plugins/font-awesome/css/font-awesome.min.css" />
   
    <link href="../bootstrap/3.3.4/css/bootstrap.min.css" rel="stylesheet" />
   
</head>
<body>
    <form id="form1" class="layui-form layui-form-pane" runat="server">
                    <asp:HiddenField ID="pk_staff_no" Value="" runat="server" />
                    <asp:HiddenField ID="pk_affair_no" Value="" runat="server" />

         <div class="admin-main">
     <!--顶部提示及导航-->
    		<blockquote class="layui-elem-quote">
          
            <i class="layui-icon">&#xe602;</i>学生网上自助报到>>预分配宿舍
            <span style="float:right" id="btnback">
            
				
                 <a href="javascript:history.go(-1);" class="layui-btn layui-btn-small">
					<i class="layui-icon">&#xe603;</i>
				</a>
               </span>
				<div style="display:none">
                    <asp:Label ID="xh" runat="server" Text=""></asp:Label></div>
			</blockquote>
                
                <style>
                    .layui-form select{height:37px;width:100%;}
                    .layui-form input[type=checkbox], .layui-form input[type=radio], .layui-form select {display:inherit
                    }
                    label{
                        vertical-align: inherit;
                    }
                    #cwts{margin:10px;}
                    .layui-elem-field legend {
                        margin-left: 20px;
                        padding: 0 10px;
                        font-size: 16px;
                        font-weight: 300;
                    }
                    legend {
    display: block;
    padding: 0;
    margin-bottom: 0px;
    font-size: 21px;
    line-height: inherit;
    color: #333;
    border: 0;
    border-bottom: 0px solid #e5e5e5;
    width: inherit;
}
                </style>

 <!--顶部提示及导航OVER-->
        <div class="container">
                      <asp:ScriptManager ID="ScriptManager1" runat="server">
         </asp:ScriptManager>
  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                  <ContentTemplate>
            <div class="col-xs-12 col-sm-4" style="margin-top:15px;border:1px solid #eee;text-align:center;" >
               <style>.noshow{display:none}</style> <p style="margin-top:20px;"><span>校区：</span><span id="xiaoqu" runat="server"><asp:Label ID="xqbh" runat="server" CssClass="noshow" Text="01"></asp:Label><asp:Label ID="xqmc" runat="server" Text="天府新区"></asp:Label></span></p>
               <%-- <p><span>类型：</span><span id="shuse" runat="server">男宿舍</span></p>--%>

                <p style="margin-top:10px;margin-bottom:10px;">
                    <img src="../images/xsgysmall.jpg" alt="宿舍照片" class="xsgytp" style="margin-top: 18px; width: 90%; height: 90%" id="shuseImg" runat="server" />
                </p>
                
            </div>
                      </ContentTemplate></asp:UpdatePanel>
            <div class="col-xs-12 col-sm-8" style="margin-top:15px;">
                
                <div class="layui-form-item" pane="">
          <label class="layui-form-label" style="width:120px;">学号：</label>
          <div class="layui-input-block" style="margin-left:120px;">
           <div class="layui-form-mid layui-word-aux-ts" style="margin-left:10px;">
               <asp:Label ID="xsxx_xh" runat="server" Text="3"></asp:Label>

           </div>

          </div>
        </div>
                 <div class="layui-form-item" pane="">
          <label class="layui-form-label" style="width:120px;">姓名：</label>
          <div class="layui-input-block" style="margin-left:120px;">
           <div class="layui-form-mid layui-word-aux-ts" style="margin-left:10px;">
               <asp:Label ID="xsxx_xm" runat="server" Text="张三"></asp:Label></div></div>
        </div>
                 <div class="layui-form-item" pane="">
          <label class="layui-form-label" style="width:120px;">班级：</label>
          <div class="layui-input-block" style="margin-left:120px;">
           <div class="layui-form-mid layui-word-aux-ts" style="margin-left:10px;">
               <asp:Label ID="xsxx_bj" runat="server" Text="汽修1701班"></asp:Label></div></div>
        </div>

        
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                  <ContentTemplate>

                 <div class="layui-form-item" id="sc_lx" runat="server">
                    <label class="layui-form-label"  style="width:120px;border:1px solid #e6e6e6">房间类型选择</label>
                <div class="layui-input-block"  style="margin-left:120px;border:1px solid #e6e6e6">
                 
                  <asp:DropDownList ID="xq_roomtype"  lay-filter="aihao" runat="server" 
             DataSourceID="ObjectDataSource1" DataTextField="name" DataValueField="id" 
             AutoPostBack="True" Enabled="false" onselectedindexchanged="xq_roomtype_SelectedIndexChanged"></asp:DropDownList>


         <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
             SelectMethod="serch_room_type" TypeName="dormitory">
             <SelectParameters>
                 <asp:ControlParameter ControlID="xsxx_xh" Name="PK_SNO" PropertyName="Text" 
                     Type="String" />
             </SelectParameters>
         </asp:ObjectDataSource>
                   
                </div>
                 </div>
                 

                       <div class="layui-form-item"  id="sc_ld" runat="server">
                    <label class="layui-form-label"  style="width:120px;">宿舍楼栋选择</label>
                <div class="layui-input-block"  style="margin-left:120px;">
                 <asp:DropDownList ID="xq_dorm" runat="server" DataSourceID="ObjectDataSource2" 
             DataTextField="Name" DataValueField="id" AutoPostBack="True" 
             onselectedindexchanged="xq_dorm_SelectedIndexChanged">
        </asp:DropDownList>
         <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
             SelectMethod="serch_dorm" TypeName="dormitory">
             <SelectParameters>
                 <asp:ControlParameter ControlID="xsxx_xh" Name="PK_SNO" PropertyName="Text" 
                     Type="String" />
             </SelectParameters>
         </asp:ObjectDataSource>

                </div>
                 </div>
                 <div class="layui-form-item"  id="sc_lc" runat="server">
                    <label class="layui-form-label"  style="width:120px;">楼层选择</label>
                <div class="layui-input-block"  style="margin-left:120px;">
                 <asp:DropDownList ID="xq_floor" runat="server" DataSourceID="ObjectDataSource3" 
             DataTextField="floor" DataValueField="floor" AutoPostBack="True" 
             onselectedindexchanged="xq_floor_SelectedIndexChanged">
        </asp:DropDownList>
         <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" 
             SelectMethod="serch_dorm" TypeName="dormitory">
             <SelectParameters>
                 <asp:ControlParameter ControlID="xsxx_xh" Name="PK_SNO" PropertyName="Text" 
                     Type="String" />
                 <asp:ControlParameter ControlID="xq_dorm" Name="dormid" 
                     PropertyName="SelectedValue" Type="String" />
             </SelectParameters>
         </asp:ObjectDataSource>
                </div>
                 </div>
           
  <fieldset class="layui-elem-field"  id="sc_fjxc"  runat="server">
  <legend>房间选择</legend>
  <div class="layui-field-box" style="    padding: 10px 5px;" >
     <div class="layui-input-block" style="    margin-left:2px;">
        <asp:RadioButtonList  onselectedindexchanged="R_room_SelectedIndexChanged" ID="R_room" RepeatDirection="Horizontal"  runat="server" 
          DataSourceID="ObjectDataSource4" DataTextField="name" DataValueField="id" 
             AutoPostBack="True">
      </asp:RadioButtonList>
         <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" 
          SelectMethod="serch_room" TypeName="dormitory">
          <SelectParameters>
              <asp:ControlParameter ControlID="xsxx_xh" Name="PK_SNO" PropertyName="Text" 
                  Type="String" />
              <asp:ControlParameter ControlID="xq_dorm" Name="dormid" 
                  PropertyName="SelectedValue" Type="String" />
              <asp:ControlParameter ControlID="xq_floor" Name="floor" 
                  PropertyName="SelectedValue" Type="String" />
          </SelectParameters>
      </asp:ObjectDataSource>
         
    </div>
  </div>
</fieldset>

                 <fieldset class="layui-elem-field"   id="sc_cwxc"  runat="server">
  <legend>床位选择</legend>
  <div class="layui-field-box" style="    padding: 10px 5px;">
     <div class="layui-input-block" style="    margin-left:2px;">
          <asp:RadioButtonList RepeatDirection="Horizontal" ID="R_bed" runat="server" AutoPostBack="True" 
             DataSourceID="ObjectDataSource5"  DataTextField="name"  onselectedindexchanged="R_bed_SelectedIndexChanged"  DataValueField="id">
         </asp:RadioButtonList>
         <asp:ObjectDataSource ID="ObjectDataSource5" runat="server" 
             SelectMethod="serch_bed" TypeName="dormitory">
             <SelectParameters>
                 <asp:ControlParameter ControlID="R_room" Name="roomid" 
                     PropertyName="SelectedValue" Type="String" />
             </SelectParameters>
         </asp:ObjectDataSource>
         <br />
      <div style="margin-left:20px;">
               <asp:Label  ID="cwts"  runat="server" Text=""></asp:Label>

           </div>
      
         
    </div>
  </div>
</fieldset>
  <fieldset class="layui-elem-field">
  <legend>提示信息</legend>
  <div class="layui-field-box" id="xzts" runat="server">
    请先选择宿舍寝室信息！
  </div>
</fieldset>
 </ContentTemplate>
</asp:UpdatePanel>


<div class="layui-form-item" style="text-align:center">
          <asp:Button ID="sc_qsxz" runat="server"  class="layui-btn"  onclick="qsxz_Click" Text="确认寝室选择" />
&nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="backmain" runat="server"  class="layui-btn"  onclick="qsxz_Click2" Text="返回操作首页" />
        </div>


            </div>

        </div>
        <asp:HiddenField ID="hidenClassNo" runat="server" />
        <asp:HiddenField ID="hidenGender" runat="server" />

             </div>

        <asp:HiddenField ID="hiddenSno" runat="server" />

    </form>

   <script type="text/javascript" src="../b_js/jquery.min2.js"></script>

    <script type="text/javascript">
        var pk_staff_no = $("#pk_staff_no");
        if (!pk_staff_no) {
            //alert("null or undefined or NaN");
        } else {
            pk_staff_no = $("#pk_staff_no").val();
            if ($.trim(pk_staff_no).length > 0) {
                $('#btnback').hide();
                $('#backmain').hide();
            }
        }
    </script>
</body>
</html>
