using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private AgendaRepository _agendaRepository;
        private AgendamentoRepository _agendamentoRepository;
        private PacienteRepository _pacienteRepository;
        private MedicoRepository _medicoRepository;
        private DataBaseContext _context;
        public UnitOfWork(DataBaseContext context)
        {
            _context = context;
        }
        public IPacienteRepository PacienteRepository
        {
            get
            {
                if (_pacienteRepository == null)
                {
                    _pacienteRepository = new PacienteRepository(_context);
                }
                return _pacienteRepository;
            }
        }

        public IAgendamentoRepository AgendamentoRepository
        {
            get
            {
                if (_agendamentoRepository == null)
                {
                    _agendamentoRepository = new AgendamentoRepository(_context);
                }
                return _agendamentoRepository;
            }
        }

        public IMedicoRepository MedicoRepository
        {
            get
            {
                if (_medicoRepository == null)
                {
                    _medicoRepository = new MedicoRepository(_context);
                }
                return _medicoRepository;
            }
        }

        public IAgendaRepository AgendaRepository
        {
            get
            {
                if (_agendaRepository == null)
                {
                    _agendaRepository = new AgendaRepository(_context);
                }
                return _agendaRepository;
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
        public void Rollback()
        {
            foreach (var entry in _context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified://??
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
            _context.Dispose();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
