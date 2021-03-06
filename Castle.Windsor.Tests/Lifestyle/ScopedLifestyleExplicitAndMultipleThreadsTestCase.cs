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

using Castle.Windsor.Tests;

namespace CastleTests.Lifestyle
{
#if !SILVERLIGHT
	// this is not working in SL at all
	using System;
	using System.Threading;
#if !DOTNET35
	using System.Threading.Tasks;
#endif
	using Castle.MicroKernel.Lifestyle;
	using Castle.MicroKernel.Registration;

	using CastleTests.Components;

	using NUnit.Framework;

	[TestFixture]
	public class ScopedLifestyleExplicitAndMultipleThreadsTestCase : AbstractContainerTestCase
	{
		protected override void AfterContainerCreated()
		{
			Container.Register(Component.For<A>().LifestyleScoped());
		}

		[Test]
		public void Context_is_passed_onto_the_next_thread_Begin_End_Invoke()
		{
			using (Container.BeginScope())
			{
				var instance = default(A);
				var instanceFromOtherThread = default(A);
				instance = Container.Resolve<A>();
				var initialThreadId = Thread.CurrentThread.ManagedThreadId;
				Action action = () =>
				{
					Assert.AreNotEqual(Thread.CurrentThread.ManagedThreadId, initialThreadId);
					instanceFromOtherThread = Container.Resolve<A>();
				};

				var result = action.BeginInvoke(null, null);
				result.AsyncWaitHandle.WaitOne();
				Assert.AreSame(instance, instanceFromOtherThread);
			}
		}

		[Test]
		public void Context_is_NOT_visible_in_unrelated_thread_Begin_End_Invoke()
		{
			var startLock = new ManualResetEvent(false);
			var resolvedLock = new ManualResetEvent(false);
			var instanceFromOtherThread = default(A);
			var initialThreadId = Thread.CurrentThread.ManagedThreadId;
			Action action = () =>
			{
				using (Container.BeginScope())
				{
					startLock.WaitOne();
					Assert.AreNotEqual(Thread.CurrentThread.ManagedThreadId, initialThreadId);
					instanceFromOtherThread = Container.Resolve<A>();
					resolvedLock.Set();
				}
			};
			var result = action.BeginInvoke(null, null);
			using (Container.BeginScope())
			{
				startLock.Set();
				var instance = Container.Resolve<A>();
				resolvedLock.WaitOne();

				result.AsyncWaitHandle.WaitOne();
				Assert.AreNotSame(instance, instanceFromOtherThread);
			}
		}

#if !DOTNET35
		[Test]
		public void Context_is_passed_onto_the_next_thread_TPL()
		{
			using (Container.BeginScope())
			{
				var instance = default(A);
				var instanceFromOtherThread = default(A);
				instance = Container.Resolve<A>();
				var initialThreadId = Thread.CurrentThread.ManagedThreadId;
				var task = Task.Factory.StartNew(() =>
				{
					Assert.AreNotEqual(Thread.CurrentThread.ManagedThreadId, initialThreadId);
					instanceFromOtherThread = Container.Resolve<A>();
				});
				task.Wait();
				Assert.AreSame(instance, instanceFromOtherThread);
			}
		}
#endif

		[Test]
		public void Context_is_passed_onto_the_next_thread_ThreadPool()
		{
			using (Container.BeginScope())
			{
				var instance = default(A);
				var @event = new ManualResetEvent(false);
				var instanceFromOtherThread = default(A);
				instance = Container.Resolve<A>();
				var initialThreadId = Thread.CurrentThread.ManagedThreadId;
				var exceptionFromTheOtherThread = default(Exception);
				ThreadPool.QueueUserWorkItem(_ =>
				{
					Assert.AreNotEqual(Thread.CurrentThread.ManagedThreadId, initialThreadId);
					try
					{
						instanceFromOtherThread = Container.Resolve<A>();
					}
					catch (Exception e)
					{
						exceptionFromTheOtherThread = e;
					}
					finally
					{
						@event.Set();
					}
				});
				var signalled = @event.WaitOne(TimeSpan.FromSeconds(2));
				if (exceptionFromTheOtherThread != null)
				{
#if DOTNET45
					var capture = System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(exceptionFromTheOtherThread);
					capture.Throw();
#else
					throw exceptionFromTheOtherThread;
#endif
				}
				Assert.IsTrue(signalled, "The other thread didn't finish on time.");
				Assert.AreSame(instance, instanceFromOtherThread);
			}
		}

		[Test]
		public void Context_is_passed_onto_the_next_thread_explicit()
		{
			using (Container.BeginScope())
			{
				var instance = default(A);
				var @event = new ManualResetEvent(false);
				var instanceFromOtherThread = default(A);
				instance = Container.Resolve<A>();
				var initialThreadId = Thread.CurrentThread.ManagedThreadId;
				var exceptionFromTheOtherThread = default(Exception);
				var otherThread = new Thread(() =>
				{
					Assert.AreNotEqual(Thread.CurrentThread.ManagedThreadId, initialThreadId);
					try
					{
						instanceFromOtherThread = Container.Resolve<A>();
					}
					catch (Exception e)
					{
						exceptionFromTheOtherThread = e;
					}
					finally
					{
						@event.Set();
					}
				});
				otherThread.Start();
				var signalled = @event.WaitOne(TimeSpan.FromSeconds(2));
				if (exceptionFromTheOtherThread != null)
				{
#if DOTNET45
					var capture = System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(exceptionFromTheOtherThread);
					capture.Throw();
#else
					throw exceptionFromTheOtherThread;
#endif
				}
				Assert.IsTrue(signalled, "The other thread didn't finish on time.");
				Assert.AreSame(instance, instanceFromOtherThread);
			}
		}
	}
#endif
}