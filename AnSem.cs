using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static BlueMoon.AnLex;
//using static BlueMoon.AnLex;

namespace BlueMoon
{
    public class AnSem
    {


        // CODIGO ORIGINAL


        private List<AnLex.Token> tokens;

        public string Analyze(List<AnLex.Token> tokens)
        {
            this.tokens = tokens;

            try
            {
                // Realizar el análisis semántico
                int result = EvaluateExpression();

                return "El resultado es: " + result.ToString();
            }
            catch (SemanticException ex)
            {
                return "Error semántico: " + ex.Message;
            }
            catch
            {
                return "Error semántico desconocido.";
            }
        }

        private int EvaluateExpression()
        {
            // Inicializar los valores
            int currentIndex = 0;
            int result = 0;

            // Evaluar la expresión
            result = EvaluateTerm(ref currentIndex);

            while (currentIndex < tokens.Count)
            {
                AnLex.Token currentToken = tokens[currentIndex];

                if (currentToken.Type == AnLex.TokenType.Operador && (currentToken.Value == "+" || currentToken.Value == "-"))
                {
                    currentIndex++;

                    int termValue = EvaluateTerm(ref currentIndex);

                    if (currentToken.Value == "+")
                    {
                        result += termValue;
                    }
                    else if (currentToken.Value == "-")
                    {
                        result -= termValue;
                    }
                }
            }

            return result;
        }

        private int EvaluateTerm(ref int currentIndex)
        {
            int factorValue = EvaluateFactor(ref currentIndex);

            while (currentIndex < tokens.Count)
            {
                AnLex.Token currentToken = tokens[currentIndex];

                if (currentToken.Type == AnLex.TokenType.Operador && (currentToken.Value == "*" || currentToken.Value == "/"))
                {
                    currentIndex++;

                    int factor = EvaluateFactor(ref currentIndex);

                    if (currentToken.Value == "*")
                    {
                        factorValue *= factor;
                    }
                    else if (currentToken.Value == "/")
                    {
                        if (factor == 0)
                        {
                            throw new SemanticException("División por cero.");
                        }

                        factorValue /= factor;
                    }
                }
                else
                {
                    break;
                }
            }

            return factorValue;
        }

        private int EvaluateFactor(ref int currentIndex)
        {
            if (currentIndex < tokens.Count)
            {
                AnLex.Token currentToken = tokens[currentIndex];

                if (currentToken.Type == AnLex.TokenType.Numero)
                {
                    currentIndex++;
                    return int.Parse(currentToken.Value);
                }
            }

            throw new SemanticException("Factor inválido.");
        }
    }

    public class SemanticException : System.Exception
    {
        public SemanticException(string message) : base(message)
        {
        }

    }
}


