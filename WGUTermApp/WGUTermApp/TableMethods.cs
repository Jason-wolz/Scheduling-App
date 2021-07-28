using System;
using System.Collections.Generic;
using SQLite;
using WGUTermApp.Models;

namespace WGUTermApp
{

    public class TableMethods//to-do: !!optional!! make methods return string to show error message?
    {
        private readonly SQLiteConnection conn;
        public TableMethods(string databasePath)
        {
            conn = new SQLiteConnection(databasePath);
            //code to reset database
            conn.DropTable<Person>();
            conn.DropTable<Course>();
            conn.DropTable<Assessment>();
            conn.DropTable<Term>();
            conn.CreateTable<Person>();
            conn.CreateTable<Course>();
            conn.CreateTable<Assessment>();
            conn.CreateTable<Term>();
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

        public List<Term> GetAllTerms()
        {
            try
            {
                return conn.Table<Term>().ToList();
            }
            catch
            {

            }
            return new List<Term>();
        }

        //add data validation
        public void AddNewRecord(Person person)
        {
            try
            {
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
                conn.Insert(course);
            }
            catch
            {

            }
        }

        public void AddNewRecord(Term term)
        {
            try
            {
                if (term.IsNullOrEmpty())
                {
                    throw new Exception("Valid information required.");
                }
                conn.Insert(term);
            }
            catch
            {

            }
        }

        public void UpdateRecord(Object obj)
        {
            if (obj is Person person)
            {
                try
                {
                    conn.RunInTransaction(() =>
                    {
                        conn.Update(person);
                    });
                }
                catch
                {

                }
            }
            else if (obj is Course course)
            {
                try
                {
                    conn.RunInTransaction(() =>
                    {
                        conn.Update(course);
                    });
                }
                catch
                {

                }
            }
            else if (obj is Assessment assessment)
            {
                try
                {
                    conn.RunInTransaction(() =>
                    {
                        conn.Update(assessment);
                    });
                }
                catch
                {

                }
            }
            else if (obj is Term term)
            {
                try
                {
                    conn.RunInTransaction(() =>
                    {
                        conn.Update(term);
                    });
                }
                catch
                {

                }
            }
        }

        public void DeleteRecord(string table, int row)
        {
            try
            {
                if (!(row < 0))
                {
                    switch (table)
                    {
                        case "Course":
                            conn.Delete<Course>(row);
                            break;
                        case "Person":
                            conn.Delete<Person>(row);
                            break;
                        case "Assessment":
                            conn.Delete<Assessment>(row);
                            break;
                        case "Term":
                            conn.Delete<Term>(row);
                            break;
                        default:
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
