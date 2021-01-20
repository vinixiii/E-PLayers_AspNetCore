using System;
using System.IO;
using EPlayers_AspNetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_PLayers_AspNetCore.Controllers
{
    //Rotas ou Endpoints é o endereço de onde a página está
    //Neste caso a rota será: http://localhost:5000/Equipe
    [Route("Equipe")]
    public class EquipeController : Controller
    {
        //Criamos uma instância da equipeModel
        Equipe equipeModel = new Equipe();

        //Tudo que estiver dentro desse método terá ligação com a página Index. Quando utilizamos esse recurso, indicamos qual página/endpoint estamos trabalhando, nesse caso, Index

        //IActionResult sempre retornará alguma ação, pode ser um status code, uma informação, ou até mesmo uma tela

        [Route("Listar")]
        public IActionResult Index()
        {
            ViewBag.Username = HttpContext.Session.GetString("_Username");
            //ViewBag é como se fosse um pacote de informações que será enviado para a View. Sendo assim, listamos todas as equipes e enviamos para a View, através da ViewBag
            ViewBag.Equipes = equipeModel.ReadAll();
            //Este retorno de View é justamente a página Index
            return View();
        }

        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            Equipe novaEquipe = new Equipe();
            novaEquipe.IdEquipe = Int32.Parse(form["IdEquipe"]);
            novaEquipe.Nome = form["Nome"];
            //Início do Upload de Imagem

            //Todo elemento do tipo IFormCollection tem a propriedade File que identifica o upload de arquivos. Essa propriedade contém índices assim como um array.
            //Verificamos se o usuário selecionou um arquvio
            if (form.Files.Count > 0)
            {
                //Recebemos o arquivo que o usuário enviou e armazenamos na variável file
                var file = form.Files[0];
                //Indicando o caminho em que a pasta será criada
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

                //Verificamos se o diretório (pasta) existe, se não existe, será criada!
                if(!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                //                      localhost:5001                                  Equipes   imagem.png        
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", folder, file.FileName);

                //Utilizaremos o using para abrir e fechar o documetno automaticamente
                //FileStream permite a manipulação de arquivos
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                novaEquipe.Imagem = file.FileName;
            } else {
                novaEquipe.Imagem = "padrao.png";
            }
            //Final do Upload de Imagem

            //Cadastramos a equipe no CSV
            equipeModel.Create(novaEquipe);
            ViewBag.Equipes = equipeModel.ReadAll();

            return LocalRedirect("~/Equipe/Listar");
        }

        [Route("{id}")]
        public IActionResult Excluir(int id)
        {
            //Deletar a equipe
            equipeModel.Delete(id);
            //Atualizar a lista
            ViewBag.Equipes = equipeModel.ReadAll();
            return LocalRedirect("~/Equipe/Listar");
        }
    }
}