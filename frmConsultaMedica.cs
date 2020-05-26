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
        // declaramos las variables de contadores, acumuladores y porcentajes
        private double porcCardio, porcOdonto, porcPedia, porcMedife, porcOSDE, porcPAMI, acuTotalMensual, acutTotalAnual, acuTotalMensualTotal, acuTotalAnualTotal;

        private int contCardio, contOdonto, contPedia, contMedife, contOSDE, contPAMI, contConsultas, contConsultasConObraSocial;
        private bool esPrimerPaciente = true;

        // declaramos los objetos de comparacion para el calculo de la especialidad mas cara y la mas barata

        private Paciente consultaMax, consultaMin;

        public frmConsultaMedica()
        {
            InitializeComponent();

            // inicializamos todas las variables declaradas previamente

            porcCardio = porcOdonto = porcPedia = porcMedife = porcOSDE = porcPAMI = acuTotalMensual = acutTotalAnual = acuTotalMensualTotal = acuTotalAnualTotal = contCardio = contOdonto = contPedia = contMedife = contOSDE = contPAMI = contConsultas = contConsultasConObraSocial = 0;
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            // declaramos una variable para verificar cual radio button de sexo se ha seleccionado

            int sexoSeleccionado = 3;

            if (rdoMasc.Checked) sexoSeleccionado = 1;
            if (rdoFem.Checked) sexoSeleccionado = 2;
            if (rdoOtro.Checked) sexoSeleccionado = 3;

            // declaracion e iniciacion de la instancia de clase, y asignacion de propiedades segun datos del formulario (usando constructor con parametros)

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

            // definiendo las consultas por especialidad

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

            // definiendo las consultas por obra social (solamente si esta seleccionada la opcion de que TIENE obra social

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

                lblPorcMedife.Text = porcMedife.ToString() + "%";
                lblPorcOsde.Text = porcOSDE.ToString() + "%";
                lblPorcPAMI.Text = porcPAMI.ToString() + "%";
            }

            // definiendo la especialidad mas cara y la mas barata, sus precios y sus fechas

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

            // definiendo los datos contables acumulados

            //          total mensual

            if (pacienteCero.FechaConsulta.Month == DateTime.Today.Month)
            {
                acuTotalMensualTotal += pacienteCero.PrecioConsulta;
                if (chkTieneObraSocial.Checked)
                {
                    switch (pacienteCero.ObraSocial)
                    {
                        case 1: acuTotalMensual += pacienteCero.PrecioConsulta * 0.8; break;
                        case 2: acuTotalMensual += pacienteCero.PrecioConsulta * 0.7; break;
                        case 3: acuTotalMensual += pacienteCero.PrecioConsulta * 0.95; break;
                    }
                }
                else acuTotalMensual += pacienteCero.PrecioConsulta;
            }

            //          total anual

            if (pacienteCero.FechaConsulta.Year == DateTime.Today.Year)
            {
                acuTotalAnualTotal += pacienteCero.PrecioConsulta;
                if (chkTieneObraSocial.Checked)
                {
                    switch (pacienteCero.ObraSocial)
                    {
                        case 1: acutTotalAnual += pacienteCero.PrecioConsulta * 0.8; break;
                        case 2: acutTotalAnual += pacienteCero.PrecioConsulta * 0.7; break;
                        case 3: acutTotalAnual += pacienteCero.PrecioConsulta * 0.95; break;
                    }
                }
                else acutTotalAnual += pacienteCero.PrecioConsulta;
            }

            //asignacion de totales y totales de descuento

            lblTotalMensual.Text = "$" + (Math.Round(acuTotalMensual, 2)).ToString();
            lblTotalAnual.Text = "$" + (Math.Round(acutTotalAnual, 2)).ToString();

            lblDescuentoMensual.Text = "$" + (Math.Round((acuTotalMensualTotal - acuTotalMensual), 2)).ToString();
            lblDescuentoAnual.Text = "$" + (Math.Round((acuTotalAnualTotal - acutTotalAnual), 2)).ToString();

            // Mensaje de alta de paciente en el registro (mostrando datos)

            MessageBox.Show(pacienteCero.MostrarDatos(), "Estado del alta", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // metodo para resetear la informacion almacenada en el sistema

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // seteamos los contadores, porcentajes y acumuladores en 0

            porcCardio = porcOdonto = porcPedia = porcMedife = porcOSDE = porcPAMI = acuTotalMensual = acutTotalAnual = acuTotalMensualTotal = acuTotalAnualTotal = contCardio = contOdonto = contPedia = contMedife = contOSDE = contPAMI = contConsultas = contConsultasConObraSocial = 0;

            // seteamos todos los lbl con resultados a sus valores x defecto

            lblCantCardio.Text = lblCantOdonto.Text = lblCantPedia.Text = lblCantMedife.Text = lblCantOsde.Text = lblCantPAMI.Text = lblEspecialidadMasCara.Text = lblEspecialidadMasBarata.Text = lblPrecioEspecialidadMasBarata.Text = lblPrecioEspecialidadMasCara.Text = lblFechaEspecialidadMasCara.Text = lblFechaEspecialidadMasBarata.Text = lblTotalAnual.Text = lblTotalMensual.Text = lblDescuentoAnual.Text = lblDescuentoMensual.Text = "-";

            lblPorcCardio.Text = lblPorcOdonto.Text = lblPorcPedia.Text = lblPorcMedife.Text = lblPorcOsde.Text = lblPorcPAMI.Text = "%";
        }

        // metodo para verificar si el checkbox de obra social esta seleccionado o no, y ,segun corresponda, habilitar el combo-box de obra social

        private void chkTieneObraSocial_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTieneObraSocial.Checked) cboObraSocial.Enabled = true;
            else cboObraSocial.Enabled = false;
        }

        // metodo para limpiar los campos de ingreso de datos

        private void btnLimpiarCampos_Click(object sender, EventArgs e)
        {
            txtMatricula.Text = txtNombre.Text = txtApellido.Text = txtNroDoc.Text = txtPrecioConsulta.Text = "";
            cboEspecialidad.SelectedIndex = cboObraSocial.SelectedIndex = cboTipoDoc.SelectedIndex = -1;
            rdoMasc.Checked = rdoFem.Checked = rdoOtro.Checked = chkEsCasado.Checked = chkTieneObraSocial.Checked = false;
            dtpFechaConsulta.Value = dtpFechaNac.Value = DateTime.Today;
        }
    }
}