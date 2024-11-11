using DJLWQB_HSZF_2024251.Model;
using DJLWQB_HSZF_2024251.Persistence.MsSql.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJLWQB_HSZF_2024251.Persistence.MsSql
{
    public interface IRentalDataProvider
    {
        void AddRental(Rental l);
        bool CanRentMovie(Rental l);
    }
    public class RentalDataProvider: IRentalDataProvider
    {
        private readonly VideoTKDBContext context;
        public RentalDataProvider(VideoTKDBContext context)
        {
            this.context = context;
        }

        public void AddRental(Rental l)
        {
            context.Rentals.Add(l);
            context.Movies.First(x => x.Id == l.MovieId).Pcs--;
            context.SaveChanges();
        }

        public bool CanRentMovie(Rental l)
        {
            return context.Movies.First(x => x.Id == l.MovieId).Pcs > 0;
        }
    }
}
