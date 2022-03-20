using EpiCalc.Service;

namespace EpiCalc.Configuration
{
    public class EpiCalcDbSettings : IEpiCalcDbSettings
    {
        public string CosmosDbConnectionString { get; set; }
        public string DbName { get; set; }
    }
}
