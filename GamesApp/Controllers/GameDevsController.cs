using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GamesApp.Models;

namespace GamesApp.Controllers
{
    public class GameDevsController : Controller
    {
        private readonly GamesDBContext _context;

        public GameDevsController(GamesDBContext context)
        {
            _context = context;
        }

        // GET: GameDevs
        public async Task<IActionResult> Index()
        {
            var gamesDBContext = _context.GameDev.Include(g => g.Developer).Include(g => g.Game);
            return View(await gamesDBContext.ToListAsync());
        }

        // GET: GameDevs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameDev = await _context.GameDev
                .Include(g => g.Developer)
                .Include(g => g.Game)
                .SingleOrDefaultAsync(m => m.GameDevId == id);
            if (gameDev == null)
            {
                return NotFound();
            }

            return View(gameDev);
        }

        // GET: GameDevs/Create
        public IActionResult Create()
        {
            ViewData["DeveloperId"] = new SelectList(_context.Developers, "DeveloperId", "Name");
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "Title");
            return View();
        }

        // POST: GameDevs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameDevId,GameId,DeveloperId,ReleaseDate")] GameDev gameDev)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gameDev);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeveloperId"] = new SelectList(_context.Developers, "DeveloperId", "Name", gameDev.DeveloperId);
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "Title", gameDev.GameId);
            return View(gameDev);
        }

        // GET: GameDevs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameDev = await _context.GameDev.SingleOrDefaultAsync(m => m.GameDevId == id);
            if (gameDev == null)
            {
                return NotFound();
            }
            ViewData["DeveloperId"] = new SelectList(_context.Developers, "DeveloperId", "DeveloperId", gameDev.DeveloperId);
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "GameId", gameDev.GameId);
            return View(gameDev);
        }

        // POST: GameDevs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GameDevId,GameId,DeveloperId,ReleaseDate")] GameDev gameDev)
        {
            if (id != gameDev.GameDevId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameDev);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameDevExists(gameDev.GameDevId))
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
            ViewData["DeveloperId"] = new SelectList(_context.Developers, "DeveloperId", "DeveloperId", gameDev.DeveloperId);
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "GameId", gameDev.GameId);
            return View(gameDev);
        }

        // GET: GameDevs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameDev = await _context.GameDev
                .Include(g => g.Developer)
                .Include(g => g.Game)
                .SingleOrDefaultAsync(m => m.GameDevId == id);
            if (gameDev == null)
            {
                return NotFound();
            }

            return View(gameDev);
        }

        // POST: GameDevs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gameDev = await _context.GameDev.SingleOrDefaultAsync(m => m.GameDevId == id);
            _context.GameDev.Remove(gameDev);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameDevExists(int id)
        {
            return _context.GameDev.Any(e => e.GameDevId == id);
        }
    }
}
