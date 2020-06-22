using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aircharts.Helpers;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Xamarin.Forms;

[assembly: Dependency(typeof(Aircharts.Droid.Dependencies.Auth))]
namespace Aircharts.Droid.Dependencies
{
    class Auth : IAuth
    {
        public async Task<bool> AuthenticateUser(string email, string password)
        {

            try
            {
                await Firebase.Auth.FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);

                return true;
            }
            catch (FirebaseAuthWeakPasswordException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (FirebaseAuthInvalidCredentialsException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (FirebaseAuthInvalidUserException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception)
            {
                throw new Exception("An unknown error occurred, please try again.");
            }


        }

        public string GetCurrentUserId()
        {

            return Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid;

        }

        public bool IsAuthenticated()
        {

            return Firebase.Auth.FirebaseAuth.Instance.CurrentUser != null;

        }
        public bool SignOut()
        {
            try
            {
                Firebase.Auth.FirebaseAuth.Instance.SignOut();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> RegisterUser(string name, string email, string password)
        {
            try
            {
                await Firebase.Auth.FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                var profileUpdates = new Firebase.Auth.UserProfileChangeRequest.Builder();
                profileUpdates.SetDisplayName(name);
                var build = profileUpdates.Build();

                var user = Firebase.Auth.FirebaseAuth.Instance.CurrentUser;
                await user.UpdateProfileAsync(build);

                return true;
            }
            catch (FirebaseAuthWeakPasswordException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (FirebaseAuthInvalidCredentialsException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (FirebaseAuthUserCollisionException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception)
            {
                throw new Exception("An unknown error occurred, please try again.");
            }

        }

        public async Task<bool> ResetPassword(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {

                await Firebase.Auth.FirebaseAuth.Instance.SendPasswordResetEmailAsync(email);
            }
            return true;
        }
    }
}