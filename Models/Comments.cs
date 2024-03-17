using System.ComponentModel.DataAnnotations;
<<<<<<< HEAD
=======
using System.Text.Json.Serialization;
>>>>>>> origin/Dev

namespace WebApplication1.Models
{
	public class Comments
	{
		[Key]
		public int idComment { get; set; }
		public int idUser { get; set; }
		public int idPicture { get; set; }
		public string comment { get; set; }

<<<<<<< HEAD
=======
		[JsonIgnore]
>>>>>>> origin/Dev
		public virtual Picture picture { get; set; }
		public virtual User User { get; set; }
	}
}
