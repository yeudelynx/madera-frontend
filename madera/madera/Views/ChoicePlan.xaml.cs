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
        public int iduser;
        public int idclient;
        public ChoicePlan ()
		{
			InitializeComponent ();

            LocalDatabase db = new LocalDatabase();
            db.tableSol.Select(x => x).ToList();

            foreach (var sol in db.tableSol)
            {
                Xamarin.Forms.Button MyControl1 = new Xamarin.Forms.Button();
                MyControl1.Text = "Sol" + sol.id;
                /*MyControl1.TextColor = "White";
                MyControl1.FontAttributes = "Bold";
                MyControl1.FontSize = "Large";
                MyControl1.HorizontalOptions = "FillAndExpand";
                MyControl1.BackgroundColor = "#BF043055";*/
                //MyControl1.Clicked += choice_plan;
                MyControl1.Clicked += (object sender, EventArgs e) => {
                    choice_plan(sender, e, sol.id);
                };

                btn_sols.Children.Add(MyControl1);
            }
        }

        public ChoicePlan(int iduser, int idclient)
        {
            this.iduser = iduser;
            this.idclient = idclient;
        }

            public void choice_plan(object sender, EventArgs e, int id)
        {
            int idsol = id;
            //var MainPages = new ViewDevisPlan() { iduser = iduser, idclient = idclient, idplan = id };
            var MainPages = new ViewDevisPlan(iduser, idclient, id); 
            Navigation.PushAsync(MainPages);
            NavigationPage.SetHasNavigationBar(MainPages, false);

        }
    }
}