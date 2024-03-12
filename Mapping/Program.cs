using System.Reflection;
using System.Text.Json;

namespace Mapping
{
    

    internal class Program
    {
        private static void Main(string[] args)
        {


            User user = new User()
            {
                Name = "ATOM",
                Email = "ATOM@gmail.com"
            };

            var userDTO = user.Map<UserDTO>();

            Console.WriteLine(JsonSerializer.Serialize(userDTO));
        }
    }

    public static class Mapper
    {
        public static TEntity Map<TEntity>(this object entity)
        {
            var newEntity = Activator.CreateInstance<TEntity>();
            var typeNewEntity = newEntity.GetType();
            var typeObject = entity.GetType();

            PropertyInfo[] properties = typeNewEntity.GetProperties();

            foreach (var property in properties)
            {
                var objectProperty = typeObject.GetProperty(property.Name);

                if (objectProperty != null)
                    property.SetValue(newEntity, objectProperty.GetValue(entity));
            }

            return (TEntity)newEntity;
        }
    }

    public class UserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
    }


}

