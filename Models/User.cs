<<<<<<< HEAD
﻿using System.ComponentModel.DataAnnotations;
=======
﻿
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
>>>>>>> origin/Dev

namespace WebApplication1.Models
{
	public class User
	{
		[Key]
		public int idUser { get; set; }
		public string pseudo  { get; set; }
		public string email { get; set; }
		public string pwd { get; set; }
		public string bio { get; set; }
		public string pdpPath { get; set; }

<<<<<<< HEAD
		public virtual ICollection<Picture> pictures { get; set; }
		public virtual ICollection<Like> likes { get; set; }
=======
		//Navigation Property
		[JsonIgnore]
		public virtual ICollection<Picture> pictures { get; set; }
		[JsonIgnore]
		public virtual ICollection<Like> likes { get; set; }
		[JsonIgnore]
>>>>>>> origin/Dev
		public virtual ICollection<Comments> comments { get; set; }
	}
}
