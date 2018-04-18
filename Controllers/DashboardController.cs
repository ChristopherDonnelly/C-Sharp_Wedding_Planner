using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wedding_Planner.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Wedding_Planner.Controllers
{
    public class DashboardController : Controller
    {
        private WeddingPlannerContext _context;

        public DashboardController(WeddingPlannerContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Dashboard")]
        public IActionResult Dashboard()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            string UserName = HttpContext.Session.GetString("UserName");
            
            ViewData["Username"] = UserName;

            if(UserId!=null){

                ViewData["UserId"] = (int)UserId;

                User user = _context.user.Include( u => u.Plans ).ThenInclude( p => p.WeddingInfo ).SingleOrDefault(u => u.UserId == UserId);
                List<WeddingInfo> weddingInfo = _context.weddinginfo.Include( u => u.Guests ).ToList();

                return View(weddingInfo);
            }else{
                return RedirectToAction("Login", "User");
            }
        }

        [HttpGet]
        [Route("NewWedding")]
        public IActionResult NewWedding()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            string UserName = HttpContext.Session.GetString("UserName");
            ViewData["Username"] = UserName;
            ViewData["UserId"] = (int)UserId;

            return View(new WeddingInfo{ CreatedById = (int)UserId });
        }

        [HttpPost]
        [Route("CreatePlan")]
        public IActionResult CreatePlan(WeddingInfo weddingInfo)
        {
            
            if(ModelState.IsValid)
            {
                
                int? UserId = HttpContext.Session.GetInt32("UserId");

                User user = _context.user.Include( u => u.Plans ).ThenInclude( p => p.WeddingInfo ).SingleOrDefault(u => u.UserId == UserId);
                
                weddingInfo.CreatedBy = user;
                _context.weddinginfo.Add(weddingInfo);
                _context.weddingplan.Add(new WeddingPlan{ WeddingId = weddingInfo.WeddingId, WeddingInfo = weddingInfo, GuestId = user.UserId, Guest = user });
                _context.SaveChanges();

                return RedirectToAction("WeddingDetails", new { id = weddingInfo.WeddingId });
                // return RedirectToAction("WeddingDetails");
            }

            return View("NewWedding", weddingInfo);
        }

        [HttpGet]
        [Route("WeddingDetails/{id}")]
        public IActionResult WeddingDetails(int id)
        {
            string UserName = HttpContext.Session.GetString("UserName");
            ViewData["Username"] = UserName;
            
            int? UserId = HttpContext.Session.GetInt32("UserId");
            ViewData["UserId"] = (int)UserId;

            WeddingInfo weddingInfo = _context.weddinginfo.Include( u => u.Guests ).ThenInclude( g => g.Guest ).SingleOrDefault(u => u.WeddingId == id );
            
            if(weddingInfo != null){
                return View(weddingInfo);
            }else{
                return RedirectToAction("Dashboard");
            }
        }

        [HttpGet]
        [Route("RSVP/{id}")]
        public IActionResult RSVP(int id)
        {
            
            int? UserId = HttpContext.Session.GetInt32("UserId");
            ViewData["UserId"] = (int)UserId;

            WeddingPlan plan = _context.weddingplan.Where(p => p.WeddingId == id).SingleOrDefault(u => u.GuestId == UserId);

            if(plan != null){
                _context.weddingplan.Remove(plan);
            }else{
                _context.weddingplan.Add(new WeddingPlan{ WeddingId = id, GuestId = (int)UserId });
            }

            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            
            int? UserId = HttpContext.Session.GetInt32("UserId");
            ViewData["UserId"] = (int)UserId;

            WeddingInfo info = _context.weddinginfo.SingleOrDefault(u => u.WeddingId == id);

            if(info != null){
                _context.weddinginfo.Remove(info);
            }

            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }

    }
}
