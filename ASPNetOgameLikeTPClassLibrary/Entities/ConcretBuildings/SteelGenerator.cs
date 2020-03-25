using ASPNetOgameLikeTPClassLibrary.Extensions;
using ASPNetOgameLikeTPClassLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetOgameLikeTPClassLibrary.Entities.ConcretBuildings
{
    public class SteelGenerator : ResourceGenerator
    {
        private Func<int?, int?> energyFunc = (int? x) => { return x; };
        private Func<int?, int?> oxygenFunc = (int? x) => { return (2 * int.Parse(Math.Pow(x.Value, 3).ToString())) + (300 * (x / 6)) + 50; };
        private Func<int?, int?> steelFunc = (int? x) => { return x + (100 * (x / 8)) + 20; };
        private Func<int?, int?> uraniumFunc = (int? x) => { return 7 * int.Parse(Math.Pow(x.Value, 3).ToString()) + (200 * (x / 12)) + 20; };
        public override List<Resource> TotalCost
        {
            get
            {
                List<Resource> result = new List<Resource>()
                {
                    new Resource() { Name = ResourceNames.Energy.GetName(), LastUpdate = DateTime.Now,
                        LastQuantity = MathUtil.FactorialExpression(energyFunc,this.Level) },
                    new Resource() { Name = ResourceNames.Oxygen.GetName(), LastUpdate = DateTime.Now,
                        LastQuantity = MathUtil.FactorialExpression(oxygenFunc,this.Level) },
                    new Resource() { Name = ResourceNames.Steel.GetName(), LastUpdate = DateTime.Now,
                        LastQuantity = MathUtil.FactorialExpression(steelFunc,this.Level) },
                    new Resource() { Name = ResourceNames.Uranium.GetName(), LastUpdate = DateTime.Now,
                        LastQuantity = MathUtil.FactorialExpression(uraniumFunc,this.Level) }
                };

                return result;
            }
        }

        public override List<Resource> NextCost
        {
            get
            {
                List<Resource> result = new List<Resource>()
                {
                    new Resource() { Name = ResourceNames.Energy.GetName(), LastUpdate = DateTime.Now, LastQuantity = energyFunc.Invoke(this.Level + 1) },
                    new Resource() { Name = ResourceNames.Oxygen.GetName(), LastUpdate = DateTime.Now, LastQuantity = oxygenFunc.Invoke(this.Level + 1) },
                    new Resource() { Name = ResourceNames.Steel.GetName(), LastUpdate = DateTime.Now, LastQuantity = steelFunc.Invoke(this.Level + 1) },
                    new Resource() { Name = ResourceNames.Uranium.GetName(), LastUpdate = DateTime.Now, LastQuantity = uraniumFunc.Invoke(this.Level + 1) }
                };

                return result;
            }
        }

        public override List<Resource> ResourceBySecond
        {
            get
            {
                List<Resource> result = new List<Resource>()
                {
                    new Resource() { Name = ResourceNames.Energy.GetName(), LastUpdate = DateTime.Now, LastQuantity = 10 * (this.Level / 2) + 10 }
                };

                return result;
            }
        }
    }
}
