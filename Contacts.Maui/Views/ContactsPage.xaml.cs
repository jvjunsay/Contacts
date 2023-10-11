using Contacts.UseCases.Interfaces;
using System.Collections.ObjectModel;
using Contact = Contacts.CoreBussiness.Contact;

namespace Contacts.Maui.Views;

public partial class ContactsPage : ContentPage
{
    private readonly IViewContactsUseCase viewUseCase;
    private readonly IDeleteContactUseCase deleteContactUseCase;

    public ContactsPage(IViewContactsUseCase viewUseCase, IDeleteContactUseCase deleteContactUseCase)
    {
        InitializeComponent();
        this.viewUseCase = viewUseCase;
        this.deleteContactUseCase = deleteContactUseCase;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        SearchBar.Text = string.Empty;
        LoadContacts();
    }

    private async void listContacts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        // logic
        if (listContacts.SelectedItem != null)
        {
            await Shell.Current.GoToAsync($"{nameof(EditContactPage)}?Id={((CoreBussiness.Contact)listContacts.SelectedItem).ContactId}");
        }

    }

    private void listContacts_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        listContacts.SelectedItem = null;
    }

    private void btnAdd_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(AddContactPage));
    }

    private void Delete_Clicked(object sender, EventArgs e)
    {
        var menuItem = sender as MenuItem;
        var contact = menuItem.CommandParameter as Contact;

        deleteContactUseCase.ExecuteAsync(contact.ContactId);
        LoadContacts();
    }

    private async void LoadContacts()
    {
        var contacts = new ObservableCollection<Contact>(await this.viewUseCase.ExecuteAsync(string.Empty));

        listContacts.ItemsSource = contacts;
    }

    private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        var contacts = new ObservableCollection<Contact>(await this.viewUseCase.ExecuteAsync(((SearchBar)sender).Text));
        listContacts.ItemsSource = contacts;
    }
}