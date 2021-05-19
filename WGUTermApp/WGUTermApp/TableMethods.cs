using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using WGUTermApp.Models;

namespace WGUTermApp
{

    public class TableMethods
    {
        SQLiteConnection conn;
        public TableMethods(string databasePath)
        {
            conn = new SQLiteConnection(databasePath);
            conn.CreateTable<Person>();
            conn.CreateTable<Course>();
        }

        public List<Person> GetAllPeople()
        {
            try
            {
                return conn.Table<Person>().ToList();
            }
            catch 
            {
                
            }

            return new List<Person>();
        }

        public List<Course> GetAllCourses()
        {
            try
            {
                return conn.Table<Course>().ToList();
            }
            catch
            {

            }

            return new List<Course>();
        }

        //add data validation
        public void AddNewRecord(Person person)
        {
            try
            {
                if (person.IsNullOrEmpty())
                {
                    throw new Exception("Valid information required.");
                }
                conn.Insert(person);
            }
            catch
            {

            }
        }

        //dates likely won't be changed to datetimes here, but keep in mind it will have to happen, likely right before calling this function
        public void AddNewRecord(Course course)
        {
            try
            {
                if (course.IsNullOrEmpty())
                {
                    throw new Exception("Valid information required.");
                }
                conn.Insert(course);
            }
            catch
            {

            }
        }

        public void DeleteRecord(string table, int row)
        {
            try
            {
                if (row > 0)
                {
                    if (table == "Course")
                    {
                        conn.Delete<Course>(row);
                    }
                    else if (table == "Person")
                    {
                        conn.Delete<Person>(row);
                    }
                    else
                    {
                        throw new Exception("Valid information required.");
                    }
                }
            }
            catch
            {

            }
        }
    }
}
