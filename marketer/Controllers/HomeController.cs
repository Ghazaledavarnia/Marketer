using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Marketer.Models;
using Marketer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Marketer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var user=await _userManager.GetUserAsync(User);
            
            if (user!=null)
            {
                ViewBag.Class = _context.Classes.Select(x=> new SelectListItem() {
                    Text = x.ClassName,
                    Value = x.Id.ToString()
                }).ToList();
                return View();
            }
            return RedirectToAction(nameof(AccountController.Login), "Account");

        }
        /// <summary>
        /// ثبت اطلاعات فروشگاه/اشخاص
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ReisterPerson(PersonViewModel person)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                var check = _context.Person.Any(s => s.Mobile == person.Mobile);
                if (!check)
                {
                    var newperson = new Person()
                    {
                        Mobile = person.Mobile,
                        Name = person.Name,
                        Note = person.Note,
                        Class = person.Class,
                        ApplicationUserId = user.Id,
                };
                    _context.Add(newperson);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                // return BadRequest("اطلاعات این شخص وجود دارد!!!");
                ModelState.AddModelError("Err", " اطلاعات این شخص وجود دارد!");
                return BadRequest(ModelState);
            }
            else { return BadRequest(ModelState); }
            // return BadRequest("فرم را پر کنید");
        }
        /// <summary>
        /// گزارش مربوط به تیبل آمار
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ReportGrid()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.Class = _context.Classes.Select(x => new SelectListItem()
            {
                Text = x.ClassName,
                Value = x.Id.ToString()
            }).ToList();
            var person = _context.Person
                .Where(s => s.ApplicationUserId == user.Id)
                .Select(x => new PersonViewModel()
                {
                    Id = x.Id,
                    Mobile = x.Mobile,
                    Class = x.Class,
                    Name = x.Name,
                    Note = x.Note
                }).ToList();
            return PartialView("_ReportGrid", person);
        }
        public IActionResult Edit(int? id)
        {
            if (id!=null)
            {
                var person = _context.Person.SingleOrDefault(m => m.Id == id);
                if (person==null)
                {
                    return NotFound();
                }
                var selectedPerson=new PersonViewModel
                {
                    Id=person.Id,
                    Mobile=person.Mobile,
                    Name=person.Name,
                   Class=person.Class,
                   Note=person.Note,
                   //ApplicationUserId=person.ApplicationUserId
                };
                return View(selectedPerson);

            }
            return BadRequest();
        }
        /// <summary>
        /// به روز رسانی اطلاعات اشخاص
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Update(PersonViewModel model)
        {
            //if (id != entranceExit.Id)
            //{
            //    return NotFound();
            //}
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                try
                {
                    //var check = _context.Person.Where(s => s.ApplicationUserId == user.UserName).FirstOrDefault();
                    var update = new Person
                    {
                        Id=model.Id,
                        Mobile=model.Mobile,
                        Name=model.Name,
                        Note=model.Note,
                        Class=model.Class,
                        ApplicationUserId = user.Id,
                    };
                    _context.Update(update);
                    await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                   
                }
               // return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("Err", " فیلدها را پر کنید!");
            return BadRequest(ModelState);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
