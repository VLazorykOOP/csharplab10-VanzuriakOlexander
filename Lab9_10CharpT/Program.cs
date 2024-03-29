using System;

class InvalidTriangleException : Exception
{
    public InvalidTriangleException(string message) : base(message)
    {
    }
}

class Triangle
{
    private int[] sides = new int[3];
    private string color;

    public Triangle(int f, int s, int t)
    {
        if (!IsValidTriangle(f, s, t))
        {
            throw new InvalidTriangleException("Invalid triangle sides");
        }

        sides[0] = f;
        sides[1] = s;
        sides[2] = t;
    }

    public Triangle(int f, int s, int t, string col) : this(f, s, t)
    {
        color = col;
    }

    public void Print()
    {
        Console.WriteLine($"Triangle lines: a = {sides[0]}, b = {sides[1]}, c = {sides[2]}");
    }

    public int Perimeter()
    {
        return sides[0] + sides[1] + sides[2];
    }

    public double Area()
    {
        double halfPerimeter = Perimeter() / 2.0;
        return Math.Sqrt(halfPerimeter * (halfPerimeter - sides[0]) * (halfPerimeter - sides[1]) * (halfPerimeter - sides[2]));
    }

    public int GetSide(int index)
    {
        if (index < 0 || index >= sides.Length)
        {
            throw new IndexOutOfRangeException("Index is out of range for triangle sides array");
        }

        return sides[index];
    }

    public void SetSide(int index, int newValue)
    {
        ValidateSide(newValue);
        sides[index] = newValue;
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
