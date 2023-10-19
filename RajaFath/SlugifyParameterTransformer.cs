public class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string TransformOutbound(object? value)
    {
        if (value == null) return null;
        return value.ToString().Replace(" ", "-");
    }
}