using madera.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using madera.Models;
using madera.Helpers;
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

        // Manipulation du plan (+ maj devis)

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
            if (selectedValue == "Aucune")
            {
                button.BackgroundColor = Color.Default;
                // j'enleve le machin du devis where 
                //listeDevis.Children.Add(selectedValue);

            }
            if (selectedValue == "Mur")
            {
                button.BackgroundColor = Color.Blue;
                Xamarin.Forms.Label label_maron = new Label();
                label_maron.Text = selectedValue;
                listeDevis.Children.Add(label_maron);

            }
            if (selectedValue == "Porte")
            {
                button.BackgroundColor = Color.Brown;
                Xamarin.Forms.Label label_maron = new Label();
                label_maron.Text = selectedValue;
                listeDevis.Children.Add(label_maron);

            }
            if (selectedValue == "Fenêtre")
            {
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

        public ViewDevisPlan(int iduser, int idclient, int iddevis, int idplan)
        {
            
            InitializeComponent();
            this.iduser = -1;
            this.idclient = idclient;
            this.iddevis = iddevis;
            this.idplan = idplan;
            this.afficherDevis();

        }
        
        public ViewDevisPlan(int iduser, int idclient, int iddevis)
        {

            InitializeComponent();
            this.iduser = iduser;
            this.idclient = idclient;
            this.iddevis = iddevis;
            this.afficherDevis();

        }



        public void afficherDevis() {
            LocalDatabase database = new LocalDatabase();
            Devis devisFinal = database.tableDevis.Where(devis => devis.id == this.iddevis).ToList()[0];
            List<Constituer> constituers = database.tableConstituer.Where(module => module.devis_id == devisFinal.id).ToList();

            //la liste de tous les modules constituant le devis final
            List<Module> modules = new List<Module>();
            foreach(var occurenceConstituer in constituers) {
                modules.Add(database.tableModule.Where(module => module.id == occurenceConstituer.module_id).ToList()[0]);
            }

            //récupérer la catégorie du module
            /*List<Categorie> categories = new List<Categorie>();
            foreach(var module in modules) {
                
                categories.Add(database.tableCategorie.Where(categorie => module.categorie_id == categorie.id).ToList()[0]);        
            }*/


            //Pour chaque module, récupère une gamme
            List<Gamme> gammes = new List<Gamme>();


            /*foreach (var moduleItem in modules){
                 
                if (database.tableGamme.Where(gamme => gamme.id == moduleItem.gamme_id).ToList()[0] == null)
                {
                    contenu_devis.Text = "Aucune gamme trouvée.";
                }
                else
                {
                    gammes.Add(database.tableGamme.Where(gamme => gamme.id == moduleItem.gamme_id).ToList()[0]);
                }
            }*/


            //On récupère également la matière de chaque module
            List<Matiere> matieres = new List<Matiere>();
            foreach(var constituer in constituers){
                matieres.Add(database.tableMatiere.Where(matiere => matiere.id == constituer.matiere_id).ToList()[0]);
            }

            if (modules.Count == matieres.Count) {
               
                StringBuilder builder = new StringBuilder("-");
                for(int i = 0; i < modules.Count; i++) {
                    builder.Append(matieres[i].lib_matiere).Append(modules[i].prix).Append(" ").Append("euros").Append('\n').Append("-");
                }

                contenu_devis.Text = builder.ToString();
            }




        }

        
    }


}