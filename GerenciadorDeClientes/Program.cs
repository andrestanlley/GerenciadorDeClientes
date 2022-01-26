using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GerenciadorDeClientes
{
    internal class Program
    {
        [System.Serializable]
        struct Cliente
        {
            public string nome;
            public string email;
            public string cpf;
        }

        static List<Cliente> clientes = new List<Cliente>();

        enum Menu { Listagem = 1, Adicionar, Remover, Sair}
        static void Main(string[] args)
        {
            Carregar();
            bool finalizar = false;

            while (!finalizar) {
                Console.WriteLine("Sistema de clientes - Bem-vindo!");
                Console.WriteLine("\n1 - Listagem\n2 - Adicionar\n3 - Remover\n4 - Sair");
                Console.Write("\nSelecione: ");
                int intOp = int.Parse(Console.ReadLine());
                Menu opcao = (Menu)intOp;

                switch (opcao)
                {
                    case Menu.Listagem:
                        Listagem();
                        break;
                    case Menu.Adicionar:
                        Adicionar();
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Sair:
                        finalizar = true;
                        break;
                    default:
                        Console.WriteLine("Essa opção não existe! Tente novamente.");
                        Console.ReadLine();
                        break;
                }
                Console.Clear();
            }
        }

        static void Adicionar()
        {
            Cliente cliente = new Cliente();
            Console.WriteLine("\nCadastro de cliente");
            Console.Write("Nome do cliente: ");
            cliente.nome = Console.ReadLine();
            Console.Write("Email do cliente: ");
            cliente.email = Console.ReadLine();
            Console.Write("CPF do cliente: ");
            cliente.cpf = Console.ReadLine();
            clientes.Add(cliente);
            Salvar();

            Console.WriteLine("\nCadastro concluido! ENTER para voltar ao Menu.");
            Console.ReadLine();
        }

        static void Listagem()
        {
            if(clientes.Count > 0) {
                Console.WriteLine("Lista de clientes: ");
                int i = 1000;
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine("\n========================\n");
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome: {cliente.nome}");
                    Console.WriteLine($"Email: {cliente.email}");
                    Console.WriteLine($"CPF: {cliente.cpf}");
                    i++;
                }
            }
            else
            {
                Console.WriteLine("Nenhum cliente cadastrado.");
            }

            Console.WriteLine("\n//Enter para continuar.");
            Console.ReadLine();
        }

        static void Remover()
        {
            Listagem();
            Console.Write("Digite o ID a ser removido: ");
            int id = int.Parse(Console.ReadLine())-1000;
            Console.WriteLine(id >= 0);
            if (id >= 0 && id < clientes.Count)
            {
                clientes.RemoveAt(id);
                Salvar();
                Console.WriteLine($"Cliente {id} removido com sucesso.\nEnter para retornar ao Menu.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("ID digitado é invalido.");
                Console.ReadLine();
            }
        }

        static void Salvar()
        {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(stream, clientes);

            stream.Close();
        }

        static void Carregar()
        {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);
            try
            {
                BinaryFormatter encoder = new BinaryFormatter();

                clientes = (List<Cliente>)encoder.Deserialize(stream);

                if(clientes == null)
                {
                    clientes = new List<Cliente>();
                }

                
            }
            catch(Exception ex)
            {
                clientes = new List<Cliente>();
            }
            stream.Close();
        }
    }
}
