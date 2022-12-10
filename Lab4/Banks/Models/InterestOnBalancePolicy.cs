using Banks.Exceptions;

namespace Banks.Models;

public record InterestOnBalancePolicy
{
    public InterestOnBalancePolicy(IReadOnlyCollection<InterestOnBalanceLayer> layers)
    {
        CheckForIntersections(layers);
        Layers = layers
            .OrderBy(layer => layer.RequiredInitialBalance)
            .ToList();
    }

    public IReadOnlyCollection<InterestOnBalanceLayer> Layers { get; }

    private static void CheckForIntersections(IReadOnlyCollection<InterestOnBalanceLayer> layers)
    {
        if (layers.DistinctBy(layer => layer.RequiredInitialBalance).Count() != layers.Count)
            throw InterestOnBalancePolicyException.LayersWithIntersectionsByInitialBalance();
    }
}