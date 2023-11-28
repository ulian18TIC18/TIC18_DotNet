// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;

class Academia
{
    private List<Treinador> treinadores;
    private List<Cliente> clientes;

    public Academia()
    {
        treinadores = new List<Treinador>();
        clientes = new List<Cliente>();
    }

    public void AdicionarTreinador(Treinador treinador)
    {
        if (treinadores.Any(t => t.CpfTreinador == treinador.CpfTreinador || t.CrefTreinador == treinador.CrefTreinador))
        {
            throw new ArgumentException("CPF ou CREF já cadastrado para outro treinador.");
        }
        treinadores.Add(treinador);
    }

    public void AdicionarCliente(Cliente cliente)
    {
        if (clientes.Any(c => c.CpfCliente == cliente.CpfCliente))
        {
            throw new ArgumentException("CPF já cadastrado para outro cliente.");
        }
        clientes.Add(cliente);
    }

    public List<Treinador> ObterTreinadoresComIdadeEntre(int idadeMinima, int idadeMaxima)
    {
        DateTime hoje = DateTime.Today;
        return treinadores
            .Where(t => (hoje - t.DataNascimento).Days / 365 >= idadeMinima && (hoje - t.DataNascimento).Days / 365 <= idadeMaxima)
            .ToList();
    }

    public List<Cliente> ObterClientesComIdadeEntre(int idadeMinima, int idadeMaxima)
    {
        DateTime hoje = DateTime.Today;
        return clientes
            .Where(c => (hoje - c.DataNascimento).Days / 365 >= idadeMinima && (hoje - c.DataNascimento).Days / 365 <= idadeMaxima)
            .ToList();
    }

    public List<Cliente> ObterClientesComIMCMaiorQue(double valorIMC)
    {
        return clientes
            .Where(c => (c.PesoCliente / Math.Pow(c.AlturaCliente, 2)) > valorIMC)
            .OrderBy(c => (c.PesoCliente / Math.Pow(c.AlturaCliente, 2)))
            .ToList();
    }

    public List<Cliente> ObterClientesEmOrdemAlfabetica()
    {
        return clientes.OrderBy(c => c.Nome).ToList();
    }

    public List<Cliente> ObterClientesDoMaisVelhoParaMaisNovo()
    {
        return clientes.OrderByDescending(c => c.DataNascimento).ToList();
    }

    public List<Pessoa> ObterAniversariantesDoMes(int mes)
    {
        List<Pessoa> aniversariantes = new List<Pessoa>();

        aniversariantes.AddRange(treinadores.Where(t => t.DataNascimento.Month == mes).Cast<Pessoa>());
        aniversariantes.AddRange(clientes.Where(c => c.DataNascimento.Month == mes).Cast<Pessoa>());

        return aniversariantes;
    }
}

class Pessoa
{
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
}

class Treinador : Pessoa
{
    private string cpfTreinador;
    private string crefTreinador;

    public string CpfTreinador
    {
        get => cpfTreinador;
        set
        {
            if (!ValidarCPF(value))
            {
                throw new ArgumentException("CPF inválido.");
            }
            cpfTreinador = value;
        }
    }

    public string CrefTreinador
    {
        get => crefTreinador;
        set
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("CREF não pode ser vazio ou nulo.");
            }
            crefTreinador = value;
        }
    }

    private bool ValidarCPF(string cpf)
    {
        return cpf.Length == 11 && cpf.All(char.IsDigit);
    }
}

class Cliente : Pessoa
{
    private string cpfCliente;
    private double alturaCliente;
    private double pesoCliente;

    public string CpfCliente
    {
        get => cpfCliente;
        set
        {
            if (!ValidarCPF(value))
            {
                throw new ArgumentException("CPF inválido.");
            }
            cpfCliente = value;
        }
    }

    public double AlturaCliente
    {
        get => alturaCliente;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Altura deve ser maior que zero.");
            }
            alturaCliente = value;
        }
    }

    public double PesoCliente
    {
        get => pesoCliente;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Peso deve ser maior que zero.");
            }
            pesoCliente = value;
        }
    }

    private bool ValidarCPF(string cpf)
    {
        return cpf.Length == 11 && cpf.All(char.IsDigit);
    }
}

class Program
{
    static void Main()
    {
        Academia academia = new Academia();

        Treinador treinador1 = new Treinador
        {
            Nome = "João",
            DataNascimento = new DateTime(1980, 5, 15),
            CpfTreinador = "12345678901",
            CrefTreinador = "ABC123"
        };

        Treinador treinador2 = new Treinador
        {
            Nome = "Maria",
            DataNascimento = new DateTime(1990, 10, 20),
            CpfTreinador = "98765432101",
            CrefTreinador = "DEF456"
        };

        Cliente cliente1 = new Cliente
        {
            Nome = "Carlos",
            DataNascimento = new DateTime(1995, 3, 8),
            CpfCliente = "11122233344",
            AlturaCliente = 1.75,
            PesoCliente = 70
        };

        Cliente cliente2 = new Cliente
        {
            Nome = "Ana",
            DataNascimento = new DateTime(1985, 7, 12),
            CpfCliente = "55566677788",
            AlturaCliente = 1.60,
            PesoCliente = 60
        };

        try
        {
            academia.AdicionarTreinador(treinador1);
            academia.AdicionarTreinador(treinador2);
            academia.AdicionarCliente(cliente1);
            academia.AdicionarCliente(cliente2);

            List<Treinador> treinadoresComIdadeEntre = academia.ObterTreinadoresComIdadeEntre(30, 50);
            Console.WriteLine("Treinadores com idade entre 30 e 50 anos:");
            foreach (var treinador in treinadoresComIdadeEntre)
            {
                Console.WriteLine($"Nome: {treinador.Nome}, Idade: {CalcularIdade(treinador.DataNascimento)} anos");
            }

            List<Cliente> clientesComIdadeEntre = academia.ObterClientesComIdadeEntre(25, 40);
            Console.WriteLine("\nClientes com idade entre 25 e 40 anos:");
            foreach (var cliente in clientesComIdadeEntre)
            {
                Console.WriteLine($"Nome: {cliente.Nome}, Idade: {CalcularIdade(cliente.DataNascimento)} anos");
            }

            List<Cliente> clientesComIMCMaiorQue25 = academia.ObterClientesComIMCMaiorQue(25);
            Console.WriteLine("\nClientes com IMC maior que 25:");
            foreach (var cliente in clientesComIMCMaiorQue25)
            {
                Console.WriteLine($"Nome: {cliente.Nome}, IMC: {CalcularIMC(cliente.PesoCliente, cliente.AlturaCliente):F2}");
            }

            List<Cliente> clientesEmOrdemAlfabetica = academia.ObterClientesEmOrdemAlfabetica();
            Console.WriteLine("\nClientes em ordem alfabética:");
            foreach (var cliente in clientesEmOrdemAlfabetica)
            {
                Console.WriteLine($"Nome: {cliente.Nome}");
            }

            List<Cliente> clientesDoMaisVelhoParaMaisNovo = academia.ObterClientesDoMaisVelhoParaMaisNovo();
            Console.WriteLine("\nClientes do mais velho para o mais novo:");
            foreach (var cliente in clientesDoMaisVelhoParaMaisNovo)
            {
                Console.WriteLine($"Nome: {cliente.Nome}, Idade: {CalcularIdade(cliente.DataNascimento)} anos");
            }

            Console.WriteLine("\nAniversariantes do mês (Treinadores e Clientes):");
            List<Pessoa> aniversariantes = academia.ObterAniversariantesDoMes(5);
            foreach (var pessoa in aniversariantes)
            {
                Console.WriteLine($"Nome: {pessoa.Nome}, Data de Nascimento: {pessoa.DataNascimento:yyyy-MM-dd}");
            }

        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }

    static int CalcularIdade(DateTime dataNascimento)
    {
        DateTime hoje = DateTime.Today;
        int idade = hoje.Year - dataNascimento.Year;

        if (dataNascimento > hoje.AddYears(-idade))
        {
            idade--;
        }

        return idade;
    }

    static double CalcularIMC(double peso, double altura)
    {
        return peso / Math.Pow(altura, 2);
    }
}
