using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Guestbook.Models;

namespace Guestbook.Controllers
{
    public class GuestbookController : Controller
    {
        private GuestbookContext _db = new GuestbookContext();
        public ActionResult Index()
        {
            var mostRecentEntries =
                (from entry in _db.Entries
                 orderby entry.DateAdded descending
                 select entry).Take(20);
            var model = mostRecentEntries.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(GuestbookEntry entry)
        {
            if (ModelState.IsValid)
            {
                entry.DateAdded = DateTime.Now;
                _db.Entries.Add(entry);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(entry);
        }
        public ViewResult Show(int id)
        {
            var entry = _db.Entries.Find(id);
            bool hasPermission = User.Identity.Name == entry.Name;
            ViewData["hasPermission"] = hasPermission;
            return View(entry);
        }
        public ActionResult CommentSummary()
        {
            var entries = from entry in _db.Entries
                group entry by entry.Name
                into groupedByName
                orderby groupedByName.Count() descending
                select new CommentSummary
                {
                    NumberOfComments = 
                        groupedByName.Count(),
                    UserName = groupedByName.Key
                };
            return View(entries.ToList());
        }
    }
}
