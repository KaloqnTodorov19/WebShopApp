using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using WebShopAppp.Infrastructure.Data.Domain;
using WebShopAppp.Models;
using WebShopAppp.Models.Client;

namespace WebShopAppp.Controllers
{
    public class ClientController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        
        public ClientController(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }
        // GET: ClientController1

        public async Task<IActionResult> Index()
        {
            var allUsers = this._userManager.Users.Select(u => new ClientIndexVM
            {
                Id = u.Id,
                UserName = u.UserName,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Address = u.Adress,
                Email = u.Email,

            }).ToList();

            var adminIds = (await _userManager.GetUsersInRoleAsync("Administrator")).Select(a => a.Id).ToList();

            foreach( var user in allUsers)
            {
                user.IsAdmin = adminIds.Contains(user.Id);
            }

            var users = allUsers.Where(x => x.IsAdmin == false).OrderBy(x => x.UserName).ToList();

            return this.View(users);
        }
        // GET: ClientController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ClientController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ClientController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientController1/Delete/5
        public ActionResult Delete(string id)
        {
            var user = this._userManager.Users.FirstOrDefault(x => x.Id == id);
            
            if(user == null)
            {
                return NotFound();
            }

            ClientDeleteVM userToDelete = new ClientDeleteVM()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Adress,
                Email = user.Email,

            };

            return View(userToDelete);
        }

        // POST: ClientController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ClientDeleteVM bidingModel)
        {
            string id = bidingModel.Id;
            var user = await _userManager.FindByIdAsync(id);
            if(user == null) 
            {
                return NotFound();
            }

            IdentityResult result = await _userManager.DeleteAsync(user);

            if(result.Succeeded)
            {
                return RedirectToAction("Success");
            }

            return NotFound();
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}
