using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManager.Services
{
    public interface ICallService
    {
        void CallContact(string contactNumber);
    }
}
