﻿using System;
using System.Collections.Generic;
using System.Linq;
using Lte.Evaluations.Test.MockItems;
using Lte.Parameters.Abstract;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities;
using Moq;

namespace Lte.Evaluations.Test.TestService
{
    public class CollegeENodebTestService
    {
        private readonly Mock<IInfrastructureRepository> _repository;
        private readonly Mock<IENodebRepository> _eNodebRepository;
        private readonly Mock<IAlarmRepository> _alarmRepository;

        public CollegeENodebTestService(Mock<IInfrastructureRepository> repository,
            Mock<IENodebRepository> eNodebRepository, Mock<IAlarmRepository> alarmRepository)
        {
            _repository = repository;
            _eNodebRepository = eNodebRepository;
            _alarmRepository = alarmRepository;
        }

        public void MockOneENodebInfrastructure(int id)
        {
            var infrastructures = new List<InfrastructureInfo>
            {
                new InfrastructureInfo
                {
                    HotspotName = "College-"+id,
                    HotspotType = HotspotType.College,
                    InfrastructureType = InfrastructureType.ENodeb,
                    InfrastructureId = id
                }
            };
            _repository.MockInfrastructures(infrastructures);
        }

        public void MockOneAlarm(string dateString)
        {
            _alarmRepository.MockAlarms(new List<AlarmStat>
            {
                new AlarmStat {HappenTime = DateTime.Parse(dateString), Details = "Single"}
            });
        }

        public void MockManyENodebInfrastructure(Dictionary<string, IEnumerable<int>> definitions)
        {
            var infrastructures = new List<InfrastructureInfo>();
            foreach (var definition in definitions)
            {
                infrastructures.AddRange(definition.Value.Select(x => new InfrastructureInfo
                {
                    HotspotName = definition.Key,
                    HotspotType = HotspotType.College,
                    InfrastructureType = InfrastructureType.ENodeb,
                    InfrastructureId = x
                }));
            }
            _repository.MockInfrastructures(infrastructures);
        }
    }
}
