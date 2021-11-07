using System;
using System.Collections.Generic;

namespace todolist_cli
{
    public class ConsoleOutput
    {
        private string[] command;
        private private NpgsqlConnection connection;

        public ConsoleInput(string[] command, NpgsqlConnection connection)
        {
            this.command = command;
            this.connection = connection;
        }

        public void DoCommand()
        {
            TododItemRepository repository = new TododItemRepository(connection);

            if (command[0] == "help")
            {
                Console.WriteLine("\nadd [title] - create new task;\ndelete [id] - delete task by id;\nupdate [id] - update task by id;\nall - get all tasks;\ntask [id] - get task by id\nhelp - get all commands;\n\n");
            }

            if (command[0] == "add")
            {
                TododItem todoItem = new TodoItem();

                command[0] = "";
                todoItem.Title = String.Join(' ', command).Remove(0, 1);

                repository.CreateTodoItem(item);

                Console.WriteLine("Task created");
            }

            if (command[0] == "delete")
            {
                repository.DeleteTodoItem(Int32.Parse(command[1]));

                Console.WriteLine("Task deleted");
            }

            if (command[0] == "update")
            {
                TodoItem item = new TodoItem();
                int stop = 0;

                item.Id = Int32.Parse(command[1]);

                while (stop == 0)
                {
                    Console.WriteLine("Enter field name and value what do you want to update(you can't change id / if you finished enter 'q' / date format YYYY-MM-DD):\n");
                    string[] column = Console.ReadLine().Split(" ");

                    if (column[0] == "q")
                    {
                        stop++;
                        break;
                    }

                    if (column[0] == "title")
                    {
                        column[0] = "";
                        item.Title = String.Join(' ', column).Remove(0, 1);
                    }

                    if (column[0] == "description")
                    {
                        column[0] = "";
                        item.Description = String.Join(' ', column).Remove(0, 1);
                    }

                    if (column[0] == "duedate")
                    {
                        item.DueDate = column[1];
                    }

                    if (column[0] == "done")
                    {
                        item.Done = Boolean.Parse(column[1]);
                    }
                }

                repository.UpdateTodoItem(item);

                Console.WriteLine("Task updated");
            }

            if (command[0] == "all")
            {
                List<TodoItem> list = repository.FindAll();

                foreach (TodoItem item in list)
                {
                    string doneFlag = item.Done ? "X" : " ";
                    Console.WriteLine($"{item.Id}. [{doneFlag}] {item.Title} ({item.DueDate.ToString("mmm-dd")})\n  {item.Description}");
                }
            }

            if (command[0] == "task")
            {
                TodoItem item = repository.FindOne(Int32.Parse(command[1]));

                Console.WriteLine($"{item.Id}. [{doneFlag}] {item.Title} ({item.DueDate.ToString("mmm-dd")})\n  {item.Description}");
            }
        }
    }
}