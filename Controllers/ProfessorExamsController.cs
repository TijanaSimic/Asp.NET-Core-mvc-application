using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Models.Database;
using Project.Models;

namespace Project.Controllers
{
    public class ProfessorExamsController : Controller
    {
        private readonly ProjectContext _context;

        public ProfessorExamsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: StudentExam
        public IActionResult Index()
        {
            ViewData["professors"] = new SelectList(_context.professors, "idProf", "fullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("idProf,date1,date2")] ProfessorExams professorExams)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Details", "ProfessorExams", professorExams);
            }
            ViewData["professors"] = new SelectList(_context.professors, "idProf", "fullName");
            return View();
        }


        // GET: Professor/Details/5
        public async Task<IActionResult> Details(ProfessorExams professorExams)
        {


            var professor = await _context.professors
                .FirstOrDefaultAsync(m => m.idProf == professorExams.idProf);


            IList<ProfessorSubject> subjects = await _context.professorSubjects
            .Include(sub => sub.subject)
            .Where(subject => subject.idProf == professorExams.idProf).ToListAsync();

            IList<Exam> exams = new List<Exam>();
            foreach (var item in subjects)
            {

                IList<Exam> tempExam = await _context.exams.Where(exam => exam.idSubj == item.idSubj && exam.date >= professorExams.date1 && exam.date <= professorExams.date2).ToListAsync();
                foreach (var temp in tempExam)
                {
                    exams.Add(temp);
                }
            }
            IList<StudentExam> studentExams = new List<StudentExam>();
            foreach (var item in exams)
            {

                IList<StudentExam> tempStudentExam = await _context.studentExams
                .Include(student => student.student)
                .Where(studExam => studExam.idExam == item.idExam && studExam.mark > 5).ToListAsync();
                foreach (var temp in tempStudentExam)
                {
                    studentExams.Add(temp);

                }
            }


            return View(studentExams);
        }
    }

}