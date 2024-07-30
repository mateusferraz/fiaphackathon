using Application.Interfaces;
using Domain.Entidades;

namespace Infrastructure.Repositories
{
    public class UserRepository: BaseRepository<Medico>, IUserRepository
    {
        public UserRepository(DataBaseContext context) : base(context) { }
    }
}
