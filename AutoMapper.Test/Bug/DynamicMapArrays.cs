﻿using System;
using System.Linq;
using Shouldly;
using NUnit.Framework;

namespace AutoMapper.Test.Bug
{
    [TestFixture]
    public class DynamicMapArrays : AutoMapperSpecBase
    {
        Source[] source;
        Destination[] destination;

        public class Source
        {
            public Source(int value)
            {
                Value = value;
            }

            public int Value { get; set; }
        }

        public class Destination
        {
            public int Value { get; set; }
        }

        protected override void Because_of()
        {
            source = Enumerable.Range(0, 9).Select(i => new Source(i)).ToArray();
            destination = Mapper.DynamicMap<Destination[]>(source);
            //Mapper.CreateMap<Source, Destination>();
            //destination = Mapper.Map<Destination[]>(source);
        }

        [Test]
        public void Should_dynamic_map_the_array()
        {
            destination.Length.ShouldBe(source.Length);
            Array.TrueForAll(source, s => s.Value == destination[s.Value].Value).ShouldBeTrue(); 
        }
    }
}