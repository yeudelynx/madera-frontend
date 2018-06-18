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
	public partial class SelectDevis : ContentPage
	{
        public int iduser;
        public int idclient;
        public SelectDevis ()
		{
			InitializeComponent ();

            LocalDatabase db = new LocalDatabase();
            db.tableDevis.Select(x => x).ToList();

            foreach (var devis in db.tableDevis)
            {
                Xamarin.Forms.Button MyControl1 = new Xamarin.Forms.Button();
                MyControl1.Text = "Devis" + devis.id;
               /* MyControl1.TextColor = "White";
                MyControl1.FontAttributes = "Bold";
                MyControl1.FontSize = "Large";
                MyControl1.HorizontalOptions = "FillAndExpand";
                MyControl1.BackgroundColor = "#BF043055";*/
                MyControl1.Clicked += (object sender, EventArgs e) => {
                    select_devis(sender, e, devis.id);
                };

                btn_devis.Children.Add(MyControl1);
            }
        }

        public SelectDevis(int iduser, int idclient)
        {
            this.iduser = iduser;
            this.idclient = idclient;
        }

        public void select_devis(object sender, EventArgs e, int id)
        {
            int iddevis = id;
            //var mainpages = new ViewDevisPlan() { iduser = iduser, idclient = idclient, iddevis = id };
            var mainpages = new ViewDevisPlan(iduser, idclient, id) ;
            Navigation.PushAsync(mainpages);
            NavigationPage.SetHasNavigationBar(mainpages, false);

        }
    }
}