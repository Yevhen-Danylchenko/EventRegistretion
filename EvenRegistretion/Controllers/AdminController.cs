using EvenRegistretion.Data;
using EvenRegistretion.Models;
using Microsoft.AspNetCore.Mvc;

namespace EvenRegistretion.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            var isAdmin = HttpContext.Session.GetString("IsAdmin");
            if (isAdmin != "true")
            {
                return RedirectToAction("Login");
            }
            return View(DataStore.Events);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string password)
        {
            if (password == "qwerty123")
            {
                HttpContext.Session.SetString("IsAdmin", "true");
                return RedirectToAction("Index");
            }
            ViewBag.Error = "Неправильний пароль";
            return View();
        }
        public IActionResult Logout()
        {
            //HttpContext.Session.Remove("IsAdmin");
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult CreateEvent()
        {
            var isAdmin = HttpContext.Session.GetString("IsAdmin");
            if (isAdmin != "true")
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        public IActionResult CreateEvent(Event newEvent)
        {
            var isAdmin = HttpContext.Session.GetString("IsAdmin");
            if (isAdmin != "true")
            {
                return RedirectToAction("Login");
            }

            if (ModelState.IsValid)
            {
                newEvent.Id = DataStore.GetNextEventId();
                DataStore.Events.Add(newEvent);
                return RedirectToAction("Index");
            }
            return View(newEvent);
        }

        public IActionResult ViewRegistrations(int eventId)
        {
            var isAdmin = HttpContext.Session.GetString("IsAdmin");
            if (isAdmin != "true")
            {
                return RedirectToAction("Login");
            }

            var evetItem = DataStore.Events.FirstOrDefault(e => e.Id == eventId);
            if (evetItem == null) { return NotFound(); }

            var registrations = DataStore.Registrations.Where(r => r.EventId == eventId).ToList();

            ViewBag.EventTitle = evetItem.Title;
            ViewBag.EventId = eventId;
            return View(registrations);
        }

        [HttpGet]
        public IActionResult EditEvent(int eventId)
        {
            var isAdmin = HttpContext.Session.GetString("IsAdmin");
            if (isAdmin != "true")
            {
                return RedirectToAction("Login");
            }
            var evetItem = DataStore.Events.FirstOrDefault(e => e.Id == eventId);
            if (evetItem == null) { return NotFound(); }
            return View(evetItem);
        }

        [HttpPost]
        public IActionResult EditEvent(Event updatedEvent)
        {
            var isAdmin = HttpContext.Session.GetString("IsAdmin");
            if (isAdmin != "true")
            {
                return RedirectToAction("Login");
            }
            var evetItem = DataStore.Events.FirstOrDefault(e => e.Id == updatedEvent.Id);
            if (evetItem == null) { return NotFound(); }
            if (ModelState.IsValid)
            {
                evetItem.Title = updatedEvent.Title;
                evetItem.Description = updatedEvent.Description;
                evetItem.Date = updatedEvent.Date;
                evetItem.Location = updatedEvent.Location;
                return RedirectToAction("Index");
            }
            return View(updatedEvent);
        }

        public IActionResult DelEvent(int eventId)
        {
            var isAdmin = HttpContext.Session.GetString("IsAdmin");
            if (isAdmin != "true")
            {
                return RedirectToAction("Login");
            }
            var evetItem = DataStore.Events.FirstOrDefault(e => e.Id == eventId);
            if (evetItem == null) { return NotFound(); }
            DataStore.Events.Remove(evetItem);
            DataStore.Registrations.RemoveAll(r => r.EventId == eventId);
            return RedirectToAction("Index");
        }
    }
}
