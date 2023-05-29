namespace Lab1;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Потрібно вказати ім'я вхідного файлу та номер користувача X як аргументи командного рядка.");
            return;
        }

        string inputFileName = args[0];
        int userX = int.Parse(args[1]);
        string outputFileName = GetOutputFileName(inputFileName);

        UserMatrixAnalyzer analyzer = new UserMatrixAnalyzer();
        MatrixReader matrixReader = new MatrixReader();
        MatrixWriter matrixWriter = new MatrixWriter();

        int[,] matrix = matrixReader.ReadMatrixFromFile(inputFileName);
        List<Tuple<int, int>> similarities = analyzer.CalculateSimilarities(matrix, userX);
        matrixWriter.WriteResultsToFile(outputFileName, userX, similarities);

        Console.WriteLine("Результати були записані у вихідний файл.");
    }

    static string GetOutputFileName(string inputFileName)
    {
        string directory = Path.GetDirectoryName(inputFileName);
        string fileName = Path.GetFileNameWithoutExtension(inputFileName);
        string outputFileName = Path.Combine(directory, $"ip-s21_Gavrylenko_01_output.txt");
        return outputFileName;
    }
}

class MatrixReader
{
    public int[,] ReadMatrixFromFile(string fileName)
    {
        string[] lines = File.ReadAllLines(fileName);
        string[] dimensions = lines[0].Split(' ');
        int users = int.Parse(dimensions[0]);
        int movies = int.Parse(dimensions[1]);
        int[,] matrix = new int[users, movies];
        for (int i = 0; i < users; i++)
        {
            string[] values = lines[i + 1].Split(' ');
            for (int j = 0; j < movies; j++)
            {
                matrix[i, j] = int.Parse(values[j]);
            }
        }
        return matrix;
    }
}

class UserMatrixAnalyzer
{
    public List<Tuple<int, int>> CalculateSimilarities(int[,] matrix, int userX)
    {
        int users = matrix.GetLength(0);
        int movies = matrix.GetLength(1);
        List<Tuple<int, int>> similarities = new List<Tuple<int, int>>();
        for (int i = 0; i < users; i++)
        {
            if (i + 1 == userX) continue;
            int[] userA = GetPreferences(matrix, userX - 1);
            int[] userB = GetPreferences(matrix, i);
            int similarity = CalculateSimilarity(userA, userB);
            similarities.Add(new Tuple<int, int>(i + 1, similarity));
        }
        similarities.Sort((x, y) => x.Item2.CompareTo(y.Item2));
        return similarities;
    }

    private int[] GetPreferences(int[,] matrix, int user)
    {
        int movies = matrix.GetLength(1);
        int[] preferences = new int[movies];
        for (int i = 0; i < movies; i++)
        {
            preferences[i] = matrix[user, i];
        }
        return preferences;
    }

    private int CalculateSimilarity(int[] arr1, int[] arr2)
    {
        int inversions = 0;
        for (int i = 0; i < arr1.Length - 1; i++)
        {
            for (int j = i + 1; j < arr1.Length; j++)
            {
                if ((arr1[i] > arr1[j] && arr2[i] < arr2[j]) || (arr1[i] < arr1[j] && arr2[i] > arr2[j]))
                {
                    inversions++;
                }
            }
        }
        return inversions;
    }
}

class MatrixWriter
{
    public void WriteResultsToFile(string fileName, int userX, List<Tuple<int, int>> similarities)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            writer.WriteLine(userX);
            writer.WriteLine(similarities.Count);
            for (int i = 0; i < similarities.Count; i++)
            {
                writer.WriteLine($"{similarities[i].Item1} {similarities[i].Item2}");
            }
        }
    }
}
