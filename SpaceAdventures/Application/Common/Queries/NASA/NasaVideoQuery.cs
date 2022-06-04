using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Models.APIConsume;

namespace SpaceAdventures.Application.Common.Queries.NASA;

public record NasaVideoQuery(string path) : IRequest<List<string>>;

public class NasaVideoQueryHandler : IRequestHandler<NasaVideoQuery, List<string>>
{
    private readonly INasaApiService _nasaApiService;

    public NasaVideoQueryHandler(INasaApiService nasaApiService)
    {
        _nasaApiService = nasaApiService;
    }

    public async Task<List<string>> Handle(NasaVideoQuery request, CancellationToken cancellationToken)
    {
        return await _nasaApiService.GetNasaVideo(request.path, cancellationToken);
    }
}