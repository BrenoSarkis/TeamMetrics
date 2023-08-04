namespace TeamMetrics.Api.Mappers;

public static class Mapper {
    public static Target Map<Source, Target>(Source source, Func<Source, Target> mappingStrategy) {
        return mappingStrategy(source);
    }

    public static IEnumerable<Target> Map<Source, Target>(IEnumerable<Source> source, Func<Source, Target> mappingStrategy) {
        return source.Select(mappingStrategy);
    }
}
