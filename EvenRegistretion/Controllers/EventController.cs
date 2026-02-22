using EvenRegistretion.Data;
using EvenRegistretion.Models;
using Microsoft.AspNetCore.Mvc;

namespace EvenRegistretion.Controllers
{
    public class EventController : Controller
    {
        // Перегляд івенту
        public IActionResult Index(int id)
        {
            var eventItem = DataStore.Events.FirstOrDefault(e => e.Id == id);
            if (eventItem == null)
            {
                return NotFound("Подію не знайдено");
            }

            return View(eventItem); // @model Event у Razor
        }

        // GET: форма створення
        public IActionResult CreateEvent()
        {
            return View(new EventModel()); // @model EventModel у Razor
        }

        // POST: створення івенту
        [HttpPost]
        public IActionResult CreateEvent(EventModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Event
                {
                    Id = DataStore.GetNextEventId(),
                    Title = model.Title,
                    Description = model.Description,
                    Location = model.Location,
                    Date = model.Date
                };

                DataStore.Events.Add(entity);

                return RedirectToAction("Success");
            }

            return View(model);
        }

        // GET: форма реєстрації
        [HttpGet]
        public IActionResult Register(int eventId)
        {
            var registration = new Registration
            {
                EventId = eventId
            };
            return View(registration);
        }

        // POST: збереження реєстрації
        [HttpPost]
        public IActionResult Register(Registration registration)
        {
            if (!ModelState.IsValid)
            {
                return View(registration);
            }

            registration.Id = DataStore.GetNextRegistrationId();
            registration.RegisteredAt = DateTime.Now;

            DataStore.Registrations.Add(registration);

            var regs = DataStore.Registrations.Where(r => r.EventId == registration.EventId).ToList();
            return View("Success", regs);
        }

        // GET: список реєстрацій
        [HttpGet]
        public IActionResult Registrations(int eventId)
        {
            var registrations = DataStore.Registrations.Where(r => r.EventId == eventId).ToList();
            return View(registrations); // @model List<Registration> у Razor
        }

        // Пошук по email
        [HttpGet]
        public IActionResult SearchByEmail(string email)
        {
            var registrations = DataStore.Registrations.Where(r => r.Email.Equals(email, StringComparison.OrdinalIgnoreCase)).ToList();
            return View(registrations); // @model List<Registration> у Razor
        }

        // POST: пошук по email
        [HttpPost]
        public IActionResult SearchByEmail(string email, string action)
        {
            if (action == "Search")
            {
                var registrations = DataStore.Registrations.Where(r => r.Email.Equals(email, StringComparison.OrdinalIgnoreCase)).ToList();
                return View(registrations); // @model List<Registration> у Razor
            }            
            return View();
        }

        // POST: сторінка успіху з реєстраціями
        [HttpGet]
        public IActionResult Success(int eventId)
        {
            var registrations = DataStore.Registrations
                .Where(r => r.EventId == eventId)
                .ToList();

            return View(registrations);
        }
        // Сторінка успіху
        //public IActionResult Success()
        //{
        //    return View();
        //}
    }
}

