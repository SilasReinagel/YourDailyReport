using System;

public class EnvironmentVariable
{
    private readonly string _value;

    public EnvironmentVariable(string name)
    {
        _value = Environment.GetEnvironmentVariable(name);
        if (string.IsNullOrWhiteSpace(_value))
            throw new ArgumentException($"Missing Required Environment Variable '{name}'");
    }

    public static implicit operator string(EnvironmentVariable e) => e._value;
}
