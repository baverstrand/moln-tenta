namespace EpiCalc.Configuration
{
    public interface IEpiCalcDbSettings
    {
        string CosmosDbConnectionString { get; set; }
        string DbName { get; set; }
    }
}
