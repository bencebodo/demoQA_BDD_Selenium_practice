namespace Demoqa_PageObjects.Models
{
    public class StudentFormData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string DateOfBirthRaw { get; set; }
        public string Subjects { get; set; }
        public string Hobbies { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        public StudentFormData(Dictionary<string, string> data)
        {
            FirstName = data["First Name"];
            LastName = data["Last Name"];
            Email = data["Email"];
            Gender = data["Gender"];
            Mobile = data["Mobile"];
            DateOfBirthRaw = data["Date of Birth"];
            Subjects = data["Subjects"];
            Hobbies = data["Hobbies"];
            State = data["State"];
            City = data["City"];
        }

        public string ExpectedName => $"{FirstName} {LastName}";
        public string ExpectedStateAndCity => $"{State} {City}";

        public string ExpectedDateOfBirth
        {
            get
            {
                DateTime date = DateTime.ParseExact(DateOfBirthRaw, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);

                return date.ToString("dd MMMM,yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        public Dictionary<string, string> GetExpectedResultMap(HashSet<string> labels)
        {
            var resultMap = new Dictionary<string, string>();
            foreach (string label in labels)
            {
                string? value = label switch
                {
                    "Student Name" => ExpectedName,
                    "Student Email" => Email,
                    "Gender" => Gender,
                    "Mobile" => Mobile,
                    "Date of Birth" => ExpectedDateOfBirth,
                    "Subjects" => Subjects,
                    "Hobbies" => Hobbies,
                    "Picture" => null,
                    "State and City" => ExpectedStateAndCity,
                    _ => null
                };

                if (value != null)
                {
                    resultMap.Add(label, value);
                }
            }
            return resultMap;
        }

    }
}
