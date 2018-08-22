﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ContactManager.Models;
using ContactManager.Services;

namespace ContactManager.ViewModels
{
    public class ContactDetailPageViewModel
    {
        private bool _isEditMode;
        private IPageService _pageService;
        private string _imageSourcePath { get { return "ContactManager.Images.image_contact.png"; } }

        public string PageTitle { get; private set; }
        public string ButtonText { get; private set; }
        public Contact Contact { get; private set; }
        public ImageSource ImageContact { get; private set; }

        public event EventHandler<Contact> ContactAdded;

        public event EventHandler<Contact> ContactUpdated;

        public ICommand SaveContactCommand { get; set; }

        public ContactDetailPageViewModel(ContactViewModel contactViewModel, IPageService pageService)
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
            else
            {
                errorMessage = string.Empty;
                return true;
            }
        }
    }
}