<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ssgl_clear.aspx.cs" Inherits="nradmingl_ssgl_clear" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>宿舍预分配清除</title>
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
        .inputw input{width:20px;height:20px;}
        .inputw  label{
            font-size: 16px;
        }
    </style>
    <form id="form1"  runat="server">
    <div class="admin-main">
      <blockquote class="layui-elem-quote">
          <i class="layui-icon">&#xe602;</i>学生寝室预分配<i class="layui-icon">&#xe602;</i>清空寝室数据
           <span style="float:right">

            <!--调用C#原生按钮设置样式举例OVER-->
          <%--               <a href="#" class="layui-btn layui-btn-small hidden-xs">
					<i class="layui-icon">&#xe630;</i> 一卡通更新
				</a>
             --%>
              
               
               
		  </span>       
      </blockquote>

                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
        
        <div>
           年度选择： <asp:DropDownList ID="year" runat="server" DataSourceID="SqlDataSource1" DataTextField="Year" DataValueField="Year"></asp:DropDownList>
            <asp:DropDownList ID="yxdm" runat="server" DataSourceID="SqlDataSource2" DataTextField="Name" DataValueField="PK_College">
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnString %>" SelectCommand="SELECT [PK_College], [Name] FROM [Base_College] WHERE ([Enabled] = @Enabled) ORDER BY [College_NO]">
                <SelectParameters>
                    <asp:Parameter DefaultValue="true" Name="Enabled" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            <br />
            <div id="delroom" style="display:none;" runat="server">
           &nbsp;&nbsp;&nbsp;&nbsp; <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnString %>" SelectCommand="SELECT DISTINCT [Year] FROM [Fresh_Room_Type] ORDER BY [Year]"></asp:SqlDataSource>
            <br /> <br />&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="c_roomtype" CssClass="inputw" runat="server" Text="清空房间类型信息" AutoPostBack="True" OnCheckedChanged="c_roomtype_CheckedChanged" />
           &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="c_dorm" runat="server" CssClass="inputw"  Text="清空公寓宿舍信息" AutoPostBack="True" OnCheckedChanged="c_dorm_CheckedChanged" />
          <br />  <br />&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="c_room" CssClass="inputw"  runat="server" Text="清除所有房间信息" AutoPostBack="True" OnCheckedChanged="c_room_CheckedChanged" />
           &nbsp;&nbsp;&nbsp;&nbsp; <asp:CheckBox ID="c_bed" runat="server" CssClass="inputw"  Text="清空所有床位信息" AutoPostBack="True" OnCheckedChanged="c_bed_CheckedChanged" />
           &nbsp;【注：房间类型、公寓、房间、床位与院系无关，仅与年度有关】<br /></div><br /> <br />&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="c_bedyfpyx" CssClass="inputw"   runat="server" Text="清空院系预分配信息" AutoPostBack="True" OnCheckedChanged="c_bedyfpyx_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="c_bedyfp" CssClass="inputw"  Checked="true" runat="server" Text="清空班级预分配信息" />
       <br /> 
            <br />
            <br /> 
              <asp:Label ID="ztts" runat="server" Font-Size="Medium" 
                  Text="请谨慎操作，该操作会清空你所能管理的所有预分配数据"></asp:Label>



            <br />
            <br />
        </div>    
  <div>   
                   
   

      
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton CssClass="layui-btn layui-btn-small" name="exportexcel1" onclick="clearyfp"  txttop="txttop" ToolTip="确认清空" ID="LinkButton13" runat="server"    Text='' ><i class="layui-icon">&#xe61e;</i>确认清空</asp:LinkButton>



              <br />
              <br />



      <asp:GridView ID="GridView1"   CssClass="site-table table-hover"  runat="server">
           
        </asp:GridView>





      <!--与后台配合的提示信息隐藏域-->
            <input id="tsxx" type="text" runat="server" value="" style="display:none" />
            <input id="tsbox" type="text" runat="server" value="" style="display:none" />
			<!--与后台配合的提示信息隐藏域OVER-->
            
			<div class="admin-table-page" style="display:none">
				<div id="page" class="page">
				</div>
			</div>
		      <br />
		</div>
     
		<!--引发ＬＡＹＵＩ前端必须ＪＳ-->
   
    </form>
</body>
</html>
