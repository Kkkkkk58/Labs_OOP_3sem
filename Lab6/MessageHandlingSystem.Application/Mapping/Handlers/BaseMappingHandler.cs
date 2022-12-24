using System.Diagnostics.CodeAnalysis;
using MessageHandlingSystem.Application.Exceptions;

namespace MessageHandlingSystem.Application.Mapping.Handlers;

public abstract class BaseMappingHandler<T, TDto> : IMappingHandler<T, TDto>
    where TDto : class
{
    private IMappingHandler<T, TDto>? _next;

    public TDto Handle(T value)
    {
        if (TryHandle(value, out TDto? result))
            return result;

        return _next?.Handle(value) ?? throw MappingHandlerException.MappingIsUndefined();
    }

    public IMappingHandler<T, TDto> SetNext(IMappingHandler<T, TDto> handler)
    {
        _next = handler;
        return handler;
    }

    protected abstract bool TryHandle(T value, [NotNullWhen(true)] out TDto? result);
}