using System;
using System.Collections.Generic;
using System.Text;

namespace N5.Entities.Customer
{
    public class Action
    {
        private CallToActionType _callToActionType;
        private string _command;

        public Action(CallToActionType callToActionType, string command)
        {
            _callToActionType = callToActionType;
            _command = command;
        }
    }
}
