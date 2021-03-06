// Copyright 2004-2010 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Castle.Windsor.Tests.Bugs
{
	using System.Collections.Generic;

	using Castle.MicroKernel.Registration;

	using NUnit.Framework;

	[TestFixture]
	public class IoC_138TestCase
	{
		[Test]
		public void TestResolveSubComponentInConstructorWithParameters()
		{
			var container = new WindsorContainer();
			ServiceLocator.Container = container;
			container.Register(Component.For<UsesServiceLocaator>().Named("A"),
			                   Component.For<DependsOnStringTest2>().Named("B"));

			var component = container.Resolve<UsesServiceLocaator>(new Dictionary<string, string> { { "test", "bla" } });
			Assert.IsNotNull(component.Other);
		}

		public class DependsOnStringTest2
		{
			public DependsOnStringTest2(string test2)
			{
			}
		}

		public static class ServiceLocator
		{
			public static IWindsorContainer Container { get; set; }
		}

		public class UsesServiceLocaator
		{
			private readonly DependsOnStringTest2 other;

			public UsesServiceLocaator(string test)
			{
				other = ServiceLocator.Container.Resolve<DependsOnStringTest2>(new Dictionary<string, string> { { "test2", "bla" } });
			}

			public DependsOnStringTest2 Other
			{
				get { return other; }
			}
		}
	}
}