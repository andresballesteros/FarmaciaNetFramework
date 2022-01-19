using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Farmacia
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private String msnError = "";
        private Boolean error;

        //Inicializacion de los componentes de la ventana principal
        public MainWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        //Se sobreescribe el método OnClosing para terminar la aplicación al cerrar la ventana
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //Función de borrado que inicializa el formulario
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            medicamento.Text = "";
            cantidad.Text = "";
            tipoMedicamento.SelectedIndex = 0;
            cofarma.IsChecked = false;
            empsephar.IsChecked = false;
            cemefar.IsChecked = false;
            principal.IsChecked = false;
            segundaria.IsChecked = false;
        }


        //Función para el envió del formulario
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //se realizan las validaciones
            validarCampos();
            if (error)
            {
                //Si hay error se presenta una ventana de dialogo con los errores presentados
                MessageBox.Show(msnError, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                //se oculta la ventana principal
                this.Hide();
                //Se inicializa la ventana de confirmación
                Window1 confirmacion = new Window1(obtenerDistribuidor(), obtenerResumenPedido(), obtenerDireccion());
                //Se muestra la ventana de confirmación del pedido
                confirmacion.Show();
            }
        }

        //Función que valida los campos del formulario de la ventana principal
        private Boolean validarCampos()
        {
            msnError = "";
            error = false;
            if (medicamento.Text.Equals("") || medicamento.Text.Equals(null))
            {
                error = true;
                msnError = msnError + "Debe ingresar un Medicamento \n";
            }
            if (!medicamento.Text.Equals("") && !medicamento.Text.Equals(null))
            {
                if (!Regex.IsMatch(medicamento.Text, "[0-9a-zA-Z]+"))
                {
                    error = true;
                    msnError = msnError + "El medicamento solo debe contener números y letras \n";
                }
            }
            if (cantidad.Text.Equals("") || medicamento.Text.Equals(null))
            {
                error = true;
                msnError = msnError + "Debe ingresar la cantidad de  medicamentos \n";
            }
            if (!cantidad.Text.Equals("") && !medicamento.Text.Equals(null))
            {
                if (!Regex.IsMatch(cantidad.Text, "[1-9]+"))
                {
                    error = true;
                    msnError = msnError + "La cantidad de medicamentos debe ser un número mayor o igual a 1 \n";
                }
            }
            if (empsephar.IsChecked == false && cofarma.IsChecked == false && cemefar.IsChecked == false)
            {
                error = true;
                msnError = msnError + "Debe seleccionar un distribuidor farmacéutico \n";
            }

            if (tipoMedicamento.SelectedIndex.Equals(-1) || tipoMedicamento.SelectedIndex.Equals(0))
            {
                error = true;
                msnError = msnError + "Debe seleccionar un tipo medicamento \n";
            }

            if (principal.IsChecked == false && segundaria.IsChecked == false)
            {
                error = true;
                msnError = msnError + "Debe seleccionar por lo menos una sucursal \n";
            }

            return error;
        }

        //Fuinción que obtiene el distribuidor seleccionado por el usuario
        private String obtenerDistribuidor()
        {
            String distribuidor = "";
            if (cofarma.IsChecked == true)
            {
                distribuidor = cofarma.Content.ToString();
            }
            else if (empsephar.IsChecked == true)
            {
                distribuidor = empsephar.Content.ToString();
            }
            else if (cemefar.IsChecked == true)
            {
                distribuidor = cemefar.Content.ToString();
            }
            return distribuidor;
        }

        //Función que construye el resumen del pedido
        private String obtenerResumenPedido()
        {
            return cantidad.Text + " unidades del " + tipoMedicamento.Text + " " + medicamento.Text;
        }

        //Función que construye la dirección del pedido
        private String obtenerDireccion()
        {
            String direccion = "";

            if (principal.IsChecked == true && segundaria.IsChecked == false)
            {
                direccion = "Para la farmacia situada en Calle de la Rosa n.28";
            }
            else if (principal.IsChecked == false && segundaria.IsChecked == true)
            {
                direccion = "Para la farmacia situada en Calle Alcazabilla n.3";
            }
            else if (principal.IsChecked == true && segundaria.IsChecked == true)
            {
                direccion = "Para la farmacia situada en Calle de la Rosa n.28 y para la situada en Calle Alcazabilla n.3";

            }
            return direccion;
        }

    }
}
