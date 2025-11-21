using System;
using System.Xml.Serialization;

namespace StudentEnrollmentApp.Models
{
    /// <summary>
    /// Клас Student съдържа данните за един студент.
    /// Автор: Владимир Василев Владимиров - F113578
    /// Коментари: Инкапсулация, конструктор и ToString override.
    /// Използва XmlSerializer за сериализация/десериализация.
    /// </summary>
    [Serializable]
    public class Student
    {
        private Guid _id;
        private string _firstName;
        private string _lastName;
        private string _facultyNumber;
        private string _email;
        private DateTime _dateOfBirth;

        public Guid Id
        {
            get => _id;
            set => _id = value;
        }

        public string FirstName
        {
            get => _firstName;
            set => _firstName = value ?? throw new ArgumentNullException(nameof(FirstName));
        }

        public string LastName
        {
            get => _lastName;
            set => _lastName = value ?? throw new ArgumentNullException(nameof(LastName));
        }

        public string FacultyNumber
        {
            get => _facultyNumber;
            set => _facultyNumber = value ?? throw new ArgumentNullException(nameof(FacultyNumber));
        }

        public string Email
        {
            get => _email;
            set => _email = value ?? string.Empty;
        }

        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set => _dateOfBirth = value;
        }

        // Публичен безпараметров конструктор е необходим за XmlSerializer
        public Student()
        {
            Id = Guid.NewGuid();
        }

        public Student(string firstName, string lastName, string facultyNumber, string email, DateTime dateOfBirth)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            FacultyNumber = facultyNumber;
            Email = email;
            DateOfBirth = dateOfBirth;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} ({FacultyNumber})";
        }
    }
}
