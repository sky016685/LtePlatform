// Copyright 2004-2009 Castle Project - http://www.castleproject.org/
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
using System.IO;
using Castle.Core.Logging;
using NUnit.Framework;

#if !SILVERLIGHT // In Silverlight Console.SetOut throws security exception
namespace Castle.Core.Test.Main
{
    [TestFixture]
	public class ConsoleLoggerTestCase
	{
		private readonly StringWriter outWriter = new StringWriter();
		private readonly StringWriter errorWriter = new StringWriter();

#if FEATURE_XUNITNET
		public ConsoleLoggerTestCase()
#else
		[SetUp]
		public void ReplaceOut()
#endif
		{
			outWriter.GetStringBuilder().Length = 0;
			errorWriter.GetStringBuilder().Length = 0;

			Console.SetOut(outWriter);
			Console.SetError(errorWriter);
		}

		[Test]
		public void InfoLogger()
		{
			var log = new ConsoleLogger("Logger", LoggerLevel.Info);

			log.Debug("Some debug message");
			log.Info("Some info message");
			log.Error("Some error message");
			log.Fatal("Some fatal error message");
			log.Warn("Some warn message");

			var logcontents = outWriter.GetStringBuilder().ToString();
			
			var expected = new StringWriter();
			expected.WriteLine("[Info] 'Logger' Some info message");
			expected.WriteLine("[Error] 'Logger' Some error message");
			expected.WriteLine("[Fatal] 'Logger' Some fatal error message");
			expected.WriteLine("[Warn] 'Logger' Some warn message");
			
			Assert.AreEqual(expected.GetStringBuilder().ToString(), logcontents, "logcontents don't match");
		}

		[Test]
		public void DebugLogger()
		{
			var log = new ConsoleLogger("Logger", LoggerLevel.Debug);

			log.Debug("Some debug message");
			log.Info("Some info message");
			log.Error("Some error message");
			log.Fatal("Some fatal error message");
			log.Warn("Some warn message");

			var logcontents = outWriter.GetStringBuilder().ToString();
			
			var expected = new StringWriter();
			expected.WriteLine("[Debug] 'Logger' Some debug message");
			expected.WriteLine("[Info] 'Logger' Some info message");
			expected.WriteLine("[Error] 'Logger' Some error message");
			expected.WriteLine("[Fatal] 'Logger' Some fatal error message");
			expected.WriteLine("[Warn] 'Logger' Some warn message");

			Assert.AreEqual(expected.GetStringBuilder().ToString(), logcontents, "logcontents don't match");
		}

		[Test]
		public void WarnLogger()
		{
			var log = new ConsoleLogger("Logger", LoggerLevel.Warn);

			log.Debug("Some debug message");
			log.Info("Some info message");
			log.Error("Some error message");
			log.Fatal("Some fatal error message");
			log.Warn("Some warn message");

			var logcontents = outWriter.GetStringBuilder().ToString();
			
			var expected = new StringWriter();
			expected.WriteLine("[Error] 'Logger' Some error message");
			expected.WriteLine("[Fatal] 'Logger' Some fatal error message");
			expected.WriteLine("[Warn] 'Logger' Some warn message");

			Assert.AreEqual(expected.GetStringBuilder().ToString(), logcontents, "logcontents don't match");
		}

		[Test]
		public void ExceptionLogging()
		{
			var log = new ConsoleLogger("Logger", LoggerLevel.Debug);

			log.Debug("Some debug message", new ApplicationException("Some exception message"));

			var logcontents = outWriter.GetStringBuilder().ToString();
			
			var expected = new StringWriter();
			expected.WriteLine("[Debug] 'Logger' Some debug message");
			expected.WriteLine("[Debug] 'Logger' System.ApplicationException: Some exception message ");

			Assert.AreEqual(expected.GetStringBuilder().ToString(), logcontents, "logcontents don't match");
		}
	}
}
#endif
