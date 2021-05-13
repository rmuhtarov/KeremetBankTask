using DataReader.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataReader.Repository
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetClients();
        Client GetClientById(int clientId);
        Client GetClientByINN(ulong clientINN);
        void InsertClient(Client client);
        void UpdateClient(Client client);
        void DeleteClient(int customerId);
        void Save();
    }
}
