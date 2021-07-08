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
            if (string.IsNullOrEmpty(Email))
            {
                return true;
            }
            if (string.IsNullOrEmpty(Name))
            {
                return true;
            }
            if (PersonId < 0)
            {
                return true;
            }
            if (string.IsNullOrEmpty(Phone))
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

        public Course(string name, DateTime start, DateTime end, string status, int instructor, int performance, int objective, int term)
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
        public int Performance { get; set; }
        [MaxLength(100), NotNull]
        public int Objective { get; set; }
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
            if (string.IsNullOrEmpty(Name))
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
            if (string.IsNullOrEmpty(Status))
            {
                return true;
            }
            if (Instructor < 0)
            {
                return true;
            }
            if (Performance < 0)
            {
                return true;
            }
            if (Objective < 0)
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
    [Table("assessments")]
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int AssessmentId { get; set; }
        [MaxLength(50), NotNull]
        public string Name { get; set; }
        [MaxLength(50), NotNull]
        public string Type { get; set; }
        [MaxLength(500), NotNull]
        public string Description { get; set; }
        [NotNull]
        public DateTime EstDueDate { get; set; }

        public Assessment() { }

        public Assessment(string name, string type, string description, DateTime dueDate)
        {
            Name = name;
            Type = type;
            Description = description;
            EstDueDate = dueDate;
        }

        public bool IsNullOrEmpty()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return true;
            }
            if (string.IsNullOrEmpty(Type))
            {
                return true;
            }
            if (string.IsNullOrEmpty(Description))
            {
                return true;
            }
            if (EstDueDate == default)
            {
                return true;
            }
            return false;
        }
    }
}
