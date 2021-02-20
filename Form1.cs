using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ejercicio3
{
    public partial class Form1 : Form
    {
        enum Posicion 
        {
        izquierda,derecha,arriba,abajo
        }
        private int x;
        private int y;
        private Posicion Objposicion;
        public Form1()
        {
            InitializeComponent();
            x = 50;
            y = 50;
            Objposicion = Posicion.abajo;
        }

        private void timermov_Tick(object sender, EventArgs e)
        {
            if(Objposicion==Posicion.derecha)
            { x += 10; }
            else if (Objposicion==Posicion.izquierda)
            { x -= 10; }
            else if (Objposicion==Posicion.arriba)
            { y -= 10; }
            else if (Objposicion == Posicion.abajo) 
            { y += 10; }

            Invalidate();
           
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(new Bitmap("img/birrete.png"), x, y, 65, 65);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Left)
            { 
                Objposicion = Posicion.izquierda;
            }
            else if (e.KeyCode == Keys.Right )
            {
                Objposicion = Posicion.derecha;
            }
            else if (e.KeyCode == Keys.Up)
            {
                Objposicion = Posicion.arriba;
            }
            else if (e.KeyCode == Keys.Down)
            {
                Objposicion = Posicion.abajo;
            }
        }
    }
}
