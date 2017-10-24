using System;
using System.Collections.Generic;
using System.Text;

namespace N5.Entities.Customer
{
    public class CallToActionType
    {
        private long _id;
        private string _name;
        private string _description;
        private int _position;
        private IEnumerable<Action> _actions;
        private DateTime _createdAt;
        private DateTime _updatedAt;

        public CallToActionType(string name, string description = "")
        {
            _name = name;
            _description = description;
        }
    }
}
