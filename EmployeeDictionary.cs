using System;
using System.Collections.Generic;
using NpgsqlTypes;

namespace todolist_cli
{
    public class EmployeeDictionary
    {
        private Dictionary<int, List<Employee>> employeeDictionary = new Dictionary<int, List<Employee>>();
        private List<Employee> employeeList = new List<Employee>();
        private DateTime actualDate = DateTime.Now;

        public void AddEmployee(Employee employee)
        {
            employeeList.Add(employee);
        }

        private void ListSort()
        {
            employeeList.Sort(delegate(Employee x, Employee y){return x.birthday.Day.CompareTo(y.birthday.Day);});
        }

        private string Polarization(int number)
        {
            return number%10 == 1 ? "год" : (2 <= number%10 && number%10 <= 4 ? "года" : "лет");
        }

        private void SortDictionary()
        {
             foreach (Employee human in employeeList)
            {
                int month = human.birthday.Month;
                if (employeeDictionary.ContainsKey(month))
                {
                    employeeDictionary[month].Add(human);
                }
                else
                {
                    List<Employee> employeeList = new List<Employee>();
                    employeeList.Add(human);
                    employeeDictionary.Add(month, employeeList);
                }
            }
        }

        public void PrintBirthdays(int counter)
        {
            ListSort();
            SortDictionary();
            int actualMonth = actualDate.Month;
            int actualYear = actualDate.Year;
            for (int i = 0; i <= counter; i++)
            {
                string title =  actualDate.AddMonths(i).ToString("MMMM yyyy");
                Console.WriteLine($"{title}");
                foreach(Employee human in employeeDictionary[actualMonth])
                {
                    int howOld = actualYear - human.birthday.Year;
                    int birthday = human.birthday.Day;
                    Console.WriteLine($"({(birthday >= 10 ? birthday : "0" + birthday)}) - {human.name} ({howOld} {Polarization(howOld)})");
                }
                actualMonth++;
                if(actualMonth > 12)
                {
                    actualYear++;
                    actualMonth = 1;
                }
            }
        }
    }
}