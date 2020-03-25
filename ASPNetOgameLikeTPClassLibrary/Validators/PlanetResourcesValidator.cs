using ASPNetOgameLikeTPClassLibrary.Entities;
using ASPNetOgameLikeTPClassLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetOgameLikeTPClassLibrary.Validators
{
    public class PlanetResourcesValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool result = true;

            try
            {
                List<Resource> resources = value as List<Resource>;
                if (resources.Count != 4)
                {
                    result = false;
                }

                bool energyBool = false;
                bool oxygenBool = false;
                bool steelBool = false;
                bool uraniumBool = false;

                resources.ForEach((x) =>
                {
                    if (ResourceNames.Energy.GetName() == x.Name)
                    {
                        energyBool = true;
                    }
                    else if (ResourceNames.Oxygen.GetName() == x.Name)
                    {
                        oxygenBool = true;
                    }
                    else if (ResourceNames.Steel.GetName() == x.Name)
                    {
                        steelBool = true;
                    }
                    else if (ResourceNames.Uranium.GetName() == x.Name)
                    {
                        uraniumBool = true;
                    }
                });

                if (!(energyBool && oxygenBool && steelBool && uraniumBool))
                {
                    result = false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                result = false;
            }

            return result;
        }
    }
}
