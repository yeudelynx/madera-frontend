using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace madera.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelectUser : ContentPage
	{
		public SelectUser ()
		{
			InitializeComponent ();
		}

        public void select_user(object sender, EventArgs e)
        {
            var select_devis = new SelectDevis();
            Navigation.PushAsync(select_devis);
            NavigationPage.SetHasNavigationBar(select_devis, false);

        }
    }
}