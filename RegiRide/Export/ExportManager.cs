namespace RegiRide.Export
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using RegiRide.Model;

    public class ExportManager
    {
        private readonly RideLineExporter rideLineExporter;

        private readonly ExportHeaderGenerator headerGenerator;

        public ExportManager(RideLineExporter rideLineExporter, ExportHeaderGenerator headerGenerator)
        {
            this.rideLineExporter = rideLineExporter;
            this.headerGenerator = headerGenerator;
        }

        public string GenerateExport(IEnumerable<Ride> ridesToExport)
        {
            var exportString = new StringBuilder();
            
            exportString.AppendLine(headerGenerator.Generate());
            foreach (Ride ride in ridesToExport)
            {
                exportString.AppendLine(this.rideLineExporter.GenerateLine(ride));
            }

            return exportString.ToString();
        }

        public string GenerateExportFileName()
        {
            return string.Format("RegiRide_Report_{0:yyyy-MM-dd-hh-mm}.txt", DateTime.Now);
        }
    }
}