using StudentManagement.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace StudentManagement.Services
{
    public class StudentService 
    {
        private readonly IMongoCollection<Student> _students;
        /*public StudentService(IStudentStoreDatabaseSettings settings, IMongoClient mongoClient) 
        {
            var database=mongoClient.GetDatabase(settings.DatabaseName);
            _students = database.GetCollection<Student>(settings.StudentCoursesCollectionName);

        }*/
        public StudentService(IOptions<StudentStoreDatabaseSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString); _students = mongoClient.GetDatabase(options.Value.DatabaseName)
            .GetCollection<Student>(options.Value.StudentCoursesCollectionName);
        }

        public Student Create(Student student)
        {
            _students.InsertOne(student);
            return student;
        }

        public List<Student> Get()
        {
            return _students.Find(student => true).ToList();
        }

        public Student Get(string id)
        {
            return _students.Find(student => student.Id==id).FirstOrDefault();
        }

        public void Remove(string id)
        {
             _students.DeleteOne(student => student.Id == id);
        }

        public void Update(string id, Student student)
        {
            _students.ReplaceOne(student => student.Id == id,student);
        }
    }
}
