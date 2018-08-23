using ContactManager.Services;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ContactManager.Tests
{
    public class MockDependencyService : IDependencyService
    {
        private readonly IDictionary registeredServices = new Dictionary<Type, object>();

        public void Register<T>(T implementation)
        {
            this.registeredServices.Add(typeof(T), implementation);
        }

        public T Get<T>() where T : class
        {
            return (T)this.registeredServices[typeof(T)];
        }
    }
}
