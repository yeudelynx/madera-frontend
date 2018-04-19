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
	public partial class SelectDevis : ContentPage
	{
		public SelectDevis ()
		{
			InitializeComponent ();
		}

        public void select_devis(object sender, EventArgs e)
        {
            var mainpages = new ViewDevisPlan();
            Navigation.PushAsync(mainpages);
            NavigationPage.SetHasNavigationBar(mainpages, false);

        }
    }
}