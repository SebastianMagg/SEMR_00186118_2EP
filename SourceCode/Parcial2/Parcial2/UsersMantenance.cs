using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Parcial2
{
    public partial class UsersMantenance : UserControl
    {
        private bool admintype = false;
        
        public UsersMantenance()
        {
            InitializeComponent();
            button2.Enabled = false;
        }
        
        private void UsersMantenance_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton2.Checked = true;
            try
            {
                var dt = ConexionBD.ExecuteQuery("SELECT * FROM APPUSER");
                dataGridView1.DataSource = dt;
                comboBox1_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un problema");
            }
        }
        
        private void comboBox1_Load()
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


        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                admintype = true;
            }
            else if(radioButton2.Checked == true)
            {
                admintype = false;
            }
            
            if (textBox1.Text.Equals("") ||
                textBox2.Text.Equals("") ||
                textBox3.Text.Equals(""))
            {
                MessageBox.Show("No se pueden dejar campos vacios ");
            }
            else
            {
                try
                {
                    ConexionBD.ExecuteNonQuery($"INSERT INTO APPUSER(fullname, username, password, userType) VALUES (" +
                                                 $"'{textBox1.Text}'," +
                                                 $"'{textBox2.Text}'," +
                                                 $"'{textBox3.Text}'," +
                                                 $"'{admintype}')");

                    MessageBox.Show("Se ha registrado el usuario");
                    comboBox1_Load();
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    radioButton2.Checked = true;
                    var dt = ConexionBD.ExecuteQuery("SELECT * FROM APPUSER");
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error");
                    var dt = ConexionBD.ExecuteQuery("SELECT * FROM APPUSER");
                    dataGridView1.DataSource = dt;
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult deleteconfirm;
            
            if (textBox4.Text == "")
            {
                MessageBox.Show("No se pueden dejar el campo vacio ");
            }
            else
            {
                button2.Enabled = true;
                
                deleteconfirm = MessageBox.Show("Se borrara el usuario","Confirmar",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                
                if (deleteconfirm == DialogResult.OK)
                {
                    try
                    {
                        ConexionBD.ExecuteNonQuery("DELETE FROM public.APPUSER WHERE username ="+
                                                     $"'{textBox4.Text}'");
                        
                        var dt = ConexionBD.ExecuteQuery("SELECT * FROM APPUSER");
                        
                        dataGridView1.DataSource = dt;
                        textBox4.Clear();
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        
                        MessageBox.Show("Usuario eliminado","Eliminar usuario", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        
                        comboBox1_Load();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                        throw;
                    }
                }
                else
                {
                    textBox4.Text = "";
                }
            }
        } 
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (comboBox1.Text != "")
            {
                button2.Enabled = true;
                textBox4.Text = comboBox1.SelectedItem.ToString();
            }
            else
            {
                button2.Enabled = false;
                textBox4.Text = "";
            }
        }
    }
}