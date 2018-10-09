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
using RegistroDetalle.BLL;
using RegistroDetalle.DAL;
using RegistroDetalle.UI;
using RegistroDetalle.UI.Registros;

namespace RegistroDetalle
{
    public partial class Registro : Form
    {
        private int id;

        public List<TelefonosDetalle> Detalle { get; set; }

        public Registro()
        {
            InitializeComponent();
            this.Detalle = new List<TelefonosDetalle>();
        }

        private void CargarGrid()
        {
            DetalleDataGridView.DataSource = null;
            DetalleDataGridView.DataSource = this.Detalle;
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

        private Personas LlenaClase()
        {
            Personas persona = new Personas();
            persona.PersonaID = Convert.ToInt32(IDnumericUpDown.Value);
            persona.Nombre = NombreTextBox.Text;
            persona.Cedula = CedulaMaskedTextBox.Text;
            persona.Direccion = DireccionTextBox.Text;
            persona.FechaNacimiento = FechaDateTimePicker.Value;

            persona.Telefonos = this.Detalle;

            return persona;
        }

        private void LlenaCampo(Personas persona)
        {
            IDnumericUpDown.Value = persona.PersonaID;
            NombreTextBox.Text = persona.Nombre;
            CedulaMaskedTextBox.Text = persona.Cedula;
            DireccionTextBox.Text = persona.Direccion;
            FechaDateTimePicker.Value = persona.FechaNacimiento;

            this.Detalle = persona.Telefonos;
            CargarGrid();
        }

        private void LlenaCampos(Personas persona)
        {
            IDnumericUpDown.Value = persona.PersonaID;
            NombreTextBox.Text = persona.Nombre;
            CedulaMaskedTextBox.Text = persona.Cedula;
            DireccionTextBox.Text = persona.Direccion;
            FechaDateTimePicker.Value = persona.FechaNacimiento;

            this.Detalle = persona.Telefonos;
            CargarGrid();
        }

        private bool Validar()
        {
            bool paso = true;
            MyErrorProvider.Clear();

            if(NombreTextBox.Text == string.Empty)
            MyErrorProvider.SetError(NombreTextBox, "No dejar campo vacio");

            if (string.IsNullOrWhiteSpace(DireccionTextBox.Text))
                MyErrorProvider.SetError(DireccionTextBox, "No dejar campo vacio");

            if (string.IsNullOrWhiteSpace(CedulaMaskedTextBox.Text))
                MyErrorProvider.SetError(CedulaMaskedTextBox, "No dejar campo vacio");

            if(this.Detalle.Count==0)
            {
                MyErrorProvider.SetError(DetalleDataGridView, "Debe agregar algun telefono");
                TelefonoMaskedTextBox.Focus();
                paso = false;
            }
            return paso;

        }

        private void AgregarButton_Click(object sender, EventArgs e)
        {
            if (DetalleDataGridView.DataSource != null)
                this.Detalle = (List<TelefonosDetalle>)DetalleDataGridView.DataSource;

            this.Detalle.Add(
                new TelefonosDetalle(
                    id = 0,
                    idPersona: (int)IDnumericUpDown.Value,
                    telefono: TelefonoMaskedTextBox.Text,
                     tipoTelefono: TipoComboBox.Text
                )
            );
            CargarGrid();
            TelefonoMaskedTextBox.Focus();
            TelefonoMaskedTextBox.Clear();
            TipoComboBox.Items.Clear();
          
        }

        private bool ExiteEnLaBaseDeDatos()
        {
            Personas persona = PersonasBLL.Buscar((int)IDnumericUpDown.Value);
            return (persona != null);
        }


        private void RemoverButton_Click(object sender, EventArgs e)
        {
            if(DetalleDataGridView.Rows.Count > 0 && DetalleDataGridView.CurrentRow != null)
            {
                Detalle.RemoveAt(DetalleDataGridView.CurrentRow.Index);
                    CargarGrid();
            }
        }

        private void tiposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rDetalle registro = new rDetalle();
            registro.ShowDialog();
        }

        private void BuscarButton_Click(object sender, EventArgs e)
        {
            MyErrorProvider.Clear();
            int id;
            Personas persona = new Personas();
            int.TryParse(IDnumericUpDown.Text, out id);

            persona = PersonasBLL.Buscar(id);

            if (persona != null)
            {
                MyErrorProvider.Clear();
                this.LlenaCampo(persona);
                MessageBox.Show("Persona Encotrada");

            }
            else
            {
                MessageBox.Show("Persona no Encotrada");
            }    

        }

        private void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            bool paso = false;
            Personas persona;
            if (!Validar())
                return;
            persona = LlenaClase();

            if (IDnumericUpDown.Value == 0)
                paso = PersonasBLL.Guardar(persona);
            else
            {
                if (!ExiteEnLaBaseDeDatos())
                {
                    MessageBox.Show("Nose puede Modificar No Exite", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                paso = PersonasBLL.Modificar(persona);
            }
            Limpiar();

            if (paso)
                MessageBox.Show("Guardado!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No se pudo guardar!!", "fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void EliminarButton_Click(object sender, EventArgs e)
        {
            MyErrorProvider.Clear();
            int id;

            int.TryParse(IDnumericUpDown.Text, out id);

            if (PersonasBLL.Eliminar(id))
            {
                MessageBox.Show("Eliminado");
            }
            else
                MyErrorProvider.SetError(IDnumericUpDown, "Persona no Exite");
        }

        private void TipoButton_Click(object sender, EventArgs e)
        {
            rDetalle tipo = new rDetalle();
            tipo.ShowDialog();
            tipo.Close();
        }

        private void LlenarComboBOx()
        {
            TipoComboBox.DataSource = PersonasBLL.GetList(x => true);
            TipoComboBox.ValueMember = "Tipo";
        }
    }
}
