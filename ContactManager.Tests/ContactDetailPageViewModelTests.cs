using System.Collections.Generic;
using ContactManager.Services;
using ContactManager.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ContactManager.Tests
{
    [TestClass]
    public class ContactDetailPageViewModelTests
    {
        private ContactDetailPageViewModel _viewModel;
        private Mock<IPageService> _pageService;
        private ContactViewModel _contactViewModel;
        private List<ContactViewModel> _contacts;

        [TestInitialize]
        public void TestInitialize()
        {
            _pageService = new Mock<IPageService>();
            _contactViewModel = new ContactViewModel();
        }

        [TestMethod]
        public void WhenPageDisplayedAndIsInEditMode_ShouldDisplayEditContactTitle()
        {
            _contactViewModel.FirstName = "Rics";
            _contactViewModel.LastName = "Magboo";
            _contactViewModel.ContactNumber = "09277607229";

            _viewModel = new ContactDetailPageViewModel(_contactViewModel, _pageService.Object, _contacts);

            Assert.IsTrue(_viewModel.PageTitle.Contains("Edit"));
        }

        [TestMethod]
        public void WhenPageDisplayedAndIsInEditMode_ShouldDisplayUpdateButtonText()
        {
            _contactViewModel.FirstName = "Rics";
            _contactViewModel.LastName = "Magboo";
            _contactViewModel.ContactNumber = "09277607229";

            _viewModel = new ContactDetailPageViewModel(_contactViewModel, _pageService.Object, _contacts);

            Assert.IsTrue(_viewModel.ButtonText.Contains("Update"));
        }

        [TestMethod]
        public void WhenPageDisplayedAndIsInEditMode_ShouldDisplayTheContactDetails()
        {
            _contactViewModel.FirstName = "Rics";
            _contactViewModel.LastName = "Magboo";
            _contactViewModel.ContactNumber = "09277607229";

            _viewModel = new ContactDetailPageViewModel(_contactViewModel, _pageService.Object, _contacts);

            Assert.IsTrue(_viewModel.Contact.FirstName.Equals(_contactViewModel.FirstName));
            Assert.IsTrue(_viewModel.Contact.LastName.Equals(_contactViewModel.LastName));
            Assert.IsTrue(_viewModel.Contact.ContactNumber.Equals(_contactViewModel.ContactNumber));
        }

        [TestMethod]
        public void WhenPageDisplayedAndIsInAddMode_ShouldDisplayAddContactTitle()
        {
            _viewModel = new ContactDetailPageViewModel(_contactViewModel, _pageService.Object, _contacts);

            Assert.IsTrue(_viewModel.PageTitle.Contains("Add"));
        }

        [TestMethod]
        public void WhenPageDisplayedAndIsInAddMode_ShouldDisplayUpdateButtonText()
        {
            _viewModel = new ContactDetailPageViewModel(_contactViewModel, _pageService.Object, _contacts);

            Assert.IsTrue(_viewModel.ButtonText.Contains("Save"));
        }

        [TestMethod]
        public void SaveContactCommand_WhenFirstNameIsEmpty_ShouldDisplayAlertAndNotRaiseEventsAndNotNavigateBackToContactsPage()
        {
            _contactViewModel.FirstName = "";
            _contactViewModel.LastName = "Magboo";
            _contactViewModel.ContactNumber = "09277607229";
            bool isContactAddedEventCalled = false;
            bool isContactUpdatedEventCalled = false;

            _viewModel = new ContactDetailPageViewModel(_contactViewModel, _pageService.Object, _contacts);
            _viewModel.SaveContactCommand.Execute(null);
            _viewModel.ContactAdded += (sender, args) => isContactAddedEventCalled = true;
            _viewModel.ContactUpdated += (sender, args) => isContactUpdatedEventCalled = true;

            _pageService.Verify(x => x.DisplayAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.IsFalse(isContactAddedEventCalled);
            Assert.IsFalse(isContactUpdatedEventCalled);
            _pageService.Verify(x => x.PopAsync(), Times.Never);
            _pageService.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void SaveContactCommand_WhenLastNameIsEmpty_ShouldDisplayAlertAndNotRaiseEventsAndNotNavigateBackToContactsPage()
        {
            _contactViewModel.FirstName = "Rics";
            _contactViewModel.LastName = "";
            _contactViewModel.ContactNumber = "09277607229";
            bool isContactAddedEventCalled = false;
            bool isContactUpdatedEventCalled = false;

            _viewModel = new ContactDetailPageViewModel(_contactViewModel, _pageService.Object, _contacts);
            _viewModel.ContactAdded += (sender, args) => isContactAddedEventCalled = true;
            _viewModel.ContactUpdated += (sender, args) => isContactUpdatedEventCalled = true;
            _viewModel.SaveContactCommand.Execute(null);
            
            _pageService.Verify(x => x.DisplayAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.IsFalse(isContactAddedEventCalled);
            Assert.IsFalse(isContactUpdatedEventCalled);
            _pageService.Verify(x => x.PopAsync(), Times.Never);
            _pageService.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void SaveContactCommand_WhenContactNumberIsEmpty_ShouldDisplayAlertAndNotRaiseEventsAndNotNavigateBackToContactsPage()
        {
            _contactViewModel.FirstName = "Rics";
            _contactViewModel.LastName = "Magboo";
            _contactViewModel.ContactNumber = "";
            bool isContactAddedEventCalled = false;
            bool isContactUpdatedEventCalled = false;

            _viewModel = new ContactDetailPageViewModel(_contactViewModel, _pageService.Object, _contacts);
            _viewModel.ContactAdded += (sender, args) => isContactAddedEventCalled = true;
            _viewModel.ContactUpdated += (sender, args) => isContactUpdatedEventCalled = true;
            _viewModel.SaveContactCommand.Execute(null);

            _pageService.Verify(x => x.DisplayAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.IsFalse(isContactAddedEventCalled);
            Assert.IsFalse(isContactUpdatedEventCalled);
            _pageService.Verify(x => x.PopAsync(), Times.Never);
            _pageService.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void SaveContactCommand_WhenAddUniqueContactDetailInput_ShouldNotDisplayAlertAndShouldRaiseContactAddedEventAndShouldNavigateBackToContactsPage()
        {
            _contacts = new List<ContactViewModel>();
            bool isContactAddedEventCalled = false;
            bool isContactUpdatedEventCalled = false;

            _viewModel = new ContactDetailPageViewModel(_contactViewModel, _pageService.Object, _contacts);
            _viewModel.Contact.FirstName = "Rics";
            _viewModel.Contact.LastName = "Magboo";
            _viewModel.Contact.ContactNumber = "09277607229";
            _viewModel.ContactAdded += (sender, args) => isContactAddedEventCalled = true;
            _viewModel.ContactUpdated += (sender, args) => isContactUpdatedEventCalled = true;
            _viewModel.SaveContactCommand.Execute(null);

            _pageService.Verify(x => x.DisplayAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            Assert.IsTrue(isContactAddedEventCalled);
            Assert.IsFalse(isContactUpdatedEventCalled);
            _pageService.Verify(x => x.PopAsync(), Times.Once);
            _pageService.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void SaveContactCommand_WhenEditUniqueContactDetailInput_ShouldNotDisplayAlertAndShouldRaiseContactUpdatedEventAndShouldNavigateBackToContactsPage()
        {
            _contactViewModel.FirstName = "Rics";
            _contactViewModel.LastName = "Magboo";
            _contactViewModel.ContactNumber = "09277607229";
            _contacts = new List<ContactViewModel>();
            bool isContactAddedEventCalled = false;
            bool isContactUpdatedEventCalled = false;

            _viewModel = new ContactDetailPageViewModel(_contactViewModel, _pageService.Object, _contacts);
            _viewModel.Contact.FirstName = "Rics Updated";
            _viewModel.Contact.LastName = "Magboo Updated";
            _viewModel.Contact.ContactNumber = "09277777777";
            _viewModel.ContactAdded += (sender, args) => isContactAddedEventCalled = true;
            _viewModel.ContactUpdated += (sender, args) => isContactUpdatedEventCalled = true;
            _viewModel.SaveContactCommand.Execute(null);

            _pageService.Verify(x => x.DisplayAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            Assert.IsFalse(isContactAddedEventCalled);
            Assert.IsTrue(isContactUpdatedEventCalled);
            _pageService.Verify(x => x.PopAsync(), Times.Once);
            _pageService.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void SaveContactCommand_WhenAddDuplicateContactDetailInput_ShouldDisplayAlertAndNotRaiseEventsAndNotNavigateBackToContactsPage()
        {
            _contacts = new List<ContactViewModel>();
            _contacts.Add(new ContactViewModel() { FirstName = "Rics", LastName = "Magboo", ContactNumber = "09277607229" });
            bool isContactAddedEventCalled = false;
            bool isContactUpdatedEventCalled = false;

            _viewModel = new ContactDetailPageViewModel(_contactViewModel, _pageService.Object, _contacts);
            _viewModel.Contact.FirstName = "Rics";
            _viewModel.Contact.LastName = "Magboo";
            _viewModel.Contact.ContactNumber = "09277607229";
            _viewModel.ContactAdded += (sender, args) => isContactAddedEventCalled = true;
            _viewModel.ContactUpdated += (sender, args) => isContactUpdatedEventCalled = true;
            _viewModel.SaveContactCommand.Execute(null);

            _pageService.Verify(x => x.DisplayAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.IsFalse(isContactAddedEventCalled);
            Assert.IsFalse(isContactUpdatedEventCalled);
            _pageService.Verify(x => x.PopAsync(), Times.Never);
            _pageService.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void SaveContactCommand_WhenEditDuplicateContactDetailInput_ShouldDisplayAlertAndNotRaiseEventsAndNotNavigateBackToContactsPage()
        {
            _contactViewModel.FirstName = "Juan";
            _contactViewModel.LastName = "Dela Cruz";
            _contactViewModel.ContactNumber = "09180000000";
            _contacts = new List<ContactViewModel>();
            _contacts.Add(new ContactViewModel() { FirstName = "Rics", LastName = "Magboo", ContactNumber = "09277607229" });
            bool isContactAddedEventCalled = false;
            bool isContactUpdatedEventCalled = false;

            _viewModel = new ContactDetailPageViewModel(_contactViewModel, _pageService.Object, _contacts);
            _viewModel.Contact.FirstName = "Rics";
            _viewModel.Contact.LastName = "Magboo";
            _viewModel.Contact.ContactNumber = "09277607229";
            _viewModel.ContactAdded += (sender, args) => isContactAddedEventCalled = true;
            _viewModel.ContactUpdated += (sender, args) => isContactUpdatedEventCalled = true;
            _viewModel.SaveContactCommand.Execute(null);

            _pageService.Verify(x => x.DisplayAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.IsFalse(isContactAddedEventCalled);
            Assert.IsFalse(isContactUpdatedEventCalled);
            _pageService.Verify(x => x.PopAsync(), Times.Never);
            _pageService.VerifyNoOtherCalls();
        }
    }
}
