using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using madera.Helpers;
using madera.Models;
using System.Linq;

namespace madera.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();

            try
            {
                SyncDatas syncDatas = new SyncDatas();
                syncDatas.Process();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error : " + ex);
            }

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
                var page_gest_client = new GestClient() {iduser = iduser};
                Navigation.PushAsync(page_gest_client);
                NavigationPage.SetHasNavigationBar(page_gest_client, false);
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