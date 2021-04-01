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
    public class ExamController : Controller
    {
        private readonly ProjectContext _context;

        public ExamController(ProjectContext context)
        {
            _context = context;
        }

        // GET: Exam
        public async Task<IActionResult> Index()
        {   
               var examContext = _context.exams.Include(b => b.subject);
            return View(await examContext.ToListAsync());
        }

        // GET: Exam/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _context.exams
            .Include(b => b.subject)
                .FirstOrDefaultAsync(m => m.idExam == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // GET: Exam/Create
        public IActionResult Create()
        {
            IList<Subject> query = _context.subjects
              .Where(sub => sub.activity == "active").ToList();
            ViewData["subjects"] = new SelectList(query, "idSub", "name");
            return View();
        }

        // POST: Exam/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idExam,date,idSubj")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                Subject subject  =  await _context.subjects 
                .FirstOrDefaultAsync(m => m.idSub == exam.idSubj);
                Exam newExam = new Exam () {
              
                            idExam = exam.idExam,
                            date = exam.date, 
                            subject =await _context.subjects 
                .FirstOrDefaultAsync(m => m.idSub == exam.idSubj),
                            idSubj =exam.idSubj,
                            subjName = subject.name  
                };
                _context.Add(newExam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["subjects"] = new SelectList(_context.subjects, "idSub", "name");
            return View(exam);
        }

        // GET: Exam/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _context.exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }
             ViewData["subjects"] = new SelectList(_context.subjects, "idSub", "name",exam.idSubj);
            return View(exam);
        }

        // POST: Exam/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idExam,date,idSubj")] Exam exam)
        {
            if (id != exam.idExam)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    Subject subject = await _context.subjects 
                .FirstOrDefaultAsync(m => m.idSub == exam.idSubj);
                    exam.subject = subject;
                    exam.subjName = subject.name;
                    _context.Update(exam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamExists(exam.idExam))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                 ViewData["subjects"] = new SelectList(_context.subjects, "idSub", "name",exam.idSubj);
                return RedirectToAction(nameof(Index));
            }
            return View(exam);
        }

        // GET: Exam/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _context.exams
            .Include(subject => subject.subject)
                .FirstOrDefaultAsync(m => m.idExam == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // POST: Exam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exam = await _context.exams.FindAsync(id);
            _context.exams.Remove(exam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool ExamExists(int id)
        {
            return _context.exams.Any(e => e.idExam == id);
        }

        

    }
}
