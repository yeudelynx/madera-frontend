using madera.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using madera.Models;
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


        public string selectedValue;
        public string selectedCategorie;
        public string selectedModule;
        public string selectedGamme;

        public string infosGamme;
        public string infosCategorie;
        public string infosModule;


        public string labelGamme;
        public string labelCategorie;
        public string labelModule;


        string[] listeBoutton = new string[] {};
        public List<Label> listeLabel = new List<Label>();




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
            pickerGamme.SelectedIndexChanged += new EventHandler(detailModule);
            panneauSelection.Children.Add(pickerGamme);
            
            //picker Categorie
            pickerCategorie.Title = "Catégorie";
            pickerCategorie.ItemDisplayBinding = new Binding("lib_categorie");
            pickerCategorie.ItemsSource = listeCategorie;
            pickerCategorie.SelectedIndexChanged += new EventHandler(detailModule);
            panneauSelection.Children.Add(pickerCategorie);

            //picker Module
            pickerModule.Title = "Module";
            pickerModule.ItemDisplayBinding = new Binding("lib_module");
            pickerModule.ItemsSource = listeModule;
            pickerModule.SelectedIndexChanged += new EventHandler(detailModule);
            panneauSelection.Children.Add(pickerModule);

            List<Point> listePoints = new List<Point>();

            var listeSols = db.tableSol.ToList();

            listePoints = JsonConvert.DeserializeObject<List<Point>>(listeSols[1].list_point_sol);

            foreach (var p in listePoints)
            {
                Debug.WriteLine("Point x : " + p.X);
                Debug.WriteLine("Point y : " + p.Y);
            }

            int x0 = 0;
            int y0 = 0;

            int x1 = 5;
            int y1 = 0;

            int x2 = 5;
            int y2 = 5;

            int x3 = 0;
            int y3 = 5;

            var gridPanneauGauche = new Grid();
            layoutPlan.Children.Add(gridPanneauGauche);
            int id = 0;
            // mur du bas 
            while (x0 < x1)
            {
                Button element = new Button();
                panneauPlan.Children.Add(element, x0, 5);
                //element.Text = x0.ToString();
                //listeBoutton.Add(element);
                element.Clicked += (object sender, EventArgs e) => {
                    affectation(sender, e, id);
                };

                element.Text = id.ToString();

                x0++;
                id++;

            }

            // mur de droite 
            while (y1-1 < y2 -1 )
            {
                Button element = new Button();
                panneauPlan.Children.Add(element, 4, y1);
                element.Clicked += (object sender, EventArgs e) => {
                    affectation(sender, e, id);
                };

                element.Text = id.ToString();
                id++;
                y1++;
                //listeBoutton.Add(element);

            }

            // mur du haut 
            while (x3 < x2 -1)
            {
                Button element = new Button();
                element.Clicked += (object sender, EventArgs e) => {
                    affectation(sender, e, id);
                };
                element.Text = id.ToString();
                id++;
                panneauPlan.Children.Add(element, x3, 0);
                x3++;
            }

            // mur de gauche 
            while (y0 < y3 )
            {
                Button element = new Button();
                element.Clicked += (object sender, EventArgs e) => {
                    affectation(sender, e, id);
                };
                element.Text = id.ToString();
                id++;
                if (y0 != 0)
                {
                    panneauPlan.Children.Add(element, 0, y0);
                }             
                y0++;
                id++;
                //listeBoutton.Add(element);

            }


        }

        // Manipulation du plan
        void affectation(object sender, EventArgs e, int id)
            //onclick sur le bouton
        {
            var button = (Button)sender;
            button.Id

            selectedValue = "";
            selectedGamme = "";
            selectedCategorie = "";
            selectedModule = "";



            // servira à recupérer les coordonnées dans l'espace => x y z
            try
            {
                selectedValue = pickerCategorie.Items[pickerCategorie.SelectedIndex];
                selectedModule = pickerModule.Items[pickerModule.SelectedIndex];

            }
            catch (Exception)
            {
                //DisplayAlert("Erreur", "Veuillez selectionner une gamme", "Ok");
            }
            if (selectedValue == "Aucune")
            {
                button.BackgroundColor = Color.Default;

            }
            if (selectedValue == "Mur")
            {
                button.BackgroundColor = Color.Blue;
                Xamarin.Forms.Label label_maron = new Label();
                listeDevis.Children.Add(label_maron);

            }
            if (selectedValue == "Porte")
            {
                button.BackgroundColor = Color.Brown;
                Xamarin.Forms.Label label_maron = new Label();
                listeDevis.Children.Add(label_maron);

            }
            if (selectedValue == "Fenêtre")
            {
                button.BackgroundColor = Color.Black;
                Xamarin.Forms.Label label_green = new Label();
                listeDevis.Children.Add(label_green);
            }
            
            button.Text = selectedModule;

            /*
                à faire : 
                1) afficher les information des items selectionnés en bas des trois pickers uniquement si les 3 pickers sont selectionnés
                2) Debugger l'affichage de la génération des devis
                3) regarder sur la DB comment sont envoyés les points pour faire des plans en fonction des maison


             
             */

            foreach (var boutton in listeBoutton)
            {
                string nomBouton = "";
                nomBouton = boutton.Text;
                if (string.IsNullOrEmpty(nomBouton))
                {

                }
                else
                {
                    Xamarin.Forms.Label labelDevis = new Label();
                    labelDevis.Text = boutton.Text;
                    listeLabel.Add(labelDevis);
                }
            }
            

            /*foreach (Button boutton in listeBoutton)
            {
                string nomBouton = "";
                nomBouton = boutton.Text;
                if (string.IsNullOrEmpty(nomBouton))
                {

                } else
                {
                    Xamarin.Forms.Label labelDevis = new Label();
                    labelDevis.Text = boutton.Text;
                    listeLabel.Add(labelDevis);
                }

            }*/
        }

        // maj ihm devis
        void actualisation(object sender, EventArgs e)
        {
            //listeDevis.Child.RemoveAll();
            //listeDevis.Children.RemoveAt();

            //< StackLayout x: Name = "listeDevis" >

            listeDevis.Children.Clear();

            Console.WriteLine(listeLabel);

            foreach (var label in listeLabel)
            {
                string text = label.Text;
                if (string.IsNullOrEmpty(text))
                {
                } else { 
                    Xamarin.Forms.Label labelDevis = new Label();
                    labelDevis.Text = "label choisi : " + label.Text;
                    listeDevis.Children.Add(labelDevis);
                }
            }
        }

        // afiche les details d'un module lorsque les trois pickers sont selectionnés
        void detailModule(object sender, EventArgs e)
        {
            try
            {
                selectedCategorie = pickerCategorie.Items[pickerCategorie.SelectedIndex];
                selectedModule = pickerModule.Items[pickerModule.SelectedIndex];
                selectedGamme = pickerGamme.Items[pickerGamme.SelectedIndex];

            }
            catch (Exception)
            {

                
            }

            if (selectedGamme != null)
            {
                labelGammeChoisie.Text = selectedGamme;
            }


            if (selectedCategorie != null)
            {
                labelCategorieChoisie.Text = selectedCategorie;
            }
            // !!! attention, ecrire les points dans l'ordre indiqué de la création des murs

            if (selectedModule != null)
            {
                // selected module . text = ce que j'affiche / selected module = la value de mon picker
                labelModuleChoisi.Text = selectedModule;


                LocalDatabase db = new LocalDatabase();
                var listeModule = db.tableModule.ToList();

                /*var query = from m in db.tableModule
                            where m.lib_module = selectedModule
                            select m;*/


                // string detailsModule = listeModule.Where(n = selectedModule);

            }

        }


        

        public void view_devis_plan(object sender, EventArgs e)
        {
            var MainPages = new ViewDevisPlan();
            Navigation.PushAsync(MainPages);
            NavigationPage.SetHasNavigationBar(MainPages, false);

        }

       /* public ViewDevisPlan(int iduser, int idclient, int iddevis, int idplan)
        {
            
            InitializeComponent();
            this.iduser = -1;
            this.idclient = idclient;
            this.iddevis = iddevis;
            this.idplan = idplan;
            this.afficherDevis();

        }*/

            public ViewDevisPlan(int iduser, int idclient, int iddevis)
        {

            InitializeComponent();
            this.iduser = iduser;
            this.idclient = idclient;
            this.iddevis = iddevis;

        }

    }
      
}

