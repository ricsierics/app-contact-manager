using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ContactManager.Models;

namespace ContactManager.Services
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetContactsAsync();
        Task<Contact> GetContactAsync(int id);
        Task AddContactAsync(Contact contact);
        Task UpdateContactAsync(Contact contact);
        Task DeleteContactAsync(Contact contact);
    }
}
