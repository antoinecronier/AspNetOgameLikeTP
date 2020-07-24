using ASPNetOgameLikeTPClassLibrary.Utils;
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
    public class Resource : IDbEntity, IPrintable
    {
        #region Private class variable
        private long? id;
        private String name;
        private double? lastQuantity;
        private DateTime lastUpdate;
        private string print;
        #endregion
        #region Properties
        [StringLength(20, MinimumLength = 5)]
        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        [DoubleValidator(0, Double.MaxValue)]
        public double? LastQuantity
        {
            get { return lastQuantity; }
            set { lastQuantity = value; }
        }

        [DatetimeLessThanNow]
        [Column(TypeName = "datetime2")]
        public DateTime LastUpdate
        {
            get { return lastUpdate; }
            set { lastUpdate = value; }
        }

        public String Print
        {
            get { return print; }
            set 
            {
                if (value != null && !value.StartsWith("~"))
                {
                    print = ClassUtil.ImgPath(value);
                }
                else
                {
                    print = value;
                }
            }
        }
        #endregion
        #region Implemented properties
        public virtual long? Id { get => this.id; set => this.id = value; }
        #endregion
    }
}
