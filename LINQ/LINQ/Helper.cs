using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LINQ
{
    partial class Program
    {
        static class Helper
        {
            // IsRandom:
            // true - zawsze generuje inną liste użytkowników 
            // false - zawsze generuje tą samą listę użytkowników
            public static bool IsRandom = true;
            public static Random Rand { get; private set; }
            public static string[] Names { get; private set; }
            static Helper()
            {
                if (Helper.IsRandom)
                { Helper.Rand = new Random(); }
                else { Helper.Rand = new Random(0); }

                Helper.Names = new string[] 
            { 
                "Zuza", "Ola", "Jaś", "Marcin", "Monika",
                "Paula", "Paweł", "Asia", "Jacek", "Stasiu", 
            };
            }
        }
        interface IUser
        {
            int Id { get; }
            string Name { get; set; }
            DateTime Date { get; set; }
            double HealthLevel { get; set; }
        }
        class Patient : IUser
        {
            private int id;
            private static int NextId;
            public int Id
            {
                get { return id; }
            }
            public string Name { get; set; }
            public DateTime Date { get; set; }
            public double HealthLevel { get; set; }

            public Patient(string name, double healthLevel)
            {
                this.id = NextId;
                this.Name = name;
                this.HealthLevel = healthLevel;
                this.Date = DateTime.Now;

                Patient.NextId++;
            }
            public override string ToString()
            {
                return string.Format("Id: {0} imię pacjenta: {1} wartość: {2}",
                    Id, Name, Math.Round(HealthLevel, 3));
            }
        }
        class Doctor : IUser
        {
            private int id;
            private static int NextId;
            public int Id
            {
                get { return id; }
            }
            public string Name { get; set; }
            public DateTime Date { get; set; }
            public double HealthLevel { get; set; }
            public List<Patient> PacientList { get; private set; }
            public int PacientCount { get { return this.PacientList.Count; } }

            public Doctor(string name, double healthLevel)
            {
                this.id = NextId;
                this.Name = name;
                this.HealthLevel = healthLevel;
                this.Date = DateTime.Now;

                PreparePatientList();
                Doctor.NextId++;
            }
            private void PreparePatientList()
            {
                var r = Helper.Rand;
                var names = Helper.Names;

                int patientCount = r.Next(1, 10);
                this.PacientList = new List<Patient>();
                for (int i = 0; i < patientCount; i++)
                {
                    double nextHealthLevel = r.NextDouble();
                    int indName = r.Next(0, names.Length);

                    var newPatient = new Patient(names[indName], nextHealthLevel);
                    this.PacientList.Add(newPatient);
                }
            }
            public override string ToString()
            {
                return string.Format("Id: {0} imię doktora: {1} wartość: {2} lb pacjentów: {3}",
                    Id, Name, Math.Round(HealthLevel, 3), PacientCount);
            }
        }

        struct PatientInfo
        {
            public double Temperature { get; set; }
            public string Diagnosis { get; set; }
        }
    }
}
