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
        /*La liste des objets labels ajoutés pour pouvoir rechercher la catégorie des modules par le libellé présent dans le champ text.*/
        private List<Label> labels;
        public int idclient;
        public int iddevis;
        public int idplan;
        public int iduser;

        public Picker pickerGamme = new Picker();
        public Picker pickerCategorie = new Picker();
        public Picker pickerModule = new Picker();


        public string idBordelCLicke;

        public ViewDevisPlan(int idclient, int iddevis, int iduser)
        {
            this.idclient = idclient;
            this.iddevis = iddevis;
            this.iduser = iduser;

        }
        
        public ViewDevisPlan()
        {
            InitializeComponent();
            
            LocalDatabase db = new LocalDatabase();
            labels = new List<Label>();
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


            // table sol...
            //var listePointsSol = db.tableSol.ToList();

            // {x1 : '9', x2 : '9', x3 : '9', x4 : '9'}

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

            }*/

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

      

            // mur du bas 
            while (x0 < x1)
            {
                Button element = new Button();
                element.Clicked += new EventHandler(affectation);
                panneauPlan.Children.Add(element, x0, 5);
                x0++;
            }

            // mur de droite 
            while (y1 < y2)
            {
                Button element = new Button();
                element.Clicked += new EventHandler(affectation);
                panneauPlan.Children.Add(element, 4, y1);
                y1++;
            }

            // mur du haut 
            while (x3 < x2)
            {
                Button element = new Button();
                element.Clicked += new EventHandler(affectation);
                panneauPlan.Children.Add(element, x3, 0);
                x3++;
            }

            // mur de gauche 
            while (y0 < y3)
            {
                Button element = new Button();
                element.Clicked += new EventHandler(affectation);
                panneauPlan.Children.Add(element, 0, y0);
                y0++;
            }


            




        }

        
        /**
         * Ecouteur pemettant d'ajouter ou d'enlever un module
         * dans le plan.
         */
        void affectation(object sender, EventArgs e)
        {
            var button = (Button)sender;
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
            switch (selectedValue)
            {
                case "Aucune":

                    button.BackgroundColor = Color.Default;
                    // j'enleve le machin du devis where 
                    //listeDevis.Children.Add(selectedValue);
                    break;
                case "Mur":

                    if (button.BackgroundColor == Color.Blue)
                    {
                        //le boutton a été cliqué
                        Label labelItem = chercherCategorie("Mur");
                        labels.Remove(labelItem);
                        listeDevis.Children.Remove(labelItem);
                        button.BackgroundColor = Color.Default;
                    }
                    else if(button.BackgroundColor != Color.Default && button.BackgroundColor != Color.Blue)
                    {
                        //Un autre module a déjà été placé. Ne rien faire.
                    }
                    else
                    {
                        button.BackgroundColor = Color.Blue;
                        Xamarin.Forms.Label label_bleue = new Label();
                        label_bleue.Text = selectedValue;
                        listeDevis.Children.Add(label_bleue);
                        labels.Add(label_bleue);
                    }

                    break;
                case "Porte":
                    if (button.BackgroundColor == Color.Brown)
                    {
                        //le boutton a été cliqué
                        Label labelItem = chercherCategorie("Porte");
                        labels.Remove(labelItem);
                        listeDevis.Children.Remove(labelItem);
                        button.BackgroundColor = Color.Default;
                    }
                    else if(button.BackgroundColor != Color.Default && button.BackgroundColor != Color.Brown)
                    {

                    }
                    else

                    {
                        button.BackgroundColor = Color.Brown;
                        Xamarin.Forms.Label label_maron = new Label();
                        label_maron.Text = selectedValue;
                        listeDevis.Children.Add(label_maron);
                        labels.Add(label_maron);
                    }
                    break;
                case "Fenêtre":
                    if (button.BackgroundColor == Color.Black)
                    {
                        //le boutton a été cliqué
                        Label labelItem = chercherCategorie("Fenêtre");
                        labels.Remove(labelItem);
                        listeDevis.Children.Remove(labelItem);
                        button.BackgroundColor = Color.Default;
                    }
                    else if (button.BackgroundColor != Color.Default && button.BackgroundColor != Color.Black)
                    { 
                    }
                    else
                    {
                        button.BackgroundColor = Color.Black;
                        Xamarin.Forms.Label label_green = new Label();
                        label_green.Text = selectedValue;
                        listeDevis.Children.Add(label_green);
                        labels.Add(label_green);
                    }
                    break;
            }
        }

        /*
         * Cherche parmi la liste des label le premier ayant une valeur Text
         * identique à celle passé en paramètre
         * Param : categorie : le libellé de la catégorie du module (Porte, Fenêtre, Mur)
         * Return : une référence sur un Label dont le champs texte correspond à la catégorie passé en paramètre
         */
        private Label chercherCategorie(string categorie) {
            int index = 0;
            if (labels == null) {
                return null;
            }

            while (labels[index].Text != categorie)
            {
                index++;
            }

            return labels[index];
        }



        public void view_devis_plan(object sender, EventArgs e)
        {
            var MainPages = new ViewDevisPlan();
            Navigation.PushAsync(MainPages);
            NavigationPage.SetHasNavigationBar(MainPages, false);

        }






    }


}