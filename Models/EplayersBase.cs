using System.Collections.Generic;
using System.IO;

namespace EPlayers_AspNetCore.Models
{
    public class EplayersBase
    {
        public void CreateFolderAndFile(string path)
        {
            //Database/Equipe.csv
            string folder = path.Split("/")[0];

            if(!Directory.Exists(folder)) {
                Directory.CreateDirectory(folder);
            }

            if(!File.Exists(path)) {
                File.Create(path);
            }
        }

        public List<string> ReadAllLinesCSV(string path)
        {
            List<string> linhas = new List<string>();

            //É responsável por abrir e fechar um arquivo automáticamente
            using(StreamReader file = new StreamReader(path))
            {
                string linha;

                //Percorres as linhas com o laço while
                while((linha = file.ReadLine()) != null) {
                    linhas.Add(linha);
                }
            }

            return linhas;
        }

        public void RewriteCSV(string path, List<string> linhas)
        {
            using(StreamWriter output = new StreamWriter(path))
            {
                foreach (var item in linhas)
                {
                    output.Write(item + "\n");
                }
            }
        }
    }
}