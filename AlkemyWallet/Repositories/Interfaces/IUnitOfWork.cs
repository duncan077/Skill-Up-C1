﻿using AlkemyWallet.Entities;

namespace AlkemyWallet.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<RoleEntity> RolesRepository { get; }
        IGenericRepository<TransactionEntity> TransactionRepository { get; }
        IGenericRepository<AccountsEntity> AccountsRepository { get; }
        IGenericRepository<FixedTermDepositEntity> FixedTermDeposit { get; }
        void Save();
    }
}
