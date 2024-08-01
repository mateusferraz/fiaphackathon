using Application.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Model
{
    public class LoginModel : IRequest<string>, IPersistable
    {
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatório")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
