﻿namespace CarStore.Domain.Repositories
{
    public interface IUnitOfWork
    {
        public Task Commit();

    }
}
