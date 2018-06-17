using System;
using System.Collections.Generic;
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
        public int iduser;
        public int idclient;
        public int iddevis;
        public int idplan;

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
            List<Categorie> categories = new List<Categorie>();
            foreach(var module in modules) {
                
                categories.Add(database.tableCategorie.Where(categorie => module.categorie_id == categorie.id).ToList()[0]);        
            }


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
                    builder.Append(categories[i].lib_categorie).Append(matieres[i].lib_matiere).Append('\n').Append("-");
                }

                contenu_devis.Text = builder.ToString();
            }




        }

        
    }
}