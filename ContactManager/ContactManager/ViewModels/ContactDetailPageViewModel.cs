using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ContactManager.Models;
using ContactManager.Services;
using System.Collections.Generic;

namespace ContactManager.ViewModels
{
    public class ContactDetailPageViewModel
    {
        private bool _isEditMode;
        private IPageService _pageService;
        private IList<ContactViewModel> _contacts;
        private ContactViewModel _contactViewModel;
        private string _imageSourcePath { get { return "ContactManager.Images.image_contact.png"; } }

        public string PageTitle { get; private set; }
        public string ButtonText { get; private set; }
        public Contact Contact { get; private set; }
        public ImageSource ImageContact { get; private set; }

        public event EventHandler<Contact> ContactAdded;

        public event EventHandler<Contact> ContactUpdated;

        public ICommand SaveContactCommand { get; set; }

        public ContactDetailPageViewModel(ContactViewModel contactViewModel, IPageService pageService, IList<ContactViewModel> contacts)
        {
            _contactViewModel = contactViewModel;
            _pageService = pageService;
            _contacts = contacts;

            SetMode();
            BindUIValuesBasedOnMode();
            BindContact();
         
            SaveContactCommand = new Command(async () => await SaveContact());
        }

        private void SetMode()
        {
            if (string.IsNullOrWhiteSpace(_contactViewModel.FirstName))
                _isEditMode = false;
            else
                _isEditMode = true;
        }

        private void BindUIValuesBasedOnMode()
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

        private void BindContact()
        {
            Contact = new Contact();
            Contact.FirstName = _contactViewModel.FirstName;
            Contact.LastName = _contactViewModel.LastName;
            Contact.ContactNumber = _contactViewModel.ContactNumber;
        }

        private async Task SaveContact()
        {
            string errorMessage = string.Empty;
            if (!IsContactValid(out errorMessage))
            {
                await _pageService.DisplayAlert("Error", errorMessage, "Ok");
                return;
            }

            if (_isEditMode)
            {
                //Call Service or Persistence layer to add the contact model
                //Ex: await _contactService.AddContactAsync(Contact);
                //Ex: await _contactStore.AddContactAsync(Contact);

                ContactUpdated?.Invoke(this, Contact.Trim());
            }
            else
            {
                //Call Service or Persistence layer to update the contact model
                //Ex: await _contactService.UpdateContactAsync(Contact);
                //Ex: await _contactStore.UpdateContactAsync(Contact);

                ContactAdded?.Invoke(this, Contact.Trim());
            }
            await _pageService.PopAsync();
        }

        private bool IsContactValid(out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(Contact.FirstName))
            {
                errorMessage = "Please enter First Name";
                return false;
            }

            else if (string.IsNullOrWhiteSpace(Contact.LastName))
            {
                errorMessage = "Please enter Last Name";
                return false;
            }

            else if (string.IsNullOrWhiteSpace(Contact.ContactNumber))
            {
                errorMessage = "Please enter Contact Number";
                return false;
            }
            else if (HasDuplicate())
            {
                errorMessage = "Duplicate contact";
                return false;
            }
            else
            {
                errorMessage = string.Empty;
                return true;
            }
        }

        private bool HasDuplicate()
        {
            if (!_isEditMode || (_isEditMode && IsContactValueChanged()))
            {
                var contactInSubject = new ContactViewModel(Contact.Trim());

                foreach (var listedContact in _contacts)
                {
                    if (listedContact.FirstName.ToUpper() == contactInSubject.FirstName.ToUpper()
                        && listedContact.LastName.ToUpper() == contactInSubject.LastName.ToUpper()
                        && listedContact.ContactNumber == contactInSubject.ContactNumber)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsContactValueChanged()
        {
            var contactInSubject = new ContactViewModel(Contact.Trim());

            if (_contactViewModel.FirstName.ToUpper() == contactInSubject.FirstName.ToUpper()
                && _contactViewModel.LastName.ToUpper() == contactInSubject.LastName.ToUpper()
                && _contactViewModel.ContactNumber == contactInSubject.ContactNumber)
            {
                return false;
            }
            return true;
        }
    }
}
