﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using Lte.Parameters.Abstract;
using Lte.Parameters.Abstract.Kpi;
using Lte.Parameters.Entities.Kpi;

namespace Lte.Parameters.Concrete.Kpi
{
    public class EFTownPreciseCoverage4GStatRepository : LightWeightRepositroyBase<TownPreciseCoverage4GStat>,
        ITownPreciseCoverage4GStatRepository
    {
        protected override DbSet<TownPreciseCoverage4GStat> Entities => context.TownPreciseCoverage4GStats;

        public List<TownPreciseCoverage4GStat> GetAllList(DateTime begin, DateTime end)
        {
            return GetAllList(x => x.StatTime >= begin && x.StatTime < end);
        }

        public TownPreciseCoverage4GStat GetByTown(int townId, DateTime statTime)
        {
            return FirstOrDefault(x => x.TownId == townId && x.StatTime == statTime);
        }
    }
}
