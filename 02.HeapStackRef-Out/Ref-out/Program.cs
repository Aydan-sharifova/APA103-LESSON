class Program
{
    static void Main()
    {
        int[] numbers = { 1, 2, 3, 4 };

        numbers = ArrayResize(numbers, 5, 6, 7);

        foreach (var item in numbers)
        {
            Console.Write(item);
        }
    }

    static int[] ArrayResize(int[] numbers, params int[] nums)
    {
        int[] newArray = new int[numbers.Length + nums.Length];

        for (int i = 0; i < numbers.Length; i++)
        {
            newArray[i] = numbers[i];
        }

        for (int i = 0; i < nums.Length; i++)
        {
            newArray[numbers.Length + i] = nums[i];
        }

        return newArray;
    }
}