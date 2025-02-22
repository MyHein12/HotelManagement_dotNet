using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using HotelManagement.Models;
using System.Security.Claims;

namespace HotelManagement.Controllers
{
    public class UsersController : Controller
    {
        private readonly HotelDbContext _context;
        public UsersController(HotelDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            var user = _context.Users.FirstOrDefault(x => x.Email == model.Email);
            if (IsValid(model.Email,model.Password))
            {
                // Đăng nhập thành công, lưu thông tin người dùng vào session và chuyển hướng đến trang chủ
                HttpContext.Session.SetInt32("UserId", user.ID);
                HttpContext.Session.SetString("Username", user.UserName);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Thông tin đăng nhập không chính xác, hiển thị thông báo lỗi
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
        }

        // GET: Logout
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear(); // Xóa tất cả các session
            return RedirectToAction("Index", "Home"); // Chuyển hướng đến trang chủ
        }

        // Kiểm tra thông tin đăng nhập
        private bool IsValid(string email, string password)
        {
            // Lấy thông tin người dùng từ database
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return true;
            }

            return false;
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.Users.FirstOrDefault(u => u.UserName == model.UserName);

                if (existingUser == null)
                {
                    var newUser = new User
                    {
                        UserName = model.UserName,
                        Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                        Email = model.Email
                    };

                    _context.Users.Add(newUser);
                    _context.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Username", "Username already exists.");
                }
            }

            return View(model);
        }
    }
}
