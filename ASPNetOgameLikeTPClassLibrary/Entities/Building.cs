using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetOgameLikeTPClassLibrary.Entities
{
    public abstract class Building : IDbEntity
    {
        #region Private class variable
        private long? id;
        private String name;
        private int? level;
        #endregion
        #region Properties

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual int? Level
        {
            get { return level; }
            set { level = value; }
        }

        [NotMapped]
        public virtual int? CellNb
        {
            get { return level; }
        }

        [NotMapped]
        public virtual List<Resource> TotalCost
        {
            get { return new List<Resource>(); }
        }

        [NotMapped]
        public virtual List<Resource> NextCost
        {
            get { return new List<Resource>(); }
        }
        #endregion
        #region Implemented properties
        public virtual long? Id { get => this.id; set => this.id = value; }
        #endregion
    }
}
