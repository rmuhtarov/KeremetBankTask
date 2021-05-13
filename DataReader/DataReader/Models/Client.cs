using System;
using System.Collections.Generic;
using System.Text;

namespace DataReader.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Fio { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public ulong SocialNumber { get; set; }

        public Client()
        {

        }

        public Client(string fio, DateTime birthdate, string phoneNumber, string address, ulong socialNuber)
        {
            Fio = fio;
            BirthDate = birthdate;
            PhoneNumber = phoneNumber;
            Address = address;
            SocialNumber = socialNuber;
        }
    }
}
