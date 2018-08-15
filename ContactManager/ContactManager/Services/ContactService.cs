using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ContactManager.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ContactManager.Services
{
    public class ContactService : IContactService
    {
        const string URL = "http://10.155.64.110:8080/retrieve/contacts";
        //const string URL = "http://loanhub.apphb.com/api/reminders";

        public Task AddContactAsync(Contact contact)
        {
            throw new NotImplementedException();
        }

        public Task DeleteContactAsync(Contact contact)
        {
            throw new NotImplementedException();
        }

        public Task<Contact> GetContactAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Contact>> GetContactsAsync()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(URL);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<IEnumerable<Contact>>(content);
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                    return null;
                }
            }
        }

        public Task UpdateContactAsync(Contact contact)
        {
            throw new NotImplementedException();
        }
    }
}
