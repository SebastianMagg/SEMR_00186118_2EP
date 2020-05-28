using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Parcial2
{
    public partial class Address : UserControl
    {
        private bool admintype = false;
        public Address()
        {
            InitializeComponent();
            button2.Enabled = false;
            button3.Enabled = false;
        }
        
        private void Address_Load(object sender, EventArgs e)
        {
            try
            {
                var dt = ConexionBD.ExecuteQuery("SELECT * FROM ADDRESS");
                dataGridView1.DataSource = dt;
                comboBox1_Load();
                comboBox2_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR");
            }
        }
        
        private void comboBox1_Load()
        {
            try
            {
                var users = ConexionBD.ExecuteQuery("SELECT name FROM ADRRESS");
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
        private void comboBox2_Load()
        {
            try
            {
                var users = ConexionBD.ExecuteQuery("SELECT name FROM ADDRESS");
                var usersCombo = new List<string>();
                usersCombo.Add("");
                foreach (DataRow dr in users.Rows)
                {
                    usersCombo.Add(dr[0].ToString());
                }
                comboBox2.DataSource = usersCombo;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals("") ||
                textBox2.Text.Equals(""))
            {
                MessageBox.Show("No se pueden dejar campos vacios ");
            }
            else
            {
                try
                {
                    ConexionBD.ExecuteNonQuery($"INSERT INTO ADDRESS(address) VALUES (" +
                                               $"'{textBox2.Text}')");

                    MessageBox.Show("Se ha registrado la direccion");
                    comboBox1_Load();
                    comboBox2_Load();
                    textBox1.Clear();
                    textBox2.Clear();
                    var dt = ConexionBD.ExecuteQuery("SELECT * FROM ADDRESS");
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR");
                }
            }
        }
    }
}