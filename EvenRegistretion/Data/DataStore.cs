using EvenRegistretion.Models;

namespace EvenRegistretion.Data
{
    public class DataStore
    {
        public static List<Event> Events { get; set; } = new List<Event>();

        public static List<Registration> Registrations = new List<Registration>();

        private static int _eventCounter = 1;
        private static int _registrationCounter = 1;

        public static int GetNextEventId()
        {
            return _eventCounter++;
        }

        public static int GetNextRegistrationId()
        {
            return _registrationCounter++;
        }
    }
}
