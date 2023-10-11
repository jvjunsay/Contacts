namespace Contacts.UseCases.Interfaces
{
    public interface IViewContactsUseCase
    {
        Task<List<CoreBussiness.Contact>> ExecuteAsync(string filterText);
    }
}