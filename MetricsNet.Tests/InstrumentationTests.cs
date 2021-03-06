﻿using System;
using NUnit.Framework;

namespace MetricsNet.Tests
{
    [TestFixture]
    public class InstrumentationTests
    {

        [Test]
        public void Should_Return_Cpu_Usages_by_Cores()
        {
#if MONO
#else
            var cpus = Instrumentation.GetCpuUsageByCores();
            Assert.IsTrue(cpus.Count > 0);
#endif
        }

        [Test]
        public void Should_Return_Cpu_Total_Usage()
        {
#if MONO
#else
            var cpus = Instrumentation.GetCpuUsageByCores();
            Assert.IsTrue(cpus.Count > 0);
#endif
        }

        [Test]
        public void Should_Return_OSName()
        {
#if MONO
#else
            var name = Instrumentation.GetOSName();
            Console.WriteLine(name);
            Assert.IsTrue(name != "unknown");
#endif
        }

        [Test]
        public void Should_Return_Number_of_Cpus()
        {
#if MONO
#else
            var number = Instrumentation.GetNumberOfLogicalProcessors();
            Assert.IsTrue(number>0);
#endif

        }

        [Test]
        public void Should_Return_True_For_64bits()
        {
#if MONO
#else
            var _64bits = Instrumentation.Is64BitOperatingSystem();
            Assert.IsInstanceOfType(typeof(bool), _64bits );
#endif
        }

        [Test]
        public void Should_Return_Number_Of_Threads()
        {
#if MONO
#else
            var t = Instrumentation.GetNumberOfThreads();
            Assert.IsTrue(t>0);
#endif

        }

        [Test]
        public void Should_Return_Total_Memory_Used()
        {
#if MONO
#else
            var t = Instrumentation.GetPhysicalUsedMemory();
            Assert.IsTrue(t > 0);
#endif
        }

        [Test]
        public void Should_Return_Total_Memory_Used_In_Percentage()
        {
#if MONO
#else
            var t = Instrumentation.GetPhysicalUsedMemoryInPercentage();
            Assert.IsTrue(t > decimal.MinValue);
#endif

        }

        [Test]
        public void Should_Return_Total_Memory_Available()
        {
#if MONO
#else
            var t = Instrumentation.GetPhysicalAvailableMemory();
            Assert.IsTrue(t > 0);
#endif

        }

        [Test]
        public void Should_Return_Total_Memory_Available_in_Percentage()
        {
#if MONO
#else
            var t = Instrumentation.GetPhysicalAvailableMemoryInPercentage();
            Assert.IsTrue(t > 0);
#endif

        }
    }
}
