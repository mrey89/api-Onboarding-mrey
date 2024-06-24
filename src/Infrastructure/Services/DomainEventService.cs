using apiOnBording.Application.Common.Interfaces;
using apiOnBording.Domain.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiOnBording.Infrastructure.Services
{
    public class DomainEventService : IDomainEventService
    {
        private readonly ILogger<DomainEventService> _logger;

        public DomainEventService(ILogger<DomainEventService> logger)
        {
            _logger = logger;
        }

        public Task Publish(DomainEvent domainEvent)
        {
            // publish
            throw new NotImplementedException();
        }

    }

}
