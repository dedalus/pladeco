using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Pladeco.Model;
using Pladeco.Web.Data;
using Pladeco.Web.Helpers;
using Pladeco.Web.Models;

namespace Pladeco.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper userHelper;
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;

        public AccountController(
            IUserHelper userHelper,
            ApplicationDbContext context,
            IConfiguration configuration)
        {
            this.userHelper = userHelper;
            this.context = context;
            this.configuration = configuration;
        }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = await this.userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (this.Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return this.Redirect(this.Request.Query["ReturnUrl"].First());
                    }

                    return this.RedirectToAction("Index", "Home");
                }
            }

            this.ModelState.AddModelError(string.Empty, "Failed to login.");
            return this.View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await this.userHelper.LogoutAsync();
            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult Create()
        {
            var model = new CreateUserViewModel
            {
                Active=true,
                Areas= new SelectList(context.Areas, "ID", "Name")
            //Countries = this.countryRepository.GetComboCountries(),
            //Cities = this.countryRepository.GetComboCities(0)
        };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(model.Username);
                if (user == null)
                {
                    user = new User
                    {
                        Name = model.Name,
                        Email = model.Username,
                        UserName = model.Username,
                        AreaID=model.AreaID
                    };

                    var result = await this.userHelper.AddUserAsync(user, model.Password);
                    if (result != IdentityResult.Success)
                    {
                        this.ModelState.AddModelError(string.Empty, "The user couldn't be created.");
                        return this.View(model);
                    }

                    //var myToken = await this.userHelper.GenerateEmailConfirmationTokenAsync(user);
                    //var tokenLink = this.Url.Action("ConfirmEmail", "Account", new
                    //{
                    //    userid = user.Id,
                    //    token = myToken
                    //}, protocol: HttpContext.Request.Scheme);

                    //this.mailHelper.SendMail(model.Username, "Shop Email confirmation", $"<h1>Shop Email Confirmation</h1>" +
                    //    $"To allow the user, " +
                    //    $"plase click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");
                    //this.ViewBag.Message = "The instructions to allow your user has been sent to email.";
                    //return this.View(model);
                    return this.RedirectToAction(nameof(Index));
                }

                this.ModelState.AddModelError(string.Empty, "The username is already registered.");
            }

            return this.View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await this.userHelper.GetUserByIdAsync(id);
            var model = new CreateUserViewModel();

            if (user != null)
            {
                model.ID = user.Id;
                model.Name = user.Name;
                model.Username = user.UserName;
                model.AreaID = user.AreaID;

                model.Areas = new SelectList(context.Areas, "ID", "Name", model.AreaID);

            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CreateUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(model.ID);
                if (user != null)
                {
                    user.Name = model.Name;
                    user.AreaID = model.AreaID;

                    var respose = await this.userHelper.UpdateUserAsync(user);
                    if (respose.Succeeded)
                    {
                        if(model.Password.Trim() != string.Empty)
                        {
                            if(model.Password.Trim() != model.Confirm.Trim())
                            {
                                this.ModelState.AddModelError(string.Empty, "Las contraseñas no coinciden.");
                            }
                            else
                            {
                                //var result = await this.userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                                //if (result.Succeeded)
                                //{
                                //    return RedirectToAction(nameof(Details), new { model.ID });
                                //}
                            }
                        }
                        return RedirectToAction(nameof(Details), new { model.ID });
                        //this.ViewBag.UserMessage = "User updated!";
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, respose.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "User not found.");
                }
            }

            return this.View(model);
        }

        //public IActionResult ChangePassword()
        //{
        //    return this.View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        //{
        //    if (this.ModelState.IsValid)
        //    {
        //        var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
        //        if (user != null)
        //        {
        //            var result = await this.userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        //            if (result.Succeeded)
        //            {
        //                return this.RedirectToAction("ChangeUser");
        //            }
        //            else
        //            {
        //                this.ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
        //            }
        //        }
        //        else
        //        {
        //            this.ModelState.AddModelError(string.Empty, "User no found.");
        //        }
        //    }

        //    return this.View(model);
        //}


        public IActionResult NotAuthorized()
        {
            return this.View();
        }


        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return this.NotFound();
            }

            var user = await this.userHelper.GetUserByIdAsync(userId);
            if (user == null)
            {
                return this.NotFound();
            }

            var result = await this.userHelper.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return this.NotFound();
            }

            return View();
        }

        public IActionResult RecoverPassword()
        {
            return this.View();
        }

        //[HttpPost]
        //public async Task<IActionResult> RecoverPassword(RecoverPasswordViewModel model)
        //{
        //    if (this.ModelState.IsValid)
        //    {
        //        var user = await this.userHelper.GetUserByEmailAsync(model.Email);
        //        if (user == null)
        //        {
        //            ModelState.AddModelError(string.Empty, "The email doesn't correspont to a registered user.");
        //            return this.View(model);
        //        }

        //        var myToken = await this.userHelper.GeneratePasswordResetTokenAsync(user);
        //        var link = this.Url.Action("ResetPassword", "Account", new { token = myToken }, protocol: HttpContext.Request.Scheme);
        //        var mailSender = new MailHelper(configuration);
        //        mailSender.SendMail(model.Email, "Shop Password Reset", $"<h1>Shop Recover Password</h1>" +
        //            $"To reset the password click in this link:</br></br>" +
        //            $"<a href = \"{link}\">Reset Password</a>");
        //        this.ViewBag.Message = "The instructions to recover your password has been sent to email.";
        //        return this.View();

        //    }

        //    return this.View(model);
        //}

        public IActionResult ResetPassword(string token)
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //    var user = await this.userHelper.GetUserByEmailAsync(model.UserName);
        //    if (user != null)
        //    {
        //        var result = await this.userHelper.ResetPasswordAsync(user, model.Token, model.Password);
        //        if (result.Succeeded)
        //        {
        //            this.ViewBag.Message = "Password reset successful.";
        //            return this.View();
        //        }

        //        this.ViewBag.Message = "Error while resetting the password.";
        //        return View(model);
        //    }

        //    this.ViewBag.Message = "User not found.";
        //    return View(model);
        //}

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var users = await this.userHelper.GetAllUsersAsync();
            //foreach (var user in users)
            //{
            //    var myUser = await this.userHelper.GetUserByIdAsync(user.Id);
            //    if (myUser != null)
            //    {
            //        user.IsAdmin = await this.userHelper.IsUserInRoleAsync(myUser, "Admin");
            //    }
            //}

            return this.View(users);
        }


        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> AdminOff(string id)
        //{
        //    if (string.IsNullOrEmpty(id))
        //    {
        //        return NotFound();
        //    }

        //    var user = await this.userHelper.GetUserByIdAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    await this.userHelper.RemoveUserFromRoleAsync(user, "Admin");
        //    return this.RedirectToAction(nameof(Index));
        //}

        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> AdminOn(string id)
        //{
        //    if (string.IsNullOrEmpty(id))
        //    {
        //        return NotFound();
        //    }

        //    var user = await this.userHelper.GetUserByIdAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    await this.userHelper.AddUserToRoleAsync(user, "Admin");
        //    return this.RedirectToAction(nameof(Index));
        //}

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userHelper.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await this.userHelper.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await this.userHelper.DeleteUserAsync(user);
            return this.RedirectToAction(nameof(Index));
        }
    }
}