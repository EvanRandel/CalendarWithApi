using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;

namespace CalendarWithApi
{
    public class Calendar
    {
        private readonly CalendarService _calendarSercive;

        public CalendarEvent(string serviceAccountJsonPath)
        {
            var credential = GoogleCredential.FromFile(serviceAccountJsonPath)
                .CreateScoped(CalendarService.Scope.CalendarReadonly);

            _calendarSercive = new CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "Reciplate"
            });
        }

        public async Task<IList<Event>> GetEventsAsync()
        {
            var listRequest = _calendarService.Events.List("primary");
            listRequest.TimeMin = DateTime.Now;
            listRequest.ShowDeleted = false;
            listRequest.SingleEvents = true;
            listRequest.MaxResults = 10;
            listRequest.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            var events = await listRequest.ExecuteAsync();
            return events.Item;
        }
    }
}
