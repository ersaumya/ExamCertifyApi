using ExamCertify.Application.Interfaces.Courses;
using ExamCertify.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamCertify.Infrastructure.Repositories
{
    public class CoursesRepository : ICourseRepository
    {
        private readonly ExamCertifyContext _examCertifyContext;

        public CoursesRepository(ExamCertifyContext examCertifyContext)
        {
            this._examCertifyContext = examCertifyContext;
        }

        public async Task AddCourseAsync(Course course)
        {
            _examCertifyContext.Courses.Add(course);
            await _examCertifyContext.SaveChangesAsync();
        }

        public async Task DeleteCourseAsync(Course course)
        {
            _examCertifyContext.Courses.Remove(course);
            await _examCertifyContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _examCertifyContext.Courses.ToListAsync();
            
        }

        public async Task<Course?> GetCourseByIdAsync(int courseId)
        {
            return await _examCertifyContext.Courses.FindAsync(courseId);
        }

        public async Task<bool> IsTitleDuplicateAsync(string title)
        {
            return await _examCertifyContext.Courses.AnyAsync(c => c.Title == title);
        }

        public async Task UpdateCourseAsync(Course course)
        {
            _examCertifyContext.Courses.Update(course);
            await _examCertifyContext.SaveChangesAsync();
        }

        public async Task UpdateDescriptionAsync(int courseId, string description)
        {
            var courses = await _examCertifyContext.Courses.FindAsync(courseId);
            if (courses == null) {
                throw new KeyNotFoundException("Course not found");
            }
            courses.Description= description;
            await _examCertifyContext.SaveChangesAsync();
        }
    }
}
