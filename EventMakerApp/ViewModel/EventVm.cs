using System;
using System.Windows.Input;
using EventMakerApp.Common;
using EventMakerApp.Model;
using EventHandler = EventMakerApp.Handler.EventHandler;

namespace EventMakerApp.ViewModel
{
    class EventVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }
        public Event SelectedEvent { get; set; }
        public Event AddEvent { get; set; }
        public EventCatalogSingleton EventCatalogSingleton { get; set; }
        public EventHandler EventHandler { get; set; }
        public ICommand CreateEventCommand { get; set; }
        public ICommand DeleteEventCommand { get; set; }
        public ICommand SelectedEventCommand { get; set; }
        // Display day and time in Coordinated Universal time (UTC)
        public DateTimeOffset Date { get; set; }
        // Time interval code
        public TimeSpan Time { get; set; }
        public EventVm()
        {
            // selected item element
            SelectedEvent = new Event();
            // Add new event
            AddEvent = new Event();
            // Event catalog singleton Instance check
            EventCatalogSingleton = EventCatalogSingleton.Instance;
            // Pass EventVm model to event Handler class
            EventHandler = new EventHandler(this);
            // Get current datetime now
            DateTime dt = DateTime.Now;
            // represent current date in Years , Months , Day, hours , min , sec
             Date = new DateTimeOffset(dt.Year,dt.Month,dt.Day,dt.Hour,dt.Minute,dt.Second, new TimeSpan());
             Time = new TimeSpan(dt.Hour, dt.Minute,dt.Second);
            // Invoke CreateEvent delegate method inside EvenHandler Class
            CreateEventCommand = new RelayArgCommand<Event>( s => EventHandler.CreateEvent());
            // Invoke DeleteEvent delegate method inside EvenHandler Class
            DeleteEventCommand = new RelayArgCommand<Event>(s => EventHandler.DeleteEvent());
            // Invoke SelectedEvent delegate method inside EventHandler
            SelectedEventCommand = new RelayArgCommand<Event>( ev=>EventHandler.SelectedEvent(ev));
        }
    }
}
