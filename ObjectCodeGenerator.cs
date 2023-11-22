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
            // Create a dynamic assembly and module 
            AssemblyName assemblyName = new AssemblyName("CalculatorAssembly");
            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("CalculatorModule");

            // Define a dynamic type to hold our main method
            TypeBuilder typeBuilder = moduleBuilder.DefineType("CalculatorType", TypeAttributes.Public);

            // Define a static method for our calculator code
            MethodBuilder methodBuilder = typeBuilder.DefineMethod(
                "Calculate",
                MethodAttributes.Public | MethodAttributes.Static,
                typeof(void),
                Type.EmptyTypes);

            // Get an ILGenerator and emit the MSIL 
            ILGenerator il = methodBuilder.GetILGenerator();

            foreach (var token in tokens)
            {
                // Generate MSIL based on the token type
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

            // Assuming the last result is the one we're interested in, pop it off the stack and print it.
            il.EmitCall(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }), null);
            il.Emit(OpCodes.Ret);

            // Create the type and method
            Type calculatorType = typeBuilder.CreateType();
            var calculateMethod = calculatorType.GetMethod("Calculate");

            // Execute the method
            calculateMethod.Invoke(null, null);


            assemblyBuilder.SetEntryPoint(methodBuilder, PEFileKinds.ConsoleApplication);
            //Type calculatorType = typeBuilder.CreateType();
           // var calculateMethod = calculatorType.GetMethod("Calculate");

            // Save the assembly as an executable file
            string assemblyFileName = "CalculatorAssembly.exe";
            assemblyBuilder.Save(assemblyFileName);

            // ... existing code to invoke the Calculate method

            // Inform the user that the executable has been created
            Console.WriteLine($"Executable '{assemblyFileName}' has been generated.");



        }
    }
}
