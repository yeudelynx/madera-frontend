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
            
            //picker Gamme
            var pickerGamme = new Picker { Title = "Gamme" };
            pickerGamme.ItemDisplayBinding = new Binding("lib_gamme");
            pickerGamme.ItemsSource = listeGamme;
            panneauSelection.Children.Add(pickerGamme);


            //picker Categorie
            var pickerCategorie = new Picker { Title = "Catégorie" };
            pickerCategorie.ItemDisplayBinding = new Binding("lib_categorie");
            pickerCategorie.ItemsSource = listeCategorie;
            panneauSelection.Children.Add(pickerCategorie);

            //picker Module
            var pickerModule = new Picker { Title = "Module" };
            pickerModule.ItemDisplayBinding = new Binding("lib_module");
            pickerModule.ItemsSource = listeModule;
            panneauSelection.Children.Add(pickerModule);




            // à supprimer quand j'aurai des données 
            /*Xamarin.Forms.Label label_test2 = new Label();
            label_test2.Text = "Module blanc";
            label_test2.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                // 1 => afficher les valeurs du bazar selectionné dans les specs en bas

                Command = new Command(() =>
                {
                    // liste d'actions du onclick
                    //test.Text = "Module blanc";
                })
            });

            btn_gamme.Children.Add(label_test2);*/


            // label de la gamme 
            foreach (var gamme in listeGamme)
            {
                Xamarin.Forms.Label label_gamme = new Label();
                label_gamme.Text = gamme.lib_gamme;
                panneauSelection.Children.Add(label_gamme);

                // label des catégories

                
                foreach (var categorie in listeGamme)
                {
                    Xamarin.Forms.Label label_categorie = new Label();
                    label_categorie.Text = categorie.lib_gamme;
                    Thickness marge_categorie = label_categorie.Margin;
                    marge_categorie.Left = 20;
                    label_categorie.Margin = marge_categorie;
                    panneauSelection.Children.Add(label_categorie);
                    Console.WriteLine(label_gamme.Text);
                    
                    // label des modules 
                    foreach (var module in listeGamme)
                    {
                        Xamarin.Forms.Label label_module = new Label();
                        // => bug sur l'ID (balance un nombre random qui s'incrémente à chaque compilation)
                        label_module.Text = module.id.ToString();
                        Console.WriteLine(label_module.Text);
                        Thickness marge_module = label_module.Margin;
                        marge_module.Left = 40;
                        label_module.Margin = marge_module;

                        //libele clickable => rendre le lien plus joli ?
                        label_module.GestureRecognizers.Add(new TapGestureRecognizer()
                        {
                            // 1 => afficher les valeurs du bazar selectionné dans les specs en bas

                            // 2 => le positionner sur le plan
                            Command = new Command(() =>
                            {
                            })
                            //Command = new Command(() => Console.WriteLine(module.id)),
                        });
                        panneauSelection.Children.Add(label_module);
                    }

                }

                Xamarin.Forms.Label saut_ligne = new Label();
                saut_ligne.Text = "\n";

                //btn_gamme.Children.Add(Mur);
                panneauSelection.Children.Add(saut_ligne);

            }
            
        }

        // Manipulation du plan (+ maj devis)

        void click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            // servira à recupérer les coordonnées dans l'espace => x y z
            // var classId = button.ClassId;
            string selectedValue = "";
            try
            {
                selectedValue = pickerGamme.Items[pickerGamme.SelectedIndex];
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
            if (selectedValue == "Acier")
            {
                button.BackgroundColor = Color.Gray;
                Xamarin.Forms.Label label_maron = new Label();
                label_maron.Text = selectedValue;
                label_maron.ClassId = "1";
                listeDevis.Children.Add(label_maron);

            }
            if (selectedValue == "Bois")
            {
                button.BackgroundColor = Color.Brown;
                Xamarin.Forms.Label label_maron = new Label();
                label_maron.Text = selectedValue;
                label_maron.ClassId = "1";
                listeDevis.Children.Add(label_maron);

            }
            if (selectedValue == "Béton")
            {
                button.BackgroundColor = Color.Black;
                Xamarin.Forms.Label label_green = new Label();
                label_green.Text = selectedValue;
                label_green.ClassId = "2";
                listeDevis.Children.Add(label_green);
            }

            if (selectedValue == "Verre")
            {
                button.BackgroundColor = Color.Cyan;
                Xamarin.Forms.Label label_green = new Label();
                label_green.Text = selectedValue;
                label_green.ClassId = "2";
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