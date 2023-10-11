namespace Contacts.UseCases.Interfaces
{
    public interface IEditContactUseCase
    {
        Task ExecuteAsync(int contactId, CoreBussiness.Contact contact);
    }
}