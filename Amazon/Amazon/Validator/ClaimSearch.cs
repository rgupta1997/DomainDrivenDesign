namespace Amazon.Validator
{
    public class ClaimSearch
    {
        public int? PolicyNumber { get; set; }
        public int? ClaimNumber { get; set; }
        public int? OhidNumber { get; set; }
        public string? DOB { get; set; }
        public string? InsuredName { get; set; }
        public int? HospitalId { get; set; }
        public string? HospitalName { get; set; }
        public long? MobileNumber { get; set; }
        public string? EmailId { get; set; }
        public List<Item> items { get; set; }

    }

    public class Item
    {
        public string DocumentName { get; set; } // alpha with space -non empty
        public string DocumentNumber { get; set; } //aplha numer - non empty
    }

    public class MedicalDetails
    {
        public string LineOfTreatment { get; set; }
        public string Duration { get; set; }
        public bool IsCritical { get; set; }
        public List<DiagnosisDetails> diagnosis { get; set; }
    }
    public class DiagnosisDetails
    {
        public string DaignosisCode { get; set; }
        public string CPTCode { get; set; }


    }
}


