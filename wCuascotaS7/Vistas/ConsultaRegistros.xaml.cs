using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace wCuascotaS7.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultaRegistros : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        private ObservableCollection<Estudiantes> TablaEstudiante;
        public ConsultaRegistros()
        {
            InitializeComponent();
            conexion = DependencyService.Get<DataBase>().GetConnection();
            ObtenerEstudiantes();
        }

        public async void ObtenerEstudiantes()
        {
            var resultadoEstudiantes = await conexion.Table<Estudiantes>().ToListAsync();
            TablaEstudiante = new ObservableCollection<Estudiantes>(resultadoEstudiantes);
            listaEstudinates.ItemsSource = TablaEstudiante;
        }

        private void listaEstudinates_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var objetoEstudiante = (Estudiantes)e.SelectedItem;
            var itemId = objetoEstudiante.Id.ToString();
            int id = Convert.ToInt32(itemId);
            string nombre = objetoEstudiante.Nombre.ToString();
            string usuario = objetoEstudiante.Usuario.ToString();
            string contrasena = objetoEstudiante.Contrasena.ToString();
            Navigation.PushAsync(new Actualizar(id, nombre, usuario, contrasena));
        }

        private void btSalir_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Login());
        }
    }
}