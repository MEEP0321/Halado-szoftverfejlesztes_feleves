using DJLWQB_HSZF_2024251.Model;
using DJLWQB_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJLWQB_HSZF_2024251.Application
{
    public interface ICostumerService
    {
        Costumer GetCostumer(string id);

        IEnumerable<Costumer> GetCostumers();

        void AddCostumer(Costumer c);

        void ModifyCostumer(string id, Costumer c);

        void RemoveCostumer(string id);
    }
    public class CostumerService: ICostumerService
    {
        private readonly ICostumerDataProvider costumerDataProvider;
        public CostumerService(ICostumerDataProvider costumerDataProvider)
        {
                this.costumerDataProvider = costumerDataProvider;
        }

        public void AddCostumer(Costumer c)
        {
            costumerDataProvider.AddCostumer(c);
        }

        public void RemoveCostumer(string id)
        {
            costumerDataProvider.RemoveCostumer(id);
        }

        public Costumer GetCostumer(string id)
        {
            return costumerDataProvider.GetCostumer(id);
        }

        public IEnumerable<Costumer> GetCostumers()
        {
            return costumerDataProvider.GetCostumers();
        }

        public void ModifyCostumer(string id, Costumer c)
        {
            costumerDataProvider.ModifyCostumer(id, c);
        }
    }
}
