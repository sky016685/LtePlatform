﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular;
using Lte.Parameters.Entities;
using Lte.Parameters.Entities.Basic;
using Lte.Parameters.Entities.ExcelCsv;

namespace Lte.Parameters.MockOperations
{
    public static class CoreMapperService
    {
        public static void MapCell()
        {
            Mapper.CreateMap<CellExcel, Cell>()
                .ForMember(d => d.AntennaPorts, opt => opt.MapFrom(s => s.TransmitReceive.GetAntennaPortsConfig()))
                .ForMember(d => d.IsOutdoor, opt => opt.MapFrom(s => s.IsIndoor.Trim() == "否"));
            Mapper.CreateMap<CdmaCellExcel, CdmaCell>()
                .ForMember(d => d.Frequency, opt => opt.Ignore())
                .ForMember(d => d.IsOutdoor, opt => opt.MapFrom(s => s.IsIndoor.Trim() == "否"));
            Mapper.CreateMap<ENodebExcel, ENodeb>()
                .ForMember(d => d.IsFdd,
                    opt => opt.MapFrom(s => s.DivisionDuplex.IndexOf("FDD", StringComparison.Ordinal) >= 0))
                .ForMember(d => d.Gateway, opt => opt.MapFrom(s => s.Gateway.AddressValue))
                .ForMember(d => d.SubIp, opt => opt.MapFrom(s => s.Ip.IpByte4));
        }
        
        public static void MapDtItems()
        {
            Mapper.CreateMap<FileRecord2G, FileRecordCoverage2G>()
                .ForMember(d => d.Longtitute, opt => opt.MapFrom(s => s.Longtitute ?? -1))
                .ForMember(d => d.Lattitute, opt => opt.MapFrom(s => s.Lattitute ?? -1))
                .ForMember(d => d.Ecio, opt => opt.MapFrom(s => s.Ecio ?? 0))
                .ForMember(d => d.RxAgc, opt => opt.MapFrom(s => s.RxAgc ?? 0))
                .ForMember(d => d.TxPower, opt => opt.MapFrom(s => s.TxPower ?? 0));
            Mapper.CreateMap<FileRecord3G, FileRecordCoverage3G>()
                .ForMember(d => d.Longtitute, opt => opt.MapFrom(s => s.Longtitute ?? -1))
                .ForMember(d => d.Lattitute, opt => opt.MapFrom(s => s.Lattitute ?? -1))
                .ForMember(d => d.RxAgc0, opt => opt.MapFrom(s => s.RxAgc0 ?? 0))
                .ForMember(d => d.RxAgc1, opt => opt.MapFrom(s => s.RxAgc1 ?? 0))
                .ForMember(d => d.Sinr, opt => opt.MapFrom(s => s.Sinr ?? 0));
            Mapper.CreateMap<FileRecord4G, FileRecordCoverage4G>()
                .ForMember(d => d.Longtitute, opt => opt.MapFrom(s => s.Longtitute ?? -1))
                .ForMember(d => d.Lattitute, opt => opt.MapFrom(s => s.Lattitute ?? -1))
                .ForMember(d => d.Rsrp, opt => opt.MapFrom(s => s.Rsrp ?? 0))
                .ForMember(d => d.Sinr, opt => opt.MapFrom(s => s.Sinr ?? 0));
        }

        public static void MapAlarms()
        {
            Mapper.CreateMap<AlarmStatCsv, AlarmStat>()
                .ForMember(d => d.AlarmLevel, opt => opt.MapFrom(s => s.AlarmLevelDescription.GetAlarmLevel()))
                .ForMember(d => d.AlarmCategory, opt => opt.MapFrom(s => s.AlarmCategoryDescription.GetCategory()))
                .ForMember(d => d.AlarmType, opt => opt.MapFrom(s => s.AlarmCodeDescription.GetAlarmType()))
                .ForMember(d => d.SectorId, opt => opt.MapFrom(s => s.ObjectId > 255 ? (byte)255 : (byte)s.ObjectId))
                .ForMember(d => d.RecoverTime,
                    opt =>
                        opt.MapFrom(
                            s => s.RecoverTime < new DateTime(2000, 1, 1) ? new DateTime(2200, 1, 1) : s.RecoverTime))
                .ForMember(d=>d.AlarmId,opt=>opt.MapFrom(s=>s.AlarmId.ConvertToInt(0)));

            Mapper.CreateMap<AlarmStatHuawei, AlarmStat>()
                .ForMember(d => d.AlarmLevel, opt => opt.MapFrom(s => s.AlarmLevelDescription.GetAlarmLevel()))
                .ForMember(d => d.AlarmCategory, opt => opt.MapFrom(s => AlarmCategory.Huawei))
                .ForMember(d => d.AlarmType, opt => opt.MapFrom(s => s.AlarmCodeDescription.GetAlarmHuawei()))
                .ForMember(d => d.ENodebId, opt => opt.MapFrom(s => s.ENodebIdString.ConvertToInt(0)))
                .ForMember(d => d.RecoverTime,
                    opt => opt.MapFrom(s => s.RecoverTime.ConvertToDateTime(new DateTime(2200, 1, 1))));
        }
    }
}
