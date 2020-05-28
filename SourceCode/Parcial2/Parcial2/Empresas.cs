using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Parcial2
{
    public partial class Empresas : UserControl
    {
        public Empresas()
        {
            InitializeComponent();
            button2.Enabled = true;
            button4.Enabled = true;
        }
        
        private void Empresas_Load(object sender, EventArgs e)
        {
            
            try
            {
                var dt = ConexionBD.ExecuteQuery("SELECT * FROM BUSINESS");
                textBox1.Clear();
                textBox2.Clear();
                textBox4.Clear();
                comboBox1_Load();
                comboBox2_Load();
                comboBox3_Load();
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
                var users = ConexionBD.ExecuteQuery("SELECT name FROM BUSINESS");
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
            
            if (textBox1.Text.Equals("") ||
                textBox2.Text.Equals(""))
            {
                MessageBox.Show("No se pueden dejar campos vacios ");
            }
            else
            {
                try
                {
                    ConexionBD.ExecuteNonQuery($"INSERT INTO BUSINESS( name, description) VALUES (" +
                                               $"'{textBox1.Text}'," +
                                               $"'{textBox2.Text}')");

                    MessageBox.Show("Se ha registrado la empresa");
                    
                    var dt = ConexionBD.ExecuteQuery("SELECT * FROM PRODUCT");
                    
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox4.Clear();
                    comboBox1_Load();
                    comboBox2_Load();
                    comboBox3_Load();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult deleteconfirm;
            
            if (comboBox1.Text == "")
            {
                MessageBox.Show("No se pueden dejar el campo vacio ");
            }
            else
            {
                button2.Enabled = true;
                
                deleteconfirm = MessageBox.Show("Se borrara la empresa","Confirmar",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                
                if (deleteconfirm == DialogResult.OK)
                {
                    try
                    {
                        var users = ConexionBD.ExecuteQuery("SELECT name FROM BUSINESS");
                        var usersCombo = new List<string>();
                        usersCombo.Add("");
                        foreach (DataRow dr in users.Rows)
                        {
                            usersCombo.Add(dr[0].ToString());
                        }
                        comboBox1.DataSource = usersCombo;

                        string cad = comboBox1.SelectedItem.ToString();
                        
                        ConexionBD.ExecuteNonQuery("DELETE FROM public.BUSINESS WHERE name ="+
                                                   $"'{cad}'");
                        
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox4.Clear();
                        comboBox1_Load();
                        comboBox2_Load();
                        comboBox3_Load();
                        
                        MessageBox.Show("Empresa eliminada","Eliminar empresa", MessageBoxButtons.OK,
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
                    comboBox1.Text = "";
                }
            }
        } 
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (comboBox1.Text != "")
            {
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
        }
        
        
        
        
        
        
        private void comboBox2_Load()
        {
            try
            {
                button4.Enabled = true;
                var users = ConexionBD.ExecuteQuery("SELECT name FROM PRODUCT");
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
        
        private void comboBox3_Load()
        {
            try
            {
                var users = ConexionBD.ExecuteQuery("SELECT name FROM BUSINESS");
                var usersCombo = new List<string>();
                usersCombo.Add("");
                foreach (DataRow dr in users.Rows)
                {
                    usersCombo.Add(dr[0].ToString());
                }
                comboBox3.DataSource = usersCombo;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            
            if (textBox4.Text.Equals(""))
            {
                MessageBox.Show("No se pueden dejar campos vacios ");
            }
            else
            {
                try
                {
                    var num = $"SELECT idBusiness FROM BUSINESS WHERE" +
                              $" name ='{comboBox3.SelectedItem.ToString()}'";
                
                    var dt = ConexionBD.ExecuteQuery(num);
                    var dr = dt.Rows[0];
                    var myNum = int.Parse(dt.Rows[0][0].ToString());

                    ConexionBD.ExecuteNonQuery($"INSERT INTO PRODUCT(idBusiness, name) VALUES (" +
                                               $"{myNum}, " + 
                                               $"'{textBox4.Text}')");

                    MessageBox.Show("Se ha registrado el producto");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox4.Clear();
                    comboBox1_Load();
                    comboBox2_Load();
                    comboBox3_Load();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error");
                }
            }
        }

        
        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult deleteconfirm;
            
            if (comboBox2.Text == "")
            {
                MessageBox.Show("No se pueden dejar el campo vacio ");
            }
            else
            {
                button4.Enabled = true;
                
                deleteconfirm = MessageBox.Show("Se borrara el producto","Confirmar",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                
                if (deleteconfirm == DialogResult.OK)
                {
                    try
                    {
                        var users = ConexionBD.ExecuteQuery("SELECT name FROM PRODUCT");
                        var usersCombo = new List<string>();
                        usersCombo.Add("");
                        foreach (DataRow dr in users.Rows)
                        {
                            usersCombo.Add(dr[0].ToString());
                        }
                        comboBox2.DataSource = usersCombo;

                        string cad = comboBox2.SelectedItem.ToString();
                        
                        ConexionBD.ExecuteNonQuery("DELETE FROM public.PRODUCT WHERE name ="+
                                                   $"'{cad}'");
                        
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox4.Clear();
                        comboBox1_Load();
                        comboBox2_Load();
                        comboBox3_Load();

                        MessageBox.Show("Producto eliminado","Eliminar producto", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                        throw;
                    }
                }
                else
                {
                    comboBox2.Text = "";
                }
            }
        } 
        
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (comboBox2.Text != "")
            {
                button4.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox3.Text != "")
            {
                button3.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
            }
        }
    }
}