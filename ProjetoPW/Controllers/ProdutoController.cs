using Microsoft.AspNetCore.Mvc;
    using ProjetoPW.Models;
    using ProjetoPW.Repositorio;

    namespace ProjetoPW.Controllers
    {
        public class ProdutoController : Controller
        {
            /*DECLARANDO UMA VARIAVEL PRIVADA SOMENTE PARA LEITURA DO TIPO UsuarioRepositorio chamada
            _usuarioRepositorio */
            private readonly ProdutoRepositorio _produtoRepositorio;

            /* Definindpo o construtor da classe usuário que vai receber uma instância
             de UsuarioRepositorio*/
            public ProdutoController(ProdutoRepositorio produtoRepositorio)
            {
                _produtoRepositorio = produtoRepositorio;
            }
  
            public IActionResult CadastrarProd()
            {
                // Retorna a View  Cadastro.
                return View();
            }

            [HttpPost]
            public IActionResult CadastrarProd(Produto produto)
            {
                // Verifica se o ModelState é válido. O ModelState é considerado válido se não houver erros de validação.
                if (ModelState.IsValid)
                {
                    /* Se o modelo for válido:
                     Chama o método AdicionarUsuario do _usuarioRepositorio, passando o objeto Usuario recebido.
                     Isso  salvará as informações do novo usuário no banco de dados.*/

                    _produtoRepositorio.AdicionarProduto(produto);

                    /* Redireciona o usuário para a action "Login" deste mesmo Controller (LoginController).
                      após um cadastro bem-sucedido, redirecionará à página de login.*/
                    return RedirectToAction("Cadastro");
                }

                /* Se o ModelState não for válido (houver erros de validação):
                 Retorna a View de Cadastro novamente, passando o objeto Usuario com os erros de validação.
                 Isso permite que a View exiba os erros para o usuário corrigir o formulário.*/
                return View(produto);

            }

        }
    }
