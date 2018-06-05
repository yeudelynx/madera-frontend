using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using madera.Models;
using System.IO;
using System.Globalization;

namespace madera.Helpers
{
    public class LocalDatabase
    {
        public SQLiteConnection db { get; set; }
        public String dbName { get; set; }
        public TableQuery<Categorie> tableCategorie { get; set; }
        public TableQuery<Client> tableClient { get; set; }
        public TableQuery<Constituer> tableConstituer { get; set; }
        public TableQuery<Couleur> tableCouleur { get; set; }
        public TableQuery<Date> tableDate { get; set; }
        public TableQuery<Devis> tableDevis { get; set; }
        public TableQuery<Gamme> tableGamme { get; set; }
        public TableQuery<Magasin> tableMagasin { get; set; }
        public TableQuery<Matiere> tableMatiere { get; set; }
        public TableQuery<Module> tableModule { get; set; }
        public TableQuery<Remise> tableRemise { get; set; }
        public TableQuery<Sol> tableSol { get; set; }
        public TableQuery<Unite> tableUnite { get; set; }
        public TableQuery<User> tableUser { get; set; }

        public LocalDatabase(string dbName = "madera.db3")
        {
            try
            {
                this.dbName = dbName;

                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), dbName);
                if (!File.Exists(dbPath))
                {
                    Console.WriteLine("Creating database");
                    db = new SQLiteConnection(dbPath);
                    db.CreateTable<Categorie>();
                    db.CreateTable<Client>();
                    db.CreateTable<Constituer>();
                    db.CreateTable<Couleur>();
                    db.CreateTable<Date>();
                    db.CreateTable<Devis>();
                    db.CreateTable<Gamme>();
                    db.CreateTable<Magasin>();
                    db.CreateTable<Matiere>();
                    db.CreateTable<Module>();
                    db.CreateTable<Remise>();
                    db.CreateTable<Sol>();
                    db.CreateTable<Unite>();
                    db.CreateTable<User>();

                    Date date = new Date();
                    date.date = DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss.ffffff", CultureInfo.InvariantCulture);
                    db.Insert(date);
                }
                else
                {
                    Console.WriteLine("Opening database");
                    db = new SQLiteConnection(dbPath);
                }
                tableCategorie = db.Table<Categorie>();
                tableClient = db.Table<Client>();
                tableConstituer = db.Table<Constituer>();
                tableCouleur = db.Table<Couleur>();
                tableDate = db.Table<Date>();
                tableDevis = db.Table<Devis>();
                tableGamme = db.Table<Gamme>();
                tableMagasin = db.Table<Magasin>();
                tableMatiere = db.Table<Matiere>();
                tableModule = db.Table<Module>();
                tableRemise = db.Table<Remise>();
                tableSol = db.Table<Sol>();
                tableUnite = db.Table<Unite>();
                tableUser = db.Table<User>();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Create LocalDatabase error : " + ex);
            }
        }

        public Boolean WriteSync(ResponseSync responseSync){
            try
            {
                db.DeleteAll<Categorie>();
                db.DeleteAll<Client>();
                db.DeleteAll<Constituer>();
                db.DeleteAll<Couleur>();
                db.DeleteAll<Date>();
                db.DeleteAll<Devis>();
                db.DeleteAll<Gamme>();
                db.DeleteAll<Magasin>();
                db.DeleteAll<Matiere>();
                db.DeleteAll<Module>();
                db.DeleteAll<Remise>();
                db.DeleteAll<Sol>();
                db.DeleteAll<Unite>();
                db.DeleteAll<User>();
                Console.WriteLine("TRACE count tableCategorie after delete : " + tableCategorie.Count());
                Console.WriteLine("TRACE count tableUsers after delete : " + tableUser.Count());

                db.Insert(responseSync.date);
                foreach (var categorie in responseSync.categories)
                {
                    db.Insert(categorie);
                }
                foreach (var client in responseSync.clients)
                {
                    db.Insert(client);
                }
                foreach (var constituer in responseSync.constituers)
                {
                    db.Insert(constituer);
                }
                foreach (var couleur in responseSync.couleurs)
                {
                    db.Insert(couleur);
                }
                foreach (var devis in responseSync.devis)
                {
                    db.Insert(devis);
                }
                foreach (var gamme in responseSync.gammes)
                {
                    db.Insert(gamme);
                }
                foreach (var magasin in responseSync.magasins)
                {
                    db.Insert(magasin);
                }
                foreach (var matiere in responseSync.matieres)
                {
                    db.Insert(matiere);
                }
                foreach (var module in responseSync.modules)
                {
                    db.Insert(module);
                }
                foreach (var remise in responseSync.remises)
                {
                    db.Insert(remise);
                }
                foreach (var sol in responseSync.sols)
                {
                    db.Insert(sol);
                }
                foreach (var unite in responseSync.unites)
                {
                    db.Insert(unite);
                }
                foreach (var user in responseSync.users)
                {
                    db.Insert(user);
                }
                Console.WriteLine("TRACE count tableCategorie after fill with sync data : " + tableCategorie.Count());
                Console.WriteLine("TRACE count tableUsers after fill with sync data : " + tableUser.Count());
                return true;
            }catch (Exception ex){
                Console.WriteLine("WriteSync error : " + ex);
                return false;
            }


        }


    }
}
