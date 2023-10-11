namespace Contacts.UseCases.Interfaces
{
    public interface IViewContactUseCase
    {
        Task<CoreBussiness.Contact> ExecuteAsync(int contactId);
    }
}