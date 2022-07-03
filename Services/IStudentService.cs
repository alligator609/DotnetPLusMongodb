using DotnetPLusMongodb.models;
using System.Collections.Generic;

namespace DotnetPLusMongodb.Services
{
    public interface IStudentService
    {
        List<Student> Get();
        Student Get(string id);
        Student Create(Student student);
        void Update(string id,Student student);
        void Remove(string id, Student student);


    }
}
