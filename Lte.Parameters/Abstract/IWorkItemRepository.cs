﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Work;

namespace Lte.Parameters.Abstract
{
    public interface IWorkItemRepository : IPagingRepository<WorkItem>
    {
        void Import(WorkItemExcel itemExcel);

        Task<List<WorkItem>> GetAllListAsync(int eNodebId, byte sectorId);

        Task<List<WorkItem>> GetAllListAsync(int eNodebId);

        Task<List<WorkItem>> GetAllListAsync(DateTime begin, DateTime end);

        Task<List<WorkItem>> GetAllKpiListAsync(DateTime begin, DateTime end);

        Task<List<WorkItem>> GetUnfinishedPreciseListAsync(DateTime begin, DateTime end);

        Task<WorkItem> GetPreciseExistedAsync(int eNodebId, byte sectorId);
    }
}
