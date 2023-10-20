using EventServiceBL.Exceptions;
using EventServiceBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventServiceBL.Managers
{
    public class EventManager
    {
        private Dictionary<string, Event> _events = new Dictionary<string, Event>();

        public EventManager()
        {
            _events.Add("ASP.NET", new Event("ASP.NET", DateTime.Parse("14/10/2023"), "Mercator", 20));
            _events.Add("MongoDB", new Event("MongoDB", DateTime.Parse("4/9/2023"), "Mercator", 25));
            _events.Add("async", new Event("async", DateTime.Parse("14/12/2023"), "Schoonmeersen", 10));
        }
        public void AddEvent(Event ev)
        {
            if (ev == null) throw new EventModelException("addevent");
            if (_events.ContainsKey(ev.Name)) throw new EventModelException("addevent");
            _events.Add(ev.Name, ev);
        }

        public Event GetEvent(string name)
        {
            //if (!_events.ContainsKey(name)) throw new EventModelException("getevent");
            return _events[name];
        }

        public List<Event> GetEventsForDate(DateTime dateTime)
        {
            return _events.Values.Where(x => x.Date== dateTime).ToList();
        }

        public List<Event> GetEventsForLocation(string location)
        {
           return _events.Values.Where(x=>x.Location==location).ToList();
        }

        public void RemoveEvent(Event ev)
        {
            if (ev == null) throw new EventModelException("removevent");
            if (!_events.ContainsKey(ev.Name)) throw new EventModelException("removeevent");
            _events.Remove(ev.Name);
        }

        public void SubscribeVisitor(Visitor visitor, Event ev)
        {
            try
            {
                _events[ev.Name].AddVisitor(visitor);
            }
            catch(Exception ex) { throw new ManagerException("subscribe",ex); }
        }
    }
}
