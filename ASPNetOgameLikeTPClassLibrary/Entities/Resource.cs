using ASPNetOgameLikeTPClassLibrary.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetOgameLikeTPClassLibrary.Entities
{
    public class Resource : IDbEntity
    {
        #region Private class variable
        private long? id;
        private String name;
        private int? lastQuantity;
        private DateTime lastUpdate;
        #endregion
        #region Properties
        [StringLength(20, MinimumLength = 5)]
        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        [IntValidator(0, int.MaxValue)]
        public int? LastQuantity
        {
            get { return lastQuantity; }
            set { lastQuantity = value; }
        }

        [DatetimeMoreThanNow]
        public DateTime LastUpdate
        {
            get { return lastUpdate; }
            set { lastUpdate = value; }
        }
        #endregion
        #region Implemented properties
        public virtual long? Id { get => this.id; set => this.id = value; }
        #endregion
    }
}
