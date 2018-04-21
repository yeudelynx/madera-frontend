using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Android.Widget;
using madera.Helpers;
using madera.Models;
using Newtonsoft.Json;



namespace madera.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();

            //Call task Refresher 

            //Show sync loading

            //Wait Refresher response

            //If Refresher response == TRUE, show sync OK

            //If Refresher response == FALSE, show sync KO...

            //Hide sync loading
            try
            {
                SyncDatas syncDatas = new SyncDatas();
                syncDatas.Process();
            }
            catch (Exception ex)
            {
                //Show popup warning here !!!!!
                // + "RollBack" ?!

                //Toast.MakeText(this, ex.StackTrace, ToastLength.Long).Show();
                Console.WriteLine("error : " + ex);
            }
        }

        public void SignInProcedure(object sender, EventArgs e)
        {

            LocalDatabase db = new LocalDatabase();

            User user = db.tableUser.Where(u => u.login.Equals(Entry_Username.Text) && u.password.Equals(Entry_Password.Text)).FirstOrDefault();
            if(user != null){
                var page_gest_user = new GestUser();
                Navigation.PushAsync(page_gest_user);
                NavigationPage.SetHasNavigationBar(page_gest_user, false);
            }
            else
            {
                var page_login_page = new LoginPage();
                Navigation.PushAsync(page_login_page);
                NavigationPage.SetHasNavigationBar(page_login_page, false);
            }

        }
    }
}