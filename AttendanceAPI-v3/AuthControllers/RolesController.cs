 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AttendanceAPI_v3.AuthenticationModels;
using AttendanceAPI_v3.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace AttendanceAPI_v3.AuthControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly AuthenticationContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(AuthenticationContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        // add instructor role
        //[Authorize(Roles = "Admin")]
        [HttpPost("add-instuctor")]
        public async Task<IActionResult> AddRoleToUser(string userEmail)
        {
            string _role = "Instructor";
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return NotFound($"User with email {userEmail} not found");
            }

            var roleExists = await _roleManager.RoleExistsAsync(_role);
            if (!roleExists)
            {
                return NotFound($"Role {_role} not found");
            }

            var result = await _userManager.AddToRoleAsync(user, _role);
            if (result.Succeeded)
            {
                return Ok($"Role {_role} added to user {user.UserName} successfully");
            }

            return BadRequest(result.Errors);
        }




       
    }
}
