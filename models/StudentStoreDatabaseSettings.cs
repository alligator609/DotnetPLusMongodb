namespace DotnetPLusMongodb.models
{
    public class StudentStoreDatabaseSettings : IStudentStoreDatabaseSettings
    {
        public string StudentCoursesCollections { get; set ; } = string.Empty;
        public string ConnectionString { get ; set ; } = string.Empty;
        public string DatabaseName { get ; set ; } = string.Empty;
    }
}
