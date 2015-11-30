﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.Policy;
using Lte.Evaluations.ViewModels;

namespace LtePlatform.Controllers.Kpi
{
    public class TopDrop2GController : ApiController
    {
        private readonly TopDrop2GService _service;

        public TopDrop2GController(TopDrop2GService service)
        {
            _service = service;
        }

        [HttpGet]
        public TopDrop2GDateView Get(DateTime statDate, string city)
        {
            return _service.GetDateView(statDate, city);
        }

        [HttpGet]
        public IEnumerable<TopDrop2GTrendView> Get(DateTime begin, DateTime end, string city)
        {
            return _service.GetTrendViews(begin, end, city);
        }

        [HttpGet]
        public IEnumerable<TopDrop2GTrendView> Get(DateTime begin, DateTime end, string city,
            string policy, int topCount)
        {
            return _service.GetTrendViews(begin, end, city).Order(policy.GetTopDrop2GPolicy(), topCount);
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return OrderTopDrop2GService.OrderSelectionList.Select(x => x.Key);
        }
    }
}