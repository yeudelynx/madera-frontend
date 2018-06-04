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
	public partial class GestClient : ContentPage
	{
        public int iduser;

        public GestClient()
		{
			InitializeComponent ();
        }

        public GestClient(int iduser)
        {
            this.iduser = iduser;
        }


        public void create_client(object sender, EventArgs e)
        {
            var page_create_client = new CreateClient() { iduser = iduser };
            Navigation.PushAsync(page_create_client);
            NavigationPage.SetHasNavigationBar(page_create_client, false);

        }

        public void choice_client(object sender, EventArgs e)
        {
            var page_select_client = new SelectClient() { iduser = iduser };
            Navigation.PushAsync(page_select_client);
            NavigationPage.SetHasNavigationBar(page_select_client, false);

        }
    }
}