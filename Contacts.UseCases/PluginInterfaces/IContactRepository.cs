using Contact = Contacts.CoreBussiness.Contact;

namespace Contacts.UseCases.PluginInterfaces
{
    public interface IContactRepository
    {
        Task AddContact(Contact contact);
        Task DeleteContact(int contactId);
        Task<Contact> GetContactById(int contactId);
        Task<List<Contact>> GetContactsAsync(string filterText);
        Task UpdateContact(int contactId, Contact contact);
    }
}
