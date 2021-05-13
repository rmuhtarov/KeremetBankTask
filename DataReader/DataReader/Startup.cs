using DataReader.EF;
using DataReader.Helpers;
using DataReader.Models;
using DataReader.Repository;
using DataReader.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace DataReader
{
    public class Startup
    {
        static void Main(string[] args)
        {
            ClientService clientService;
            Client findClient = new Client();
            try
            {
                clientService = new ClientService(new ClientRepository(new ClientDbContext()));
                InitializeDb(clientService);

                Console.WriteLine("Введите ИНН клиента: ");
                var INN = Convert.ToUInt64(Console.ReadLine());
                findClient = clientService.GetClientByINN(INN);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"При создании базы и инициализации её данными произошла ошибка: {ex}");
                Process.GetCurrentProcess().Kill();
            }

            try
            {
                string templateFilePath = $"{Directory.GetCurrentDirectory()}/../../../TemplateFile/example.xlsx";
                string resultFilePath = $"{Directory.GetCurrentDirectory()}/../../../ResultFile/result.xlsx";
                TemplateReader.ReadAndCreateFile(templateFilePath, resultFilePath, findClient);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"При чтении и создании Excel файла произошла ошибка: {ex}");
            }


            Console.WriteLine("Конец программы!");
        }

        private static void InitializeDb(ClientService clientService)
        {
            var clients = new List<Client>();
            clients.Add(new Client("Тестовый клиент1", new DateTime(1991, 03, 08), "123", "г.Баткен", 12345678901234));
            clients.Add(new Client("Тестовый клиент2", new DateTime(1996, 04, 20), "456", "г.Бишкек", 98765432101234));
            clients.Add(new Client("Тестовый клиент3", new DateTime(1995, 08, 04), "789", "г.Нарын", 12345543211234));
            clients.Add(new Client("Тестовый клиент4", new DateTime(1989, 02, 25), "012", "с.Комсомольское", 12345671234567));
            foreach (var client in clients)
            {
                clientService.InsertClient(client);
            }
        }
    }
}
