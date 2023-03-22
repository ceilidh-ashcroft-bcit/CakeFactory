using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CakeFactoryProd.Models;

public partial class IPN
{
    [Key]
    public int Id { get; set; }

    public string Custom { get; set; } = string.Empty;

    public string PaymentId { get; set; } = string.Empty;

    public string Cart { get; set; } = string.Empty;

    public string CreateTime { get; set; } = string.Empty;

    public string PayerId { get; set; } = string.Empty;

    public string PayerFirstName { get; set; } = string.Empty;

    public string PayerLastName { get; set; } = string.Empty;

    public string PayerMiddleName { get; set; } = string.Empty;

    public string PayerEmail { get; set; } = string.Empty;

    public string PayerCountryCode { get; set; } = string.Empty;

    public string PayerStatus { get; set; } = string.Empty;

    public string Amount { get; set; } = string.Empty;

    public string Currency { get; set; } = string.Empty;

    public string Intent { get; set; } = string.Empty;

    public string PaymentMethod { get; set; } = string.Empty;

    public string PaymentState { get; set; } = string.Empty;

    public int? OrderId { get; set; }
    public virtual Order Order { get; set; } = null!;

}
