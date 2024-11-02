using DataLayer.Data;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class EntityRepository : IEntityRepository
    {
        private readonly ApplicationDbContext _context; 
        public EntityRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAccountAsync(Account account)
        {

            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddContactAsync(Contact contact)
        {
            contact.Key_value = Math.Abs(Guid.NewGuid().GetHashCode()); 
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddIncidentAsync(Account account, Incident incident)
        {
           if(account.Incident == null)
           {
                account.Incident_Name = incident.Incident_Name;
                await _context.Incidents.AddAsync(incident);
                await _context.SaveChangesAsync();
                return true;
           }
           return false;
        }

        public async Task<Contact> FindContactByEmailAsync(string email)
        {
            
            var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Email == email);
            if (contact == null)
            {
                return null;
            }
            return contact;
        }

        public async Task<Account> GetAccountAsync(string name)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Name == name);
            return account;
        }

        public async Task<Contact> GetContactByUniqueKeyAsync(int key)
        {
           var contact = _context.Contacts.FirstOrDefault(c => c.Key_value == key);
            return await Task.FromResult(contact);
        }
        public async Task<string> GetIncidentAsync(string accountName)
        {
           var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Name == accountName);
           
            return await Task.FromResult(account.Incident_Name);
        }
        public async Task<bool> LinkContactToAccountAsync(int id, string name)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Name == name);
            contact.Account = account;
            await _context.SaveChangesAsync();
            return true;
           
        }
        public Task<bool> UpdateContactAsync(string email, string firstName, string lastName)
        {
           var contact = _context.Contacts.FirstOrDefault(c => c.Email == email);
            contact.First_Name = firstName;
            contact.Last_Name = lastName;
            _context.SaveChanges();
            return  Task.FromResult(true);
        }
    }
}
