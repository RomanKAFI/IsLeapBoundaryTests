public partial class UnitTests
{
    public static HashSet<int> ExpectedValues = new HashSet<int>
    {
        int.MinValue,
        int.MinValue+1,
        1581, 1582, 1583, 1584, 1585,
        1599, 1600, 1601, 1699, 1700,
        1701,
        int.MaxValue-1, int.MaxValue
    };

    public static int ExtraValuesCount = 0;

    public static bool IsException = false;

    public bool IsLeap(int year)
    {
        if (ExpectedValues.Contains(year))
        {
            ExpectedValues.Remove(year);
        }
        else
        {
            ExtraValuesCount++;
        }

        IsException = false;

        if (year <= 1581)
        {
            IsException = true;
            return false;
        }

        return (year % 400 == 0) ||
            (!(year % 100 == 0) && (year % 4 == 0));
    }
}

// TestClassAttribute
[AttributeUsage(AttributeTargets.Class)]
public class TestClassAttribute : Attribute
{
}

// TestMethodAttribute
[AttributeUsage(AttributeTargets.Method)]
public class TestMethodAttribute : Attribute
{
}

// ExpectedExceptionAttribute
[AttributeUsage(AttributeTargets.Method)]
public class ExpectedExceptionAttribute : Attribute
{
    public Type ExceptionType { get; }

    public ExpectedExceptionAttribute(Type exceptionType)
    {
        ExceptionType = exceptionType;
    }
}

// Assert class
public static class Assert
{
    public static void AreEqual<T>(T expected, T actual)
    {
        if (!EqualityComparer<T>.Default.Equals(expected, actual))
        {
            UnitTests.IsException = true;
            Console.WriteLine($"Expected {expected} but got {actual}");
        }
    }
}

// Main test runner
public class Program
{
    public static void Main()
    {
    var testInstance = new UnitTests();
    var methods = typeof(UnitTests).GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);

        int passed = 0;
        int failed = 0;

        Array.Sort(methods, (a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));

        foreach (var method in methods)
        {
            var testMethodAttr = method.GetCustomAttributes(typeof(TestMethodAttribute), false);
            if (testMethodAttr.Length > 0)
            {
                string testName = method.Name;
                var expectedExceptionAttrArr = method.GetCustomAttributes(typeof(ExpectedExceptionAttribute), false);
                ExpectedExceptionAttribute? expectedExceptionAttr = expectedExceptionAttrArr.Length > 0 ? (ExpectedExceptionAttribute)expectedExceptionAttrArr[0] : null;

                try
                {
                    method.Invoke(testInstance, null);
                }
                catch (Exception)
                {
                    UnitTests.IsException = true;
                }

                if (expectedExceptionAttr != null && !UnitTests.IsException)
                {
                    Console.WriteLine($"{testName} FAILED: Expected exception {expectedExceptionAttr.ExceptionType.Name} was not thrown.");
                    failed++;
                }
                else if (expectedExceptionAttr == null && UnitTests.IsException)
                {
                    Console.WriteLine($"{testName} FAILED");
                    failed++;
                }
                else
                {
                    Console.WriteLine($"{testName} passed.");
                    passed++;
                }
                UnitTests.IsException = false;
            }
        }

        Console.WriteLine("All tests completed.");
        Console.WriteLine($"Summary: {passed} passed, {failed} failed.");

        // Print missing and extra test values count
        Console.WriteLine($"Missing test values count: {UnitTests.ExpectedValues.Count}");
        Console.WriteLine($"Extra test values count: {UnitTests.ExtraValuesCount}");
    }
}