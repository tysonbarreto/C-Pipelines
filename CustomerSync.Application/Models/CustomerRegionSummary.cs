namespace CustomerSync.Application.Models;

/// <summary>
/// Represents a summary of customers within a region.
/// </summary>
public sealed class CustomerRegionSummary
{
    /// <summary>
    /// Region name.
    /// </summary>
    public string Region { get; init; } = string.Empty;

    /// <summary>
    /// Number of customers in the region.
    /// </summary>
    public int CustomerCount { get; init; }
}