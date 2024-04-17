namespace Calculator;

internal class Program {

    public enum Operation { ADD, SUBTRACT, MULTIPLY, DIVIDE }
    public enum State { FIRST_NUMBER, OPERATION, SECOND_NUMBER }

    static void Main(string[] args) {
        Number number1 = NewNumber(State.FIRST_NUMBER).Simplify();
        Operation operation = (Operation)Prompt(State.OPERATION);
        Number number2 = NewNumber(State.SECOND_NUMBER).Simplify();

        Console.WriteLine("--------------");
        Console.WriteLine($"Výsledek: {Calculate(number1, operation, number2).WriteAsString()}\r\n");
    }

    public static double Prompt(State state)
    {
        switch (state)
        {
            case State.OPERATION:
                Console.Write("Operátor: ");
                switch (Console.ReadLine())
                {
                    case "+":
                        return (double)Operation.ADD;
                    case "-":
                        return (double)Operation.SUBTRACT;
                    case "*":
                        return (double)Operation.MULTIPLY;
                    case "/":
                        return (double)Operation.DIVIDE;
                    default:
                        Console.WriteLine("Chyba vstupu. Platný pouze operátor +, -, *, /.");
                        return Prompt(state);
                }
                
            default:
                return 0;
        }
    }

    public static Number NewNumber(State state, string input = default)
    {
        switch (state) {
            case State.FIRST_NUMBER:
                Console.Write("Operand 1: ");
                break;

            case State.SECOND_NUMBER:
                Console.Write("Operand 2: ");
                break;
        }

        input = Console.ReadLine();

        int FractionLineCount = input.Count(c => c == '|');
        switch (FractionLineCount)
        {
            case 0:
                try {
                    return new Number(double.Parse(input.Replace(',', '.')));
                } catch (FormatException) {
                    Console.WriteLine("Chyba vstupu. Platná pouze číslice 0-9, desetiny oddělovat čárkou.");
                    return NewNumber(state, input);
                }
            case 1:
                string[] parts = input.Split("|");
                try {
                    return new Number(double.Parse(parts[0].Replace(',', '.')), double.Parse(parts[1].Replace(',', '.')));
                } catch (FormatException) {
                    Console.WriteLine("Chyba vstupu. Platná pouze číslice 0-9, desetiny oddělovat čárkou.");
                    return NewNumber(state, input);
                }
            default:
                Console.WriteLine("Chyba vstupu. Povolena max. 1 zlomková čára.");
                return NewNumber(state, input);
        }
    }

    public static Number Calculate(Number number1, Operation operation, Number number2)
    {
        switch (operation)
        {
            case Operation.ADD:
                return number1.Add(number2);
            case Operation.SUBTRACT:
                return number1.Subtract(number2);
            case Operation.MULTIPLY:
                return number1.Multiply(number2);
            case Operation.DIVIDE:
                return number1.Divide(number2);
            default:
                throw new ArgumentOutOfRangeException("Neznámá operace.");
        }
    }
}
