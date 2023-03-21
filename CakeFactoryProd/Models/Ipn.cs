﻿using System;
using System.Collections.Generic;

namespace CakeFactoryProd.Models;

public partial class IPN
{
    public int Id { get; set; }

    public string? Custom { get; set; }

    public string? PaymentId { get; set; }

    public string? Cart { get; set; }

    public string CreateTime { get; set; } = null!;

    public string? PayerId { get; set; }

    public string? PayerFirstName { get; set; }

    public string? PayerLastName { get; set; }

    public string? PayerMiddleName { get; set; }

    public string? PayerEmail { get; set; }

    public string? PayerCountryCode { get; set; }

    public string? PayerStatus { get; set; }

    public string? Amount { get; set; }

    public string? Currency { get; set; }

    public string? Intent { get; set; }

    public string? PaymentMethod { get; set; }

    public string? PaymentState { get; set; }

    public int? OrderId { get; set; }
    public virtual Order Order { get; set; } = null!;

}
