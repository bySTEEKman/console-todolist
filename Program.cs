using System;
using System.Threading.Tasks;
using Npgsql;

namespace todolist_cli
{
    class Program
    {
        static void Main()
        {
            var connString = "Host=127.0.0.1;Username=todolist_app;Password=firstBase;Database=todolist";
            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            // EmployeeDictionary dictionary = new EmployeeDictionary();
            // ReadBirthdays(dictionary);
            // dictionary.PrintBirthdays(0);
            Console.WriteLine("Enter the command:\n");
            string[] command = Console.ReadLine().Split(" ");

            ConsoleOutput todoDirectory = new ConsoleOutput(command, conn);
            todoDirectory.DoCommand();

            Main();
        }
        // static void ReadBirthdays(EmployeeDictionary someDictionary)
        // {   
        //     var connString = "Host=127.0.0.1;Username=todolist_app;Password=firstBase;Database=todolist";

        //     using var conn = new NpgsqlConnection(connString);
        //     conn.Open();

        //     using (var cmd = new NpgsqlCommand("SELECT fullname, birthday FROM birthdays", conn))
        //     {
        //         using (var reader = cmd.ExecuteReader())
        //         {
        //             while (reader.Read())
        //             {
        //                 Employee employee = new Employee(reader.GetString(0), reader.GetDate(1));
        //                 someDictionary.AddEmployee(employee);
        //             }
        //         }
        //     }
        // }
    }
}
