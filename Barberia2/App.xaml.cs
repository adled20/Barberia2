using Barberia2.Data;

namespace Barberia2
{
    public partial class App : Application
    {
        private static CarritoDatabase DataBase;
        public static CarritoDatabase dataBase
        {
            get
            {
                if (DataBase == null)
                {
                    var url = Path.Combine(FileSystem.AppDataDirectory, "BaseDatos.db3");
                    return DataBase = new CarritoDatabase(url);
                }
                else
                {
                    return DataBase;
                }
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
            
        }

       
    }
}