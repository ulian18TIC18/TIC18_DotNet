// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;

public class Produto
{
    public int Codigo { get; }
    public string Nome { get; }
    public int Quantidade { get; set; }
    public decimal Preco { get; }

    public Produto(int codigo, string nome, int quantidade, decimal preco)
    {
        if (codigo <= 0 || string.IsNullOrEmpty(nome) || quantidade < 0 || preco <= 0)
        {
            throw new ArgumentException("Os dados informados para o produto são inválidos.");
        }

        Codigo = codigo;
        Nome = nome;
        Quantidade = quantidade;
        Preco = preco;
    }
}

public class Estoque
{
    private List<Produto> _produtos;

    public Estoque()
    {
        _produtos = new List<Produto>();
    }

    public void CadastrarProduto(int codigo, string nome, int quantidade, decimal preco)
    {
        try
        {
            var novoProduto = new Produto(codigo, nome, quantidade, preco);
            _produtos.Add(novoProduto);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void AtualizarEstoque(int codigo, int quantidade)
    {
        try
        {
            var produto = _produtos.FirstOrDefault(p => p.Codigo == codigo);

            if (produto == null)
            {
                throw new ArgumentException("Não foi encontrado nenhum produto com o código informado.");
            }

            if (produto.Quantidade < quantidade)
            {
                throw new ArgumentException("A quantidade informada é insuficiente para a saída.");
            }

            produto.Quantidade -= quantidade;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public List<Produto> GerarRelatorio(Func<Produto, bool> criterio)
    {
        try
        {
            return _produtos.Where(criterio).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public decimal CalcularValorTotal()
    {
        try
        {
            return _produtos.Sum(p => p.Quantidade * p.Preco);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return 0;
        }
    }
}

public class Program
{
    public static void Main()
    {
        var estoque = new Estoque();

        // tester
        estoque.CadastrarProduto(001, "Sabonete", 10, 5.00m);
        estoque.CadastrarProduto(002, "Bola", 15, 8.00m);

        Console.WriteLine("Cadastro de Novos Produtos: ");

        char respostaCadastro;
        do
        {
            Console.Write("Deseja cadastrar um novo produto? (Digite 's' para sim, 'n' para finalizar): ");
            respostaCadastro = char.ToLower(Console.ReadKey().KeyChar);
            Console.WriteLine();

            if (respostaCadastro == 's')
            {
                Console.Write("Código do Produto: ");
                int codigo = int.Parse(Console.ReadLine());

                Console.Write("Nome do Produto: ");
                string nome = Console.ReadLine();

                Console.Write("Quantidade em Estoque: ");
                int quantidade = int.Parse(Console.ReadLine());

                Console.Write("Preço Unitário em R$: ");
                decimal preco = decimal.Parse(Console.ReadLine());

                estoque.CadastrarProduto(codigo, nome, quantidade, preco);
            }

        } while (respostaCadastro == 's');

        estoque.AtualizarEstoque(1, 3);

        var relatorioQuantidadeLimite = estoque.GerarRelatorio(p => p.Quantidade < 10);
        var relatorioProdutosValorMinMax = estoque.GerarRelatorio(p => p.Preco >= 5.0m && p.Preco <= 10.0m);
        var valorTotalEstoque = estoque.CalcularValorTotal();

        Console.WriteLine("\nRelatório de produtos com quantidade abaixo de 10:");
        relatorioQuantidadeLimite?.ForEach(p => Console.WriteLine($"{p.Nome} - Quantidade: {p.Quantidade}"));

        Console.WriteLine("\nRelatório de produtos com valor entre 5.0 e 10.0:");
        relatorioProdutosValorMinMax?.ForEach(p => Console.WriteLine($"{p.Nome} - Preço em R$: {p.Preco}"));

        Console.WriteLine($"\nValor total do estoque em R$: {valorTotalEstoque}");
    }
}