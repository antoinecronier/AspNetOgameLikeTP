using ASPNetOgameLikeTPClassLibrary.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetOgameLikeTPClassLibrary.Entities
{
    public class Planet : IDbEntity
    {
        #region Private class variable
        private long? id;
        private String name;
        private int? caseNb;
        private List<Resource> resources;
        private List<Building> buildings;
        #endregion

        #region Properties
        [StringLength(20, MinimumLength = 5)]
        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        [IntOverValidator(Min = 0, Max = int.MaxValue)]
        public int? CaseNb
        {
            get { return caseNb; }
            set { caseNb = value; }
        }

        public virtual List<Resource> Resources
        {
            get { return resources; }
            set { resources = value; }
        }

        //Because no concret class exist we cannot map this.
        [NotMapped]
        public virtual List<Building> Buildings
        {
            get { return buildings; }
            set { buildings = value; }
        }

        #endregion

        #region Implemented properties
        public virtual long? Id { get => this.id; set => this.id = value; }
        #endregion

        #region Constructors
        public Planet()
        {
            this.buildings = new List<Building>();
            this.resources = new List<Resource>();
        }
        #endregion
    }
}
