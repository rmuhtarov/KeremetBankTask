using DataReader.EF;
using DataReader.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataReader.Repository
{
    public class ClientRepository: IClientRepository
    {
        private ClientDbContext context { get; set; }
        public ClientRepository(ClientDbContext dbContext)
        {
            context = dbContext;
        }
        public IEnumerable<Client> GetClients()
        {
            return context.Clients.ToList();
        }

        public Client GetClientById(int clientId)
        {
            return context.Clients.Find(clientId);
        }

        public Client GetClientByINN(ulong clientINN)
        {
            return context.Clients.FirstOrDefault(c=> c.SocialNumber == clientINN);
        }

        public void InsertClient(Client client)
        {
            context.Clients.Add(client);
        }

        public void UpdateClient(Client client)
        {
            context.Entry(client).State = EntityState.Modified;
        }

        public void DeleteClient(int clientId)
        {
            Client client = context.Clients.Find(clientId);
            context.Clients.Remove(client);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
