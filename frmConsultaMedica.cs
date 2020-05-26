using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stream_25_05
{
    public partial class frmConsultaMedica : Form
    {
        private double porcCardio, porcOdonto, porcPedia, porcMedife, porcOSDE, porcPAMI, acuTotalMensual, acutTotalAnual, acuDescuentoMensual, acuDescuentoAnual;
        private int contCardio, contOdonto, contPedia, contMedife, contOSDE, contPAMI, contConsultas, contConsultasConObraSocial;
        private bool esPrimerPaciente = true;
        private Paciente consultaMax, consultaMin;

        public frmConsultaMedica()
        {
            InitializeComponent();
            porcCardio = porcOdonto = porcPedia = porcMedife = porcOSDE = porcPAMI = acuTotalMensual = acutTotalAnual = acuDescuentoMensual = acuDescuentoAnual = contCardio = contOdonto = contPedia = contMedife = contOSDE = contPAMI = contConsultas = contConsultasConObraSocial = 0;
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            // variable para verificar cual radio button de sexo se ha seleccionado

            int sexoSeleccionado = 3;

            if (rdoMasc.Checked) sexoSeleccionado = 1;
            if (rdoFem.Checked) sexoSeleccionado = 2;
            if (rdoOtro.Checked) sexoSeleccionado = 3;

            // --------------------------------------------------------------------------
            // instancia de clase, y asignacion de propiedades segun datos del formulario (usando constructor con parametros)

            Paciente pacienteCero = new Paciente(
                int.Parse(txtMatricula.Text),
                txtNombre.Text,
                txtApellido.Text,
                cboTipoDoc.SelectedIndex,
                int.Parse(txtNroDoc.Text),
                sexoSeleccionado,
                dtpFechaNac.Value,
                chkEsCasado.Checked,
                chkTieneObraSocial.Checked,
                cboObraSocial.SelectedIndex,
                cboEspecialidad.SelectedIndex,
                double.Parse(txtPrecioConsulta.Text),
                dtpFechaConsulta.Value);

            // en caso de usar el constructor vacio, se podria asignar las propiedades a mano usando la asignacion

            //pacienteCero.Matricula = int.Parse(txtMatricula.Text);
            //pacienteCero.Nombre = txtNombre.Text;
            //pacienteCero.Apellido = txtApellido.Text;
            //pacienteCero.TipoDoc = cboTipoDoc.SelectedIndex;
            //pacienteCero.NroDoc = int.Parse(txtNroDoc.Text);
            //pacienteCero.Sexo = sexoSeleccionado;
            //pacienteCero.FechaNac = dtpFechaNac.Value;
            //pacienteCero.EsCasado = chkEsCasado.Checked;
            //pacienteCero.TieneObraSocial = chkTieneObraSocial.Checked;
            //pacienteCero.ObraSocial = cboObraSocial.SelectedIndex;
            //pacienteCero.EspecialidadConsulta = cboEspecialidad.SelectedIndex;
            //pacienteCero.PrecioConsulta = double.Parse(txtPrecioConsulta.Text);
            //pacienteCero.FechaConsulta = dtpFechaConsulta.Value;

            // consultas x especialidad

            contConsultas++;
            switch (pacienteCero.EspecialidadConsulta)
            {
                case 1: contCardio++; break;
                case 2: contOdonto++; break;
                case 3: contPedia++; break;
            }

            porcCardio = (contCardio * 100) / contConsultas;
            porcOdonto = (contOdonto * 100) / contConsultas;
            porcPedia = (contPedia * 100) / contConsultas;

            lblCantCardio.Text = contCardio.ToString();
            lblCantOdonto.Text = contOdonto.ToString();
            lblCantPedia.Text = contPedia.ToString();

            lblPorcCardio.Text = porcCardio.ToString() + "%";
            lblPorcOdonto.Text = porcOdonto.ToString() + "%";
            lblPorcPedia.Text = porcPedia.ToString() + "%";

            // consultas x especialidad (solamente si esta seleccionada la opcion de que TIENE obra social

            if (chkTieneObraSocial.Checked)
            {
                switch (pacienteCero.ObraSocial)
                {
                    case 1: contMedife++; break;
                    case 2: contOSDE++; break;
                    case 3: contPAMI++; break;
                }

                contConsultasConObraSocial++;

                porcMedife = (contMedife * 100) / contConsultasConObraSocial;
                porcOSDE = (contOSDE * 100) / contConsultasConObraSocial;
                porcPAMI = (contPAMI * 100) / contConsultasConObraSocial;

                lblCantMedife.Text = contMedife.ToString();
                lblCantOsde.Text = contOSDE.ToString();
                lblCantPAMI.Text = contPAMI.ToString();

                lblPorcMedife.Text = porcMedife.ToString();
                lblPorcOsde.Text = porcOSDE.ToString();
                lblPorcPAMI.Text = porcPAMI.ToString();
            }

            // especialidad mas cara y precio y mas barata y precio

            if (esPrimerPaciente)
            {
                esPrimerPaciente = false;
                consultaMax = pacienteCero;
                consultaMin = pacienteCero;
            }
            else
            {
                if (pacienteCero.PrecioConsulta > consultaMax.PrecioConsulta) consultaMax = pacienteCero;
                if (pacienteCero.PrecioConsulta < consultaMin.PrecioConsulta) consultaMin = pacienteCero;
            }

            lblEspecialidadMasCara.Text = consultaMax.EspecialidadToString();
            lblPrecioEspecialidadMasCara.Text = consultaMax.PrecioConsulta.ToString();
            lblFechaEspecialidadMasCara.Text = consultaMax.FechaConsulta.ToShortDateString();

            lblEspecialidadMasBarata.Text = consultaMin.EspecialidadToString();
            lblPrecioEspecialidadMasBarata.Text = consultaMin.PrecioConsulta.ToString();
            lblFechaEspecialidadMasBarata.Text = consultaMin.FechaConsulta.ToShortDateString();

            // datos contables acumulados (en proceso, queda para el proximo zoom)

            if (pacienteCero.FechaConsulta.Month == DateTime.Today.Month && !chkTieneObraSocial.Checked) acuTotalMensual += pacienteCero.PrecioConsulta;

            if (pacienteCero.FechaConsulta.Month == DateTime.Today.Month && chkTieneObraSocial.Checked && pacienteCero.ObraSocial == 1) acuTotalMensual += pacienteCero.PrecioConsulta * 0.8;

            if (pacienteCero.FechaConsulta.Month == DateTime.Today.Month && chkTieneObraSocial.Checked && pacienteCero.ObraSocial == 2) acuTotalMensual += pacienteCero.PrecioConsulta * 0.7;

            if (pacienteCero.FechaConsulta.Month == DateTime.Today.Month && chkTieneObraSocial.Checked && pacienteCero.ObraSocial == 2) acuTotalMensual += pacienteCero.PrecioConsulta * 0.95;

            lblTotalMensual.Text = acuTotalMensual.ToString();

            // Mensaje de alta de paciente en el registro

            //MessageBox.Show(pacienteCero.MostrarDatos(), "Estado de la venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
        }

        private void chkTieneObraSocial_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTieneObraSocial.Checked) cboObraSocial.Enabled = true;
            else cboObraSocial.Enabled = false;
        }
    }
}