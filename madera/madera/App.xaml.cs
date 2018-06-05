using System;
using madera.Helpers;
using madera.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace madera
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            MainPage = new NavigationPage(new madera.Views.LoginPage());
        }

		protected override void OnStart ()
		{

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

        protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
