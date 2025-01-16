using Donet_Mini_Projet.Models.Repositories;
using Donet_Mini_Projet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Donet_Mini_Projet.Controllers
{
    [Authorize(Roles = "User")]
    public class ClientController : Controller
    {
        private readonly IRepository<Article> _articleRepository;
        private readonly IRepository<Reclamation> _reclamationRepository;

        public ClientController(IRepository<Article> articleRepository, IRepository<Reclamation> reclamationRepository)
        {
            _articleRepository = articleRepository;
            _reclamationRepository = reclamationRepository;
        }

        // GET: Client/Articles
        public async Task<ActionResult> Articles()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Retrieve UserId
            if (userId == null)
            {
                return Unauthorized(); // Ensure the user is authenticated
            }

            var articles = await _articleRepository.GetAllAsync();
            var clientArticles = articles.Where(a => a.ClientId == int.Parse(userId)).ToList(); // Filter by UserId
            return View(clientArticles);
        }

        // GET: Client/Reclamations
        public async Task<ActionResult> Reclamations(int clientId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var reclamations = await _reclamationRepository.GetAllAsync();
            var clientReclamations = reclamations.Where(r => r.ClientId == int.Parse(userId)).ToList();
            return View(clientReclamations);
        }

        // GET: Client/CreateArticle
        public ActionResult CreateArticle()
        {
            return View();
        }

        // POST: Client/CreateArticle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateArticle(Article article)
        {
            if (ModelState.IsValid)
            {
                await _articleRepository.AddAsync(article);
                await _articleRepository.SaveAsync();
                return RedirectToAction("Articles", new { clientId = article.ClientId });
            }
            return View(article);
        }

        // GET: Client/EditArticle/{id}
        public async Task<ActionResult> EditArticle(int id)
        {
            var article = await _articleRepository.GetByIdAsync(id);
            if (article == null)
                return NotFound();
            return View(article);
        }

        // POST: Client/EditArticle/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditArticle(int id, Article article)
        {
            if (id != article.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _articleRepository.UpdateAsync(article);
                await _articleRepository.SaveAsync();
                return RedirectToAction("Articles", new { clientId = article.ClientId });
            }
            return View(article);
        }

        // GET: Client/DeleteArticle/{id}
        public async Task<ActionResult> DeleteArticle(int id)
        {
            var article = await _articleRepository.GetByIdAsync(id);
            if (article == null)
                return NotFound();
            return View(article);
        }

        // POST: Client/DeleteArticle/{id}
        [HttpPost, ActionName("DeleteArticle")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _articleRepository.DeleteAsync(id);
            await _articleRepository.SaveAsync();
            return RedirectToAction("Articles");
        }

        // GET: Client/CreateReclamation
        public ActionResult CreateReclamation()
        {
            return View();
        }

        // POST: Client/CreateReclamation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateReclamation(Reclamation reclamation)
        {
            if (ModelState.IsValid)
            {
                await _reclamationRepository.AddAsync(reclamation);
                await _reclamationRepository.SaveAsync();
                return RedirectToAction("Reclamations", new { clientId = reclamation.ClientId });
            }
            return View(reclamation);
        }

        // GET: Client/EditReclamation/{id}
        public async Task<ActionResult> EditReclamation(int id)
        {
            var reclamation = await _reclamationRepository.GetByIdAsync(id);
            if (reclamation == null)
                return NotFound();
            return View(reclamation);
        }

        // POST: Client/EditReclamation/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditReclamation(int id, Reclamation reclamation)
        {
            if (id != reclamation.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _reclamationRepository.UpdateAsync(reclamation);
                await _reclamationRepository.SaveAsync();
                return RedirectToAction("Reclamations", new { clientId = reclamation.ClientId });
            }
            return View(reclamation);
        }

        // GET: Client/DeleteReclamation/{id}
        public async Task<ActionResult> DeleteReclamation(int id)
        {
            var reclamation = await _reclamationRepository.GetByIdAsync(id);
            if (reclamation == null)
                return NotFound();
            return View(reclamation);
        }

        // POST: Client/DeleteReclamation/{id}
        [HttpPost, ActionName("DeleteReclamation")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteReclamationConfirmed(int id)
        {
            await _reclamationRepository.DeleteAsync(id);
            await _reclamationRepository.SaveAsync();
            return RedirectToAction("Reclamations");
        }
    }
}
