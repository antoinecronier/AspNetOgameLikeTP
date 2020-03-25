using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetOgameLikeTPClassLibrary.Entities
{
    public class Configuration
    {
        #region Private class variable
        private String key;
        private String data;
        #endregion

        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public String Key
        {
            get { return key; }
            set { key = value; }
        }

        public String Data
        {
            get { return data; }
            set { data = value; }
        }
        #endregion
    }
}
