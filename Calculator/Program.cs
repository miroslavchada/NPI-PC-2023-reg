namespace Calculator;

internal class Program {

    public enum Operation { ADD, SUBTRACT, MULTIPLY, DIVIDE }
    public enum State { FIRST_NUMBER, SECOND_NUMBER }

    static void Main(string[] args) {
        Console.WriteLine("Jednoduchá kalkulačka\r\n" +
            "Desetiny se oddělují \",\" nebo \".\"\r\n" +
            "Zlomek se nechá zadat pomocí \"|\" jako zlomková čára\r\n");

        Number number1 = NewNumber(State.FIRST_NUMBER).Simplify();
        Operation operation = OperationChoose();
        Number number2 = NewNumber(State.SECOND_NUMBER).Simplify();

        Console.WriteLine("--------------");
        Console.WriteLine($"Výsledek: {Calculate(number1, operation, number2).WriteAsString()}\r\n");
        Console.ReadKey();
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

        int fractionLineCount = input.Count(c => c == '|');
        switch (fractionLineCount)
        {
            case 0:
                try {
                    return new Number(double.Parse(input.Replace(',', '.')));
                } catch (FormatException) {
                    Console.WriteLine("Chyba vstupu. Platná pouze číslice 0-9, desetiny oddělovat čárkou.");
                    return NewNumber(state, input);
                }
            case 1:
                string[] parts = input.Replace(',', '.').Split("|");
                try {
                    return new Number(double.Parse(parts[0]), double.Parse(parts[1]));
                } catch (FormatException) {
                    Console.WriteLine("Chyba vstupu. Platná pouze číslice 0-9, desetiny oddělovat čárkou.");
                    return NewNumber(state, input);
                }
            default:
                Console.WriteLine("Chyba vstupu. Povolena max. 1 zlomková čára.");
                return NewNumber(state, input);
        }
    }

    public static Operation OperationChoose()
    {
        Console.Write("Operátor: ");
        switch (Console.ReadLine())
        {
            case "+":
                return Operation.ADD;
            case "-":
                return Operation.SUBTRACT;
            case "*":
                return Operation.MULTIPLY;
            case "/":
                return Operation.DIVIDE;
            default:
                Console.WriteLine("Chyba vstupu. Platný pouze operátor +, -, *, /.");
                return OperationChoose();
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
