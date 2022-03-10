using System;
using System.Collections.Generic;

namespace Agenda.db
{
    public partial class Contato
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Fone { get; set; }
        public int? Estrelas { get; set; }
    }
}
