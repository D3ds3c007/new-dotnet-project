using System.ComponentModel.DataAnnotations;
<<<<<<< HEAD
=======
using System.Text.Json.Serialization;
>>>>>>> origin/Dev

namespace WebApplication1.Models
{
	public class CategoryPicture
	{
		[Key]
		public int idCategoryPicture { get; set; }
		public int idCategory { get; set; }
		public int idPicture { get; set; }

<<<<<<< HEAD
		public virtual Picture picture { get; set; }
=======
		[JsonIgnore]
        public virtual Picture picture { get; set; }
>>>>>>> origin/Dev
		public virtual Category category { get; set; }
	}
}
