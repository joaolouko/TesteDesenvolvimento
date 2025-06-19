using System;
using System.Windows.Forms;
using Npgsql;

namespace WinFormsDemo
{
    public partial class Main : Form
    {
        string connection = "Host=localhost;Port=5432;Username=postgres;Password=13795272;Database=devTeste";
        public Main()
        {
            InitializeComponent();
        }
       

        private void button1_Click(object sender, EventArgs e)
        {
            string texto = textBox1.Text;
            string numeroStr = textBox2.Text;

            if (string.IsNullOrEmpty(texto) || string.IsNullOrEmpty(numeroStr))
            {
                MessageBox.Show("Há campos vazios");
                return;
            }

            if (!int.TryParse(numeroStr, out int numero))
            {
                MessageBox.Show("Número invalido.");
                return;
            }

            using (NpgsqlConnection conn = new NpgsqlConnection(connection))
            {
                try
                {
                    conn.Open();
                    string sql = "insert into cadastro (texto, numero) values (@texto, @numero)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@texto", texto);
                        cmd.Parameters.AddWithValue("@numero", numero);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Registro inserido com sucesso!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao salvar: " + ex.Message);
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string texto = textBox1.Text;
            string numeroStr = textBox2.Text;

            if (string.IsNullOrEmpty(texto) || string.IsNullOrEmpty(numeroStr))
            {
                MessageBox.Show("Preencha todos os campos");
                return;
            }

            if (!int.TryParse(numeroStr, out int numero))
            {
                MessageBox.Show("Número inválido");
                return;
            }

            using (NpgsqlConnection conn = new NpgsqlConnection(connection))
            {
                try
                {
                    conn.Open();
                    string sql = "UPDATE cadastro SET texto = @texto WHERE numero = @numero";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@texto", texto);
                        cmd.Parameters.AddWithValue("@numero", numero);
                        int linhasAfetadas = cmd.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                            MessageBox.Show("Registro atualizado com sucesso!");
                        else
                            MessageBox.Show("Nenhum registro encontrado para atualizar.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao atualizar: " + ex.Message);
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string numeroStr = textBox2.Text;

            if (string.IsNullOrEmpty(numeroStr))
            {
                MessageBox.Show("Informe o número para deletar.");
                return;
            }

            if (!int.TryParse(numeroStr, out int numero))
            {
                MessageBox.Show("Número inválido.");
                return;
            }

            using (NpgsqlConnection conn = new NpgsqlConnection(connection))
            {
                try
                {
                    conn.Open();
                    string sql = "DELETE FROM cadastro WHERE numero = @numero";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@numero", numero);
                        int linhasAfetadas = cmd.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                            MessageBox.Show("Registro deletado com sucesso!");
                        else
                            MessageBox.Show("Nenhum registro encontrado para deletar.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao deletar: " + ex.Message);
                }
            }

        }

        
    }
}
