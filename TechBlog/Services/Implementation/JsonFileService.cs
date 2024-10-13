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

        public static async Task WriteJsonPostsToFileAsync(string jsonFilePath)
        {
            // Read the JSON file
            var jsonData = await File.ReadAllTextAsync(jsonFilePath);

            // Create options with the custom converter
            var options = new JsonSerializerOptions
            {
                Converters =
            {
                new CustomPostConverter() // Use your custom converter directly
            }
            };

            // Deserialize the JSON to a list of Posts
            var posts = JsonSerializer.Deserialize<List<Post>>(jsonData, options);

            if (posts == null) return;

            // Convert the list to a C# syntax string, excluding lists
            var postsCSharpSyntax = "var posts = new List<Post>\n{\n" +
                                     string.Join(",\n", posts.Select(post =>
                                     $"    new Post {{ Id = {post.Id}, Title = \"{post.Title}\", Text = \"{post.Text}\", Description = \"{post.Description}\", UserId = {post.UserId}, Tags = \"{post.Tags}\", PostingTime = new DateTime({post.PostingTime.Year}, {post.PostingTime.Month}, {post.PostingTime.Day}) }}")) +
                                     "\n};";

            // Get the path to write the file
            var directory = Path.GetDirectoryName(jsonFilePath);
            var outputFilePath = Path.Combine(directory, "PostsList.cs");

            // Write the C# syntax string to a new file
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

    // Add an overload for adding integers or other types if necessary
    public void Add(string key, int value)
    {
        this[key] = value;
    }

    // You can add more overloads for other types as needed
    public void Add(string key, DateTime value)
    {
        this[key] = value.ToString("o"); // ISO 8601 format
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
                    // Handle Tags as a joined string
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
                    // Handle PostingTime
                    if (reader.TokenType == JsonTokenType.String)
                    {
                        string dateString = reader.GetString();
                        // Try parsing the date in the specified format
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
                    // Store the property as is if it's not explicitly handled
                    // You can skip this part or handle it as necessary
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



