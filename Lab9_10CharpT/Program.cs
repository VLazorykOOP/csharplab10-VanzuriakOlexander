using System;

class InvalidTriangleException : Exception
{
    public InvalidTriangleException(string message) : base(message)
    {
    }
}

class Triangle
{
    private int a, b, c;
    private string color;

    public Triangle(int f, int s, int t)
    {
        if (!IsValidTriangle(f, s, t))
        {
            throw new InvalidTriangleException("Invalid triangle sides");
        }

        a = f;
        b = s;
        c = t;
    }

    public Triangle(int f, int s, int t, string col) : this(f, s, t)
    {
        color = col;
    }

    public void Print()
    {
        Console.WriteLine($"Triangle lines: a = {a}, b = {b}, c = {c}");
    }

    public int Perimeter()
    {
        return a + b + c;
    }

    public double Area()
    {
        double halfPerimeter = Perimeter() / 2.0;
        return Math.Sqrt(halfPerimeter * (halfPerimeter - a) * (halfPerimeter - b) * (halfPerimeter - c));
    }

    public int GetFirst()
    {
        return a;
    }

    public int GetSecond()
    {
        return b;
    }

    public int GetThird()
    {
        return c;
    }

    public void SetFirst(int newValue)
    {
        ValidateSide(newValue);
        a = newValue;
    }

    public void SetSecond(int newValue)
    {
        ValidateSide(newValue);
        b = newValue;
    }

    public void SetThird(int newValue)
    {
        ValidateSide(newValue);
        c = newValue;
    }

    public string GetColor()
    {
        return color;
    }

    private bool IsValidTriangle(int side1, int side2, int side3)
    {
        return side1 > 0 && side2 > 0 && side3 > 0 &&
               side1 + side2 > side3 &&
               side1 + side3 > side2 &&
               side2 + side3 > side1;
    }

    private void ValidateSide(int side)
    {
        if (side <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(side), "Triangle side length must be positive");
        }
    }
}

class FacultyDayEventArgs : EventArgs
{
    public string EventName { get; set; }

    public FacultyDayEventArgs(string eventName)
    {
        EventName = eventName;
    }
}

class Faculty
{
    public event EventHandler<FacultyDayEventArgs> FacultyDay;

    public void CelebrateFacultyDay()
    {
        Console.WriteLine("Faculty Day is being celebrated!");
        OnFacultyDay(new FacultyDayEventArgs("Faculty Day"));
    }

    protected virtual void OnFacultyDay(FacultyDayEventArgs e)
    {
        FacultyDay?.Invoke(this, e);
    }
}

class Student
{
    public Student(Faculty faculty)
    {
        faculty.FacultyDay += Faculty_FacultyDay;
    }

    private void Faculty_FacultyDay(object sender, FacultyDayEventArgs e)
    {
        Console.WriteLine($"Student is celebrating {e.EventName}!");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter the task");
        string? str = Console.ReadLine();
        int n = 0;
        if (str != null) n = int.Parse(str);
        if (n == 1)
        {
            try
            {
                Triangle invalidTriangle = new Triangle(3, 4, 8);
            }
            catch (InvalidTriangleException ex)
            {
                Console.WriteLine($"InvalidTriangleException caught: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        else if (n == 2)
        {
            Faculty faculty = new Faculty();

            Student student = new Student(faculty);

            faculty.CelebrateFacultyDay();
        }
    }
}
