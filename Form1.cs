using Compiladores.US;
using Compiladores.US.LexicalAnalyzer;
using Compiladores.US.SyntaxAnalyzer;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Compiladores
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void cmdParse_Click(object sender, EventArgs e)
        {
            try
            {
                // Paso 1: Obtener el código fuente.
                // La clase permite ir carácter por carácter dentro del texto de entrada.
                var sourceCode = SourceCode.CreateFromString(txtCode.Text);

                // Paso 2: El escaner se encarga de obtener los tokens en su forma más básica.
                var lexer = new Lexer();
                var tokens = lexer.Tokenize(sourceCode);

                // Una vez con todos los tokens, se procede a construir la tabla de sintaxis.
                var parser = new Parser(tokens);
                var program = parser.Parse();

                // Para mostrar el árbol de sintaxis, se convertirá a JSON el programa y se imprimirá
                // en el cuadro de texto.
                astCode.Text = JsonConvert.SerializeObject(program, Formatting.Indented);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de sintáxis");
            }
        }
    }
}
