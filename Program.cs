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
        string input = REPL.read();
        while (input != null)
        {
          handleExpression(input);
          input = REPL.read();
        }
      }
      else
        handleExpression(args[0]);

    }

    static private void handleExpression(string s)
    {
      try
      {
        RPN calc = new RPN(s);
        REPL.write(calc.evaluate());
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
    }
  }

  class RPN
  {
    Stack<double> st = new Stack<double>();
    string[] tokens;

    public RPN(string s)
    {
      tokens = s.Split();
    }
    public RPN(string[] ss)
    {
      tokens = new string[ss.Length];
      ss.CopyTo(tokens, 0);
    }

    public double evaluate()
    {

      for (int i = 0; i < tokens.Length; i++)
      {
        try
        {
          object val = convertOne(i);

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
                  throw new Exception("got unknown operator");
              }
            }
            catch (InvalidOperationException)
            {
              throw new Exception("operator requires 2 arguments");
            }
          }
        }
        catch (Exception e)
        {
          throw new Exception(positionPointer(i, 2) + ' ' + e.Message);
        }
      }

      if (st.Count != 1) throw new Exception("Expression is not finished");

      return st.Pop();
    }

    private string positionPointer(int pos, int gap)
    {
      string prepend = "";
      for (int i = 1; i <= gap && pos - i >= 0; i++)
        prepend = tokens[pos - i] + ' ' + prepend;
      if (pos > gap) prepend = "... " + prepend;

      string append = " ";
      for (int i = 1; i <= gap && pos + i < tokens.Length; i++)
        append += tokens[pos + i] + ' ';

      if (pos < tokens.Length - 2) append += "...";

      string arrow = "^".PadLeft(prepend.Length + 1);

      return prepend + tokens[pos] + append + '\n' + arrow;
    }

    private object convertOne(int i)
    {
      string s = tokens[i];
      double res;
      if (Double.TryParse(s, out res)) return res;
      else if (s.Length == 1) return s[0];
      else throw new Exception("got multiple characters operator");
    }
  }

  class REPL
  {
    static public string read()
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

    static public void write<T>(T o)
    {
      Console.WriteLine(o);
    }
  }
}
