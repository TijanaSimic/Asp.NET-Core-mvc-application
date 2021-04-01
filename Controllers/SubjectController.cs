using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Models.Database;

namespace Project.Controllers
{
    public class SubjectController : Controller
    {

        private static int lastId = 1;
        private readonly ProjectContext _context;

        public SubjectController(ProjectContext context)
        {
            _context = context;
        }

        // GET: Subject
        public async Task<IActionResult> Index()
        {
            return View(await _context.subjects.ToListAsync());
        }

        // GET: Subject/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.subjects
            .Include(subjectProf=> subjectProf.professorSubjectList)
                .ThenInclude(subjects => subjects.professor)
                .FirstOrDefaultAsync(m => m.idSub == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // GET: Subject/Create
        public IActionResult Create()
        {
            ViewData ["professors"] = new MultiSelectList(_context.professors,"idProf","fullName");
            return View();
        }

        
        // POST: Subject/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idSub,name,activity,professors")] Subject subject)
        {
            
            if (ModelState.IsValid)
               {
                if(subject.professorSubjectList==null) {
                    subject.professorSubjectList = new List<ProfessorSubject> ();
                }
              
                foreach(int professorId in subject.professors) {
                    subject.professorSubjectList.Add(
                        new ProfessorSubject () {
                            idProf = professorId,
                            subject = subject,
                            professor =await _context.professors 
                .FirstOrDefaultAsync(m => m.idProf == professorId),
                            idSubj =lastId

                        }
                    );
                }
                
                    
                _context.Add(subject);
                await _context.SaveChangesAsync();
                ++lastId;
                return RedirectToAction(nameof(Index));
            }
              ViewData ["professors"] = new MultiSelectList(_context.professors,"idProf","fullName");
         
            return View(subject);
        }

        // GET: Subject/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.subjects.FirstOrDefaultAsync(m=>m.idSub==id);
            if (subject == null)
            {
                return NotFound();
            }
               
             ViewData ["professors"] = new MultiSelectList(_context.professors,"idProf","fullName",subject.professors);
         
            return View(subject);
        }

        // POST: Subject/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idSub,name,activity,professors")] Subject subject)
        {
            if (id != subject.idSub)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subject);
                    //ucitavamo veznu tabelu nad entitetom subject
                    this._context.Entry(subject).Collection( 
                        subject=>subject.professorSubjectList
                    ).Load();
                    foreach (ProfessorSubject item in subject.professorSubjectList)
                    {
                        _context.professorSubjects.Remove(item);
                    }
                     foreach (int professorId in subject.professors) {
                        subject.professorSubjectList.Add(
                            new ProfessorSubject() {
                              idProf = professorId,
                            subject = subject,
                            professor =await _context.professors 
                .FirstOrDefaultAsync(m => m.idProf == professorId),
                            idSubj =subject.idSub      
                            }
                        );
                    }
                    {
                        
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectExists(subject.idSub))
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
            
                ViewData ["professors"] = new MultiSelectList(_context.professors,"idProf","fullName",subject.professors);
            return View(subject);
        }

        // GET: Subject/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.subjects
            .Include(subjectProf => subjectProf.professorSubjectList)
            .ThenInclude(prof => prof.professor)
                .FirstOrDefaultAsync(m => m.idSub == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST: Subject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           
            var subject = await _context.subjects
            .Include(exam =>exam.exams)
            .ThenInclude(studentExams => studentExams.studentExamList)
            .Include(subjectProf => subjectProf.professorSubjectList)
            .ThenInclude(prof => prof.professor)
         .FirstOrDefaultAsync(m => m.idSub == id);
                
            if(subject.exams!= null)
            foreach(Exam exam in subject.exams) {
                foreach (StudentExam studentExam in exam.studentExamList)
                {
                    if(studentExam.mark==0) 
                    _context.studentExams.Remove(studentExam);
                }
                _context.exams.Remove(exam);
            }
            List<ProfessorSubject> list = await _context.professorSubjects.Where(profSub => profSub.subject.idSub == id).ToListAsync();
            foreach (ProfessorSubject item in list)
            {
                _context.professorSubjects.Remove(item);
            }
            subject.activity = "inactive";
            _context.Update(subject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectExists(int id)
        {
            return _context.subjects.Any(e => e.idSub == id);
        }
    }
}
