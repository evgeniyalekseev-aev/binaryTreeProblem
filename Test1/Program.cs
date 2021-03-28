using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Test1
{
    
    class Tree<T>
        where T: IComparable, IComparable<T>
    {
        public Node<T> Root { get; private set; }
        public int Count { get; private set; }

        public void Add(T data)
        {
            if(Root == null)
            {
                Root = new Node<T>(data);
                Count = 1;
                return;
            }

            Root.Add(data);
            Count++;
        }

        public List<Node<T>> downNodes()
        {
            if (Root == null)
            {
                return new List<Node<T>>();
            }

            return downNodes(Root);
        }

        private List<Node<T>> downNodes(Node<T> node)
        {
            var list = new List<Node<T>>();
            if(node != null)
            {
                if(node.Left != null)
                {
                    list.AddRange(downNodes(node.Left));
                }
                else if(node.Right == null)
                {
                    if(!list.Contains(node))
                        list.Add(node);
                }

                if (node.Right != null)
                {
                    list.AddRange(downNodes(node.Right));
                }
                else if (node.Left == null)
                {
                    if(!list.Contains(node))
                        list.Add(node);
                }
            }
            return list;
        }
        
    }
    class Node<T> : IComparable<T>, IComparable
        where T: IComparable, IComparable<T>
    {
        public Node<T> Parent{get;set;}
        public T Data {get; set;}
        public Node<T> Left {get; private set;}
        public Node<T> Right {get; private set;}

        public Node(T data)
        {
            Data = data;
        }

        public void Add(T data)
        {
            var node = new Node<T>(data);

            if(node.Data.CompareTo(Data) == -1)
            {
                if(Left == null)
                {
                    Left = node;
                    node.Parent = this;
                }
                else
                {
                    Left.Add(data);
                }
            }
            else
            {
                if(Right == null)
                {
                    Right = node;
                    node.Parent = this;
                }
                else
                {
                    Right.Add(data);
                }
            }
        }

        public int CompareTo(object obj)
        {
            if(obj is Node<T> item)
            {
                return Data.CompareTo(item);
            }
            else
            {
                throw new ArgumentException("Несовпадение типов");
            }
        }

        public int CompareTo(T other)
        {
            return Data.CompareTo(other);
        }

    }
    class Program
    {

        static void Main(string[] args)
        {
            var tree = new Tree<float>();

            tree.Add(100);
            tree.Add(200);
            tree.Add(300);
            tree.Add(400);
            tree.Add(500);
            tree.Add(700);
            tree.Add(800);
            tree.Add(925);
            tree.Add(950);
            tree.Add(1000);

            foreach (var item in tree.downNodes())
            {
                var currentNode = item;
                while(currentNode.Parent != null)
                {
                    currentNode.Data -= 0.1f;
                    currentNode.Parent.Data += 0.1f;
                    currentNode = currentNode.Parent;
                }
            }
            Console.WriteLine(Math.Round(tree.Root.Data, 1));
            Console.ReadLine();
        }
    }
}