//using Domain_Models;
//using DTOs.Post;

//namespace Mappers
//{
//    public static class PostMapper
//    {
//        public static PostDto ToModel(this Post model)
//        {
//            var dto = new PostDto();
//            if(model != null)
//            {
//                dto.Id = model.Id;
//                dto.Title = model.Title;
//                dto.Description = model.Description;
//                dto.Image = model.Image;
//                dto.PostingTime = model.PostingTime;
//                //dto.Tags = model.Tags,
//                //dto.Rating = model.Stars.Sum(x => x.Rating) / model.Stars.Count(),
//            }
//            return dto;
//        }
//        public static PostDetailsDto ToDetailedModel(this Post model)
//        {
//            var dto = new PostDetailsDto();
//            if(model != null)
//            {
//                dto.Title = model.Title;
//                dto.Text = model.Text;
//                dto.User = model.User.ToUserDto();
//                //dto.Rating = model.Stars.Sum(x => x.Rating) / model.Stars.Count;
//                dto.Comments = model.Comments;
//                dto.PostingTime = model.PostingTime;
//                //dto.Image = model.Image;
//                //dto.Tags = model.Tags;
//            }
//            return dto;
//        }

//        public static Post ToModel(this PostCreateDto model)
//        {
//            var dto = new Post();
//            if(model != null)
//            {
//                dto.Title = model.Title;
//                dto.Text = model.Text;
//                dto.Description = model.Description;
//                dto.UserId = model.UserId;
//                //dto.Image = model.Image;
//                //dto.Tags = model.Tags;
//            }
//            return dto;
//        }

//    }
//}