﻿using AutoMapper.Should;
using NUnit.Framework;
using Shouldly;

namespace AutoMapper.Test.Bug
{
    [TestFixture]
    public class ObjectTypeMapFailure : NonValidatingSpecBase
    {
        [Test]
        public void Should_map_the_object_type()
        {
            var displayModel = new DisplayModel
            {
                Radius = 300
            };
            object vm = new SomeViewModel();
            Mapper.CreateMap<DisplayModel, SomeViewModel>();

            Mapper.Map(displayModel, vm);
            ((SomeViewModel)vm).Radius.ShouldBe(300); // fails

            var vm2 = new SomeViewModel();
            Mapper.Map(displayModel, vm2);
            vm2.Radius.ShouldBe(300); // succeeds
        }

        public class SomeViewModel
        {
            public int Radius { get; set; }
        }

        public class DisplayModel
        {
            public int Radius { get; set; }
        }
    }
}
