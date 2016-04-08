﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using Lte.Domain.LinqToCsv.Context;
using Lte.Domain.LinqToCsv.Description;
using Lte.Parameters.Entities;
using Lte.Parameters.MockOperations;
using NUnit.Framework;

namespace Lte.Parameters.Test.Entities
{
    [TestFixture]
    public class AlarmStatCsvTest
    { 
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            AlarmMapperService.MapAlarms();
        }

        [Test]
        public void Test_ReadOneLine()
        {
            var fileDescriptionNamesUs = CsvFileDescription.CommaDescription;

            const string testInput = @"根源告警标识,确认状态,告警级别,网元,网元内定位,告警码,发生时间,网元类型,告警类型,告警原因,附加文本,ADMC告警,告警恢复时间,重复计数,告警对象类型,告警对象DN,单板类型,告警对象ID,站点名称(局向),站点ID(局向),告警对象名称,产品,告警标识,影响网元,影响网元内定位,告警修改时间,附加内容,确认/反确认用户,确认/反确认系统,告警确认/反确认时间,告警确认/反确认信息,清除用户,清除系统,恢复方式,告警注释,注释用户,注释系统,注释时间,告警编号,网元IP,链路,网元分组,网元代理,系统类型,持续时间(hh:mm:ss),关联业务,产生方式,门限任务信息,调测状态,标准告警码,清除信息
,未确认,主要,西樵电信机房LBBU18(550974),""地面资源(MO SDR)=1,机架(MO SDR)=1,机框(MO SDR)=1,单板(MO SDR)=1"",X2断链告警(198094421),2015/8/18 13:22,管理网元(MO SDR),通信告警,1.SCTP偶联断； 2.X2AP建立失败（协商失败或基站无小区）。,单板序列号: 281713201433; L eNBId:550974,否,2015/8/18 13:32,,SDR,""SubNetwork=440610,MEID=550974,TransportNetwork=1"",CCC,550974,西樵电信机房LBBU18,550974,西樵电信机房LBBU18,LTE FDD,148,,,,站点ID(局向) : 550974; 站点名称(局向) : 西樵电信机房LBBU18; 告警对象类型 : SDR; 告警对象ID : 550974; 告警对象名称 : 西樵电信机房LBBU18; 单板类型 : CCC; ,,,,,,,正常恢复,,,,,1.43923E+12,8.142.53.170,,""广东电信LTE_OMMB5,佛山市南海区3(440610)"",广东电信LTE_OMMB5,LTE FDD业务告警(20428),0:09:43,,,,,通信子系统故障(306),";

            IEnumerable<AlarmStatCsv> stats = CsvContext.ReadString<AlarmStatCsv>(testInput, fileDescriptionNamesUs).ToList();
            Assert.IsNotNull(stats);
            Assert.AreEqual(stats.Count(), 1);
            var stat = stats.ElementAt(0);
            Assert.AreEqual(stat.AlarmCodeDescription, "X2断链告警(198094421)");
            Assert.AreEqual(stat.AlarmLevelDescription, "主要");
            Assert.AreEqual(stat.NetworkItem, "西樵电信机房LBBU18(550974)");
            Assert.AreEqual(stat.Position, "地面资源(MO SDR)=1,机架(MO SDR)=1,机框(MO SDR)=1,单板(MO SDR)=1");
        }

        [TestCase(@"根源告警标识,确认状态,告警级别,网元,网元内定位,告警码,发生时间,网元类型,告警类型,告警原因,附加文本,ADMC告警,告警恢复时间,重复计数,告警对象类型,告警对象DN,单板类型,告警对象ID,站点名称(局向),站点ID(局向),告警对象名称,产品,告警标识,影响网元,影响网元内定位,告警修改时间,附加内容,确认/反确认用户,确认/反确认系统,告警确认/反确认时间,告警确认/反确认信息,清除用户,清除系统,恢复方式,告警注释,注释用户,注释系统,注释时间,告警编号,网元IP,链路,网元分组,网元代理,系统类型,持续时间(hh:mm:ss),关联业务,产生方式,门限任务信息,调测状态,标准告警码,清除信息
,未确认,主要,西樵电信机房LBBU18(550974),""地面资源(MO SDR)=1,机架(MO SDR)=1,机框(MO SDR)=1,单板(MO SDR)=1"",X2断链告警(198094421),2015/8/18 13:22,管理网元(MO SDR),通信告警,1.SCTP偶联断； 2.X2AP建立失败（协商失败或基站无小区）。,单板序列号: 281713201433; L eNBId:550974,否,2015/8/18 13:32,,SDR,""SubNetwork=440610,MEID=550974,TransportNetwork=1"",CCC,550974,西樵电信机房LBBU18,550974,西樵电信机房LBBU18,LTE FDD,148,,,,站点ID(局向) : 550974; 站点名称(局向) : 西樵电信机房LBBU18; 告警对象类型 : SDR; 告警对象ID : 550974; 告警对象名称 : 西樵电信机房LBBU18; 单板类型 : CCC; ,,,,,,,正常恢复,,,,,1.43923E+12,8.142.53.170,,""广东电信LTE_OMMB5,佛山市南海区3(440610)"",广东电信LTE_OMMB5,LTE FDD业务告警(20428),0:09:43,,,,,通信子系统故障(306),
,未确认,主要,西樵电信机房LBBU25(551424),""地面资源(MO SDR)=1,机架(MO SDR)=1,机框(MO SDR)=1,单板(MO SDR)=1"",X2断链告警(198094421),2015/8/18 13:22,管理网元(MO SDR),通信告警,1.SCTP偶联断； 2.X2AP建立失败（协商失败或基站无小区）。,单板序列号: 283448400911; L eNBId:551424,否,2015/8/18 13:32,,SDR,""SubNetwork=440610,MEID=551424,TransportNetwork=1"",CCC,551424,西樵电信机房LBBU25,551424,西樵电信机房LBBU25,LTE FDD,128,,,,站点ID(局向) : 551424; 站点名称(局向) : 西樵电信机房LBBU25; 告警对象类型 : SDR; 告警对象ID : 551424; 告警对象名称 : 西樵电信机房LBBU25; 单板类型 : CCC; ,,,,,,,正常恢复,,,,,1.43923E+12,8.142.85.142,,""广东电信LTE_OMMB5,佛山市南海区3(440610)"",广东电信LTE_OMMB5,LTE FDD业务告警(20428),0:09:43,,,,,通信子系统故障(306),
,未确认,主要,西樵职校(503120),""地面资源(MO SDR)=1,机架(MO SDR)=1,机框(MO SDR)=1,单板(MO SDR)=1"",X2断链告警(198094421),2015/8/18 13:22,管理网元(MO SDR),通信告警,1.SCTP偶联断； 2.X2AP建立失败（协商失败或基站无小区）。,单板序列号: 274912500754; L eNBId:503120,否,2015/8/18 13:32,,SDR,""SubNetwork=440604,MEID=503120,TransportNetwork=1"",CCC,503120,西樵职校,503120,西樵职校,LTE FDD,164,,,,站点ID(局向) : 503120; 站点名称(局向) : 西樵职校; 告警对象类型 : SDR; 告警对象ID : 503120; 告警对象名称 : 西樵职校; 单板类型 : CCC; ,,,,,,,正常恢复,,,,,1.43923E+12,8.142.53.137,,""广东电信LTE_OMMB5,佛山市南海区2(440604)"",广东电信LTE_OMMB5,LTE FDD业务告警(20428),0:09:45,,,,,通信子系统故障(306),
,未确认,主要,西樵民乐(502926),""地面资源(MO SDR)=1,机架(MO SDR)=1,机框(MO SDR)=1,单板(MO SDR)=1"",X2断链告警(198094421),2015/8/18 13:22,管理网元(MO SDR),通信告警,1.SCTP偶联断； 2.X2AP建立失败（协商失败或基站无小区）。,单板序列号: 274747400116; L eNBId:502926,否,2015/8/18 13:32,,SDR,""SubNetwork=440604,MEID=502926,TransportNetwork=1"",CCC,502926,西樵民乐,502926,西樵民乐,LTE FDD,93,,,,站点ID(局向) : 502926; 站点名称(局向) : 西樵民乐; 告警对象类型 : SDR; 告警对象ID : 502926; 告警对象名称 : 西樵民乐; 单板类型 : CCC; ,,,,,,,正常恢复,,,,,1.43923E+12,8.142.53.136,,""广东电信LTE_OMMB5,佛山市南海区2(440604)"",广东电信LTE_OMMB5,LTE FDD业务告警(20428),0:09:45,,,,,通信子系统故障(306),
,未确认,主要,民乐向阳(502934),""地面资源(MO SDR)=1,机架(MO SDR)=1,机框(MO SDR)=1,单板(MO SDR)=1"",X2断链告警(198094421),2015/8/18 13:22,管理网元(MO SDR),通信告警,1.SCTP偶联断； 2.X2AP建立失败（协商失败或基站无小区）。,单板序列号: 274875701398; L eNBId:502934,否,2015/8/18 13:32,,SDR,""SubNetwork=440604,MEID=502934,TransportNetwork=1"",CCC,502934,民乐向阳,502934,民乐向阳,LTE FDD,110,,,,站点ID(局向) : 502934; 站点名称(局向) : 民乐向阳; 告警对象类型 : SDR; 告警对象ID : 502934; 告警对象名称 : 民乐向阳; 单板类型 : CCC; ,,,,,,,正常恢复,,,,,1.43923E+12,8.142.53.130,,""广东电信LTE_OMMB5,佛山市南海区2(440604)"",广东电信LTE_OMMB5,LTE FDD业务告警(20428),0:09:45,,,,,通信子系统故障(306),
,未确认,主要,西樵儒溪接入机房LBBU1(551057),""地面资源(MO SDR)=1,机架(MO SDR)=1,机框(MO SDR)=1,单板(MO SDR)=1"",X2断链告警(198094421),2015/8/18 13:22,管理网元(MO SDR),通信告警,1.SCTP偶联断； 2.X2AP建立失败（协商失败或基站无小区）。,单板序列号: 282646400330; L eNBId:551057,否,2015/8/18 13:32,,SDR,""SubNetwork=440610,MEID=551057,TransportNetwork=1"",CCC,551057,西樵儒溪接入机房LBBU1,551057,西樵儒溪接入机房LBBU1,LTE FDD,149,,,,站点ID(局向) : 551057; 站点名称(局向) : 西樵儒溪接入机房LBBU1; 告警对象类型 : SDR; 告警对象ID : 551057; 告警对象名称 : 西樵儒溪接入机房LBBU1; 单板类型 : CCC; ,,,,,,,正常恢复,,,,,1.43923E+12,8.142.85.130,,""广东电信LTE_OMMB5,佛山市南海区3(440610)"",广东电信LTE_OMMB5,LTE FDD业务告警(20428),0:09:45,,,,,通信子系统故障(306),
,未确认,主要,西樵简村(503193),""地面资源(MO SDR)=1,机架(MO SDR)=1,机框(MO SDR)=1,单板(MO SDR)=1"",X2断链告警(198094421),2015/8/18 13:22,管理网元(MO SDR),通信告警,1.SCTP偶联断； 2.X2AP建立失败（协商失败或基站无小区）。,单板序列号: 274912500913; L eNBId:503193,否,2015/8/18 13:32,,SDR,""SubNetwork=440604,MEID=503193,TransportNetwork=1"",CCC,503193,西樵简村,503193,西樵简村,LTE FDD,224,,,,站点ID(局向) : 503193; 站点名称(局向) : 西樵简村; 告警对象类型 : SDR; 告警对象ID : 503193; 告警对象名称 : 西樵简村; 单板类型 : CCC; ,,,,,,,正常恢复,,,,,1.43923E+12,8.142.53.133,,""广东电信LTE_OMMB5,佛山市南海区2(440604)"",广东电信LTE_OMMB5,LTE FDD业务告警(20428),0:09:46,,,,,通信子系统故障(306),
,未确认,主要,丹灶建行(502442),""地面资源(MO SDR)=1,机架(MO SDR)=1,机框(MO SDR)=1,单板(MO SDR)=1"",X2断链告警(198094421),2015/8/18 13:19,管理网元(MO SDR),通信告警,1.SCTP偶联断； 2.X2AP建立失败（协商失败或基站无小区）。,单板序列号: 274030500557; L eNBId:502442,否,2015/8/18 13:25,,SDR,""SubNetwork=440603,MEID=502442,TransportNetwork=1"",CCC,502442,丹灶建行,502442,丹灶建行,LTE FDD,430,,,,站点ID(局向) : 502442; 站点名称(局向) : 丹灶建行; 告警对象类型 : SDR; 告警对象ID : 502442; 告警对象名称 : 丹灶建行; 单板类型 : CCC; ,,,,,,,正常恢复,,,,,1.43923E+12,8.142.6.11,,""广东电信LTE_OMMB5,佛山市南海区1(440603)"",广东电信LTE_OMMB5,LTE FDD业务告警(20428),0:05:51,,,,,通信子系统故障(306),",
            8)]
        [TestCase(@"根源告警标识,确认状态,告警级别,网元,网元内定位,告警码,发生时间,网元类型,告警类型,告警原因,附加文本,ADMC告警,告警恢复时间,重复计数,告警对象类型,告警对象DN,单板类型,告警对象ID,站点名称(局向),站点ID(局向),告警对象名称,产品,告警标识,影响网元,影响网元内定位,告警修改时间,附加内容,确认/反确认用户,确认/反确认系统,告警确认/反确认时间,告警确认/反确认信息,清除用户,清除系统,恢复方式,告警注释,注释用户,注释系统,注释时间,告警编号,网元IP,链路,网元分组,网元代理,系统类型,持续时间(hh:mm:ss),关联业务,产生方式,门限任务信息,调测状态,标准告警码,清除信息
"""",未确认,主要,环市镇安中一(501927),""地面资源(MO SDR)=1,机架(MO SDR)=52,机框(MO SDR)=1,单板(MO SDR)=1"",RRU链路断(198097605),2015-10-15 10:35:43,管理网元(MO SDR),处理错误告警,1. RRU运行异常。 2. RRU与主控板之间的通讯链路故障。,""L eNBId:501927; 拓扑: 光接口板=(1,1,6), 光接口板光口号=1, 主链级联号=1"",否,2015-10-15 10:36:03,"""",RU,""SubNetwork=440601,MEID=501927,Equipment=1,Rack=52,SubRack=1,Slot=1,PlugInUnit=1"",R8862A S2100(A6A),52,环市镇安中一,501927,环市镇安中一,LTE FDD,7,"""","""","""",站点ID(局向) : 501927; 站点名称(局向) : 环市镇安中一; 告警对象类型 : RU; 告警对象ID : 52; 告警对象名称 : 环市镇安中一; 单板类型 : R8862A S2100(A6A); ,"""","""","""","""","""","""",调测恢复,"""","""","""","""",1439235125714,8.142.3.168,"""",""广东电信LTE_OMMB3,佛山市禅城区1(440601)"",广东电信LTE_OMMB3,平台告警(20420),00:00:20,"""","""","""","""",不可用(14),""""
"""",未确认,次要,环市镇安中一(501927),""地面资源(MO SDR)=1,机架(MO SDR)=1,机框(MO SDR)=1,单板(MO SDR)=6"",光口接收链路故障(198098319),2015-10-15 10:35:43,管理网元(MO SDR),通信告警,1. 光纤/电缆损坏； 2. 光纤/电缆端面污染； 3. 本端或对端光/电模块或光纤/电缆没插好； 4. 对端设备的光/电模块损坏； 5. 光纤实际长度大于光模块支持的长度； 6. 本端、对端的光模块速率不匹配； 7. 对端设备工作异常。,光口1; 光口未接收到光信号; 单板序列号: 277707000044; L eNBId:501927,否,2015-10-15 10:36:03,"""",SDR,""SubNetwork=440601,MEID=501927,Equipment=1,Rack=1,SubRack=1,Slot=6,PlugInUnit=1,SdrDeviceGroup=1,FiberDeviceSet=1,FiberDevice=1"",BPL1,501927,环市镇安中一,501927,环市镇安中一,LTE FDD,4,"""","""","""",站点ID(局向) : 501927; 站点名称(局向) : 环市镇安中一; 告警对象类型 : SDR; 告警对象ID : 501927; 告警对象名称 : 环市镇安中一; 单板类型 : BPL1; ,"""","""","""","""","""","""",调测恢复,"""","""","""","""",1439235125716,8.142.3.168,"""",""广东电信LTE_OMMB3,佛山市禅城区1(440601)"",广东电信LTE_OMMB3,平台告警(20420),00:00:20,"""","""","""","""",线路故障(517),""""
"""",未确认,严重,环市镇安中一(501927),""地面资源(MO SDR)=1,机架(MO SDR)=1,机框(MO SDR)=1,单板(MO SDR)=1"",LTE小区退出服务(198094419),2015-10-15 10:35:43,管理网元(MO SDR),服务质量告警,1.S1链路故障； 2.小区所使用的主控板、基带板或RRU故障； 3.小区配置失败； 4.时钟失锁。,""小区用户标识: 环市镇安中一_1, 小区标识：1：RRU板异常导致; 小区类型：普通小区; 单板序列号: 275836101155; L eNBId:501927"",否,2015-10-15 10:36:03,"""",CELL,""SubNetwork=440601,MEID=501927,ENBFunctionFDD=501927,EUtranCellFDD=1"",CCC,1,环市镇安中一,501927,环市镇安中一_1,LTE FDD,10,"""","""","""",站点ID(局向) : 501927; 站点名称(局向) : 环市镇安中一; 告警对象类型 : CELL; 告警对象ID : 1; 告警对象名称 : 环市镇安中一_1; 单板类型 : CCC; ,"""","""","""","""","""","""",调测恢复,"""","""","""","""",1439235125713,8.142.3.168,"""",""广东电信LTE_OMMB3,佛山市禅城区1(440601)"",广东电信LTE_OMMB3,LTE FDD业务告警(20428),00:00:20,"""","""","""","""",通信子系统故障(306),""""
"""",未确认,主要,环市镇安中一(501927),""地面资源(MO SDR)=1,机架(MO SDR)=1,机框(MO SDR)=1,单板(MO SDR)=1"",X2断链告警(198094421),2015-10-15 10:35:43,管理网元(MO SDR),通信告警,1.SCTP偶联断； 2.X2AP建立失败（协商失败或基站无小区）。,单板序列号: 275836101155; L eNBId:501927,否,2015-10-15 10:36:03,"""",SDR,""SubNetwork=440601,MEID=501927,TransportNetwork=1"",CCC,501927,环市镇安中一,501927,环市镇安中一,LTE FDD,3,"""","""","""",站点ID(局向) : 501927; 站点名称(局向) : 环市镇安中一; 告警对象类型 : SDR; 告警对象ID : 501927; 告警对象名称 : 环市镇安中一; 单板类型 : CCC; ,"""","""","""","""","""","""",调测恢复,"""","""","""","""",1439235125715,8.142.3.168,"""",""广东电信LTE_OMMB3,佛山市禅城区1(440601)"",广东电信LTE_OMMB3,LTE FDD业务告警(20428),00:00:20,"""","""","""","""",通信子系统故障(306),""""
"""",未确认,主要,永丰大厦(502100),""地面资源(MO SDR)=1,机架(MO SDR)=53,机框(MO SDR)=1,单板(MO SDR)=1"",PA去使能(198098440),2015-10-15 10:34:08,管理网元(MO SDR),设备告警,1. 人工停用PA。 2. 天馈过驻波。 3. 内部故障导致PA停用。,""PA-ANT4; 原因信息: ANT OVER VSWR; 单板序列号: 219030688753; L eNBId:502100; 拓扑: 光接口板=(1,1,6), 光接口板光口号=2, 主链级联号=2"",否,2015-10-15 10:34:43,"""",RU,""SubNetwork=440601,MEID=502100,Equipment=1,Rack=53,SubRack=1,Slot=1,PlugInUnit=1"",R8882 S2100(B),53,永丰大厦,502100,永丰大厦,LTE FDD,4,"""","""","""",站点ID(局向) : 502100; 站点名称(局向) : 永丰大厦; 告警对象类型 : RU; 告警对象ID : 53; 告警对象名称 : 永丰大厦; 单板类型 : R8882 S2100(B); ,"""","""","""","""","""","""",调测恢复,"""","""","""","""",1439235125669,8.142.1.23,"""",""广东电信LTE_OMMB3,佛山市禅城区1(440601)"",广东电信LTE_OMMB3,平台告警(20420),00:00:35,"""","""","""","""",基础资源不可用(356),""""
"""",未确认,严重,永丰大厦(502100),""地面资源(MO SDR)=1,机架(MO SDR)=53,机框(MO SDR)=1,单板(MO SDR)=1"",天馈驻波比异常(198098465),2015-10-15 10:34:08,管理网元(MO SDR),设备告警,天馈线缆连接故障。,""TX-ANT4; 驻波比等级: 严重, 驻波比值: 3.31, 小区ID: CellId[0] = 2; 单板序列号: 219030688753; L eNBId:502100; 拓扑: 光接口板=(1,1,6), 光接口板光口号=2, 主链级联号=2"",否,2015-10-15 10:34:43,"""",RU,""SubNetwork=440601,MEID=502100,Equipment=1,Rack=53,SubRack=1,Slot=1,PlugInUnit=1"",R8882 S2100(B),53,永丰大厦,502100,永丰大厦,LTE FDD,3,"""","""","""",站点ID(局向) : 502100; 站点名称(局向) : 永丰大厦; 告警对象类型 : RU; 告警对象ID : 53; 告警对象名称 : 永丰大厦; 单板类型 : R8882 S2100(B); ,"""","""","""","""","""","""",调测恢复,"""","""","""","""",1439235125670,8.142.1.23,"""",""广东电信LTE_OMMB3,佛山市禅城区1(440601)"",广东电信LTE_OMMB3,平台告警(20420),00:00:35,"""","""","""","""",天线问题(503),""""",
            6)]
        public void Test_MultiLines(string testInput, int count)
        {
            IEnumerable<AlarmStatCsv> stats = CsvContext.ReadString<AlarmStatCsv>(testInput, CsvFileDescription.CommaDescription).ToList();
            Assert.IsNotNull(stats);
            Assert.AreEqual(stats.Count(), count);

            var statList = Mapper.Map<IEnumerable<AlarmStatCsv>, List<AlarmStat>>(stats);
            Assert.AreEqual(statList.Count, count);
        }

        [TestCase("test1", 15)]
        [TestCase("test2", 30)]
        [TestCase("test3", 99)]
        [TestCase("test4", 300)]
        [TestCase("test5", 14)]
        [TestCase("test51", 500)]
        [TestCase("test52", 500)]
        [TestCase("test53", 250)]
        [TestCase("test54", 120)]
        [TestCase("test55", 70)]
        [TestCase("test56", 31)]
        [TestCase("test57", 8)]
        [TestCase("test571", 3)]
        [TestCase("test5711", 4)]
        [TestCase("历史告警查询(所有列)-0914-20-zte2", 3052)]
        public void Test_Integrity(string fileName, int count)
        {
            var testDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CsvFiles");
            var path = Path.Combine(testDir, fileName + ".csv");
            IEnumerable<AlarmStatCsv> stats = CsvContext.Read<AlarmStatCsv>(path,
                CsvFileDescription.CommaDescription).ToList();
            Assert.IsNotNull(stats);
            Assert.AreEqual(stats.Count(), count);

            var statList = Mapper.Map<IEnumerable<AlarmStatCsv>, List<AlarmStat>>(stats);
            Assert.AreEqual(statList.Count, count);
        }

        [TestCase(@"根源告警标识,确认状态,告警级别,网元,网元内定位,告警码,发生时间,网元类型,告警类型,告警原因,附加文本,ADMC告警,告警恢复时间,重复计数,告警对象类型,告警对象DN,单板类型,告警对象ID,站点名称(局向),站点ID(局向),告警对象名称,标准告警码,产品,告警标识,影响网元,影响网元内定位,告警修改时间,附加内容,确认/反确认用户,确认/反确认系统,告警确认/反确认时间,告警确认/反确认信息,清除用户,清除系统,恢复方式,清除信息,告警注释,注释用户,注释系统,注释时间,告警编号,网元IP,链路,网元分组,网元代理,系统类型,持续时间(hh:mm:ss),关联业务,产生方式,门限任务信息,调测状态
"""",已确认,警告,丹灶环保食堂P(551977),"""",性能数据入库延迟(15010001),2015-09-17 13:07:56,管理网元(MO SDR),设备告警,性能数据入库延迟,""2015-09-17 12:15:00--2015-09-17 12:30:00 其他错误： 天线端口相关统计(37391),传输层VLAN tag流量统计(37390),传输层DSCP code流量统计(37385),传输层IP对流量统计(37384),PHR统计(37404),载频资源扩展统计(49539),小区基于原因的切换扩展统计(37401),小区CQI扩展统计(37402),CP级性能统计(37403),eNB SON统计(37399),SCTP链路相关统计(37392),传输层eNodeb流量统计(37393),eNB系统资源统计(37394),OAM链路相关统计(37395),CPU单板资源利用率(37001),小区RLC统计(37412),RRC扩展统计(37411),RRU功率测量(37019),eNB间S1口异频切换统计(小区对)(37365),eNB间X2口异频切换统计(小区对)(37364),LTE与CDMA切换统计(小区对)(37367),LTE与UTRAN切换统计(小区对)(37366),eNB间X2口同频切换统计(小区对)(37361),小区UE context统计(49502),eNB内同频切换统计(小区对)(37360),eNB内异频切换统计(小区对)(37363),小区丢包统计(37455),eNB间S1口同频切换统计(小区对)(37362),小区吞吐量统计(37454),小区RRC连接统计(49500),基于原因的切换统计(小区对)(37373),小区SON统计(小区对)(37372),切换无线信号测量统计(小区对)(37374),邻区测量上报统计(小区对)(37369),LTE与GSM切换统计(小区对)(37368),小区基于原因的切换统计(37348),小区调制方式统计(37351),小区随机接入统计(37344),小区BLER统计(37345),小区SON统计(小区)(37347),小区包时延统计(37456),ICIC统计(37356),小区包比特率统计(37457),噪声与干扰检测统计(37358),小区用户数统计(37458),载频资源扩展统计(37359),接口PDCP吞吐量统计(37352),小区PDCP抖动统计(37353),TA统计(37355),小区丢包统计(37335)"",否,2015-09-17 13:22:56,"""","""","""","""","""","""","""","""",连接建立错误(566),LTE FDD,"""","""","""","""","""",历史告警自动确认定时任务,EMS服务器(172.16.21.21),2015-09-20 21:20:00,"""","""","""",正常恢复,"""","""","""","""","""",1440170332794,8.142.74.155,"""",""广东电信LTE_OMMB11,佛山市南海区4(440606)"",广东电信LTE_OMMB11,无线公用告警(20402),00:15:00,"""","""","""",""""",
            1)]
        public void Test_SpecialCase(string testInput, int count)
        {
            IEnumerable<AlarmStatCsv> stats =
                CsvContext.ReadString<AlarmStatCsv>(testInput, CsvFileDescription.CommaDescription).ToList();
            Assert.IsNotNull(stats);
            Assert.AreEqual(stats.Count(), count);

            var statList = Mapper.Map<IEnumerable<AlarmStatCsv>, List<AlarmStat>>(stats);
            Assert.AreEqual(statList.Count, count);
        }

        [TestCase(@" ,级别,告警ID,名称,网元类型,告警源,MO对象,定位信息,发生时间(NT),清除时间(NT),RRU名称,BBU名称,eNodeB ID,用户自定义标识,确认时间(ST),确认用户,所属子网
-,提示,29247,小区PCI冲突告警,BTS3900 LTE,龙江龙山接入机房LBBU6,""eNodeB名称=龙江龙山接入机房LBBU6, 本地小区标识=2, 小区名称=龙江建业路R_2, 小区双工模式=CELL_FDD"",""eNodeB名称=龙江龙山接入机房LBBU6, 本地小区标识=2, PCI值=342, 下行频点=1825, 小区双工模式=FDD, 冲突类型=混淆, 小区名称=龙江建业路R_2, eNodeB标识=500628, 小区标识=50"",09/19/2015 20:08:17,10/09/2015 11:27:54,-,龙江龙山接入机房LBBU6,500628,-,10/09/2015 11:27:54, < 系统 > ,根子网/佛山电信LTE/佛山电信LTE-拉远",
            1)]
        [TestCase(@" ,级别,告警ID,名称,网元类型,告警源,MO对象,定位信息,发生时间(NT),清除时间(NT),RRU名称,BBU名称,eNodeB ID,用户自定义标识,确认时间(ST),确认用户,所属子网
-,重要,26532,射频单元硬件故障告警,BTS3900 LTE,乐从东平桥脚,乐从东平桥脚,""柜号=0, 框号=122, 槽号=0, 单板类型=MRRU"",09/19/2015 23:46:06,09/23/2015 19:13:53,乐从东平桥脚_4,乐从东平桥脚,500264,-,09/23/2015 19:13:56, < 系统 > ,根子网/佛山电信LTE/佛山基站
-,提示,29247,小区PCI冲突告警,BTS3900 LTE,容桂轻轨冠天楼,""eNodeB名称=容桂轻轨冠天楼, 本地小区标识=1, 小区名称=容桂轻轨冠天楼_1, 小区双工模式=CELL_FDD"",""eNodeB名称=容桂轻轨冠天楼, 本地小区标识=1, PCI值=251, 下行频点=1825, 小区双工模式=FDD, 冲突类型=混淆, 小区名称=容桂轻轨冠天楼_1, eNodeB标识=500278, 小区标识=49"",09/20/2015 01:37:07,10/06/2015 03:18:31,-,容桂轻轨冠天楼,500278,-,10/06/2015 03:20:47, < 系统 > ,根子网/佛山电信LTE/佛山待开通
-,提示,29247,小区PCI冲突告警,BTS3900 LTE,均安建安路,""eNodeB名称=均安建安路, 本地小区标识=2, 小区名称=均安建安路_2, 小区双工模式=CELL_FDD"",""eNodeB名称=均安建安路, 本地小区标识=2, PCI值=18, 下行频点=1825, 小区双工模式=FDD, 冲突类型=混淆, 小区名称=均安建安路_2, eNodeB标识=500272, 小区标识=50"",09/20/2015 02:45:39,10/09/2015 11:06:19,-,均安建安路,500272,-,10/09/2015 11:06:20, < 系统 > ,根子网/佛山电信LTE/佛山基站
-,提示,29247,小区PCI冲突告警,BTS3900 LTE,容桂轻轨冠天楼,""eNodeB名称=容桂轻轨冠天楼, 本地小区标识=2, 小区名称=容桂轻轨冠天楼_2, 小区双工模式=CELL_FDD"",""eNodeB名称=容桂轻轨冠天楼, 本地小区标识=2, PCI值=323, 下行频点=1825, 小区双工模式=FDD, 冲突类型=混淆, 小区名称=容桂轻轨冠天楼_2, eNodeB标识=500278, 小区标识=50"",09/20/2015 04:46:25,10/06/2015 03:18:31,-,容桂轻轨冠天楼,500278,-,10/06/2015 03:20:47, < 系统 > ,根子网/佛山电信LTE/佛山待开通
-,提示,29247,小区PCI冲突告警,BTS3900 LTE,北窖碧江电信LBBU11,""eNodeB名称=北窖碧江电信LBBU11, 本地小区标识=1, 小区名称=北滘碧桂水蓝湾R_1, 小区双工模式=CELL_FDD"",""eNodeB名称=北窖碧江电信LBBU11, 本地小区标识=1, PCI值=500, 下行频点=1825, 小区双工模式=FDD, 冲突类型=混淆, 小区名称=北滘碧桂水蓝湾R_1, eNodeB标识=500570, 小区标识=49"",09/20/2015 08:11:17,09/23/2015 14:27:55,-,北窖碧江电信LBBU11,500570,-,09/23/2015 14:29:32, < 系统 > ,根子网/佛山电信LTE/佛山待开通
-,提示,29247,小区PCI冲突告警,BTS3900 LTE,容桂新有路LBBU2,""eNodeB名称=容桂新有路LBBU2, 本地小区标识=1, 小区名称=容桂碧桂南路R_1, 小区双工模式=CELL_FDD"",""eNodeB名称=容桂新有路LBBU2, 本地小区标识=1, PCI值=321, 下行频点=1825, 小区双工模式=FDD, 冲突类型=混淆, 小区名称=容桂碧桂南路R_1, eNodeB标识=552501, 小区标识=49"",09/20/2015 09:05:10,10/09/2015 13:33:05,-,容桂新有路LBBU2,552501,-,10/09/2015 13:33:07, < 系统 > ,根子网/佛山电信LTE/佛山待开通
-,次要,26521,射频单元接收通道RTWP/RSSI过低告警,BTS3900 LTE,逢沙污水厂,逢沙污水厂,""柜号=0, 框号=62, 槽号=0, 接收通道号=0, 单板类型=MRRU, RTWP值(0.1dBm)=-1145"",09/18/2015 11:36:13,09/29/2015 09:11:06,-,逢沙污水厂,499828,-,09/29/2015 09:11:07, < 系统 > ,根子网/佛山电信LTE/佛山电信LTE-室外
-,次要,26232,BBU光模块收发异常告警,BTS3900 LTE,大良南区电信LBBU8,大良南区电信LBBU8,""柜号=0, 框号=0, 槽号=1, 端口号=0, 单板类型=LBBP, 具体问题=接收功率过低"",09/20/2015 09:40:54,10/04/2015 19:06:42,-,大良南区电信LBBU8,501142,-,10/04/2015 19:09:34, < 系统 > ,根子网/佛山电信LTE/佛山电信LTE-室分
-,提示,29247,小区PCI冲突告警,BTS3900 LTE,乐从东悦花园接入机房LBBU12,""eNodeB名称=乐从东悦花园接入机房LBBU12, 本地小区标识=2, 小区名称=乐从佛山新城百顺路R_2, 小区双工模式=CELL_FDD"",""eNodeB名称=乐从东悦花园接入机房LBBU12, 本地小区标识=2, PCI值=267, 下行频点=1825, 小区双工模式=FDD, 冲突类型=混淆, 小区名称=乐从佛山新城百顺路R_2, eNodeB标识=552667, 小区标识=50"",09/20/2015 11:34:59,09/30/2015 15:27:12,-,乐从东悦花园接入机房LBBU12,552667,-,09/30/2015 15:27:15, < 系统 > ,根子网/佛山电信LTE/佛山电信LTE-拉远
-,提示,29247,小区PCI冲突告警,BTS3900 LTE,龙江丰华接入机房LBBU4,""eNodeB名称=龙江丰华接入机房LBBU4, 本地小区标识=0, 小区名称=龙江扒头工业区R_0, 小区双工模式=CELL_FDD"",""eNodeB名称=龙江丰华接入机房LBBU4, 本地小区标识=0, PCI值=301, 下行频点=1825, 小区双工模式=FDD, 冲突类型=混淆, 小区名称=龙江扒头工业区R_0, eNodeB标识=552508, 小区标识=48"",09/20/2015 12:37:18,09/23/2015 15:57:58,-,龙江丰华接入机房LBBU4,552508,-,09/23/2015 15:57:58, < 系统 > ,根子网/佛山电信LTE/佛山待开通
-,提示,29247,小区PCI冲突告警,BTS3900 LTE,陈村电信机楼LBBU14,""eNodeB名称=陈村电信机楼LBBU14, 本地小区标识=2, 小区名称=陈村观音路R_2, 小区双工模式=CELL_FDD"",""eNodeB名称=陈村电信机楼LBBU14, 本地小区标识=2, PCI值=285, 下行频点=1825, 小区双工模式=FDD, 冲突类型=混淆, 小区名称=陈村观音路R_2, eNodeB标识=500769, 小区标识=50"",09/20/2015 12:53:24,10/09/2015 20:18:53,-,陈村电信机楼LBBU14,500769,-,10/09/2015 20:18:54, < 系统 > ,根子网/佛山电信LTE/佛山待开通",
        11)]
        public void Test_Huawei(string testInput, int count)
        {
            IEnumerable<AlarmStatHuawei> stats =
                CsvContext.ReadString<AlarmStatHuawei>(testInput, CsvFileDescription.CommaDescription).ToList();
            Assert.IsNotNull(stats);
            Assert.AreEqual(stats.Count(), count);

            var statList = Mapper.Map<IEnumerable<AlarmStatHuawei>, List<AlarmStat>>(stats);
            Assert.AreEqual(statList.Count, count);
        }

        [TestCase("huawei", 6)]
        [TestCase("huawei9273", 9272)]
        public void Test_Integrity_Huawei(string fileName, int count)
        {
            var testDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CsvFiles");
            var path = Path.Combine(testDir, fileName + ".csv");
            IEnumerable<AlarmStatHuawei> stats = CsvContext.Read<AlarmStatHuawei>(path,
                CsvFileDescription.CommaDescription).ToList();
            Assert.IsNotNull(stats);
            Assert.AreEqual(stats.Count(), count);

            var statList = Mapper.Map<IEnumerable<AlarmStatHuawei>, List<AlarmStat>>(stats);
            Assert.AreEqual(statList.Count, count);
        }
    }
}
