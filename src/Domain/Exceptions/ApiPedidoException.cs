using apiOnBording.Domain.Common;
using System;

namespace apiOnBording.Domain.Exceptions;

public class ApiPedidoException : Exception
{
    private readonly ErrorMessage _errorMessage;

    public ErrorMessage ErrorMessage => _errorMessage;

    public ApiPedidoException(ErrorMessage errorMessage, Exception ex)
        : base(errorMessage.Detail, ex)
    {
        _errorMessage = errorMessage;
    }

    public ApiPedidoException(ErrorMessage errorMessage)
       : base(errorMessage.Detail)
    {
        _errorMessage = errorMessage;
    }

}
