using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MvcMovie.Controllers
{
    public class RoleController : Controller
    {

        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        // GET: Role/Edit/id
public async Task<IActionResult> Edit(string id)
{
    var role = await _roleManager.FindByIdAsync(id);
    if (role == null)
    {
        return NotFound();
    }
    return View(role);
}

// POST: Role/Edit
[HttpPost]
public async Task<IActionResult> Edit(string id, string newName)
{
    var role = await _roleManager.FindByIdAsync(id);
    if (role == null)
    {
        return NotFound();
    }

    role.Name = newName;
    await _roleManager.UpdateAsync(role);
    return RedirectToAction("Index");
}
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
public async Task<IActionResult> Create(string roleName)
{
    if (!string.IsNullOrEmpty(roleName))
    {
        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Lỗi khi tạo Role.");
            }
        }
        else
        {
            ModelState.AddModelError("", "Role đã tồn tại.");
        }
    }
    return View();
}

        

[HttpGet]
public IActionResult Create()
{
    return View();
}

    }
}
