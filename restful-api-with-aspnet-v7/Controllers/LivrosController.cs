using restful_api_with_aspnet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace restful_api_with_aspnet.Controllers
{
    //[Route("api/[controller]")]
    public class LivrosController : Controller
    {
        /*private readonly MySQLContext _context;

        public LivrosController(MySQLContext context)
        {
            _context = context;    
        }

        [HttpGet(Name = "/Livros")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Livros.ToListAsync());
        }

        [HttpGet(Name = "/Livros/Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livros
                .SingleOrDefaultAsync(m => m.Id == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        [HttpGet(Name = "/Livros/Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Autor,Preco,Lancamento")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(livro);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(livro);
        }

        [HttpGet(Name = "/Livros/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livros.SingleOrDefaultAsync(m => m.Id == id);
            if (livro == null)
            {
                return NotFound();
            }
            return View(livro);
        }

        [HttpPost(Name = "/Livros/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Autor,Preco,Lancamento")] Livro livro)
        {
            if (id != livro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(livro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivroExists(livro.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(livro);
        }

        [HttpGet(Name = "/Livros/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livros
                .SingleOrDefaultAsync(m => m.Id == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        [HttpPost(Name = "/Livros/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livro = await _context.Livros.SingleOrDefaultAsync(m => m.Id == id);
            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool LivroExists(int id)
        {
            return _context.Livros.Any(e => e.Id == id);
        }*/
    }
}
