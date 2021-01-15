using System.Collections.Generic;
using EPlayers_AspNetCore.Models;

namespace EPLayers_AspNetCore.Interfaces
{
    public interface IJogador
    {
         //Cração do CRUD

         //Create
         void Create(Jogador j);

         //Read
         List<Jogador> ReadAll();

         //Update
         void Update(Jogador j);

         //Excluir
         void Delete(int id);
    }
}