using Compiladores.US;
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
            // UI: Realiza algunos cambios en el formulario.
            // Eliminar los datos del control de árbol.
            var treeNode = new TreeNode("AST");
            AstTreeView.Nodes.Clear();
            txtTokens.Text = string.Empty;

            try
            {
                // Paso 1: Obtener el código fuente.
                // La clase permite ir carácter por carácter dentro del texto de entrada.
                var sourceCode = new SourceCode(txtCode.Text);

                // Paso 2: El escaner se encarga de obtener los tokens en su forma más básica.
                // 1.3 devolverá 3 tokens: (1, ., 3), es decir un número, un punto y otro número.
                var scanner = new Scanner();
                var lexemas = scanner.GetLexemas(sourceCode);

                // Paso 3: El analizador se encarga de unir los tokens comúnes.
                // Siguiendo el ejemplo anterior, 1.3 se unirá en un solo token NUMBER cuyo valor será 1.3.
                var tokens = new Parser().Tokenize(lexemas).ToList();

                // Una vez con todos los tokens, se procede a construir un grafo.
                // Por el momento, se asume que los paréntesis son los únicos que definen jerarquía.
                var astBuilder = new AstBuilder();
                var ast = astBuilder.Build(tokens);

                // Validar la sintaxis.
                ast.ValidateSyntax();

                // UI: Mostrar en txtTokens cada uno de los tokens.
                foreach (var token in tokens)
                {
                    txtTokens.Text += "Car: " + token.StartIndex + ", " + token.Name + " \"" + sourceCode.Extract(token) + "\"\r\n";
                }

                // UI: Actualiza el nodo del control de árbol.
                BuildTreeView(ast, treeNode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de sintáxis");
                return;
            }

            AstTreeView.Nodes.Add(treeNode);
            AstTreeView.ExpandAll();
        }

        private void BuildTreeView(ASTNode node, TreeNode parentNode)
        {
            foreach (var n in node.Nodes)
            {
                var tn = new TreeNode(n.Token == null ? "?" : n.Token.Name);
                parentNode.Nodes.Add(tn);
                BuildTreeView(n, tn);
            }
        }
    }
}
