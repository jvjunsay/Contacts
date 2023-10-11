using Contacts.UseCases.Interfaces;
using Contacts.UseCases.PluginInterfaces;

using Contact = Contacts.CoreBussiness.Contact;

namespace Contacts.UseCases
{
    public class EditContactUseCase : IEditContactUseCase
    {
        private readonly IContactRepository contactRepository;

        public EditContactUseCase(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }
        public async Task ExecuteAsync(int contactId, Contact contact)
        {
            await this.contactRepository.UpdateContact(contactId, contact);
        }
    }
}
