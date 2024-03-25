using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sys = Cosmos.System;

namespace MyOS
{
    public class Kernel : Sys.Kernel
    {

        private List<PatientRecord> patientRecords;

        public class PatientRecord
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public string Diagnosis { get; set; }
            public List<string> Medications { get; set; }
            public List<string> Appointments { get; set; }
        }

        protected override void BeforeRun()
        {
            Console.WriteLine("Medical File System");
            patientRecords = new List<PatientRecord>();
        }

        protected override void Run()
        {
            Console.WriteLine("Main Menu:");
            Console.WriteLine("1. Create Patient Record");
            Console.WriteLine("2. View Patient Records");
            Console.WriteLine("3. Add Medication to Patient");
            Console.WriteLine("4. Schedule Appointment for Patient");
            Console.WriteLine("5. Search and Filter Patients");
            Console.WriteLine("6. Exit");

            var choice = Console.ReadKey();
            Console.WriteLine();

            switch (choice.KeyChar)
            {
                case '1':
                    CreatePatientRecord();
                    break;
                case '2':
                    ViewPatientRecords();
                    break;
                case '3':
                    AddMedicationToPatient();
                    break;
                case '4':
                    ScheduleAppointmentForPatient();
                    break;
                case '5':
                    SearchAndFilterPatients();
                    break;
                case '6':
                    Cosmos.System.Power.Shutdown();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        private void CreatePatientRecord()
        {
            Console.Write("Enter Patient Name: ");
            var name = Console.ReadLine();
            Console.Write("Enter Patient Age: ");
            if (int.TryParse(Console.ReadLine(), out int age))
            {
                Console.Write("Enter Diagnosis: ");
                var diagnosis = Console.ReadLine();
                var record = new PatientRecord
                {
                    Name = name,
                    Age = age,
                    Diagnosis = diagnosis,
                    Medications = new List<string>(),
                    Appointments = new List<string>()
                };
                patientRecords.Add(record);
                Console.WriteLine("Patient Record Created.");
            }
            else
            {
                Console.WriteLine("Invalid age. Please try again.");
            }
        }

        private void ViewPatientRecords()
        {
            Console.WriteLine("Patient Records:");
            foreach (var record in patientRecords)
            {
                Console.WriteLine($"Name: {record.Name}, Age: {record.Age}, Diagnosis: {record.Diagnosis}");
                Console.WriteLine("Medications:");
                foreach (var medication in record.Medications)
                {
                    Console.WriteLine($"- {medication}");
                }
                Console.WriteLine("Appointments:");
                foreach (var appointment in record.Appointments)
                {
                    Console.WriteLine($"- {appointment}");
                }
            }
        }

        private void AddMedicationToPatient()
        {
            Console.Write("Enter Patient Name: ");
            var name = Console.ReadLine();
            var patient = patientRecords.Find(p => p.Name == name);
            if (patient != null)
            {
                Console.Write("Enter Medication: ");
                var medication = Console.ReadLine();
                patient.Medications.Add(medication);
                Console.WriteLine("Medication added to patient.");
            }
            else
            {
                Console.WriteLine("Patient not found. Please try again.");
            }
        }

        private void ScheduleAppointmentForPatient()
        {
            Console.Write("Enter Patient Name: ");
            var name = Console.ReadLine();
            var patient = patientRecords.Find(p => p.Name == name);
            if (patient != null)
            {
                Console.Write("Enter Appointment Date and Time: ");
                var appointment = Console.ReadLine();
                patient.Appointments.Add(appointment);
                Console.WriteLine("Appointment scheduled for the patient.");
            }
            else
            {
                Console.WriteLine("Patient not found. Please try again.");
            }
        }

        private void SearchAndFilterPatients()
        {
            Console.WriteLine("Search and Filter Patients:");
            Console.WriteLine("1. Search by Name");
            Console.WriteLine("2. Search by Age");
            Console.WriteLine("3. Search by Diagnosis");
            Console.WriteLine("4. Back to Main Menu");

            var choice = Console.ReadKey();
            Console.WriteLine();

            switch (choice.KeyChar)
            {
                case '1':
                    Console.Write("Enter Patient Name to search: ");
                    var searchName = Console.ReadLine();
                    var nameMatches = patientRecords.Where(p => p.Name.Contains(searchName, StringComparison.OrdinalIgnoreCase));
                    DisplayFilteredPatients(nameMatches);
                    break;
                case '2':
                    Console.Write("Enter Patient Age to search: ");
                    if (int.TryParse(Console.ReadLine(), out int searchAge))
                    {
                        var ageMatches = patientRecords.Where(p => p.Age == searchAge);
                        DisplayFilteredPatients(ageMatches);
                    }
                    else
                    {
                        Console.WriteLine("Invalid age. Please try again.");
                    }
                    break;
                case '3':
                    Console.Write("Enter Diagnosis to search: ");
                    var searchDiagnosis = Console.ReadLine();
                    var diagnosisMatches = patientRecords.Where(p => p.Diagnosis.Contains(searchDiagnosis, StringComparison.OrdinalIgnoreCase));
                    DisplayFilteredPatients(diagnosisMatches);
                    break;
                case '4':
                    // Return to the main menu
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        private void DisplayFilteredPatients(IEnumerable<PatientRecord> filteredPatients)
        {
            Console.WriteLine("Filtered Patients:");
            foreach (var record in filteredPatients)
            {
                Console.WriteLine($"Name: {record.Name}, Age: {record.Age}, Diagnosis: {record.Diagnosis}");
                Console.WriteLine("Medications:");
                foreach (var medication in record.Medications)
                {
                    Console.WriteLine($"- {medication}");
                }
                Console.WriteLine("Appointments:");
                foreach (var appointment in record.Appointments)
                {
                    Console.WriteLine($"- {appointment}");
                }
            }
        }


    }
}
