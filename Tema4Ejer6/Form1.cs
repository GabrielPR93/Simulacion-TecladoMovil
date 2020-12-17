using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tema4Ejer6
{
    public partial class Form1 : Form
    {
        private Button btn;
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int cont = 0;
            int contraseña = 1234;

            do
            {
                Form2 f = new Form2();
                DialogResult res;
                res = f.ShowDialog();

                switch (res)
                {
                    case DialogResult.OK:
                        try
                        {

                            if ((Convert.ToInt32(f.textBoxContraseña.Text)) == contraseña)
                            {
                                cont = 4;
                                MessageBox.Show("Contraseña Aceptada", "Iniciar Sesión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                cont++;
                                MessageBox.Show("Contraseña Incorrecta", "Iniciar Sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                if (cont == 3)
                                {
                                    this.Close();
                                }
                            }
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Error de Formato", "Iniciar Sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                        catch (OverflowException)
                        {

                            MessageBox.Show("Contraseña demasiado larga", "Iniciar Sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                        break;

                    case DialogResult.Cancel:
                        MessageBox.Show("Inicio Cancelado", "Iniciar Sesión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        cont = 3;
                        this.Close();

                        break;

                }
            } while (cont < 3);
            int punto = 100;
            int punto2 = 70;

            for (int i = 1; i < 13; i++)
            {

                btn = new Button();
                btn.Text = i.ToString();
                btn.Location = new Point(punto, punto2);
                punto += 50;
                if (i == 3 || i == 6 || i == 9)
                {
                    punto2 += 25;
                    punto = 100;
                }
                if (i == 10)
                {
                    btn.Text = "#";
                }
                else if (i == 11)
                {
                    btn.Text = "0";
                }
                else if (i == 12)
                {
                    btn.Text = "*";
                }

                btn.Size = new Size(50, 20);
                btn.Enabled = true;

                this.btn.Click += new System.EventHandler(this.btnClick);
                this.btn.MouseLeave += this.btnSalir;
                this.btn.MouseEnter += this.btnMove;
                this.Controls.Add(btn);

            }

        }

        void btnClick(object sender, System.EventArgs e)
        {

            textBox1.Text += ((Button)sender).Text;
            ((Button)sender).BackColor = Color.Red;
        }

        void btnMove(object sender, EventArgs e)
        {
            if (((Button)sender).BackColor != Color.Red)
            {
                ((Button)sender).BackColor = Color.DarkCyan;

            }
        }
        void btnSalir(object sender, EventArgs e)
        {

            if (((Button)sender).BackColor != Color.Red)
            {
                ((Button)sender).BackColor = default;
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";

            foreach (Control boton in this.Controls)
            {
                if (boton.GetType() == typeof(Button)) //Si es un boton cambiamos el color a default;
                {
                    boton.BackColor = default;
                }
            }
        }

        private void SalirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ResetToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = "";
            foreach (Control boton in this.Controls)
            {
                if (boton.GetType() == typeof(Button))
                {
                    boton.BackColor = default;
                   
                }
            }
        }

        private void GuardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res2;
            StreamWriter sr;
            this.saveFileDialog1.Title = "Selecciona directorio de almacenado";
            this.saveFileDialog1.InitialDirectory = "C:\\";
            this.saveFileDialog1.Filter = "texto(*.txt)|*.txt|Todos los archivos|*.*";
            this.saveFileDialog1.ValidateNames = true;
            res2 = this.saveFileDialog1.ShowDialog();

            switch (res2)
            {
                case DialogResult.OK:
                    using (sr = new StreamWriter(this.saveFileDialog1.FileName))
                    {
                        if (textBox1.Text.Length > 0)
                        {
                            sr.Write(textBox1.Text);
                        }
                    }
                    break;
                case DialogResult.Cancel:
                    MessageBox.Show("Operacion cancelada", "Guardado Cancelado", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    break;

            }
        }
    }
}
