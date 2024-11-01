namespace DataLayer.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Key_value { get; set; }
        public string? Incident_Name { get; set; }
        public Incident? Incident { get; set; }
        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
       

    }
}
