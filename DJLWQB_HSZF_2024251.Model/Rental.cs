using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJLWQB_HSZF_2024251.Model
{
    public class Rental
    {
        private DateTime loanData;
        private int price;

        public Rental(string costumerId, string movieId, DateTime loanDate, int price)
        {
            CostumerId = costumerId;
            MovieId = movieId;
            LoanDate = loanDate;
            Price = price;

            Id = Guid.NewGuid().ToString();
        }

        //DB things
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; private set; }

        [Required]
        public string CostumerId { get; private set; }
        public virtual Costumer Costumer { get; set; }

        [Required]
        public string MovieId { get; private set; }
        public virtual Movie Movie { get; set; }


        //Props, Functions
        [Required]
        public DateTime LoanDate
        {
            get
            {
                return loanData;
            }
            set
            {
                loanData = value;
            }
        }

        [Required]
        public int Price
        {
            get
            {
                return price;
            }
            set
            {
                if (value >= 0)
                {
                    price = value;
                }
            }
        }
    }
}
