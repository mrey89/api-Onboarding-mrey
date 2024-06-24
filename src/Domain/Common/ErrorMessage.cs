using System;

namespace apiOnBording.Domain.Common;

public class ErrorMessage
{
    public const string NOT_FOUND_RECORD = "{0} with id: {1} not found";

    public const string INTERNAL_ERROR_RECORD = "Error interno al dar de alta {0} con id {1}";
  
    public string Detail { get; set; }
    public int Status { get; set; }
    public string Title { get; set; }
}
