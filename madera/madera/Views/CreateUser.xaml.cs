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
	public partial class CreateUser : ContentPage
	{
		public CreateUser ()
		{
			InitializeComponent ();
		}

        public void validate_user(object sender, EventArgs e)
        {
            var page_choice_plan = new ChoicePlan();
            Navigation.PushAsync(page_choice_plan);
            NavigationPage.SetHasNavigationBar(page_choice_plan, false);

        }
    }
}