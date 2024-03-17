<<<<<<< HEAD
﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTO
{
	public class LikeDTO
	{
		[Key]
		public int idUser { get; set; }
		public int idPicture { get; set; }
		public Like GetLike()
		{
			return new Like
			{
				idUser = this.idUser,
				idPicture = this.idPicture
			};
		}
	}

=======
﻿namespace WebApplication1.Models.DTO
{
    public class LikeDTO : Like
    {
          public UserDTO user { get; set; }
  
    }
>>>>>>> origin/Dev
}
