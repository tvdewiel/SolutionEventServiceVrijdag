using EventServiceBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventServiceBL.Model
{
    public class Event
    {
        private Dictionary<int,Visitor> _visitors = new Dictionary<int,Visitor>();

        public Event(string name, DateTime date, string location, int maxVisitors)
        {
            Name = name;
            Date = date;
            Location = location;
            MaxVisitors = maxVisitors;
        }

        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int MaxVisitors { get; set; }
        public IReadOnlyList<Visitor> Visitors => _visitors.Values.ToList().AsReadOnly();

        public void AddVisitor(Visitor visitor)
        {
            if (visitor == null) throw new EventModelException("AddVisitor");
            if (_visitors.ContainsKey(visitor.Id)) throw new EventModelException("AddVisitor");
            if (_visitors.Values.Count==MaxVisitors) throw new EventModelException("AddVisitor");
            _visitors.Add(visitor.Id, visitor);
        }
        public void RemoveVisitor(Visitor visitor)
        {
            if (visitor == null) throw new EventModelException("RemoveVisitor");
            if (!_visitors.ContainsKey(visitor.Id)) throw new EventModelException("RemoveVisitor");
            _visitors.Remove(visitor.Id);
        }
        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new EventModelException("SetName");
            Name = name;
        }
        public void SetLocation(string location)
        {
            if (string.IsNullOrEmpty(location)) throw new EventModelException("SetLocation");
            Location=location;
        }
        public void SetMaxVisitors(int max) { if (max <= 0) throw new EventModelException("setMax"); MaxVisitors = max; }
    }
}
