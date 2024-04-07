using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Taqm.Core.Bases;
using Taqm.Core.Features.Posts.Queries.Models;
using Taqm.Core.Features.Posts.Queries.Responses;
using Taqm.Core.Resources;
using Taqm.Service.Abstracts;

namespace Taqm.Core.Features.Posts.Queries.Handlers
{
    public class PostHandler : ResponseHandler, IRequestHandler<GetPostByIdQuery, Response<GetPostByIdResponse>>
    {
        #region Fields
        private readonly IPostureService _postureService;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper;
        #endregion
        #region Constructors
        public PostHandler(IPostureService postureService, IStringLocalizer<SharedResources> stringLocalizer, IMapper mapper)
            : base(stringLocalizer)
        {
            _postureService = postureService;
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
        }
        #endregion
        #region Methods
        public async Task<Response<GetPostByIdResponse>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _postureService.GetByIdAsync(request.Id);
            var mappedPost = _mapper.Map<GetPostByIdResponse>(post);
            return mappedPost is null ? NotFound<GetPostByIdResponse>(_stringLocalizer[SharedResourcesKeys.NotFound])
                : Success(mappedPost);

        }
        #endregion

    }
}
