using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using madera.Helpers;


namespace madera.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelectClient : ContentPage
	{
        public int iduser;
        public SelectClient ()
		{
			InitializeComponent ();

            LocalDatabase db = new LocalDatabase();
            db.tableClient.Select(x => x).ToList();

            foreach (var clients in db.tableClient)
            {
                Xamarin.Forms.Button MyControl2 = new Xamarin.Forms.Button();
                MyControl2.Text = "Client" + clients.prenom +" "+ clients.nom;
                /*MyControl1.TextColor = "White";
                MyControl1.FontAttributes = "Bold";
                MyControl1.FontSize = "Large";
                MyControl1.HorizontalOptions = "FillAndExpand";
                MyControl1.BackgroundColor = "#BF043055";*/
                MyControl2.Clicked += (object sender, EventArgs e) => {
                    select_client(sender, e, clients.id);
                };

                btn_clients.Children.Add(MyControl2);
            }
        }

        public SelectClient(int iduser)
        {
            this.iduser = iduser;
        }

        public void select_client(object sender, EventArgs e, int id)
        {
            int idclient = id;
            var select_devis = new SelectDevis() { iduser = iduser, idclient = id };
            Navigation.PushAsync(select_devis);
            NavigationPage.SetHasNavigationBar(select_devis, false);

        }
    }
}