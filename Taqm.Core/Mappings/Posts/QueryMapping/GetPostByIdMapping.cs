﻿using Taqm.Core.Features.Posts.Queries.Responses;
using Taqm.Data.Entities;

namespace Taqm.Core.Mappings.Posts
{
    public partial class PostProfile
    {
        public void GetPostByIdMapping()
        {
            CreateMap<Post, GetPostByIdResponse>()
                .ForMember(dest => dest.SellerLocation, opt => opt.MapFrom(src => $"{src.User.City} {src.User.Region}"))
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"));
        }
    }
}
