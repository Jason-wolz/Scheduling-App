using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

//database schema
namespace WGUTermApp.Models
{
    [Table("people")]
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int PersonId { get; set; }
        [MaxLength(50), NotNull]
        public string Name { get; set; }
        [MaxLength(15), NotNull]
        public string Phone { get; set; }
        [MaxLength(50), NotNull]
        public string Email { get; set; }

        public Person(string name, string phone, string email)
        {
            Name = name;
            Phone = phone;
            Email = email;
        }

        public Person()
        {
        }

        public bool IsEqual(Person p)
        {
            if (p.Name == this.Name && p.Phone == this.Phone && p.Email == this.Email)
            {
                return true;
            }
            return false;
        }

        public bool IsNullOrEmpty()
        {
            if (this == null)
            {
                return true;
            }
            if (Email == null || Email == "")
            {
                return true;
            }
            if (Name == null || Name == "")
            {
                return true;
            }
            if (PersonId < 0)
            {
                return true;
            }
            if (Phone == null || Phone == "")
            {
                return true;
            }
            return false;
        }
    }
    [Table("classes")]
    public class Course//add another table for terms?
    {
        public Course()
        {
        }

        public Course(string name, DateTime start, DateTime end, string status, int instructor, string performance, string objective, int term)
        {
            Name = name;
            Start = start;
            End = end;
            Status = status;
            Instructor = instructor;
            Performance = performance;
            Objective = objective;
            Term = term;
        }

        [PrimaryKey, AutoIncrement]
        public int CourseId { get; set; }
        [MaxLength(50), NotNull]
        public string Name { get; set; }
        [NotNull]
        public DateTime Start { get; set; }
        [NotNull]
        public DateTime End { get; set; }
        [NotNull]
        public string Status { get; set; }
        [NotNull]
        public int Instructor { get; set; }
        [MaxLength(100), NotNull]
        public string Performance { get; set; }
        [MaxLength(100), NotNull]
        public string Objective { get; set; }
        [NotNull]
        public int Term { get; set; }

        public bool IsEqual(Course c)
        {
            if (this.Name == c.Name && this.Start == c.Start)
            {
                if (this.End == c.End && this.Status == c.Status)
                {
                    if (this.Instructor == c.Instructor && this.Term == c.Term)
                    {
                        if (this.Performance == c.Performance && this.Objective == c.Objective)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool IsNullOrEmpty()
        {
            if (this == null)
            {
                return true;
            }
            if (this.CourseId < 0)
            {
                return true;
            }
            if (Name == null || Name == "")
            {
                return true;
            }
            if (Start == default)
            {
                return true;
            }
            if (End == default)
            {
                return true;
            }
            if (Status == null || Status == "")
            {
                return true;
            }
            if (Instructor < 0)
            {
                return true;
            }
            if (Performance == null || Performance == "")
            {
                return true;
            }
            if (Objective == null || Objective == "")
            {
                return true;
            }
            if (Term < 0)
            {
                return true;
            }
            return false;
        }
    }
}
