using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Mercado.Helper;
using System.IO;

namespace Mercado
{
    public partial class App : Application
    {
        static SQLiteDatabaseHelper bd;

        public static SQLiteDatabaseHelper BD
        {
            get
            {
                if(bd == null)
                {
                    string path = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "Mercadinho.db3"
                        );
                    bd = new SQLiteDatabaseHelper(path);
                }
                return bd;

            }

        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new View.Lista());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
