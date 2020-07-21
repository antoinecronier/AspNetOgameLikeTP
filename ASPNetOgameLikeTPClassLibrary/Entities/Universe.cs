using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetOgameLikeTPClassLibrary.Entities
{
    public class Universe : IDbEntity
    {
		#region Private class variable
		private long? id;
		private String name;
		private List<SolarSystem> solarSystems;
		#endregion

		#region Properties
		[StringLength(20, MinimumLength = 5)]
		public String Name
		{
			get { return name; }
			set { name = value; }
		}

		public virtual List<SolarSystem> SolarSystems
		{
			get { return solarSystems; }
		}
		#endregion

		#region Implemented properties
		public virtual long? Id { get => this.id; set => this.id = value; }
		#endregion

		#region Constructors
		public Universe()
		{
			this.solarSystems = new List<SolarSystem>();
		}
		#endregion
	}
}
