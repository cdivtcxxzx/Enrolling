<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="nradmingl_Default2" %>

<!DOCTYPE html>
<html lang="zh-cn">
<head runat="server">
    <title>宿舍分配-预分配</title>
      <meta charset="utf-8">
  
  <meta name="renderer" content="webkit">
  <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
  <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
  <link rel="stylesheet" href="//res.layui.com/layui/build/css/layui.css"  media="all">
    
     <!--引用ＬＡＹＵＩ前端必须ＣＳＳ OVER-->
    

</head>
<body>
    <form id="form1" class="layui-form" runat="server">
   
  



      <div class="layui-form-item">
    <div class="layui-inline">
      <label class="layui-form-label">分组选择框</label>
      <div class="layui-input-inline">
        <select name="quiz">
          <option value="">请选择问题</option>
          <optgroup label="城市记忆">
            <option value="你工作的第一个城市">你工作的第一个城市</option>
          </optgroup>
          <optgroup label="学生时代">
            <option value="你的工号">你的工号</option>
            <option value="你最喜欢的老师">你最喜欢的老师</option>
          </optgroup>
        </select><div class="layui-unselect layui-form-select"><div class="layui-select-title"><input type="text" placeholder="请选择问题" value="" readonly="" class="layui-input layui-unselect"><i class="layui-edge"></i></div><dl class="layui-anim layui-anim-upbit layui-select-group"><dt>城市记忆</dt><dd lay-value="你工作的第一个城市" class="">你工作的第一个城市</dd><dt>学生时代</dt><dd lay-value="你的工号" class="">你的工号</dd><dd lay-value="你最喜欢的老师" class="">你最喜欢的老师</dd></dl></div>
      </div>
    </div>
    <div class="layui-inline">
      <label class="layui-form-label">搜索选择框</label>
      <div class="layui-input-inline">
        <select name="modules" lay-verify="required" lay-search="">
          <option value="">直接选择或搜索选择</option>
          <option value="1">layer</option>
          <option value="2">form</option>
          <option value="3">layim</option>
          <option value="4">element</option>
          <option value="5">laytpl</option>
          <option value="6">upload</option>
          <option value="7">laydate</option>
          <option value="8">laypage</option>
          <option value="9">flow</option>
          <option value="10">util</option>
          <option value="11">code</option>
          <option value="12">tree</option>
          <option value="13">layedit</option>
          <option value="14">nav</option>
          <option value="15">tab</option>
          <option value="16">table</option>
          <option value="17">select</option>
          <option value="18">checkbox</option>
          <option value="19">switch</option>
          <option value="20">radio</option>
        </select><div class="layui-unselect layui-form-select"><div class="layui-select-title"><input type="text" placeholder="直接选择或搜索选择" value="" class="layui-input layui-unselect"><i class="layui-edge"></i></div><dl class="layui-anim layui-anim-upbit"><dd lay-value="1" class="">layer</dd><dd lay-value="2" class="">form</dd><dd lay-value="3" class="">layim</dd><dd lay-value="4" class="">element</dd><dd lay-value="5" class="">laytpl</dd><dd lay-value="6" class="">upload</dd><dd lay-value="7" class="">laydate</dd><dd lay-value="8" class="">laypage</dd><dd lay-value="9" class="">flow</dd><dd lay-value="10" class="">util</dd><dd lay-value="11" class="">code</dd><dd lay-value="12" class="">tree</dd><dd lay-value="13" class="">layedit</dd><dd lay-value="14" class="">nav</dd><dd lay-value="15" class="">tab</dd><dd lay-value="16" class="">table</dd><dd lay-value="17" class="">select</dd><dd lay-value="18" class="">checkbox</dd><dd lay-value="19" class="">switch</dd><dd lay-value="20" class="">radio</dd></dl></div>
      </div>
    </div>
  </div>














    

      
       
    </form>  <script src="//res.layui.com/layui/build/layui.js" charset="utf-8"></script>
</body>
</html>
