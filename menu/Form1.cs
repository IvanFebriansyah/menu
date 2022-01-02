using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace menu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-ABD7H3A;Initial Catalog=kantinAmikom;Integrated Security=True");
        

        private string CaesarCipher(string value, int shift)
        {
            string[] joinCipher = new string[200];
            string jointext = "";
            string[] wordArray = value.Split(' ');

            try
            {
                for (int x = 0; x < wordArray.Length; x++)
                {
                    char[] buffer = wordArray[x].ToCharArray();
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        char letter = buffer[i];
                        letter = (char)(letter + shift);

                        if (letter > 'z')
                        {
                            letter = (char)(letter - 26);
                        }
                        else if (letter < 'a')
                        {
                            letter = (char)(letter + 26);
                        }

                        buffer[i] = letter;
                    }

                    string HasilKonversi = new string(buffer);
                    joinCipher[x] = HasilKonversi;
                }

                jointext = string.Join(" ", joinCipher);
                return jointext;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" | textBox2.Text == "")
            {
                MessageBox.Show("Semua data harus diisi", "Peringatan");
                goto berhenti;
            }

            string tekscipher = null;
            tekscipher = CaesarCipher(textBox2.Text, 17);

            con.Close();
            SqlCommand cmd = new SqlCommand("SELECT * FROM admin WHERE userid='" + textBox1.Text + "' and password= '" + tekscipher + "'", con);
            con.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {
                rd.Read();
                Home menu = new Home();
                menu.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("User id atau password tidak valid", "Peringatan");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox1.Focus();
                rd.Close();
            }

        berhenti:
            ;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToLower());
        }
    }
}
