using AutoMapper;
using Domain_Models;
using DTOs.CommentDto;
using DTOs.Image;
using DTOs.Post;
using DTOs.User;
namespace Mappers.MapperConfig
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<Comment, CommentDto>()
                .ForMember(x => x.UserId, y => y.MapFrom(z => z.UserId));

            CreateMap<PaginatedList, PaginatedListDto>();

            CreateMap<Post, PostDto>()
            .ForMember(x => x.Rating, y => y.MapFrom(z => z.Stars.GetPostRating()))
            .ForMember(x => x.Tags, y => y.MapFrom(z => z.Tags.GetPostTagsBE()))
            .ForMember(x => x.Image, y => y.MapFrom(z => z.Image.Data))
            .ForMember(x => x.Comments, y => y.MapFrom(z => z.Comments.Count));

            CreateMap<Post, PostDetailsDto>()
                .ForMember(x => x.Rating, y => y.MapFrom(z => z.Stars.GetPostRating()))
                .ForMember(x => x.Tags, y => y.MapFrom(z => z.Tags.GetPostTagsBE()))
                .ForMember(x => x.Image, y => y.MapFrom(z => z.Image.Data));


            CreateMap<PostCreateDto, Post>()
                .ForMember(x => x.Tags, y => y.MapFrom(z => z.Tags.GetPostTags()));

            CreateMap<UploadImageDto, Image>().ReverseMap();
            CreateMap<Image, ImageDto>().ReverseMap();
                
        }
    }
}