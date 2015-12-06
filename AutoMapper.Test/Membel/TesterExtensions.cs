using System.Collections.Generic;
using System.Linq;
using AutoMapper.Should;

namespace AutoMapper.Test.Membel
{
	public static class StopgapNBehaveExtensions
	{
		public static void ShouldBeOfLength<T>(this IEnumerable<T> collection, int length)
		{
			collection.ShouldNotBeNull();
			collection.Count().ShouldEqual(length);
		}
	}
}