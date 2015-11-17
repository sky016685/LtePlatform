﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities;

namespace Lte.Parameters.Abstract
{
    public interface ITownPreciseCoverage4GStatRepository : IRepository<TownPreciseCoverage4GStat>
    {
        List<TownPreciseCoverage4GStat> GetByDateSpan(DateTime begin, DateTime end);
    }
}
