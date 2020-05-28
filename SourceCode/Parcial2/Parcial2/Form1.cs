using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parcial2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                var dt = ConexionBD.ExecuteQuery("SELECT * FROM APPUSER");
                ComboBox1_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un problema");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            SiAdmin sia = new SiAdmin();
            NoAdmin noa = new NoAdmin();
            
            try
            {
                string Uquery = $"SELECT userType FROM APPUSER WHERE" +
                                $" username ='{textBox1.Text}'AND " +
                                $" password ='{textBox2.Text}'";

                var dt = ConexionBD.ExecuteQuery(Uquery);
                var dr = dt.Rows[0];
                var admin = Convert.ToString(dr[0].ToString());

                if (admin.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    sia.Show();
                    this.Hide();
                }
                else
                {
                    this.Hide();
                    noa.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Usuario no registrado o los datos ingresados incorrectos", "Error de inicio de sesion",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void ComboBox1_Load()
        {
            try
            {
                var users = ConexionBD.ExecuteQuery("SELECT username FROM APPUSER");
                var usersCombo = new List<string>();
                usersCombo.Add("");
                foreach (DataRow dr in users.Rows)
                {
                    usersCombo.Add(dr[0].ToString());
                }
                comboBox1.DataSource = usersCombo;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                button2.Enabled = true;
                try
                {
                    string Uquery = $"SELECT password FROM APPUSER WHERE" +
                                    $" username ='{comboBox1.SelectedItem.ToString()}'";


                    var dt = ConexionBD.ExecuteQuery(Uquery);
                    var dr = dt.Rows[0];
                    var upass = Convert.ToString(dr[0].ToString());
                    textBox3.Text = comboBox1.SelectedItem.ToString();
                    textBox4.Text = upass;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                textBox3.Text = "";
                button2.Enabled = false;
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Equals(""))
            {
                MessageBox.Show("No se pueden dejar campos vacios ");
            }
            else
            {
                try
                {
                    ConexionBD.ExecuteNonQuery("UPDATE APPUSER SET " +
                                                 $"username= '{textBox3.Text}', " +
                                                 $"password= '{textBox4.Text}' " +
                                                 $"WHERE username= '{comboBox1.SelectedItem.ToString()}'");

                    MessageBox.Show("Se han actualizado los datos del usuario");
                    ComboBox1_Load();
                    textBox3.Clear();
                    textBox4.Clear();
                    var dt = ConexionBD.ExecuteQuery("SELECT * FROM APPUSER");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}