namespace RegiRide.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RegiRide.Export;
    using RegiRide.Model;

    [TestClass]
    public class RideExportManagerTest
    {
        [TestMethod]
        public void GenerateExport_Should_Generate_Line_For_Every_Ride()
        {
            const int NumberOfLinesToGenerate = 10;
            var mock = new RideLineExporterMock();
            var exportManager = new ExportManager(mock, new ExportHeaderGenerator());
            string exportFile = exportManager.GenerateExport(RideGeneratorMother.CreateRideList(NumberOfLinesToGenerate));
            Assert.AreEqual(NumberOfLinesToGenerate, mock.NumberOfLinesGenerated);
        }

        internal class RideLineExporterMock : RideLineExporter
        {
            public RideLineExporterMock() : base(new RideTypeConverter())
            {
            }

            public int NumberOfLinesGenerated { get; set; }

            public override string GenerateLine(Ride rideToExport)
            {
                NumberOfLinesGenerated++;
                return string.Empty;
            }
        }
    }
}