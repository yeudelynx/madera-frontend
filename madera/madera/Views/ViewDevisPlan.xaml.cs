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

        public class CustomButton : Button
        {
        }
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
            var tableGamme = db.tableGamme.ToList();
            var tableCategorie = db.tableCategorie.ToList();
            var tableModule = db.tableModule.ToList();

            // à supprimer quand j'aurai des données 
            Xamarin.Forms.Label label_test = new Label();
            label_test.Text = "Module noir";
            label_test.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                // 1 => afficher les valeurs du bazar selectionné dans les specs en bas

                Command = new Command(() =>
                {
                    // liste d'actions du onclick
                    //test.Text = "Module noir";
                    idBordelCLicke = "idBordelCLicke";

                })
            });

            btn_gamme.Children.Add(label_test);

            // à supprimer quand j'aurai des données 
            Xamarin.Forms.Label label_test2 = new Label();
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

            btn_gamme.Children.Add(label_test2);

            gammePicker.Items.Add("Aucune");
            gammePicker.Items.Add("Marron");
            gammePicker.Items.Add("Vert");




            
            // label de la gamme 
            foreach (var gamme in tableGamme)
            {
                Xamarin.Forms.Label label_gamme = new Label();
                label_gamme.Text = gamme.lib_gamme;
                btn_gamme.Children.Add(label_gamme);

                // label des catégories
                foreach (var categorie in tableCategorie)
                {
                    Xamarin.Forms.Label label_categorie = new Label();
                    label_categorie.Text = categorie.lib_categorie;
                    Thickness marge_categorie = label_categorie.Margin;
                    marge_categorie.Left = 20;
                    label_categorie.Margin = marge_categorie;
                    btn_gamme.Children.Add(label_categorie);
                    Console.WriteLine(label_gamme.Text);
                    
                    // label des modules 
                    foreach (var module in tableModule)
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
                                DisplayAlert("Task", "Clicked " + module.longueur, "ok");
                            })
                            //Command = new Command(() => Console.WriteLine(module.id)),
                        });
                        btn_gamme.Children.Add(label_module);
                    }


                }

                Xamarin.Forms.Label saut_ligne = new Label();
                saut_ligne.Text = "\n";

                //btn_gamme.Children.Add(Mur);
                btn_gamme.Children.Add(saut_ligne);




            }
        }

        void click(object sender, EventArgs e)
        {
            string selectedValue = "";
            try
            {
                selectedValue = gammePicker.Items[gammePicker.SelectedIndex];
            }
            catch (Exception)
            {
                //DisplayAlert("Erreur", "Veuillez selectionner une gamme", "Ok");
            }
            var button = (Button)sender;


            if (selectedValue == "Aucune")
            {
                button.BackgroundColor = Color.Default;
            }
            if (selectedValue == "Marron")
            {
                button.BackgroundColor = Color.Brown;
            }
            if (selectedValue == "Vert")
            {
                button.BackgroundColor = Color.Green;
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