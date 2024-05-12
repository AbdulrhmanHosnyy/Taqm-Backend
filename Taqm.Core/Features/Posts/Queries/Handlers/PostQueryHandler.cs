using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;
using Taqm.Core.Bases;
using Taqm.Core.Features.Posts.Queries.Models;
using Taqm.Core.Features.Posts.Queries.Responses;
using Taqm.Core.Resources;
using Taqm.Core.Wrappers;
using Taqm.Data.Consts;
using Taqm.Data.Entities;
using Taqm.Data.Enums;
using Taqm.Data.Responses;
using Taqm.Service.Abstracts;

namespace Taqm.Core.Features.Posts.Queries.Handlers
{
    public class PostQueryHandler : ResponseHandler,
        IRequestHandler<GetPostByIdQuery, Response<GetPostByIdResponse>>,
        IRequestHandler<GetPostPaginatedListQuery, PaginatedResult<GetPostPagintaedListResponse>>
    {
        #region Fields
        private readonly IPostService _postService;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public PostQueryHandler(IPostService postService, IStringLocalizer<SharedResources> stringLocalizer, IMapper mapper)
            : base(stringLocalizer)
        {
            _postService = postService;
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
        }
        #endregion

        #region Handlers
        public async Task<Response<GetPostByIdResponse>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _postService.GetByIdIncludingUserAsync(request.Id);
            var mappedPost = _mapper.Map<GetPostByIdResponse>(post);
            return mappedPost is null ? NotFound<GetPostByIdResponse>(_stringLocalizer[SharedResourcesKeys.NotFound])
                : Success(mappedPost);
        }

        public async Task<PaginatedResult<GetPostPagintaedListResponse>> Handle(GetPostPaginatedListQuery request, CancellationToken cancellationToken)
        {
            //  Expression to Filter the List
            Expression<Func<Post, bool>> filteringExpression = _postService.FilterPostExpression(request);

            //  Expression to specify the order criteria
            Expression<Func<Post, object>> orderingExpression = _postService.OrderPostExpression(request.OrderBy);

            //  Checking the orderDirection
            var orderDirection = request.OrderBy == (PostOrderingEnum)3 ? OrderBy.Descending : OrderBy.Ascending;

            //  Expression to map from post to the reponse
            Expression<Func<Post, GetPostPagintaedListResponse>> mappingExpression = _postService.MapPostExpression();

            //  Getting the querableList
            var processedPost = _postService.GetListAsQuerable(filteringExpression, orderingExpression, orderDirection);

            //  Mapping The post
            var mappedPost = processedPost.Select(mappingExpression);

            //  Getting the required page and its size with the expression specified
            var paginatedList = await mappedPost.ToPaginatedListAsync(request.PageNumber, request.PageSize);

            //  Return the count of the list in the meta data
            paginatedList.Meta = new { Count = paginatedList.Data.Count() };

            return paginatedList;
        }
        #endregion
    }
}
