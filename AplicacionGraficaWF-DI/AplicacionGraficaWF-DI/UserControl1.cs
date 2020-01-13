using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplicacionGraficaWF_DI
{
    public partial class UserControl1: UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }
        readonly static int maxSize = 50;
        string[,] DirImagen = new string[maxSize, 5];
        int numImagenAUsar = 0;
        private void EliminarButton_Click(object sender, EventArgs e)
        {
            if (listBox2.Items.Count > 0) { 
                DialogResult di = MessageBox.Show("Qiere borrar la imagen?", "Borrar Imagen", MessageBoxButtons.YesNo);

                if (di == DialogResult.Yes)
                {
                    //Borrar imagen
                    for (int i = listBox2.SelectedIndex; i < maxSize-1; i++)
                    {
                        sustituir(i);
                    }

                    listBox2.Items.RemoveAt(listBox2.SelectedIndex);


                    numImagenAUsar -= 1;

                }
            }
        }
        void sustituir(int pos) {
            DirImagen[pos, 0] = DirImagen[pos + 1, 0];
            DirImagen[pos, 1] = DirImagen[pos + 1, 1];
            DirImagen[pos, 2] = DirImagen[pos + 1, 2];
            DirImagen[pos, 3] = DirImagen[pos + 1, 3];
            DirImagen[pos, 4] = DirImagen[pos + 1, 4];
        }

        private void CargarButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                string dir= openFileDialog1.FileName;
                int index = dir.LastIndexOf("\\");
                textBox2.Text = dir;
                DirImagen[numImagenAUsar,1] = dir;
                DirImagen[numImagenAUsar,0] = dir.Substring(index + 1, dir.Length - index - 1);
                DirImagen[numImagenAUsar,2] = dir.Substring(dir.Length - 4, 4);
                DirImagen[numImagenAUsar,3] = "add size";
                DirImagen[numImagenAUsar,4] = "add dimensions";

                listBox2.Items.Add(DirImagen[numImagenAUsar,0]);

                VerDatos(numImagenAUsar);

            }


            listBox2.SelectedIndex =listBox2.Items.Count - 1;
            numImagenAUsar += 1;
        }

        void VerDatos(int pos) {
            textBox1.Text = DirImagen[pos,0];
            textBox2.Text = DirImagen[pos,1];
            textBox3.Text = DirImagen[pos,2];
            textBox4.Text = DirImagen[pos,3];
            textBox5.Text = DirImagen[pos,4];
        }
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult di = MessageBox.Show("Qiere salir?","Salir",MessageBoxButtons.YesNo);
            
            if(di==DialogResult.Yes)
                Application.Exit();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Al cambiar el apartado elegido
            if (listBox2.SelectedIndex != -1){ 
                int select = listBox2.SelectedIndex;
                VerDatos(select);
                //cargar 
            }
        }
    }
}
