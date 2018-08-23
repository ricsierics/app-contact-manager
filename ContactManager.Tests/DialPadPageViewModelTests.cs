using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContactManager.Services;
using Moq;
using ContactManager.ViewModels;

namespace ContactManager.Tests
{   
    [TestClass]
    public class DialPadPageViewModelTests
    {
        private Mock<IPageService> _pageService;
        private DialPadPageViewModel _viewModel;

        [TestInitialize]
        public void TestInitialize()
        {
            _pageService = new Mock<IPageService>();
            _viewModel = new DialPadPageViewModel(_pageService.Object);
        }

        [TestMethod]
        public void DialCommand_WhenCalledWithParameter_ShouldSetDialDisplayWithThatOfTheParameter()
        {
            _viewModel.DialCommand.Execute("123");

            Assert.AreEqual(_viewModel.DialDisplay, "123");
        }

        [TestMethod]
        public void BackspaceCommand_WhenDialDisplayIsEmpty_ShouldSetDialDisplayEmpty()
        {
            _viewModel.DialCommand.Execute("");
            _viewModel.BackspaceCommand.Execute(null);

            Assert.AreEqual(_viewModel.DialDisplay, "");
        }

        [TestMethod]
        public void BackspaceCommand_WhenDialDisplayHasOneDigit_ShouldRemoveTheDigit()
        {
            _viewModel.DialCommand.Execute("1");
            _viewModel.BackspaceCommand.Execute(null);

            Assert.AreEqual(_viewModel.DialDisplay, "");
        }

        [TestMethod]
        public void BackspaceCommand_WhenDialDisplayHasMultipleDigits_ShouldRemoveTheLastDigit()
        {
            _viewModel.DialCommand.Execute("123");
            _viewModel.BackspaceCommand.Execute(null);

            Assert.AreEqual(_viewModel.DialDisplay, "12");
        }

        [TestMethod]
        public void CallCommand_WhenDialDisplayIsEmpty_ShouldDisplayAlert()
        {
            _viewModel.DialCommand.Execute("");
            _viewModel.CallCommand.Execute(null);

            _pageService.Verify(x => x.DisplayAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void CallCommand_WhenDialDisplayIsNotEmpty_ShouldNotDisplayAlertAndCall()
        {
            _viewModel.DialCommand.Execute("09277607229");
            _viewModel.CallCommand.Execute(null);

            _pageService.Verify(x => x.DisplayAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}
