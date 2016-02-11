﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Parameters.Abstract;
using Lte.Parameters.Concrete.Mr;
using Lte.Parameters.Entities.Mr;
using NUnit.Framework;
using Shouldly;

namespace Lte.Parameters.Test.Mr
{
    [TestFixture]
    public class InterferenceMongoRepositoryTests
    {
        private readonly IInterferenceMongoRepository _repository = new InterferenceMongoRepository();

        [Test]
        public void Test_GetMongoInfos_First()
        {
            var results = _repository.GetByENodebInfo("522409_304_241_1825");
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count, 302);
            Assert.AreEqual((double)results[0].INTERF_ONLY_COFREQ, 11.88, 1E-7);
            results[0].current_date.ShouldBe("201512301530");
            results[0].MOD3_COUNT.ShouldBe(2);
            results[0].MOD6_COUNT.ShouldBe(0);
            results[0].OVERCOVER_COFREQ_6DB.ShouldBe(2);
            results[0].OVERCOVER_COFREQ_10DB.ShouldBe(2);
        }

        [Test]
        public void Test_GetWithENodebIdAndPci()
        {
            var results = _repository.GetList(522409, 304);
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count, 13308);
            Assert.AreEqual((double)results[0].INTERF_ONLY_COFREQ, 3.74, 1E-7);
            results[0].current_date.ShouldBe("201601091430");
            results[0].ENODEBID_PCI_NPCI_NFREQ.ShouldBe("522409_304_107_1825");
            results[0].MOD3_COUNT.ShouldBe(0);
            results[0].MOD6_COUNT.ShouldBe(0);
            results[0].OVERCOVER_COFREQ_6DB.ShouldBe(0);
            results[0].OVERCOVER_COFREQ_10DB.ShouldBeNull();
        }

        [Test]
        public void Test_GetWithENodebIdAndPciAndTime()
        {
            var results = _repository.GetList(522409, 304, new DateTime(2015, 12, 30, 15, 30, 0));
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count, 10);
            Assert.AreEqual((double)results[0].INTERF_ONLY_COFREQ, 3.84, 1E-7);
            results[0].current_date.ShouldBe("201512301530");
            results[0].ENODEBID_PCI_NPCI_NFREQ.ShouldBe("522409_304_129_1825");
            results[0].MOD3_COUNT.ShouldBe(0);
            results[0].MOD6_COUNT.ShouldBe(0);
            results[0].OVERCOVER_COFREQ_6DB.ShouldBe(0);
            results[0].OVERCOVER_COFREQ_10DB.ShouldBeNull();
        }
        
        [Test]
        public void Test_GetOne()
        {
            var result = _repository.GetOne("522409_304_241_1825", "201512301530");
            Assert.IsNotNull(result);

            Assert.AreEqual((double)result.INTERF_ONLY_COFREQ, 11.88, 1E-7);
            result.current_date.ShouldBe("201512301530");
            result.MOD3_COUNT.ShouldBe(2);
            result.MOD6_COUNT.ShouldBe(0);
            result.OVERCOVER_COFREQ_6DB.ShouldBe(2);
            result.OVERCOVER_COFREQ_10DB.ShouldBe(2);
        }
    }
}