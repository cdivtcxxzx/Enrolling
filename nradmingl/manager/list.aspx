<%@ Page Language="C#" AutoEventWireup="true" CodeFile="list.aspx.cs" Inherits="nradmingl_manager_list" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ul>
            <li><a href="college.aspx">学院数据</a></li>
            <li><a href="spe.aspx">专业数据</a></li>
            <li><a href="class.aspx">班级数据</a></li>
            <li><a href="counseller.aspx">班主任数据</a></li>
            <li><a href="staff.aspx">迎新员工数据</a></li>
            <li><a href="base_stu.aspx">学生基本数据</a></li>
        </ul>
        <hr />
        <ul>
            <li><a href="batch.aspx">迎新批次</a></li>
            <li><a href="operate.aspx">迎新操作</a></li>
                        <li><a href="affair.aspx">迎新事务</a></li>
            <li><a href="staff_affair_auth.aspx">迎新员工迎新事务授权数据</a></li>
            <li><a href="staff_affair_auth_scope.aspx">迎新员工迎新事务授权操作范围数据</a></li>
        </ul>
    </div>
    </form>
</body>
</html>
