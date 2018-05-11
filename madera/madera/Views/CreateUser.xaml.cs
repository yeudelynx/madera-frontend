using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Android.Widget;
using madera.Helpers;
using madera.Models;
using Newtonsoft.Json;
using SQLite;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace madera.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateUser : ContentPage
	{
		public CreateUser ()
		{
			InitializeComponent ();
		}

        public void validate_user(object sender, EventArgs e)
        {
            if (entry_nom.Text != "" && entry_prenom.Text != "" && entry_mail.Text != "" && entry_telephone.Text != "" && entry_adresse.Text != "") {

                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "madera.db3");
                var db = new SQLiteConnection(dbPath);
                String datetimestr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Client client = new Client {
                    nom = (String)entry_nom.Text,
                    prenom = (String)entry_prenom.Text,
                    mail = (String)entry_mail.Text,
                    tel = (String)entry_telephone.Text,
                    adresse = (String)entry_adresse.Text,
                    updated_at = datetimestr
                };

                db.Insert(client);

                var page_choice_plan = new ChoicePlan();
                Navigation.PushAsync(page_choice_plan);
                NavigationPage.SetHasNavigationBar(page_choice_plan, false);

            }

        }
    }
}