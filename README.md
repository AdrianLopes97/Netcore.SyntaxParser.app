```markdown
# Netcore.SyntaxParser

## Descrição
O **Netcore.SyntaxParser** é uma biblioteca desenvolvida em C# para realizar análise léxica e sintática de linhas de atribuição em uma pseudolinguagem. Ele valida se uma linha segue uma gramática específica, permitindo identificar e processar tokens como identificadores, números, operadores e atribuições.

### Gramática Suportada
A biblioteca valida linhas de atribuição que seguem a seguinte gramática:

```ebnf
<linha> ::= <ID> "=" <expressao>
<expressao> ::= <termo> | <expressao> <op> <termo>
<termo> ::= <ID> | <NUM>
<op> ::= "+" | "-" | "*" | "/"
<ID> ::= letra { letra | digito }*
<NUM> ::= digito { digito }*
letra ::= "a" | "b" | ... | "z" | "A" | "B" | ... | "Z" | "_"
digito ::= "0" | "1" | ... | "9"
```

### Exemplos de Linhas Válidas
```plaintext
salario = salario * 3
salario = salario * bonus
salario = 1000 * 50
salario = salario - descontos + beneficios
```

### Exemplos de Linhas Inválidas
```plaintext
salario = salario *
salario = *
salario =
salario
```

---

## Funcionalidades

1. **Analisador Léxico**:
   - Converte uma string de entrada em uma lista de tokens.
   - Identifica os seguintes tipos de tokens:
     - `TOKEN_ID`: Identificadores (ex.: `salario`, `bonus`).
     - `TOKEN_NUM`: Números (ex.: `1000`, `50`).
     - `TOKEN_OP`: Operadores aritméticos (`+`, `-`, `*`, `/`).
     - `TOKEN_ASSIGN`: Operador de atribuição (`=`).

2. **Analisador Sintático**:
   - Valida se os tokens gerados seguem a gramática da linha de atribuição.
   - Garante que a estrutura da linha está correta.

---

## Como Usar

### Pré-requisitos
- .NET 8 instalado no sistema.

### Instalação
```bash
# Clone o repositório:
git clone https://github.com/seu-usuario/netcore-syntax-parser.git

# Navegue até o diretório do projeto:
cd netcore-syntax-parser
```

### Exemplo de Uso
No arquivo `Program.cs`, você pode testar a biblioteca com o seguinte código:

```csharp
using Netcore.SyntaxParser.app.Utils;

class Program
{
    static void Main(string[] args)
    {
        var lines = new[]
        {
            "salario = salario * 3",
            "salario = salario * bonus",
            "salario = 1000 * 50",
            "salario = salario - descontos + beneficios",
            "salario = salario *",
            "salario = *",
            "salario =",
            "salario"
        };

        // Testando as linhas de exemplo
        foreach (var line in lines)
        {
            Console.WriteLine($"Testing line: {line}");
            Parser.Tokenize(line);
            Parser.PrintTokens();
            bool isValid = Parser.ValidateAssignmentLine();
            Console.WriteLine($"Is valid: {isValid}");
            Console.WriteLine("----------------------------");
        }
    }
}
```

### Saída Esperada
Para as linhas fornecidas:

- **Válidas**:
  ```plaintext
  salario = salario * 3
  salario = salario * bonus
  salario = 1000 * 50
  salario = salario - descontos + beneficios
  ```
  Retornam `Is valid: True`.

- **Inválidas**:
  ```plaintext
  salario = salario *
  salario = *
  salario =
  salario
  ```
  Retornam `Is valid: False`.

---

## Estrutura do Código

### Métodos Principais
1. **`Tokenize(string input)`**:
   - Realiza a análise léxica da string de entrada e gera tokens.

2. **`ValidateAssignmentLine()`**:
   - Valida se os tokens gerados representam uma linha de atribuição válida.

3. **`PrintTokens()`**:
   - Imprime os tokens gerados no console.

### Métodos Auxiliares
- **`HandleNumber`**: Processa números na entrada.
- **`HandleIdentifier`**: Processa identificadores na entrada.
- **`ValidateExpression`**: Valida expressões conforme a gramática.
- **`ValidateTerm`**: Valida termos (identificadores ou números).
```
 
