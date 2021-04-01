using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Models.Database;

namespace Project.Controllers
{
    public class StudentExamController : Controller
    {
        private readonly ProjectContext _context;

        public StudentExamController(ProjectContext context)
        {
            _context = context;
        }

        // GET: StudentExam
        public async Task<IActionResult> Index()
        {
            IList<StudentExam> studentExams = await _context.studentExams.
            Include(student=>student.student).
            Include(exam => exam.exam).
            Where(m => m.mark==0)
            .ToListAsync();
            return View(studentExams);
        }

        // GET: StudentExam/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentExam = await _context.studentExams
                .FirstOrDefaultAsync(m => m.idStud == id);
            if (studentExam == null)
            {
                return NotFound();
            }

            return View(studentExam);
        
        }


        
          private bool DoesStudentExamExists (int idStud, int idExam) {
            return this._context.studentExams.Where ( 
                student => student.idStud== idStud && 
                           student.idExam == idExam
            ).Any ( );
        }

          private bool DoesStudentExist (int index) {
            return this._context.students.Where ( 
                student => student.idStud== index
            ).Any ( );
        }



        

        // GET: StudentExam/Create
        public IActionResult Create()
        {
           ViewData ["subjects"] = new SelectList(_context.exams,"idExam","nameDate");
            return View();
        }

          public int validateExams(int index, DateTime date, int idSub ) {
              IList<StudentExam> query = _context.studentExams
              .Include(studentExam => studentExam.exam)
              .Where(studentExam => studentExam.idStud == index &&
                                studentExam.exam.idSubj == idSub
                                ).ToList();
                int number = 0;
               foreach (StudentExam item in query)
               {
                   if((date- item.exam.date).Days<365) number++;
               }
               return number;
                          
              
            }

        // POST: StudentExam/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idStud,idExam")] StudentExam studentExam)
        {
            if (ModelState.IsValid)
            {
                
                if(DoesStudentExist(studentExam.idStud)) {
                
                if(!DoesStudentExamExists(studentExam.idStud, studentExam.idExam)) {
                 Exam exam = await _context.exams.FirstOrDefaultAsync(m => m.idExam == studentExam.idExam);
               if(validateExams(studentExam.idStud,exam.date,exam.idSubj) <5) {
                  studentExam.student = await _context.students
                .FirstOrDefaultAsync(m => m.idStud == studentExam.idStud);
                  studentExam.exam = await _context.exams
                .FirstOrDefaultAsync(m => m.idExam == studentExam.idExam);
                studentExam.mark = 0;
                _context.Add(studentExam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
              }
              else {
                     ModelState.AddModelError ("", "Student has already registered for this subject 5 times this year.");
              }
            }
            else {
                 ModelState.AddModelError ("", "Student has already registered for this exam.");
            }
                }
            else {
               ModelState.AddModelError ("", "Student with this index does not exist in database.");
            } }
            ViewData ["subjects"] = new SelectList(_context.exams,"idExam","nameDate");
            return View(studentExam);
        }

        // GET: StudentExam/Edit/5
        public async Task<IActionResult> Edit(int idStud, int idExam)
        {
           

            var studentExam = await _context.studentExams.Where(temp => temp.idStud == idStud && temp.idExam == idExam).FirstOrDefaultAsync();
            if (studentExam == null)
            {
                return NotFound();
            }
            List<int> niz = new List<int>();
            niz.Add(5);
            niz.Add(6);
            niz.Add(7);
            niz.Add(8);
            niz.Add(9);
            niz.Add(10);
            ViewData["grades"] = new SelectList(niz,niz[0]);
         return View(studentExam);
        }

        // POST: StudentExam/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("idStud,idExam,mark")] StudentExam studentExam)
        {
            

            if (ModelState.IsValid)
            {
                try
                { 
                    _context.Update(studentExam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExamExists(studentExam.idStud))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }   
            List<int> niz = new List<int>();
            niz.Add(5);
            niz.Add(6);
            niz.Add(7);
            niz.Add(8);
            niz.Add(9);
            niz.Add(10);
            ViewData["grades"] = new SelectList(niz,niz[0]);
            return View(studentExam);
        }

        // GET: StudentExam/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentExam = await _context.studentExams
                .FirstOrDefaultAsync(m => m.idStud == id);
            if (studentExam == null)
            {
                return NotFound();
            }

            return View(studentExam);
        }

        // POST: StudentExam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentExam = await _context.studentExams.FindAsync(id);
            _context.studentExams.Remove(studentExam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExamExists(int id)
        {
            return _context.studentExams.Any(e => e.idStud == id);
        }
    }
}
