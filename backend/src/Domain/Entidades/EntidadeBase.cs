namespace Domain.Entidades
{
    public abstract class EntidadeBase
    {
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }

        protected EntidadeBase()
        {
            Id = Guid.NewGuid();
        }
    }
}
