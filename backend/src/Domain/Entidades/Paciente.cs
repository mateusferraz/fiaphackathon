using Domain.Entidades;

namespace Domain.Entities
{
    public class Paciente : EntidadeBase
    {
        public string Nome { get; set; }
        public string Documento { get => documento; set => documento = FormatarDocumento(value); }
        public string Email { get; set; }
        public string Senha { get; set; }

        public virtual IEnumerable<Agendamento> Agendamentos { get; set; }

        private string documento;
        private string FormatarDocumento(string valor)
        {
            if (valor != null)
            {
                return valor.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
            }
            return null;
        }
    }
}
