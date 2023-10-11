using Contacts.UseCases.PluginInterfaces;
using Contact = Contacts.CoreBussiness.Contact;

namespace Contacts.Plugins.DataStore.InMemory
{
    // All the code in this file is included in all platforms.
    public class ContactInMemoryRepository : IContactRepository
    {
        public static List<Contact> _contacts;

        public ContactInMemoryRepository()
        {
            _contacts = new List<Contact>()
            {
                new Contact{Name = "John Doe", Email="johndoe@gmail.com", ContactId=1},
                new Contact { Name = "Jane Doe", Email = "janedoe@gmail.com",ContactId=2 },
                new Contact { Name = "Tom Hanks", Email = "tomhanks@gmail.com", ContactId=3 },
                new Contact { Name = "JV Junsay", Email = "jvjunsay@gmail.com",ContactId=4 }
            };
        }

        public Task AddContact(Contact contact)
        {
            var maxId = _contacts.Max(q => q.ContactId);
            contact.ContactId = maxId + 1;

            _contacts.Add(contact);
            return Task.CompletedTask;
        }

        public Task DeleteContact(int contactId)
        {
            var contact = _contacts.FirstOrDefault(q => q.ContactId == contactId);
            if (contact != null)
            {
                _contacts.Remove(contact);
            }

            return Task.CompletedTask;
        }

        public Task<Contact> GetContactById(int contactId)
        {
            var contact = _contacts.FirstOrDefault(q => q.ContactId == contactId);
            if (contact != null)
            {
                return Task.FromResult(new Contact
                {
                    Address = contact.Address,
                    Email = contact.Email,
                    Name = contact.Name,
                    Phone = contact.Phone,
                    ContactId = contactId
                });
            }

            return null;
        }

        public Task<List<Contact>> GetContactsAsync(string filterText)
        {
            if (string.IsNullOrWhiteSpace(filterText))
            {
                return Task.FromResult(_contacts);
            }

            var contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.StartsWith(filterText, StringComparison.OrdinalIgnoreCase)).ToList();

            if (contacts == null || contacts.Count <= 0)
                contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Email) && x.Email.StartsWith(filterText, StringComparison.OrdinalIgnoreCase)).ToList();
            else
                return Task.FromResult(contacts);

            if (contacts == null || contacts.Count <= 0)
                contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Address) && x.Address.StartsWith(filterText, StringComparison.OrdinalIgnoreCase)).ToList();
            else
                return Task.FromResult(contacts);

            if (contacts == null || contacts.Count <= 0)
                contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Phone) && x.Phone.StartsWith(filterText, StringComparison.OrdinalIgnoreCase)).ToList();
            else
                return Task.FromResult(contacts);

            return Task.FromResult(contacts);
        }

        public Task UpdateContact(int contactId, Contact contact)
        {
            if (contactId != contact.ContactId) return Task.CompletedTask;

            var contactToUpdate = _contacts.FirstOrDefault(q => q.ContactId == contactId);
            if (contactToUpdate != null)
            {
                contactToUpdate.Name = contact.Name;
                contactToUpdate.Email = contact.Email;
                contactToUpdate.Address = contact.Address;
                contactToUpdate.Phone = contact.Phone;
            }

            return Task.CompletedTask;
        }
    }
}