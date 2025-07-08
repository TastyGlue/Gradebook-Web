using Gradebook.Shared.Models.DTOs;

namespace Gradebook.API
{
    public static class MapperConfig
    {
        
        public static void ConfigureMappings()
        {
            TypeAdapterConfig<Absence, AbsenceDto>.NewConfig()
                .MaxDepth(3);
            TypeAdapterConfig<Admin, AdminDto>.NewConfig()
                .MaxDepth(3);
            TypeAdapterConfig<Class, ClassDto>.NewConfig()
                .MaxDepth(3);
            TypeAdapterConfig<Grade, GradeDto>.NewConfig()
                .MaxDepth(3);
            TypeAdapterConfig<Headmaster, HeadmasterDto>.NewConfig()
                .MaxDepth(3);
            TypeAdapterConfig<Parent, ParentDto>.NewConfig()
                .MaxDepth(3);
            TypeAdapterConfig<Profile, ProfileDto>.NewConfig()
                .MaxDepth(3);
            TypeAdapterConfig<School, SchoolDto>.NewConfig()
                .MaxDepth(3);
            TypeAdapterConfig<SchoolYear, SchoolYearDto>.NewConfig()
                .MaxDepth(3);
            TypeAdapterConfig<Student, StudentDto>.NewConfig()
                .MaxDepth(3);
            TypeAdapterConfig<Subject, SubjectDto>.NewConfig()
                .MaxDepth(3);
            TypeAdapterConfig<Teacher, TeacherDto>.NewConfig()
                .MaxDepth(3);
            TypeAdapterConfig<Timetable, TimetableDto>.NewConfig()
                .MaxDepth(4);
            TypeAdapterConfig<User, UserDto>.NewConfig()
                .MaxDepth(3);
            TypeAdapterConfig<UserDto, User>.NewConfig()
                .Ignore(dest => dest.Profiles);
        }
    }
}
