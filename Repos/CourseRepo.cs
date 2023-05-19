using DeadLine.DataProvide;
using DeadLine.Models;

namespace DeadLine.Repos
{
    public class CourseRepo : ICourseRepo
    {
        private readonly DeadlineContext _context;
        private readonly ApplicationDbContext _userContext;

        public CourseRepo(DeadlineContext context, ApplicationDbContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public async Task<object> AddCourse(string professorId, AddCourseDTO dto)
        {
            var exist = _context.Courses
                .Where(x => x.ShareId == dto.ShareId || x.Name == dto.Title)
                .FirstOrDefault();
            if (exist != null)
                throw new InvalidOperationException("course is alreaddy defined");

            await _context.Courses.AddAsync(
                new Course
                {
                    ProfessorId = professorId,
                    Name = dto.Title,
                    ShareId = dto.ShareId,
                    Password = dto.Password,
                    CreatedDate = DateTime.Now,
                    MaxSize = dto.MaxSize,
                    CourseStatus = 1,
                    Description = dto.Description
                }
            );
            await _context.SaveChangesAsync();
            return new { message = "Added Sucessfully" };
        }

        public async Task<object> GetCourse(string userId, int id)
        {
            var course = _context.Courses.Where(c => c.Id == id).FirstOrDefault();
            if (course == null)
                throw new InvalidDataException("course is not founded");

            var students = _context.StudentCourses.Where(sc => sc.StudentId == userId && sc.CourseId == id).FirstOrDefault();
            if (students == null && course.ProfessorId != userId)
                throw new InvalidDataException("course is not permitted");

            return new
            {
                course.Id,
                course.Name,
                course.CourseStatus,
                course.CreatedDate,
                course.Description,
                course.MaxSize,
                course.ProfessorId,
                course.ShareId
            };
        }

        public async Task<object> GetDeadlines(string userId, int id)
        {
            var course = _context.Courses.Where(c => c.Id == id).FirstOrDefault();
            if (course == null)
                throw new InvalidDataException("course is not founded");

            var students = _context.StudentCourses.Where(sc => sc.StudentId == userId && sc.CourseId == id).FirstOrDefault();
            if (students == null && course.ProfessorId != userId)
                throw new InvalidDataException("operation is not permitted");
            
            var deadlines = (from d in course.Deadlines select d.Id).ToList();
            return deadlines;
        }

        public async Task<object> GetDiscussions(string userId, int id)
        {
            var course = _context.Courses.Where(c => c.Id == id).FirstOrDefault();
            if (course == null)
                throw new InvalidDataException("course is not founded");
            var discussions = (from d in course.Discussions select d.Id).ToList();
            return discussions;
        }

        public async Task<object> GetProfessorCourses(string id)
        {
            var courses = _context.Courses.Where(c => c.ProfessorId == id).ToList();
            if (courses == null)
                throw new InvalidDataException("course is not founded");
            return from course in courses
                   select new
                   {
                       course.Id,
                       course.Name,
                       course.CourseStatus,
                       course.CreatedDate,
                       course.Description,
                       course.MaxSize,
                       course.ProfessorId,
                       course.ShareId
                   };
        }

        public async Task<object> GetStudentCourses(string id)
        {
            var studentCourses = _context.StudentCourses.Where(c => c.StudentId == id).ToList();
            if (studentCourses == null)
                throw new InvalidDataException("course is not founded");
            return from sc in studentCourses
                   join course in _context.Courses on sc.CourseId equals course.Id
                   select new
                   {
                       course.Id,
                       course.Name,
                       course.CourseStatus,
                       course.CreatedDate,
                       course.Description,
                       course.MaxSize,
                       course.ProfessorId,
                       course.ShareId
                   };
        }

        public async Task<object> GetStudents(string userId, int id)
        {
            var c = _context.Courses.Where(c => c.ProfessorId == userId && c.Id == id).FirstOrDefault();
            var s = _context.StudentCourses.Where(sc => sc.StudentId == userId && sc.CourseId == id).FirstOrDefault();
            if (s == null && c == null)
                throw new InvalidDataException("data is not permitted");
            
            var courseStudents = _context.StudentCourses.Where(c => c.CourseId == id).ToList();
            var users = _userContext.Users.ToList();
            return (from cc in courseStudents
                    join student in users on cc.StudentId.ToString() equals student.Id
                    select new
                    {
                        student.Id,
                        student.UserName,
                        student.FirstName,
                        student.LastName
                    }).ToList();
        }

        public async Task<object> JoinCourse(string studentId, JoinCourseDTO dto)
        {
            var course = _context.Courses.Where(c => c.ShareId == dto.ShareId).FirstOrDefault();
            if (course == null) throw new InvalidDataException("course is not founded");
            if (course.Password != dto.Password) throw new InvalidOperationException("password is not correct");
            var exist = _context.StudentCourses.Where(c => c.CourseId == course.Id && c.StudentId == studentId).FirstOrDefault();
            if(exist != null) throw new InvalidOperationException("already added");
            
            _context.StudentCourses.Add(new StudentCourse { CourseId = course.Id, StudentId = studentId ,JoinDate= DateTime.Now});
            await _context.SaveChangesAsync();
            return new { message = "Added Sucessfully" };

        }
    }
}
