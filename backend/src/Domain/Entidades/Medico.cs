namespace Domain.Entidades
{
    public class Medico : EntidadeBase
    {
        public string Nome { get; set; }
        public string Documento { get => documento; set => documento = FormatarDocumento(value); }
        public string Crm { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public virtual IEnumerable<Agenda> Agendas { get; set; }

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
