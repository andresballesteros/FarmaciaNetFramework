using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Farmacia
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        //se crea un nuevo objeto de la ventana principal
        MainWindow ventanaPrincipal = new MainWindow();

        //Se sobreescribe el método OnClosing para terminar la aplicación al cerrar la ventana
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //Inicializacion de los componentes de la ventana principal
        public Window1(String distribuidor, String resumenPedido, String resumenDireccion)
        {
            //Se centra la ventana en la pantalla
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            //Se establece el títuilo de la ventana segundaria 
            Title = "Pedido al distribuidor " + distribuidor;
            InitializeComponent();
            //Se asignan los textos de el resumen del pedido y la dirección
            resumen.Content = resumenPedido;
            direccion.Content = resumenDireccion;
        }

        //Función del boton para la cancelación del envío del pedido
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Se oculta la ventana segundaria
            this.Hide();
            //Se muestra la nueva ventana
            ventanaPrincipal.Show();
        }

        //Función del boton para la confirmación del pedido
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Dialogo de confirmación
            MessageBoxResult result = MessageBox.Show("¿Desea enviar este pedido?", "Confirmación", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            //Se valida la confirmación del dialogo
            if (result == MessageBoxResult.OK)
            {
                //Muestra el mensaje exitoso del pedido
                MessageBox.Show("Pedido enviado", "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);
                //Oculta la ventana segundaria
                this.Hide();
                //Muestra la ventana principañ
                ventanaPrincipal.Show();
            }
        }
    }
}
