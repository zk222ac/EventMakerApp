using System;
using Windows.UI.Popups;
using EventMakerApp.Converter;
using EventMakerApp.Model;
using EventMakerApp.ViewModel;

namespace EventMakerApp.Handler
{
    class EventHandler
    {
        // dependency Injection technique
        public EventVm EventVm { get; set; }
        public EventHandler(EventVm eventVm)
        {
            EventVm = eventVm;
        }

        public void SelectedEvent(Event ev)
        {
            EventVm.SelectedEvent = ev;
        }

        public async void CreateEvent()
        {
            Event newEvent = new Event()
            {
                Id = EventVm.AddEvent.Id,
                Name = EventVm.AddEvent.Name,
                Place =  EventVm.AddEvent.Place,
                Description = EventVm.AddEvent.Description,
                DateTime =  DateTimeConverter.DateTimeOffsetAndTimeSetToDateTime(EventVm.Date , EventVm.Time)
            };
           // EventVm --> EventCatalogSingleton --> Add
           EventVm.EventCatalogSingleton.Add(newEvent);

            // Display successful message
            var messageDialog = new MessageDialog("Your have been successfully Added your new Event in calender: " + EventVm );
            await messageDialog.ShowAsync();

        }
        public async void DeleteEvent()
        {

            // Create the message dialog and set its content
             var messageDialog = new MessageDialog("Are you sure you want to Delete the Event: " + EventVm.SelectedEvent.Name + " ?");

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
              messageDialog.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(this.CommandInvokedHandler)));
              messageDialog.Commands.Add(new UICommand("No", null));

             // Set the command that will be invoked by default
              messageDialog.DefaultCommandIndex = 0;

            // Set the command to be invoked when escape is pressed
             messageDialog.CancelCommandIndex = 1;

            // Show the message dialog
            await messageDialog.ShowAsync();

        }

        private void CommandInvokedHandler(IUICommand command)
        {
            EventVm.EventCatalogSingleton.Remove(EventVm.SelectedEvent);
        }
    }
}
