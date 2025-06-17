namespace Gradebook.Web;

public static class MapperConfig
{
    public static void ConfigureMappings()
    {
        TypeAdapterConfig<HeadmasterDto, CreateRoleUserViewModel<HeadmasterViewModel>>.NewConfig()
            .Map(dest => dest.Role, src => src)
            .Map(dest => dest.User, src => src.User);

        TypeAdapterConfig<TeacherDto, CreateRoleUserViewModel<TeacherViewModel>>.NewConfig()
            .Map(dest => dest.Role, src => src)
            .Map(dest => dest.User, src => src.User);

        //TypeAdapterConfig<StudentDto, CreateRoleUserViewModel<StudentViewModel>>.NewConfig()
        //    .Map(dest => dest.Role, src => src)
        //    .Map(dest => dest.User, src => src.User);

        //TypeAdapterConfig<ParentDto, CreateRoleUserViewModel<ParentViewModel>>.NewConfig()
        //    .Map(dest => dest.Role, src => src)
        //    .Map(dest => dest.User, src => src.User);
    }
}
