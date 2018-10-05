using RegistroDetalle.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistroDetalle
{
    public partial class Registro : Form
    {
        public List<TelefonosDetalle> Detalle { get; set; }

        public Registro()
        {
            InitializeComponent();
            this.Detalle = new List<TelefonosDetalle>();
        } 
        

        public void Limpiar ()
        {
            MyErrorProvider.Clear();
            IDnumericUpDown.Value = 0;
            NombreTextBox.Text = string.Empty;
            CedulaMaskedTextBox.Text = string.Empty;
            DireccionTextBox.Text = string.Empty;
            FechaDateTimePicker.Value = DateTime.Now;

            this.Detalle = new List<TelefonosDetalle>();
            CargarGrid();
        }
  
    }
}
