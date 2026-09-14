using System;

namespace HipoSim.Platform.PropertyCatalog.Application.DataTransferObjects;

public record ProjectResponse(Guid Id, string Name, string Location, decimal BasePrice);