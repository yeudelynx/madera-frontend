using Android.Widget;
using madera.Helpers;
using madera.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xamarin.Forms;


namespace madera
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            //Call task Refresher 

            //Show sync loading

            //Wait Refresher response

            //If Refresher response == TRUE, show sync OK

            //If Refresher response == FALSE, show sync KO...

            //Hide sync loading

            ProcessSync();
        }

        async void ProcessSync()
        {
            try
            {
                SyncDatas syncDatas = new SyncDatas();
                ResponseSync responseSync = await syncDatas.Process();

                //Save datas in DB.
                LocalDatabase db = new LocalDatabase();
                db.WriteSync(responseSync);
                Console.WriteLine(db.tableDate.Where(s => s.id.Equals(1)));

                foreach(var user in db.tableUser){
                    Console.WriteLine(" ++++++ USER LOGIN" + user.id +" : " + user.login);
                }
            }
            catch (Exception ex){
                //Show popup warning here !!!!!
                // + "RollBack" ?!

                //Toast.MakeText(this, ex.StackTrace, ToastLength.Long).Show();
                Console.WriteLine("error : " + ex);
            }


        }
    }
}
