using ContactManager.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ContactManager.ViewModels
{
    public class ContactDetailViewModel
    {
        private bool _isEditMode;
        private IPageService _pageService;
        
        public string PageTitle { get; private set; }
        public string ButtonText { get; private set; }
        public Contact Contact { get; private set; }
        private string _imageSourcePath { get { return "ContactManager.Images.image_contact.png"; } } 
        public ImageSource ImageContact { get; private set; }

        public event EventHandler<Contact> ContactAdded;

        public event EventHandler<Contact> ContactUpdated;

        public ICommand SaveContactCommand { get; set; }

        public ContactDetailViewModel(ContactViewModel contactViewModel, IPageService pageService)
        {
            if (string.IsNullOrWhiteSpace(contactViewModel.FirstName))
                _isEditMode = false;
            else
                _isEditMode = true;

            BindUIValues(_isEditMode);

            Contact = new Contact();
            Contact.FirstName = contactViewModel.FirstName;
            Contact.LastName = contactViewModel.LastName;
            Contact.ContactNumber = contactViewModel.ContactNumber;
           
            _pageService = pageService;

            SaveContactCommand = new Command(async () => await SaveContact());
        }

        private void BindUIValues(bool mode)
        {
            ImageContact = ImageSource.FromResource(_imageSourcePath);
            if (_isEditMode)
            {
                PageTitle = "Edit Contact";
                ButtonText = "Update";
            }
            else
            {
                PageTitle = "Add Contact";
                ButtonText = "Save";
            }
        }

        private async Task SaveContact()
        {
            if (string.IsNullOrWhiteSpace(Contact.FirstName))
            {
                await _pageService.DisplayAlert("Error", "Please enter First Name", "Ok");
                return;
            }

            if (string.IsNullOrWhiteSpace(Contact.LastName))
            {
                await _pageService.DisplayAlert("Error", "Please enter Last Name", "Ok");
                return;
            }

            if (string.IsNullOrWhiteSpace(Contact.ContactNumber))
            {
                await _pageService.DisplayAlert("Error", "Please enter Contact Number", "Ok");
                return;
            }

            if (_isEditMode)
            {
                //Call Service or Persistence layer to add the contact model
                //Ex: await _contactService.AddContactAsync(Contact);
                //Ex: await _contactStore.AddContactAsync(Contact);

                ContactUpdated?.Invoke(this, Contact);
            }
            else
            {
                //Call Service or Persistence layer to update the contact model
                //Ex: await _contactService.UpdateContactAsync(Contact);
                //Ex: await _contactStore.UpdateContactAsync(Contact);

                ContactAdded?.Invoke(this, Contact);
            }
            await _pageService.PopAsync();
        }
    }
}
