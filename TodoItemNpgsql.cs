using System;
using System.Linq;
using Npgsql;
using NpgsqlTypes;

namespace todolist_cli
{
    public class TodoItemNphsql
    {
        public TodoItem GetTaskById(NpgsqlConnection conn, int taskId)
        {
            TodoItem item = new TodoItem();

            using (var cmd = new NpgsqlCommand("SELECT * FROM items WHERE id=(@id)", conn))
            {
                cmd.Parameters.AddWithValue("id", taskId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        item.Title = reader.GetString(0);
                        item.Description = reader.GetString(1);
                        item.Done = reader.GetBoolean(2);
                        item.Id = reader.GetInt32(3);
                        item.DueDate = reader.GetString(4);
                    }
                }
            }

            if (item.Title == null)
            {
                return null;
            }

            return item;
        }

        public List<TodoItem> GetAllTasks(NpgsqlConnection conn)
        {
            List<TodoItem> list = new List<TodoItem>();

            using (var cmd = new NpgsqlCommand("SELECT * FROM items", conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TodoItem item = new TodoItem();

                        item.Title = reader.GetString(0);
                        item.Description = reader.GetString(1);
                        item.Done = reader.GetBoolean(2);
                        item.Id = reader.GetInt32(3);
                        item.DueDate = reader.GetString(4);

                        list.Add(item);
                    }
                }
            }

            return list;
        }

        public void UpdateTask(NpgsqlConnection conn, TodoItem item)
        {
            if (item.Title != null)
            {
                using (var cmd = new NpgsqlCommand("UPDATE items SET title=(@title) WHERE id=(@id)", conn))
                {
                    cmd.Parameters.AddWithValue("id", item.Id);
                    cmd.Parameters.AddWithValue("title", item.Title);
                    cmd.ExecuteNonQuery();
                }
            }
            if(item.Description != null)
            {
                using (var cmd = new NpgsqlCommand("UPDATE items SET description=(@description) WHERE id=(@id)", conn))
                {
                    cmd.Parameters.AddWithValue("id", item.Id);
                    cmd.Parameters.AddWithValue("description", item.Description);
                    cmd.ExecuteNonQuery();
                }
            }
            if(item.Done != false)
            {
                using (var cmd = new NpgsqlCommand("UPDATE items SET done=true WHERE id=(@id)", conn))
                {
                    cmd.Parameters.AddWithValue("id", item.Id);
                    cmd.ExecuteNonQuery();
                }
            }
            if(item.DueDate != null)
            {
                using (var cmd = new NpgsqlCommand("UPDATE items SET duedate=(@date) WHERE id=(@id)", conn))
                {
                    cmd.Parameters.AddWithValue("id", item.Id);
                    cmd.Parameters.AddWithValue("date", item.DueDate);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTask(NpgsqlConnection conn, int taskId)
        {
            using (var cmd = new NpgsqlCommand("DELETE FROM items WHERE id=(@id)", conn))
            {
                cmd.Parameters.AddWithValue("id", taskId);
                cmd.ExecuteNonQuery();
            }
        }

        public void AddTask(NpgsqlConnection conn, TodoItem item)
        {
            using (var cmd = new NpgsqlCommand("INSERT INTO items (title, description, duedate, done) VALUES (@title, @description, @date, false)", conn))
            {
                cmd.Parameters.AddWithValue("description", item.Description);
                cmd.Parameters.AddWithValue("date", item.Date);
                cmd.Parameters.AddWithValue("title", item.Title);
                cmd.ExecuteNonQuery();
            }
        }
    }
}