﻿namespace Application.Interfaces.Persistence
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> Repository<T>() where T : class;

        Task<int> SaveChangesAsync();
    }
}
