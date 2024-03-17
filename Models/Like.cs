using System.ComponentModel.DataAnnotations;
<<<<<<< HEAD
=======
using System.Text.Json.Serialization;
using WebApplication1.Models.DTO;
>>>>>>> origin/Dev

namespace WebApplication1.Models
{
	public class Like
	{
		[Key]
		public int idLike { get; set; }
		public int idUser { get; set; }
		public int idPicture { get; set; }

<<<<<<< HEAD
		public virtual Picture picture { get; set; }
		public virtual User user { get; set; }
=======
		[JsonIgnore]
		public virtual Picture picture { get; set; }
        public virtual User user { get; set; }


	
>>>>>>> origin/Dev
	}
}
