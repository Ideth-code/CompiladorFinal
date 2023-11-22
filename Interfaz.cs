using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.CSharp;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static BlueMoon.AnLex;

namespace BlueMoon
{
    public partial class Interfaz : Form
    {
        private AnLex AL;
        private AnSin ASin;
        private AnSem AnSem;
        private CodInter CodInter;
      //  private readonly string[] intermediateCode;

        // Variable para almacenar los tokens generados por el AnLex
        //private List<AnLex.Token> tokens;

        public Interfaz()
        {
            InitializeComponent();
            AL = new AnLex();
            ASin = new AnSin();
            AnSem = new AnSem();
            CodInter = new CodInter();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Interfaz_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void tTSB3_Click(object sender, EventArgs e)
        {
            // Analizador Lexico
            string sourceCode = txtCod.Text;
            List<AnLex.Token> tokens = AL.Analyze(sourceCode);

            txtResult.Text = string.Empty;
            foreach (AnLex.Token token in tokens)
            {
                txtResult.Text += $"Tipo: {token.Type}, Valor: {token.Value}\r\n";
            }

            // Analizador Sintactico
            string result = ASin.Analyze(tokens);
            txtResult2.Text = result;

            if (!result.StartsWith("Error de sintaxis"))
            {
                // Analizador Semantico
                string semanticResult = AnSem.Analyze(tokens);
                txtResult3.Text = semanticResult; // This line has been corrected to display the semantic result

                if (!semanticResult.StartsWith("Error semántico")) // The condition has been corrected to check for semantic error
                {
                    // Generating intermediate code
                    string intermediateCode = CodInter.Generate(tokens);
                    // Displaying intermediate code in the fourth TextBox
                    txtResult4.Text = intermediateCode;
                }
                else
                {
                    // Display the semantic error
                    txtResult4.Text = semanticResult; // Display the semantic error in the fourth TextBox
                }
            }
            else
            {
                // Display syntactic analysis result in the third TextBox
                txtResult3.Text = result;
                txtResult4.Clear(); // Clear the content of the fourth TextBox as there's a syntax error
            }

            //------------------------------------------------------- EXE
            //string sourceCode = txtCod.Text;
            //List<AnLex.Token> tokens = AL.Analyze(sourceCode);

            if (tokens == null)
            {
                MessageBox.Show("No hay tokens para guardar. Por favor, realice el análisis primero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string filePath = "resultados.txt"; // Nombre del archivo de salida.
                                                    // Usar 'using' para asegurar que el writer se cierra después del bloque
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("Tokens:");
                    foreach (AnLex.Token token in tokens)  // Suponer que tokens es una lista válida.
                    {
                        writer.WriteLine($"Tipo: {token.Type}, Valor: {token.Value}");
                    }

                    writer.WriteLine("\nResultado de la operación");
                    if (txtResult3 != null)
                    {
                        writer.WriteLine("-----------------------------------------------");
                        writer.WriteLine(txtResult3.Text); // Donde 'txtResult2' es el control que contiene el resultado.
                        writer.WriteLine(txtResult2.Text);
                        writer.WriteLine("-----------------------------------------------");
                    }
                    else
                    {
                        writer.WriteLine("No hay resultado para mostrar.");
                    }
                }

                // Notificar al usuario que el archivo se ha guardado con éxito
                MessageBox.Show($"Los resultados se han guardado en el archivo: {filePath}", "Guardado Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Notificar al usuario que hubo un error al guardar el archivo
                MessageBox.Show($"Error al guardar los resultados: {ex.Message}", "Error de Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void txtResult2_TextChanged(object sender, EventArgs e)
        {

        }

        private void TSB2_Click(object sender, EventArgs e)
        {
            string sourceCode = txtCod.Text;
            List<AnLex.Token> tokens = AL.Analyze(sourceCode);

            if (tokens == null)
            {
                MessageBox.Show("No hay tokens para guardar. Por favor, realice el análisis primero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string filePath = "resultados.txt"; // Nombre del archivo de salida.
                                                    // Usar 'using' para asegurar que el writer se cierra después del bloque
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("Tokens:");
                    foreach (AnLex.Token token in tokens)  // Suponer que tokens es una lista válida.
                    {
                        writer.WriteLine($"Tipo: {token.Type}, Valor: {token.Value}");
                    }

                    writer.WriteLine("\nResultado de la operación");
                    if (txtResult3 != null)
                    {
                        writer.WriteLine("-----------------------------------------------");
                        writer.WriteLine(txtResult3.Text); // Donde 'txtResult2' es el control que contiene el resultado.
                        writer.WriteLine(txtResult2.Text);
                        writer.WriteLine("-----------------------------------------------");
                    }
                    else
                    {
                        writer.WriteLine("No hay resultado para mostrar.");
                    }
                }

                // Notificar al usuario que el archivo se ha guardado con éxito
                MessageBox.Show($"Los resultados se han guardado en el archivo: {filePath}", "Guardado Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Notificar al usuario que hubo un error al guardar el archivo
                MessageBox.Show($"Error al guardar los resultados: {ex.Message}", "Error de Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           

        }

        private void txtResult3_TextChanged(object sender, EventArgs e)
        {

        }

        private void TSB1_Click(object sender, EventArgs e)
        {
            string pathToTxt = Path.Combine(Application.StartupPath, "resultados.txt");

            if (File.Exists(pathToTxt))
            {
                try
                {
                    // Usar Process.Start para abrir el archivo en el editor de texto predeterminado.
                    Process.Start(pathToTxt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo abrir el archivo de texto.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("El archivo de texto no existe.\nAsegúrese de compilar primero.", "No Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}