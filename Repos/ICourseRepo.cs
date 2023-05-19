using DeadLine.Models; 
namespace DeadLine.Repos
{
    interface ICourseRepo
    {
        public Task<object> AddCourse(AddCourseDTO dto);
        public Task<object> GetCourse(int id);

        public Task<object> JoinCourse(int studentId, JoinCourseDTO dto);
        public Task<object> GetStudents(int id);

        public Task<object> GetDeadlines(int id);
        public Task<object> GetDiscussions(int id);
        public Task<object> GetProfessorCourses(int id);
        public Task<object> GetStudentCourses(int id);

    }
}