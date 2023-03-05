using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ProjetoCRUDCadastroForm
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\mathe\\OneDrive\\Documentos\\Pessoa.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        int ID = 0;
        public Form1()
        {
            InitializeComponent();
            ExibirDados();
        }

        private void ExibirDados()
        {
            try
            {
                con.Open();
                DataTable dt = new DataTable();
                adapt = new SqlDataAdapter("SELECT * FROM Pessoa", con);
                adapt.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtEmail.Text = "";
            maskedTextCpf.Text = "";
            maskedTextTel.Text = "";
            txtName.Focus();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtEmail.Text != ""  && maskedTextTel.Text != "" && maskedTextCpf.Text != "")
            {
                try
                {
                    cmd = new SqlCommand("INSERT INTO Pessoa(nome,email,telefone,cpf) VALUES(@nome,@email,@telefone,@cpf)", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@nome", txtName.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text.ToUpper());                    
                    cmd.Parameters.AddWithValue("@telefone", maskedTextTel.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@cpf", maskedTextCpf.Text.ToLower());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registro incluído com sucesso...");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro : " + ex.Message);
                }
                finally
                {   
                    con.Close();
                    ExibirDados();
                    ///LimparDados();
                }
            }
            else
            {
                MessageBox.Show("Informe todos os dados requeridos");
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtEmail.Text != "" && maskedTextTel.Text != "" && maskedTextCpf.Text != "")
            {
                try
                {
                    cmd = new SqlCommand("UPDATE Pessoa SET nome=@nome,email=@email,telefone=@telefone,cpf=@cpf WHERE id=@id", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", ID);
                    cmd.Parameters.AddWithValue("@nome", txtName.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@telefone", maskedTextTel.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@cpf", maskedTextCpf.Text.ToLower());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registro atualizado com sucesso...");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro : " + ex.Message);
                }
                finally
                {
                    con.Close();
                    ExibirDados();
                    //LimparDados();
                }
            }
            else
            {
                MessageBox.Show("Informe todos os dados requeridos");
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                if (MessageBox.Show("Deseja Deletar este registro ?", "Pessoa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        cmd = new SqlCommand("DELETE Pessoa WHERE id=@id", con);
                        con.Open();
                        cmd.Parameters.AddWithValue("@id", ID);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("registro deletado com sucesso...!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro : " + ex.Message);
                    }
                    finally
                    {
                        con.Close();
                        ExibirDados();
                        //LimparDados();
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione um registro para deletar");
            }
        }
    }
}
