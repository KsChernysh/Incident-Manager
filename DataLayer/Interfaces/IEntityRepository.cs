using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IEntityRepository
    {
        Task<bool> AddAccountAsync(Account account);
        Task<bool> AddContactAsync(Contact contact);
        Task<bool> AddIncidentAsync(Account account, Incident incident);
        Task<Account> GetAccountAsync(string name);
        Task<Contact> FindContactByEmailAsync(string email);
        Task<bool> LinkContactToAccountAsync(int id, string name);
        Task<Contact> GetContactByUniqueKeyAsync(int key);
        Task<bool> UpdateContactAsync(string email, string firstName, string lastName);
        Task<string> GetIncidentAsync(string accountName);




    }
}
