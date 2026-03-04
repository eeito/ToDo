using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using TodoList;

namespace TodoList
{
    class Program
    {
        static void Main(string[] args)
        {
            using var context = new DataContext();
            context.Database.EnsureCreated();

            List<Tarefa> lista = context.Tarefas.ToList();
            var tarefa = context.Tarefas.ToList();
            context.Database.ExecuteSqlRaw("DELETE FROM sqlite_sequence WHERE name='Tarefas';");

            Console.Clear();
            Console.WriteLine("Lista: \n");
            foreach (var t in context.Tarefas.ToList())
            {
                Console.WriteLine($"{t.Id} - {t.Conteudo}");
            }

            while (true)
            {

                string perg = Console.ReadLine();

                switch (perg)
                {

                    case "list":
                        Console.Clear();
                        Console.WriteLine("Lista: \n");
                        foreach (var t in context.Tarefas.ToList())
                        {
                            Console.WriteLine($" \n {t.Id} - {t.Conteudo}");
                        }
                        break;

                    case "adicionar":
                        Console.Clear();
                        Adicionar();
                        break;
                    case "remover":
                        Console.Clear();
                        Remover();
                        break;


                }

                if (perg == "sair")
                {
                    break;
                }
            }

            void Adicionar()
            {
                Console.WriteLine("Adicionar?: \n");
                var tarefa = new Tarefa { Conteudo = Console.ReadLine() };
                context.Tarefas.Add(tarefa);
                context.SaveChanges();
                Console.WriteLine("Adicionado!(use list para ver sua lista)");

            }
            void Remover()
            {
                Console.WriteLine("Id?: \n");
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    var tarefa = context.Tarefas.Find(id);

                    if (tarefa != null)
                    {
                        context.Tarefas.Remove(tarefa);
                        context.SaveChanges();
                        Console.WriteLine("Removido!");
                    }
                    else
                    {
                        Console.WriteLine("Id não encontrado.");
                    }
                }

            }

        }
    }
}
