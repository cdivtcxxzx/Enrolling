<%@ Page Language="C#" AutoEventWireup="true" CodeFile="newyear.aspx.cs" Inherits="nradmingl_newyear" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    
    <meta charset="utf-8">
    <title>成都工业职业技术学院2016年新年致辞</title>
   
    <meta name="renderer" content="webkit">
		<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
		<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
		<meta name="apple-mobile-web-app-status-bar-style" content="black">
		<meta name="apple-mobile-web-app-capable" content="yes">
		<meta name="format-detection" content="telephone=no">

    <!--引用ＬＡＹＵＩ前端必须ＣＳＳ-->
        <link rel="stylesheet" href="plugins/layui/css/layui.css" media="all" />
		<link rel="stylesheet" href="plugins/global.css" media="all" />
         <!--引用ＬＡＹＵＩ前端字体图标ＣＳＳ-->
		<link rel="stylesheet" href="plugins/font-awesome/css/font-awesome.min.css" />
         <!--引用ＬＡＹＵＩ前端表格ＣＳＳ,使用表格时才引用-->
		<link rel="stylesheet" href="plugins/table.css" />
    
     <!--引用ＬＡＹＵＩ前端必须ＣＳＳ OVER-->
   
   
</head>
<body style="background:#F8F3BB;"> 
<!--展示响应式CSS块-->
 <style>
         
.responsive-utilities-test .col-xs-6 {
    margin-bottom: 10px;
}
.responsive-utilities-test .col-xs-6 {
    margin-bottom: 10px;
}
.col-xs-6 {
    width: 50%;
    float:left;
}
.visible-on .col-xs-6 .hidden-xs, .visible-on .col-xs-6 .hidden-sm, .visible-on .col-xs-6 .hidden-md, .visible-on .col-xs-6 .hidden-lg, .hidden-on .col-xs-6 .hidden-xs, .hidden-on .col-xs-6 .hidden-sm, .hidden-on .col-xs-6 .hidden-md, .hidden-on .col-xs-6 .hidden-lg {
    color: #999;
    border: 1px solid #ddd;
}
     .visible-on .col-xs-6 .visible-xs-block, .visible-on .col-xs-6 .visible-sm-block, .visible-on .col-xs-6 .visible-md-block, .visible-on .col-xs-6 .visible-lg-block, .hidden-on .col-xs-6 .visible-xs-block, .hidden-on .col-xs-6 .visible-sm-block, .hidden-on .col-xs-6 .visible-md-block, .hidden-on .col-xs-6 .visible-lg-block {
    color: #468847;
    background-color: #dff0d8;
    border: 1px solid #d6e9c6;
}
.responsive-utilities-test span {
    display: block;
    padding: 15px 10px;
    font-size: 14px;
    font-weight: 700;
    line-height: 1.1;
    text-align: center;
    border-radius: 4px;
}

/*新年CSS*/
#top
{
    width:100%;
    background:url(images/top50.png) #CE141F;
    background-repeat:repeat-x;
    height:60px;
    }
    #title
    {
        margin-top:20px;
        width:100%;
        text-align:center;
    }
    
    
    
    .stripes {
			height: 250px;
			width: 98%;
		
			
			-webkit-background-size: 80px 80px;
			-moz-background-size:80px 80px;
			background-size: 80px 80px; /* Controls the size of the stripes */
			
			
		}.vertical 
{
    height:96%;width:60%;margin-left:20%; float:left;
			background-color: #F8F3BB;/*#F8F3BB #f90*/
			background-image: -webkit-gradient(linear, 0 0, 100% 0, color-stop(.1, rgba(255, 255, 255, .1)), color-stop(.2, transparent), to(transparent));
			background-image: -moz-linear-gradient(0deg, rgba(255, 255, 255, .2) 50%, transparent 50%, transparent);
			background-image: -o-linear-gradient(0deg, rgba(255, 255, 255, .2) 50%, transparent 50%, transparent);
			background-image: linear-gradient(0deg, rgba(255, 255, 255, .2) 50%, transparent 50%, transparent);
		}        .content
        {
            width:90%;
            margin-left:5%;
        font-family: "Microsoft YaHei", "Noto Sans Myanmar", 微软雅黑, SimHei, helvetica, arial, verdana, tahoma, sans-serif;
    font-size: 20px;
    line-height: 2.0;
    color: #333;
            }
            .con2
            {margin-top:16px;
                font-family: "Microsoft YaHei", "Noto Sans Myanmar", 微软雅黑, SimHei, helvetica, arial, verdana, tahoma, sans-serif;
    font-size: 20px;}
    .con2 a{color:White;}
            .con1{width:60%;margin-left:20%; float:left;}
            #bottom{margin-top:50px;margin-left:50px;float:left;}
           @media screen and (max-width: 990px) 
           {
               .vertical 
{
    height:96%;width:94%!important;margin-left:3%; float:left;
   
           
}
 .con1{width:94%!important;margin-left:3%; float:left; }
 #bottom{margin-top:50px;margin-left:0px;float:left; width:80px;heigt:80px;}
           } 
           @media screen and (max-width: 500px) 
           {
               #bottom{display:none;}
                #title img{width:90%;}
                
           }
           
     </style>
     <!--展示响应式CSS块over-->
     <!--顶部-->
     <div id="top"><span class="con2" style="float:left;margin-left:30px; "><a href="http://www.cdivtc.com.cn/">返回学院网站</a></span>&nbsp;</div>
     
     <div class="admin-main vertical stripes" style="" >
     <!--顶部提示及导航-->
    		<div id="title"><img src="images/yeartitle2.png"  /></div>
 <!--顶部提示及导航OVER-->
 <div class="content" style="margin-bottom:20px;margin-top:16px;">

 <p>老师们、同学们：</p>
  <p style="text-indent:2em;">春华秋实，岁月流金。在这辞旧迎新、继往开来的美好时刻，我们谨代表学院，向一年来在教学、科研、管理、服务等各个领域心系学生、辛勤耕耘的广大教职员工及离退休老同志表示诚挚的问候！向朝气蓬勃、奋发向上的全体同学致以亲切的祝福！向一年来关心、支持、帮助学院不断发展的各级领导、各界友人致以衷心的感谢！</p>
    <p style="text-indent:2em;">众志凝心佳绩创，全员聚力硕果丰。2016年，是学院贯彻实施“十三五规划”的开局之年，也是学院创建人才培养合格高职的开局之年。学院在各级领导的关心支持下，学院党委的正确领导下，在全院师生的共同努力下，对标先进，开拓进取，各项事业取得良好进展。</p>
<p style="text-indent:2em;"><b>这一年，我们建立现代高校制度，积极推进现代治理。</b>以《学院章程》为核心制定了覆盖教学管理、学术科研等10个领域185项制度；科学编制了“十三五”发展规划及人才培养、队伍建设、科技创新等7个专项规划；《产业转型升级背景下高职院校现代治理改革试点》成功入选第三批四川省教育综合改革试点项目。</p>
<p style="text-indent:2em;"><b>这一年，我们创新人才培养模式，全创工作成效凸显。  </b>全面启动合格评估工作；规划组建了8大专业集群；修订了12个专业人才培养方案，新建报关与国际货运等新专业7个，新申报轨道机车等专业3个、四川省高职创新发展行动计划项目9个；推进市属高校市级质量工程立项项目，3个市级重点专业和2个市级重点实验室项目顺利通过中期检查评估工作；《基于国际合作的职业教育质量提升实践研究》获得成都市优秀教学成果一等奖；学院“以产业链为导向的集群式技术技能人才培养模式”入选成都市、四川省全面创新改革经验事项。
</p>
<p style="text-indent:2em;"><b>这一年，我们提升师资队伍素养，对外合作取得突破。</b>学院教师入选2016年“成都优秀人才培养对象”，获得四川省高等职业院校2016年第二届青年体育教师说课比赛一等奖等各级各类大赛奖项26个；学院成为“中德（四川成都）创新产业合作职业教育平台”建设牵头单位；与一汽大众签署校企合作协议；与同济大学共同成立“中德（四川成都）跨企业培训中心”。学院当选为“教育部中国职业技术教育学会培训工作委员会”主任单位、“四川省中德创新产业合作促进会”副会长单位、“成都工业人才促进会”执行会长单位、成都市工业人才培训基地、“长风模式会计专业全国免费试点”首批院校。</p>
<p style="text-indent:2em;"><b>这一年，我们加快科技成果转化，双创工作成效明显。</b>学院职业教育研究所被教育部职业教育中心所确定为研究基地；成功注册国家科技计划项目申报系统、四川省科技厅项目管理系统，构建了部省市三级科技平台；在建先进涂层中心、物流工程长风教育职业研究院四川分院、建筑材料质量检测中心3个平台。学院参加全国机器人锦标赛暨第七届国际仿人机器人奥林匹克大赛、全国挑战杯—彩虹人生创新创业大赛、中国汽车工程学会巴哈大赛等国家级赛事共获奖21个，其中嵌入式全自动智能电饭煲在四川省经信委组织的创新创业大赛中，荣获创客组“2016年度四川省十大创客之星”荣誉称号。</p>
<p style="text-indent:2em;"><b>这一年，我们创建文化育人基地，培育大国工匠精神。</b>积极开展以工业文化传承为主线的学校文化建设，构建“艺术与人文社会科学课程体系”；汽车类“特色院校”建设任务顺利通过市局验收；“三馆一区”规划布局初步建构。学院被中共四川省委宣传部和四川省社科联认定为第六批“四川省社会科学普及基地”，荣获2015年度“成都市文明单位”称号。</p>
<p style="text-indent:2em;"><b>这一年，我们实施职教帮扶引领，服务能力稳步提升。</b>学院获批高等教育自学考试主考学院资格；蒲江校区建设成为“中国发展研究基金会‘赢未来计划’试点学校”、“四川省现代学徒制试点单位”、“AHK德国双元制职业教育联盟单位”；对口帮扶精准扶贫石渠县，建立“成都工业职业技术学院精准扶贫技术技能人才培训中心”。</p>
<p style="text-indent:2em;"><b>这一年，我们坚持从严治党，切实加强党的建设。</b>深入开展“两学一做”学习教育，受到市委督导组高度评价，特色项目入选市委“两学一做”学习教育简报；不断完善党建制度体系，制订思想政治建设等制度40多项；严格落实党建责任，建立学院党委、系部党总支、直属党支部抓党建定期评议考核机制；加强基层组织建设，推进支部“三分类三升级”活动;加强党风廉政建设和反腐败工作，认真履行“一岗双责”;建立了“一站二微两窗”的宣传平台;加强群团工作和统战工作;共青团建设工作受到国家副主席李源潮和团中央的充分肯定。</p>
<p style="text-indent:2em;">踏石留印，抓铁有痕。所有这些成绩的取得，无不凝聚着全院教职员工的辛勤劳动和艰苦付出。在此，我们谨代表学院向全体教职员工，致以崇高的敬意和诚挚的谢意！</p>
<p style="text-indent:2em;">老师们、同学们！拼搏蕴含机遇，创新成就伟业。回顾过去，我们感慨万千；展望未来，我们更当勤勉。2017年，是学院进一步深化改革、创新发展的关键一年，也是充满机遇、充满挑战的一年。面对高职教育千帆竞发、百舸争流的态势，我们要众志成城、奋楫直行、埋头苦干、锐意进取，立足天府新区，服务全域成都，为助力教育供给侧结构性改革，为谱写学院建设发展新篇章，作出积极努力！为学院三步发展、跨越发展贡献更大力量！</p>
<p style="text-indent:2em;">新的一年开启新的希望，新的征程承载新的梦想。在新年的钟声里，让我们衷心祝愿伟大祖国国泰民安、繁荣昌盛!衷心祝愿学院发展蒸蒸日上、兴旺发达!衷心祝愿全院师生心想事成、幸福安康!</p>
<p>&nbsp;</p>
 </div>
  </div>
  <div class="con1" style=" background:url(images/yearbot.png) ;background-repeat:no-repeat; background-position: 10% 90%;" >
  <div class="content"  >
<div id="bottom"  class＝"hidden-xs">


</div>
<div style="float:right"  ><p>&nbsp;</p><p>&nbsp;</p><p style=" text-align:right">党委书记:&nbsp;&nbsp;<img style="margin-top:-10px;" src="images/yk2.png" /></p>
<p style=" text-align:right">院&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;长:&nbsp;&nbsp;<img style="margin-top:30px;margin-right:10px;" src="images/kl.png" /></p></div>

<p>&nbsp;</p>
<p>&nbsp;</p>
</div>
</div>
  <!--标签框架-->
		
       

       

         <div style="height:50px; float:left;width:100%;">&nbsp;</div>
        


</body>
</html>
