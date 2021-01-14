using System;
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
            novaEquipe.Imagem = form["Imagem"];

            //Cadastramos a equipe no CSV
            equipeModel.Create(novaEquipe);
            ViewBag.Equipes = equipeModel.ReadAll();

            return LocalRedirect("~/Equipe/Listar");
        }
    }
}