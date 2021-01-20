using System.Collections.Generic;
using EPlayers_AspNetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_PLayers_AspNetCore.Controllers
{
    [Route("Login")]
    public class LoginController : Controller
    {
        [TempData]
        public string Mensagem { get; set; }

        Jogador jogadorModel = new Jogador();
        
        public IActionResult Index()
        {
            return View();
        }

        [Route("Logar")]
        public IActionResult Logar(IFormCollection form)
        {
        // Lemos todos os arquivos do CSV
        List<string> csv = jogadorModel.ReadAllLinesCSV("Database/Jogador.csv");

        // Verificamos se as informações passadas existe na lista de string
        var logado = 
        csv.Find(
            x => 
            x.Split(";")[3] == form["Email"] && 
            x.Split(";")[4] == form["Senha"]
        );


        // Redirecionamos o usuário logado caso encontrado
        if(logado != null)
        {
            //Criamos uma sessão com os dados do usuário
            HttpContext.Session.SetString("_Username", logado.Split(";")[1]);
            HttpContext.Session.SetString("_UserId", logado.Split(";")[0]);

            return LocalRedirect("~/");
        }

        Mensagem = "Dados incorretos, tente novamente...";
        return LocalRedirect("~/Login");
        }
        
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("_Username");
            return LocalRedirect("~/");
        }
    }
}