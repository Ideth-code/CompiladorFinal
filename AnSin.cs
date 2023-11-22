using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueMoon
{
    public class AnSin
    {


              // CODIGO ORIGINAL


              private List<AnLex.Token> tokens;
              private int currentIndex;
              private AnLex.Token currentToken;

              public string Analyze(List<AnLex.Token> tokens)
              {
                  this.tokens = tokens;
                  currentIndex = 0;
                  currentToken = tokens[currentIndex];


                  try
                  {
                      // Comenzamos con el símbolo inicial de la gramática
                      Expression();

                      return "Análisis sintáctico exitoso. El código es válido.";
                  }
                  catch (SyntaxException ex)
                  {
                      return "Error de sintaxis: " + ex.Message;
                  }
                  catch
                  {
                      return "Error de sintaxis desconocido.";
                  }
              }

              private void NextToken()
              {
                  currentIndex++;
                  if (currentIndex < tokens.Count)
                      currentToken = tokens[currentIndex];
              }

              private void Expression()
              {
                  Term();
                  ExpressionPrime();
              }

              private void ExpressionPrime()
              {

                  if (currentToken.Type == AnLex.TokenType.Operador && (currentToken.Value == "+" || currentToken.Value == "-"))
                  {
                      NextToken();
                      Term();
                      ExpressionPrime();
                  }

              }

              private void Term()
              {
                  Factor();
                  TermPrime();
              }

              private void TermPrime()
              {
                  if (currentToken.Type == AnLex.TokenType.Operador && (currentToken.Value == "*" || currentToken.Value == "/"))
                  {
                      NextToken();
                      Factor();
                      TermPrime();
                  }
              }

              private void Factor()
              {
                  if (currentToken.Type == AnLex.TokenType.Numero)
                  {
                      NextToken();
                  }
                  else
                  {
                      throw new SyntaxException("Se esperaba un número.");
                  }
              }
          }

          public class SyntaxException : System.Exception
          {
              public SyntaxException(string message) : base(message)
              {
              }


          


}
}
