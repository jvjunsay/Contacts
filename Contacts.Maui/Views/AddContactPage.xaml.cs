using Contacts.UseCases.Interfaces;

using Contact = Contacts.CoreBussiness.Contact;

namespace Contacts.Maui.Views;

public partial class AddContactPage : ContentPage
{
    private readonly IAddContactUseCase addContactUseCase;

    public AddContactPage(IAddContactUseCase addContactUseCase)
    {
        InitializeComponent();
        this.addContactUseCase = addContactUseCase;
    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//{nameof(ContactsPage)}");
    }

    private async void contactControl_OnSave(object sender, EventArgs e)
    {
        await addContactUseCase.ExecuteAsync(new Contact
        {
            Name = contactControl.Name,
            Email = contactControl.Email,
            Phone = contactControl.Phone,
            Address = contactControl.Address,
        });
        await Shell.Current.GoToAsync("..");
    }

    private void contactControl_OnError(object sender, string e)
    {
        DisplayAlert("Error", e, "OK");
    }
}