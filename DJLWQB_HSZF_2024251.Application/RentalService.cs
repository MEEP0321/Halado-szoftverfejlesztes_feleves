using DJLWQB_HSZF_2024251.Model;
using DJLWQB_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJLWQB_HSZF_2024251.Application
{
    public interface IRentalService
    {
        void AddRental(Rental r);
    }
    public class RentalService: IRentalService
    {
        private readonly IRentalDataProvider rentalDataProvider;

        public RentalService(IRentalDataProvider rentalDataProvider)
        {
            this.rentalDataProvider = rentalDataProvider;
        }

        public void AddRental(Rental r)
        {
            rentalDataProvider.AddRental(r);

        }
    }
}
