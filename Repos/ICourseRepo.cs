using DeadLine.Models; 
namespace DeadLine.Repos
{
    public interface ICourseRepo
    {
        public Task<object> AddCourse(string professorId, AddCourseDTO dto);
        public Task<object> GetCourse(string userId,int id);

        public Task<object> JoinCourse(string studentId, JoinCourseDTO dto);
        public Task<object> GetStudents(string userId, int id);

        public Task<object> GetDeadlines(int id);
        public Task<object> GetDiscussions(int id);
        public Task<object> GetProfessorCourses(string id);
        public Task<object> GetStudentCourses(string id);

    }
}