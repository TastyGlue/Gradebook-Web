﻿namespace Gradebook.Shared.Constants;

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

    public const string PROFILE_USERID_ADMIN_INDEX = "IX_Profile_UserId_Admin";
    public static IndexViolation ProfileUserIdAdminViolation => new(PROFILE_USERID_ADMIN_INDEX, "This user is already registered as an Administrator.");

    public const string PROFILE_USERID_HEADMASTER_INDEX = "IX_Profile_UserId_Headmaster";
    public static IndexViolation ProfileUserIdHeadmasterViolation => new(PROFILE_USERID_HEADMASTER_INDEX, "This user is already registered as a Headmaster in this school.");

    public const string PROFILE_USERID_TEACHER_INDEX = "IX_Profile_UserId_Teacher";
    public static IndexViolation ProfileUserIdTeacherViolation => new(PROFILE_USERID_TEACHER_INDEX, "This user is already registered as a Teacher in this school.");

    public const string PROFILE_USERID_STUDENT_INDEX = "IX_Profile_UserId_Student";
    public static IndexViolation ProfileUserIdStudentViolation => new(PROFILE_USERID_STUDENT_INDEX, "This user is already registered as a Student in this school.");

    public const string PROFILE_USERID_PARENT_INDEX = "IX_Profile_UserId_Parent";
    public static IndexViolation ProfileUserIdParentViolation => new(PROFILE_USERID_PARENT_INDEX, "This user is already registered as a Parent.");

    public static readonly HashSet<IndexViolation> IndexViolations =
    [
        SchoolNameViolation,
        ClassSignatureViolation,
        SubjectNameViolation,
        SchoolYearUniqueViolation,
        ProfileUserIdAdminViolation,
        ProfileUserIdHeadmasterViolation,
        ProfileUserIdTeacherViolation,
        ProfileUserIdStudentViolation,
        ProfileUserIdParentViolation
    ];
}
