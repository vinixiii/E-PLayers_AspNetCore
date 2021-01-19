using System;
using System.Collections.Generic;
using System.IO;
using EPLayers_AspNetCore.Interfaces;

namespace EPlayers_AspNetCore.Models
{
    public class Jogador : EplayersBase , IJogador
    {
        public int IdJogador { get; set; }
        public string Nome { get; set; }
        public int IdEquipe { get; set; }

        //Login
        public string Email { get; set; }
        public string Senha { get; set; }

        public const string PATH = "Database/Jogador.csv";

        public string _PATH {
            get{return PATH;}
        }

        public Jogador()
        {
            CreateFolderAndFile(PATH);
        }

        public string PrepareLine(Jogador j)
        {
            return $"{j.IdJogador};{j.Nome};{j.IdEquipe};{j.Email};{j.Senha}";
        }

        public void Create(Jogador j)
        {
            string[] linhas = {PrepareLine(j)};
            File.AppendAllLines(PATH, linhas);
        }

        public List<Jogador> ReadAll()
        {
            List<Jogador> jogadores = new List<Jogador>();
            
            string[] linhas = File.ReadAllLines(PATH);

            foreach (var item in linhas)
            {
                string[] linha = item.Split(";");

                Jogador jogador = new Jogador();
                jogador.IdJogador = Int32.Parse(linha[0]);
                jogador.Nome = linha[1];
                jogador.IdEquipe = Int32.Parse(linha[2]);
                jogador.Email = linha[3];
                jogador.Senha = linha[4];

                jogadores.Add(jogador);
            }
            return jogadores;
        }

        public void Update(Jogador j)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);
            linhas.RemoveAll(x => x.Split(";")[0] == j.IdJogador.ToString());
            linhas.Add(PrepareLine(j));
            RewriteCSV(PATH, linhas);
        }

        public void Delete(int id)
        {
            //Pega todas as linhas do arquivo passado pelo PATH
            List<string> linhas = ReadAllLinesCSV(PATH);
            linhas.RemoveAll(x => x.Split(";")[0] == id.ToString());
            RewriteCSV(PATH, linhas);
        }
    }
}