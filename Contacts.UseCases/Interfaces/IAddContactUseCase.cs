namespace Contacts.UseCases.Interfaces
{
    public interface IAddContactUseCase
    {
        Task ExecuteAsync(CoreBussiness.Contact contact);
    }
}