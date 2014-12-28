using System;
using NUnit.Framework;

namespace MetricsNet.Tests
{
    [TestFixture]
    public class InstrumentationTests
    {

        [Test]
        public void Should_Return_Cpu_Usages_by_Cores()
        {
            var cpus = Instrumentation.GetCpuUsageByCores();
            Assert.IsTrue(cpus.Count > 0);
        }

        [Test]
        public void Should_Return_Cpu_Total_Usage()
        {
            var total = Instrumentation.GetTotalCpuUsage();
            Assert.IsTrue(total >= 0);
        }

        [Test]
        public void Should_Return_OSName()
        {
            var name = Instrumentation.GetOSName();
            Console.WriteLine(name);
            Assert.IsTrue(name != "unknown");
        }

        [Test]
        public void Should_Return_Number_of_Cpus()
        {
            var number = Instrumentation.GetNumberOfLogicalProcessors();
            Assert.IsTrue(number>0);
        }

        [Test]
        public void Should_Return_True_For_64bits()
        {
            var _64bits = Instrumentation.Is64BitOperatingSystem();
            Assert.IsInstanceOfType(typeof(bool), _64bits );
        }

        [Test]
        public void Should_Return_Number_Of_Threads()
        {
            var t = Instrumentation.GetNumberOfThreads();
            Assert.IsTrue(t>0);
        }

        [Test]
        public void Should_Return_Total_Memory_Used()
        {
            var t = Instrumentation.GetPhysicalUsedMemory();
            Assert.IsTrue(t > 0);
        }

        [Test]
        public void Should_Return_Total_Memory_Used_In_Percentage()
        {
            var t = Instrumentation.GetPhysicalUsedMemoryInPercentage();
            Assert.IsTrue(t > decimal.MinValue);
        }

        [Test]
        public void Should_Return_Total_Memory_Available()
        {
            var t = Instrumentation.GetPhysicalAvailableMemory();
            Assert.IsTrue(t > 0);
        }

        [Test]
        public void Should_Return_Total_Memory_Available_in_Percentage()
        {
            var t = Instrumentation.GetPhysicalAvailableMemoryInPercentage();
            Assert.IsTrue(t > 0);
        }
    }
}
