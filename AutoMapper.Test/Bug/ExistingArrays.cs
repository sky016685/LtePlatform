﻿using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AutoMapper.Test.Bug
{
    [TestFixture]
    public class ExistingArrays : AutoMapperSpecBase
    {
        protected override void Establish_context()
        {
            Mapper.CreateMap<Source, Dest>();
            Mapper.CreateMap<Source, DestWithIEnumerableInitializer>();
        }

        [Test]
        public void should_map_array_inside_object()
        {
            var source = new Source { Values = new[] { "1", "2" } };
            var dest = Mapper.Map<Dest>(source);
        }


        [Test]
        public void should_map_over_enumerable_empty()
        {
            var source = new Source { Values = new[] { "1", "2" } };
            var dest = Mapper.Map<DestWithIEnumerableInitializer>(source);
        }

        public class Source
        {
            public Source()
            {
                Values = new string[0];
            }

            public string[] Values { get; set; }
        }

        public class Dest
        {
            public Dest()
            {
                // remove this line will get it fixed. 
                Values = new string[0];
            }

            public string[] Values { get; set; }
        }

        public class DestWithIEnumerableInitializer
        {
            public DestWithIEnumerableInitializer()
            {
                // remove this line will get it fixed. 
                Values = Enumerable.Empty<string>();
            }

            public IEnumerable<string> Values { get; set; }
        }
    }
}