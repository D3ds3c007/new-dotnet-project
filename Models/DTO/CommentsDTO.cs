using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
	public class CommentsDTO
	{
		[Key]
		public int idUser { get; set; }
		public int idPicture { get; set; }
		public string comment { get; set; }
		public Comments GetComments()
		{
			return new Comments
			{
				idUser = this.idUser,
				idPicture = this.idPicture,
				comment = this.comment
			};
		}
	}
}
