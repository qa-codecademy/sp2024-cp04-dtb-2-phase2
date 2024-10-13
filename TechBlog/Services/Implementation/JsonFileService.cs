using Domain_Models;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

public static class JsonFileService
{
    public static async Task WriteUsersToFileAsync(string jsonFilePath)
    {
        var jsonData = await File.ReadAllTextAsync(jsonFilePath);

        var users = JsonSerializer.Deserialize<List<User>>(jsonData);

        if (users == null) return;

        var usersCSharpSyntax = "var users = new List<User>\n{\n" +
                                 string.Join(",\n", users.Select(user =>
                                 $"    new User {{ Id = {user.Id}, FirstName = \"{user.FirstName}\", LastName = \"{user.LastName}\", Email = \"{user.Email}\", Password = \"{user.Password}\", IsSubscribed = {user.IsSubscribed} }}")) +
                                 "\n};";

        var directory = Path.GetDirectoryName(jsonFilePath);
        var outputFilePath = Path.Combine(directory, "UsersList.cs");

        await File.WriteAllTextAsync(outputFilePath, usersCSharpSyntax);
    }


        
    public static async Task AddUserIdToPostsAndSave(string outputFilePath)
    {
        var random = new Random();
        var posts = new List<Post>
{
    new Post { Id = 1, Title = "Best Tools for Web Development", Text = "Donec dapibus. Duis at velit eu est congue elementum. In hac habitasse platea dictumst.", Description = "How AI is Revolutionizing Healthcare", UserId = 8, Tags = "innovation,internet", PostingTime = new DateTime(2024, 9, 15) },
    new Post { Id = 2, Title = "The Rise of Blockchain TechnologySEO Strategies for Small Businesses", Text = "Vivamus vel nulla eget eros elementum pellentesque.", Description = "5 Tips for Effective Remote Work", UserId = 33, Tags = "gadgets,internet", PostingTime = new DateTime(2024, 9, 7) },
    new Post { Id = 3, Title = "Machine Learning Demystified", Text = "aliquet at", Description = "How AI is Revolutionizing Healthcare", UserId = 22, Tags = "innovation,software", PostingTime = new DateTime(2024, 4, 10) },
    new Post { Id = 4, Title = "Cloud Computing Simplified", Text = "hendrerit at", Description = "Blockchain Technology Explained", UserId = 31, Tags = "technology,coding", PostingTime = new DateTime(2020, 5, 3) },
    new Post { Id = 5, Title = "Tech Gadgets Every Geek Should Have", Text = "Duis ac nibh. Fusce lacus purus", Description = "5 Tips for Effective Remote Work", UserId = 83, Tags = "AI,gadgets", PostingTime = new DateTime(2022, 4, 29) },
    new Post { Id = 6, Title = "The Rise of Blockchain TechnologySEO Strategies for Small Businesses", Text = "nisl. Aenean lectus.", Description = "How AI is Revolutionizing Healthcare", UserId = 39, Tags = "innovation,AI", PostingTime = new DateTime(2024, 2, 24) },
    new Post { Id = 7, Title = "The Impact of Big Data on Businesses", Text = "Vivamus metus arcu", Description = "Top 10 Tech Trends for 2021", UserId = 18, Tags = "cybersecurity,digital", PostingTime = new DateTime(2024, 3, 30) },
    new Post { Id = 8, Title = "Tech Gadgets Every Geek Should Have", Text = "pede.", Description = "How AI is Revolutionizing Healthcare", UserId = 48, Tags = "coding,innovation", PostingTime = new DateTime(2023, 10, 6) },
    new Post { Id = 9, Title = "Best Tools for Web Development", Text = "feugiat non", Description = "The Future of Cybersecurity", UserId = 41, Tags = "internet,innovation", PostingTime = new DateTime(2020, 1, 2) },
    new Post { Id = 10, Title = "How to Improve Your Coding Skills", Text = "lectus.", Description = "The Future of Cybersecurity", UserId = 9, Tags = "innovation,coding", PostingTime = new DateTime(2021, 1, 20) },
    new Post { Id = 11, Title = "The Future of Virtual Reality", Text = "Curabitur gravida nisi at nibh. In hac habitasse platea dictumst.", Description = "Blockchain Technology Explained", UserId = 64, Tags = "technology,data", PostingTime = new DateTime(2022, 7, 9) },
    new Post { Id = 12, Title = "Top 10 Tech Trends for 2021", Text = "hendrerit at", Description = "How AI is Revolutionizing Healthcare", UserId = 84, Tags = "data,coding", PostingTime = new DateTime(2022, 8, 30) },
    new Post { Id = 13, Title = "The Future of Virtual Reality", Text = "Vivamus metus arcu", Description = "Blockchain Technology Explained", UserId = 88, Tags = "technology,gadgets", PostingTime = new DateTime(2023, 2, 2) },
    new Post { Id = 14, Title = "The Impact of Big Data on Businesses", Text = "nisl. Aenean lectus.", Description = "Blockchain Technology Explained", UserId = 64, Tags = "internet,innovation", PostingTime = new DateTime(2022, 3, 14) },
    new Post { Id = 15, Title = "Tech Gadgets Every Geek Should Have", Text = "Maecenas pulvinar lobortis est. Phasellus sit amet erat. Nulla tempus.", Description = "Top 10 Tech Trends for 2021", UserId = 8, Tags = "cybersecurity,internet", PostingTime = new DateTime(2022, 11, 10) },
    new Post { Id = 16, Title = "The Rise of Blockchain TechnologySEO Strategies for Small Businesses", Text = "consectetuer adipiscing elit.", Description = "How AI is Revolutionizing Healthcare", UserId = 3, Tags = "gadgets,digital", PostingTime = new DateTime(2022, 6, 5) },
    new Post { Id = 17, Title = "Top 10 Tech Trends of 2021", Text = "tempus vel", Description = "How AI is Revolutionizing Healthcare", UserId = 94, Tags = "software,data", PostingTime = new DateTime(2020, 9, 11) },
    new Post { Id = 18, Title = "Top 10 Tech Trends for 2021", Text = "nisl.", Description = "5 Tips for Effective Remote Work", UserId = 71, Tags = "internet,technology", PostingTime = new DateTime(2020, 6, 14) },
    new Post { Id = 19, Title = "Cybersecurity Tips for Beginners", Text = "Vivamus vel nulla eget eros elementum pellentesque.", Description = "How AI is Revolutionizing Healthcare", UserId = 7, Tags = "gadgets,innovation", PostingTime = new DateTime(2022, 7, 27) },
    new Post { Id = 20, Title = "Best Tools for Web Development", Text = "turpis nec euismod scelerisque", Description = "5 Tips for Effective Remote Work", UserId = 18, Tags = "gadgets,cybersecurity", PostingTime = new DateTime(2022, 3, 19) },
    new Post { Id = 21, Title = "E-commerce Trends to Watch Out For", Text = "adipiscing molestie", Description = "Top 10 Tech Trends for 2021", UserId = 72, Tags = "innovation,internet", PostingTime = new DateTime(2024, 5, 9) },
    new Post { Id = 22, Title = "How AI is Revolutionizing the Tech Industry", Text = "vulputate vitae", Description = "How AI is Revolutionizing Healthcare", UserId = 64, Tags = "gadgets,software", PostingTime = new DateTime(2022, 1, 21) },
    new Post { Id = 23, Title = "How AI is Revolutionizing the Tech Industry", Text = "Donec dapibus. Duis at velit eu est congue elementum. In hac habitasse platea dictumst.", Description = "How AI is Revolutionizing Healthcare", UserId = 93, Tags = "AI,software", PostingTime = new DateTime(2023, 6, 15) },
    new Post { Id = 24, Title = "Cybersecurity Tips for Beginners", Text = "lectus.", Description = "Blockchain Technology Explained", UserId = 5, Tags = "cybersecurity,data", PostingTime = new DateTime(2022, 1, 2) },
    new Post { Id = 25, Title = "How to Improve Your Coding Skills", Text = "Donec dapibus. Duis at velit eu est congue elementum. In hac habitasse platea dictumst.", Description = "The Future of Cybersecurity", UserId = 3, Tags = "software,cybersecurity", PostingTime = new DateTime(2022, 7, 24) },
    new Post { Id = 26, Title = "The Future of Virtual Reality", Text = "hendrerit at", Description = "How AI is Revolutionizing Healthcare", UserId = 33, Tags = "coding,technology", PostingTime = new DateTime(2024, 1, 10) },
    new Post { Id = 27, Title = "The Future of Virtual Reality", Text = "Duis ac nibh. Fusce lacus purus", Description = "5 Tips for Effective Remote Work", UserId = 53, Tags = "software,coding", PostingTime = new DateTime(2023, 2, 20) },
    new Post { Id = 28, Title = "Top 10 Tech Trends of 2021", Text = "Duis ac nibh. Fusce lacus purus", Description = "The Future of Cybersecurity", UserId = 36, Tags = "internet,AI", PostingTime = new DateTime(2024, 7, 14) },
    new Post { Id = 29, Title = "Innovations in Mobile App Development", Text = "Nulla ut erat id mauris vulputate elementum. Nullam varius. Nulla facilisi. Cras non velit nec nisi vulputate nonummy.", Description = "The Future of Cybersecurity", UserId = 66, Tags = "digital,innovation", PostingTime = new DateTime(2021, 8, 18) },
    new Post { Id = 30, Title = "Innovations in Mobile App Development", Text = "hendrerit at", Description = "The Future of Cybersecurity", UserId = 48, Tags = "cybersecurity,technology", PostingTime = new DateTime(2024, 3, 10) },
    new Post { Id = 31, Title = "Social Media Marketing Techniques", Text = "nisl. Aenean lectus.", Description = "Top 10 Tech Trends for 2021", UserId = 7, Tags = "internet,technology", PostingTime = new DateTime(2020, 2, 19) },
    new Post { Id = 32, Title = "Digital Marketing Strategies for Success", Text = "hendrerit at", Description = "Blockchain Technology Explained", UserId = 49, Tags = "cybersecurity,software", PostingTime = new DateTime(2024, 2, 1) },
    new Post { Id = 33, Title = "Machine Learning Demystified", Text = "Vivamus metus arcu", Description = "Blockchain Technology Explained", UserId = 59, Tags = "AI,coding", PostingTime = new DateTime(2024, 3, 27) },
    new Post { Id = 34, Title = "Cybersecurity Tips for Beginners", Text = "nisl. Aenean lectus.", Description = "Blockchain Technology Explained", UserId = 8, Tags = "gadgets,internet", PostingTime = new DateTime(2020, 5, 5) },
    new Post { Id = 35, Title = "The Future of Virtual Reality", Text = "hendrerit at", Description = "How AI is Revolutionizing Healthcare", UserId = 40, Tags = "gadgets,coding", PostingTime = new DateTime(2022, 6, 24) },
    new Post { Id = 36, Title = "Data Privacy in the Digital Age", Text = "nisl. Aenean lectus.", Description = "Top 10 Tech Trends for 2021", UserId = 78, Tags = "digital,data", PostingTime = new DateTime(2024, 7, 1) },
    new Post { Id = 37, Title = "Machine Learning Demystified", Text = "vulputate vitae", Description = "The Future of Cybersecurity", UserId = 12, Tags = "cybersecurity,internet", PostingTime = new DateTime(2020, 5, 2) },
    new Post { Id = 38, Title = "Tech Gadgets Every Geek Should Have", Text = "Cum sociis natoque penatibus et magnis dis parturient montes", Description = "How AI is Revolutionizing Healthcare", UserId = 95, Tags = "digital,innovation", PostingTime = new DateTime(2024, 7, 11) },
    new Post { Id = 39, Title = "Social Media Marketing Techniques", Text = "tincidunt eget", Description = "Blockchain Technology Explained", UserId = 49, Tags = "cybersecurity,data", PostingTime = new DateTime(2021, 8, 9) },
    new Post { Id = 40, Title = "Best Tools for Web Development", Text = "Integer pede justo", Description = "5 Tips for Effective Remote Work", UserId = 95, Tags = "internet,technology", PostingTime = new DateTime(2023, 9, 24) },
    new Post { Id = 41, Title = "SEO Strategies for Small Businesses", Text = "vulputate vitae", Description = "Top 10 Tech Trends for 2021", UserId = 63, Tags = "internet,innovation", PostingTime = new DateTime(2020, 6, 3) },
    new Post { Id = 42, Title = "Best Tools for Web Development", Text = "nisl.", Description = "Top 10 Tech Trends for 2021", UserId = 75, Tags = "innovation,AI", PostingTime = new DateTime(2024, 2, 22) },
    new Post { Id = 43, Title = "The Impact of Big Data on Businesses", Text = "Morbi quis tortor id nulla ultrices aliquet.", Description = "The Future of Cybersecurity", UserId = 29, Tags = "innovation,internet", PostingTime = new DateTime(2021, 12, 3) },
    new Post { Id = 44, Title = "Social Media Marketing Techniques", Text = "vulputate vitae", Description = "5 Tips for Effective Remote Work", UserId = 36, Tags = "digital,software", PostingTime = new DateTime(2024, 2, 6) },
    new Post { Id = 45, Title = "The Power of Influencer Marketing", Text = "hendrerit at", Description = "The Future of Cybersecurity", UserId = 3, Tags = "coding,cybersecurity", PostingTime = new DateTime(2023, 6, 15) },
    new Post { Id = 46, Title = "Tech Gadgets Every Geek Should Have", Text = "vulputate vitae", Description = "The Future of Cybersecurity", UserId = 41, Tags = "cybersecurity,innovation", PostingTime = new DateTime(2022, 5, 1) },
    new Post { Id = 47, Title = "The Power of Influencer Marketing", Text = "Morbi quis tortor id nulla ultrices aliquet.", Description = "Top 10 Tech Trends for 2021", UserId = 77, Tags = "innovation,coding", PostingTime = new DateTime(2023, 3, 19) },
    new Post { Id = 48, Title = "E-commerce Trends to Watch Out For", Text = "vulputate vitae", Description = "Blockchain Technology Explained", UserId = 73, Tags = "cybersecurity,software", PostingTime = new DateTime(2022, 12, 20) },
    new Post { Id = 49, Title = "The Future of Virtual Reality", Text = "nisl. Aenean lectus.", Description = "Blockchain Technology Explained", UserId = 24, Tags = "digital,AI", PostingTime = new DateTime(2021, 6, 14) },
    new Post { Id = 50, Title = "E-commerce Trends to Watch Out For", Text = "tincidunt eget", Description = "5 Tips for Effective Remote Work", UserId = 55, Tags = "data,software", PostingTime = new DateTime(2023, 5, 29) },
    new Post { Id = 51, Title = "The Power of Influencer Marketing", Text = "lacinia eget", Description = "Blockchain Technology Explained", UserId = 64, Tags = "internet,technology", PostingTime = new DateTime(2023, 9, 23) },
    new Post { Id = 52, Title = "Blockchain Technology Explained", Text = "nisl.", Description = "How AI is Revolutionizing Healthcare", UserId = 14, Tags = "digital,internet", PostingTime = new DateTime(2020, 4, 19) },
    new Post { Id = 53, Title = "How to Improve Your Coding Skills", Text = "Morbi quis tortor id nulla ultrices aliquet.", Description = "Top 10 Tech Trends for 2021", UserId = 1, Tags = "internet,software", PostingTime = new DateTime(2020, 5, 12) },
    new Post { Id = 54, Title = "How to Improve Your Coding Skills", Text = "lectus.", Description = "5 Tips for Effective Remote Work", UserId = 20, Tags = "cybersecurity,internet", PostingTime = new DateTime(2021, 8, 23) },
    new Post { Id = 55, Title = "Tech Gadgets Every Geek Should Have", Text = "tempus vel", Description = "Blockchain Technology Explained", UserId = 19, Tags = "internet,software", PostingTime = new DateTime(2024, 7, 5) },
    new Post { Id = 56, Title = "Cybersecurity Tips for Beginners", Text = "lacinia eget", Description = "Top 10 Tech Trends for 2021", UserId = 47, Tags = "technology,data", PostingTime = new DateTime(2021, 10, 7) },
    new Post { Id = 57, Title = "The Power of Influencer Marketing", Text = "vulputate vitae", Description = "5 Tips for Effective Remote Work", UserId = 21, Tags = "internet,data", PostingTime = new DateTime(2022, 3, 22) },
    new Post { Id = 58, Title = "How AI is Revolutionizing the Tech Industry", Text = "vulputate vitae", Description = "The Future of Cybersecurity", UserId = 5, Tags = "data,software", PostingTime = new DateTime(2024, 3, 28) },
    new Post { Id = 59, Title = "The Power of Influencer Marketing", Text = "pretium quis", Description = "The Future of Cybersecurity", UserId = 64, Tags = "gadgets,innovation", PostingTime = new DateTime(2024, 4, 25) },
    new Post { Id = 60, Title = "The Impact of Big Data on Businesses", Text = "nascetur ridiculus mus.", Description = "The Future of Cybersecurity", UserId = 54, Tags = "internet,AI", PostingTime = new DateTime(2020, 11, 21) },
    new Post { Id = 61, Title = "Social Media Marketing Techniques", Text = "nascetur ridiculus mus.", Description = "How AI is Revolutionizing Healthcare", UserId = 43, Tags = "internet,technology", PostingTime = new DateTime(2023, 5, 26) },
    new Post { Id = 62, Title = "Top 10 Tech Trends for 2021", Text = "lectus.", Description = "How AI is Revolutionizing Healthcare", UserId = 60, Tags = "coding,digital", PostingTime = new DateTime(2020, 8, 8) },
    new Post { Id = 63, Title = "Cybersecurity in the Digital Age", Text = "aliquet at", Description = "The Future of Cybersecurity", UserId = 58, Tags = "internet,technology", PostingTime = new DateTime(2022, 7, 22) },
    new Post { Id = 64, Title = "How to Improve Your Coding Skills", Text = "adipiscing molestie", Description = "Blockchain Technology Explained", UserId = 7, Tags = "AI,digital", PostingTime = new DateTime(2023, 6, 23) },
    new Post { Id = 65, Title = "Tech Gadgets Every Geek Should Have", Text = "nisl. Aenean lectus.", Description = "Top 10 Tech Trends for 2021", UserId = 99, Tags = "gadgets,internet", PostingTime = new DateTime(2022, 4, 19) },
    new Post { Id = 66, Title = "The Rise of Blockchain TechnologySEO Strategies for Small Businesses", Text = "nisl.", Description = "Blockchain Technology Explained", UserId = 70, Tags = "coding,innovation", PostingTime = new DateTime(2021, 12, 9) },
    new Post { Id = 67, Title = "Digital Marketing Strategies for Success", Text = "turpis nec euismod scelerisque", Description = "The Future of Cybersecurity", UserId = 26, Tags = "data,internet", PostingTime = new DateTime(2021, 7, 17) },
    new Post { Id = 68, Title = "Data Privacy in the Digital Age", Text = "pede.", Description = "The Future of Cybersecurity", UserId = 12, Tags = "AI,internet", PostingTime = new DateTime(2022, 1, 25) },
    new Post { Id = 69, Title = "How to Improve Your Coding Skills", Text = "Maecenas pulvinar lobortis est. Phasellus sit amet erat. Nulla tempus.", Description = "The Future of Cybersecurity", UserId = 94, Tags = "AI,coding", PostingTime = new DateTime(2021, 5, 30) },
    new Post { Id = 70, Title = "How to Improve Your Coding Skills", Text = "Phasellus id sapien in sapien iaculis congue. Vivamus metus arcu", Description = "The Future of Cybersecurity", UserId = 81, Tags = "coding,technology", PostingTime = new DateTime(2020, 4, 19) },
    new Post { Id = 71, Title = "Tech Gadgets Every Geek Should Have", Text = "Vivamus metus arcu", Description = "The Future of Cybersecurity", UserId = 78, Tags = "data,technology", PostingTime = new DateTime(2022, 7, 16) },
    new Post { Id = 72, Title = "SEO Strategies for Small Businesses", Text = "nisl.", Description = "The Future of Cybersecurity", UserId = 3, Tags = "AI,technology", PostingTime = new DateTime(2022, 2, 26) },
    new Post { Id = 73, Title = "The Impact of Big Data on Businesses", Text = "Vivamus metus arcu", Description = "The Future of Cybersecurity", UserId = 10, Tags = "technology,software", PostingTime = new DateTime(2020, 3, 8) },
    new Post { Id = 74, Title = "SEO Strategies for Small Businesses", Text = "Curabitur gravida nisi at nibh. In hac habitasse platea dictumst.", Description = "Blockchain Technology Explained", UserId = 97, Tags = "technology,coding", PostingTime = new DateTime(2022, 12, 26) },
    new Post { Id = 75, Title = "Top 10 Tech Trends of 2021", Text = "Cum sociis natoque penatibus et magnis dis parturient montes", Description = "5 Tips for Effective Remote Work", UserId = 37, Tags = "cybersecurity,AI", PostingTime = new DateTime(2022, 1, 26) },
    new Post { Id = 76, Title = "Tech Gadgets Every Geek Should Have", Text = "hendrerit at", Description = "The Future of Cybersecurity", UserId = 12, Tags = "AI,innovation", PostingTime = new DateTime(2024, 1, 5) },
    new Post { Id = 77, Title = "SEO Strategies for Small Businesses", Text = "Duis ac nibh. Fusce lacus purus", Description = "Top 10 Tech Trends for 2021", UserId = 38, Tags = "cybersecurity,gadgets", PostingTime = new DateTime(2020, 12, 5) },
    new Post { Id = 78, Title = "The Impact of Big Data on Businesses", Text = "adipiscing molestie", Description = "How AI is Revolutionizing Healthcare", UserId = 96, Tags = "coding,technology", PostingTime = new DateTime(2023, 6, 8) },
    new Post { Id = 79, Title = "Creating Engaging Content for Your Blog", Text = "tempus vel", Description = "Top 10 Tech Trends for 2021", UserId = 65, Tags = "technology,AI", PostingTime = new DateTime(2024, 2, 16) },
    new Post { Id = 80, Title = "The Impact of Big Data on Businesses", Text = "Vivamus vel nulla eget eros elementum pellentesque.", Description = "Top 10 Tech Trends for 2021", UserId = 25, Tags = "software,technology", PostingTime = new DateTime(2022, 11, 28) },
    new Post { Id = 81, Title = "Cybersecurity in the Digital Age", Text = "turpis nec euismod scelerisque", Description = "5 Tips for Effective Remote Work", UserId = 33, Tags = "coding,AI", PostingTime = new DateTime(2021, 2, 9) },
    new Post { Id = 82, Title = "Tech Gadgets Every Geek Should Have", Text = "Duis ac nibh. Fusce lacus purus", Description = "The Future of Cybersecurity", UserId = 93, Tags = "digital,coding", PostingTime = new DateTime(2020, 10, 11) },
    new Post { Id = 83, Title = "Digital Marketing Strategies for Success", Text = "nisl.", Description = "5 Tips for Effective Remote Work", UserId = 95, Tags = "data,gadgets", PostingTime = new DateTime(2020, 12, 26) },
    new Post { Id = 84, Title = "Data Privacy in the Digital Age", Text = "consectetuer adipiscing elit.", Description = "How AI is Revolutionizing Healthcare", UserId = 13, Tags = "coding,technology", PostingTime = new DateTime(2023, 3, 3) },
    new Post { Id = 85, Title = "Cybersecurity Tips for Beginners", Text = "lectus.", Description = "Top 10 Tech Trends for 2021", UserId = 43, Tags = "internet,gadgets", PostingTime = new DateTime(2021, 3, 14) },
    new Post { Id = 86, Title = "The Power of Influencer Marketing", Text = "Vivamus metus arcu", Description = "Top 10 Tech Trends for 2021", UserId = 93, Tags = "coding,innovation", PostingTime = new DateTime(2022, 3, 12) },
    new Post { Id = 87, Title = "Top 10 Tech Trends of 2021", Text = "consectetuer adipiscing elit.", Description = "How AI is Revolutionizing Healthcare", UserId = 80, Tags = "cybersecurity,digital", PostingTime = new DateTime(2020, 7, 16) },
    new Post { Id = 88, Title = "How to Improve Your Coding Skills", Text = "Phasellus id sapien in sapien iaculis congue. Vivamus metus arcu", Description = "Top 10 Tech Trends for 2021", UserId = 90, Tags = "coding,gadgets", PostingTime = new DateTime(2020, 11, 10) },
    new Post { Id = 89, Title = "The Power of Influencer Marketing", Text = "Maecenas pulvinar lobortis est. Phasellus sit amet erat. Nulla tempus.", Description = "Blockchain Technology Explained", UserId = 100, Tags = "technology,cybersecurity", PostingTime = new DateTime(2023, 9, 17) },
    new Post { Id = 90, Title = "The Rise of Blockchain TechnologySEO Strategies for Small Businesses", Text = "turpis nec euismod scelerisque", Description = "How AI is Revolutionizing Healthcare", UserId = 11, Tags = "AI,technology", PostingTime = new DateTime(2023, 4, 24) },
    new Post { Id = 91, Title = "Tech Gadgets Every Geek Should Have", Text = "hendrerit at", Description = "The Future of Cybersecurity", UserId = 92, Tags = "innovation,gadgets", PostingTime = new DateTime(2022, 1, 8) },
    new Post { Id = 92, Title = "The Power of Influencer Marketing", Text = "tempus vel", Description = "Blockchain Technology Explained", UserId = 28, Tags = "software,gadgets", PostingTime = new DateTime(2021, 1, 11) },
    new Post { Id = 93, Title = "E-commerce Trends to Watch Out For", Text = "pede.", Description = "5 Tips for Effective Remote Work", UserId = 24, Tags = "coding,technology", PostingTime = new DateTime(2022, 6, 17) },
    new Post { Id = 94, Title = "Cloud Computing Simplified", Text = "lectus.", Description = "Blockchain Technology Explained", UserId = 6, Tags = "cybersecurity,digital", PostingTime = new DateTime(2023, 12, 20) },
    new Post { Id = 95, Title = "Data Privacy in the Digital Age", Text = "aliquet at", Description = "The Future of Cybersecurity", UserId = 43, Tags = "innovation,AI", PostingTime = new DateTime(2022, 1, 28) },
    new Post { Id = 96, Title = "The Power of Influencer Marketing", Text = "turpis nec euismod scelerisque", Description = "The Future of Cybersecurity", UserId = 15, Tags = "coding,technology", PostingTime = new DateTime(2021, 1, 3) },
    new Post { Id = 97, Title = "Machine Learning Demystified", Text = "Cum sociis natoque penatibus et magnis dis parturient montes", Description = "Top 10 Tech Trends for 2021", UserId = 24, Tags = "AI,cybersecurity", PostingTime = new DateTime(2020, 12, 13) },
    new Post { Id = 98, Title = "Social Media Marketing Techniques", Text = "lectus.", Description = "5 Tips for Effective Remote Work", UserId = 10, Tags = "technology,digital", PostingTime = new DateTime(2021, 1, 31) },
    new Post { Id = 99, Title = "The Power of Influencer Marketing", Text = "nisl.", Description = "The Future of Cybersecurity", UserId = 59, Tags = "cybersecurity,innovation", PostingTime = new DateTime(2022, 1, 25) },
    new Post { Id = 100, Title = "Top 10 Tech Trends for 2021", Text = "vitae mattis nibh ligula nec sem.", Description = "5 Tips for Effective Remote Work", UserId = 16, Tags = "innovation,technology", PostingTime = new DateTime(2023, 7, 8) },
    new Post { Id = 101, Title = "Cybersecurity in the Digital Age", Text = "consectetuer adipiscing elit.", Description = "The Future of Cybersecurity", UserId = 7, Tags = "internet,software", PostingTime = new DateTime(2023, 12, 11) },
    new Post { Id = 102, Title = "Best Tools for Web Development", Text = "adipiscing molestie", Description = "5 Tips for Effective Remote Work", UserId = 6, Tags = "innovation,software", PostingTime = new DateTime(2020, 7, 4) },
    new Post { Id = 103, Title = "How to Improve Your Coding Skills", Text = "feugiat non", Description = "Top 10 Tech Trends for 2021", UserId = 67, Tags = "digital,software", PostingTime = new DateTime(2020, 9, 20) },
    new Post { Id = 104, Title = "Social Media Marketing Techniques", Text = "vulputate vitae", Description = "Blockchain Technology Explained", UserId = 78, Tags = "technology,cybersecurity", PostingTime = new DateTime(2023, 3, 28) },
    new Post { Id = 105, Title = "Cybersecurity in the Digital Age", Text = "pede.", Description = "How AI is Revolutionizing Healthcare", UserId = 25, Tags = "software,data", PostingTime = new DateTime(2022, 7, 24) },
    new Post { Id = 106, Title = "The Rise of Blockchain TechnologySEO Strategies for Small Businesses", Text = "lectus.", Description = "Blockchain Technology Explained", UserId = 26, Tags = "innovation,software", PostingTime = new DateTime(2022, 12, 10) },
    new Post { Id = 107, Title = "How to Improve Your Coding Skills", Text = "tincidunt eget", Description = "The Future of Cybersecurity", UserId = 87, Tags = "technology,gadgets", PostingTime = new DateTime(2021, 3, 28) },
    new Post { Id = 108, Title = "How to Improve Your Coding Skills", Text = "Morbi quis tortor id nulla ultrices aliquet.", Description = "Top 10 Tech Trends for 2021", UserId = 33, Tags = "gadgets,AI", PostingTime = new DateTime(2024, 5, 5) },
    new Post { Id = 109, Title = "Data Privacy in the Digital Age", Text = "nisl.", Description = "How AI is Revolutionizing Healthcare", UserId = 60, Tags = "cybersecurity,digital", PostingTime = new DateTime(2023, 2, 14) },
    new Post { Id = 110, Title = "How to Improve Your Coding Skills", Text = "nisl.", Description = "5 Tips for Effective Remote Work", UserId = 83, Tags = "software,technology", PostingTime = new DateTime(2021, 7, 13) },
    new Post { Id = 111, Title = "Best Tools for Web Development", Text = "Curabitur gravida nisi at nibh. In hac habitasse platea dictumst.", Description = "The Future of Cybersecurity", UserId = 12, Tags = "coding,internet", PostingTime = new DateTime(2021, 9, 9) },
    new Post { Id = 112, Title = "The Impact of Big Data on Businesses", Text = "Curabitur gravida nisi at nibh. In hac habitasse platea dictumst.", Description = "How AI is Revolutionizing Healthcare", UserId = 76, Tags = "coding,data", PostingTime = new DateTime(2024, 5, 16) },
    new Post { Id = 113, Title = "Tech Gadgets Every Geek Should Have", Text = "Cum sociis natoque penatibus et magnis dis parturient montes", Description = "5 Tips for Effective Remote Work", UserId = 72, Tags = "AI,gadgets", PostingTime = new DateTime(2022, 9, 20) },
    new Post { Id = 114, Title = "Blockchain Technology Explained", Text = "Vivamus vel nulla eget eros elementum pellentesque.", Description = "How AI is Revolutionizing Healthcare", UserId = 4, Tags = "software,internet", PostingTime = new DateTime(2024, 2, 5) },
    new Post { Id = 115, Title = "Tech Gadgets Every Geek Should Have", Text = "consectetuer adipiscing elit.", Description = "Blockchain Technology Explained", UserId = 36, Tags = "data,software", PostingTime = new DateTime(2020, 10, 18) },
    new Post { Id = 116, Title = "How AI is Revolutionizing the Tech Industry", Text = "pede.", Description = "5 Tips for Effective Remote Work", UserId = 7, Tags = "AI,data", PostingTime = new DateTime(2022, 11, 23) },
    new Post { Id = 117, Title = "Tech Gadgets Every Geek Should Have", Text = "Morbi quis tortor id nulla ultrices aliquet.", Description = "5 Tips for Effective Remote Work", UserId = 58, Tags = "data,gadgets", PostingTime = new DateTime(2024, 2, 19) },
    new Post { Id = 118, Title = "Machine Learning Demystified", Text = "Duis ac nibh. Fusce lacus purus", Description = "The Future of Cybersecurity", UserId = 100, Tags = "digital,AI", PostingTime = new DateTime(2022, 12, 1) },
    new Post { Id = 119, Title = "Blockchain Technology Explained", Text = "In blandit ultrices enim. Lorem ipsum dolor sit amet", Description = "How AI is Revolutionizing Healthcare", UserId = 84, Tags = "internet,software", PostingTime = new DateTime(2022, 3, 16) },
    new Post { Id = 120, Title = "How to Improve Your Coding Skills", Text = "pretium quis", Description = "How AI is Revolutionizing Healthcare", UserId = 47, Tags = "gadgets,cybersecurity", PostingTime = new DateTime(2022, 11, 4) },
    new Post { Id = 121, Title = "The Future of Virtual Reality", Text = "nascetur ridiculus mus.", Description = "How AI is Revolutionizing Healthcare", UserId = 96, Tags = "coding,internet", PostingTime = new DateTime(2020, 10, 3) },
    new Post { Id = 122, Title = "E-commerce Trends to Watch Out For", Text = "adipiscing molestie", Description = "The Future of Cybersecurity", UserId = 44, Tags = "coding,cybersecurity", PostingTime = new DateTime(2024, 1, 20) },
    new Post { Id = 123, Title = "Data Privacy in the Digital Age", Text = "Phasellus id sapien in sapien iaculis congue. Vivamus metus arcu", Description = "5 Tips for Effective Remote Work", UserId = 95, Tags = "digital,AI", PostingTime = new DateTime(2023, 6, 27) },
    new Post { Id = 124, Title = "How AI is Revolutionizing the Tech Industry", Text = "lectus.", Description = "How AI is Revolutionizing Healthcare", UserId = 27, Tags = "digital,technology", PostingTime = new DateTime(2022, 10, 25) },
    new Post { Id = 125, Title = "The Rise of Blockchain TechnologySEO Strategies for Small Businesses", Text = "adipiscing molestie", Description = "Blockchain Technology Explained", UserId = 83, Tags = "innovation,digital", PostingTime = new DateTime(2021, 4, 12) },
    new Post { Id = 126, Title = "The Power of Influencer Marketing", Text = "tempus vel", Description = "How AI is Revolutionizing Healthcare", UserId = 51, Tags = "software,gadgets", PostingTime = new DateTime(2020, 10, 15) },
    new Post { Id = 127, Title = "Tech Gadgets Every Geek Should Have", Text = "Vivamus metus arcu", Description = "The Future of Cybersecurity", UserId = 40, Tags = "cybersecurity,coding", PostingTime = new DateTime(2021, 5, 14) },
    new Post { Id = 128, Title = "The Impact of Big Data on Businesses", Text = "nisl. Aenean lectus.", Description = "5 Tips for Effective Remote Work", UserId = 79, Tags = "AI,technology", PostingTime = new DateTime(2022, 5, 4) },
    new Post { Id = 129, Title = "How to Improve Your Coding Skills", Text = "hendrerit at", Description = "How AI is Revolutionizing Healthcare", UserId = 72, Tags = "digital,software", PostingTime = new DateTime(2023, 1, 22) },
    new Post { Id = 130, Title = "Digital Marketing Strategies for Success", Text = "Cum sociis natoque penatibus et magnis dis parturient montes", Description = "5 Tips for Effective Remote Work", UserId = 56, Tags = "innovation,cybersecurity", PostingTime = new DateTime(2022, 6, 4) },
    new Post { Id = 131, Title = "The Rise of Blockchain TechnologySEO Strategies for Small Businesses", Text = "consectetuer adipiscing elit.", Description = "Top 10 Tech Trends for 2021", UserId = 12, Tags = "AI,cybersecurity", PostingTime = new DateTime(2022, 12, 25) },
    new Post { Id = 132, Title = "Creating Engaging Content for Your Blog", Text = "Nulla ut erat id mauris vulputate elementum. Nullam varius. Nulla facilisi. Cras non velit nec nisi vulputate nonummy.", Description = "5 Tips for Effective Remote Work", UserId = 62, Tags = "technology,digital", PostingTime = new DateTime(2024, 5, 21) },
    new Post { Id = 133, Title = "How AI is Revolutionizing the Tech Industry", Text = "consectetuer adipiscing elit.", Description = "The Future of Cybersecurity", UserId = 98, Tags = "coding,digital", PostingTime = new DateTime(2022, 10, 29) },
    new Post { Id = 134, Title = "SEO Strategies for Small Businesses", Text = "Vivamus metus arcu", Description = "The Future of Cybersecurity", UserId = 18, Tags = "technology,internet", PostingTime = new DateTime(2020, 2, 10) },
    new Post { Id = 135, Title = "Best Tools for Web Development", Text = "vulputate vitae", Description = "Blockchain Technology Explained", UserId = 39, Tags = "AI,cybersecurity", PostingTime = new DateTime(2023, 4, 2) },
    new Post { Id = 136, Title = "SEO Strategies for Small Businesses", Text = "lectus.", Description = "Top 10 Tech Trends for 2021", UserId = 87, Tags = "internet,technology", PostingTime = new DateTime(2020, 9, 29) },
    new Post { Id = 137, Title = "The Impact of Big Data on Businesses", Text = "Cum sociis natoque penatibus et magnis dis parturient montes", Description = "Blockchain Technology Explained", UserId = 96, Tags = "AI,cybersecurity", PostingTime = new DateTime(2020, 3, 13) },
    new Post { Id = 138, Title = "Cybersecurity Tips for Beginners", Text = "aliquet at", Description = "Blockchain Technology Explained", UserId = 9, Tags = "innovation,technology", PostingTime = new DateTime(2021, 4, 20) },
    new Post { Id = 139, Title = "Blockchain Technology Explained", Text = "adipiscing molestie", Description = "The Future of Cybersecurity", UserId = 18, Tags = "software,coding", PostingTime = new DateTime(2023, 6, 3) },
    new Post { Id = 140, Title = "Top 10 Tech Trends of 2021", Text = "nascetur ridiculus mus.", Description = "The Future of Cybersecurity", UserId = 17, Tags = "internet,innovation", PostingTime = new DateTime(2021, 5, 13) },
    new Post { Id = 141, Title = "E-commerce Trends to Watch Out For", Text = "adipiscing molestie", Description = "5 Tips for Effective Remote Work", UserId = 31, Tags = "cybersecurity,digital", PostingTime = new DateTime(2020, 2, 12) },
    new Post { Id = 142, Title = "Creating Engaging Content for Your Blog", Text = "Integer pede justo", Description = "Top 10 Tech Trends for 2021", UserId = 77, Tags = "digital,technology", PostingTime = new DateTime(2022, 8, 15) },
    new Post { Id = 143, Title = "Cloud Computing Simplified", Text = "Maecenas pulvinar lobortis est. Phasellus sit amet erat. Nulla tempus.", Description = "How AI is Revolutionizing Healthcare", UserId = 13, Tags = "AI,data", PostingTime = new DateTime(2021, 9, 11) },
    new Post { Id = 144, Title = "Machine Learning Demystified", Text = "Cum sociis natoque penatibus et magnis dis parturient montes", Description = "How AI is Revolutionizing Healthcare", UserId = 28, Tags = "internet,AI", PostingTime = new DateTime(2020, 7, 12) },
    new Post { Id = 145, Title = "Digital Marketing Strategies for Success", Text = "feugiat non", Description = "The Future of Cybersecurity", UserId = 79, Tags = "AI,cybersecurity", PostingTime = new DateTime(2021, 10, 4) },
    new Post { Id = 146, Title = "How to Improve Your Coding Skills", Text = "In blandit ultrices enim. Lorem ipsum dolor sit amet", Description = "Top 10 Tech Trends for 2021", UserId = 73, Tags = "technology,innovation", PostingTime = new DateTime(2020, 2, 8) },
    new Post { Id = 147, Title = "Cybersecurity in the Digital Age", Text = "feugiat non", Description = "Top 10 Tech Trends for 2021", UserId = 10, Tags = "technology,data", PostingTime = new DateTime(2022, 11, 13) },
    new Post { Id = 148, Title = "Top 10 Tech Trends of 2021", Text = "pretium quis", Description = "How AI is Revolutionizing Healthcare", UserId = 96, Tags = "innovation,digital", PostingTime = new DateTime(2023, 6, 22) },
    new Post { Id = 149, Title = "Data Privacy in the Digital Age", Text = "turpis nec euismod scelerisque", Description = "The Future of Cybersecurity", UserId = 42, Tags = "data,software", PostingTime = new DateTime(2023, 7, 22) },
    new Post { Id = 150, Title = "Creating Engaging Content for Your Blog", Text = "pede.", Description = "Blockchain Technology Explained", UserId = 85, Tags = "innovation,data", PostingTime = new DateTime(2022, 11, 14) },
    new Post { Id = 151, Title = "Cloud Computing Simplified", Text = "pretium quis", Description = "Top 10 Tech Trends for 2021", UserId = 30, Tags = "coding,internet", PostingTime = new DateTime(2021, 2, 23) },
    new Post { Id = 152, Title = "How AI is Revolutionizing the Tech Industry", Text = "vulputate vitae", Description = "5 Tips for Effective Remote Work", UserId = 40, Tags = "technology,coding", PostingTime = new DateTime(2022, 3, 6) },
    new Post { Id = 153, Title = "Cybersecurity Tips for Beginners", Text = "adipiscing molestie", Description = "5 Tips for Effective Remote Work", UserId = 15, Tags = "data,internet", PostingTime = new DateTime(2022, 8, 18) },
    new Post { Id = 154, Title = "Best Tools for Web Development", Text = "hendrerit at", Description = "Blockchain Technology Explained", UserId = 24, Tags = "data,cybersecurity", PostingTime = new DateTime(2024, 3, 5) },
    new Post { Id = 155, Title = "Cloud Computing Simplified", Text = "Cum sociis natoque penatibus et magnis dis parturient montes", Description = "Top 10 Tech Trends for 2021", UserId = 70, Tags = "AI,digital", PostingTime = new DateTime(2021, 3, 4) },
    new Post { Id = 156, Title = "How AI is Revolutionizing the Tech Industry", Text = "pede.", Description = "The Future of Cybersecurity", UserId = 7, Tags = "AI,internet", PostingTime = new DateTime(2021, 1, 21) },
    new Post { Id = 157, Title = "The Future of Virtual Reality", Text = "In blandit ultrices enim. Lorem ipsum dolor sit amet", Description = "Top 10 Tech Trends for 2021", UserId = 72, Tags = "software,technology", PostingTime = new DateTime(2021, 1, 7) },
    new Post { Id = 158, Title = "Cloud Computing Simplified", Text = "Integer pede justo", Description = "Blockchain Technology Explained", UserId = 47, Tags = "AI,cybersecurity", PostingTime = new DateTime(2023, 12, 22) },
    new Post { Id = 159, Title = "How AI is Revolutionizing the Tech Industry", Text = "Integer pede justo", Description = "The Future of Cybersecurity", UserId = 64, Tags = "innovation,coding", PostingTime = new DateTime(2023, 1, 5) },
    new Post { Id = 160, Title = "The Rise of Blockchain TechnologySEO Strategies for Small Businesses", Text = "lectus.", Description = "5 Tips for Effective Remote Work", UserId = 93, Tags = "cybersecurity,digital", PostingTime = new DateTime(2024, 7, 12) },
    new Post { Id = 161, Title = "Innovations in Mobile App Development", Text = "adipiscing molestie", Description = "5 Tips for Effective Remote Work", UserId = 81, Tags = "software,coding", PostingTime = new DateTime(2024, 7, 30) },
    new Post { Id = 162, Title = "The Power of Influencer Marketing", Text = "Vivamus metus arcu", Description = "The Future of Cybersecurity", UserId = 13, Tags = "data,AI", PostingTime = new DateTime(2024, 9, 12) },
    new Post { Id = 163, Title = "Data Privacy in the Digital Age", Text = "feugiat non", Description = "5 Tips for Effective Remote Work", UserId = 84, Tags = "gadgets,digital", PostingTime = new DateTime(2020, 10, 10) },
    new Post { Id = 164, Title = "The Rise of Blockchain TechnologySEO Strategies for Small Businesses", Text = "vulputate vitae", Description = "How AI is Revolutionizing Healthcare", UserId = 87, Tags = "coding,data", PostingTime = new DateTime(2022, 5, 26) },
    new Post { Id = 165, Title = "Cybersecurity Tips for Beginners", Text = "nisl.", Description = "Blockchain Technology Explained", UserId = 46, Tags = "cybersecurity,coding", PostingTime = new DateTime(2022, 6, 2) },
    new Post { Id = 166, Title = "Tech Gadgets Every Geek Should Have", Text = "In blandit ultrices enim. Lorem ipsum dolor sit amet", Description = "The Future of Cybersecurity", UserId = 81, Tags = "digital,gadgets", PostingTime = new DateTime(2021, 10, 30) },
    new Post { Id = 167, Title = "Cybersecurity in the Digital Age", Text = "aliquet at", Description = "How AI is Revolutionizing Healthcare", UserId = 59, Tags = "AI,technology", PostingTime = new DateTime(2022, 11, 14) },
    new Post { Id = 168, Title = "Best Tools for Web Development", Text = "nisl.", Description = "How AI is Revolutionizing Healthcare", UserId = 95, Tags = "technology,innovation", PostingTime = new DateTime(2023, 4, 19) },
    new Post { Id = 169, Title = "Blockchain Technology Explained", Text = "pretium quis", Description = "The Future of Cybersecurity", UserId = 75, Tags = "cybersecurity,gadgets", PostingTime = new DateTime(2020, 9, 20) },
    new Post { Id = 170, Title = "How to Improve Your Coding Skills", Text = "hendrerit at", Description = "5 Tips for Effective Remote Work", UserId = 10, Tags = "software,data", PostingTime = new DateTime(2023, 2, 24) },
    new Post { Id = 171, Title = "Digital Marketing Strategies for Success", Text = "pretium quis", Description = "5 Tips for Effective Remote Work", UserId = 62, Tags = "AI,technology", PostingTime = new DateTime(2021, 12, 10) },
    new Post { Id = 172, Title = "The Impact of Big Data on Businesses", Text = "pede.", Description = "The Future of Cybersecurity", UserId = 15, Tags = "data,gadgets", PostingTime = new DateTime(2024, 5, 23) },
    new Post { Id = 173, Title = "Cloud Computing Simplified", Text = "In blandit ultrices enim. Lorem ipsum dolor sit amet", Description = "5 Tips for Effective Remote Work", UserId = 23, Tags = "technology,internet", PostingTime = new DateTime(2021, 2, 27) },
    new Post { Id = 174, Title = "Cybersecurity in the Digital Age", Text = "feugiat non", Description = "The Future of Cybersecurity", UserId = 1, Tags = "data,AI", PostingTime = new DateTime(2024, 3, 16) },
    new Post { Id = 175, Title = "Cybersecurity in the Digital Age", Text = "nascetur ridiculus mus.", Description = "Blockchain Technology Explained", UserId = 50, Tags = "data,AI", PostingTime = new DateTime(2020, 8, 1) },
    new Post { Id = 176, Title = "The Future of Virtual Reality", Text = "Donec dapibus. Duis at velit eu est congue elementum. In hac habitasse platea dictumst.", Description = "The Future of Cybersecurity", UserId = 35, Tags = "software,data", PostingTime = new DateTime(2023, 3, 7) },
    new Post { Id = 177, Title = "The Impact of Big Data on Businesses", Text = "adipiscing molestie", Description = "5 Tips for Effective Remote Work", UserId = 19, Tags = "AI,coding", PostingTime = new DateTime(2024, 4, 25) },
    new Post { Id = 178, Title = "The Future of Virtual Reality", Text = "lectus.", Description = "5 Tips for Effective Remote Work", UserId = 29, Tags = "digital,software", PostingTime = new DateTime(2023, 10, 28) },
    new Post { Id = 179, Title = "How to Improve Your Coding Skills", Text = "Vivamus metus arcu", Description = "Blockchain Technology Explained", UserId = 22, Tags = "coding,data", PostingTime = new DateTime(2022, 3, 8) },
    new Post { Id = 180, Title = "Blockchain Technology Explained", Text = "adipiscing molestie", Description = "Blockchain Technology Explained", UserId = 65, Tags = "cybersecurity,software", PostingTime = new DateTime(2024, 5, 3) },
    new Post { Id = 181, Title = "The Future of Virtual Reality", Text = "adipiscing molestie", Description = "The Future of Cybersecurity", UserId = 11, Tags = "AI,data", PostingTime = new DateTime(2022, 11, 22) },
    new Post { Id = 182, Title = "Top 10 Tech Trends of 2021", Text = "nascetur ridiculus mus.", Description = "Blockchain Technology Explained", UserId = 52, Tags = "digital,internet", PostingTime = new DateTime(2022, 2, 12) },
    new Post { Id = 183, Title = "How to Improve Your Coding Skills", Text = "vitae mattis nibh ligula nec sem.", Description = "5 Tips for Effective Remote Work", UserId = 20, Tags = "digital,gadgets", PostingTime = new DateTime(2022, 1, 10) },
    new Post { Id = 184, Title = "E-commerce Trends to Watch Out For", Text = "tincidunt eget", Description = "The Future of Cybersecurity", UserId = 58, Tags = "AI,digital", PostingTime = new DateTime(2023, 9, 8) },
    new Post { Id = 185, Title = "Cybersecurity Tips for Beginners", Text = "tincidunt eget", Description = "How AI is Revolutionizing Healthcare", UserId = 51, Tags = "gadgets,software", PostingTime = new DateTime(2021, 6, 9) },
    new Post { Id = 186, Title = "Top 10 Tech Trends for 2021", Text = "nisl.", Description = "5 Tips for Effective Remote Work", UserId = 38, Tags = "software,innovation", PostingTime = new DateTime(2024, 3, 14) },
    new Post { Id = 187, Title = "Cybersecurity in the Digital Age", Text = "quam turpis adipiscing lorem", Description = "Top 10 Tech Trends for 2021", UserId = 91, Tags = "innovation,internet", PostingTime = new DateTime(2020, 11, 11) },
    new Post { Id = 188, Title = "Cloud Computing Simplified", Text = "hendrerit at", Description = "Blockchain Technology Explained", UserId = 17, Tags = "AI,data", PostingTime = new DateTime(2023, 7, 14) },
    new Post { Id = 189, Title = "The Rise of Blockchain TechnologySEO Strategies for Small Businesses", Text = "nisl. Aenean lectus.", Description = "Top 10 Tech Trends for 2021", UserId = 62, Tags = "digital,technology", PostingTime = new DateTime(2022, 11, 25) },
    new Post { Id = 190, Title = "E-commerce Trends to Watch Out For", Text = "Vivamus metus arcu", Description = "Blockchain Technology Explained", UserId = 73, Tags = "technology,cybersecurity", PostingTime = new DateTime(2023, 7, 15) },
    new Post { Id = 191, Title = "Cloud Computing Simplified", Text = "consectetuer adipiscing elit.", Description = "5 Tips for Effective Remote Work", UserId = 4, Tags = "software,technology", PostingTime = new DateTime(2021, 7, 26) },
    new Post { Id = 192, Title = "The Future of Virtual Reality", Text = "lectus.", Description = "Blockchain Technology Explained", UserId = 50, Tags = "AI,innovation", PostingTime = new DateTime(2023, 8, 7) },
    new Post { Id = 193, Title = "Data Privacy in the Digital Age", Text = "hendrerit at", Description = "Blockchain Technology Explained", UserId = 16, Tags = "innovation,data", PostingTime = new DateTime(2021, 10, 11) },
    new Post { Id = 194, Title = "Top 10 Tech Trends of 2021", Text = "vulputate vitae", Description = "How AI is Revolutionizing Healthcare", UserId = 36, Tags = "internet,innovation", PostingTime = new DateTime(2024, 6, 18) },
    new Post { Id = 195, Title = "Cybersecurity in the Digital Age", Text = "Morbi quis tortor id nulla ultrices aliquet.", Description = "Top 10 Tech Trends for 2021", UserId = 20, Tags = "digital,internet", PostingTime = new DateTime(2023, 9, 27) },
    new Post { Id = 196, Title = "Machine Learning Demystified", Text = "Nulla ac enim. In tempor", Description = "5 Tips for Effective Remote Work", UserId = 62, Tags = "gadgets,AI", PostingTime = new DateTime(2021, 2, 10) },
    new Post { Id = 197, Title = "Cybersecurity in the Digital Age", Text = "hendrerit at", Description = "5 Tips for Effective Remote Work", UserId = 12, Tags = "AI,digital", PostingTime = new DateTime(2020, 4, 18) },
    new Post { Id = 198, Title = "Cloud Computing Simplified", Text = "adipiscing molestie", Description = "5 Tips for Effective Remote Work", UserId = 99, Tags = "software,digital", PostingTime = new DateTime(2020, 2, 28) },
    new Post { Id = 199, Title = "E-commerce Trends to Watch Out For", Text = "Vivamus metus arcu", Description = "Top 10 Tech Trends for 2021", UserId = 22, Tags = "coding,technology", PostingTime = new DateTime(2020, 9, 22) },
    new Post { Id = 200, Title = "Cloud Computing Simplified", Text = "vulputate vitae", Description = "How AI is Revolutionizing Healthcare", UserId = 59, Tags = "software,internet", PostingTime = new DateTime(2021, 10, 16) }
};

        foreach (Post post in posts)
        {
            int num = random.Next(1, 31);
            if (num == 23)
            {
                while (num == 23)
                {
                    num = random.Next(1, 31);
                }
            }
            post.ImageId = num;
        }

        var postsCSharpSyntax = "var posts = new List<Post>\n{\n" +
                                 string.Join(",\n", posts.Select(post =>
                                 $"    new Post {{ Id = {post.Id}, Title = \"{post.Title}\", Text = \"{post.Text}\", Description = \"{post.Description}\", UserId = {post.UserId}, Tags = \"{post.Tags}\", PostingTime = new DateTime({post.PostingTime.Year}, {post.PostingTime.Month}, {post.PostingTime.Day}), ImageId = {post.ImageId} }}")) +
                                 "\n};";

        await File.WriteAllTextAsync(outputFilePath, postsCSharpSyntax);
    }


    public static async Task WriteJsonPostsToFileAsync(string jsonFilePath)
        {
            var jsonData = await File.ReadAllTextAsync(jsonFilePath);

            var options = new JsonSerializerOptions
            {
                Converters =
            {
                new CustomPostConverter() 
            }
            };

            var posts = JsonSerializer.Deserialize<List<Post>>(jsonData, options);

            if (posts == null) return;

            var postsCSharpSyntax = "var posts = new List<Post>\n{\n" +
                                     string.Join(",\n", posts.Select(post =>
                                     $"    new Post {{ Id = {post.Id}, Title = \"{post.Title}\", Text = \"{post.Text}\", Description = \"{post.Description}\", UserId = {post.UserId}, Tags = \"{post.Tags}\", PostingTime = new DateTime({post.PostingTime.Year}, {post.PostingTime.Month}, {post.PostingTime.Day}) }}")) +
                                     "\n};";

            var directory = Path.GetDirectoryName(jsonFilePath);
            var outputFilePath = Path.Combine(directory, "PostsList.cs");

            await File.WriteAllTextAsync(outputFilePath, postsCSharpSyntax);
        }
    


    public static async Task WriteStarsToFileAsync(string jsonFilePath)
    {
        var jsonData = await File.ReadAllTextAsync(jsonFilePath);

        var stars = JsonSerializer.Deserialize<List<Star>>(jsonData);

        if (stars == null) return;

        var starsCSharpSyntax = "var stars = new List<Star>\n{\n" +
                                string.Join(",\n", stars.Select(star =>
                                $"    new Star {{ Id = {star.Id}, UserId = {star.UserId}, PostId = {star.PostId}, Rating = {star.Rating} }}")) +
                                "\n};";

        var directory = Path.GetDirectoryName(jsonFilePath);
        var outputFilePath = Path.Combine(directory, "StarsList.cs");

        await File.WriteAllTextAsync(outputFilePath, starsCSharpSyntax);
    }

}

public class JsonObject : Dictionary<string, object>
{
    public void Add(string key, string value)
    {
        this[key] = value;
    }
    public void Add(string key, int value)
    {
        this[key] = value;
    }

    public void Add(string key, DateTime value)
    {
        this[key] = value.ToString("o"); 
    }

    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }
}

public class CustomPostConverter : JsonConverter<Post>
{
    public override Post Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var post = new Post();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                break;

            // Read the property name
            var propertyName = reader.GetString();
            reader.Read(); // Move to the property value

            switch (propertyName)
            {
                case "Title":
                    post.Title = reader.GetString();
                    break;

                case "Id":
                    post.Id = reader.GetInt32();
                    break;

                case "Text":
                    post.Text = reader.GetString();
                    break;

                case "Description":
                    post.Description = reader.GetString();
                    break;

                case "UserId":
                    post.UserId = reader.GetInt32();
                    break;

                case "Tags":
                    if (reader.TokenType == JsonTokenType.StartArray)
                    {
                        var tagsList = new List<string>();
                        while (reader.Read())
                        {
                            if (reader.TokenType == JsonTokenType.EndArray)
                                break;

                            tagsList.Add(reader.GetString());
                        }
                        post.Tags = string.Join(",", tagsList);
                    }
                    break;

                case "PostingTime":
                    if (reader.TokenType == JsonTokenType.String)
                    {
                        string dateString = reader.GetString();
                        if (DateTime.TryParseExact(dateString, "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime postingTime))
                        {
                            post.PostingTime = postingTime;
                        }
                        else
                        {
                            throw new JsonException($"Invalid date format: {dateString}");
                        }
                    }
                    break;

                default:
                    break;
            }
        }

        return post;
    }

    public override void Write(Utf8JsonWriter writer, Post value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteString("Title", value.Title);
        writer.WriteNumber("Id", value.Id);
        writer.WriteString("Text", value.Text);
        writer.WriteString("Description", value.Description);
        writer.WriteNumber("UserId", value.UserId);
        writer.WriteString("Tags", value.Tags);
        writer.WriteString("PostingTime", value.PostingTime.ToString("M/d/yyyy", CultureInfo.InvariantCulture));

        writer.WriteEndObject();
    }
}



