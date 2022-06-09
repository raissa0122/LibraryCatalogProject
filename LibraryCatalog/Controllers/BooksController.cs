using Data;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCatalog.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryCatalogContext _context;
        public BooksController(LibraryCatalogContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            var books = await _context.Books.ToListAsync();

            foreach (var item in books)
            {
                item.Author = _context.Authors.FirstOrDefault(t => t.Id == item.AuthorId);
                item.Genre = _context.Genres.FirstOrDefault(t => t.Id == item.GenreId);
                item.Topic = _context.Topics.FirstOrDefault(t => t.Id == item.TopicId);
            }
           
            return View(books);
        }

        public ActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Sobriquet");
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name");
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Title,YearOfPublishing,Price,AuthorId,GenreId,TopicId,Id")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Sobriquet",book.AuthorId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name",book.GenreId);
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Name",book.TopicId);
            return View(book);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Sobriquet", book.AuthorId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", book.GenreId);
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Name", book.TopicId);
            return View(book);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,YearOfPublishing,Price,AuthorId,GenreId,TopicId,Id")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BooksExists(book.Id))
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
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Sobriquet", book.AuthorId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", book.GenreId);
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Name", book.TopicId);
            return View(book);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(u => u.Author)
                .Include(u => u.Genre)
                .Include(u => u.Topic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool BooksExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }

    }
}
