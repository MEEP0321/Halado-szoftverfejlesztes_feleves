using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace DJLWQB_HSZF_2024251.Model
{
    public class Costumer
    {
        private string name;
        //private Contacts contacts;
        private int age;

        public Costumer(string name, int age, string contact=" - ")
        {
            Name = name;
            Contact = contact;
            Age = age;

            Id = Guid.NewGuid().ToString();
            Loans = new HashSet<Rental>();
        }

        public Costumer()
        {
            Loans = new HashSet<Rental>();
        }

        //DB thigs
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; private set; }

        public virtual ICollection<Rental> Loans { get; set; }
        //public virtual Contacts Contacts { get; set; }


        //Props and functions
        [Required]
        [StringLength(100)]
        public string Name {
            get
            {
                return name;
            }
            set
            {
                if (value is not null) {
                    name = value;
                }
            }
        }
        public int Age {
            get
            {
                return age;
            }
            set
            {
                if (value >= 0)
                {
                    age = value;
                }
            }
        }

        public string Contact { get; set; }

        public override string? ToString()
        {
            
            return String.Format("{0, -15} {1, -3} {2, -25}", Name, Age, Contact);
        }
    }
}
