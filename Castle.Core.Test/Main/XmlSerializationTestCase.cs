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

using System.IO;
using System.Xml.Serialization;
using Castle.Core.Test.DynamicProxy.Classes;
using Castle.DynamicProxy;
using NUnit.Framework;

#if !SILVERLIGHT
namespace Castle.Core.Test.Main
{
    [TestFixture]
	public class XmlSerializationTestCase : BasePEVerifyTestCase
	{
		[Test, Ignore("Could not come up with a solution for this")]
		public void ProxyIsXmlSerializable()
		{
			var proxy = (ClassToSerialize)
			                         generator.CreateClassProxy(typeof (ClassToSerialize), new StandardInterceptor());

			var serializer = new XmlSerializer(proxy.GetType());

			var writer = new StringWriter();

			serializer.Serialize(writer, proxy);

			var reader = new StringReader(writer.GetStringBuilder().ToString());

			var newObj = serializer.Deserialize(reader);

			Assert.IsNotNull(newObj);
			Assert.IsInstanceOf(typeof (ClassToSerialize), newObj);
		}
	}
}
#endif