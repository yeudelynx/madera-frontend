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
	public partial class GestUser : ContentPage
	{
		public GestUser ()
		{
			InitializeComponent ();
		}

        public void create_user(object sender, EventArgs e)
        {
            var page_create_user = new CreateUser();
            Navigation.PushAsync(page_create_user);
            NavigationPage.SetHasNavigationBar(page_create_user, false);

        }

        public void choice_user(object sender, EventArgs e)
        {
            var page_choice_user = new SelectUser();
            Navigation.PushAsync(page_choice_user);
            NavigationPage.SetHasNavigationBar(page_choice_user, false);

        }
    }
}