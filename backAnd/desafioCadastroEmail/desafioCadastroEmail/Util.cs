﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemaEmail
{
    class Util
    {
        /// <summary>
        /// Mostrar a lista ao usuário
        /// </summary>
        /// <param name="frase"></param>
        /// <param name="lista"></param>
        public static void exibirLista(string frase, List<Usuario> lista)
        {
            Console.WriteLine(frase);
            foreach (Usuario i in lista)
            {
                Console.WriteLine(i.Nome + " - " + i.Email);
            }
        }

        /// <summary>
        /// Alimentando a lista
        /// </summary>
        /// <param name="lista"></param>
        public static void carregarListas(List<Usuario> lista)
        {
            StreamReader leitor;
            string nomeArquivo = "dadosUsuarios.dat";
            string[] vetorDados;
            try
            {
                leitor = new StreamReader(nomeArquivo);
                do
                {
                    vetorDados = leitor.ReadLine().Split(";"); //nome;email
                    lista.Add(new Usuario(vetorDados[0], vetorDados[1]));
                } while (!leitor.EndOfStream); //repete-se a leitura da linha até que o leitor não chegue no final do arquivo
            }
            catch (Exception e)
            {
                Console.WriteLine("Arquivo de usuários com problemas ou inexistente");
            }
        }

        /// <summary>
        /// Método para comparar os usuários que ja estão cadastrados
        /// </summary>
        /// <param name="objeto"></param>
        /// <param name="lista"></param>
        /// <returns></returns>
        private static bool jaCadastrado(Usuario objeto, List<Usuario> lista)
        {
            foreach (Usuario i in lista)
            {
                if (objeto.Email.Equals(i.Email))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Método para gravar no arquivo
        /// </summary>
        /// <param name="objeto"></param>
        private static void gravarArquivo(Usuario objeto)
        {
            try
            {
                StreamWriter escritor = new StreamWriter("dadosUsuarios.dat", true);

                escritor.WriteLine(objeto.Nome + ";" + objeto.Email);
                escritor.Flush();
                escritor.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Arquivo de usuários com problemas");
            }
        }

        /// <summary>
        /// Método para cadastro de usuário
        /// </summary>
        /// <param name="frase"></param>
        /// <param name="lista"></param>
        public static void cadastrarUsuario(string frase, List<Usuario> lista)
        {
            Console.WriteLine(frase);
            string nome;
            string[] vetorDados;
            do
            {
                Console.Write("Digite o nome completo: ");
                nome = Console.ReadLine();
                vetorDados = nome.Split(' ');
                if (vetorDados.Length < 2)
                {
                    Console.WriteLine("Digite pelo menos nome e sobrenome");
                }
                else
                {
                    break;
                }
            } while (true);

            Usuario objeto = new Usuario(nome);

            if (!Util.jaCadastrado(objeto, lista))
            {
                lista.Add(objeto);
                //gravar no arquivo
                Util.gravarArquivo(objeto);
            }
            else
            {
                Console.WriteLine("Usuário com esse email já está na base de dados!");
            }
        }
    }
}