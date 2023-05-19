using DeadLine.Models;

namespace DeadLine.Repos{
    class CourseRepo : ICourseRepo
    {
        public Task<object> AddCourse(AddCourseDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetCourse(int id)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetDeadlines(int id)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetDiscussions(int id)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetProfessorCourses(int id)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetStudentCourses(int id)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetStudents(int id)
        {
            throw new NotImplementedException();
        }

        public Task<object> JoinCourse(int studentId, JoinCourseDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}