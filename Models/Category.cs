using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
	public class Category
	{
		[Key]
		public int idCategory { get; set; }
		public string categoryName { get; set; }

		[JsonIgnore]
		public virtual ICollection<CategoryPicture> categoryPictures { get; set; }
	}
}
