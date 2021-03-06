﻿using System;
using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.College;
using Lte.Evaluations.ViewModels.Precise;

namespace LtePlatform.Controllers.College
{
    public class CollegePreciseController : ApiController
    {
        private readonly CollegePreciseService _service;

        public CollegePreciseController(CollegePreciseService service)
        {
            _service = service;
        }

        public IEnumerable<CellPreciseKpiView> Get(string collegeName, DateTime begin, DateTime end)
        {
            return _service.GetViews(collegeName, begin, end);
        }
    }
}
