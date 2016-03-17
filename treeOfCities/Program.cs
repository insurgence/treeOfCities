using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

sealed class Node<T>
{
    public T Data;  // Payload.

    public Node<T> Next;
    public Node<T> Child;
}

sealed class Tree<T> : IEnumerable<T>
{
    public Node<T> Root;

    public Node<T> AddChild(Node<T> parent, T data)
    {
        parent.Child = new Node<T>
        {
            Data = data,
            Next = parent.Child
        };

        return parent.Child;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return enumerate(Root).GetEnumerator();
    }

    private IEnumerable<T> enumerate(Node<T> root)
    {
        for (var node = root; node != null; node = node.Next)
        {
            yield return node.Data;

            foreach (var data in enumerate(node.Child))
                yield return data;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

static class DemoUtil
{
    public static void Print(this object self)
    {
        Console.WriteLine(self);
    }

    public static void Print(this string self)
    {
        Console.WriteLine(self);
    }

    public static void Print<T>(this IEnumerable<T> self)
    {
        int Kludge = 1;
        foreach (var item in self)
            if (Kludge == 1)
            {
                Console.WriteLine(item + ":");
                ++Kludge;
            }
            else
            {
                Console.WriteLine(" => " + item);
            }
    }
}

static class Rec
{
    public static string[] myRec(string[] childrens)
    {
        return childrens;
    }
}

namespace treeOfCities
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\src.txt";
            
            if (!File.Exists(path))
            {
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
            else
            {
                List<string> touch  = new List<string>();
                var tree = new Tree<string>();
                string[] temp;

                Console.Write("Write country for search: ");
                string country = Console.ReadLine();

                List<string> lines = File.ReadLines(path).ToList();

                foreach(var line in lines)
                {
                    if (line.Substring(0, country.Length).Contains(country))
                    {
                        string  stemp = line;
                                 temp = stemp.
                                            Substring(line.IndexOf(":") + 1, line.Length - line.IndexOf(":") - 1).
                                            Trim().
                                            Replace(" ", string.Empty).
                                            Split(',');

                        tree.Root = new Node<string> { Data = country };
                        foreach(var child in temp)
                            tree.AddChild(tree.Root, child);

                        Rec.myRec(temp);

                        break;
                    }
                }

                tree.Print();

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}


