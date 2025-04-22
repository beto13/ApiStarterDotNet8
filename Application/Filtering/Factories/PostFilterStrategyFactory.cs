using Application.Dtos.Posts;
using Application.Filtering.Interfaces;
using Application.Filtering.Strategies;
using Domain.Entities;

namespace Application.Filtering.Factories
{
    public class PostFilterStrategyFactory : IFilterStrategyFactory<Post, PostFilterDto>
    {
        public List<IFilterStrategy<Post>> CreateStrategies(PostFilterDto filter)
        {
            var filters = new List<IFilterStrategy<Post>>();
            var actions = new List<(Func<bool> condition, Func<IFilterStrategy<Post>> strategy)>
            {
                (() => !string.IsNullOrEmpty(filter.Title),() => new ContainsFilterStrategy<Post>(p => p.Title, filter.Title!)),
            };

            foreach (var (condition, strategy) in actions)
            {
                if (condition()) filters.Add(strategy());
            }

            return filters;
        }
    }
}
