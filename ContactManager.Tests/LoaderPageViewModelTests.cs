using ContactManager.Services;
using ContactManager.ViewModels;
using ContactManager.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace ContactManager.Tests
{
    [TestClass]
    public class LoaderPageViewModelTests
    {
        private LoaderPageViewModel _viewModel;
        private Mock<IPageService> _pageService;
        private Mock<IContactService> _contactService;

        [TestInitialize]
        public void TestInitialize()
        {
            _pageService = new Mock<IPageService>();
            _contactService = new Mock<IContactService>();
            _viewModel = new LoaderPageViewModel(_pageService.Object, _contactService.Object);
        }

        [Ignore]
        [TestMethod]
        public void LoadDataCommand_WhenCalled_ShouldCallTheContactService()
        {
            _viewModel.LoadDataCommand.Execute(null);
            _contactService.Verify(g => g.GetContactsAsync());
        }

        [Ignore]
        [TestMethod]
        public void GoToHomePageCommand_WhenCalled_ShouldNavigateTheUserToHomePage()
        {
            _viewModel.GoToHomePageCommand.Execute(null);
            _pageService.Verify(p => p.PushAsync(It.IsAny<HomePage>()));
        }

        [Ignore]
        [TestMethod]
        public void LoadDataCommand_WhenContactServiceReturnsNull_ShouldSetError()
        {
            _contactService.Setup(x => x.GetContactsAsync()).ReturnsAsync(() => null);
            _viewModel.LoadDataCommand.Execute(null);

            Assert.IsTrue(_viewModel.IsErrorEncountered, "IsErrorEncountered should be set to true");
            Assert.IsTrue(_viewModel.BodyText.Contains("error"), "BodyText should contain error");
        }
    }
}
