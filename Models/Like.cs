﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.DTO;

namespace WebApplication1.Models
{
	public class Like
	{
		[Key]
		public int idLike { get; set; }
		public int idUser { get; set; }
		public int idPicture { get; set; }

		[JsonIgnore]
		public virtual Picture picture { get; set; }
        public virtual User user { get; set; }


	
	}
}
