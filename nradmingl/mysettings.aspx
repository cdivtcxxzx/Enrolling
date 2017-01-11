<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mysettings.aspx.cs" Inherits="admin_mysettings" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link runat="server" id="webcss" type="text/css" href="styleqt.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../files/common/jquery.js"></script>
<script type="text/javascript" src="include/common.js"></script>


<link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
<script src="images/jquery-1.9.1.js"></script>
  <script src="images/jquery-ui.js"></script>

  <script type="text/javascript">
      $(function () {
          $.datepicker.regional['zh-CN'] = {
              closeText: '关闭',
              prevText: '&#x3c;上月',
              nextText: '下月&#x3e;',
              currentText: '今天',
              monthNames: ['一月', '二月', '三月', '四月', '五月', '六月',
                '七月', '八月', '九月', '十月', '十一月', '十二月'],
              monthNamesShort: ['一', '二', '三', '四', '五', '六',
                '七', '八', '九', '十', '十一', '十二'],
              dayNames: ['星期日', '星期一', '星期二', '星期三', '星期四', '星期五', '星期六'],
              dayNamesShort: ['周日', '周一', '周二', '周三', '周四', '周五', '周六'],
              dayNamesMin: ['日', '一', '二', '三', '四', '五', '六'],
              weekHeader: '周',
              dateFormat: 'yy-mm-dd',
              firstDay: 1,
              isRTL: false,
              showMonthAfterYear: true,
              yearSuffix: '年'
          };
          $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
          $("#formView1_TB_csrq").datepicker({ dateFormat: "yy-mm-dd '00:00:00'" });
          //$("#datepicker1").datepicker();
          $("#formView1_TB_zcsj").datepicker({ dateFormat: "yy-mm-dd '23:59:59'" });
          // $("#datepicker2").datepicker();
      });
  </script>





</head>
<body id="C_User">
    <form id="form1" runat="server">
       <div id="PanelUpdate">
    <table class="table subsubmenu">
	<thead>
	<tr>
		<td>
        <asp:LinkButton runat="server" ID="LB_top" Text="个人设置&gt;&gt;&nbsp;个人资料"></asp:LinkButton>
           <a href="mysettings.aspx">个人资料</a>
		   <a href="showpower.aspx">查看权限</a>
           <a href="changpwd.aspx">修改密码</a>
		</td>
		

        
	</tr>
  </thead>
  
<asp:FormView id="formView1" runat="server"  CssClass="table"  
        DataSourceID="SqlDataSource1"   AllowPaging="false"  DefaultMode="Edit" DataKeyNames="yhid"
        >
<EditItemTemplate> 
<%-- <tr id="manage_UserName">
		<td rowspan="2" style="text-align: left;" class="style1">
	    <asp:Image ID="zp" runat="server" 
            ImageUrl='<%# Eval("yhzp") %>'  Width="99px" />
</tr>--%>
<tr>

<td class="style1">姓名&nbsp;</a></td>
    <td  style="width:395px;">
    
    <asp:TextBox ID="TB_xm" Text='<%# Bind("xm") %>'  runat="server"></asp:TextBox></td>
    <td style="width:120px;">姓名拼音&nbsp;</td>
    <td style="width:395px;">
	
	<asp:TextBox ID="TB_xmpy" Text='<%# Bind("xmpy") %>'  runat="server"></asp:TextBox>
	</td>
</tr>
<tr id="manage_Name">
	<td class="style1">性别&nbsp;</td>
	<td  >
	
    <asp:TextBox ID="TB_xb"  Text='<%# Bind("xb") %>' runat="server" Visible="false"></asp:TextBox>
      <asp:DropDownList ID="DDL_xb" runat="server"  >
    <asp:ListItem   Value="男">男</asp:ListItem>
    <asp:ListItem  Value="女">女</asp:ListItem>
    </asp:DropDownList>
	
	</td><td style="width:120px;">民族&nbsp;</a></td>
	<td  >
	
	<asp:TextBox ID="TB_mz"  Text='<%# Bind("mz") %>'  runat="server" Visible="false"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="DDL_mz" runat="server"  DataSourceID="Sql_mz" DataTextField="mz" DataValueField="mz">
    </asp:DropDownList>
    <asp:SqlDataSource ID="Sql_mz" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
        SelectCommand = "SELECT * FROM dm_minzu"
        >
    </asp:SqlDataSource>
        </td>
</tr>
 <tr id="manage_Email">
	<td class="style1">身份证</a></td>
	<td  >
	
	<asp:TextBox ID="TB_sfzjh"  Text='<%# Bind("sfzjh") %>' runat="server"></asp:TextBox>
	</td><td style="width:120px;">出生日期&nbsp;</a></td>
	<td>
	
	<asp:TextBox ID="TB_csrq"  Text='<%# Bind("csrq") %>' runat="server" ></asp:TextBox>
	</td>
</tr>
<tr id="manage_Tel">
	<td style="width:120px;">所属系部&nbsp;</a></td>
	<td  >
	
	<asp:Label ID="TB_yx"  Text='<%# Eval("yxmc") %>' runat="server"></asp:Label>
	</td>
    <td style="width:120px;">政治面貌&nbsp;</a></td>
	<td>
	
    <asp:TextBox ID="TB_zzmm"  Text='<%# Bind("zzmm") %>' runat="server" Visible="false"></asp:TextBox>
        <asp:DropDownList ID="DDL_zzmm" runat="server"  >
    <asp:ListItem   Value="">请选择</asp:ListItem>
    <asp:ListItem  Value="中共党员">中共党员</asp:ListItem>
    <asp:ListItem  Value="共青团员">共青团员</asp:ListItem>
    <asp:ListItem  Value="群众">群众</asp:ListItem>
    <asp:ListItem  Value="其它">其它</asp:ListItem>
    </asp:DropDownList></td>
</tr>



<tr id="manage_UserGroup">
    <td style="width:120px;">联系电话&nbsp;</td>
	<td   >
	
    <asp:TextBox ID="TB_lxdh"  Text='<%# Bind("lxdh") %> ' runat="server"></asp:TextBox>
	</td>
	<td style="width:120px;">E-MAIL地址</td>
	<td >
	
    <asp:TextBox ID="TB_email"  Text='<%# Bind("email") %> ' runat="server"></asp:TextBox>
	</td>
</tr>
<tr id="Tr2">
    <td style="width:120px;">
	    学历&nbsp;</td>
    <td  >
        
        <asp:TextBox ID="TB_xl" runat="server" Text='<%# Bind("xl") %> ' Visible="false"></asp:TextBox>
        <asp:DropDownList ID="DDL_xl" runat="server"  >
    <asp:ListItem   Value="">请选择</asp:ListItem>
    <asp:ListItem  Value="本科">本科</asp:ListItem>
    <asp:ListItem  Value="大专">大专</asp:ListItem>
    <asp:ListItem  Value="硕士">硕士</asp:ListItem>
    <asp:ListItem  Value="博士">博士</asp:ListItem>
    <asp:ListItem  Value="高中">高中</asp:ListItem>
    <asp:ListItem  Value="其它">其它</asp:ListItem>
    </asp:DropDownList>
    </td>

    <td style="width:120px;">
    学位&nbsp;</td>
    <td>
	
    <asp:TextBox ID="TB_xw"  Text='<%# Bind("xw") %> ' runat="server"></asp:TextBox>
	</td>
<tr>
    <td style="width:120px;">毕业学校</td>
        <td  >
            
            <asp:TextBox ID="TB_byyx" runat="server" Text='<%# Bind("byyx") %>'></asp:TextBox>
        </td>
    <td style="width:120px;">
	    毕业专业&nbsp;</td>
        <td>
            
            <asp:TextBox ID="TB_byzy" runat="server" Text='<%# Bind("byzy") %>' ></asp:TextBox>
            
        </td>
</tr>
	    

 <tr id="Tr3">
 <td style="width:120px;">来校年月</td>
	 <td  >
         
	<asp:TextBox ID="TB_lxny"  Text='<%# Eval("lxny") %>' runat="server" ></asp:TextBox>
     </td>
	 <td style="width:120px;">
         从教起始年月&nbsp;</td>
	<td  >
    
         <asp:TextBox ID="TB_cjqsny" runat="server" Text='<%# Bind("cjqsny") %>'></asp:TextBox>
	
	</td>
</tr>
<tr id="Tr1">
    <td style="width:120px;">职务&nbsp;</td>
	<td   >
	
    <asp:TextBox ID="TB_zw"  Text='<%# Bind("zw") %> ' runat="server"></asp:TextBox>
	</td>
	<td style="width:120px;">教师资格证</td>
	<td >
	
    <asp:TextBox ID="TB_jszgzs"  Text='<%# Bind("jszgzs") %> ' runat="server"></asp:TextBox>
	</td>
</tr>
<tr id="Tr4">
	<td style="width:120px;">技术职称&nbsp;
    <br /><br />职称专业
    <br /><br />职称时间</td>
	<td  >
	
	<asp:TextBox ID="TB_jszc"  Text='<%# Bind("jszc") %>'   runat="server" ></asp:TextBox><br /><br />
    <asp:TextBox ID="TB_zczy"  Text='<%# Bind("zczy") %>'   runat="server" ></asp:TextBox><br /><br />
    <asp:TextBox ID="TB_zcsj"  Text='<%# Bind("zcsj") %>'   runat="server" ></asp:TextBox>
      
        </td>
    <td style="width:120px;">技能等级&nbsp;
    <br /><br />技能等级工种
    <br /><br />技能等级时间</td>
	<td  >
	
	<asp:TextBox ID="TB_jndj"  Text='<%# Bind("jndj") %>'   runat="server" ></asp:TextBox><br /><br />
    <asp:TextBox ID="TB_jndjgz"  Text='<%# Bind("jndjgz") %>'   runat="server" ></asp:TextBox><br /><br />
    <asp:TextBox ID="TB_jndjsj"  Text='<%# Bind("jndjsj") %>'   runat="server" ></asp:TextBox>
      
        </td>
</tr>
<tr id="Tr5">
    <td style="width:120px;">理论/实训&nbsp;</td>
	<td   >
	
    <asp:TextBox ID="TB_llsx"  Text='<%# Bind("llsx") %> ' runat="server"></asp:TextBox>
	</td>
	<td style="width:120px;">基础/专业</td>
	<td >
	
    <asp:TextBox ID="TB_jczy"  Text='<%# Bind("jczy") %> ' runat="server"></asp:TextBox>
	</td>
</tr>
<tr id="Tr6">
    <td style="width:120px;">教研室（组）&nbsp;</td>
	<td   >
	
    <asp:TextBox ID="TB_jyzmc"  Text='<%# Eval("jyzmc") %> ' runat="server"></asp:TextBox>
	</td>
	<td style="width:120px;">教学及科研专业方向</td>
	<td >
	
    <asp:TextBox ID="TB_jxkyzyfx"  Text='<%# Eval("jxkyzyfx") %> ' runat="server"></asp:TextBox>
	</td>
</tr>
<tr >
	<td colspan="5">
	    <asp:Button ID="LB_update" runat="server"  CssClass="click"
          OnClick="LB_update_Click"  Enabled="true"  Text="更新资料"></asp:Button>
	</td>
</tr>
</EditItemTemplate>

</asp:FormView>
</table>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
        SelectCommand = "SELECT a.yhid,c.yxmc,xm,lxdh,email,yxmc,yhzp,xmpy,xb,sfzjh,csrq,zzmm,mz,xl,xw,byyx,byzy,zw,cjqsny,lxny,jszc,zcsj,zczy,jndj,jndjsj,jndjgz,jszgzs,llsx,jczy,jxkyzyfx,jyzmc FROM yonghqx a left join userdata b on a.yhid=b.yhid left join dm_yuanxi c on a.yxdm=c.yxdm left join dm_jyz d on b.jyzdm=d.jyzdm WHERE a.yhid=@yhid"
        
        >
    <SelectParameters>
        <asp:SessionParameter Name="yhid" SessionField="UserName" />
    </SelectParameters>
    </asp:SqlDataSource>
</div>
</form>
</body>
</html>
