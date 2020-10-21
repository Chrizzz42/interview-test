using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IConventionRepository
{
    Task<bool> RegisterTalk();
    Task<bool> RegisterUserForConvention(int conventionId, int userId);
    Task<ConventionEntity> CreateConvention(Convention convention);
    Task<List<ConventionEntity>> GetConventions();
    Task<ConventionEntity> GetConvention(int id);
    Task<bool> EditConvention(int conventionId, Convention con);
}
