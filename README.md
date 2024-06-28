# Compiladores

- Karen Moo

- Adirlan Quijano
- Carlos Huchim Ahumada



![image-20240628152656466](C:\Users\da227\Documents\GitHub\practica-compiladores-2\assets\image-20240628152656466.png)

El primer cuadro es para introducir el código fuente, el segundo de símbolos muestra cada simbolo que fue extraído del código. Por último el árbol de sintáxis, muestra únicamente cada nodo y sus hijos. La aplicación fue hecha en Windows Forms (.NET Framework 4.8).

## Lenguaje

```
terminal DOT, NUMBER, PAR_OPEN, PAR_CLOSE, PLUS, MINUS, MULTIPLY, DIVIDE

DOT ::= "."
NUMBER = [0-9]*
PAR_OPEN = '('
PAR_CLOSE = ')'
PLUS ::= '+'
MINUS ::= '-'
MULTIPLY ::= '*'
DIVIDE ::= '/'
```

## Paso 1: Procesar el código fuente

La clase `SourceCode` se desplaza carácter por carácter dentro del texto de entrada hasta llegar al final del dato de entrada. Por lo tanto, el dato de entrada será `textCode.Text` del formulario.

```csharp
// Paso 1: Obtener el código fuente.
// La clase permite ir carácter por carácter dentro del texto de entrada.
var sourceCode = new SourceCode(txtCode.Text);
```

Como se desplaza carácter por carácter, tiene un contador denominado "cursor", que indica en qué parte del código fuente se encuentra. Una función importante de la clase es `Move()`, que va desplazando el cursor conforme va detectando cada uno de los simbolos.

## Paso 2: Extrae cada simbolo

Se usa la clase `Scanner` que obtiene cada simbolo del dato de entrada. Este paso es parte del analizador léxico y solo cumple la función de extraer los simbolos sin realizar ninguna evaluación.

```csharp
// Paso 2: El escaner se encarga de obtener los tokens en su forma más básica.
// 1.3 devolverá 3 tokens: (1, ., 3), es decir un número, un punto y otro número.
var scanner = new Scanner();
var lexemas = scanner.GetLexemas(sourceCode);
```

Para ello, dentro de la misma clase se define una lista de palabras que irá buscando. Algunas palabras son expresiones regulares como `[0-9]` y otras son textos fijos como el operador `=`.

### Punto decimal

Por cada carácter que coincida con `.` se considerará como un punto decimal.

```csharp
internal class Scanner
{
  public Scanner()
  {
	_grammar = new List<Symbol>() {
	  new Symbol("DOT", "."),
    };
}
```

### Número

El símbolo debe definirse como una expresión regular, ya que no es una cadena fija. Aquí es importante que el cuarto parámetro, corresponde a aquellos otros simbolos con los que se puede unir, con la finalidad de producir un número más grande. Es decir: `10.3 = (1, 0, ., 3)` donde se devuelven 43 unidades que corresponden a un número, un número, un punto decimal y otro número. Esto permite más adelante que el analizador lo convierta a un solo token con el valor de `10.3`.

```csharp
internal class Scanner
{
  public Scanner()
  {
	_grammar = new List<Symbol>() {
	  new Symbol("NUMBER", "[0-9]+", isRegularExpression: true, new string[] { "DOT", "NUMBER" }),
    };
}
```

### Apertura de paréntesis

El simbolo debe definirse con una propiedad adicional para indicar que este puede "contener" otros, de tal manera que se pueda generar una jerarquía. Otros simbolos que pueden crearse más adelante, son los corchetes `[...]`

```csharp
internal class Scanner
{
  public Scanner()
  {
	_grammar = new List<Symbol>() {
	  new Symbol("PAR_OPEN", "(") { IsOpen = true },
    };
}
```

### Clausura de paréntesis

Similar al paréntesis de apertura, en este caso se configura la propiedad `IsClose` para indicar al analizador que la expresión finaliza en este token.

```csharp
internal class Scanner
{
  public Scanner()
  {
	_grammar = new List<Symbol>() {
	  new Symbol("PAR_CLOSE", ")") { IsClose = true },
    };
}
```

### Operadores

Los operadores se definen como simbolos de longitud fija, ya que no son expresiones regulares. La propiedad `IsOperator`, permite más adelante validar que después de un operador no exista otro, como por ejemplo: `1 ++ 2` lo que no es válido.

```csharp
internal class Scanner
{
  public Scanner()
  {
	_grammar = new List<Symbol>() {
      new Symbol("PLUS", "+") { IsOperator = true },
      new Symbol("MINUS", "-") { IsOperator = true },
      new Symbol("MULTIPLY", "*") { IsOperator = true },
      new Symbol("DIVIDE", "/") { IsOperator = true },
    };
}
```

### Espacios en blanco

Los tokens de espacios en blanco, generalmente se pueden omitir, sin embargo se deben definir porque forman parte del vocabulario, y aunque en esta versión, solo se está evaluando operaciones matemáticas (básicas), pueden ser necesarias dentro de una literal como por ejemplo `"Universidad del Sur"` a diferencia de `1   + 1 -> (1+1)`.

```csharp
internal class Scanner
{
  public Scanner()
  {
	_grammar = new List<Symbol>() {
	  new Symbol("WHITESPACE", " ") { IsWhiteSpace = true },
    };
}
```

### Carácteres

Los simbolos de texto, fueron agregados para probar, aunque no son parte de esta versión. En el ejemplo, se consideró que `aaa1256` se tomará como un token, ya que aunque la expresión regular indique que solo carácteres, este simbolo se puede unir a la derecha con números.


```csharp
internal class Scanner
{
  public Scanner()
  {
	_grammar = new List<Symbol>() {
	  new Symbol("CARACTER", "[a-zA-Z]+", true, new string[] { "NUMBER", "CARACTER" }),
    };
}
```

## Paso 3: Obtener los tokens.

Una vez que todos los tokens han sido obtenidos, es necesario unir aquellos tokens, por ejemplo `123456789` genera 9 tokens, pero en realidad todos conforman un único token; lo mismo pasa con `12345.6789`. 

```csharp
// Paso 3: El analizador se encarga de unir los tokens comúnes.
// Siguiendo el ejemplo anterior, 1.3 se unirá en un solo token NUMBER cuyo valor será 1.3.
var tokens = new Parser().Tokenize(lexemas).ToList();
```

Este paso lo único que hace entonces, es que recorre todos los tokens y aumenta el tamaño del token de acuerdo a cómo se definió en el paso 2.

## Paso 4: Validar sintáxis

Por último, se realiza la validación de la sintáxis, pero para poder validar que el código o valor en `txtCode.Text` sea válido, se requiere construir un árbol con cada token.

```csharp
// Una vez con todos los tokens, se procede a construir un grafo.
// Por el momento, se asume que los paréntesis son los únicos que definen jerarquía.
var astBuilder = new AstBuilder();
var ast = astBuilder.Build(tokens);

// Validar la sintaxis.
ast.ValidateSyntax();
```

Por ejemplo, en `2+(1+1)+5` existe un paréntesis de apertura y uno de clausura, y al momento de construir el árbol (grafo), todos los siguientes tokens se crean como hijos del nodo padre, hasta llegar al paréntesis de clausura, donde se regresa.

```
NUMBER: 2
SUM:    +
EXPR:
├── PAR_OPEN:  (
├── NUMBER:    1
├── SUM:       +
├── NUMBER:    1
└── PAR_CLOSE: )
SUM:    +
NUMBER: 5
```

Y se puede crear más complejo: `(((3+5)))`

```
PAR_OPEN:  (
EXPR:
├── PAR_OPEN:  (
├── EXPR:
│   ├── PAR_OPEN:  (
│   ├── NUMBER:    3
│   ├── SUM:       +
│   ├── NUMBER:    5
│   └── PAR_CLOSE: )
└── PAR_CLOSE: )
PAR_CLOSE: )
```

### Reglas de validación

Las reglas de la validación se encuentran en la clase: `ASTExpresionNode`, son muy básicas, pero son las siguientes:

- Si el primer nodo es una apertura, el último nodo debe ser una clausura: `(1+2 = Sintaxis inválida.`
- Si el último nodo es una clausura, entonces el primer nodo debe ser una apertur: `1+2) = Sintáxis inválida.`
- No puede haber dos o más operadores juntos: `1 ++ 3 = Sintáxis inválida`.

## Pantallas

### Formula

![image-20240628161834507](C:\Users\da227\Documents\GitHub\practica-compiladores-2\assets\image-20240628161834507.png)

### Escáner de tokens

Se encarga de extraer todos los tokens. Algunos en esta pantalla fueron agregados como prueba, pero no forman parte del vocabulario de esta versión.

![image-20240628162037898](C:\Users\da227\Documents\GitHub\practica-compiladores-2\assets\image-20240628162037898.png)

### Analizador léxico

Genera todos los tokens.

![image-20240628162051976](C:\Users\da227\Documents\GitHub\practica-compiladores-2\assets\image-20240628162051976.png)

### Analizar sintáctico

Este es construye el AST partiendo de todos los tokens. Cuando detecta un simbolo de apertura los agrega como hijos del nodo, y cuando detecta un simbolo de clausura, regresa al nodo padre.

![image-20240628162104447](C:\Users\da227\Documents\GitHub\practica-compiladores-2\assets\image-20240628162104447.png)

### Formulario principal

Este ejecyta todos los pasos y muestra los resultados a los controles de texto y el control de árbol. Un mensaje de error aparecerá cuando existan errores de acuerdo a las reglas de validadación definidas más arriba.

![image-20240628162122233](C:\Users\da227\Documents\GitHub\practica-compiladores-2\assets\image-20240628162122233.png)
