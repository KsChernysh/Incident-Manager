using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_task.Profiles;
using Test_task.Validator;

namespace Test_task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IEntityRepository _repository;
        private readonly ValidationScripts _validationScripts;
        public HomeController(IEntityRepository repository, ValidationScripts validationScripts)
        {
            _repository = repository;
            _validationScripts = validationScripts;

        }
        [HttpPost("AddAccount")]
        public async Task<IActionResult> AddAccountAsync([FromBody] AccountProfile account)
        {
            //check if the account isn't null 
            var (isContactValid, contactError) = await _validationScripts.ValidateContactExistsAsync(account.keyValue);
            if (!isContactValid)
            {
                return contactError;
            }
            //check if the account name is unique
            var (isAccountValid, accountError) = await _validationScripts.ValidateAccountDuplicateAsync(account.name);
            if (!isAccountValid)
            {
                return accountError;
            }

            var accountDomain = new Account
            {
                Name = account.name,
                Key_value = account.keyValue.ToString()
            };
           
            await _repository.AddAccountAsync(accountDomain);
            return Ok("Account added successfully");
        }
        [HttpPost("AddContact")]
        public async Task<IActionResult> AddContactAsync([FromBody] ContactProfile contact)
        {
            // check if the contact isn't null && it's email is unique
            if (contact == null )
            {
                return BadRequest("Your contact information is invalid");
            }
            
            var (isEmailUnique, emailValidationError) = await _validationScripts.ValidateEmailUniquenessAsync(contact.email);
            if (!isEmailUnique)
            {
                return emailValidationError;
            }
            var contactDomain = new Contact
            {
                First_Name = contact.firstName,
                Last_Name = contact.lastName,
                Email = contact.email
            };
            
            await _repository.AddContactAsync(contactDomain);
     
            return Ok("Your unique contact number:" + contactDomain.Key_value);
        }
        [HttpPost("AddIncident")]
        public async Task<IActionResult> AddIncidentAsync([FromBody] IncidentProfile incident)
        {
            //check if the account isn't new
            var (isAccountValid, accountError) = await _validationScripts.ValidateAccountExistsAsync(incident.accountName);
            if (!isAccountValid)
            {
                return accountError;
            }
            //check if the contact exists (by the email)
            var contactDomain = await _repository.FindContactByEmailAsync(incident.contactEmail);
            //if contact is new - create it and link to account
            if (contactDomain == null)
            {
                var contact = new Contact
                {
                    First_Name = incident.contactFirstName,
                    Last_Name = incident.contactLastName,
                    Email = incident.contactEmail
                };
                //check if the contact isn't linked to another account
                var (isContactValid, contactError) = await _validationScripts.ValidateSingleAccountAssociationAsync(contactDomain.Email);
                if (!isContactValid)
                {
                    return contactError;
                }
                await _repository.AddContactAsync(contact);
                await _repository.LinkContactToAccountAsync(contact.Id, incident.accountName);

            }
            // if contact exists - update it and link to account
            else if (contactDomain.First_Name != incident.contactFirstName || contactDomain.Last_Name != incident.contactLastName) {
                //check if the contact isn't linked to another account
                var (isContactValid, contactError) = await _validationScripts.ValidateSingleAccountAssociationAsync(contactDomain.Email);
                if (!isContactValid)
                {
                    return contactError;
                }
                await _repository.UpdateContactAsync(contactDomain.Email, incident.contactFirstName, incident.contactLastName);
                await _repository.LinkContactToAccountAsync(contactDomain.Id, incident.accountName);
            }
            //check if the account doesn't have an incident
            var (isIncidentValid, incidentError) = await _validationScripts.ValidateIncidentForAccountAsync(incident.accountName);
            if (!isIncidentValid)
            {
                return incidentError;
            }
            var account = await _repository.GetAccountAsync(incident.accountName);
            var incidentDomain = new Incident 
            { 
                Description = incident.incidentDescription
            };
            await _repository.AddIncidentAsync(account, incidentDomain);
          
        
            return Ok("Incident added successfully");
        }

       
    }
}
