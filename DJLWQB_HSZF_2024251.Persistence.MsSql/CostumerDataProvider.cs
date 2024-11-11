using DJLWQB_HSZF_2024251.Model;
using DJLWQB_HSZF_2024251.Persistence.MsSql.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJLWQB_HSZF_2024251.Persistence.MsSql
{
    public interface ICostumerDataProvider
    {
        Costumer GetCostumer(string id);

        IEnumerable<Costumer> GetCostumers();

        void AddCostumer(Costumer c);

        void ModifyCostumer(string id, Costumer c);

        void RemoveCostumer(string id);
    }
    public class CostumerDataProvider: ICostumerDataProvider
    {
        private readonly VideoTKDBContext context;
        public CostumerDataProvider(VideoTKDBContext context)
        {
            this.context = context;
        }

        public void AddCostumer(Costumer c)
        {
            context.Costumers.Add(c);
            context.SaveChanges();
        }

        public void RemoveCostumer(string id)
        {
            context.Costumers.Remove(GetCostumer(id));
            context.SaveChanges();
        }

        public Costumer GetCostumer(string id)
        {
            return context.Costumers.First(x=>x.Id == id);
        }

        public IEnumerable<Costumer> GetCostumers()
        {
            return context.Costumers;
        }

        public void ModifyCostumer(string id, Costumer c)
        {
            Type type = typeof(Costumer);

            foreach (var prop in type.GetProperties())
            {
                if (prop.Name != "Id")
                {
                    prop.SetValue(GetCostumer(id), prop.GetValue(c));
                }
            }
            context.SaveChanges();
        }
    }
}
