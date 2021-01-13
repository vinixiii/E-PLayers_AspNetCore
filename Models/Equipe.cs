using System.Collections.Generic;
using System.IO;
using EPlayers_AspNetCore.Interfaces;

namespace EPlayers_AspNetCore.Models
{
    // Para herdar uma interface junto com outra classe, utiliza-se a vírgula. Lembrando que a classe tem que vir antes da interface 
    public class Equipe : EplayersBase , IEquipe
    {
        public int IdEquipe { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }

        private const string PATH = "Database/Equipe.csv";

        public Equipe() 
        {
            CreateFolderAndFile(PATH);
        }

        //Criamos um método para preparar a linha do CSV
        //Esse método é responsável por transformar o objeto "Equipe" em uma linha de arquivo CSV
        public string Prepare(Equipe e)
        {
            return $"{e.IdEquipe};{e.Nome};{e.Imagem}";
        }

        public void Create(Equipe e)
        {
            //Este array é responsável por receber as linhas do método Prepare
            string[] linhas = {Prepare(e)};

            //AppendAllLines pede um path e um array de strings
            File.AppendAllLines(PATH, linhas);
        }

        public void Delete(int id)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);

            linhas.RemoveAll(x => x.Split(";")[0] == id.ToString());

            RewriteCSV(PATH, linhas);
        }

        public List<Equipe> ReadAll()
        {
            List<Equipe> equipes = new List<Equipe>();

            //Responsável por ler todas as linhas do arquivo CSV
            string[] linhas = File.ReadAllLines(PATH);

            foreach (string item in linhas)
            {
                //Exemplo de linha: 1;Cloud 9;cloud9.jpg

                //Quebra a linha sempre que achar um ponto e vírgula (;), ou seja, cada elemento da linha irá para um índice no array 
                string[] linha = item.Split(";");

                Equipe novaEquipe = new Equipe();
                novaEquipe.IdEquipe = int.Parse(linha[0]);
                novaEquipe.Nome = linha[1];
                novaEquipe.Imagem = linha[2];

                equipes.Add(novaEquipe);
            }

            return equipes;
        }

        public void Update(Equipe e)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);

            linhas.RemoveAll(x => x.Split(";")[0] == e.IdEquipe.ToString());

            linhas.Add(Prepare(e));

            RewriteCSV(PATH, linhas);
        }
    }
}