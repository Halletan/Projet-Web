using System.Reflection;
using Application.Common.Interfaces;
using AutoMapper;

namespace SpaceAdventures.Application.Common.Mappings;

public class MappingProfile : Profile // I just forgot to implement the profile Class :)
{
    public MappingProfile()
    {
        // Calling the private method
        ApplyMappingFromAssembly(Assembly.GetExecutingAssembly());
    }

    private void ApplyMappingFromAssembly(Assembly assembly)
    {
        // Getting the list of classes and all types that implement IMapFrom

        var types = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
            .ToList();

        // Here we're looping through all elements in order to apply the method Mapping

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod("Mapping") ??
                             type.GetInterface("IMapFrom`1")!.GetMethod("Mapping");
            methodInfo?.Invoke(instance, new object[] {this});
        }
    }
}