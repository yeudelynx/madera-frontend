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
	public partial class CreateClient : ContentPage
	{
        public int iduser;
        public CreateClient()
		{
			InitializeComponent ();
		}
        public CreateClient(int iduser)
        {
            this.iduser = iduser;
        }

        public void validate_client(object sender, EventArgs e)
        {
            //inscription client
            if (entry_nom.Text != "" && entry_prenom.Text != "" && entry_mail.Text != "" && entry_telephone.Text != "" && entry_adresse.Text != "") {

                LocalDatabase db = new LocalDatabase();

                String datetimestr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Client client = new Client {
                    nom = (String)entry_nom.Text,
                    prenom = (String)entry_prenom.Text,
                    mail = (String)entry_mail.Text,
                    tel = (String)entry_telephone.Text,
                    adresse = (String)entry_adresse.Text,
                    updated_at = datetimestr
                };

                db.db.Insert(client);

                int idclient = -1;
                var tableClient = db.tableClient.ToList();
                foreach (var clients in tableClient)
                {
                    if (clients.nom == entry_nom.Text && clients.prenom == entry_prenom.Text)
                    {
                        idclient = clients.id;
                    }
                }

                if (idclient != -1)
                {
                    var page_choice_plan = new ChoicePlan() { iduser = iduser, idclient = idclient };
                    Navigation.PushAsync(page_choice_plan);
                    NavigationPage.SetHasNavigationBar(page_choice_plan, false);
                }

            }

        }
        
    }
}