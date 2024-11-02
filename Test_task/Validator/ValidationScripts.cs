using DataLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Test_task.Validator
{
    public class ValidationScripts
    {
        private readonly IEntityRepository _repository;
        public ValidationScripts(IEntityRepository repository)
        {
            _repository = repository;   
        }
        //validation for contacts
        public async Task<(bool isValid, IActionResult errorResult)> ValidateContactExistsAsync(int keyValue)
        {
            var contact = await _repository.GetContactByUniqueKeyAsync(keyValue);
            if (contact == null || contact.Id == 0)
            {
                return (false, ResponseHelper.CreateErrorResponse("Contact not found", $"Contact with unique identifier {keyValue} does not exist", 400));
            }
            return (true, null);
        }
        public async Task<(bool isValid, IActionResult errorResult)> ValidateEmailUniquenessAsync(string email)
        {
            var existingContact = await _repository.FindContactByEmailAsync(email);
            if (existingContact != null)
            {
                return (false, ResponseHelper.CreateErrorResponse("Duplicate email", "A contact with this email already exists.", 400));
            }
            return (true, null);
        }
        public async Task<(bool isValid, IActionResult errorResult)> ValidateSingleAccountAssociationAsync(string email)
        {
            var contact = await _repository.FindContactByEmailAsync(email);
            if (contact?.AccountId != null)
            {
                return (false, ResponseHelper.CreateErrorResponse("Multiple account association", "This contact is already linked to another account.", 400));
            }
            return (true, null);
        }
        //validation for accounts
        public async Task<(bool isValid, IActionResult errorResult)> ValidateAccountExistsAsync(string accountName)
        {
            var account = await _repository.GetAccountAsync(accountName);
            if (account == null || account.Name == null)
            {
                return (false, ResponseHelper.CreateErrorResponse("Account not found", $"Account with name {accountName} does not exist", 400));
            }
            return (true, null);
        }

        public async Task<(bool isValid, IActionResult errorResult)> ValidateAccountDuplicateAsync(string accountName)
        {
            var account = await _repository.GetAccountAsync(accountName);
            if (account.Name == accountName)
            {
                return (false, ResponseHelper.CreateErrorResponse("Duplicate account", $"Account with name {accountName} already exists", 400));
            }
            return (true, null);
        }
        public async Task<(bool isValid, IActionResult errorResult)> ValidateIncidentForAccountAsync(string accountName)
        {
            var account = await _repository.GetAccountAsync(accountName);
            var incident = await _repository.GetIncidentAsync(accountName);
            if (account != null && incident != null)
            {
                return (false, ResponseHelper.CreateErrorResponse("Incident already exists", $"Account with name {accountName} already has an incident", 400));
            }
            return (true, null);
        }
      
    }
}
