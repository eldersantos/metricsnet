using System.Diagnostics;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetricsNet.Tests
{
    [TestClass]
    public class InstrumentationTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Should_Return_Cpu_Usages_by_Cores()
        {
            var cpus = Instrumentation.GetCpuUsageByCores();
            Assert.IsTrue(cpus.Count > 0);
        }

        [TestMethod]
        public void Should_Return_Cpu_Total_Usage()
        {
            var total = Instrumentation.GetTotalCpuUsage();
            TestContext.WriteLine(total.ToString(CultureInfo.InvariantCulture));
            Assert.IsTrue(total >= 0);
        }

        [TestMethod]
        public void Should_Return_Number_of_Cpus()
        {
            var number = Instrumentation.GetNumberOfLogicalProcessors();
            TestContext.WriteLine(number.ToString(CultureInfo.InvariantCulture));
            Assert.IsTrue(number>0);
        }

        [TestMethod]
        public void Should_Return_True_For_64bits()
        {
            var _64bits = Instrumentation.Is64BitOperatingSystem();
            TestContext.WriteLine(_64bits.ToString());
            Assert.IsInstanceOfType(_64bits, typeof(bool));
        }

        [TestMethod]
        public void Should_Return_Number_Of_Threads()
        {
            var t = Instrumentation.GetNumberOfThreads();
            TestContext.WriteLine(t.ToString(CultureInfo.InvariantCulture));
            Assert.IsTrue(t>0);
        }

        [TestMethod]
        public void Should_Return_Total_Memory_Used()
        {
            var t = Instrumentation.GetPhysicalUsedMemory();
            TestContext.WriteLine(t.ToString(CultureInfo.InvariantCulture));
            Assert.IsTrue(t > 0);
        }

        [TestMethod]
        public void Should_Return_Total_Memory_Used_In_Percentage()
        {
            var t = Instrumentation.GetPhysicalUsedMemoryInPercentage();
            TestContext.WriteLine(t.ToString(CultureInfo.InvariantCulture));
            Assert.IsTrue(t > decimal.MinValue);
        }

        [TestMethod]
        public void Should_Return_Total_Memory_Available()
        {
            var t = Instrumentation.GetPhysicalAvailableMemory();
            TestContext.WriteLine(t.ToString(CultureInfo.InvariantCulture));
            Assert.IsTrue(t > 0);
        }

        [TestMethod]
        public void Should_Return_Total_Memory_Available_in_Percentage()
        {
            var t = Instrumentation.GetPhysicalAvailableMemoryInPercentage();
            TestContext.WriteLine(t.ToString(CultureInfo.InvariantCulture));
            Assert.IsTrue(t > 0);
        }
    }
}
