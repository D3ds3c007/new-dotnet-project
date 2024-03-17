using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTO
{
	public class UserPdpPathDTO
	{
		[Key]
		public int idUser { get; set; }
		public string pdpPath { get; set; }
		public User GetUser()
		{
			return new User
			{
				idUser = this.idUser,
				pdpPath = this.pdpPath
			};
		}
	}
}
