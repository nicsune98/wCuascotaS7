using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace wCuascotaS7.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        public Login()
        {
            InitializeComponent();
            conexion = DependencyService.Get<DataBase>().GetConnection();
        }

        public static IEnumerable<Estudiantes> Select_Where(SQLiteConnection db, string usuario, string contrasena) 
        {
            return db.Query<Estudiantes>("SELECT * FROM Estudiantes Where Usuario =? and Contrasena =?",usuario,contrasena);
        }
        private void btInicio_Clicked(object sender, EventArgs e)
        {
            try
            {
                var ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(ruta);
                db.CreateTable<Estudiantes>();
                IEnumerable<Estudiantes> resultado = Select_Where(db, txtUsuario.Text, txtContrasena.Text);
                if (resultado.Count() > 0)
                {
                    Navigation.PushAsync(new Vistas.ConsultaRegistros());
                }
                else 
                {
                    DisplayAlert("Alerta", "Usuario o Contraseña Incorrectos", "Aceptar");
                }
            }
            catch (Exception)
            {
                throw;

            }
        }

        private void btRegistro_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Registro());
        }
    }
}