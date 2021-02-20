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
    public partial class Snake : Form
    {
        List<PictureBox> Lista = new List<PictureBox>();//lista que contendra el Cuerpo del Gusano
        int TamanioPiezaPrincipal = 26, tiempo = 10; //minutos
        PictureBox Comida = new PictureBox();//IMG donde aparecera la Manzan    
        String Direccion = "right"; //NosServira mas adelante como truca visual XD

        public Snake()
        {
            InitializeComponent();
            IniciarJuego();//Meto de Inicial Juego
                
        }
        public void IniciarJuego() //Creando metodo para  Inicar el Juego
        {
            tiempo = 10; //Inicializamos tienpo 
            Direccion = "right";//Inicializamos Direccion hacia donde empesaria a moverse
            timer1.Interval = 200;
            Lista = new List<PictureBox>();
            //Piezas Iniciales
            //Saldran 3 img 0,1,2
            for (int i = 2; 0 <= i; i--)
            {
                CreateSnake(Lista, this, (i * TamanioPiezaPrincipal) + 70, 80);
            }
            CrearComida();
        } 
        //Metodo para la creacion del Gusano 
        public void CreateSnake(List<PictureBox> ListaPelota, Form formulario, int posicionX, int posicionY)
        {
            PictureBox pb = new PictureBox();//Creando la img de body
            pb.Location = new Point(posicionX, posicionY);//Posicion donde Aparecera
            pb.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("Cuerpo");//Busa en los recursos la img llamada Cuerpo
            pb.BackColor = Color.Transparent;
            pb.SizeMode = PictureBoxSizeMode.AutoSize;//Tamañana automaticao
            ListaPelota.Add(pb);//agrega la img a la lista
            formulario.Controls.Add(pb); //Añade el control al Form
        }
        private void CrearComida()
        {
            Random rnd = new Random();
            int enteroX = rnd.Next(1, this.Width - TamanioPiezaPrincipal - 10);//rango de valores y es desd 1 hasta el ancho del form-el punto de inicio -10
            int enteroY = rnd.Next(1, this.Height - TamanioPiezaPrincipal - 40);

            PictureBox pb = new PictureBox(); //Creando la img de comida
            pb.Location = new Point(enteroX, enteroY);//Posicion donde Aparecera
            pb.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("Manzana");//img a buscar
            pb.BackColor = Color.Transparent;
            pb.SizeMode = PictureBoxSizeMode.AutoSize;//Tamañana automaticao
            Comida = pb;//Crea la img
            this.Controls.Add(pb);//Añade el control al Form

        }
        private void Snake_KeyDown(object sender, KeyEventArgs e)// Metodo que  lee las teclas precionadas 
        {
            //si la tecla precionada es arriba entonces dirreccion es UP si no dirreccion;
            Direccion = ((e.KeyCode & Keys.Up) == Keys.Up) ? "up" : Direccion;
            Direccion = ((e.KeyCode & Keys.Down) == Keys.Down) ? "down" : Direccion;
            Direccion = ((e.KeyCode & Keys.Left) == Keys.Left) ? "left" : Direccion;
            Direccion = ((e.KeyCode & Keys.Right) == Keys.Right) ? "right" : Direccion;
            //asi es como la variable cambia de valor y sirve para saber que cabeza Mostrar

        }

        public void ReinicarJuego() 
        { 
            foreach (PictureBox Serpiente in Lista) { this.Controls.Remove(Serpiente); }//Removemos todas las img
            this.Controls.Remove(Comida);//Removemos la comida
            IniciarJuego();//Iniciamos de nuevo
        
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int nx = Lista[0].Location.X;//Obtenemos los Punto de la img creado por el metdo CreateSnake
            int ny = Lista[0].Location.Y;//Obtenemos los Punto de la img creado por el metdo CreateSnake
            Lista[0].Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("Cabeza" + Direccion);//remplazamos la img

            for (int i = Lista.Count-1;i>=0;i--)
            {
                if (i == 0)  // se evalua si la pieza es la img de cabeza
                {
                    if (Direccion == "right") nx = nx + TamanioPiezaPrincipal;// se evalua la dirreccion
                    else if (Direccion == "left") nx = nx - TamanioPiezaPrincipal;
                    else if (Direccion == "up") ny = ny - TamanioPiezaPrincipal;
                    else if (Direccion == "down") ny = ny + TamanioPiezaPrincipal;

                    Lista[0].Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("Cabeza" + Direccion);//remplazamos la img
                    Lista[0].Location = new Point(nx, ny);
                }
                else 
                {
                    //efecto de Transicion, fila anterior por fila nueva
                    Lista[i].Location = new Point((Lista[i - 1].Location.X), (Lista[i].Location.Y));
                    Lista[i].Location = new Point((Lista[i].Location.X), (Lista[i - 1].Location.Y));
                }
            }

            //Detectamos coliiones  con la img
            for (int contarPiezas=1; contarPiezas<Lista.Count; contarPiezas++) // contamos todas las piezas del gusano
                {
                    if(Lista[contarPiezas].Bounds.IntersectsWith(Comida.Bounds))// verifica si hubo uno interseccion
                    {
                        this.Controls.Remove(Comida);//Quitamos el control
                        tiempo = Convert.ToInt32(timer1.Interval);
                        if (tiempo > 10) { timer1.Interval = tiempo - 10; }//Dismunuimos el tiempo
                        lblPuntos.Text=(Convert.ToInt32(lblPuntos.Text) + 1).ToString();// aumentamos los puntos
                        CrearComida();//Creamos una nueva comida(Manzana)
                        CreateSnake(Lista, this, Lista[Lista.Count - 1].Location.X * TamanioPiezaPrincipal, 0);//añanadimos una nueva parte al gusano

                    }
                
                }

            //Dtectamos Colision con pared 
            if ((Lista[0].Location.X>= this.Width-15) || (Lista[0].Location.Y>= this.Height-50) || (Lista[0].Location.Y < -10) || (Lista[0].Location.X < -30))
            {
                // si es mayor que lo  ancho va hacia la  izquierda  
                //si es mayor que el heigth es por que va  hacia arriba
                ReinicarJuego();
            }

            //Dtectamos Colision   entre el cuerpo del gusano 
            for (int contrarPiezas =1;  contrarPiezas< Lista.Count; contrarPiezas++) 
            {
              if (Lista[0].Bounds.IntersectsWith(Lista[contrarPiezas].Bounds)) { ReinicarJuego(); }
            }



        }



    }
}
