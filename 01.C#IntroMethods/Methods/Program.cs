namespace _01.C_IntroMethods
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter number 1: ");
            double num1 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter number 2: ");
            double num2 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Choose the operation(+, -, *, /, %): ");
            char opt = Convert.ToChar(Console.ReadLine());

            Calculator(num1, num2, opt);

            int[] nums = { 14, 20, 35, 40, 57, 60, 100 };
            FindNumber(nums);

            int result = Sum(nums);
            Console.WriteLine("Həm 4-ə, həm də 5-ə bölünən elementlərin cəmi: " + result);

            Console.Write("Enter a sentence: ");
            string sentence = Console.ReadLine();

            Console.Write("Enter a character: ");
            char chars = Convert.ToChar(Console.ReadLine());

            int count = CountSymbol(sentence, chars);
            Console.WriteLine($"There are {count} characters from the character '{chars}'.");
        }

        public static void Calculator(double a, double b, char opt)
        {
            switch (opt)
            {
                    case '+':
                        Console.WriteLine($"Result: {a + b}");
                        break;

                    case '-':
                        Console.WriteLine($"Result {a - b}");
                        break;

                    case '*':
                        Console.WriteLine($"Result: {a * b}");
                        break;

                    case '/':
                        if (b != 0)
                            Console.WriteLine($"Result: {a / b}");
                        else
                            Console.WriteLine("Cannot be divided by 0.");
                        break;

                    case '%':
                        if (b != 0)
                            Console.WriteLine($"Result: {a % b}");
                        else
                            Console.WriteLine("Cannot be divided by 0");
                        break;

                    default:
                        Console.WriteLine("Invalid operation.");
                        break;
                }
            }

            public static void FindNumber(int[] arr)
            {
                Console.WriteLine("Even Numbers:");
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] % 2 == 0)
                    {
                        Console.Write(arr[i] + " ");
                    }
                }

                Console.WriteLine();

                Console.WriteLine("Odd numbers:");
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] % 2 != 0)
                    {
                        Console.Write(arr[i] + " ");
                    }
                }

                Console.WriteLine();
            }

            public static int Sum(int[] arr)
            {
                int sum = 0;

                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] % 4 == 0 && arr[i] % 5 == 0)
                    {
                        sum += arr[i];
                    }
                }

                return sum;
            }

        public static int CountSymbol(string sentence, char chars)
        {
            int count = 0;
            for (int i = 0; i < sentence.Length; i++)
            {
                if (sentence[i] == chars)
                {
                    count++;
                }
            }
            return count;
        }
    }

