using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models.ViewModels;
using System.Security.Claims;
using MvcMovie.Models.Constants;
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
        public async Task<IActionResult> AssignClaim(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return BadRequest();
            }

var allPermissions = SystemPermissions.AllPermissions;            var roleClaims = await _roleManager.GetClaimsAsync(role);
            if (roleClaims == null)
            {
                roleClaims = new List<Claim>();
            }

            var model = new RoleClaimVM
            {
                RoleId = role.Id,
                RoleName = role.Name,
                Claims = allPermissions.Select(p => new RoleClaim
                {
                    Type = "Permission",
                    Value = p,
                    Selected = roleClaims.Any(c => c.Type == "Permission" && c.Value == p)
                }).ToList()
            };

            return View(model);
        }
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> AssignClaim(RoleClaimVM model)
{
    if (!ModelState.IsValid)
    {
        return View(model);
    }

    var role = await _roleManager.FindByIdAsync(model.RoleId);
    if (role == null)
    {
        return BadRequest();
    }

    var claims = await _roleManager.GetClaimsAsync(role);
    if (claims == null)
    {
        claims = new List<Claim>();
    }

    foreach (var claim in claims.Where(c => c.Type == "Permission"))
    {
        await _roleManager.RemoveClaimAsync(role, claim);
    }

    foreach (var claim in model.Claims.Where(c => c.Selected))
    {
        await _roleManager.AddClaimAsync(role, new Claim(claim.Type, claim.Value));
    }

    return RedirectToAction(nameof(Index));
}
    
    }
}
