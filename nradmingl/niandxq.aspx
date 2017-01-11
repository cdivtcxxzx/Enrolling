<%@ Page Language="C#" AutoEventWireup="true" CodeFile="niandxq.aspx.cs" Inherits="admin_niandxq" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<title>年度学期管理</title>
<link runat="server" id="webcss" type="text/css" href="style.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../files/common/jquery.js"></script>
<script type="text/javascript" src="include/common.js"></script>

 

  


</head>
<body id="C_User">
    <form id="form1" runat="server">
    <table class="table subsubmenu">
	<thead>
	<tr>
		<td>
			<a href="?action=manage">当前位置&gt;&gt;&nbsp;教学评价系统&gt;&gt;&nbsp;年度学期维护</a>&nbsp;
			
			
		</td>
		
		<td style="text-align:right">输入年度:<asp:DropDownList ID="DropDownList1" 
                runat="server">
            </asp:DropDownList>
            &nbsp;学期<asp:TextBox ID="searchtext0" 
                runat="server" name="1" txttop="txttop" title="根据输入的部门名称添加" Width="161px"></asp:TextBox>
           <asp:Button 
                ID="Button1" runat="server" Text="添加新的年度学期" CssClass="click" 
                onclick="Search_Onclick" />
        </td>
	</tr>
  </thead>
</table>
 
  <br />
 
<div id="PanelDefault">
<asp:ScriptManager ID="sm1" runat="server" />
<asp:UpdatePanel ID="up1" runat="server" ><ContentTemplate >



    <asp:GridView ID="GridView1" CssClass="table" runat="server" 
        AutoGenerateColumns="False" EmptyDataText="暂无年度学期信息"  DataKeyNames="id"  DataSourceID="SqlDataSource1">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="编号" InsertVisible="False" 
                ReadOnly="True" SortExpression="id" />
            <asp:BoundField DataField="nd" HeaderText="年度" SortExpression="nd" />
            <asp:BoundField DataField="xueqi" HeaderText="学期" SortExpression="xueqi" />

           
            <asp:CheckBoxField DataField="zt" HeaderText="是否为默认年度学期" SortExpression="zt" />
            <asp:CommandField HeaderText="管理操作" ShowDeleteButton="True" 
                ShowEditButton="True" />
        </Columns>
    </asp:GridView>



    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
        
        SelectCommand="SELECT * FROM [DM_ndxueqi] WHERE ([sys] = '教学评价系统') ORDER BY [id], [nd], [xueqi],[zt]"
        UpdateCommand ="UPDATE DM_ndxueqi SET nd = @nd, xueqi = @xueqi,zt=@zt WHERE (id = @id)" 
        DeleteCommand="DELETE FROM  DM_ndxueqi WHERE (id = @id)" >
        
        <UpdateParameters>
            <asp:Parameter Name="nd" Type="Int32" />
            <asp:Parameter Name="xueqi"  />
            <asp:Parameter Name="id" />
            <asp:Parameter Name="zt" />
        </UpdateParameters>
    </asp:SqlDataSource>



    </ContentTemplate></asp:UpdatePanel>
   </div>
    </form>
    
    
    <!--提示信息段-->
    <input id="alertMessage" runat="server"  style="font-size:Large;" type="hidden" />
   
    <script>
        // skin demo
        (function () {
            var _skin, _jQuery;
            var _search = window.location.search;
            if (_search) {
                _skin = _search.split('demoSkin=')[1];
                _jQuery = _search.indexOf('jQuery=true') !== -1;
                if (_jQuery) document.write('<scr' + 'ipt src="artDialog/jquery-1.6.2.min.js"></sc' + 'ript>');
            };

            document.write('<scr' + 'ipt src="artDialog/artDialog.source.js?skin=' + (_skin || 'blue') + '"></sc' + 'ript>');
            window._isDemoSkin = !!_skin;
            window.onload = function () {
                _skin && art.dialog({
                    content: '欢迎使用"artDialog"对话框组件!',
                    icon: 'succeed',
                    fixed: true
                }, function () {
                    this.title('2秒后自动关闭').lock().time(0);
                    return false;
                });
            };
        })();
</script>

<script  type="text/javascript" src="/admin/artDialog/plugins/iframeTools.source.js"></script>

<script type="text/javascript">
    //var ckeditor;
    //$(function(){
    //    ckeditor = CKEDITOR.replace("=txtBoxNote.ClientID ");
    // });
    if (document.all("alertMessage").value != "") {
        showts(document.all("alertMessage").value);
        document.all("alertMessage").value = "";
    }
    function showurl(url) {
        art.dialog.open(url, { title: '数据详情', left: '270px', width: '700px', height: '80%', fixed: true });
    }
    function showts(content) {
        var timer;
        art.dialog({
            id: 'xxtszm',
            content: content,
            init: function () {
                var that = this, i = 20;
                var fn = function () {
                    that.title('信息提示!' + i + '秒后自动关闭');
                    !i && that.close();
                    i--;
                };
                timer = setInterval(fn, 1000);
                fn();
            },
            close: function () {
                clearInterval(timer);
            }
        }).show();
    }

    function Image10_onclick() {

    }

</script>
<!--提示信息结束，后台调用，this.alertMessage="<span style=\"font-size:Large;\"><font color=red>对不起，你的结束时间格式不正确，请检查！</font></span>";-->
  
</body>
</html>