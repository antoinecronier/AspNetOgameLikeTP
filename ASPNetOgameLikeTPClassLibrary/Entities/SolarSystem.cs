using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNetOgameLikeTPClassLibrary.Entities
{
    public class SolarSystem : IDbEntity
    {
		#region Private class variable
		private long? id;
		private String name;
		private List<Planet> planets;
		#endregion

		#region Properties
		[StringLength(20,MinimumLength = 5)]
		public String Name
		{
			get { return name; }
			set { name = value; }
		}

		public virtual List<Planet> Planets
		{
			get { return planets; }
			set { planets = value; }
		}
		#endregion

		#region Implemented properties
		public virtual long? Id { get => this.id; set => this.id = value; }
		#endregion

		#region Constructors
		public SolarSystem()
		{
			this.planets = new List<Planet>();
		}
		#endregion
	}
}
