using madera.Helpers;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using madera.Models;
using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace madera.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewDevisPlan : TabbedPage
    {

        /*La liste des objets labels ajoutés pour pouvoir rechercher la catégorie des modules par le libellé présent dans le champ text.*/
        private List<Label> labels;
        private LocalDatabase database;
        private Dictionary<Color, string> categories;
        //Permet de récupérer le libellé de la catégorie référencé par le module(libellé de la catégorie dont la clé primaire est clé étrangère dans Module)
        private Dictionary<Module, string> listCategorie;

        //Tous les modules ajoutés dans le devis.
        private List<Module> modulesAjoutes;
        private List<Gamme> listeGamme;

        //les modules disponnibles
        private List<Module> listeModule;
        private List<Categorie> listeCategorie;
        private List<Constituer> listeConstituer;



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
            categories = new Dictionary<Color, string>();
            categories[Color.Blue] = "Mur";
            categories[Color.Brown] = "Porte";
            categories[Color.Black] = "Fenêtre";
            modulesAjoutes = new List<Module>();
            database = new LocalDatabase();
            labels = new List<Label>();
            listeGamme = database.tableGamme.ToList();
            listeCategorie = database.tableCategorie.ToList();
            listeModule = database.tableModule.ToList();
            listCategorie = new Dictionary<Module, string>();
;            fillDictionary(ref listCategorie);

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

        private void fillDictionary(ref Dictionary<Module, string> categories) {
            foreach(var moduleItem in listeModule) {
                int categorie_id = moduleItem.categorie_id;
                Categorie cat = (listeCategorie.Where(item => item.id == categorie_id).ToList().Count > 0 ? listeCategorie.Where(item => item.id == categorie_id).ToList()[0] : null) ;
                categories[moduleItem] = (cat == null ? "-" : cat.lib_categorie);
            }

        }

        
        /**
         * Ecouteur pemettant d'ajouter ou d'enlever un module
         * dans le plan.
         */
        void affectation(object sender, EventArgs e)
        {
            int indexModuleChoisi = -1;
            var button = (Button)sender;
            string selectedValue ="";
            
            // servira à recupérer les coordonnées dans l'espace => x y z
            try
            {
                indexModuleChoisi = pickerModule.SelectedIndex;
                int identifiant_categorie = listeModule[indexModuleChoisi].categorie_id;
                Categorie catModule = listeCategorie.Where(item => item.id == identifiant_categorie).ToList()[0];
                selectedValue = catModule.lib_categorie;
             
            }
            catch (Exception)
            {
                //DisplayAlert("Erreur", "Veuillez selectionner une gamme", "Ok");
            }
            // TODO : remplacer par un switch
            StringBuilder builder;
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
                        //récupérer dans la base de données l'identifiant de catégorie
                        //element.categorie_id = listeCategorie.Find(categorie => categorie.lib_categorie == labelItem.Text).id;

                        labels.Remove(labelItem);
                        listeDevis.Children.Remove(labelItem);
                        button.BackgroundColor = Color.Default;
                        modulesAjoutes.Add(listeModule[indexModuleChoisi]);
                    }
                    else if(button.BackgroundColor != Color.Default && button.BackgroundColor != Color.Blue)
                    {

                        this.remplacerModule(ref button, listeModule[indexModuleChoisi], Color.Blue);
                    }
                    else
                    {
                        //c'est la couleur par défaut. Aucun module à cette emplacement.
                        button.BackgroundColor = Color.Blue;
                        Xamarin.Forms.Label label_bleue = new Label();
                        
                        builder = new StringBuilder();
                        builder.Append(selectedValue).Append(" ").Append(listeModule[indexModuleChoisi].prix).Append(" ").Append("euros");
                        label_bleue.Text = builder.ToString();
                        listeDevis.Children.Add(label_bleue);
                        modulesAjoutes.Add(listeModule[indexModuleChoisi]);
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
                        modulesAjoutes.Add(listeModule[indexModuleChoisi]);
                    }
                    else if(button.BackgroundColor != Color.Default && button.BackgroundColor != Color.Brown)
                    {
                        this.remplacerModule(ref button, listeModule[indexModuleChoisi], Color.Brown);
                    }
                    else

                    {
                        button.BackgroundColor = Color.Brown;
                        Xamarin.Forms.Label label_maron = new Label();
                        builder = new StringBuilder();
                        builder.Append(selectedValue).Append(" ").Append(listeModule[indexModuleChoisi].prix).Append(" ").Append("euros");
                        label_maron.Text = builder.ToString();
                        modulesAjoutes.Add(listeModule[indexModuleChoisi]);
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
                        modulesAjoutes.Add(listeModule[indexModuleChoisi]);
                        button.BackgroundColor = Color.Default;
                    }
                    else if (button.BackgroundColor != Color.Default && button.BackgroundColor != Color.Black)
                    {
                        this.remplacerModule(ref button, listeModule[indexModuleChoisi], Color.Black);
                    }
                    else
                    {
                        button.BackgroundColor = Color.Black;
                        Xamarin.Forms.Label label_green = new Label();
                        builder = new StringBuilder();
                        builder.Append(selectedValue).Append(" ").Append(listeModule[indexModuleChoisi].prix).Append(" ").Append("euros");
                        label_green.Text = builder.ToString();
                        listeDevis.Children.Add(label_green);
                        modulesAjoutes.Add(listeModule[indexModuleChoisi]);
                        labels.Add(label_green);
                    }
                    break;
            }
        }

        /**
         * Remplace un module symbolisé par un boutton par un autre module.
         * Param : button : le module à remplacer
         * nvelleCategorie : la nouvelle categorie du nouveau module.
         */
        private void remplacerModule(ref Button button, Module nveauModule, Color nvelleCouleur)
        {
            //Chercher la catégorie du module placer.
            Color ancienneCouleur = button.BackgroundColor;
            Label labelItem = chercherCategorie(categories[button.BackgroundColor]);

            //supprimer la catégorie de la liste des labels
            labels.Remove(labelItem);
            listeDevis.Children.Remove(labelItem);

            //créer un nouveau label
            labelItem = new Label();

            //remplacer l'ancienne catégorie par la nouvel.
            StringBuilder builder = new StringBuilder();
            builder.Append(listCategorie[nveauModule]).Append(" ").Append(nveauModule.prix).Append(" ").Append("euros.");
            labelItem.Text = builder.ToString();
            button.BackgroundColor = nvelleCouleur;
            modulesAjoutes.Add(nveauModule);
            listeDevis.Children.Add(labelItem);

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
            char[] separator = new char[1];
            separator[0] = ' ';
            while (labels[index].Text.Split(separator)[0] != categorie && index < labels.Count)
            {
                index++;
            }

            return labels[index];
        }

        /*
         * Ecouteur permettant la sauvegarde des modules associés dans la base de données. 
         */
        public void sauvegarder(object sender, EventArgs e)
        {

            const int ETAGE_DEFAUT = 0;
            //sauvegarder la liste des modules.
            Constituer constitutionDevis = null;
            Date date = new Date();
            foreach (var module in modulesAjoutes) {
                constitutionDevis = new Constituer();
                constitutionDevis.prix_module = module.prix;
                constitutionDevis.module_id = module.id;
                constitutionDevis.etage_plan = ETAGE_DEFAUT;
                constitutionDevis.created_at = DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss.ffffff", CultureInfo.InvariantCulture);
                constitutionDevis.updated_at = DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss.ffffff", CultureInfo.InvariantCulture);
                constitutionDevis.devis_id = this.iddevis;
                database.db.Insert(constitutionDevis);
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