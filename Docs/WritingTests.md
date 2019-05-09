# Writing Tests

When writing tests, follow a few basic principles:

1. One assert per test

2. Name test fixture classes based on the class being tested and add a suffix "Tests" to the name.
   e.g. RateServiceTests

3. Name test methods using the MethodName_StateUnderTest_ExpectedBehavior convention below.

    ```
	[MethodName_StateUnderTest_ExpectedBehavior]
	```

    Examples:

	``` csharp
    public void Sum_NegativeNumberAs1stParam_ExceptionThrown()
    public void Sum_NegativeNumberAs2ndParam_ExceptionThrown ()
    public void Sum_simpleValues_Calculated ()
	```
