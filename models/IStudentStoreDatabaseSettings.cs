namespace DotnetPLusMongodb.models
{
    public interface IStudentStoreDatabaseSettings
    {
        string StudentCoursesCollections { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }


    }
}
