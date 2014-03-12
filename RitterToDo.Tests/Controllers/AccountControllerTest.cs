using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoFakeItEasy;
using RitterToDo.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Should;
using RitterToDo.Tests.TestHelpers;
using FakeItEasy;
using RitterToDo.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Web.Mvc;

namespace RitterToDo.Tests.Controllers
{
    [TestFixture]
    public class AccountControllerTest
    {
        private AccountController CreateSUT()
        {
            var sut = new AccountController(
                A.Fake<UserManager<ApplicationUser>>(),
                A.Fake<IAuthenticationManager>());
            return sut;
        }

        [Test]
        public void Login_LoginPageRequest_ReturnsView()
        {
            var sut = CreateSUT();
            var url = @"http://www.uniritter.com.br";
            var result = sut.Login(url);
            var vr = result.ShouldBeViewResult();
            string returnUrl = vr.ViewBag.ReturnUrl;
            returnUrl.ShouldBeSameAs(url);
        }

        [Test]
        public async Task Login_InvalidUser_ReturnsError()
        {
            var sut = CreateSUT();
            var url = @"http://www.uniritter.com.br";
            var fixture = new Fixture();
            var lvm = fixture.Create<LoginViewModel>();
            A.CallTo(() => sut.UserManager.FindAsync(lvm.UserName, lvm.Password))
                .Returns(Task.FromResult<ApplicationUser>(null));

            var result = await sut.Login(lvm, url);

            var vr = result.ShouldBeViewResult();
            vr.Model.ShouldBeSameAs(lvm);
            sut.ModelState.IsValid.ShouldBeFalse();
        }
        
        [Test]
        public async Task Login_ValidUser_RedirectsToUrl()
        {
            var sut = CreateSUT();
            var url = @"http://www.uniritter.com.br";
            var fixture = new Fixture();
            var lvm = fixture.Create<LoginViewModel>();
            var appUser = fixture.Create<ApplicationUser>();
            var claimsId = new ClaimsIdentity();
            sut.Url = A.Fake<UrlHelper>();

            A.CallTo(() => sut.UserManager.FindAsync(lvm.UserName, lvm.Password))
                .Returns(Task.FromResult<ApplicationUser>(appUser));
            A.CallTo(() => sut.UserManager.CreateIdentityAsync(
                appUser,
                DefaultAuthenticationTypes.ApplicationCookie))
                .Returns(Task.FromResult<ClaimsIdentity>(claimsId));
            A.CallTo(() => sut.Url.IsLocalUrl(url))
                .Returns(true);

            var result = await sut.Login(lvm, url);

            var vr = result.ShouldBeRedirectResult();
            vr.Url.ShouldBeSameAs(url);
        }
    }
}
