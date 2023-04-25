namespace RollOfHonour.DataImport.WW2;

interface IDataInsertService
{
    Task<List<WW2Data>> AddMissingData(List<WW2Data> results);
}
