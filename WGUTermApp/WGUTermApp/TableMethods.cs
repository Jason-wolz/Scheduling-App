using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using WGUTermApp.Models;

namespace WGUTermApp
{

    public class TableMethods
    {
        readonly SQLiteConnection conn;
        public TableMethods(string databasePath)
        {
            conn = new SQLiteConnection(databasePath);
            conn.CreateTable<Person>();
            conn.CreateTable<Course>();
            conn.DropTable<Assessment>();
            conn.CreateTable<Assessment>();
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

        public List<Assessment> GetAllAssessments()
        {
            try
            {
                return conn.Table<Assessment>().ToList();
            }
            catch
            {

            }
            return new List<Assessment>();
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

        public void AddNewRecord(Assessment assessment)
        {
            try
            {
                if (assessment.IsNullOrEmpty())
                {
                    throw new Exception("Valid information required.");
                }
                conn.Insert(assessment);
            }
            catch
            {

            }
        }

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
                if (!(row < 0))
                {
                    if (table == "Course")
                    {
                        conn.Delete<Course>(row);
                    }
                    else if (table == "Person")
                    {
                        conn.Delete<Person>(row);
                    }
                    else if (table == "Assessments")
                    {
                        conn.Delete<Assessment>(row);
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
