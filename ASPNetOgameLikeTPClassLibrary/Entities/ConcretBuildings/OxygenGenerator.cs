using ASPNetOgameLikeTPClassLibrary.Extensions;
using ASPNetOgameLikeTPClassLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetOgameLikeTPClassLibrary.Entities.ConcretBuildings
{
    public class OxygenGenerator : ResourceGenerator
    {
        private Func<int?, int?> energyFunc = (int? x) => { return x; };
        private Func<int?, int?> oxygenFunc = (int? x) => { return (200 * (x / 12)) + 20; };
        private Func<int?, int?> steelFunc = (int? x) => { return (1000 * (x / 8)) + 20; };
        private Func<int?, int?> uraniumFunc = (int? x) => { return (1500 * (x / 20)) + 20; };

        private String oxygenFuncString;
        public String OxygenFuncString
        {
            get { return this.oxygenFuncString; }
            set 
            {
                this.oxygenFuncString = value;
                int[] terms = value.Split('|').Select(x => int.Parse(x)).ToArray();
                if (terms.Length == 3)
                {
                    this.oxygenFunc = (int? x) => { return (terms[0] * (x / terms[1])) + terms[2]; };
                }
                else if(terms.Length == 4)
                {
                    this.oxygenFunc = (int? x) => { return (terms[0] * (x * x)) + (terms[1] * (x / terms[2])) + terms[3]; };
                }
                else if (terms.Length == 5)
                {
                    this.oxygenFunc = (int? x) => { return (terms[0] * (x * x * x)) +  (terms[1] * (x * x)) + (terms[2] * (x / terms[3])) + terms[4]; };
                }
            }
        }


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
                    new Resource() { Name = ResourceNames.Oxygen.GetName(), LastUpdate = DateTime.Now, LastQuantity = (20 * (this.Level / 2)) + 5 }
                };
                
                return result;
            }
        }
    }
}
