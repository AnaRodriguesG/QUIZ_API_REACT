using System.ComponentModel.DataAnnotations.Schema;

namespace QUIZ_API_REACT.Entities
{
    [Table("usuario")]
    public class User
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string FotoPerfil { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
    }
}
