using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueMoon
{
    public class CodInter
    {
       
        
        // CODIGO ORIGINAL

        public string Generate(List<AnLex.Token> tokens)
                {
                    string intermediateCode = "";

                    foreach (var token in tokens)
                    {
                        if (token.Type == AnLex.TokenType.Numero)
                        {
                            intermediateCode += $"LOAD {token.Value}\n";
                        }
                        else if (token.Type == AnLex.TokenType.Operador)
                        {
                            intermediateCode += $"{GetOperatorInstruction(token.Value)}\n";
                        }
                        else if (token.Type == AnLex.TokenType.Desconocido)
                        {
                            intermediateCode += "STORE\n";
                        }
                    }

                    return intermediateCode;
                }

                // Método auxiliar para obtener la instrucción de operador correspondiente
                private string GetOperatorInstruction(string operatorToken)
                {
                    switch (operatorToken)
                    {
                        case "+":
                            return "SUM";
                        case "-":
                            return "RES";
                        case "*":
                            return "MUL";
                        case "/":
                            return "DIV";
                        default:
                            return "";
                    }
                }

        
    }
}










/*
 modifica el codigo del analizador lexico para que se creen dos sintaxis: "Imprime" 
 para mostrar cadenas de texto y "Calcula" para realizar operaciones matematicas basicas. ejemplo (Imprime Hola mundo), (Calcula 5+ 5)
     */
