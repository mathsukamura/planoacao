using System.Collections.Generic;

namespace apiplanoacao.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public ICollection<PlanoAcaoModel> PlanoAcaoResponsavel { get; set; }

        public ICollection<PlanoAcaoModel> PlanoacaoColaborador { get; set; }

        public ICollection<PlanoAcaoModel> PlanoAcaos { get; set; }
    }
}
