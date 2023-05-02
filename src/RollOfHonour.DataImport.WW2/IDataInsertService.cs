namespace RollOfHonour.DataImport.WW2;

interface IDataInsertService
{
    Task<List<WW2Data>> AddMissingMilitaryData(List<WW2Data> results);
    Task<List<WW2Data>> AddMissingCivilianData(List<WW2Data> results);
}
