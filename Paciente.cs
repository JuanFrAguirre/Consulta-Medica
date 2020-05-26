using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stream_25_05
{
    internal class Paciente
    {
        // Propiedades (auto-implementadas)

        public int Matricula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int TipoDoc { get; set; }
        public int NroDoc { get; set; }
        public int Sexo { get; set; }
        public DateTime FechaNac { get; set; }
        public bool EsCasado { get; set; }
        public bool TieneObraSocial { get; set; }
        public int ObraSocial { get; set; }
        public int EspecialidadConsulta { get; set; }
        public double PrecioConsulta { get; set; }
        public DateTime FechaConsulta { get; set; }

        // --------------------------------------------------------------------------
        // Constructores

        public Paciente()
        {
            Matricula = 0;
            Nombre = "";
            Apellido = "";
            TipoDoc = 0;
            NroDoc = 0;
            Sexo = 0;
            FechaNac = DateTime.Today;
            EsCasado = false;
            TieneObraSocial = false;
            ObraSocial = 0;
            EspecialidadConsulta = 0;
            FechaConsulta = DateTime.Today;
        }

        public Paciente(int matricula, string nombre, string apellido, int tipoDoc, int nroDoc, int sexo, DateTime fechaNac, bool esCasado, bool tieneObraSocial, int obraSocial, int especialidadConsulta, double precioConsulta, DateTime fechaConsulta)
        {
            Matricula = matricula;
            Nombre = nombre;
            Apellido = apellido;
            TipoDoc = tipoDoc;
            NroDoc = nroDoc;
            Sexo = sexo;
            FechaNac = fechaNac;
            EsCasado = esCasado;
            TieneObraSocial = tieneObraSocial;
            ObraSocial = obraSocial;
            EspecialidadConsulta = especialidadConsulta;
            PrecioConsulta = precioConsulta;
            FechaConsulta = fechaConsulta;
        }

        // --------------------------------------------------------------------------
        // Metodos

        public string EspecialidadToString()
        {
            switch (EspecialidadConsulta)
            {
                case 1: return "Cardiologia";
                case 2: return "Odontologia";
                case 3: return "Pediatria";
                default: return "No ha seleccionado una especialidad valida";
            }
        }

        public string TipoDocumentoToString()
        {
            switch (TipoDoc)
            {
                case 1: return "DNI";
                case 2: return "LE";
                case 3: return "LC";
                case 4: return "Pasaporte";
                default: return "No ha seleccionado un tipo de documento valido";
            }
        }

        public string ObraSocialToString()
        {
            switch (ObraSocial)
            {
                case 1: return "Medife";
                case 2: return "OSDE";
                case 3: return "PAMI";
                default: return "No ha seleccionado una obra social valida";
            }
        }

        public string SexoToString()
        {
            switch (Sexo)
            {
                case 1: return "Masculino";
                case 2: return "Femenino";
                default: return "No Binario";
            }
        }

        public string EsCasadoToString()
        {
            if (EsCasado) return "Casado";
            return "Soltero";
        }

        public string MostrarDatos()
        {
            string mensaje = $"Alta registrada satisfactoriamente\n" +
                $"---------------------------------------\n\n" +
                $"     Datos paciente:\n" +
                $"    -----------------\n\n" +
                $"Nombre completo:\n" +
                $"{Nombre} {Apellido}\n\n" +
                $"Tipo y Numero de Documento:\n" +
                $"{TipoDocumentoToString()}: {NroDoc}\n\n" +
                $"Sexo y Fecha Nacimiento:\n" +
                $"{SexoToString()}  -  {FechaNac.ToShortDateString()}\n\n" +
                $"Estado civil: \n" +
                $"{EsCasadoToString()}\n\n";
            if (TieneObraSocial)
            {
                mensaje += $"Obra Social:\n" +
                    $"{ObraSocialToString()}\n\n";
            }
            mensaje += $"\n     Datos consulta:\n" +
                $"    ----------------\n\n" +
                $"Especialidad de la consulta:\n" +
                $"{EspecialidadToString()}\n\n" +
                $"Precio y fecha de la consulta:\n" +
                $"${PrecioConsulta}  -  {FechaConsulta.ToShortDateString()}\n\n" +
                $"---------------------------------------";
            return mensaje;
        }
    }
}