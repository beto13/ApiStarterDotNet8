﻿using Application.Dtos.User;
using Application.Filtering.Interfaces;
using Application.Filtering.Strategies;
using Domain.Entities;

namespace Application.Filtering.Factories
{
    public class UserFilterStrategyFactory : IFilterStrategyFactory<User, UserFilterDto>
    {
        public List<IFilterStrategy<User>> CreateStrategies(UserFilterDto filter)
        {
            var filters = new List<IFilterStrategy<User>>();

            var actions = new List<(Func<bool> condition, Func<IFilterStrategy<User>> strategy)>
            {
                (() => !string.IsNullOrEmpty(filter.Email),() => new ContainsFilterStrategy<User>(u => u.Email, filter.Email!)),
                (() => !string.IsNullOrEmpty(filter.Name),() => new ContainsFilterStrategy<User>(u => u.Name, filter.Name!)),
                (() => filter.DateFrom.HasValue && filter.DateTo.HasValue, () => new DateRangeFilterStrategy<User>(u => u.CreatedAt, filter.DateFrom!.Value, filter.DateTo!.Value))
            };

            foreach (var (condition, strategy) in actions)
            {
                if (condition()) filters.Add(strategy());
            }

            return filters;
        }
    }
}
