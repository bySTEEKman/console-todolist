using NpgsqlTypes;

namespace todolist_cli
{
    public class Employee
    {
        internal string name;
        internal NpgsqlDate birthday;

        public Employee(string name, NpgsqlDate birthday)
        {
            this.name = name;
            this.birthday = birthday;
        }
    }
}