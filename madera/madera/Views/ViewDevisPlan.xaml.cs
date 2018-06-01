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
        public int iduser;
        public int idclient;
        public int iddevis;
        public int idplan;
        public ViewDevisPlan ()
        {
            InitializeComponent();
        }

        public ViewDevisPlan(int iduser, int idclient, int iddevis, int idplan)
        {
            this.iduser = iduser;
            this.idclient = idclient;
            this.iddevis = iddevis;
            this.idplan = idplan;
        }
    }
}