using Plugin.Connectivity;
using Riddha.CourierMS.Common.Model;
using Riddha.CourierMS.Common.Services;
using Riddha.CourierMS.DependencyServices;
using Riddha.CourierMS.Model;
using Riddha.CourierMS.Services;
using Riddha.CourierMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Riddha.CourierMS.App;

namespace Riddha.CourierMS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        LoginServices _loginServices;
        bool PasswordValid = false;
        bool UserNameValid = false;
        LoginVm vm;
        public LoginView()
        {
            vm = new LoginVm();
            InitializeComponent();
            this.BindingContext = vm;
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IKeyboardHelper>().HideKeyboard();
            this.Navigation.PushModalAsync(new MainPage());
        }



        private void UsernameEntry_Focused(object sender, FocusEventArgs e)
        {
            UsernameFrame.BorderColor = Color.FromHex("#7BE495");
        }
        private void UsernameEntry_UnFocused(object sender, FocusEventArgs e)
        {
            UsernameFrame.BorderColor = Color.LightGray;
        }
        private void UsernameText_Changed(object sender, TextChangedEventArgs e)
        {


            if (e.NewTextValue.Length >= 1)
            {
                UsernameSucessIndicator.IsVisible = true;
                UserNameValid = true;
            }
            else
            {
                UsernameSucessIndicator.IsVisible = false;
                UserNameValid = false;
            }
        }



        private void VisiblePasswordButton_Clicked(object sender, EventArgs e)
        {
            if (PasswordEntry.IsPassword == true)
            {
                PasswordEntry.IsPassword = false;
                PasswordButton.Source = "HidePassword.png";
            }
            else
            {
                PasswordEntry.IsPassword = true;
                PasswordButton.Source = "ShowPassword.png";
            }
        }

        private void PasswordEntry_Focused(object sender, FocusEventArgs e)
        {
            PasswordFrame.BorderColor = Color.FromHex("#7BE495");
        }

        private void PasswordEntry_UnFocused(object sender, FocusEventArgs e)
        {
            PasswordFrame.BorderColor = Color.LightGray;
        }

        private void PasswordText_Changed(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length >= 1)
            {
                PasswordValid = true;
            }
            else
            {
                PasswordValid = false;
            }
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IKeyboardHelper>().HideKeyboard();
            if (UserNameValid && PasswordValid)
            {

                if (CrossConnectivity.Current.IsConnected)
                {
                    _loginServices = new LoginServices();

                    LoginModel model = new LoginModel()
                    {
                        UserName = vm.LoginViewModel.UserName,
                        Password = vm.LoginViewModel.Password,
                    };

                    var result = new ServiceResult<LoginModel>();
                    //Correct Exception
                    try
                    {
                        result = await _loginServices.Login(model);
                        switch (result.Status)
                        {
                            case Common.Services.MobileResultStatus.InvalidToken:
                                break;
                            case Common.Services.MobileResultStatus.Ok:
                                var item = InitializeDB.Database.GetUsersInfoListAsync();
                                if (item.Count > 0)
                                {
                                    item[0].UserId = result.Data.UserId;
                                    item[0].CustomerId = result.Data.CustomerId;
                                    item[0].UserName = result.Data.UserName;
                                    item[0].Password = result.Data.Password;
                                    item[0].Token = result.Token;
                                    item[0].UserType = result.Data.LoginUserType;
                                    item[0].ProfilePhoto = result.Data.ProfilePhoto;
                                    item[0].ServiceCenterId = result.Data.ServiceCenterId;
                                    item[0].ServiceCenterName = result.Data.ServiceCenterName;
                                    InitializeDB.Database.SaveUserInfoAsync(item[0]);
                                    App.LoginType = LoginStatus.JustLogin;
                                    App.Current.MainPage = new TabPageView();
                                }
                                else
                                {
                                    var userInfo = new UserInfoDb()
                                    {
                                        UserId = result.Data.UserId,
                                        UserName = result.Data.UserName,
                                        CustomerId = result.Data.CustomerId,
                                        Password = result.Data.Password,
                                        Token = result.Token,
                                        UserType = result.Data.LoginUserType,
                                        ProfilePhoto = result.Data.ProfilePhoto,
                                        ServiceCenterId = result.Data.ServiceCenterId,
                                        ServiceCenterName = result.Data.ServiceCenterName,
                                    };
                                    var taskresult = InitializeDB.Database.SaveUserInfoAsync(userInfo);
                                    App.Current.MainPage = new TabPageView();
                                }
                                break;
                            case Common.Services.MobileResultStatus.ParameterError:
                                break;
                            case Common.Services.MobileResultStatus.DatabaseConnectionError:
                                break;
                            case Common.Services.MobileResultStatus.ProcessError:
                                DependencyService.Get<Toast>().Show(result.Message);
                                break;
                            case Common.Services.MobileResultStatus.UnhandledException:
                                await DisplayAlert(App.SystemErrorTitle, App.SystemErrorMessage, App.SystemErrorButtonText);
                                break;
                            default:
                                break;

                        }
                    }
                    catch (Exception ex)
                    {

                        await DisplayAlert(App.ConnectionTimeoutTitle, App.ConnectionTimeoutMessage, App.ConnectionTimeoutButtonText);
                    }

                }
                else
                {
                    await DisplayAlert(App.NetworkConnectionTitle, App.NetworkConnectionMessage, App.NetworkConnectionButtonText);
                }

            }
            else
            {
                if (!UserNameValid)
                {
                    DependencyService.Get<Toast>().Show("User Name is not valid...");
                }
                else if (!PasswordValid)
                {
                    DependencyService.Get<Toast>().Show("Password is not valid...");
                }
            }
        }

        private void SignUpButton_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IKeyboardHelper>().HideKeyboard();
            SignupVm model = new SignupVm();
            this.Navigation.PushModalAsync(new SignUpView(model));
        }
    }
}