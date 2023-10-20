using EventServiceBL.Exceptions;

namespace EventServiceBL.Model
{
    public class Visitor
    {
        public Visitor(string name, DateTime birthday, int id)
        {
            Name = name;
            Birthday = birthday;
            Id = id;
        }

        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public int Id { get; set; }
        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new EventModelException("SetName");
            Name = name;
        }
        public void SetId(int id) { if (id <= 0) throw new EventModelException("setid"); Id = id; }
    }
}