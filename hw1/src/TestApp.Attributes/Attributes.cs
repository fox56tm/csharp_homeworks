namespace TestApp.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class TestAttribute : Attribute
{
    public Type Expected { get; set; }
    public string Ignore { get; set; }
}
[AttributeUsage(AttributeTargets.Method)] public class SetUpAttribute : Attribute { }
[AttributeUsage(AttributeTargets.Method)] public class TearDownAttribute : Attribute { }
[AttributeUsage(AttributeTargets.Method)] public class OneTimeSetUpAttribute : Attribute { }
[AttributeUsage(AttributeTargets.Method)] public class OneTimeTearDownAttribute : Attribute { }
