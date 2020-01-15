using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            KeyPreview = true;
            comprobarMovimientos();
        }

        readonly static int maxSize = 50;
        string[,] DirImagen = new string[maxSize, 5];
        int numImagenAUsar = 0;
        bool sig = false, ant = false, res = false, eli = false, ima = false;
        string Tip1= "Está visualizando la imagen ", Tip2= "La imagen seleccionada es ";
        void sustituir(int pos)
        {
            DirImagen[pos, 0] = DirImagen[pos + 1, 0];
            DirImagen[pos, 1] = DirImagen[pos + 1, 1];
            DirImagen[pos, 2] = DirImagen[pos + 1, 2];
            DirImagen[pos, 3] = DirImagen[pos + 1, 3];
            DirImagen[pos, 4] = DirImagen[pos + 1, 4];
        }

        void VerDatos(int pos)
        {
            textBox1.Text = DirImagen[pos, 0];
            textBox2.Text = DirImagen[pos, 1];
            textBox3.Text = DirImagen[pos, 2];
            textBox4.Text = DirImagen[pos, 3];
            textBox5.Text = DirImagen[pos, 4];

            Image imagencargada = Image.FromFile(DirImagen[pos, 1]);
            pictureBox2.Image = imagencargada;
            this.Text = DirImagen[pos, 0].Replace(DirImagen[pos, 2], " ") + "- Visor";
            toolTip1.SetToolTip(pictureBox2, Tip1 + DirImagen[pos, 0]);
            toolTip1.SetToolTip(listBox1, Tip2 + DirImagen[pos, 0]);
        }
        void eliminar() {
            if (listBox1.Items.Count > 0)
            {
                DialogResult di = MessageBox.Show("Qiere borrar la imagen?", "Borrar Imagen", MessageBoxButtons.YesNo);

                if (di == DialogResult.Yes)
                {
                    //Borrar imagen
                    for (int i = listBox1.SelectedIndex; i < maxSize - 1; i++)
                    {
                        sustituir(i);
                    }
                    int index = listBox1.SelectedIndex;
                    listBox1.Items.RemoveAt(index);
                    int count = listBox1.Items.Count;

                    if (index==0 && count != 0)
                        listBox1.SelectedIndex = index;
                    else if (count != 0)
                        listBox1.SelectedIndex = index - 1;
                    if (count == 0)
                        limpiar();

                    numImagenAUsar -= 1;

                }
            }
        }

        void cargar() {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string dir = openFileDialog1.FileName;
                if (comprobarImagen(openFileDialog1.SafeFileName))
                    MessageBox.Show("Ya esta en la lista tal archivo", "Repetición");
                else { 
                    int index = dir.LastIndexOf("\\");
                    textBox2.Text = dir;
                    DirImagen[numImagenAUsar, 1] = dir;
                    DirImagen[numImagenAUsar, 0] = dir.Substring(index + 1, dir.Length - index - 1);
                    DirImagen[numImagenAUsar, 2] = dir.Substring(dir.Length - 4, 4);
                    DirImagen[numImagenAUsar, 3] = new System.IO.FileInfo(dir).Length.ToString();

                    Image imagencargada = Image.FromFile(dir);

                    pictureBox2.Image = imagencargada;

                    DirImagen[numImagenAUsar, 4] = imagencargada.Width + " x " + imagencargada.Height;

                    listBox1.Items.Add(DirImagen[numImagenAUsar, 0]);

                    VerDatos(numImagenAUsar);

                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                    numImagenAUsar += 1;
                }
            }
        }

        void limpiar() {
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            textBox5.Text = null;
            pictureBox2.Image = null;
            comprobarMovimientos();
            this.Text = "Y - Visor";
        }

        void salir() {
            DialogResult di = MessageBox.Show("Qiere salir?", "Salir", MessageBoxButtons.YesNo);

            if (di == DialogResult.Yes)
                Application.Exit();
        }
        void siguiente() {
            if (sig)
            if (listBox1.SelectedIndex < listBox1.Items.Count - 1)
                listBox1.SelectedIndex += 1;
        }
        void anterior() {
            if (ant)
            if (listBox1.SelectedIndex > 0)
                listBox1.SelectedIndex -= 1;
        }
        void comprobarMovimientos() {
            int count = listBox1.Items.Count;
            int index = listBox1.SelectedIndex;
            res = true;
            eli = true;
            ima = true;
            if (count == 0)
            {
                sig = false;
                ant = false;
                res = false;
                eli = false;
                ima = false;
            }
            else if (index <= 0)
            {
                ant = false;
                if (count > 1)
                    sig = true;
                else
                    sig = false;
            }
            else if (index >= count - 1)
            {
                if (count > 1)
                    ant = true;
                else
                    ant = false;
                sig = false;
            }
            else {
                ant = true;
                sig = true;
            }

            siguienteToolStripMenuItem.Enabled = sig;
            siguienteToolStripMenuItem.Visible = sig;

            anteriorToolStripMenuItem.Enabled = ant;
            anteriorToolStripMenuItem.Visible = ant;

            resetToolStripMenuItem.Enabled =res;
            resetToolStripMenuItem.Visible =res;

            eliminarToolStripMenuItem.Enabled = eli;
            eliminarToolStripMenuItem.Visible = eli;

            imagenToolStripMenuItem.Visible =ima;
        }
        bool comprobarImagen(string nom) {
            for (int i = 0; i < numImagenAUsar; i++) {
                if (DirImagen[i, 0] == nom)
                    return true;
            }
            return false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            cargar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            salir();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Al cambiar el apartado elegido
            if (listBox1.SelectedIndex != -1)
            {
                int select = listBox1.SelectedIndex;
                VerDatos(select);
                //cargar 
                comprobarMovimientos();
            }
        }

        void reset() {
            if (res) {
                DialogResult di = MessageBox.Show("Limpiar la lista?", "Reset", MessageBoxButtons.YesNo);

                if (di == DialogResult.Yes) {
                    listBox1.Items.Clear();
                    numImagenAUsar = 0;
                    comprobarMovimientos();
                    toolTip1.SetToolTip(pictureBox2, "No se representa ninguna imagen");
                    toolTip1.SetToolTip(listBox1, "No está elegida ninguna imagen");
                }
            }
        }
        private void cargarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cargar();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void siguienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            siguiente();
        }

        private void anteriorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            anterior();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar) {
                case 's': siguiente(); break;
                case 'w': anterior(); break;
                case 'e': eliminar(); break;
                case 'c': cargar(); break;
                case 'r': reset(); break;
                case 'q': salir(); break;
            }
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reset();
        }
    }
}
