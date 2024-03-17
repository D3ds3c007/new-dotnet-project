using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTO
{
	public class UserBioDTO
	{
		[Key]
		public int idUser { get; set; }
		public string bio { get; set; }
		public User GetUser()
		{
			return new User
			{
				idUser = this.idUser,
				bio = this.bio
			};
		}
	}
}
