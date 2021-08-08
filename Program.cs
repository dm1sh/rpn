using System;
using System.Collections.Generic;

namespace calculator
{
  class Program
  {
    static void Main(string[] args)
    {
      if (args.Length == 0)
      {
        string input = readREPL();
        while (input != null)
        {
          writeREPL(evaluate(input));
          input = readREPL();
        }
      }
      else
      {
        writeREPL(evaluate(args[0]));
      }
    }

    static private double evaluate(string s)
    {
      try
      {
        string[] expr = s.Split();
        Stack<double> st = new Stack<double>();

        for (int i = 0; i < expr.Length; i++)
        {
          object val = convertOne(expr[i]);

          if (val is double number)
            st.Push(number);
          else if (val is char oper)
          {
            try
            {
              double b = st.Pop();
              double a = st.Pop();
              switch (oper)
              {
                case '+':
                  st.Push(a + b);
                  break;
                case '-':
                  st.Push(a - b);
                  break;
                case '*':
                  st.Push(a * b);
                  break;
                case '/':
                  st.Push(a / b);
                  break;
                case '^':
                  st.Push(Math.Pow(a, b));
                  break;
                default:
                  throw new Exception("Got unknown operator");
              }
            }
            catch (InvalidOperationException)
            {
              throw new Exception(oper + " operation requires two operands");
            }
          }
          else throw new Exception("Stack corrupted");
        }

        return st.Pop();
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        Environment.Exit(1);
        return -1;
      }
    }

    static private object convertOne(string s)
    {
      double res;
      if (Double.TryParse(s, out res)) return res;
      else if (s.Length == 1) return s[0];
      else throw new Exception("Got multiple characters operator");
    }

    static private string readREPL()
    {
      Console.Write(">> ");
      string input = Console.ReadLine();

      switch (input)
      {
        case "exit":
        case "exit()":
        case "quit":
        case "quit()":
        case "q":
        case "":
          return null;
        default:
          return input;
      }
    }

    static private void writeREPL<T>(T o)
    {
      Console.WriteLine(o);
    }
  }
}
