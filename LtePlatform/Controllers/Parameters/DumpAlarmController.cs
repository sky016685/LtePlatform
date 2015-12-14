﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("导入告警信息处理器")]
    public class DumpAlarmController : ApiController
    {
        private readonly AlarmsService _service;

        public DumpAlarmController(AlarmsService service)
        {
            _service = service;
        }

        [ApiDoc("导入一条告警信息")]
        [ApiResponse("导入结果")]
        public bool Get()
        {
            return _service.DumpOneStat();
        }
    }
}