using EventServiceBL.Exceptions;
using EventServiceBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EventServiceBL.Managers
{
    public class VisitorManager
    {
        private int _id=1;
        private Dictionary<int,Visitor> _visitors=new Dictionary<int,Visitor>();

        public VisitorManager()
        {
            _visitors.Add(_id, new Visitor("John",DateTime.Parse("12/8/1989"), _id++));
            _visitors.Add(_id, new Visitor("Jane", DateTime.Parse("2/8/1987"), _id++));
            _visitors.Add(_id, new Visitor("David", DateTime.Parse("18/7/1999"), _id++));
            _visitors.Add(_id, new Visitor("Chris", DateTime.Parse("6/2/2000"), _id++));
        }

        public bool ExistsVisitor(int id)
        {
            return _visitors.ContainsKey(id);
        }

        public List<Visitor> GetAllVisitors()
        {
            return _visitors.Values.ToList();
        }
        public Visitor GetVisitor(int id)
        {
            return _visitors[id];
        }
        public void RegisterVisitor(Visitor visitor)
        {
            visitor.SetId(_id++);            
            //return _id++;
        }

        public void SubscribeVisitor(Visitor visitor)
        {
            if (visitor==null) throw new ManagerException("subscribe");
            if (_visitors.ContainsKey(visitor.Id)) throw new ManagerException("subscribe");
           _visitors.Add(visitor.Id, visitor);
        }

        public void UnsubscribeVisitor(Visitor visitor)
        {
            if (visitor == null) throw new ManagerException("unsubscribe");
            if (!_visitors.ContainsKey(visitor.Id)) throw new ManagerException("unsubscribe");
            _visitors.Remove(visitor.Id);
        }

        public void UpdateVisitor(Visitor visitor)
        {
            if (visitor == null) throw new ManagerException("unsubscribe");
            if (!_visitors.ContainsKey(visitor.Id)) throw new ManagerException("unsubscribe");
            _visitors[visitor.Id] = visitor;
        }
    }
}
