namespace MessageHandlingSystem.Application.Mapping.Handlers;

public interface IMappingHandler<T, TDto>
{
    TDto Handle(T value);
    IMappingHandler<T, TDto> SetNext(IMappingHandler<T, TDto> handler);
}