// Copyright 2004-2014 Castle Project - http://www.castleproject.org/
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

using System;
using System.Collections.Generic;
using Castle.Core.Test.DynamicProxy.Classes;
using Castle.Core.Test.Main;
using Castle.DynamicProxy;
using Castle.DynamicProxy.Generators;
using NUnit.Framework;

namespace Castle.Core.Test.DynamicProxy
{
    [TestFixture]
	public class ClassProxyConstructorsTestCase : BasePEVerifyTestCase
	{
		[Test]
		public void ShouldGenerateTypeWithDuplicatedBaseInterfacesClassProxy()
		{
			generator.CreateClassProxy(
				typeof(MyOwnClass),
				new Type[] { },
				ProxyGenerationOptions.Default,
				new object[] { },
				new StandardInterceptor());
		}

		[Test]
		[Ignore("I don't see any simple way of doing this...")]
		public void Should_properly_interpret_array_of_objects()
		{
			var proxy =
				(ClassWithVariousConstructors)
				generator.CreateClassProxy(typeof(ClassWithVariousConstructors), new object[] { new object[] { null } });
			Assert.AreEqual(Constructor.ArrayOfObjects, proxy.ConstructorCalled);
		}

		[Test]
		public void Should_properly_interpret_array_of_objects_and_string()
		{
			var proxy =
				(ClassWithVariousConstructors)
				generator.CreateClassProxy(typeof(ClassWithVariousConstructors), new object[] { new object[] { null }, "foo" });
			Assert.AreEqual(Constructor.ArrayOfObjectsAndSingleString, proxy.ConstructorCalled);
		}

		[Test]
		public void Should_properly_interpret_array_of_strings_and_string()
		{
			var proxy =
				(ClassWithVariousConstructors)
				generator.CreateClassProxy(typeof(ClassWithVariousConstructors), new object[] { new string[] { null }, "foo" });
			Assert.AreEqual(Constructor.ArrayAndSingleString, proxy.ConstructorCalled);
		}

		[Test]
		public void Should_properly_interpret_empty_array_as_ctor_argument()
		{
			var proxy =
				(ClassWithVariousConstructors)
				generator.CreateClassProxy(typeof(ClassWithVariousConstructors), new object[] { new string[] { } });
			Assert.AreEqual(Constructor.ArrayOfStrings, proxy.ConstructorCalled);
		}

		[Test]
		public void Can_pass_params_arguments_inline()
		{
				generator.CreateClassProxy(typeof(HasCtorWithParamsStrings), new object[] { });
		}
		[Test]
		public void Can_pass_params_arguments_inline2()
		{
			generator.CreateClassProxy(typeof(HasCtorWithParamsArgument), new object[] { });
		}

		[Test]
		public void Can_pass_params_arguments_inline_implicitly()
		{
			generator.CreateClassProxy(typeof(HasCtorWithIntAndParamsArgument), new object[] { 5 });
		}

		[Test]
		public void Should_properly_interpret_nothing_as_lack_of_ctor_arguments()
		{
			var proxy =
				(ClassWithVariousConstructors)generator.CreateClassProxy(typeof(ClassWithVariousConstructors));
			Assert.AreEqual(Constructor.Default, proxy.ConstructorCalled);
		}

		[Test]
		public void Cannot_proxy_open_generic_type()
		{
			var exception = Assert.Throws<GeneratorException>(() => generator.CreateClassProxy(typeof(List<>)));
			Assert.AreEqual(exception.Message, "Can not create proxy for type System.Collections.Generic.List`1 because it is an open generic type.");
		}

		[Test]
		public void Cannot_proxy_generic_type_with_open_generic_type_parameter()
		{
			var innerType = typeof(List<>);
			var targetType = innerType.MakeGenericType(typeof(List<>));
			var ex = Assert.Throws<GeneratorException>(() => generator.CreateClassProxy(targetType));
			StringAssert.StartsWith(
#if __MonoCS__
				"Can not create proxy for type System.Collections.Generic.List`1[[System.Collections.Generic.List`1, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] because type System.Collections.Generic.List`1 is an open generic type.",
#else
				"Can not create proxy for type List`1 because type System.Collections.Generic.List`1 is an open generic type.",
#endif
				ex.Message);
		}

		[Test]
		public void Cannot_proxy_inaccessible_class()
		{
			var ex = Assert.Throws<GeneratorException>(() => generator.CreateClassProxy(typeof(PrivateClass)));
			StringAssert.StartsWith(
                "Can not create proxy for type Castle.Core.Test.DynamicProxy.ClassProxyConstructorsTestCase+PrivateClass because it is not accessible. Make it public, or internal",
				ex.Message);
		}

		[Test]
		public void Cannot_proxy_generic_class_with_inaccessible_type_argument()
		{
			var ex = Assert.Throws<GeneratorException>(() =>
				generator.CreateClassProxy(typeof(List<PrivateClass>)));
			StringAssert.StartsWith(
                "Can not create proxy for type System.Collections.Generic.List`1[[Castle.Core.Test.DynamicProxy.ClassProxyConstructorsTestCase+PrivateClass, Castle.Core.Test, Version=1.1.0.0, Culture=neutral, PublicKeyToken=407dd0808d44fbdc]] because type Castle.Core.Test.DynamicProxy.ClassProxyConstructorsTestCase+PrivateClass is not accessible. Make it public, or internal",
				ex.Message);
		}

		[Test]
		public void Cannot_proxy_generic_class_with_type_argument_that_has_inaccessible_type_argument()
		{
			var ex = Assert.Throws<GeneratorException>(() => generator.CreateClassProxy(typeof(List<List<PrivateClass>>)));

			var expected = string.Format("Can not create proxy for type {0} because type {1} is not accessible. Make it public, or internal",
				typeof(List<List<PrivateClass>>).FullName, typeof(PrivateClass).FullName);
			StringAssert.StartsWith(expected, ex.Message);
		}

		[Test]
		public void Can_proxy_generic_class()
		{
			generator.CreateClassProxy(typeof(List<object>));
		}

		[Test]
		[Ignore("I don't see any simple way of doing this...")]
		public void Should_properly_interpret_null_as_ctor_argument()
		{
			var proxy =
				(ClassWithVariousConstructors)
				generator.CreateClassProxy(typeof(ClassWithVariousConstructors), new[] { default(object) });
			Assert.AreEqual(Constructor.Object, proxy.ConstructorCalled);
		}

		private class PrivateClass { }
	}

	public abstract class MyOwnClass
	{
		public virtual void Foo<T>(List<T>[] action)
		{
		}

		/* ... */
	}
}