using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlanoPilotes.UI
{

    public enum OpcionSeleccion
    {
        Individual,
        Multiple,
        VistaActual,
        Ninguna
    }

    public partial class Form1 : Form
    {
        public OpcionSeleccion OpcionElegida { get; private set; } = OpcionSeleccion.Ninguna;


        public Form1()
        {
            InitializeComponent();

            
            btnSeleccionIndividual.Click += btnSeleccionIndividual_Click;
            btnSeleccionMultiple.Click += btnSeleccionMultiple_Click;
            btnSeleccionTodos.Click += btnSeleccionTodos_Click;
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSeleccionIndividual_Click(object sender, EventArgs e)
        {
            OpcionElegida = OpcionSeleccion.Individual;
            this.Close();
        }

        private void btnSeleccionMultiple_Click(object sender, EventArgs e)
        {
            OpcionElegida = OpcionSeleccion.Multiple;
            this.Close();
        }

        private void btnSeleccionTodos_Click(object sender, EventArgs e)
        {
            OpcionElegida = OpcionSeleccion.VistaActual;
            this.Close();
        }
    }
}
