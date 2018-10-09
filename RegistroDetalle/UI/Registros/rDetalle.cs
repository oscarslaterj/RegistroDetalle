using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RegistroDetalle.Entidades;
using RegistroDetalle.BLL;

namespace RegistroDetalle.UI.Registros
{
    public partial class rDetalle : Form
    {
        public rDetalle()
        {
            InitializeComponent();
        }

        private void GuardarDetallebutton_Click(object sender, EventArgs e)
        {
            bool paso = false;
            TelefonosDetalle tipo;
            if (!Validar())
                return;
            tipo = LlenaClase();

            if (IdDetallenumericUpDown.Value == 0)
                paso = PersonasBLL.Guardar(tipo);


            if (paso)
                MessageBox.Show("Guardado!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No se pudo guardar!!", "fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }



        private TelefonosDetalle LlenaClase()
        {
            TelefonosDetalle tipo = new TelefonosDetalle();
            tipo.Id = Convert.ToInt32(IdDetallenumericUpDown.Value);
            tipo.TipoTelefono = TipoDetalletextBox.Text;


            return tipo;
        }

     


        private bool Validar()
        {
            bool paso = true;
            errorProvider1.Clear();
            if (string.IsNullOrWhiteSpace(TipoDetalletextBox.Text))
            {
                errorProvider1.SetError(TipoDetalletextBox, "Campo esta vacio");
                paso = false;
            }

            return paso;
        }

    }
}
