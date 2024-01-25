namespace RajaFath.Interfaces
{
    public interface IMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
