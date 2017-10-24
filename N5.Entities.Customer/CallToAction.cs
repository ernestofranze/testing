using System;
using System.Collections.Generic;
using System.Text;

namespace N5.Entities.Customer
{
    public class CallToAction
    {
        private long _id;
        private Customer _customer;
        private CallToActionType _type;
        private double _weight;
        private DateTime _createdAt;
        private DateTime _updatedAt;
    }
}
