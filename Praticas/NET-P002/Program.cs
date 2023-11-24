using System;
using System.Collections.Generic;
using System.Linq;

class GerenciadorTarefas
{
    class Tarefa
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataVencimento { get; set; }
        public bool EstaConcluida { get; set; }

        public Tarefa(string titulo, string descricao, DateTime dataVencimento)
        {
            Titulo = titulo;
            Descricao = descricao;
            DataVencimento = dataVencimento;
            EstaConcluida = false;
        }
    }

    static List<Tarefa> tarefas = new List<Tarefa>();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("1: Adicionar Tarefa");
            Console.WriteLine("2: Visualizar Tarefas");
            Console.WriteLine("3: Concluir Tarefa");
            Console.WriteLine("4: Excluir Tarefa");
            Console.WriteLine("5: Pesquisar Tarefas");
            Console.WriteLine("6: Estatísticas");
            Console.WriteLine("7: Sair");
            Console.Write("Escolha uma das opção anteriores: ");

            if (int.TryParse(Console.ReadLine(), out int escolha))
            {
                switch (escolha)
                {
                    case 1:
                        AdicionarTarefa();
                        break;
                    case 2:
                        VisualizarTarefas();
                        break;
                    case 3:
                        ConcluirTarefa();
                        break;
                    case 4:
                        ExcluirTarefa();
                        break;
                    case 5:
                        PesquisarTarefas();
                        break;
                    case 6:
                        ExibirEstatisticas();
                        break;
                    case 7:
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida. Tente novamente.");
            }
        }
    }

    static void AdicionarTarefa()
    {
        Console.Write("Digite o título da tarefa: ");
        string titulo = Console.ReadLine();
        Console.Write("Digite a descrição: ");
        string descricao = Console.ReadLine();
        Console.Write("Digite a data de vencimento em dd/MM/yyyy: ");

        if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dataVencimento))
        {
            tarefas.Add(new Tarefa(titulo, descricao, dataVencimento));
            Console.WriteLine("Tarefa adicionada com sucesso.");
        }
        else
        {
            Console.WriteLine("Formato de data inválida.");
        }
    }

    static void VisualizarTarefas()
    {
        Console.WriteLine("Tarefas Pendentes:");
        ImprimirTarefas(tarefas.Where(t => !t.EstaConcluida));

        Console.WriteLine("\nTarefas Concluídas:");
        ImprimirTarefas(tarefas.Where(t => t.EstaConcluida));
    }

    static void ImprimirTarefas(IEnumerable<Tarefa> tarefasParaImprimir)
    {
        foreach (var tarefa in tarefasParaImprimir)
        {
            Console.WriteLine($"{tarefa.Titulo} - {tarefa.Descricao} - {tarefa.DataVencimento:dd/MM/yyyy} - {(tarefa.EstaConcluida ? "Concluída" : "Pendente")}");
        }
    }

    static void ConcluirTarefa()
    {
        Console.Write("Digite o título da tarefa a ser concluída: ");
        string titulo = Console.ReadLine();
        var tarefa = tarefas.FirstOrDefault(t => t.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));

        if (tarefa != null)
        {
            tarefa.EstaConcluida = true;
            Console.WriteLine("Marcada como concluída.");
        }
        else
        {
            Console.WriteLine("Tarefa não encontrada.");
        }
    }

    static void ExcluirTarefa()
    {
        Console.Write("Digite o título da tarefa a ser excluída: ");
        string titulo = Console.ReadLine();
        var tarefa = tarefas.FirstOrDefault(t => t.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));

        if (tarefa != null)
        {
            tarefas.Remove(tarefa);
            Console.WriteLine("Tarefa excluída com sucesso.");
        }
        else
        {
            Console.WriteLine("Tarefa não encontrada.");
        }
    }

    static void PesquisarTarefas()
    {
        Console.Write("Digite uma palavra para pesquisa: ");
        string palavraChave = Console.ReadLine();

        var resultadosPesquisa = tarefas.Where(t => t.Titulo.Contains(palavraChave, StringComparison.OrdinalIgnoreCase) ||
                                             t.Descricao.Contains(palavraChave, StringComparison.OrdinalIgnoreCase));

        Console.WriteLine("Resultados da Pesquisa:");
        ImprimirTarefas(resultadosPesquisa);
    }

    static void ExibirEstatisticas()
    {
        int totalTarefas = tarefas.Count;
        int tarefasConcluidas = tarefas.Count(t => t.EstaConcluida);
        int tarefasPendentes = totalTarefas - tarefasConcluidas;

        if (totalTarefas > 0)
        {
            var tarefaMaisAntiga = tarefas.OrderBy(t => t.DataVencimento).First();
            var tarefaMaisRecente = tarefas.OrderByDescending(t => t.DataVencimento).First();

            Console.WriteLine($"Total de Tarefas: {totalTarefas}");
            Console.WriteLine($"Tarefas Concluídas: {tarefasConcluidas}");
            Console.WriteLine($"NúTarefas Pendentes: {tarefasPendentes}");
            Console.WriteLine($"Tarefa Mais Antiga: {tarefaMaisAntiga.Titulo} - {tarefaMaisAntiga.DataVencimento:dd/MM/yyyy}");
            Console.WriteLine($"Tarefa Mais Recente: {tarefaMaisRecente.Titulo} - {tarefaMaisRecente.DataVencimento:dd/MM/yyyy}");
        }
        else
        {
            Console.WriteLine("Não há tarefas para exibir.");
        }
    }
}
