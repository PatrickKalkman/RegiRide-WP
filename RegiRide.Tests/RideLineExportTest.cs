namespace RegiRide.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RegiRide.Export;
    using RegiRide.Model;

    [TestClass]
    public class RideLineExporterTest
    {
        [TestMethod]
        public void FullExportRideShouldGenerateSingleCommaSeparatedLine()
        {
            var rideLineExporter = new RideLineExporter(new RideTypeConverter());
            string rideExportLine = rideLineExporter.GenerateLine(RideGeneratorMother.CreateFullDummyRide());
            Assert.AreEqual(
                "2010/12/11,Hinttech,Delftechpark 37i,2628 XJ,Delft,Mediacatalyst,Herengracht 182,1016 BR,Amsterdam,153800,153875,Business,Via schiphol to prevent getting stuck in traffic jam", 
                rideExportLine);
        }

        [TestMethod]
        public void ExportRideWithEmptyFieldsShouldGenerateSingleCommaSeparatedLine()
        {
            var rideLineExporter = new RideLineExporter(new RideTypeConverter());
            string rideExportLine = rideLineExporter.GenerateLine(RideGeneratorMother.CreateDummyRideWithEmptyFields());
            Assert.AreEqual(
                "2010/12/11,Hinttech,Delftechpark 37i,,,,,1016 BR,Amsterdam,153800,153875,Business,",
                rideExportLine);
        }
    }
}