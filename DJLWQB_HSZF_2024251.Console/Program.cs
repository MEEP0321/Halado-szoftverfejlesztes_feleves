using DJLWQB_HSZF_2024251.Application;
using DJLWQB_HSZF_2024251;
using DJLWQB_HSZF_2024251.Model;
using DJLWQB_HSZF_2024251.Persistence.MsSql;
using DJLWQB_HSZF_2024251.Persistence.MsSql.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DJLWQB_HSZF_2024251.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            //helloooooo
            Display d = new Display(CreateHost());
            d.StartApp();
            
        }

        static IHost CreateHost()
        {
            return Host.CreateDefaultBuilder().ConfigureServices((hostContext, sevices) =>
            {
                //DB
                sevices.AddScoped<VideoTKDBContext>();

                //Movie things
                sevices.AddSingleton<IMovieDataProvider, MovieDataProvider>();
                sevices.AddSingleton<IMovieService, MovieService>();

                //Costumer things
                sevices.AddSingleton<ICostumerDataProvider, CostumerDataProvider>();
                sevices.AddSingleton<ICostumerService, CostumerService>();

                //Loan things
                sevices.AddSingleton<IRentalDataProvider, RentalDataProvider>();
                sevices.AddSingleton<IRentalService, RentalService>();

            }).Build();
        }
    }
}
