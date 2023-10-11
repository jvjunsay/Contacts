

namespace Contacts.Maui.Models
{
    public static class ContactRepository
    {
        public static List<Contact> _contacts = new List<Contact>()
        {
            new Contact{Name = "John Doe", Email="johndoe@gmail.com", ContactId=1},
            new Contact { Name = "Jane Doe", Email = "janedoe@gmail.com",ContactId=2 },
            new Contact { Name = "Tom Hanks", Email = "tomhanks@gmail.com", ContactId=3 },
            new Contact { Name = "JV Junsay", Email = "jvjunsay@gmail.com",ContactId=4 }
        };

        public static List<Contact> GetContacts() => _contacts;

        public static Contact GetContactById(int contactId)
        {
            var contact = _contacts.FirstOrDefault(q => q.ContactId == contactId);
            if (contact != null)
            {
                return new Contact
                {
                    Address = contact.Address,
                    Email = contact.Email,
                    Name = contact.Name,
                    Phone = contact.Phone,
                    ContactId = contactId
                };
            }

            return null;
        }

        public static void UpdateContact(int contactId, Contact contact)
        {
            var contactToUpdate = _contacts.FirstOrDefault(q => q.ContactId == contactId);
            if (contactToUpdate != null)
            {
                contactToUpdate.Name = contact.Name;
                contactToUpdate.Email = contact.Email;
                contactToUpdate.Address = contact.Address;
                contactToUpdate.Phone = contact.Phone;
            }
        }

        public static void AddContact(Contact contact)
        {

            var maxId = _contacts.Max(q => q.ContactId);
            contact.ContactId = maxId + 1;

            _contacts.Add(contact);
        }

        public static void DeleteContact(int contactId)
        {
            var contact = _contacts.FirstOrDefault(q => q.ContactId == contactId);
            if (contact != null)
            {
                _contacts.Remove(contact);
            }
        }

        public static List<Contact> SearchContacts(string filterText)
        {
            var contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.StartsWith(filterText, StringComparison.OrdinalIgnoreCase)).ToList();

            if (contacts == null || contacts.Count <= 0)
                contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Email) && x.Email.StartsWith(filterText, StringComparison.OrdinalIgnoreCase)).ToList();
            else
                return contacts;

            if (contacts == null || contacts.Count <= 0)
                contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Address) && x.Address.StartsWith(filterText, StringComparison.OrdinalIgnoreCase)).ToList();
            else
                return contacts;

            if (contacts == null || contacts.Count <= 0)
                contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Phone) && x.Phone.StartsWith(filterText, StringComparison.OrdinalIgnoreCase)).ToList();
            else
                return contacts;

            return contacts;
        }
    }
}
