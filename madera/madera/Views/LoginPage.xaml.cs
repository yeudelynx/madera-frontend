using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using madera.Helpers;
using madera.Models;
using System.Linq;
using System.Diagnostics;

namespace madera.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();

        }

        public void SignInProcedure(object sender, EventArgs e)
        {
            
            LocalDatabase db = new LocalDatabase();

            int iduser = 0;
            var tableUser = db.tableUser.ToList();
            
            foreach(var user in tableUser)
            {
                if (user.login.ToString() == Entry_Username.Text && user.password.ToString() == Entry_Password.Text)
                {
                    iduser = user.id;
                }
            }

            if (iduser != 0)
            {
                var ViewDevisplan = new ViewDevisPlan() {iduser = iduser};
                Navigation.PushAsync(ViewDevisplan);
                NavigationPage.SetHasNavigationBar(ViewDevisplan, false);
            }
            else
            {
                var ViewDevisplan = new ViewDevisPlan();
                Navigation.PushAsync(ViewDevisplan);
                NavigationPage.SetHasNavigationBar(ViewDevisplan, false);
            }
            
        }
        
    }
}