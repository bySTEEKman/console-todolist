using System;
using System.Linq;
using Npgsql;
using NpgsqlTypes;

namespace todolist_cli
{
    internal class TodoItemRepository
    {
        public void GetAndDoCommand(string[] command, NpgsqlConnection conn)
        {
            if (command[0] == "help")
            {
                Console.WriteLine("\nadd [title] - create new task;\ndelete [id] - delete task by id;\nupdate [id] - update task by id;\nall - get all tasks;\ntask [id] - get task by id\ngetid [title] - get task id by title;\nhelp - get all commands;\n\n");
            }

            if (command[0] == "add")
            {
                command[0] = "";
                string value = String.Join(' ', command);
                value = value.Remove(0, 1);
                AddTask(conn, value);
            }

            if (command[0] == "delete")
            {
                DeleteTask(conn, Int32.Parse(command[1]));
            }

            if (command[0] == "update")
            {
                UpdateTask(conn, Int32.Parse(command[1]));
            }

            if (command[0] == "all")
            {
                GetAllTasks(conn);
            }

            if (command[0] == "task")
            {
                GetTaskById(conn, Int32.Parse(command[1]));
            }

            if (command[0] == "getid")
            {
                command[0] = "";
                string value = String.Join(' ', command);
                value = value.Remove(0, 1);
                GetTaskId(conn, value);
            }
        }

        private void GetTaskId(NpgsqlConnection conn, string value)
        {
            using (var cmd = new NpgsqlCommand("SELECT id FROM items WHERE title=(@title)", conn))
            {
                cmd.Parameters.AddWithValue("title", value);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        var id = reader.GetInt32(0);
                        Console.WriteLine($"\n{value} id is - {id}\n");
                    }
                }
            }
        }

        private void GetTaskById(NpgsqlConnection conn, int taskId)
        {
            using (var cmd = new NpgsqlCommand("SELECT * FROM items WHERE id=(@id)", conn))
            {
                cmd.Parameters.AddWithValue("id", taskId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var title = reader.GetString(0);
                        var description = reader.GetString(1);
                        var done = reader.GetBoolean(2);
                        var id = reader.GetInt32(3);
                        var duedate = reader.GetString(4);
                        string doneFlag = done ? "x" : " ";
                        Console.WriteLine($"\n{id} - [{doneFlag}] - {title} - {duedate}\n{description}\n");
                    }
                }
            }
        }

        private void GetAllTasks(NpgsqlConnection conn)
        {
            using (var cmd = new NpgsqlCommand("SELECT * FROM items", conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var title = reader.GetString(0);
                        var description = reader.GetString(1);
                        var done = reader.GetBoolean(2);
                        var id = reader.GetInt32(3);
                        var duedate = reader.GetString(4);
                        string doneFlag = done ? "x" : " ";
                        Console.WriteLine($"\n{id} - [{doneFlag}] - {title} - {duedate}\n{description}\n");
                    }
                }
            }
        }

        private void UpdateTask(NpgsqlConnection conn, int tId)
        {
            int taskId = tId;
            int stop = 0;
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
                    string value = String.Join(' ', column);
                    value = value.Remove(0, 1);

                    using (var cmd = new NpgsqlCommand("UPDATE items SET title=(@title) WHERE id=(@id)", conn))
                    {
                        cmd.Parameters.AddWithValue("id", taskId);
                        cmd.Parameters.AddWithValue("title", $"{value}");
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("\nTitle updated\n");
                    }
                }

                if (column[0] == "description")
                {
                    column[0] = "";
                    string value = String.Join(' ', column);
                    value = value.Remove(0, 1);

                    using (var cmd = new NpgsqlCommand("UPDATE items SET description=(@desc) WHERE id=(@id)", conn))
                    {
                        cmd.Parameters.AddWithValue("desc", $"{value}");
                        cmd.Parameters.AddWithValue("id", taskId);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("\nDescription updated\n");
                    }
                }

                if (column[0] == "duedate")
                {
                    string value = column[1];

                    using (var cmd = new NpgsqlCommand("UPDATE items SET duedate=(@date) WHERE id=(@id)", conn))
                    {
                        cmd.Parameters.AddWithValue("id", taskId);
                        cmd.Parameters.AddWithValue("date", $"{value}");
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("\nDate updated\n");
                    }
                }

                if (column[0] == "done")
                {
                    bool value = Boolean.Parse(column[1]);

                    using (var cmd = new NpgsqlCommand("UPDATE items SET done=(@done) WHERE id=(@Id)", conn))
                    {
                        cmd.Parameters.AddWithValue("id", taskId);
                        cmd.Parameters.AddWithValue("done", value);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("\nTask done updated\n");
                    }
                }
            }

        }

        private void DeleteTask(NpgsqlConnection conn, int taskId)
        {
            using (var cmd = new NpgsqlCommand("DELETE FROM items WHERE id=(@id)", conn))
            {
                cmd.Parameters.AddWithValue("id", taskId);
                cmd.ExecuteNonQuery();
                Console.WriteLine("\nTask deleted\n");
            }
        }

        private void AddTask(NpgsqlConnection conn, string taskName)
        {
            using (var cmd = new NpgsqlCommand("INSERT INTO items (title, description, duedate, done) VALUES (@title, 'no description', @date, false)", conn))
            {
                string date = DateTime.Now.ToString("yyyy:mm:dd");
                cmd.Parameters.AddWithValue("date", $"{date}");
                cmd.Parameters.AddWithValue("title", $"{taskName}");
                cmd.ExecuteNonQuery();
                Console.WriteLine("\nTask created\n");
            }
        }
    }
}