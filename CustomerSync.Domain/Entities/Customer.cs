
namespace CustomerSync.Domain.Entities;

/// <summary>
/// Represents a customer within the CustomerSync Platform.
/// </summary>
public class Customer
{
    /// <summary>
    /// Unique identifier for the customer.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Customer email address.
    /// Used as the primary deduplication key.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Customer first name.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Customer last name.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Customer geographic region.
    /// Examples: UK, US, EU.
    /// </summary>
    public string Region { get; set; } = string.Empty;
}