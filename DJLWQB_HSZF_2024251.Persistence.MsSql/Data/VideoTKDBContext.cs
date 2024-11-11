using DJLWQB_HSZF_2024251.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJLWQB_HSZF_2024251.Persistence.MsSql.Data
{
    public class VideoTKDBContext : DbContext
    {
        public VideoTKDBContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
            SetDefaultDatas();
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Costumer> Costumers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=VideoTk;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            optionsBuilder.UseSqlServer(connStr).UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public void SetDefaultDatas()
        {
            Movie[] movies = {
                new Movie("Inception", "Sci-Fi", new DateTime(2010, 7, 16), 1500, 10),
                new Movie("The Godfather", "Crime", new DateTime(1972, 3, 24), 2500, 5),
                new Movie("The Dark Knight", "Action", new DateTime(2008, 7, 18), 2000, 8),
                new Movie("Pulp Fiction", "Crime", new DateTime(1994, 10, 14), 1800, 12),
                new Movie("The Shawshank Redemption", "Drama", new DateTime(1994, 9, 23), 1600, 15),
                new Movie("Forrest Gump", "Drama", new DateTime(1994, 7, 6), 1700, 7),
                new Movie("Fight Club", "Drama", new DateTime(1999, 10, 15), 1900, 6),
                new Movie("Interstellar", "Sci-Fi", new DateTime(2014, 11, 7), 2200, 9),
                new Movie("The Matrix", "Sci-Fi", new DateTime(1999, 3, 31), 2100, 10),
                new Movie("Gladiator", "Action", new DateTime(2000, 5, 5), 2300, 5),
                new Movie("Titanic", "Romance", new DateTime(1997, 12, 19), 3000, 20),
                new Movie("Jurassic Park", "Adventure", new DateTime(1993, 6, 11), 1400, 10),
                new Movie("Avatar", "Sci-Fi", new DateTime(2009, 12, 18), 3400, 3),
                new Movie("The Silence of the Lambs", "Thriller", new DateTime(1991, 2, 14), 1200, 8),
                new Movie("Saving Private Ryan", "War", new DateTime(1998, 7, 24), 1900, 6),
                new Movie("The Social Network", "Drama", new DateTime(2010, 10, 1), 2100, 12),
                new Movie("The Avengers", "Action", new DateTime(2012, 5, 4), 3400, 9),
                new Movie("Frozen", "Animation", new DateTime(2013, 11, 27), 2500, 15),
                new Movie("Inside Out", "Animation", new DateTime(2015, 6, 19), 2300, 11),
                new Movie("Zootopia", "Animation", new DateTime(2016, 3, 17), 2100, 13),
                new Movie("Wonder Woman", "Action", new DateTime(2017, 6, 2), 2700, 4),
                new Movie("Black Panther", "Action", new DateTime(2018, 2, 16), 3000, 8),
                new Movie("Parasite", "Thriller", new DateTime(2019, 5, 30), 2000, 6),
                new Movie("Dune", "Sci-Fi", new DateTime(2021, 10, 22), 3500, 5)
            };
            Movies.AddRange(movies);

            Costumer[] costumers = {
                new Costumer("Kiss Ádám", 22, "kiss.adam@gmail.com"),
                new Costumer("Nagy László", 30, "nagy.laszlo@yahoo.com"),
                new Costumer("Szabó Júlia", 25, "szabo.julia@hotmail.com"),
                new Costumer("Tóth Péter", 28, "toth.peter@outlook.com"),
                new Costumer("Varga Anna", 35, "varga.anna@gmail.com"),
                new Costumer("Molnár Zoltán", 40, "molnar.zoltan@icloud.com"),
                new Costumer("Kovács Eszter", 32, "kovacs.eszter@gmail.com"),
                new Costumer("Farkas Gábor", 27, "farkas.gabor@gmail.com"),
                new Costumer("Papp Mária", 29, "papp.maria@yahoo.com"),
                new Costumer("Balogh András", 31, "balogh.andras@hotmail.com")
            };
            Costumers.AddRange(costumers);

            SaveChanges();
        }

    }
}
