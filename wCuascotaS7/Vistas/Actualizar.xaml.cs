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
    public partial class Actualizar : ContentPage
    {
        private int idSeleccionado;
        private SQLiteAsyncConnection conexion;
        IEnumerable<Estudiantes> REactualizar;
        IEnumerable<Estudiantes> REeliminar;
        public Actualizar(int id, string nombre, string usuario, string contrasena)
        {
            InitializeComponent();
            txtId.Text = id.ToString();
            txtNombre.Text = nombre;
            txtUsuario.Text = usuario;
            txtContrasena.Text = contrasena;
            conexion = DependencyService.Get<DataBase>().GetConnection();
            idSeleccionado = id;
        }

        public static IEnumerable<Estudiantes> Eliminar(SQLiteConnection db, int id) 
        {
            return db.Query<Estudiantes>("DELETE FROM Estudiantes Where Id =?", id);
        }

        public static IEnumerable<Estudiantes> ActualizarEstudiante(SQLiteConnection db, string nombre, string usuario, string contrasena, int id) 
        {
            return db.Query<Estudiantes>("UPDATE Estudiantes set Nombre =?, Usuario =?, Contrasena =? Where Id =?", nombre, usuario, contrasena, id);
        }

        private async void btActualizar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(ruta);
                if (await DisplayAlert("Confirmación", "¿Está seguro que desea actualizar el elemento", "SI", "NO"))
                {
                    REactualizar = ActualizarEstudiante(db, txtNombre.Text, txtUsuario.Text, txtContrasena.Text, idSeleccionado);
                    await Navigation.PushAsync(new ConsultaRegistros());
                }
                else
                {
                    await DisplayAlert("Mensaje", "No se pudo actualizar al estudiante", "Aceptar");
                }
            }
            catch (Exception ex)
            {

                await DisplayAlert("Error", ex.Message, "Cerrar");
            }
            
        }

        private async void btEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(ruta);
                if (await DisplayAlert("Confirmación", "¿Está seguro que desea eliminar el elemento?" + Environment.NewLine + "Esta acción no se puede revertir", "SI", "NO"))
                {
                    REeliminar = Eliminar(db,idSeleccionado);
                    await Navigation.PushAsync(new ConsultaRegistros());
                }
                else
                {
                    await DisplayAlert("Mensaje", "No se pudo eliminar al estudiante", "Aceptar");
                }
            }
            catch (Exception ex)
            {

                await DisplayAlert("Error", ex.Message, "Cerrar");
            }
        }
    }
}