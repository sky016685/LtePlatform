﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.DataService.College;
using Lte.Parameters.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("查询校园网LTE室内分布的控制器")]
    public class CollegeLteDistributionsController : ApiController
    {
        private readonly CollegeDistributionService _service;

        public CollegeLteDistributionsController(CollegeDistributionService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询校园网LTE室内分布列表")]
        [ApiParameterDoc("collegeName", "校园名称")]
        [ApiResponse("校园网LTE室内分布列表")]
        public IEnumerable<IndoorDistribution> Get(string collegeName)
        {
            return _service.QueryLteDistributions(collegeName);
        }

        [HttpPost]
        [ApiDoc("查询多个校园对应的LTE室内分布列表（可用于地理化显示）")]
        [ApiParameterDoc("collegeNames", "校园名称列表")]
        [ApiResponse("LTE室内分布列表（可用于地理化显示）")]
        public IEnumerable<IndoorDistribution> Post(CollegeNamesContainer collegeNames)
        {
            return _service.QueryLteDistributions(collegeNames.Names);
        }
    }
}
