using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using madera.Helpers;


namespace madera.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelectUser : ContentPage
	{
        public int iduser;
        public SelectUser ()
		{
			InitializeComponent ();

            LocalDatabase db = new LocalDatabase();
            db.tableUser.Select(x => x).ToList();

            foreach (var user in db.tableUser)
            {
                Xamarin.Forms.Button MyControl2 = new Xamarin.Forms.Button();
                MyControl2.Text = "Utilisateur" + user.id;
                /*MyControl1.TextColor = "White";
                MyControl1.FontAttributes = "Bold";
                MyControl1.FontSize = "Large";
                MyControl1.HorizontalOptions = "FillAndExpand";
                MyControl1.BackgroundColor = "#BF043055";*/
                MyControl2.Clicked += (object sender, EventArgs e) => {
                    select_user(sender, e, user.id);
                };

                btn_users.Children.Add(MyControl2);
            }
        }

        public SelectUser(int iduser)
        {
            this.iduser = iduser;
        }

        public void select_user(object sender, EventArgs e, int id)
        {
            int idclient = id;
            var select_devis = new SelectDevis() { iduser = iduser, idclient = id };
            Navigation.PushAsync(select_devis);
            NavigationPage.SetHasNavigationBar(select_devis, false);

        }
    }
}