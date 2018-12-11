using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventMakerApp.Persistency;

namespace EventMakerApp.Model
{
    class EventCatalogSingleton
    {
        private static  EventCatalogSingleton _instance = null;

        public static EventCatalogSingleton Instance => _instance ?? (_instance = new EventCatalogSingleton());

        public  ObservableCollection<Event> Events { get; set; }

        private readonly FilePersistency<Event> _filePersistency;

        public EventCatalogSingleton()
        {
            Events = new ObservableCollection<Event>
            {
                new Event(1,"Pitching 2nd semester projects", "Final semester project","Auditorium 101", new DateTime(2018,12,9,22,16,22)),
                new Event(2, "Guru Programming", "Conference projects", "Local 202", new DateTime(2018, 12, 9, 22, 16, 22))
            };
            _filePersistency = new FilePersistency<Event>();

            // Load the Event 
        }

        // Add Event
        public async void Add(Event newEvent)
        {
            // Add event into EventList
            Events.Add(newEvent);
            // Save the List into File
            await _filePersistency.SaveAsync(Events);
        }
        // Remove Event
        public async void Remove(Event newRemove)
        {
            Events.Remove(newRemove);
            await _filePersistency.SaveAsync(Events);
        }

        // Load Event
        public async void LoadEventAsync()
        {
            var events = await _filePersistency.LoadAsync();
            if (events != null)
            {
                foreach (var ev in events)
                {
                    Events.Add(ev);
                }
            }
            else
            {
                // fill the Testing data 
                Events.Add(new Event(1,"Pitching 2nd semester projects", "Final semester project","Auditorium 101", new DateTime(2018,12,9,22,16,22)));
                Events.Add(new Event(2, "Guru Programming", "Conference projects", "Local 202", new DateTime(2018, 12, 9, 22, 16, 22)));
            }
        }
    }
}
