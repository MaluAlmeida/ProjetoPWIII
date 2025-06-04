using MySql.Data.MySqlClient;
using ProjetoPW.Models;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoPW.Repositorio
 
{

        // Define a classe UsuarioRepositorio, responsável por operações de acesso a dados para a entidade Usuario.
        public class ProdutoRepositorio(IConfiguration configuration)
        {
            // Declara um campo privado somente leitura para armazenar a string de conexão com o MySQL.
            private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");

            public Produto ObterProduto(string nome)
            {
                // Cria uma nova instância da conexão MySQL dentro de um bloco 'using'.
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    // Abre a conexão com o banco de dados MySQL.
                    conexao.Open();
                    // Cria um novo comando SQL para selecionar todos os campos da tabela 'Usuario' onde o campo 'Email' corresponde ao parâmetro fornecido.
                    MySqlCommand cmd = new("SELECT * FROM Produtos WHERE Nome = @nome", conexao);
                    // Adiciona um parâmetro ao comando SQL para o campo 'Email', especificando o tipo como VarChar e utilizando o valor do parâmetro 'email'.
                    cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = nome;

                    // Executa o comando SQL SELECT e obtém um leitor de dados (MySqlDataReader). O CommandBehavior.CloseConnection garante que a conexão
                    // será fechada automaticamente quando o leitor for fechado.
                    using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        // Inicializa uma variável 'usuario' como null. Ela será preenchida se um usuário for encontrado.
                        Produto produto = null;
                        // Lê a próxima linha do resultado da consulta. Retorna true se houver uma linha e false caso contrário.
                        if (dr.Read())
                        {
                            // Cria uma nova instância do objeto 'Usuario'.
                            produto = new Produto
                            {
                                // Lê o valor da coluna "Id" da linha atual do resultado, converte para inteiro e atribui à propriedade 'Id' do objeto 'usuario'.
                                Id = Convert.ToInt32(dr["Id"]), 
                                Nome = dr["Nome"].ToString(), 
                                Descricao = dr["Descricao"].ToString(),
                                Preco = Convert.ToDecimal(dr["Preco"]),
                                Quantidade = Convert.ToInt32(dr["Quantidade"])
                            };
                        }
                        /* Retorna o objeto 'usuario'. Se nenhum usuário foi encontrado com o email fornecido, a variável 'usuario'
                         permanecerá null e será retornado.*/
                        return produto;
                    }
                }
            }

            // Define um método público para adicionar um novo usuário ao banco de dados. Recebe um objeto 'Usuario' como parâmetro.
            public void AdicionarProduto(Produto produto)
            {
                /* Cria uma nova instância da conexão MySQL dentro de um bloco 'using'.
                 Isso garante que a conexão será fechada e descartada corretamente após o uso, mesmo em caso de erro.*/
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    // Abre a conexão com o banco de dados MySQL.

                    conexao.Open();
                    /* Cria um novo comando SQL para inserir dados na tabela 'Usuario'. Os valores para Nome, Email e Senha são passados como parâmetros
                     (@Nome, @Email, @Senha) para evitar SQL Injection.*/

                    MySqlCommand cmd = new("INSERT INTO Usuarios (Nome, Descricao, Preco, Quantidade) VALUES (@Nome, @Descricao, @Preco, @Quantidade)", conexao);
                    // Adiciona um parâmetro ao comando SQL para o campo 'Nome', utilizando o valor da propriedade 'Nome' do objeto 'usuario'.
                    cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                    // Adiciona um parâmetro ao comando SQL para o campo 'Email', utilizando o valor da propriedade 'Email' do objeto 'usuario'.
                    cmd.Parameters.AddWithValue("@Descricao", produto.Descricao);
                    // Adiciona um parâmetro ao comando SQL para o campo 'Senha', utilizando o valor da propriedade 'Senha' do objeto 'usuario'.
                    cmd.Parameters.AddWithValue("@Preco", produto.Preco);
                    cmd.Parameters.AddWithValue("@Quantidade", produto.Quantidade);
                // Executa o comando SQL INSERT no banco de dados. Retorna o número de linhas afetadas.
                cmd.ExecuteNonQuery(); //executa as coisas do banco 
                                           // Fecha a conexão com o banco de dados (embora o 'using' já faria isso, só garante o fechamento).
                    conexao.Close();
                }
            }

        }
    }


