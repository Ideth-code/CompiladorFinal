using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlueMoon
{
    public class AnLex
    {
        
    
        //CODIGO ORIGINAL


        // Definir unas estructura para representar los tokens
        public struct Token
        {
          public TokenType Type;
          public string Value;
        }

        // Definir los tipos de tokens posibles
        public enum TokenType
        {
         Numero,
         Operador,
         Desconocido
        }

        // Definir los operadores válidos
        private List<string> operators = new List<string> { "+", "-", "*", "/" };

        // Método para analizar el código fuente
        public List<Token> Analyze(string sourceCode)
        {
         List<Token> tokens = new List<Token>();

        // Dividir el código en palabras separadas
          string[] lines = sourceCode.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

         foreach (string line in lines)
           {
        string[] words = line.Split(' ');

         foreach (string word in words)
         {
          Token token;
          switch (word)
          {
          case "+":
           case "-":
          case "*":
          case "/":
             token.Type = TokenType.Operador;
               break;
           default:
            if (IsNumeric(word))
           {
               token.Type = TokenType.Numero;
            }
              else
              {
                 token.Type = TokenType.Desconocido;
             }
               break;
           }
           token.Value = word;
             tokens.Add(token);
           }
          }
           return tokens;
        }

        // Método auxiliar para verificar si una cadena es un número
        private bool IsNumeric(string input)
        {
         return Regex.IsMatch(input, @"^\d+$");
        }


        


}
    
}



