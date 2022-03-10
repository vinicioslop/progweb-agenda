using System;
using System.Linq;
using Agenda.db;

namespace Agenda
{
    class Program
    {
        static void Main(string[] args)
        {
            bool sair = false;
            while (!sair)
            {
                string opcao = SelecionaOpcaoEmMenu();

                Console.WriteLine($"Opção selecionada: {opcao}\n");

                switch (opcao)
                {
                    case "L":
                        ListarTodosContatos();
                        break;

                    case "T":
                        Top5Contatos();
                        break;

                    case "C":
                        ConsultarContatosPorCodigo();
                        break;

                    case "N":
                        ConsultarContatosPorNome();
                        break;

                    case "I":
                        IncluirContato();
                        break;

                    case "S":
                        Console.WriteLine("- Sair");
                        sair = true;
                        break;

                    default:
                        Console.WriteLine($"Opção não reconhecida.");
                        break;
                }

                Console.Write("\nPressione uma tecla para continuar...");
                Console.ReadKey();
            }
        }

        static string SelecionaOpcaoEmMenu()
        {
            Console.Clear();
            Console.WriteLine("-- Agenda --\n");
            Console.WriteLine("[L]istar todos os contatos");
            Console.WriteLine("[T]op 5 contatos");
            Console.WriteLine("Consultar contatos por [C]ódigo");
            Console.WriteLine("Consultar contatos por [N]ome");
            Console.WriteLine("[I]ncluir contato");
            Console.WriteLine("[S]air");
            Console.Write("\nDigite a sua opção: ");

            string entrada = Console.ReadLine().ToUpper().Trim();
            return entrada.Length < 2 ? entrada : entrada.Substring(0, 1);
        }

        static void ListarTodosContatos()
        {
            Console.WriteLine("- Listar todos os contatos:");

            // Continue daqui
            using (var _db = new agendaContext())
            {
                var contatos = _db.Contato.ToList<Contato>();

                Console.WriteLine();
                foreach (var contato in contatos)
                {
                    Console.WriteLine($"[{contato.Id}] - Nome: {contato.Nome}, Telefone: {contato.Fone}");
                }

                int quantidadeContatos = contatos.Count();
                Console.WriteLine($"\nEncontrados {quantidadeContatos} contato(s).");
            }
        }

        static void Top5Contatos()
        {
            Console.WriteLine("- Top 5 contatos:");

            // Continue daqui
            using (var _db = new agendaContext())
            {
                var contatos = _db.Contato.Take(5).ToList<Contato>();

                Console.WriteLine();
                foreach (var contato in contatos)
                {
                    Console.WriteLine($"[{contato.Id}] - Nome: {contato.Nome}, Telefone: {contato.Fone}");
                }

                int quantidadeContatos = contatos.Count();
                Console.WriteLine($"\nEncontrados {quantidadeContatos} contato(s).");
            }
        }

        static void ConsultarContatosPorCodigo()
        {
            Console.WriteLine("- Consultar contatos por Código:");

            // Continue daqui
            Console.Write("\nInforme o ID do contato a pesquisar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            if (id < 0)
            {
                Console.WriteLine("\nID informado é inválido.");
                return;
            }

            using (var _db = new agendaContext())
            {
                var contato = _db.Contato.Find(id);

                if (contato == null)
                {
                    Console.WriteLine("\nContato inexistente.");
                    return;
                }

                Console.WriteLine($"\n[{contato.Id}] - Nome: {contato.Nome}, Telefone: {contato.Fone}");
            }
        }

        static void ConsultarContatosPorNome()
        {
            Console.WriteLine("- Consultar contatos por Nome:");

            // Continue daqui
            Console.Write("\nDigite o Nome desejado: ");
            string nome = (Console.ReadLine() ?? "").Trim();

            using (var _db = new agendaContext())
            {
                var contatos = _db.Contato.Where(c => c.Nome.Contains(nome)).OrderBy(c => c.Id).ToList<Contato>();

                Console.WriteLine();
                foreach (var contato in contatos)
                {
                    Console.WriteLine($"[{contato.Id}] - Nome: {contato.Nome}, Telefone: {contato.Fone}");
                }

                int quantidadeContatos = contatos.Count();
                Console.WriteLine($"\nEncontrados {quantidadeContatos} contato(s).");
            }
        }

        static void IncluirContato()
        {
            Console.WriteLine("- Incluir contato:");

            // Continue daqui
            Console.WriteLine("\nInforme o nome do novo contato: ");
            string nome = (Console.ReadLine() ?? "").Trim();
            Console.WriteLine("Informe o telefone do novo contato: ");
            string telefone = (Console.ReadLine() ?? "").Trim();
            Console.WriteLine("Informe a quantidade de estrelas do novo contato: ");
            int estrelas = Convert.ToInt32((Console.ReadLine() ?? "").Trim());

            if (String.IsNullOrEmpty(nome))
            {
                Console.WriteLine("\nNão é possível adicionar um novo contato sem nome.");
                return;
            }

            using (var _db = new agendaContext())
            {
                var contato = new Contato
                {
                    Nome = nome,
                    Fone = telefone,
                    Estrelas = estrelas
                };

                _db.Contato.Add(contato);
                _db.SaveChanges();

                Console.WriteLine($"\n[{contato.Id}] - Nome: {contato.Nome}, Telefone: {contato.Fone}");
            }
        }
    }
}
