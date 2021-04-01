using System;
using Xunit;
using Project.Models;
using Project.Models.Database;
using Project.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

namespace Project.Tests
{

    public class TestClass
    {
        static DbContextOptions options = new DbContextOptionsBuilder<ProjectContext>().UseInMemoryDatabase(databaseName: "TestDB").Options;
        static ProjectContext _context = new ProjectContext(options);
        static StudentController controller = new StudentController(_context);
        static StudentExamController studentExamController = new StudentExamController(_context);

        [Fact]
        public async Task IndexTest()
        {
            IActionResult action = await controller.Index();
            Assert.IsType<ViewResult>(action);
        }
        [Fact]
        public async Task CreateTest()
        {
            //Arrange

            _context.RemoveRange(_context.students);

            //ACT
            var student = new Student
            {
                idStud = 1,
                firstName = "Marko",
                lastName = "Markovic"
            };

            IActionResult action = await controller.Create(student);

            //ASSERT
            var stud = await _context.students.FirstOrDefaultAsync(c => c.idStud == student.idStud);

            Assert.Equal(student.firstName, stud.firstName);
            Assert.Equal(student.lastName, stud.lastName);
        }



        [Fact]
        public async Task DetailTest_IdNull()
        {
            IActionResult action = await controller.Details(null);

            Assert.IsType<NotFoundResult>(action);
        }



        [Fact]
        public async Task DetailTest_IdFromDB()
        {

            _context.RemoveRange(_context.students);
            var s = new Student
            {
                idStud = 1,
                firstName = "Marko",
                lastName = "Markovic"
            };

            _context.students.Add(s);
            _context.SaveChanges();


            ViewResult action = await controller.Details(s.idStud) as ViewResult;
            Assert.IsType<Student>(action.Model);
        }


        [Fact]
        public async Task DetailTest_IdNotFromDB()
        {
            _context.RemoveRange(_context.students);

            IActionResult action = await controller.Details(5);
            Assert.IsType<NotFoundResult>(action);
        }



        [Fact]
        public async Task EditTest_IdFromDB()
        {

            _context.RemoveRange(_context.students);
            var s = new Student
            {
                idStud = 1,
                firstName = "Marko",
                lastName = "Markovic"
            };

            _context.students.Add(s);
            _context.SaveChanges();


            s.firstName = "Milos";
            s.lastName = "Milosevic";


            IActionResult action = await controller.Edit(s.idStud, s);

            Student student = await _context.students.FirstOrDefaultAsync(m => m.idStud == s.idStud);

            Assert.Equal(s.firstName, student.firstName);
            Assert.Equal(s.lastName, student.lastName);
            Assert.IsType<RedirectToActionResult>(action);

        }



        [Fact]
        public async Task EditTest_IdNotFromDB()
        {

            _context.RemoveRange(_context.students);
            var s = new Student
            {
                idStud = 1,
                firstName = "Marko",
                lastName = "Markovic"
            };

            _context.students.Add(s);
            _context.SaveChanges();


            s.firstName = "Milos";
            s.lastName = "Milosevic";


            Assert.IsType<NotFoundResult>(await controller.Edit(2, s));

        }


        [Fact]
        public async Task DeleteTest_IdNotFromDB()
        {

            _context.RemoveRange(_context.students);


            Assert.IsType<NotFoundResult>(await controller.Delete(2));

        }
        [Fact]
        public async Task DeleteTest_IdNull()
        {

            _context.RemoveRange(_context.students);


            Assert.IsType<NotFoundResult>(await controller.Delete(null));

        }
        [Fact]
        public async Task DeleteTest_IdFromDB()
        {

            _context.RemoveRange(_context.students);
            var s = new Student
            {
                idStud = 1,
                firstName = "Marko",
                lastName = "Markovic"
            };

            _context.students.Add(s);
            _context.SaveChanges();

            IActionResult action = await controller.DeleteConfirmed(s.idStud);

            Assert.IsType<RedirectToActionResult>(action);
            Assert.Equal(0, await _context.students.CountAsync());

        }


        [Fact]
        public async Task ValidateExamsTest()
        {
            _context.RemoveRange(_context.subjects);
            _context.RemoveRange(_context.exams);
            _context.RemoveRange(_context.studentExams);
            _context.RemoveRange(_context.students);

            Student student = new Student
            {
                idStud = 1,
                firstName = "Marko",
                lastName = "Markovic"
            };

            _context.students.Add(student);
            Subject subject = new Subject()
            {
                idSub = 1,
                name = "Math",
                activity = "active"
            };


            Exam exam = new Exam()
            {
                idExam = 1,
                date = DateTime.Now,
                idSubj = 1,
                subject = subject
            };

            _context.subjects.Add(subject);
            _context.exams.Add(exam);
            StudentExam studExam = new StudentExam()
            {
                idStud = 1,
                student = student,
                exam = exam,
                idExam = 1,
                mark = 0
            };
            _context.studentExams.Add(studExam);
            _context.SaveChanges();

            int number = studentExamController.validateExams(student.idStud, exam.date, exam.idSubj);

                      IActionResult action = await studentExamController.Create(studExam);

            Assert.Equal(1, number);
             Assert.IsType<ViewResult>(action);
            
        }



        [Fact]
         public async Task RegisterExamTest()
        {
            _context.RemoveRange(_context.subjects);
            _context.RemoveRange(_context.exams);
            _context.RemoveRange(_context.studentExams);
            _context.RemoveRange(_context.students);

            Student student = new Student
            {
                idStud = 1,
                firstName = "Marko",
                lastName = "Markovic"
            };

            _context.students.Add(student);
            Subject subject = new Subject()
            {
                idSub = 1,
                name = "Math",
                activity = "active"
            };


            Exam exam = new Exam()
            {
                idExam = 1,
                date = DateTime.Now,
                idSubj = 1,
                subject = subject
            };
            Exam exam2 = new Exam()
            {
                idExam = 2,
                date = DateTime.Now,
                idSubj = 1,
                subject = subject
            };
            Exam exam3 = new Exam()
            {
                idExam = 3,
                date = DateTime.Now,
                idSubj = 1,
                subject = subject
            };
            Exam exam4 = new Exam()
            {
                idExam = 4,
                date = DateTime.Now,
                idSubj = 1,
                subject = subject
            };
            Exam exam5 = new Exam()
            {
                idExam = 5,
                date = DateTime.Now,
                idSubj = 1,
                subject = subject
            };
              Exam exam6 = new Exam()
            {
                idExam = 6,
                date = DateTime.Now,
                idSubj = 1,
                subject = subject
            };

            _context.subjects.Add(subject);
            _context.exams.Add(exam2);
                _context.exams.Add(exam6);
            _context.exams.Add(exam3);
            _context.exams.Add(exam4);
            _context.exams.Add(exam);
            _context.exams.Add(exam5);
            StudentExam studExam = new StudentExam()
            {
                idStud = 1,
                student = student,
                exam = exam,
                idExam = 1,
                mark = 0
            };
            StudentExam studExam2 = new StudentExam()
            {
                idStud = 1,
                student = student,
                exam = exam2,
                idExam = 2,
                mark = 0
            };
            StudentExam studExam3 = new StudentExam()
            {
                idStud = 1,
                student = student,
                exam = exam3,
                idExam = 3,
                mark = 0
            };
            StudentExam studExam4 = new StudentExam()
            {
                idStud = 1,
                student = student,
                exam = exam4,
                idExam = 4,
                mark = 0
            };
            StudentExam studExam5 = new StudentExam()
            {
                idStud = 1,
                student = student,
                exam = exam5,
                idExam = 5,
                mark = 0
            };
            _context.studentExams.Add(studExam);
            _context.studentExams.Add(studExam2);
            _context.studentExams.Add(studExam3);
            _context.studentExams.Add(studExam4);
            _context.studentExams.Add(studExam5);
            _context.SaveChanges();

              StudentExam studExam6 = new StudentExam()
            {
                idStud = 1,
                student = student,
                exam = exam6,
                idExam = 6,
                mark = 0
            };
              IActionResult action = await studentExamController.Create(studExam6);
               Assert.IsNotType<RedirectToActionResult>(action);
            
        }





    }
}