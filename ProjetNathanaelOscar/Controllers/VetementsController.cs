using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetNathanaelOscar.Data;
using Microsoft.AspNetCore.Identity;
using ProjetNathanaelOscar.Models;
using ProjetNathanaelOscar.Authorizations;
using Microsoft.AspNetCore.Authorization;

namespace ProjetNathanaelOscar.Controllers
{
    public class VetementsController : Controller
    {
        private  ApplicationDbContext _context { get; }
        private  IAuthorizationService _authorizationService { get; }
        private  UserManager<IdentityUser> _userManager { get; }

        public VetementsController(ApplicationDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        // GET: Vetement
        public async Task<IActionResult> Index(string TypeVetement, string searchString)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Vetement
                                            orderby m.TypeVetement
                                            select m.TypeVetement;
            var vetements = from m in _context.Vetement
                         select m;
            //Permet a l utilisateur de voir que c est vetements (Securite) ne fonctionne pas
            /*var isAuthorized = User.IsInRole(AuthorizationConstants.VetementAdministratorsRole);
            var currentUserId = _userManager.GetUserId(User);

            if(!isAuthorized)
            {
                vetements = vetements.Where(m => m.ProprietaireId == currentUserId);
            }
            return View(await vetements.ToListAsync());*/

            if (!string.IsNullOrEmpty(searchString))
            {
                vetements = vetements.Where(s => s.Nom!.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(TypeVetement))
            {
                vetements = vetements.Where(x => x.TypeVetement == TypeVetement);
            }

            var vetementGenreVM = new VetementTypeViewModel
            {
                TypeVetement = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Vetements = await vetements.ToListAsync()
            };

            return View(vetementGenreVM);
        }

        // GET: Vetements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vetement == null)
            {
                return NotFound();
            }

            var vetement = await _context.Vetement
                .FirstOrDefaultAsync(m => m.VetementId == id);
            if (vetement == null)
            {
                return NotFound();
            }

            return View(vetement);
        }

        // GET: Vetements/Create
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VetementId,Nom,Description,DateObtention,Cote,TypeVetement,Image")] Vetement vetement)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                vetement.ProprietaireId = currentUser.Id; // Assigner l'ID du propriétaire

                _context.Add(vetement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vetement);
        }

        // GET: Vetements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vetement == null)
            {
                return NotFound();
            }

            var vetement = await _context.Vetement.FindAsync(id);
            if (vetement == null)
            {
                return NotFound();
            }
            return View(vetement);
        }

        // POST: Vetements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VetementId,Nom,Description,DateObtention,Cote,TypeVetement,Image")] Vetement vetement)
        {
            if (id != vetement.VetementId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vetement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VetementExists(vetement.VetementId))
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
            return View(vetement);
        }

        // GET: Vetements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vetement == null)
            {
                return NotFound();
            }

            var vetement = await _context.Vetement
                .FirstOrDefaultAsync(m => m.VetementId == id);
            if (vetement == null)
            {
                return NotFound();
            }

            return View(vetement);
        }

        // POST: Vetements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vetement == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Vetement'  is null.");
            }
            var vetement = await _context.Vetement.FindAsync(id);
            if (vetement != null)
            {
                _context.Vetement.Remove(vetement);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VetementExists(int id)
        {
          return (_context.Vetement?.Any(e => e.VetementId == id)).GetValueOrDefault();
        }


        public async Task<IActionResult> RandomClothes()
        {
            IQueryable<string> genreQuery = from m in _context.Vetement
                                            orderby m.TypeVetement
                                            select m.TypeVetement;
            var vetements = from m in _context.Vetement
                            select m;

            var random = new Random();
            int randomNumber = random.Next(1, 3);

            if (randomNumber == 1)
            {
                var hauts = vetements.Where(v => v.TypeVetement == "Haut").ToList();
                var bas = vetements.Where(v => v.TypeVetement == "Bas").ToList();
                var chaussures = vetements.Where(v => v.TypeVetement == "Chaussure").ToList();

                var randomHaut = hauts[random.Next(hauts.Count)];
                var randomBas = bas[random.Next(bas.Count)];
                var randomChaussure = chaussures[random.Next(chaussures.Count)];

                var vetementGenreVM = new VetementTypeViewModel
                {
                    TypeVetement = new SelectList(await genreQuery.Distinct().ToListAsync()),
                    Vetements = new List<Vetement> { randomHaut, randomBas, randomChaussure }.Where(v => v != null).ToList()
                };

                return View(vetementGenreVM);
            }
            else if (randomNumber == 2)
            {
                var combinaisons = vetements.Where(v => v.TypeVetement == "combinaison").ToList();
                var chaussures = vetements.Where(v => v.TypeVetement == "Chaussure").ToList();

                var randomCombinaison = combinaisons[random.Next(combinaisons.Count)];
                var randomChaussure = chaussures[random.Next(chaussures.Count)];

                var vetementGenreVM = new VetementTypeViewModel
                {
                    TypeVetement = new SelectList(await genreQuery.Distinct().ToListAsync()),
                    Vetements = new List<Vetement> { randomCombinaison, randomChaussure }.Where(v => v != null).ToList()
                };

                return View(vetementGenreVM);
            }

            return View();
        }
    }
}
