using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SpaceAdventures.Application.Common.Commands.Membership;
using SpaceAdventures.Application.Common.Exceptions;
using SpaceAdventures.Application.Common.Interfaces;
using SpaceAdventures.Application.Common.Queries.Membership;

namespace SpaceAdventures.Infrastructure.Persistence.Services;

public class MembershipService : IMembershipService
{
    private readonly ISpaceAdventureDbContext _context;
    private readonly IMapper _mapper;

    public MembershipService(ISpaceAdventureDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<MembershipVm> GetAllMemberships(CancellationToken cancellationToken)
    {
        return new MembershipVm
        {
            MembershipsList = await _context.MembershipTypes
                .ProjectTo<MembershipDto>(_mapper.ConfigurationProvider)
                .OrderBy(c => c.Name)
                .ToListAsync(cancellationToken)
        };
    }

    public async Task<MembershipDto> GetMembershipById(int MembershipId, CancellationToken cancellation = default)
    {
        var Membership = await _context.MembershipTypes.FindAsync(MembershipId);
        if (Membership == null) throw new NotFoundException("Membership", MembershipId);

        return _mapper.Map<MembershipDto>(Membership);
    }

    public async Task<MembershipDto> CreateMembership(MembershipInput MembershipInput,
        CancellationToken cancellation = default)
    {
        var Membership = _mapper.Map<MembershipType>(MembershipInput);

        try
        {
            await _context.MembershipTypes.AddAsync(Membership, cancellation);
            await _context.SaveChangesAsync(cancellation);
            return _mapper.Map<MembershipDto>(Membership);
        }
        catch (Exception)
        {
            throw new ValidationException();
        }
    }

    public async Task<MembershipDto> UpdateMembership(int MembershipId, MembershipInput MembershipInput,
        CancellationToken cancellation = default)
    {
        var Membership = await _context.MembershipTypes.FindAsync(MembershipId);

        if (Membership == null) throw new NotFoundException("Membership", MembershipId);

        try
        {
            Membership.Name = MembershipInput.Name;
            Membership.IdMemberShipType = MembershipInput.IdMemberShipType;

            _context.MembershipTypes.Update(Membership);
            await _context.SaveChangesAsync(cancellation);
            return _mapper.Map<MembershipDto>(Membership);
        }
        catch (Exception)
        {
            throw new ValidationException();
        }
    }

    public async Task DeleteMembership(int MembershipId, CancellationToken cancellation = default)
    {
        var Membership = await _context.MembershipTypes.FindAsync(MembershipId);

        if (Membership == null) throw new NotFoundException("Membership", MembershipId);
        _context.MembershipTypes.Remove(Membership);
        await _context.SaveChangesAsync(cancellation);
    }

    public async Task<bool> MembershipExists(string name)
    {
        return await _context.MembershipTypes.AnyAsync(c => c.Name == name);
    }

    public bool MembershipExists(int id, MembershipInput MembershipInput)
    {
        return _context.MembershipTypes.Any(c => c.IdMemberShipType != id && c.Name == MembershipInput.Name);
    }
}