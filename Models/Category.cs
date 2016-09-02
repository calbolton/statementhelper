namespace StatementHelper.Models
{
    public class Category
    {
        public Category(string name, Category parentCategory)
        {
            Name = name;
            ParentCategory = parentCategory;
        }

        public string Name { get; set; }

        public Category ParentCategory { get; set; }
    }
}