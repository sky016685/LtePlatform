﻿using System.Collections.Generic;
using System.Linq;
using Lte.Evaluations.ViewModels.College;
using Lte.Parameters.Abstract;
using Lte.Parameters.Abstract.College;
using Lte.Parameters.Entities.College;

namespace Lte.Evaluations.DataService.College
{
    public class CollegeStatService
    {
        private readonly ICollegeRepository _repository;
        private readonly IInfrastructureRepository _infrastructureRepository;

        public CollegeStatService(ICollegeRepository repository, IInfrastructureRepository infrastructureRepository)
        {
            _repository = repository;
            _infrastructureRepository = infrastructureRepository;
        }

        public CollegeStat QueryStat(int id)
        {
            var info = _repository.Get(id);
            return info == null
                ? null
                : new CollegeStat(_repository, info, _infrastructureRepository);
        }

        public IEnumerable<CollegeStat> QueryStats()
        {
            IEnumerable<CollegeInfo> infos = _repository.GetAllList();
            return !infos.Any()
                ? new List<CollegeStat>()
                : infos.Select(x => new CollegeStat(_repository, x, _infrastructureRepository));
        }

        public IEnumerable<string> QueryNames()
        {
            return _repository.GetAllList().Select(x => x.Name).Distinct();
        }

        public IEnumerable<string> QueryNames(int year)
        {
            return _repository.GetAllList(year).Select(x => x.Name).Distinct();
        }
    }
}
