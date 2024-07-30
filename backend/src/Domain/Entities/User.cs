using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User: EntityBase
    {
        public string Name { get; set; }
        public string UserType { get; set; }
        public string Document { get => document; set => document = FormatDocument(value); }
        public string Crm { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        private string document;
        private string FormatDocument(string value)
        {
            if (value != null)
            {
                return value.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
            }
            return null;
        }
    }
}
