using AutoMapper;
using Domain_Models;
using DTOs.CommentDto;
using DTOs.Image;
using DTOs.NewsLetter;
using DTOs.Post;
using DTOs.User;
using Microsoft.VisualBasic;
using System.IO.Compression;
namespace Mappers.MapperConfig
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<NewsLetter, NewsLetterDto>()
                .ForMember(x => x.Tags, y => y.MapFrom(z => z.Tags.GetPostTagsBE()));
                //.ForMember(x => x.Authors, y => y.MapFrom(z => z.Authors));

            CreateMap<User, UserDto>();
            CreateMap<User, DetailedUserDto>();
            CreateMap<User, UserWithNewsLettersDto>()
                .ForMember(x => x.NewsLetter, y => y.MapFrom(z => z.NewsLetters.Count > 0 ? z.NewsLetters.First() : null));

            CreateMap<Comment, CommentDto>()
                .ForMember(x => x.UserId, y => y.MapFrom(z => z.UserId));

            CreateMap<Comment, CommentResponseDto>();

            CreateMap<PaginatedList, PaginatedListDto>();

            CreateMap<Post, PostDto>()
            .ForMember(x => x.Rating, y => y.MapFrom(z => z.Stars.GetPostRating()))
            .ForMember(x => x.Tags, y => y.MapFrom(z => z.Tags.GetPostTagsBE()))
            .ForMember(x => x.Image, y => y.MapFrom(z => z.ImageBase64 ?? z.Image.Data))
            .ForMember(x => x.Comments, y => y.MapFrom(z => z.Comments.Count))
            .ForMember(x => x.User, y => y.MapFrom(z => z.User))
            .ForMember(x => x.Ratings, y => y.MapFrom(z => z.Stars.Count));


            CreateMap<Post, PostDetailsDto>()
                .ForMember(x => x.Rating, y => y.MapFrom(z => z.Stars.GetPostRating()))
                .ForMember(x => x.Tags, y => y.MapFrom(z => z.Tags.GetPostTagsBE()))
                .ForMember(dto => dto.Image, opt => opt.MapFrom(src => src.ImageBase64 ?? src.Image.Data));

            CreateMap<PostCreateDto, Post>()
                .ForMember(x => x.Tags, y => y.MapFrom(z => z.Tags.GetPostTags()));

            CreateMap<UploadImageDto, Image>().ReverseMap();
            CreateMap<Image, ImageDto>().ReverseMap();
                
        }
    }
}