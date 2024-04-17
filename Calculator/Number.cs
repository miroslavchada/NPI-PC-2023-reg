namespace Calculator;
internal class Number {
    public double numerator { get; }
    public double denominator { get; }

    // Constructor for whole numbers
    public Number(double value)
    {
        numerator = value;
        denominator = 1;
    }

    // Constructor for fractions
    public Number(double numerator, double denominator) {
        this.numerator = numerator;
        this.denominator = denominator;
    }

    public Number Simplify()
    {
        double gcd = GCD(numerator, denominator);
        return new Number(numerator / gcd, denominator / gcd);
    }

    private double GCD(double a, double b)
    {
        while (b != 0)
        {
            double t = b;
            b = a % b;
            a = t;
        }
        return a;
    }

    public Number Add(Number number)
    {
        return new Number(numerator * number.denominator + number.numerator * denominator, denominator * number.denominator).Simplify();
    }

    public Number Subtract(Number number) { 
        return new Number(numerator * number.denominator - number.numerator * denominator, denominator * number.denominator).Simplify();
    }

    public Number Multiply(Number number)
    {
        return new Number(numerator * number.numerator, denominator * number.denominator).Simplify();
    }

    public Number Divide(Number number) {
        return new Number(numerator * number.denominator, denominator * number.numerator).Simplify();
    }

    public string WriteAsString()
    {
        if (denominator == 1)
        {
            return numerator.ToString();
        }
        return $"{numerator}|{denominator}";
    }
}
