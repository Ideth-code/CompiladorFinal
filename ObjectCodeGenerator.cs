using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Reflection.Emit;
using System.Reflection;


namespace BlueMoon
{
    class ObjectCodeGenerator
    {
        public void GenerateObjectCode(List<AnLex.Token> tokens)
        {
            // ensamble dinamico y modulos
            AssemblyName assemblyName = new AssemblyName("ResultadosAss");
            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("ModuloResultados");
            
            TypeBuilder typeBuilder = moduleBuilder.DefineType("Operaciones", TypeAttributes.Public);

      
            MethodBuilder methodBuilder = typeBuilder.DefineMethod(
                "Calcula",
                MethodAttributes.Public | MethodAttributes.Static,
                typeof(void),
                Type.EmptyTypes);

            // MSIL 
            ILGenerator il = methodBuilder.GetILGenerator();

            foreach (var token in tokens)
            {
                //  Tipo de token
                if (token.Type == AnLex.TokenType.Numero)
                {
                    il.Emit(OpCodes.Ldc_I4, int.Parse(token.Value, CultureInfo.InvariantCulture));
                }
                else if (token.Type == AnLex.TokenType.Operador)
                {
                    switch (token.Value)
                    {
                        case "+":
                            il.Emit(OpCodes.Add);
                            break;
                        case "-":
                            il.Emit(OpCodes.Sub);
                            break;
                        case "*":
                            il.Emit(OpCodes.Mul);
                            break;
                        case "/":
                            il.Emit(OpCodes.Div);
                            break;
                    }
                }
            }
            
            il.EmitCall(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }), null);
            il.Emit(OpCodes.Ret);

            // Create the type and method
            Type calculatorType = typeBuilder.CreateType();
            var calculateMethod = calculatorType.GetMethod("Calculate");

            // ejecutar metodo
            calculateMethod.Invoke(null, null);


            assemblyBuilder.SetEntryPoint(methodBuilder, PEFileKinds.ConsoleApplication);
           
            string assemblyFileName = "ResultadosAss.exe";
            assemblyBuilder.Save(assemblyFileName);

 
            Console.WriteLine($"Executable '{assemblyFileName}' generado.");



        }
    }
}
