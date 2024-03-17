using Algoritimos.Models;
using System;

class Program
{
    static void Main()
    {
        string entrada = "Oradea-Zerind-71 Oradea-Sibiu-151 Zerind-Arad-75 Arad-Sibiu-140 Arad-Timisoara-118 Sibiu-Fagaras-99 Sibiu-RimnicuVilcea-80 Timisoara-Lugoj-111 Lugoj-Mehadia-70 Mehadia-Dobreta-75 Dobreta-Craiova-120 Craiova-Pitesti-138 Craiova-RimnicuVilcea-146 RimnicuVilcea-Pitesti-97 Pitesti-Bucharest-101 Fagaras-Bucharest-211 Bucharest-Urziceni-85 Bucharest-Giurgiu-90 Urziceni-Vaslui-142 Vaslui-lasi-92 lasi-Neamt-87 Urziceni-Hirsova-98 Hirsova-Eforie-80";
        
        var grafo = ConverterStringEmNo(entrada);

        Console.WriteLine("Ponto inicial:");
        var incio = Console.ReadLine();

        Console.WriteLine("Ponto final:");
        var fim = Console.ReadLine();

        Console.WriteLine("Tipo de busca (0 - largura   1 - profundidade):");
        bool tipoPesquisa = Console.ReadLine() == "0"; //True - largura   false - profundidade

        if (tipoPesquisa)
        {
            var buscarLargura = BuscaEmLargura(incio, fim, grafo);

            Console.WriteLine(string.Join(" - ", buscarLargura));     
        }
        else
        {
            var buscaProfundidade = BuscaEmProfundidade(incio, fim, grafo);

            Console.WriteLine(string.Join(" - ", buscaProfundidade));
        }

    }

    static List<No> ConverterStringEmNo(string grafo) => grafo.Split(' ').Select(s =>
    {
        var splited = s.Split('-');

        return new No
        {
            Ponto1 = splited[0],
            Ponto2 = splited[1],
            Gasto = int.Parse(splited[2]),
        };
    }).ToList();

    public static List<string> BuscaEmLargura(string inicio, string fim, List<No> grafo)
    {
        List<string> caminho = new List<string>
        {
            inicio
        };
        
        var atual = inicio;

        while(atual != fim)
        {
            var proximosNos = grafo.Where(x => x.Ponto1 == atual || x.Ponto2 == atual).ToList();

            foreach(var x in proximosNos)
            {
                if (!caminho.Any(y => x.Ponto1 == atual ? x.Ponto2 == y : x.Ponto1 == y))
                {
                    caminho.Add(x.Ponto1 == atual ? x.Ponto2 : x.Ponto1);

                    if (x.Ponto1 == fim || x.Ponto2 == fim)
                    {
                        return caminho;
                    }
                }
            }

            atual = caminho.ElementAt(caminho.IndexOf(atual) + 1);
        }

        return caminho;
    }

    public static List<string> BuscaEmProfundidade(string inicio, string fim, List<No> grafo)
    {
        List<string> caminho = new List<string>
        {
            inicio
        };

        List<string> fila = new List<string>
        {
            inicio
        };

        var atual = inicio;

        while(atual != fim)
        {
            var proximosNo = grafo.FirstOrDefault(x => (x.Ponto1 == atual || x.Ponto2 == atual) && (!caminho.Any(y => x.Ponto1 == atual ? y == x.Ponto2 : y == x.Ponto1)));

            if (proximosNo == null)
            {
                atual = caminho.ElementAt(caminho.IndexOf(atual) - 1);
            }
            else
            {
                caminho.Add(proximosNo.Ponto1 == atual ? proximosNo.Ponto2 : proximosNo.Ponto1);

                if (proximosNo.Ponto1 == fim || proximosNo.Ponto2 == fim)
                {
                    return caminho;
                }

                atual = caminho.Last();
            }

            
        }

        return caminho;
    }
}