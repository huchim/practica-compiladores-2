using Compiladores.US.LexicalAnalyzer;
using Compiladores.US.SyntaxAnalyzer;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Windows.Forms;

namespace Compiladores
{
    public partial class Form1 : Form
    {
        private readonly Lexer _lexer;

        public Form1()
        {
            InitializeComponent();

            // Inicializamos nuestros componentes.
            _lexer = new Lexer();
            _lexer.OnTokenFound += Lexer_OnTokenFound;
        }

        private void Lexer_OnTokenFound(object sender, OnTokenFoundEventArgs e)
        {
            GetSymbolsTable().Rows.Add(e.Token.Category.TokenType, e.Token.Lexema.Value, e.Token.Lexema.Position.StartIndex, e.Token.Lexema.Position.Length);
        }

        private void ClearSymbolsTable()
        {
            GetSymbolsTable().Rows.Clear();
        }

        private DataTable GetSymbolsTable()
        {
            return dataSet1.Tables["TablaSimbolos"];
        }

        private void cmdParse_Click(object sender, EventArgs e)
        {
            ClearSymbolsTable();

            try
            {
                // Paso 1: Obtener el código fuente.
                // La clase permite ir carácter por carácter dentro del texto de entrada.
                var sourceCode = SourceCode.CreateFromString(txtCode.Text);

                // Paso 2: El escaner se encarga de obtener los tokens en su forma más básica.
                var tokens = _lexer.Tokenize(sourceCode);

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

        private void Form1_Load(object sender, EventArgs e)
        {
            // Cargar una lista de todos los archivos TXT dentro del directorio ./Examples
            var examples = System.IO.Directory.GetFiles("./Examples", "*.txt");

            // Llenar el combo box con los nombres de los archivos.
            foreach (var example in examples)
            {
                cmbExamples.Items.Add(System.IO.Path.GetFileName(example));
            }

            // Seleccionar de manera automáticamente el primer archivo.
            cmbExamples.SelectedIndex = 0;

            // Cargar el archivo.
            LoadActiveExample();
        }

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            LoadActiveExample();
        }

        private void LoadActiveExample()
        {
            // Verificar si existe un elemento del combobox seleccionado.
            if (cmbExamples.SelectedItem == null)
            {
                return;
            }

            // Cada elemento del combobox es el nombre del archivo.
            // Verificar si existe en el directorio, antes de abrir.
            var example = $"./Examples/{cmbExamples.SelectedItem}";

            if (!System.IO.File.Exists(example))
            {
                return;
            }

            // Cargar el contenido del archivo en el cuadro de texto txtCode.
            txtCode.Text = System.IO.File.ReadAllText(example);
        }
    }
}
