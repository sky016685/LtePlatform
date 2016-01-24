// Copyright 2004-2011 Castle Project - http://www.castleproject.org/
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


#if !(SILVERLIGHT || DOTNET35)

namespace CastleTests
{
	using System;
	using System.ComponentModel;
	using System.Linq;
	using System.Reflection;
	using System.Text;

	using Castle.Core.Internal;
	using Castle.MicroKernel;
	using Castle.Windsor;

	using NUnit.Framework;

	[Explicit]
	public class ConventionVerification
	{
		private void Scan(MethodInfo interfaceMethod, MethodInfo classMethod, StringBuilder message)
		{
			var obsolete = EnsureBothHave<ObsoleteAttribute>(interfaceMethod, classMethod, message);
			if (obsolete.Item3)
			{
				if (obsolete.Item1.IsError != obsolete.Item2.IsError)
				{
					message.AppendLine($"Different error levels for {interfaceMethod}");
				}
				if (obsolete.Item1.Message != obsolete.Item2.Message)
				{
					message.AppendLine($"Different message for {interfaceMethod}");
					message.AppendLine($"\t interface: {obsolete.Item1.Message}");
					message.AppendLine($"\t class    : {obsolete.Item2.Message}");
				}
			}
			else
			{
				return;
			}
			var browsable = EnsureBothHave<EditorBrowsableAttribute>(interfaceMethod, classMethod, message);
			{
				if (browsable.Item3 == false)
				{
					message.AppendLine($"EditorBrowsable not applied to {interfaceMethod}");
					return;
				}
				if (browsable.Item1.State != browsable.Item2.State || browsable.Item2.State != EditorBrowsableState.Never)
				{
					message.AppendLine($"Different/wrong browsable states for {interfaceMethod}");
				}
			}
		}

		private static Tuple<TAttribute, TAttribute, bool> EnsureBothHave<TAttribute>(MethodInfo interfaceMethod, MethodInfo classMethod, StringBuilder message)
			where TAttribute : Attribute
		{
			var fromInterface = interfaceMethod.GetAttributes<TAttribute>().SingleOrDefault();
			var fromClass = classMethod.GetAttributes<TAttribute>().SingleOrDefault();
		    if (fromInterface != null)
			{
			    if (fromClass != null) return Tuple.Create(fromInterface, fromClass, true);
			    message.AppendLine($"Method {interfaceMethod} has {typeof (TAttribute)} on the interface, but not on the class.");
			}
			else
			{
				if (fromClass != null)
				{
					message.AppendLine($"Method {interfaceMethod} has {typeof (TAttribute)}  on the class, but not on the interface.");
				}
			}
			return Tuple.Create(fromInterface, fromClass, false);
		}

		[Test]
		public void Obsolete_members_of_kernel_are_in_sync()
		{
			var message = new StringBuilder();
			var kernelMap = typeof(DefaultKernel).GetInterfaceMap(typeof(IKernel));
			for (var i = 0; i < kernelMap.TargetMethods.Length; i++)
			{
				var interfaceMethod = kernelMap.InterfaceMethods[i];
				var classMethod = kernelMap.TargetMethods[i];
				Scan(interfaceMethod, classMethod, message);
			}

			Assert.IsEmpty(message.ToString(), message.ToString());
		}

		[Test]
		public void Obsolete_members_of_windsor_are_in_sync()
		{
			var message = new StringBuilder();
			var kernelMap = typeof(WindsorContainer).GetInterfaceMap(typeof(IWindsorContainer));
			for (var i = 0; i < kernelMap.TargetMethods.Length; i++)
			{
				var interfaceMethod = kernelMap.InterfaceMethods[i];
				var classMethod = kernelMap.TargetMethods[i];
				Scan(interfaceMethod, classMethod, message);
			}

			Assert.IsEmpty(message.ToString(), message.ToString());
		}

        [Test]
	    public void Test_KernelMap_WindsorContainer()
	    {
            var kernelMap = typeof(WindsorContainer).GetInterfaceMap(typeof(IWindsorContainer));
	        Assert.AreEqual(kernelMap.InterfaceMethods.Length, 56);
            Assert.AreEqual(kernelMap.TargetMethods.Length, 56);
	    }
	}
}

#endif