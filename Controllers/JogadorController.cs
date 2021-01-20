

using System;
using EPlayers_AspNetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EPLayers_AspNetCore.Controllers
{
    [Route("Jogador")]
    public class JogadorController : Controller
    {
        Jogador jogadorModel = new Jogador();
        Equipe equipeModel = new Equipe();
        
        // [Route("Listar-Jogadores")]
        public IActionResult Index()
        {
            ViewBag.Username = HttpContext.Session.GetString("_Username");
            ViewBag.Jogadores = jogadorModel.ReadAll();
            ViewBag.Equipes = equipeModel.ReadAll();
            return View();
        }

        [Route("Cadastrar-Jogador")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            Jogador novoJogador     = new Jogador();
            novoJogador.IdJogador   = Int32.Parse(form["IdJogador"]);
            novoJogador.Nome        = form["Nome"];
            novoJogador.Email       = form["Email"];
            novoJogador.Senha       = form["Senha"];

            jogadorModel.Create(novoJogador);            
            ViewBag.Jogadores = jogadorModel.ReadAll();

            return LocalRedirect("~/Jogador");
        }

        [Route("Jogador/{id}")]
        public IActionResult Excluir(int id)
        {
            //Recebe o Id do usuário que está logado
            var userId = HttpContext.Session.GetString("_UserId");

            if(userId == id.ToString())
            {
                //Deletar a equipe
                jogadorModel.Delete(id);
            }

            //Atualizar a lista
            ViewBag.Equipes = jogadorModel.ReadAll();
            return LocalRedirect("~/Jogador");
        }
    }
}