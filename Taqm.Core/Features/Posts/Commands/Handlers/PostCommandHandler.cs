using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Taqm.Core.Bases;
using Taqm.Core.Features.Posts.Commands.Models;
using Taqm.Core.Resources;
using Taqm.Data.Entities;
using Taqm.Service.Abstracts;

namespace Taqm.Core.Features.Posts.Commands.Handlers
{
    public class PostCommandHandler : ResponseHandler,
        IRequestHandler<CreatePostCommand, Response<string>>,
        IRequestHandler<UpdatePostCommand, Response<string>>,
        IRequestHandler<RenewPostCommand, Response<string>>,
        IRequestHandler<DeletePostCommand, Response<string>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IPostService _postService;
        #endregion

        #region Constructors
        public PostCommandHandler(IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer, IPostService postService) : base(stringLocalizer)
        {
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _postService = postService;
        }
        #endregion

        #region Handlers
        public async Task<Response<string>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            //  Mapping Post
            var post = _mapper.Map<Post>(request);

            // Create Post
            var result = await _postService.CreateAsync(post, request.ProductImage);
            switch (result)
            {
                case "FailedToUploadImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUploadImage]);
                case "Failed": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToCreate]);
            }
            return Success("");
        }
        public async Task<Response<string>> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            //  Check user existance
            var oldPost = await _postService.GetByIdAsync(request.PostId);
            if (oldPost is null) return NotFound<string>();

            //  Mapping
            var newPost = _mapper.Map(request, oldPost);

            //  Updating
            var result = await _postService.UpdateAsync(newPost, request.ProductImage);

            // Return message
            switch (result)
            {
                case "FailedToUploadImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUploadImage]);
                case "Failed": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UpdateFailed]);
            }
            return Success<string>(_stringLocalizer[SharedResourcesKeys.Updated]);
        }
        public async Task<Response<string>> Handle(RenewPostCommand request, CancellationToken cancellationToken)
        {
            return await _postService.RenewAsync(request.Id) ?
                Success((string)_stringLocalizer[SharedResourcesKeys.RenewedSuccessfully]) :
                BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToRenew]);
        }
        public async Task<Response<string>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var deleteResult = await _postService.DeleteAsync(request.Id);

            switch (deleteResult)
            {
                case "NotFound": return NotFound<string>();
                case "Failed": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.DeleteFailed]);
                case "Success": return Success((string)_stringLocalizer[SharedResourcesKeys.Deleted]);
                default: return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.DeleteFailed]);
            }
        }
        #endregion
    }
}
