namespace Banks.Models;

public class InterestOnBalancePolicy
{
    private readonly List<InterestOnBalanceLayer> _layers;

    public InterestOnBalancePolicy()
        : this(new List<InterestOnBalanceLayer>())
    {
    }

    public InterestOnBalancePolicy(IEnumerable<InterestOnBalanceLayer> layers)
    {
        _layers = layers.OrderBy(layer => layer.RequiredInitialBalance).ToList();
    }

    public IReadOnlyCollection<InterestOnBalanceLayer> Layers => _layers.AsReadOnly();

    public InterestOnBalanceLayer AddLayer(InterestOnBalanceLayer layer)
    {
        _layers.Add(layer);
        _layers.Sort((a, b) => a.RequiredInitialBalance.CompareTo(b.RequiredInitialBalance));
        return layer;
    }

    public void RemoveLayer(InterestOnBalanceLayer layer)
    {
        _layers.Remove(layer);
    }
}