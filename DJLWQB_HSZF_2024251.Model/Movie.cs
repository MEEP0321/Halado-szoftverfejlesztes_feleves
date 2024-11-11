using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace DJLWQB_HSZF_2024251.Model
{
    public class Movie
    {
        private string title;
        private string genre;
        private DateTime release;
        private int price;

        private int pcs;

        public Movie(string title, string genre, DateTime release, int price, int pcs = 1)
        {
            Id = Guid.NewGuid().ToString();
            Title = title;
            Genre = genre;
            Release = release;
            Price = price;
            Pcs = pcs;

            Loans = new HashSet<Rental>();
        }

        public Movie()
        {
            Loans = new HashSet<Rental>();
        }

        //DB things
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; private set; }

        public virtual ICollection<Rental> Loans { get; set; } 


        //Props, Functions
        [Required]
        [StringLength(255)]
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (value is not null)
                {
                    title = value;
                }
            }
        }

        [StringLength(255)]
        [Required]
        public string Genre
        {
            get
            {
                return genre;
            }
            set
            {
                if (value is not null)
                {
                    genre = value;
                }
            }
        }

        [Required]
        public DateTime Release
        {
            get
            {
                return release;
            }
            set
            {
                release = value; 
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

        [Required]
        public int Pcs
        {
            get
            {
                return pcs;
            }
            set
            {
                if (value >= 0) 
                {
                    pcs = value;
                }
            }
        }

        public override string? ToString()
        {
            return String.Format("{0, -30} {1, -10} {2:d} {3, -5} {4, -3}", Title, Genre, Release, Price, Pcs);
        }

        public override bool Equals(object? obj)
        {
            if (obj is Movie)
            {
                Movie temp = (Movie)obj;
                return this.Title == temp.Title && this.Genre == temp.Genre && this.Release == temp.Release && this.Price == temp.Price;
            }
            else
            {
                return false;
            }
        }
    }
}
