

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
        
        [Route("Listar-Jogadores")]
        public IActionResult Index()
        {
            ViewBag.Jogadores = jogadorModel.ReadAll();
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

            return LocalRedirect("~/Jogador/Listar-Jogadores");
        }
    }
}