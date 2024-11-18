using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotizAPI.Data;
using NotizAPI.Models;

namespace NotizAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotizController : ControllerBase
    {
        // Dependency Injection für den Datenbankkontext
        private readonly NotizenContext _context;
    // Dependency Injection für den Datenbankkontext
        public NotizController(NotizenContext context)
        {
            _context = context;
        }
        // GET: api/notizen
  [HttpGet]
    public async Task<ActionResult<IEnumerable<Notiz>>> GetNotizen()
    {
        // Verwendt die Notizen-Eigenschaft des Datenbankkontexts, um alle Notizen aus der Datenbank abzurufen
        return await _context.notizen.ToListAsync();
    }
// GET: api/notizen/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Notiz>> GetNotizById(int id)
    {
        // Verwenden die Notizen-Eigenschaft des Datenbankkontexts, um eine Notiz anhand ihrer ID aus der Datenbank abzurufen
        var notiz = await _context.notizen.FindAsync(id);
        if (notiz == null)
        {
            // Wenn die Notiz nicht gefunden wurde, gibt er eine 404-Fehlermeldung zurück
            return NotFound();
        }
        // Wenn die Notiz gefunden wurde, gibt er ihn zurück
        return notiz;
    }

    // POST: api/notizen
    [HttpPost]
    public async Task<ActionResult<Notiz>> CreateNotiz(Notiz notiz)
    {
        // Fügt die neue Notiz zur Datenbank hinzu
        _context.notizen.Add(notiz);
        // Speichert die Änderungen in der Datenbank
        await _context.SaveChangesAsync();

        // Geben die erstellte Notiz zurück
        return CreatedAtAction(nameof(GetNotizById), new { id = notiz.Id }, notiz);
    }

    // PUT: api/notizen/{id}
    [HttpPut("{id}")]
    
    public async Task<IActionResult> UpdateNotiz(int id, Notiz updatedNotiz)
    {
        // Überprüft ob die Notiz mit der angegebenen ID existiert
        if (id != updatedNotiz.Id)
        {
            return BadRequest();
        }
        // Altualisiert die Notiz in der Datenbank
        updatedNotiz.UpdatedAt = DateTime.Now;
        // Altualisiert den Status der Notiz im Datenbankkontext
        _context.Entry(updatedNotiz).State = EntityState.Modified;
    /// Speichern die Änderungen in der Datenbank
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.notizen.Any(e => e.Id == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }
    // DELETE: api/notizen/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNotiz(int id)
    {
        // Überprüft ob die Notiz mit der angegebenen ID existiert
        var notiz = await _context.notizen.FindAsync(id);
        if (notiz == null)
        {
            return NotFound();
        }
        // Löschen die Notiz aus der Datenbank
        _context.notizen.Remove(notiz);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    }
}