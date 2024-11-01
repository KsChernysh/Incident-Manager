using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Incident
    {
        [Key]     
        public string Incident_Name { get; set; } = Guid.NewGuid().ToString();
        public string Description { get; set; }
        public ICollection<Account> Accounts { get; set; } = new List<Account>();


    }
}
