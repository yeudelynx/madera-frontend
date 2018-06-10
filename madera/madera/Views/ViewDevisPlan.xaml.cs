using madera.Helpers;
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
    public partial class ViewDevisPlan : TabbedPage
    {
        public int idclient;
        public int iddevis;
        public int idplan;
        public int iduser;

        public Picker pickerGamme = new Picker();
        public Picker pickerCategorie = new Picker();
        public Picker pickerModule = new Picker();


        public string idBordelCLicke;

        public ViewDevisPlan(int idclient, int iddevis, int idplan, int iduser)
        {
            this.idclient = idclient;
            this.iddevis = iddevis;
            this.idplan = idplan;
            this.iduser = iduser;

        }
        
        public ViewDevisPlan()
        {
            InitializeComponent();

            LocalDatabase db = new LocalDatabase();
            var listeGamme = db.tableGamme.ToList();
            var listeCategorie = db.tableCategorie.ToList();
            var listeModule = db.tableModule.ToList();
            

            //panneau de gauche
            //picker Gamme
            pickerGamme.Title ="Gamme";
            pickerGamme.ItemDisplayBinding = new Binding("lib_gamme");
            pickerGamme.ItemsSource = listeGamme;
            panneauSelection.Children.Add(pickerGamme);
            
            //picker Categorie
            pickerCategorie.Title = "Catégorie";
            pickerCategorie.ItemDisplayBinding = new Binding("lib_categorie");
            pickerCategorie.ItemsSource = listeCategorie;
            panneauSelection.Children.Add(pickerCategorie);

            //picker Module
            pickerModule.Title = "Module";
            pickerModule.ItemDisplayBinding = new Binding("lib_module");
            pickerModule.ItemsSource = listeModule;
            panneauSelection.Children.Add(pickerModule);

           // dynamic plan = new Newtonsoft.Json.Linq.JObject();
                
           // plan = "[{ "x": "0", "y": "0"}, { "x": "5", "y": "0"}, { "x": "0", "y": "5"}, { "x": "5", "y": "5"}]";



            // panneau de droite

            /* algo placement points : 


        tracé mur du bas       
        a = x;
        while (plan.0.x <= plan.1.x) 
            {
                fait un bouton avec coordonnée (a, y)
                a++
            }

        tracé mur de gauche 
        a = y;
        while (plan.0.x <= plan.3.x)
            {
                fait un bouton avec coordonnée (x, a)
                a++
            }

             
            
     
              */

            while (true)
            {

            }

            Button element = new Button();
            element.HeightRequest = 50;
            element.WidthRequest = 10;
            element.Text = "Clickmoaaa";
            element.Clicked += new EventHandler(affectation);

            panneauPlan.Children.Add(element);






        }

        // Manipulation du plan (+ maj devis)

        void affectation(object sender, EventArgs e)
        {
            var button = (Button)sender;
            Console.WriteLine("yolo");

            string selectedValue ="";
            
            // servira à recupérer les coordonnées dans l'espace => x y z
            try
            {
                selectedValue = pickerCategorie.Items[pickerCategorie.SelectedIndex];
            }
            catch (Exception)
            {
                //DisplayAlert("Erreur", "Veuillez selectionner une gamme", "Ok");
            }
            // TODO : remplacer par un switch
            if (selectedValue == "Aucune")
            {
                button.BackgroundColor = Color.Default;
                // j'enleve le machin du devis where 
                //listeDevis.Children.Add(selectedValue);

            }
            if (selectedValue == "Mur")
            {
                Console.WriteLine(selectedValue);
                button.BackgroundColor = Color.Blue;
                Xamarin.Forms.Label label_maron = new Label();
                label_maron.Text = selectedValue;
                listeDevis.Children.Add(label_maron);

            }
            if (selectedValue == "Porte")
            {
                Console.WriteLine(selectedValue);
                button.BackgroundColor = Color.Brown;
                Xamarin.Forms.Label label_maron = new Label();
                label_maron.Text = selectedValue;
                listeDevis.Children.Add(label_maron);

            }
            if (selectedValue == "Fenêtre")
            {
                Console.WriteLine(selectedValue);
                button.BackgroundColor = Color.Black;
                Xamarin.Forms.Label label_green = new Label();
                label_green.Text = selectedValue;
                listeDevis.Children.Add(label_green);
            }


        }

        public void view_devis_plan(object sender, EventArgs e)
        {
            var MainPages = new ViewDevisPlan();
            Navigation.PushAsync(MainPages);
            NavigationPage.SetHasNavigationBar(MainPages, false);

        }






    }


}