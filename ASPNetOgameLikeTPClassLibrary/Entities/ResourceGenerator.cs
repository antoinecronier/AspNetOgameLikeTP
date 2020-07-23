using ASPNetOgameLikeTPClassLibrary.Extensions;
using ASPNetOgameLikeTPClassLibrary.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetOgameLikeTPClassLibrary.Entities
{
    public class ResourceGenerator : Building
    {
        #region Private class variable
        private Func<int?, int?> energyCostFunc;
        private Func<int?, int?> oxygenCostFunc;
        private Func<int?, int?> steelCostFunc;
        private Func<int?, int?> uraniumCostFunc;

        private Func<int?, int?> energyGenFunc;
        private Func<int?, int?> oxygenGenFunc;
        private Func<int?, int?> steelGenFunc;
        private Func<int?, int?> uraniumGenFunc;

        private String energyCostFuncString;
        private String oxygenCostFuncString;
        private String steelCostFuncString;
        private String uraniumCostFuncString;

        private String energyGenFuncString;
        private String oxygenGenFuncString;
        private String steelGenFuncString;
        private String uraniumGenFuncString;

        #endregion

        #region Properties
        [NotMapped]
        public String EnergyCostFuncString
        {
            get { return this.energyCostFuncString; }
            set
            {
                this.energyCostFuncString = value;
                this.energyCostFunc = MathUtil.MathFuncFromString(value);
            }
        }

        [NotMapped]
        public String OxygenCostFuncString
        {
            get { return this.oxygenCostFuncString; }
            set
            {
                this.oxygenCostFuncString = value;
                this.oxygenCostFunc = MathUtil.MathFuncFromString(value);
            }
        }

        [NotMapped]
        public String SteelCostFuncString
        {
            get { return this.steelCostFuncString; }
            set
            {
                this.steelCostFuncString = value;
                this.steelCostFunc = MathUtil.MathFuncFromString(value);
            }
        }

        [NotMapped]
        public String UraniumCostFuncString
        {
            get { return this.uraniumCostFuncString; }
            set
            {
                this.uraniumCostFuncString = value;
                this.uraniumCostFunc = MathUtil.MathFuncFromString(value);
            }
        }

        [NotMapped]
        public String EnergyGenFuncString
        {
            get { return this.energyGenFuncString; }
            set
            {
                this.energyGenFuncString = value;
                this.energyGenFunc = MathUtil.MathFuncFromString(value);
            }
        }

        [NotMapped]
        public String OxygenGenFuncString
        {
            get { return this.oxygenGenFuncString; }
            set
            {
                this.oxygenGenFuncString = value;
                this.oxygenGenFunc = MathUtil.MathFuncFromString(value);
            }
        }

        [NotMapped]
        public String SteelGenFuncString
        {
            get { return this.steelGenFuncString; }
            set
            {
                this.steelGenFuncString = value;
                this.steelGenFunc = MathUtil.MathFuncFromString(value);
            }
        }

        [NotMapped]
        public String UraniumGenFuncString
        {
            get { return this.uraniumGenFuncString; }
            set
            {
                this.uraniumGenFuncString = value;
                this.uraniumGenFunc = MathUtil.MathFuncFromString(value);
            }
        }

        public override List<Resource> TotalCost
        {
            get
            {
                List<Resource> result = new List<Resource>()
                {
                    new Resource() { Name = ResourceNames.Energy.GetName(), LastUpdate = DateTime.Now,
                        LastQuantity = MathUtil.FactorialExpression(energyCostFunc,this.Level) },
                    new Resource() { Name = ResourceNames.Oxygen.GetName(), LastUpdate = DateTime.Now,
                        LastQuantity = MathUtil.FactorialExpression(oxygenCostFunc,this.Level) },
                    new Resource() { Name = ResourceNames.Steel.GetName(), LastUpdate = DateTime.Now,
                        LastQuantity = MathUtil.FactorialExpression(steelCostFunc,this.Level) },
                    new Resource() { Name = ResourceNames.Uranium.GetName(), LastUpdate = DateTime.Now,
                        LastQuantity = MathUtil.FactorialExpression(uraniumCostFunc,this.Level) }
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
                    new Resource() { Name = ResourceNames.Energy.GetName(), LastQuantity = energyCostFunc.Invoke(this.Level + 1) },
                    new Resource() { Name = ResourceNames.Oxygen.GetName(), LastQuantity = oxygenCostFunc.Invoke(this.Level + 1) },
                    new Resource() { Name = ResourceNames.Steel.GetName(), LastQuantity = steelCostFunc.Invoke(this.Level + 1) },
                    new Resource() { Name = ResourceNames.Uranium.GetName(), LastQuantity = uraniumCostFunc.Invoke(this.Level + 1) }
                };

                return result;
            }
        }

        [NotMapped]
        public List<Resource> ResourceBySecond
        {
            get
            {
                List<Resource> result = new List<Resource>()
                {
                    new Resource() { Name = ResourceNames.Energy.GetName(), LastUpdate = DateTime.Now, LastQuantity = energyGenFunc.Invoke(this.Level) },
                    new Resource() { Name = ResourceNames.Oxygen.GetName(), LastUpdate = DateTime.Now, LastQuantity = oxygenGenFunc.Invoke(this.Level) },
                    new Resource() { Name = ResourceNames.Steel.GetName(), LastUpdate = DateTime.Now, LastQuantity = steelGenFunc.Invoke(this.Level) },
                    new Resource() { Name = ResourceNames.Uranium.GetName(), LastUpdate = DateTime.Now, LastQuantity = uraniumGenFunc.Invoke(this.Level) }
                };

                return result;
            }
        }
        #endregion

        #region Constructors
        public ResourceGenerator()
        {
        }
        #endregion
    }
}
