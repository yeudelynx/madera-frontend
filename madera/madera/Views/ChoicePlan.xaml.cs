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
using SQLite;
using System.IO;

namespace madera.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChoicePlan : ContentPage
	{
		public ChoicePlan ()
		{
			InitializeComponent ();

            LocalDatabase db = new LocalDatabase();
            db.tableSol.Select(x => x).ToList();

            foreach (var sol in db.tableSol)
            {
                //Console.WriteLine(" ++++++ USER LOGIN" + user.id + " : " + user.login);
                Xamarin.Forms.Button MyControl1 = new Xamarin.Forms.Button();
                MyControl1.Text = "Sol" + sol.id;
                MyControl1.Clicked += choice_plan;

                btn_sols.Children.Add(MyControl1);
            }
        }

        public void choice_plan(object sender, EventArgs e)
        {
            var MainPages = new ViewDevisPlan();
            Navigation.PushAsync(MainPages);
            NavigationPage.SetHasNavigationBar(MainPages, false);

        }
    }
}