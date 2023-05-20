using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.IO;

namespace wCuascotaS7.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registro : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        public Registro()
        {
            InitializeComponent();
            conexion = DependencyService.Get<DataBase>().GetConnection();
        }

        private void btRegistrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var datos = new Estudiantes
                {
                    Nombre = txtNombre.Text,
                    Usuario = txtUsuario.Text,
                    Contrasena = txtContrasena.Text,
                };
                conexion.InsertAsync(datos);
                DisplayAlert("Mensaje", "Usuario Creado", "Aceptar");
                txtNombre.Text = "";
                txtUsuario.Text = "";
                txtContrasena.Text = "";
            }
            catch (Exception ex)
            {

                DisplayAlert("Alerta",ex.Message,"Cerrar");
            }
        }

        private void brregresar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Login());
        }
    }
}