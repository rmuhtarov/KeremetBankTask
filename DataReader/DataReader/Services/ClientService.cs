using DataReader.Models;
using DataReader.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataReader.Services
{
    public class ClientService
    {
        private IClientRepository ClientRepository { get; set; }
        public ClientService(IClientRepository clientRepository)
        {
            ClientRepository = clientRepository;
        }
        public IEnumerable<Client> GetClients()
        {
            return ClientRepository.GetClients();
        }

        public Client GetClientByINN(ulong clientINN)
        {
            return ClientRepository.GetClientByINN(clientINN);
        }

        public Client GetClientById(int clientId)
        {
            return ClientRepository.GetClientById(clientId);
        }

        public void InsertClient(Client client)
        {
            ClientRepository.InsertClient(client);
            ClientRepository.Save();
        }

        public void UpdateClient(Client client)
        {
            ClientRepository.UpdateClient(client);
            ClientRepository.Save();
        }

        public void DeleteClient(int clientId)
        {
            ClientRepository.DeleteClient(clientId);
            ClientRepository.Save();
        }
    }
}
