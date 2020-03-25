using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetOgameLikeTPClassLibrary.Entities
{
    public abstract class ResourceGenerator : Building
    {
        #region Private class variable
        private List<Resource> resourceBySecond;
        #endregion

        #region Properties
        [NotMapped]
        public virtual List<Resource> ResourceBySecond
        {
            get { return resourceBySecond; }
        }
        #endregion

        #region Constructors
        public ResourceGenerator()
        {
            this.resourceBySecond = new List<Resource>();
        }
        #endregion
    }
}
