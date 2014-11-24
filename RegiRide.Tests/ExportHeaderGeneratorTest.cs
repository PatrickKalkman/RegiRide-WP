namespace RegiRide.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RegiRide.Export;
    using RegiRide.Resources;

    [TestClass]
    public class ExportHeaderGeneratorTest
    {
        [TestMethod]
        public void Generate_Should_Return_Export_Header_String()
        {
            var exportHeaderGenerator = new ExportHeaderGenerator();
            string header = exportHeaderGenerator.Generate();
            Assert.AreEqual(AppResources.ExportHeader, header);
        }
    }
}
