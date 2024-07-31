namespace Application.Interfaces
{
    public interface IUnitOfWork
    {
        public IAgendaRepository AgendaRepository { get; }
        public IAgendamentoRepository AgendamentoRepository { get; }
        public IPacienteRepository PacienteRepository { get; }
        public IMedicoRepository MedicoRepository { get; }

        void Commit();
        void Rollback();
    }
}
