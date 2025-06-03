namespace Gradebook.Shared.Constants;

public static class IndexConstants
{
    public const string SCHOOL_NAME_INDEX = "IX_School_Name";
    public static IndexViolation SchoolNameViolation => new(SCHOOL_NAME_INDEX, "School name must be unique.");

    public const string CLASS_SIGNATURE_INDEX = "IX_Class_Year_Signature_SchoolId";
    public static IndexViolation ClassSignatureViolation => new(CLASS_SIGNATURE_INDEX, "Class already exists in this school.");

    public const string SUBJECT_NAME_INDEX = "IX_Subject_Name_SchoolId";
    public static IndexViolation SubjectNameViolation => new(SUBJECT_NAME_INDEX, "Subject name must be unique.");

    public const string SCHOOL_YEAR_UNIQUE_INDEX = "IX_SchoolYear_SchoolId_Year_Semester";
    public static IndexViolation SchoolYearUniqueViolation => new(SCHOOL_YEAR_UNIQUE_INDEX, "School year must be unique for the given school, year, and semester.");
}
