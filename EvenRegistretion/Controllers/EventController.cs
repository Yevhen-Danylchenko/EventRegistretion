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

            return View("Success");
        }


        // Сторінка успіху
        public IActionResult Success()
        {
            return View();
        }
    }
}

